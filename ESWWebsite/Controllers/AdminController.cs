using Newtonsoft.Json;
using Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebsiteBackEnd.Models;

namespace ESWWebsite.Controllers
{
    public class AdminController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (GetSessionAdmin() != null)
            {
                if (filterContext.ActionDescriptor.ActionName.Equals("Signin"))
                {
                    filterContext.Result = RedirectToAction("Index", "Admin");
                }
            }
            else
            {
                if (!filterContext.ActionDescriptor.ActionName.Equals("Signin"))
                {
                    filterContext.Result = RedirectToAction("Signin", "Admin");
                }
            }
        }
        // GET: Admin
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public string Signin(jpadmin model)
        {
            string ret = Shared.Constants.MSG_ERROR.Text;
            List<jpadmin> lstjpADMIN = jpadminManager.Getjpadmin(" ADMINUSERNAME = '" + model.ADMINUSERNAME + "'", null); //and PASSWORD = '"+model.PASSWORD+"'
            if (lstjpADMIN.Count > 0)
            {
                if (lstjpADMIN.First().PASSWORD.Equals(model.PASSWORD))
                {
                    SetSessionAdmin(lstjpADMIN.First());
                    ret = Shared.Constants.MSG_SUCCESS.Text;
                }
                else
                {
                    ret = Shared.Constants.MSG_ERR_INVALIDCRED.Text;
                }
            }

            return ret;
        }

        public ActionResult Logout()
        {
            SetSessionAdmin(null);
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult Index()
        {
            return View();
        }

        public string GetCountryList(string id) {
            var lst = Shared.Constants.lstCountryCity.FindAll(a => a.Country.ToLower().Contains(id.ToLower())).Select(c => c.Country).Distinct().OrderBy(a=> a).ToList();
            return JsonConvert.SerializeObject(lst);
        }

        public string GetCityList(string id)
        {
            var lst = Shared.Constants.lstCountryCity.FindAll(a => a.Country.ToLower().Contains(id.ToLower())).Select(c => c.City).Distinct().OrderBy(a => a).ToList();
            return JsonConvert.SerializeObject(lst);
        }

        public ActionResult Dashboard()
        {
            return PartialView();
        }

        public ActionResult ListJobs()
        {
            List<jpopening> lstjpopening = jpopeningManager.Getjpopening(null);
            return PartialView(lstjpopening);
        }

        public ActionResult ListJobApplications(string id)
        {
            List<jpopeningapplication> lstjpopapp = jpopeningapplicationManager.Getjpopeningapplication("openingid = '" + id + "'");
            if (lstjpopapp.Count > 0)
            {
                string profiles = string.Join(",", lstjpopapp.Select(a => a.PROFILEID).ToList());
                List<jpprofile> lstprofile = jpprofileManager.Getjpprofile(" profileid in (" + profiles + ")");
                if (lstprofile.Count > 0)
                {
                    foreach (var profile in lstprofile)
                    {
                        profile.expectedSalary = lstjpopapp.Find(a => a.PROFILEID == profile.PROFILEID).EXPECTEDSALARY;
                        profile.lstjpqualification = jpqualificationManager.Getjpqualification("PROFILEID = '" + profile.PROFILEID + "'").OrderByDescending(d => d.PASSINGYEAR.Value).ToList();
                        profile.lstjpexperience = jpexperienceManager.Getjpexperience("PROFILEID = '" + profile.PROFILEID + "'").OrderByDescending(d => d.STARTINGYEAR.Value).ToList();
                    }
                }
                return PartialView(lstprofile);
            }

            return PartialView(new List<jpprofile>());
        }

        public ActionResult PostJob(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                List<jpopening> lstjpopening = jpopeningManager.Getjpopening(" OPENINGID ='" + id + "'");
                if (lstjpopening.Count > 0)
                {
                    return PartialView(lstjpopening.First());
                }
                
            }
            return PartialView(new jpopening());
        }

        [HttpPost]
        public string PostJob(jpopening model)
        {
            model.JOBPOSTDATE = DateTime.Now;
            string ret = jpopeningManager.Savejpopening(model);
            if (ret.Equals(Shared.Constants.MSG_OK_DBSAVE.Text))
            {
                return Shared.Constants.MSG_SUCCESS.Text;
            }

            return Shared.Constants.MSG_ERROR.Text;
        }

        public jpadmin GetSessionAdmin()
        {
            if (Session[Shared.Constants.SESSION_ADMIN] != null)
            {
                return Session[Shared.Constants.SESSION_ADMIN] as jpadmin;
            }

            return null;
        }

        public void SetSessionAdmin(jpadmin obj)
        {
            Session[Shared.Constants.SESSION_ADMIN] = obj;
        }
        
        public string DownloadUploadedCV(string id) {
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            #region ::make pdf file::
            List<jpuser> lstjpUSER = jpuserManager.Getjpuser(" USERID = '" + id + "'", null);
            if (lstjpUSER.Count > 0)
            {
                lstjpUSER.First().objjpprofile = GetCompleteUserProfile(lstjpUSER.First().USERID);
                //lstjpUSER.First().objjpprofile.EMAIL = lstjpUSER.First().EMAIL;

                if (lstjpUSER.First().objjpprofile != null)
                {
                    try
                    {
                        System.IO.File.WriteAllBytes(basePath + ConfigurationManager.AppSettings[Shared.Constants.ASK_RESPDF_FNAME], Convert.FromBase64String(lstjpUSER.First().objjpprofile.LONGCV));
                        #region copy pdf to directory
                        string resumeDIR = basePath + ConfigurationManager.AppSettings[Shared.Constants.ASK_RESPDF_DIRECTORY] + "//";

                        if (!System.IO.Directory.Exists(resumeDIR))
                        {
                            System.IO.Directory.CreateDirectory(resumeDIR);
                        }


                        if (System.IO.File.Exists(basePath + ConfigurationManager.AppSettings[Shared.Constants.ASK_RESPDF_FNAME]))
                        {
                            System.IO.File.Copy(basePath + ConfigurationManager.AppSettings[Shared.Constants.ASK_RESPDF_FNAME], resumeDIR + ConfigurationManager.AppSettings[Shared.Constants.ASK_RESPDF_FNAME], true);
                            System.IO.File.Delete(basePath + ConfigurationManager.AppSettings[Shared.Constants.ASK_RESPDF_FNAME]);

                            return Shared.Constants.MSG_SUCCESS.Text;
                        }
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        Logger._log.Error(ex.Message + "\n" + ex.StackTrace);
                        return Shared.Constants.MSG_ERROR.Text;
                    }
                }
            }
            return Shared.Constants.MSG_ERROR.Text;
            #endregion
        }
        public async Task<string> DownloadCV(string id)
        {
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            string nodeSrvpath = ConfigurationManager.AppSettings[Shared.Constants.ASK_NODE_SRV_PATH];

            #region ::make json object::
            string json = "";
            List<jpuser> lstjpUSER = jpuserManager.Getjpuser(" USERID = '" + id + "'", null);
            if (lstjpUSER.Count > 0)
            {
                lstjpUSER.First().objjpprofile = GetCompleteUserProfile(lstjpUSER.First().USERID);
         //       lstjpUSER.First().objjpprofile.EMAIL = lstjpUSER.First().EMAIL;

                jsonProfileWrapper obj = new jsonProfileWrapper();
                obj.basics = lstjpUSER.First().objjpprofile;
                obj.education = lstjpUSER.First().objjpprofile.lstjpqualification;
                obj.work = lstjpUSER.First().objjpprofile.lstjpexperience;

                json = JsonConvert.SerializeObject(obj);
            }

            if (!string.IsNullOrEmpty(json)) {
                try
                {
                    System.IO.File.WriteAllText(nodeSrvpath + ConfigurationManager.AppSettings[Shared.Constants.ASK_RESJSON_FNAME], json);
                }
                catch (Exception ex) {
                    Logger._log.Error(ex.Message + "\n" + ex.StackTrace);
                    return Shared.Constants.MSG_ERROR.Text;
                }
            }
            #endregion

            try
            {
                #region ::make resume pdf (NodeJS process)::

                #region Batch CMD Process code
                /*ProcessStartInfo ProcessInfo;
                Process process;

                ProcessInfo = new ProcessStartInfo(basePath + ConfigurationManager.AppSettings[Shared.Constants.ASK_RESBAT_FNAME] , "");
                ProcessInfo.CreateNoWindow = false;
                ProcessInfo.UseShellExecute = false;
                ProcessInfo.WorkingDirectory = basePath;
                // *** Redirect the output ***
                ProcessInfo.RedirectStandardError = true;
                ProcessInfo.RedirectStandardOutput = true;

                process = Process.Start(ProcessInfo);
                if (process.WaitForExit(5000))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.Close();
                }
                else {
                    process.Close();
                }*/
                #endregion

                var client = new HttpClient();
                var queryString = HttpUtility.ParseQueryString(string.Empty);

                // Request headers
                //client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "444085ce3f10484997c9aed8f7da8327");

                var uri = ConfigurationManager.AppSettings[Shared.Constants.ASK_NODE_SRVURL];//constant

                var response = await client.GetAsync(uri);
                string body = await response.Content.ReadAsStringAsync();
                try
                {
                    var obj = (body);
                    if (obj.Equals(ConfigurationManager.AppSettings[Shared.Constants.ASK_NODE_SRVURL_RETSUCCESS]))//constant
                    {
                        #region copy pdf to directory
                        string resumeDIR = basePath + ConfigurationManager.AppSettings[Shared.Constants.ASK_RESPDF_DIRECTORY] + "//";

                        if (!System.IO.Directory.Exists(resumeDIR))
                        {
                            System.IO.Directory.CreateDirectory(resumeDIR);
                        }


                        if (System.IO.File.Exists(nodeSrvpath + ConfigurationManager.AppSettings[Shared.Constants.ASK_RESPDF_FNAME]))
                        {
                            System.IO.File.Copy(nodeSrvpath + ConfigurationManager.AppSettings[Shared.Constants.ASK_RESPDF_FNAME], resumeDIR + ConfigurationManager.AppSettings[Shared.Constants.ASK_RESPDF_FNAME], true);

                            //byte[] bytes = System.IO.File.ReadAllBytes(basePath + ConfigurationManager.AppSettings[Shared.Constants.ASK_RESPDF_FNAME]);
                            //string file = Convert.ToBase64String(bytes);

                            System.IO.File.Delete(nodeSrvpath + ConfigurationManager.AppSettings[Shared.Constants.ASK_RESPDF_FNAME]);

                            return Shared.Constants.MSG_SUCCESS.Text;
                        }
                        
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    Logger._log.Error(ex.Message + "\n" + ex.StackTrace);
                }

                #endregion

                //return Shared.Constants.MSG_ERROR.Text;
            }
            catch (Exception ex)
            {
                Logger._log.Error(ex.Message + "\n" + ex.StackTrace);
                //return Shared.Constants.MSG_ERROR.Text;
            }
            return Shared.Constants.MSG_ERROR.Text;
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
    }
}