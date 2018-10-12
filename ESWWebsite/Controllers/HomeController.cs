using ESWWebsite.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebsiteBackEnd.Models;
using WebsiteBackEnd.ResumeParse;

namespace ESWWebsite.Controllers
{
    public class HomeController : Controller
    {
        
        protected override void OnActionExecuting(ActionExecutingContext filterContext) {
            if (GetSessionUser() != null)
            {
                if(Shared.Constants.getSessionActionList().Exists(m=> m.Equals(filterContext.ActionDescriptor.ActionName))){
                    filterContext.Result =  RedirectToAction("Index", "Home");
                }
            }
            else
            {
                if (Shared.Constants.getEmptySessionActionList().Exists(m => m.Equals(filterContext.ActionDescriptor.ActionName)))
                {
                    filterContext.Result = RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult Logout()
        {
            SetSessionUser(null);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Error()
        {
            return View("~/Views/Shared/Error404.cshtml");
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            List<jpopening> lstjpopening = jpopeningManager.Getjpopening(" JOBSTATUS in ('O') ");
            lstjpopening = lstjpopening.OrderByDescending(o => o.JOBPOSTDATE).Take(4).ToList();
            ViewBag.RecentOpenings = lstjpopening;
            return View();
        }

        public ActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        public string Signin(jpuser model)
        {
            string ret = Shared.Constants.MSG_ERR_NOUSEREXIST.Text;
            List<jpuser> lstjpUSER = jpuserManager.Getjpuser(" EMAIL = '" + model.EMAIL + "'", null); //and PASSWORD = '"+model.PASSWORD+"'
            if (lstjpUSER.Count > 0)
            {
                if (lstjpUSER.First().PASSWORD.Equals(model.PASSWORD))
                {
                    lstjpUSER.First().objjpprofile = GetCompleteUserProfile(lstjpUSER.First().USERID);
                    lstjpUSER.First().isProfileComplete = isProfileComplete(lstjpUSER.First().objjpprofile);
                    SetSessionUser(lstjpUSER.First());

                    ret = Shared.Constants.MSG_SUCCESS.Text;
                }
                else
                {
                    ret = Shared.Constants.MSG_ERR_INVALIDCRED.Text;
                }
            }

            return ret;
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public string Signup(VMSignUp model)
        {
            List<jpuser> lstjpUSER = jpuserManager.Getjpuser(" USERNAME = '" + model.Username + "'", null);
            if (lstjpUSER.Count > 0)
            {
                return "Username already Exists";
            }

            lstjpUSER = jpuserManager.Getjpuser(" EMAIL = '" + model.Email + "'", null);
            if (lstjpUSER.Count > 0)
            {
                return "An account already exists with the given Email";
            }

            jpuser userobj = new jpuser();
            userobj.EMAIL = model.Email;
            userobj.PASSWORD = model.Password;
            userobj.USERNAME = model.Username;
            userobj.ACTIVE = "N";

            jpprofile profileobj = new jpprofile();
            profileobj.NAME = " ";
            profileobj.ADDRESS = " ";
            profileobj.CONTACT = model.Contact;
            profileobj.DOB = DateTime.ParseExact(model.DOB, Shared.Constants.DATE_RFC_FORMAT, CultureInfo.InvariantCulture);
            profileobj.GENDER = model.Gender;

            MySqlConnection conn = Shared.BaseManager.PrimaryConnection();
            conn.Open();
            var transaction = conn.BeginTransaction();
            string ret = jpuserManager.Savejpuser(userobj, conn, transaction);
            if (!ret.Equals(Shared.Constants.MSG_ERR_DBSAVE.Text))
            {
                profileobj.USERID = int.Parse(ret);
                ret = jpprofileManager.Savejpprofile(profileobj, conn, transaction);
                if (ret.Equals(Shared.Constants.MSG_OK_DBSAVE.Text))
                {
                    transaction.Commit();
                    conn.Close();
                    conn.Dispose();
                    //add verification url code here
                    userobj.USERID = profileobj.USERID;
                    string _token = (string)getVerificationURL(userobj);
                    
                    return sendEmail(userobj.EMAIL, _token);
                }
            }

            transaction.Rollback();
            conn.Close();
            conn.Dispose();
            return Shared.Constants.MSG_ERR_SERVER.Text;
        }

        public ActionResult MyProfile()
        {
            jpuser SessionUser = GetSessionUser();
            if(SessionUser!=null)
            {
                if (SessionUser.objjpprofile == null)
                {
                    SessionUser.objjpprofile = GetCompleteUserProfile(SessionUser.USERID);
                    SetSessionUser(SessionUser);
                    return View(SessionUser.objjpprofile);
                }
                else
                {
                    return View(SessionUser.objjpprofile);
                }
            }
            return View(new jpprofile());
        }

        [HttpPost]
        public string MyProfile(jpprofile model)
        {
            jpuser SessionUser = GetSessionUser();
            if (SessionUser != null)
            {
                if (SessionUser.objjpprofile != null)
                {
                    if (SessionUser.objjpprofile.lstjpqualification.Count > 0)
                    {
                        if (SessionUser.objjpprofile.lstjpexperience.Count > 0)
                        {
                            SessionUser.objjpprofile.CONTACT = model.CONTACT;
                            SessionUser.objjpprofile.CURRENTSALARY = model.CURRENTSALARY;
                            SessionUser.objjpprofile.NAME = model.NAME;
                            SessionUser.objjpprofile.GENDER = model.GENDER;

                            SessionUser.objjpprofile.lstjpqualification.FindAll(a=> a.isNewRecord).Select(c => { c.QUALIFICATIONID = 0; return c; }).ToList();
                            SessionUser.objjpprofile.lstjpexperience.FindAll(a => a.isNewRecord).Select(c => { c.EXPID = 0; return c; }).ToList();

                            MySqlConnection conn = Shared.BaseManager.PrimaryConnection();
                            conn.Open();
                            var transaction = conn.BeginTransaction();
                            string ret = jpprofileManager.Savejpprofile(SessionUser.objjpprofile, conn, transaction);
                            if (ret.Equals(Shared.Constants.MSG_OK_DBSAVE))
                            {
                                bool EduContinue = true;
                                //ret = jpqualificationManager.Deletejpqualification()
                                foreach(var edu in SessionUser.objjpprofile.lstjpqualification)
                                {
                                    ret = jpqualificationManager.Savejpqualification(edu, conn, transaction);
                                    if (!ret.Equals(Shared.Constants.MSG_OK_DBSAVE))
                                    {
                                        EduContinue = false;
                                        break;
                                    }
                                }

                                if (EduContinue)
                                {
                                    bool ExpContinue = true;
                                    foreach (var exp in SessionUser.objjpprofile.lstjpexperience)
                                    {
                                        ret = jpexperienceManager.Savejpexperience(exp, conn, transaction);
                                        if (!ret.Equals(Shared.Constants.MSG_OK_DBSAVE))
                                        {
                                            ExpContinue = false;
                                            break;
                                        }
                                    }
                                    if (ExpContinue)
                                    {
                                        transaction.Commit();
                                        conn.Close();
                                        conn.Dispose();

                                        SessionUser.objjpprofile = GetCompleteUserProfile(SessionUser.USERID);
                                        SessionUser.isProfileComplete = isProfileComplete(SessionUser.objjpprofile);
                                        SetSessionUser(SessionUser);

                                        return Shared.Constants.MSG_SUCCESS.Text;
                                    }
                                }
                            }

                            transaction.Rollback();
                            conn.Close();
                            conn.Dispose();
                            return Shared.Constants.MSG_ERR_SERVER.Text;
                        }
                    }
                    
                }
            }

            return Shared.Constants.MSG_ERROR.Text;
        }

        public ActionResult _EduTabView()
        {   
            jpuser SessionUser = GetSessionUser();
            if (SessionUser != null)
            {
                if(SessionUser.objjpprofile != null)
                {
                    if(SessionUser.objjpprofile.lstjpqualification != null)
                    {
                        return PartialView(SessionUser.objjpprofile.lstjpqualification);
                    }
                }
            }
            
            return PartialView(new List<jpqualification>());
        }

        public ActionResult _EduFormView(string id)
        {
            jpuser SessionUser = GetSessionUser();
            if (!string.IsNullOrEmpty(id))
            {
                if (SessionUser != null)
                {
                    if (SessionUser.objjpprofile != null)
                    {
                        if (SessionUser.objjpprofile.lstjpqualification != null)
                        {
                            jpqualification obj = SessionUser.objjpprofile.lstjpqualification.Find(q => q.QUALIFICATIONID == int.Parse(id) && q.PROFILEID.Equals(SessionUser.objjpprofile.PROFILEID));
                            if(obj != null)
                            {
                                return PartialView(obj);
                            }
                        }
                    }
                }
            }
            return PartialView(new jpqualification() { isNewRecord = true });
        }

        [HttpPost]
        public string _EduFormView(jpqualification model)
        {
            jpuser SessionUser = GetSessionUser();
            if (model != null)
            {
                if (SessionUser != null)
                {
                    if (SessionUser.objjpprofile != null)
                    {
                        if (SessionUser.objjpprofile.lstjpqualification != null)
                        {
                            model.PROFILEID = SessionUser.objjpprofile.PROFILEID;
                            if(model.QUALIFICATIONID == 0)
                            {
                                model.QUALIFICATIONID = GetTempID(SessionUser.objjpprofile.lstjpqualification);
                            }
                            else
                            {
                                jpqualification obj = SessionUser.objjpprofile.lstjpqualification.Find(a => a.QUALIFICATIONID == model.QUALIFICATIONID);
                                if(obj != null)
                                {
                                    SessionUser.objjpprofile.lstjpqualification.Remove(obj);
                                }
                            }
                            SessionUser.objjpprofile.lstjpqualification.Add(model);
                            return Shared.Constants.MSG_SUCCESS.Text;
                            
                        }
                    }
                }
            }
            return Shared.Constants.MSG_ERROR.Text;
        }

        public string _EduRecRemove(string id)
        {
            jpuser SessionUser = GetSessionUser();
            if (!string.IsNullOrEmpty(id))
            {
                if (SessionUser != null)
                {
                    if (SessionUser.objjpprofile != null)
                    {
                        if (SessionUser.objjpprofile.lstjpqualification != null)
                        {
                            jpqualification obj = SessionUser.objjpprofile.lstjpqualification.Find(q => q.QUALIFICATIONID == int.Parse(id) && q.PROFILEID.Equals(SessionUser.objjpprofile.PROFILEID));
                            if (obj != null)
                            {
                                var ret = jpqualificationManager.Deletejpqualification(obj.QUALIFICATIONID);
                                if (ret.Equals(Shared.Constants.MSG_OK_DBSAVE))
                                {
                                    SessionUser.objjpprofile.lstjpqualification.Remove(obj);
                                    return Shared.Constants.MSG_SUCCESS.Text;
                                }
                            }
                        }
                    }
                }
            }
            return Shared.Constants.MSG_ERROR.Text;
        }

        public ActionResult _ExpTabView()
        {
            jpuser SessionUser = GetSessionUser();
            if (SessionUser != null)
            {
                if (SessionUser.objjpprofile != null)
                {
                    if (SessionUser.objjpprofile.lstjpexperience != null)
                    {
                        return PartialView(SessionUser.objjpprofile.lstjpexperience);
                    }
                }
            }

            return PartialView(new List<jpexperience>());
        }

        public ActionResult _ExpFormView(string id)
        {
            jpuser SessionUser = GetSessionUser();
            if (!string.IsNullOrEmpty(id))
            {
                if (SessionUser != null)
                {
                    if (SessionUser.objjpprofile != null)
                    {
                        if (SessionUser.objjpprofile.lstjpexperience != null)
                        {
                            jpexperience obj = SessionUser.objjpprofile.lstjpexperience.Find(q => q.EXPID == int.Parse(id) && q.PROFILEID.Equals(SessionUser.objjpprofile.PROFILEID));
                            if (obj != null)
                            {
                                return PartialView(obj);
                            }
                        }
                    }
                }
            }
            return PartialView(new jpexperience() { isNewRecord = true});
        }

        [HttpPost]
        public string _ExpFormView(jpexperience model)
        {
            jpuser SessionUser = GetSessionUser();
            if (model != null)
            {
                if (SessionUser != null)
                {
                    if (SessionUser.objjpprofile != null)
                    {
                        if (SessionUser.objjpprofile.lstjpexperience != null)
                        {
                            model.PROFILEID = SessionUser.objjpprofile.PROFILEID;
                            if (model.EXPID == 0)
                            {
                                model.EXPID = GetTempID(SessionUser.objjpprofile.lstjpexperience);
                            }
                            else
                            {
                                jpexperience obj = SessionUser.objjpprofile.lstjpexperience.Find(a => a.EXPID == model.EXPID);
                                if (obj != null)
                                {
                                    SessionUser.objjpprofile.lstjpexperience.Remove(obj);
                                }
                            }
                            SessionUser.objjpprofile.lstjpexperience.Add(model);
                            return Shared.Constants.MSG_SUCCESS.Text;

                        }
                    }
                }
            }
            return Shared.Constants.MSG_ERROR.Text;
        }

        public string _ExpRecRemove(string id)
        {
            jpuser SessionUser = GetSessionUser();
            if (!string.IsNullOrEmpty(id))
            {
                if (SessionUser != null)
                {
                    if (SessionUser.objjpprofile != null)
                    {
                        if (SessionUser.objjpprofile.lstjpexperience != null)
                        {
                            jpexperience obj = SessionUser.objjpprofile.lstjpexperience.Find(q => q.EXPID == int.Parse(id) && q.PROFILEID.Equals(SessionUser.objjpprofile.PROFILEID));
                            if (obj != null)
                            {
                                var ret = jpexperienceManager.Deletejpexperience(obj.EXPID);
                                if (ret.Equals(Shared.Constants.MSG_OK_DBSAVE))
                                {
                                    SessionUser.objjpprofile.lstjpexperience.Remove(obj);
                                    return Shared.Constants.MSG_SUCCESS.Text;
                                }
                            }
                        }
                    }
                }
            }
            return Shared.Constants.MSG_ERROR.Text;
        }

        public void ParseResumeReq()
        {
            ParseResume obj = new ParseResume();
            //obj.ParsePdfResume();
        }

        public ActionResult ListJobs(string search) {
            ViewBag.navActive = Shared.Constants.SP_LIST_JOBS;
            if (!string.IsNullOrEmpty(search))
                ViewBag.Search = search;

            return View();
        }

        public ActionResult JobCards(string id) {
            List<jpopening> lstjpopening = jpopeningManager.Getjpopening(" JOBSTATUS in ('O') ");
            if (!string.IsNullOrEmpty(id)) {
                id = id.ToLower();
                lstjpopening = lstjpopening.FindAll(x => 
                x.JOBTITLE.ToLower().Contains(id) || 
                x.COMPANYNAME.ToLower().Contains(id) || 
                x.CITYNAME.ToLower().Contains(id) || 
                x.COUNTRYNAME.ToLower().Contains(id) || 
                x.DEPARTMENT.ToLower().Contains(id) || 
                x.SHORTDESC.ToLower().Contains(id) || 
                x.MINIMUMEDUCATION.ToLower().Contains(id));
            }
            return PartialView(lstjpopening);
        }

        public ActionResult JobDetail(string id) {
            List<jpopening> lstjpopening = jpopeningManager.Getjpopening("OPENINGID = '"+id+"'");
            if (lstjpopening.Count > 0)
            {
                return PartialView(lstjpopening[Shared.Constants.ZERO]);
            }

            return PartialView(new jpopening());
        }

        public string validateApplyJob(string id)
        {
            if (GetSessionUser() == null)
            {
                return Shared.Constants.MSG_SESSION_USER_EMPTY.Text;
            }
            else
            {
                jpuser SessionUser = GetSessionUser();
                if (SessionUser.ACTIVE.Equals(Shared.Constants.STR_NO))
                {
                    return Shared.Constants.MSG_SESSION_USER_UNV.Text;
                }
                if (!SessionUser.isProfileComplete)
                {
                    return Shared.Constants.MSG_SESSION_USER_INC_PROFILE.Text;
                }
                List<jpopening> lstopening = jpopeningManager.Getjpopening("OPENINGID = '" + id + "'");
                if (lstopening.Count > 0)
                {
                    if (lstopening[Shared.Constants.ZERO].JOBSTATUS.Equals("C"))
                    {
                        return Shared.Constants.MSG_POS_CLOSED.Text;
                    }
                }
                List<jpopeningapplication> lst = jpopeningapplicationManager.Getjpopeningapplication("OPENINGID = '" + id + "' and PROFILEID = '" + SessionUser.objjpprofile.PROFILEID + "'");
                if (lst.Count > 0)
                {
                    return Shared.Constants.MSG_ALREADY_APPLIED.Text;
                }
                else
                {
                    return Shared.Constants.MSG_SUCCESS.Text;
                }
            }
        }
        
        [HttpPost]
        public string ApplyJobCV(FormCollection fc) {
            //string Name = Request.Form[1];
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                byte[] fileInBytes = new byte[file.ContentLength];
                using (BinaryReader theReader = new BinaryReader(file.InputStream))
                {
                    fileInBytes = theReader.ReadBytes(file.ContentLength);
                }
                string fileAsString = Convert.ToBase64String(fileInBytes);

                jpuser SessionUser = GetSessionUser();
                SessionUser.objjpprofile.LONGCV = fileAsString;
                string ret = jpprofileManager.Savejpprofile(SessionUser.objjpprofile);
                
                if (ret.Equals(Shared.Constants.MSG_OK_DBSAVE))
                {
                    string id = fc["id"].ToString();
                    string expsal = fc["expsal"].ToString();
                    if (ApplyJob(id, expsal).Equals(Shared.Constants.MSG_SUCCESS))
                        return Shared.Constants.MSG_SUCCESS.Text;
                }
            }

            return Shared.Constants.MSG_ERROR.Text;
        }
        public string ApplyJob(string id, string expsal) {
            
            jpuser SessionUser = GetSessionUser();
            
            List<jpopening> lstopening = jpopeningManager.Getjpopening("OPENINGID = '" + id + "'");
            List<jpopeningapplication> lst = jpopeningapplicationManager.Getjpopeningapplication("OPENINGID = '" + id + "' and PROFILEID = '"+ SessionUser.objjpprofile.PROFILEID + "'");
                
            jpopeningapplication obj = new jpopeningapplication();
            obj.OPENINGID = int.Parse(id);
            obj.PROFILEID = SessionUser.objjpprofile.PROFILEID;
            obj.EXPECTEDSALARY = expsal;
            obj.APPLIEDDATE = DateTime.Now;

            string ret = jpopeningapplicationManager.Savejpopeningapplication(obj);
            if (ret.Equals(Shared.Constants.MSG_OK_DBSAVE))
                return Shared.Constants.MSG_SUCCESS.Text;

            return Shared.Constants.MSG_ERROR.Text;
                
        }

        private string sendEmail(string recipient, string token) {
            MailMessage mail = new MailMessage(ConfigurationManager.AppSettings[Shared.Constants.ASK_MAILING_ACC], recipient);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = ConfigurationManager.AppSettings[Shared.Constants.ASK_MAIL_SMTP_HOST];
            mail.Subject = "Verification link from " + ConfigurationManager.AppSettings[Shared.Constants.ASK_ORG_NAME];
            mail.Body = "Please click on this URL to get verified on our system : " + ConfigurationManager.AppSettings[Shared.Constants.ASK_BASE_URL] + token;
            try
            {
                client.Send(mail);
                return Shared.Constants.MSG_SUCCESS.Text;
            }
            catch(Exception ex)
            {
                return Shared.Constants.MSG_ERROR.Text;
            }
            
            
        }

        private object getVerificationURL(jpuser obj) {
            var payload = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(prop => prop.Name, prop => prop.GetValue(obj, null));
            return Shared.Utility.encodeJWT(payload);
        }

        public ActionResult Verify(string token) {
            if (!string.IsNullOrEmpty(token))
            {
                object jsonJWT = Shared.Utility.decodeJWT(token);
                if (jsonJWT.GetType() == typeof(jpuser))
                {
                    jpuser obj = (jpuser)jsonJWT;
                    List<jpuser> lst = jpuserManager.Getjpuser("USERID = '"+obj.USERID+ "'");
                    if (lst.Count > 0)
                    {
                        if (!lst[Shared.Constants.ZERO].ACTIVE.Equals(Shared.Constants.STR_YES))
                        {
                            obj.ACTIVE = Shared.Constants.STR_YES;
                            string ret = jpuserManager.Savejpuser(obj);
                            if (ret.Equals(Shared.Constants.MSG_OK_DBSAVE))
                            {
                                obj.objjpprofile = GetCompleteUserProfile(obj.USERID);
                                SetSessionUser(obj);
                                return RedirectToAction("Index");
                            }
                        }
                        return RedirectToAction("Index");
                    }
                }
            }

            return RedirectToAction("Error");
        }

        public string ReSendVerfLink() {
            if(GetSessionUser()!= null)
            {
                jpuser obj = GetSessionUser();
                string ret = sendEmail(obj.EMAIL, (string)getVerificationURL(obj));
                if (ret.Equals(Shared.Constants.MSG_SUCCESS))
                {
                    obj.isVerifyLinkSent = true;
                    SetSessionUser(obj);
                }
                return ret;
            }
            return Shared.Constants.MSG_SESSION_USER_EMPTY.Text;
        }

        public ActionResult _AppliedJobs() {

            jpuser SessionUser = GetSessionUser();
            if (SessionUser != null)
            {
                if (SessionUser.objjpprofile != null)
                {
                    List<jpopeningapplication> lstjpopeningapplication = jpopeningapplicationManager.Getjpopeningapplication("PROFILEID = '"+ SessionUser.objjpprofile.PROFILEID + "'");
                    if(lstjpopeningapplication.Count > 0)
                    {
                        string joined = string.Join(",", lstjpopeningapplication.Select(x => x.OPENINGID));
                        List<jpopening> lstjpopening = jpopeningManager.Getjpopening("OPENINGID in (" + joined + ")");
                    }
                    
                    //ExpectedSalary
                    /*
                    if (SessionUser.objjpprofile.lstjpexperience != null)
                    {
                        return PartialView(SessionUser.objjpprofile.lstjpexperience);
                    }
                    */
                }

            }
            return PartialView();
        }

        #region ::Static Pages:: 
        public ActionResult CareerCounselling()
        {
            ViewBag.navActive = Shared.Constants.SP_CAREER_COUNCIL;
            return View();

        }

        public ActionResult OurClients()
        {
            ViewBag.navActive = Shared.Constants.SP_CLIENT;
            return View();

        }

        public ActionResult Outsourcing()
        {
            ViewBag.navActive = Shared.Constants.SP_OUTSOURCE;
            return View();

        }

        public ActionResult Training()
        {
            ViewBag.navActive = Shared.Constants.SP_TRAIN_DEV;
            return View();

        }

        public ActionResult SoftwareServices()
        {
            ViewBag.navActive = Shared.Constants.SP_SOFTWARE_SRVS;
            return View();

        }

        public ActionResult AboutUs()
        {
            ViewBag.navActive = Shared.Constants.SP_ABOUT_US;
            return View();
        }
        #endregion

        #region ::General Functions::
        public jpuser GetSessionUser()
        {
            if(Session[Shared.Constants.SESSION_USER]!=null)
            {
                return Session[Shared.Constants.SESSION_USER] as jpuser;
            }

            return null;
        }

        public void SetSessionUser(jpuser obj)
        {
            Session[Shared.Constants.SESSION_USER] = obj;
        }
        
        public jpprofile GetCompleteUserProfile(int USERID)
        {
            List<jpprofile> lstjpProfile = jpprofileManager.Getjpprofile("USERID = '" + USERID + "'");
            if (lstjpProfile.Count > 0)
            {
                lstjpProfile.First().lstjpqualification = jpqualificationManager.Getjpqualification("PROFILEID = '" + lstjpProfile.First().PROFILEID + "'");
                lstjpProfile.First().lstjpexperience = jpexperienceManager.Getjpexperience("PROFILEID = '" + lstjpProfile.First().PROFILEID + "'");
                
                return lstjpProfile.First();
            }

            return new jpprofile();
        }

        public int GetTempID(List<jpqualification> lst)
        {
            int id = lst.Count() + 1;
            while(lst.Exists(a=> a.QUALIFICATIONID == id))
            {
                id++;
            }
            return id;
        }

        public int GetTempID(List<jpexperience> lst)
        {
            int id = lst.Count() + 1;
            while (lst.Exists(a => a.EXPID == id))
            {
                id++;
            }
            return id;
        }

        private bool isProfileComplete(jpprofile objjpprofile) {
            if (!string.IsNullOrEmpty(objjpprofile.NAME))
            {
                if (!string.IsNullOrEmpty(objjpprofile.CURRENTSALARY))
                {
                    if (objjpprofile.lstjpqualification.Count > 0)
                    {
                        if (objjpprofile.lstjpexperience.Count > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
