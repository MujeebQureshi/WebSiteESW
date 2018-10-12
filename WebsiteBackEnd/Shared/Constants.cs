using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared
{
    public class Constants
    {
        public const string ASK_JWT_KEY = "JWTSecretKey";
        public const string ASK_MAILING_ACC = "MailSendingProfile";
        public const string ASK_MAIL_SMTP_HOST = "SMTPHost";
        public const string ASK_ORG_NAME = "OrganizationName";
        public const string ASK_BASE_URL = "BaseUrl";
        public const string ASK_ORG_INITIALS = "OrganizationInitials";
        public const string ASK_COUNTRY_CITY = "CountryCityList";
        public const string ASK_RESPDF_FNAME = "ResumePDFFileName";
        public const string ASK_RESPDF_DIRECTORY = "ResumePDFDIRName";
        public const string ASK_RESJSON_FNAME = "ResumeJsonFileName";
        public const string ASK_RESBAT_FNAME = "ResumeBATFileName";
        public const string ASK_ORG_CONTACT_ADD = "OrganizationContactAddress";
        public const string ASK_ORG_CONTACT_PH = "OrganizationContactPhone";
        public const string ASK_ORG_CONTACT_EML = "OrganizationContactEmail";

        public const int ZERO = 0;

        public const string STR_YES = "Y";
        public const string STR_NO = "N";

        public const string SESSION_USER = "SessionUser";
        public const string SESSION_ADMIN = "SessionAdmin";

        public static DDList MSG_SESSION_USER_EMPTY = new DDList("sessionUserEmpty", "No user seems to be signed in! ");
        public static DDList MSG_SESSION_USER_UNV = new DDList("sessionUserUnverified", "User is not yet verified! ");
        public static DDList MSG_SESSION_USER_INC_PROFILE = new DDList("sessionUserIncProfile", "The profile seems to be incomplete! ");
        public static DDList MSG_ALREADY_APPLIED = new DDList("AlreadyApplied", "You have already applied for this job opening! ");
        public static DDList MSG_POS_CLOSED = new DDList("PositionClosed", "This job opening is now closed! ");
        public static DDList MSG_SUCCESS = new DDList("success", "Successfully completed! ");
        public static DDList MSG_ERROR = new DDList("error", "Errmm... There seems to be an error! ");
        public static DDList MSG_ERR_INVALIDCRED = new DDList("InvalidCredentials", "The username or password seems to be incorrect ");
        public static DDList MSG_ERR_NOUSEREXIST = new DDList("UserDoesnotExist", "The provided username/email is not listed in our system! ");
        public static DDList MSG_ERR_SERVER = new DDList("DBError", "Now this is embarrassing... there seems to be a problem! ");
        public static DDList MSG_ERR_DBSAVE = new DDList("Unable to save, Please contact ISD", "Unable to retrieve or save record in the database. ");
        public static DDList MSG_OK_DBSAVE = new DDList("OK", "Record successfully saved / retrieved from the database");

        /*
        public static Dictionary<string,string> MSG { get; set; }
        public static void FillMsgDictonary() {
            MSG = new Dictionary<string, string>();

            string MSG_SESSION_USER_EMPTY = "sessionUserEmpty";
            string MSG_SESSION_USER_UNV = "sessionUserUnverified";
            string MSG_SESSION_USER_INC_PROFILE = "sessionUserIncProfile";
            string MSG_ALREADY_APPLIED = "AlreadyApplied";
            string MSG_POS_CLOSED = "PositionClosed";
            string MSG_SUCCESS = "success";
            string MSG_ERROR = "error";
            string MSG_ERR_INVALIDCRED = "InvalidCredentials";
            string MSG_ERR_NOUSEREXIST = "UserDoesnotExist";
            string MSG_ERR_SERVER = "DBError";
            string MSG_ERR_DBSAVE = "Unable to save, Please contact ISD";
            string MSG_OK_DBSAVE = "OK";

            MSG.Add(MSG_SESSION_USER_EMPTY, "No user seems to be signed in! ");
            MSG.Add(MSG_SESSION_USER_UNV, "User is not yet verified! ");
            MSG.Add(MSG_SESSION_USER_INC_PROFILE, "The profile seems to be incomplete! ");
            MSG.Add(MSG_ALREADY_APPLIED, "You have already applied for this job opening! ");
            MSG.Add(MSG_POS_CLOSED, "This job opening is now closed! ");
            MSG.Add(MSG_SUCCESS, "Successfully completed! ");
            MSG.Add(MSG_ERROR, "Errmm... There seems to be an error! ");
            MSG.Add(MSG_ERR_INVALIDCRED, "The username or password seems to be incorrect ");
            MSG.Add(MSG_ERR_NOUSEREXIST, "The provided username is not listed in our system! ");
            MSG.Add(MSG_ERR_SERVER, "Now this is embarrassing... there seems to be a problem! ");
            MSG.Add(MSG_ERR_DBSAVE, "Unable to retrieve or save record in the database. ");
            MSG.Add(MSG_OK_DBSAVE, "Record successfully saved / retrieved from the database");

        }
        */

        public const string DATE_RFC_FORMAT = "yyyy-MM-dd";
        public const string DATE_MONTH_YEAR_FORMAT = "MMMM yyyy";
        public const string DATE_DISP_TAB_FORMAT = "MMMM dd, yyyy HH:mm";
        public const string DATE_RFC_YEAR_MONTH_FORMAT = "yyyy-MM";

        public const string SP_CAREER_COUNCIL = "CareerCouncil";
        public const string SP_OUTSOURCE = "Outsourcing";
        public const string SP_TRAIN_DEV = "TrainingDev";
        public const string SP_CLIENT = "Client";
        public const string SP_SOFTWARE_SRVS = "SoftwareServices";
        public const string SP_LIST_JOBS = "ListJobs";
        public const string SP_ABOUT_US = "AboutUs";

        public const string CountryNameField = "country_name";
        public const string CityNameField = "city_name";

        public static List<CountryCityList> lstCountryCity { get; set; }
        public static void SetCountryCityList()
        {
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            var lines = File.ReadAllLines(basePath + ConfigurationManager.AppSettings[ASK_COUNTRY_CITY]).Select(a => a.Split(',')).ToList();
            int countryIndex = Array.IndexOf(lines[ZERO], CountryNameField);
            int cityIndex = Array.IndexOf(lines[ZERO], CityNameField);
            lines.RemoveAt(ZERO);
            lstCountryCity = new List<CountryCityList>();
            foreach (var item in lines) {
                item[countryIndex] = Regex.Replace(item[countryIndex], @"[^a-zA-Z\._]", string.Empty);
                item[cityIndex] = Regex.Replace(item[cityIndex], @"[^a-zA-Z\._]", string.Empty);
                if (!string.IsNullOrEmpty(item[countryIndex]) && !string.IsNullOrEmpty(item[cityIndex])) {
                    lstCountryCity.Add(new CountryCityList(item[countryIndex], item[cityIndex]));
                }
            }
        }

        public static List<DDList> lstGender { get; set; }
        public static void getGender()
        {
            lstGender = new List<DDList>() {
                { new DDList("--Select Gender--", "") },
                { new DDList("Male", "M") },
                { new DDList("Female", "F") },
                { new DDList("Other", "O") }
           };
        }
        public static List<DDList> getNewGender()
        {
            return new List<DDList>() {
                { new DDList("--Select Gender--", "") },
                { new DDList("Male", "M") },
                { new DDList("Female", "F") },
                { new DDList("Other", "O") }
           };
        }

        public static List<DDList> lstSalary { get; set; }
        public static void getSalary()
        {
            lstSalary = new List<DDList>() {
                { new DDList("--Select Salary--", "") },
                { new DDList("less than 10,000", "1") },
                { new DDList("10,000 - 15,000", "2") },
                { new DDList("15,000 - 25,000", "3") },
                { new DDList("25,000 - 35,000", "4") },
                { new DDList("35,000 - 50,000", "5") },
                { new DDList("50,000 - 100,000", "6") },
                { new DDList("more than 100,000", "7") }
           };
        }
        public static List<DDList> getNewSalary()
        {
            return new List<DDList>() {
                { new DDList("--Select Salary--", "") },
                { new DDList("less than 10,000", "1") },
                { new DDList("10,000 - 15,000", "2") },
                { new DDList("15,000 - 25,000", "3") },
                { new DDList("25,000 - 35,000", "4") },
                { new DDList("35,000 - 50,000", "5") },
                { new DDList("50,000 - 100,000", "6") },
                { new DDList("more than 100,000", "7") }
           };
        }

        public static List<DDList> lstDegreeType { get; set; }
        public static void getDegreeType()
        {
            lstDegreeType = new List<DDList>() {
                { new DDList("--Select Degree Type--", "") },
                { new DDList("Matriculation/O-Level", "1") },
                { new DDList("Intermediate/A-Level", "2") },
                { new DDList("Undergraduate", "3") },
                { new DDList("Graduate", "4") },
                { new DDList("Post Graduate / Masters", "5") },
                { new DDList("PhD", "6") }
           };
        }
        public static List<DDList> getNewDegreeType()
        {
            return new List<DDList>() {
                { new DDList("--Select Degree Type--", "") },
                { new DDList("Matriculation/O-Level", "1") },
                { new DDList("Intermediate/A-Level", "2") },
                { new DDList("Undergraduate", "3") },
                { new DDList("Graduate", "4") },
                { new DDList("Post Graduate / Masters", "5") },
                { new DDList("PhD", "6") }
           };
        }

        public static List<string> getSessionActionList()
        {
            return new List<string>() {
                "Signin", "Signup"
           };
        }
        public static List<string> getEmptySessionActionList()
        {
            return new List<string>() {
                "MyProfile"
           };
        }

        #region ::Admin Lists::

        public static List<DDList> lstJobGender { get; set; }
        public static void getJobGender()
        {
            lstJobGender = new List<DDList>() {
                { new DDList("--Select Gender--", "") },
                { new DDList("Male", "M") },
                { new DDList("All", "A") },
                { new DDList("Female", "F") },
                { new DDList("Other", "O") }
           };
        }
        public static List<DDList> getNewJobGender()
        {
            return new List<DDList>() {
                { new DDList("--Select Gender--", "") },
                { new DDList("Male", "M") },
                { new DDList("All", "A") },
                { new DDList("Female", "F") },
                { new DDList("Other", "O") }
           };
        }

        public static List<DDList> lstJobStatus { get; set; }
        public static void getJobStatus()
        {
            lstJobStatus = new List<DDList>() {
                { new DDList("--Select Job Status--", "") },
                { new DDList("Open", "O") },
                { new DDList("Hold", "H") },
                { new DDList("Closed", "C") }
           };
        }
        public static List<DDList> getNewJobStatus()
        {
            return new List<DDList>() {
                { new DDList("--Select Job Status--", "") },
                { new DDList("Open", "O") },
                { new DDList("Hold", "H") },
                { new DDList("Closed", "C") }
           };
        }

        public static List<DDList> lstJobType { get; set; }
        public static void getJobType()
        {
            lstJobType = new List<DDList>() {
                { new DDList("--Select Job Type--", "") },
                { new DDList("Full Time", "FT") },
                { new DDList("Part Time", "PT") },
                { new DDList("Both (Full Time and Part Time)", "B") }
           };
        }
        public static List<DDList> getNewJobType()
        {
            return new List<DDList>() {
                { new DDList("--Select Job Type--", "") },
                { new DDList("Full Time", "FT") },
                { new DDList("Part Time", "PT") },
                { new DDList("Both (Full Time and Part Time)", "B") }
           };
        }

        public static List<DDList> lstYesNo { get; set; }
        public static void getYesNoList()
        {
            lstYesNo = new List<DDList>() {
                { new DDList("--Please Select--", "") },
                { new DDList("No", "N") },
                { new DDList("Yes", "Y") }
           };
        }
        public static List<DDList> getNewYesNoList()
        {
            return new List<DDList>() {
                { new DDList("--Please Select--", "") },
                { new DDList("No", "N") },
                { new DDList("Yes", "Y") }
           };
        }

        #endregion
    }

    public class DDList{
        public DDList(string Text, string Value) {
            this.Text = Text;
            this.Value = Value;
        }
        public string Text { get; set; }
        public string Value { get; set; }
        public bool isSelected { get; set; }
    }

    public class CountryCityList
    {
        public CountryCityList(string Country, string City)
        {
            this.Country = Country;
            this.City = City;
        }
        public string Country { get; set; }
        public string City { get; set; }
        //public bool isSelected { get; set; }
    }
}
