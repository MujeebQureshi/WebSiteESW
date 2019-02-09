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
        public const string ASK_ORG_CONTACT_LL = "OrganizationContactLandLine";
        public const string ASK_ORG_CONTACT_MBL = "OrganizationContactMobile";
        public const string ASK_ORG_CONTACT_EML = "OrganizationContactEmail";
        public const string ASK_NODE_SRVURL = "LocalNodeServer";
        public const string ASK_NODE_SRVURL_RETSUCCESS = "LocalNodeServerRetSuccess";
        public const string ASK_NODE_SRVURL_RETERROR = "LocalNodeServerRetFailure";
        public const string ASK_NODE_SRV_PATH = "LocalNodeServerPath";
        
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
        public const string SP_OURSERVICES = "OurServices";
        public const string SP_TRAIN_DEV = "TrainingDev";
        public const string SP_CLIENT = "Client";
        public const string SP_SOFTWARE_SRVS = "SoftwareServices";
        public const string SP_LIST_JOBS = "ListJobs";
        public const string SP_ABOUT_US = "AboutUs";
        public const string SP_EXPERTISE = "Expertise";
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

        //DEGREE LIST 

        
        //public static void Setdegree() {
        //    string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
        //    var lines = File.ReadAllLines(basePath + ConfigurationManager.AppSettings["degreelist"]).Select(a => a.Split(',')).ToList();

        //    int degreeIndex = Array.IndexOf(lines[ZERO], "degreename");
        //    lines.RemoveAt(ZERO);
        //    lstdegreelist = new List<degreelist>();
        //    foreach(var item in lines)
        //    {
        //        item[degreeIndex] = Regex.Replace(item[degreeIndex], @"[^a-zA-Z\._]", string.Empty);
        //        if (!string.IsNullOrEmpty(item[degreeIndex]))
        //        {
        //            lstdegreelist.Add(new degreelist(item[degreeIndex]));
        //        }
        //    }


        //}

        public static List<DDList> lstdegree { get; set; }
        public static void setdegree()
        {
            lstdegree = new List<DDList>()
            {
                    { new DDList("--Select degree--", "") },
                    { new DDList("Associate Degree in Administration of Justice", "Associate Degree in Administration of Justice")},
                    { new DDList("Associate Degree in Advertising", "Associate Degree in Advertising")},
                    { new DDList("Associate Degree in Agribusiness", "Associate Degree in Agribusiness")},
                    { new DDList("Associate Degree in Animal Management", "Associate Degree in Animal Management")},
                    { new DDList("Associate Degree in Architectural Building Engineering Technology", "Associate Degree in Architectural Building Engineering Technology")},
                    { new DDList("Associate Degree in Architecture and Career Options", "Associate Degree in Architecture and Career Options")},
                    { new DDList("Associate Degree in Art", "Associate Degree in Art")},
                    { new DDList("Associate Degree in Automotive Maintenance Technology", "Associate Degree in Automotive Maintenance Technology")},
                    { new DDList("Associate Degree in Aviation Mechanics", "Associate Degree in Aviation Mechanics")},
                    { new DDList("Associate Degree in Behavioral Science", "Associate Degree in Behavioral Science")},
                    { new DDList("Associate Degree in Boat Mechanics", "Associate Degree in Boat Mechanics")},
                    { new DDList("Associate Degree in Boat Repair and Maintenance", "Associate Degree in Boat Repair and Maintenance")},
                    { new DDList("Associate Degree in Cabinet Design Technology", "Associate Degree in Cabinet Design Technology")},
                    { new DDList("Associate Degree in Child Development: Program Summary", "Associate Degree in Child Development: Program Summary")},
                    { new DDList("Associate Degree in Christian Ministry", "Associate Degree in Christian Ministry")},
                    { new DDList("Associate Degree in Cosmetology Business", "Associate Degree in Cosmetology Business")},
                    { new DDList("Associate Degree in Digital Media", "Associate Degree in Digital Media")},
                    { new DDList("Associate Degree in Early Childhood Special Education", "Associate Degree in Early Childhood Special Education")},
                    { new DDList("Associate Degree in Elementary Education", "Associate Degree in Elementary Education")},
                    { new DDList("Associate Degree in English", "Associate Degree in English")},
                    { new DDList("Associate Degree in Environmental Science", "Associate Degree in Environmental Science")},
                    { new DDList("Associate Degree in Environmental Studies", "Associate Degree in Environmental Studies")},
                    { new DDList("Associate Degree in General Psychology", "Associate Degree in General Psychology")},
                    { new DDList("Associate Degree in History and Information", "Associate Degree in History and Information")},
                    { new DDList("Associate Degree in Interdisciplinary Studies", "Associate Degree in Interdisciplinary Studies")},
                    { new DDList("Associate Degree in International Relations", "Associate Degree in International Relations")},
                    { new DDList("Associate Degree in Landscape Architecture", "Associate Degree in Landscape Architecture")},
                    { new DDList("Associate Degree in Landscaping Design", "Associate Degree in Landscaping Design")},
                    { new DDList("Associate Degree in Library Science", "Associate Degree in Library Science")},
                    { new DDList("Associate Degree in Music", "Associate Degree in Music")},
                    { new DDList("Associate Degree in Wildlife Management", "Associate Degree in Wildlife Management")},
                    { new DDList("Associate Degree in Education", "Associate Degree in Education")},
                    { new DDList("Associate of Applied Science (AAS) in Accelerated Culinary Arts", "Associate of Applied Science (AAS) in Accelerated Culinary Arts")},
                    { new DDList("Associate of Applied Science (AAS) in Accounting Specialist", "Associate of Applied Science (AAS) in Accounting Specialist")},
                    { new DDList("Associate of Applied Science (AAS) in Administrative Support", "Associate of Applied Science (AAS) in Administrative Support")},
                    { new DDList("Associate of Applied Science (AAS) in Baking and Pastry", "Associate of Applied Science (AAS) in Baking and Pastry")},
                    { new DDList("Associate of Applied Science (AAS) in Business Administration", "Associate of Applied Science (AAS) in Business Administration")},
                    { new DDList("Associate of Applied Science (AAS) in Business Administration - Finance", "Associate of Applied Science (AAS) in Business Administration - Finance")},
                    { new DDList("Associate of Applied Science (AAS) in Business Information Systems", "Associate of Applied Science (AAS) in Business Information Systems")},
                    { new DDList("Associate of Applied Science (AAS) in Civil Justice - Law Enforcement", "Associate of Applied Science (AAS) in Civil Justice - Law Enforcement")},
                    { new DDList("Associate of Applied Science (AAS) in Clinical Medical Assisting", "Associate of Applied Science (AAS) in Clinical Medical Assisting")},
                    { new DDList("Associate of Applied Science (AAS) in Computer Applications", "Associate of Applied Science (AAS) in Computer Applications")},
                    { new DDList("Associate of Applied Science (AAS) in Computer Electronics", "Associate of Applied Science (AAS) in Computer Electronics")},
                    { new DDList("Associate of Applied Science (AAS) in Computer Game Design", "Associate of Applied Science (AAS) in Computer Game Design")},
                    { new DDList("Associate of Applied Science (AAS) in Computer Information Systems", "Associate of Applied Science (AAS) in Computer Information Systems")},
                    { new DDList("Associate of Applied Science (AAS) in Culinary Arts", "Associate of Applied Science (AAS) in Culinary Arts")},
                    { new DDList("Associate of Applied Science (AAS) in Digital Media Communications", "Associate of Applied Science (AAS) in Digital Media Communications")},
                    { new DDList("Associate of Applied Science (AAS) in Digital Photography", "Associate of Applied Science (AAS) in Digital Photography")},
                    { new DDList("Associate of Applied Science (AAS) in Electronic Engineering", "Associate of Applied Science (AAS) in Electronic Engineering")},
                    { new DDList("Associate of Applied Science (AAS) in Emergency Medical Services", "Associate of Applied Science (AAS) in Emergency Medical Services")},
                    { new DDList("Associate of Applied Science (AAS) in Health Care Management", "Associate of Applied Science (AAS) in Health Care Management")},
                    { new DDList("Associate of Applied Science (AAS) in Health Information Management", "Associate of Applied Science (AAS) in Health Information Management")},
                    { new DDList("Associate of Applied Science (AAS) in Healthcare Administration", "Associate of Applied Science (AAS) in Healthcare Administration")},
                    { new DDList("Associate of Applied Science (AAS) in Legal Office E-ministration", "Associate of Applied Science (AAS) in Legal Office E-ministration")},
                    { new DDList("Associate of Applied Science (AAS) in Telecommunications Technology", "Associate of Applied Science (AAS) in Telecommunications Technology")},
                    { new DDList("Associate of Applied Science (AAS) in Television Production", "Associate of Applied Science (AAS) in Television Production")},
                    { new DDList("Associate of Applied Science (AAS) in Visual Communications", "Associate of Applied Science (AAS) in Visual Communications")},
                    { new DDList("Associate of Arts (AA) in Computer Information Systems", "Associate of Arts (AA) in Computer Information Systems")},
                    { new DDList("Associate of Arts (AA) in Internetworking Technology", "Associate of Arts (AA) in Internetworking Technology")},
                    { new DDList("Associate of Arts (AA) in Psychology", "Associate of Arts (AA) in Psychology")},
                    { new DDList("Associate of Arts (AA) in Interior Architecture and Design", "Associate of Arts (AA) in Interior Architecture and Design")},
                    { new DDList("Associate of Biotechnology", "Associate of Biotechnology")},
                    { new DDList("Associate of Business Science (ABS) in Individualized Studies", "Associate of Business Science (ABS) in Individualized Studies")},
                    { new DDList("Associate of Early Childhood Education (AECE)", "Associate of Early Childhood Education (AECE)")},
                    { new DDList("Associate of Occupational Studies (AOS) in Legal Office Administration", "Associate of Occupational Studies (AOS) in Legal Office Administration")},
                    { new DDList("Associate of Science (AS) in Computer Information Science", "Associate of Science (AS) in Computer Information Science")},
                    { new DDList("Associate of Science (AS) in Computer Science", "Associate of Science (AS) in Computer Science")},
                    { new DDList("Associate of Science (AS) in Corrections, Probation, & Parole", "Associate of Science (AS) in Corrections, Probation, & Parole")},
                    { new DDList("Associate of Science (AS) in Electronics Engineering Technology", "Associate of Science (AS) in Electronics Engineering Technology")},
                    { new DDList("Associate of Science (AS) in Interactive & Graphic Art", "Associate of Science (AS) in Interactive & Graphic Art")},
                    { new DDList("Associate of Science (AS) in Industrial Maintenance Technology", "Associate of Science (AS) in Industrial Maintenance Technology")},
                    { new DDList("Associate of Arts and Science", "Associate of Arts and Science")},
                    { new DDList("Bachelor of Architecture", "Bachelor of Architecture")},
                    { new DDList("Bachelor of Biomedical Science", "Bachelor of Biomedical Science")},
                    { new DDList("Bachelor of Business Administration", "Bachelor of Business Administration")},
                    { new DDList("Bachelor of Clinical Science", "Bachelor of Clinical Science")},
                    { new DDList("Bachelor of Commerce", "Bachelor of Commerce")},
                    { new DDList("Bachelor of Computer Applications", "Bachelor of Computer Applications")},
                    { new DDList("Bachelor of Community Health", "Bachelor of Community Health")},
                    { new DDList("Bachelor of Computer Information Systems", "Bachelor of Computer Information Systems")},
                    { new DDList("Bachelor of Science in Construction Technology", "Bachelor of Science in Construction Technology")},
                    { new DDList("Bachelor of Criminal Justice", "Bachelor of Criminal Justice")},
                    { new DDList("Bachelor of Divinity", "Bachelor of Divinity")},
                    { new DDList("Bachelor of Economics", "Bachelor of Economics")},
                    { new DDList("Bachelor of Education", "Bachelor of Education")},
                    { new DDList("Bachelor of Engineering", "Bachelor of Engineering")},
                    { new DDList("Bachelor of Fine Arts", "Bachelor of Fine Arts")},
                    { new DDList("Bachelor of Letters", "Bachelor of Letters")},
                    { new DDList("Bachelor of Information Systems", "Bachelor of Information Systems")},
                    { new DDList("Bachelor of Management", "Bachelor of Management")},
                    { new DDList("Bachelor of Music", "Bachelor of Music")},
                    { new DDList("Bachelor of Pharmacy", "Bachelor of Pharmacy")},
                    { new DDList("Bachelor of Philosophy", "Bachelor of Philosophy")},
                    { new DDList("Bachelor of Social Work", "Bachelor of Social Work")},
                    { new DDList("Bachelor of Technology", "Bachelor of Technology")},
                    { new DDList("Bachelor of Accountancy", "Bachelor of Accountancy")},
                    { new DDList("Bachelor of Arts in American Studies", "Bachelor of Arts in American Studies")},
                    { new DDList("Bachelor of Arts in American Indian Studies", "Bachelor of Arts in American Indian Studies")},
                    { new DDList("Bachelor of Arts in Applied Psychology", "Bachelor of Arts in Applied Psychology")},
                    { new DDList("Bachelor of Arts in Biology", "Bachelor of Arts in Biology")},
                    { new DDList("Bachelor of Arts in Anthropology", "Bachelor of Arts in Anthropology")},
                    { new DDList("Bachelor of Arts in Child Advocacy", "Bachelor of Arts in Child Advocacy")},
                    { new DDList("Bachelor of Arts in Clinical Psychology", "Bachelor of Arts in Clinical Psychology")},
                    { new DDList("Bachelor of Arts in Forensic Psychology", "Bachelor of Arts in Forensic Psychology")},
                    { new DDList("Bachelor of Arts in Organizational Psychology", "Bachelor of Arts in Organizational Psychology")},
                    { new DDList("Bachelor of Science in Aerospace Engineering", "Bachelor of Science in Aerospace Engineering")},
                    { new DDList("Bachelor of Science in Actuarial", "Bachelor of Science in Actuarial")},
                    { new DDList("Bachelor of Science in Agriculture", "Bachelor of Science in Agriculture")},
                    { new DDList("Bachelor of Science in Architecture", "Bachelor of Science in Architecture")},
                    { new DDList("Bachelor of Science in Architectural Engineering", "Bachelor of Science in Architectural Engineering")},
                    { new DDList("Bachelor of Science in Athletic Training", "Bachelor of Science in Athletic Training")},
                    { new DDList("Bachelor of Science in Biology", "Bachelor of Science in Biology")},
                    { new DDList("Bachelor of Science in Biomedical Engineering", "Bachelor of Science in Biomedical Engineering")},
                    { new DDList("Bachelor of Science in Bible", "Bachelor of Science in Bible")},
                    { new DDList("Bachelor of Science in Business Administration", "Bachelor of Science in Business Administration")},
                    { new DDList("Bachelor of Science in Business and Technology", "Bachelor of Science in Business and Technology")},
                    { new DDList("Bachelor of Science in Chemical Engineering", "Bachelor of Science in Chemical Engineering")},
                    { new DDList("Bachelor of Science in Chemistry", "Bachelor of Science in Chemistry")},
                    { new DDList("Bachelor of Science in Civil Engineering", "Bachelor of Science in Civil Engineering")},
                    { new DDList("Bachelor of Science in Clinical Laboratory Science", "Bachelor of Science in Clinical Laboratory Science")},
                    { new DDList("Bachelor of Science in Cognitive Science", "Bachelor of Science in Cognitive Science")},
                    { new DDList("Bachelor of Science in Computer Engineering", "Bachelor of Science in Computer Engineering")},
                    { new DDList("Bachelor of Science in Computer Science", "Bachelor of Science in Computer Science")},
                    { new DDList("Bachelor of Science in Construction Engineering", "Bachelor of Science in Construction Engineering")},
                    { new DDList("Bachelor of Science in Construction Management", "Bachelor of Science in Construction Management")},
                    { new DDList("Bachelor of Science in Criminal Justice", "Bachelor of Science in Criminal Justice")},
                    { new DDList("Bachelor of Science in Criminology", "Bachelor of Science in Criminology")},
                    { new DDList("Bachelor of Science in Diagnostic Radiography", "Bachelor of Science in Diagnostic Radiography")},
                    { new DDList("Bachelor of Science in Education", "Bachelor of Science in Education")},
                    { new DDList("Bachelor of Science in Electrical Engineering", "Bachelor of Science in Electrical Engineering")},
                    { new DDList("Bachelor of Science in Engineering Physics", "Bachelor of Science in Engineering Physics")},
                    { new DDList("Bachelor of Science in Engineering Science", "Bachelor of Science in Engineering Science")},
                    { new DDList("Bachelor of Science in Engineering Technology", "Bachelor of Science in Engineering Technology")},
                    { new DDList("Bachelor of Science in English Literature", "Bachelor of Science in English Literature")},
                    { new DDList("Bachelor of Science in Environmental Engineering", "Bachelor of Science in Environmental Engineering")},
                    { new DDList("Bachelor of Science in Environmental Science", "Bachelor of Science in Environmental Science")},
                    { new DDList("Bachelor of Science in Environmental Studies", "Bachelor of Science in Environmental Studies")},
                    { new DDList("Bachelor of Science in Food Science", "Bachelor of Science in Food Science")},
                    { new DDList("Bachelor of Science in Foreign Service", "Bachelor of Science in Foreign Service")},
                    { new DDList("Bachelor of Science in Forensic Science", "Bachelor of Science in Forensic Science")},
                    { new DDList("Bachelor of Science in Forestry", "Bachelor of Science in Forestry")},
                    { new DDList("Bachelor of Science in History", "Bachelor of Science in History")},
                    { new DDList("Bachelor of Science in Hospitality Management", "Bachelor of Science in Hospitality Management")},
                    { new DDList("Bachelor of Science in Human Resources Management", "Bachelor of Science in Human Resources Management")},
                    { new DDList("Bachelor of Science in Industrial Engineering", "Bachelor of Science in Industrial Engineering")},
                    { new DDList("Bachelor of Science in Information Technology", "Bachelor of Science in Information Technology")},
                    { new DDList("Bachelor of Science in Information Systems", "Bachelor of Science in Information Systems")},
                    { new DDList("Bachelor of Science in Integrated Science", "Bachelor of Science in Integrated Science")},
                    { new DDList("Bachelor of Science in International Relations", "Bachelor of Science in International Relations")},
                    { new DDList("Bachelor of Science in Journalism", "Bachelor of Science in Journalism")},
                    { new DDList("Bachelor of Science in Legal Management", "Bachelor of Science in Legal Management")},
                    { new DDList("Bachelor of Science in Management", "Bachelor of Science in Management")},
                    { new DDList("Bachelor of Science in Manufacturing Engineering", "Bachelor of Science in Manufacturing Engineering")},
                    { new DDList("Bachelor of Science in Marketing", "Bachelor of Science in Marketing")},
                    { new DDList("Bachelor of Science in Mathematics", "Bachelor of Science in Mathematics")},
                    { new DDList("Bachelor of Science in Mechanical Engineering", "Bachelor of Science in Mechanical Engineering")},
                    { new DDList("Bachelor of Science in Medical Technology", "Bachelor of Science in Medical Technology")},
                    { new DDList("Bachelor of Science in Metallurgical Engineering", "Bachelor of Science in Metallurgical Engineering")},
                    { new DDList("Bachelor of Science in Meteorology", "Bachelor of Science in Meteorology")},
                    { new DDList("Bachelor of Science in Microbiology", "Bachelor of Science in Microbiology")},
                    { new DDList("Bachelor of Science in Mining Engineering", "Bachelor of Science in Mining Engineering")},
                    { new DDList("Bachelor of Science in Molecular Biology", "Bachelor of Science in Molecular Biology")},
                    { new DDList("Bachelor of Science in Neuroscience", "Bachelor of Science in Neuroscience")},
                    { new DDList("Bachelor of Science in Nursing", "Bachelor of Science in Nursing")},
                    { new DDList("Bachelor of Science in Nutrition science", "Bachelor of Science in Nutrition science")},
                    { new DDList("Bachelor of Science in Software Engineering", "Bachelor of Science in Software Engineering")},
                    { new DDList("Bachelor of Science in Petroleum Engineering", "Bachelor of Science in Petroleum Engineering")},
                    { new DDList("Bachelor of Science in Podiatry", "Bachelor of Science in Podiatry")},
                    { new DDList("Bachelor of Science in Pharmacology", "Bachelor of Science in Pharmacology")},
                    { new DDList("Bachelor of Science in Pharmacy", "Bachelor of Science in Pharmacy")},
                    { new DDList("Bachelor of Science in Physical Therapy", "Bachelor of Science in Physical Therapy")},
                    { new DDList("Bachelor of Science in Physics", "Bachelor of Science in Physics")},
                    { new DDList("Bachelor of Science in Plant Science", "Bachelor of Science in Plant Science")},
                    { new DDList("Bachelor of Science in Politics", "Bachelor of Science in Politics")},
                    { new DDList("Bachelor of Science in Psychology", "Bachelor of Science in Psychology")},
                    { new DDList("Bachelor of Science in Public Safety", "Bachelor of Science in Public Safety")},
                    { new DDList("Bachelor of Science in Physiology", "Bachelor of Science in Physiology")},
                    { new DDList("Bachelor of Science in Quantity Surveying Engineering", "Bachelor of Science in Quantity Surveying Engineering")},
                    { new DDList("Bachelor of Science in Radiologic Technology", "Bachelor of Science in Radiologic Technology")},
                    { new DDList("Bachelor of Science in Real-Time Interactive Simulation", "Bachelor of Science in Real-Time Interactive Simulation")},
                    { new DDList("Bachelor of Science in Religion", "Bachelor of Science in Religion")},
                    { new DDList("Bachelor of Science in Respiratory Therapy", "Bachelor of Science in Respiratory Therapy")},
                    { new DDList("Bachelor of Science in Risk Management and Insurance", "Bachelor of Science in Risk Management and Insurance")},
                    { new DDList("Bachelor of Science in Science Education", "Bachelor of Science in Science Education")},
                    { new DDList("Bachelor of Science in Sports Management", "Bachelor of Science in Sports Management")},
                    { new DDList("Bachelor of Science in Systems Engineering", "Bachelor of Science in Systems Engineering")},
                    { new DDList("Bachelor of Music in Jazz Studies", "Bachelor of Music in Jazz Studies")},
                    { new DDList("Bachelor of Music in Composition", "Bachelor of Music in Composition")},
                    { new DDList("Bachelor of Music in Performance", "Bachelor of Music in Performance")},
                    { new DDList("Bachelor of Music in Theory", "Bachelor of Music in Theory")},
                    { new DDList("Bachelor of Music in Music Education", "Bachelor of Music in Music Education")},
                    { new DDList("Bachelor of Science in Veterinary Technology", "Bachelor of Science in Veterinary Technology")},
                    { new DDList("Master of Accountancy", "Master of Accountancy")},
                    { new DDList("Master of Accounting and Information Systems", "Master of Accounting and Information Systems")},
                    { new DDList("Master of Advanced Study", "Master of Advanced Study")},
                    { new DDList("Master of Applied Finance", "Master of Applied Finance")},
                    { new DDList("Master of Applied Mathematical Sciences", "Master of Applied Mathematical Sciences")},
                    { new DDList("Master of Applied Psychology", "Master of Applied Psychology")},
                    { new DDList("Master of Applied Science", "Master of Applied Science")},
                    { new DDList("Master of Architecture", "Master of Architecture")},
                    { new DDList("Master of Arts", "Master of Arts")},
                    { new DDList("Master of Arts in Archives and Records Management", "Master of Arts in Archives and Records Management")},
                    { new DDList("Master of Arts in Bioethics", "Master of Arts in Bioethics")},
                    { new DDList("Master of Arts in Liberal Studies", "Master of Arts in Liberal Studies")},
                    { new DDList("Master of Arts in Museum Studies", "Master of Arts in Museum Studies")},
                    { new DDList("Master of Arts in Strategic Communication Management", "Master of Arts in Strategic Communication Management")},
                    { new DDList("Master of Arts in Teaching", "Master of Arts in Teaching")},
                    { new DDList("Master of Athletic Training", "Master of Athletic Training")},
                    { new DDList("Master of Bioethics", "Master of Bioethics")},
                    { new DDList("Master of Bioinformatics", "Master of Bioinformatics")},
                    { new DDList("Master of Biotechnology", "Master of Biotechnology")},
                    { new DDList("Master of Business Administration", "Master of Business Administration")},
                    { new DDList("Master of Business Administration Management of Technology", "Master of Business Administration Management of Technology")},
                    { new DDList("Master of Business", "Master of Business")},
                    { new DDList("Master of Business Economics", "Master of Business Economics")},
                    { new DDList("Master of Business Engineering", "Master of Business Engineering")},
                    { new DDList("Master of Business Informatics", "Master of Business Informatics")},
                    { new DDList("Master of Chemistry", "Master of Chemistry")},
                    { new DDList("Master of City Planning", "Master of City Planning")},
                    { new DDList("Master of Commerce", "Master of Commerce")},
                    { new DDList("Master of Community Health", "Master of Community Health")},
                    { new DDList("Master of Computational Finance", "Master of Computational Finance")},
                    { new DDList("Master of Computer Applications", "Master of Computer Applications")},
                    { new DDList("Master of Computer Science", "Master of Computer Science")},
                    { new DDList("Master of Communication", "Master of Communication")},
                    { new DDList("Master of Counseling", "Master of Counseling")},
                    { new DDList("Master of Criminal Justice", "Master of Criminal Justice")},
                    { new DDList("Master in Creative Technologies", "Master in Creative Technologies")},
                    { new DDList("Master of Design", "Master of Design")},
                    { new DDList("Master of Divinity", "Master of Divinity")},
                    { new DDList("Master of Economics", "Master of Economics")},
                    { new DDList("Master of Education", "Master of Education")},
                    { new DDList("Master of Educational Technology", "Master of Educational Technology")},
                    { new DDList("Master of Engineering", "Master of Engineering")},
                    { new DDList("Master of Engineering Management", "Master of Engineering Management")},
                    { new DDList("Master of Enterprise", "Master of Enterprise")},
                    { new DDList("Master of European Law", "Master of European Law")},
                    { new DDList("Master of Finance", "Master of Finance")},
                    { new DDList("Master of Financial Economics", "Master of Financial Economics")},
                    { new DDList("Master of Financial Engineering", "Master of Financial Engineering")},
                    { new DDList("Master of Financial Mathematics", "Master of Financial Mathematics")},
                    { new DDList("Master of Fine Arts", "Master of Fine Arts")},
                    { new DDList("Master of Geospatial Science & Technology", "Master of Geospatial Science & Technology")},
                    { new DDList("Master of Health Administration", "Master of Health Administration")},
                    { new DDList("Master of Health Science", "Master of Health Science")},
                    { new DDList("Master of Humanities", "Master of Humanities")},
                    { new DDList("Master of Industrial and Labor Relations", "Master of Industrial and Labor Relations")},
                    { new DDList("Master of International Affairs", "Master of International Affairs")},
                    { new DDList("Master of International Business", "Master of International Business")},
                    { new DDList("Master of International Economics", "Master of International Economics")},
                    { new DDList("Master of International Public Policy", "Master of International Public Policy")},
                    { new DDList("Master of International Studies", "Master of International Studies")},
                    { new DDList("Master of Information", "Master of Information")},
                    { new DDList("Master of Information Management", "Master of Information Management")},
                    { new DDList("Master of Information Systems", "Master of Information Systems")},
                    { new DDList("Master of Information System Management", "Master of Information System Management")},
                    { new DDList("Master of Islamic Studies", "Master of Islamic Studies")},
                    { new DDList("Master of IT", "Master of IT")},
                    { new DDList("Master of Jurisprudence", "Master of Jurisprudence")},
                    { new DDList("Master of Laws", "Master of Laws")},
                    { new DDList("Master of Studies in Law", "Master of Studies in Law")},
                    { new DDList("Master of Landscape Architecture", "Master of Landscape Architecture")},
                    { new DDList("Master of Letters", "Master of Letters")},
                    { new DDList("Master of Liberal Arts", "Master of Liberal Arts")},
                    { new DDList("Master of Library and Information Science", "Master of Library and Information Science")},
                    { new DDList("Master of Management", "Master of Management")},
                    { new DDList("Master of Mass Communication and Journalism", "Master of Mass Communication and Journalism")},
                    { new DDList("Master of Mathematical Finance", "Master of Mathematical Finance")},
                    { new DDList("Master of Mathematics", "Master of Mathematics")},
                    { new DDList("Master of Mathematics and Computer Science", "Master of Mathematics and Computer Science")},
                    { new DDList("Master of Mathematics and Philosophy", "Master of Mathematics and Philosophy")},
                    { new DDList("Master of Medical Science", "Master of Medical Science")},
                    { new DDList("Master of Medicine", "Master of Medicine")},
                    { new DDList("Master of Military Art and Science", "Master of Military Art and Science")},
                    { new DDList("Master of Music", "Master of Music")},
                    { new DDList("Master of Network and Communications Management", "Master of Network and Communications Management")},
                    { new DDList("Master of Occupational Therapy", "Master of Occupational Therapy")},
                    { new DDList("Master of Pharmacy", "Master of Pharmacy")},
                    { new DDList("Master of Philosophy", "Master of Philosophy")},
                    { new DDList("Master of Physician Assistant Studies", "Master of Physician Assistant Studies")},
                    { new DDList("Master of Physics", "Master of Physics")},
                    { new DDList("Master of Political Science", "Master of Political Science")},
                    { new DDList("Master of Professional Studies", "Master of Professional Studies")},
                    { new DDList("Master of Psychology", "Master of Psychology")},
                    { new DDList("Master of Public Administration", "Master of Public Administration")},
                    { new DDList("Master of Public Affairs", "Master of Public Affairs")},
                    { new DDList("Master of Public Diplomacy", "Master of Public Diplomacy")},
                    { new DDList("Master of Public Health", "Master of Public Health")},
                    { new DDList("Master of Public Management", "Master of Public Management")},
                    { new DDList("Master of Public Policy", "Master of Public Policy")},
                    { new DDList("Master of Public Relations", "Master of Public Relations")},
                    { new DDList("Master of Public Service", "Master of Public Service")},
                    { new DDList("Master of Quantitative Finance", "Master of Quantitative Finance")},
                    { new DDList("Master of Rabbinic Studies", "Master of Rabbinic Studies")},
                    { new DDList("Master of Real Estate Development", "Master of Real Estate Development")},
                    { new DDList("Master of Religious Education", "Master of Religious Education")},
                    { new DDList("Master of Research", "Master of Research")},
                    { new DDList("Master of Sacred Music", "Master of Sacred Music")},
                    { new DDList("Master of Sacred Theology", "Master of Sacred Theology")},
                    { new DDList("Master of Science", "Master of Science")},
                    { new DDList("Master of Science in Applied Cognition and Neuroscience", "Master of Science in Applied Cognition and Neuroscience")},
                    { new DDList("Master of Science in Athletic Training", "Master of Science in Athletic Training")},
                    { new DDList("Master of Science in Bioinformatics", "Master of Science in Bioinformatics")},
                    { new DDList("Master of Science in Clinical Epidemiology", "Master of Science in Clinical Epidemiology")},
                    { new DDList("Master of Science in Computing Research", "Master of Science in Computing Research")},
                    { new DDList("Master of Science in Cyber Security", "Master of Science in Cyber Security")},
                    { new DDList("Master of Science in Education", "Master of Science in Education")},
                    { new DDList("Master of Science in Engineering", "Master of Science in Engineering")},
                    { new DDList("Master of Science in Development Administration", "Master of Science in Development Administration")},
                    { new DDList("Master of Science in Finance", "Master of Science in Finance")},
                    { new DDList("Master of Science in Governance & Organizational Sciences", "Master of Science in Governance & Organizational Sciences")},
                    { new DDList("Master of Science in Government Contracts", "Master of Science in Government Contracts")},
                    { new DDList("Master of Science in Health Informatics", "Master of Science in Health Informatics")},
                    { new DDList("Master of Science in Human Resource Development", "Master of Science in Human Resource Development")},
                    { new DDList("Master of Science in Information Assurance", "Master of Science in Information Assurance")},
                    { new DDList("Master of Science in Information Systems", "Master of Science in Information Systems")},
                    { new DDList("Master of Science in Information Technology", "Master of Science in Information Technology")},
                    { new DDList("Master of Science in Leadership", "Master of Science in Leadership")},
                    { new DDList("Master of Science in Management", "Master of Science in Management")},
                    { new DDList("Master of Science in Nursing", "Master of Science in Nursing")},
                    { new DDList("Master of Science in Project Management", "Master of Science in Project Management")},
                    { new DDList("Master of Science in Quality Assurance", "Master of Science in Quality Assurance")},
                    { new DDList("Master of Science in Risk Management", "Master of Science in Risk Management")},
                    { new DDList("Master of Science in Supply Chain Management", "Master of Science in Supply Chain Management")},
                    { new DDList("Master of Science in Teaching", "Master of Science in Teaching")},
                    { new DDList("Master of Science in Taxation", "Master of Science in Taxation")},
                    { new DDList("Master of Social Science", "Master of Social Science")},
                    { new DDList("Master of Social Work", "Master of Social Work")},
                    { new DDList("Master of Statistics", "Master of Statistics")},
                    { new DDList("Master of Strategic Studies", "Master of Strategic Studies")},
                    { new DDList("Master of Studies", "Master of Studies")},
                    { new DDList("Master of Surgery", "Master of Surgery")},
                    { new DDList("Master of Theological Studies", "Master of Theological Studies")},
                    { new DDList("Master of Technology", "Master of Technology")},
                    { new DDList("Master of Theology", "Master of Theology")},
                    { new DDList("Master of Urban Planning", "Master of Urban Planning")},
                    { new DDList("Master of Veterinary Science", "Master of Veterinary Science")},
                    { new DDList("Doctor of Audiology", "Doctor of Audiology")},
                    { new DDList("Doctor of Aviation (Av.D.)", "Doctor of Aviation (Av.D.)")},
                    { new DDList("Doctor of Business Administration", "Doctor of Business Administration")},
                    { new DDList("Doctor of Canon Law", "Doctor of Canon Law")},
                    { new DDList("Doctor of Chiropractic", "Doctor of Chiropractic")},
                    { new DDList("Doctor of Commerce", "Doctor of Commerce")},
                    { new DDList("Doctor of Community Health", "Doctor of Community Health")},
                    { new DDList("Doctor of Dental Surgery", "Doctor of Dental Surgery")},
                    { new DDList("Doctor of Divinity", "Doctor of Divinity")},
                    { new DDList("Doctor of Education", "Doctor of Education")},
                    { new DDList("Doctor of Engineering", "Doctor of Engineering")},
                    { new DDList("Doctor of Health Administration", "Doctor of Health Administration")},
                    { new DDList("Doctor of Health Science", "Doctor of Health Science")},
                    { new DDList("Doctor of Juridical Science; Juris Doctor", "Doctor of Juridical Science; Juris Doctor")},
                    { new DDList("Doctor of Law; Legum Doctor", "Doctor of Law; Legum Doctor")},
                    { new DDList("Doctor of Liberal Studies", "Doctor of Liberal Studies")},
                    { new DDList("Doctor of Management", "Doctor of Management")},
                    { new DDList("Doctor of Medicine (M.D.)", "Doctor of Medicine (M.D.)")},
                    { new DDList("Doctor of Ministry", "Doctor of Ministry")},
                    { new DDList("Doctor of Modern Languages", "Doctor of Modern Languages")},
                    { new DDList("Doctor of Musical Arts", "Doctor of Musical Arts")},
                    { new DDList("Doctor of Naturopathic Medicine", "Doctor of Naturopathic Medicine")},
                    { new DDList("Doctor of Optometry", "Doctor of Optometry")},
                    { new DDList("Doctor of Osteopathic Medicine", "Doctor of Osteopathic Medicine")},
                    { new DDList("Doctor of Pharmacy", "Doctor of Pharmacy")},
                    { new DDList("Doctor of Philosophy", "Doctor of Philosophy")},
                    { new DDList("Doctor of Public Administration", "Doctor of Public Administration")},
                    { new DDList("Doctor of Science", "Doctor of Science")},
                    { new DDList("Doctor of Theology", "Doctor of Theology")},
                    { new DDList("Doctor of Veterinary Medicine", "Doctor of Veterinary Medicine")},
                    { new DDList("Doctor of Radio oncology", "Doctor of Radio oncology")}

            };

        }
        public static List<DDList> getdegree()
        {
            return new List<DDList>()
            {
                    { new DDList("--Select degree--", "") },
                    { new DDList("Associate Degree in Administration of Justice", "Associate Degree in Administration of Justice")},
                    { new DDList("Associate Degree in Advertising", "Associate Degree in Advertising")},
                    { new DDList("Associate Degree in Agribusiness", "Associate Degree in Agribusiness")},
                    { new DDList("Associate Degree in Animal Management", "Associate Degree in Animal Management")},
                    { new DDList("Associate Degree in Architectural Building Engineering Technology", "Associate Degree in Architectural Building Engineering Technology")},
                    { new DDList("Associate Degree in Architecture and Career Options", "Associate Degree in Architecture and Career Options")},
                    { new DDList("Associate Degree in Art", "Associate Degree in Art")},
                    { new DDList("Associate Degree in Automotive Maintenance Technology", "Associate Degree in Automotive Maintenance Technology")},
                    { new DDList("Associate Degree in Aviation Mechanics", "Associate Degree in Aviation Mechanics")},
                    { new DDList("Associate Degree in Behavioral Science", "Associate Degree in Behavioral Science")},
                    { new DDList("Associate Degree in Boat Mechanics", "Associate Degree in Boat Mechanics")},
                    { new DDList("Associate Degree in Boat Repair and Maintenance", "Associate Degree in Boat Repair and Maintenance")},
                    { new DDList("Associate Degree in Cabinet Design Technology", "Associate Degree in Cabinet Design Technology")},
                    { new DDList("Associate Degree in Child Development: Program Summary", "Associate Degree in Child Development: Program Summary")},
                    { new DDList("Associate Degree in Christian Ministry", "Associate Degree in Christian Ministry")},
                    { new DDList("Associate Degree in Cosmetology Business", "Associate Degree in Cosmetology Business")},
                    { new DDList("Associate Degree in Digital Media", "Associate Degree in Digital Media")},
                    { new DDList("Associate Degree in Early Childhood Special Education", "Associate Degree in Early Childhood Special Education")},
                    { new DDList("Associate Degree in Elementary Education", "Associate Degree in Elementary Education")},
                    { new DDList("Associate Degree in English", "Associate Degree in English")},
                    { new DDList("Associate Degree in Environmental Science", "Associate Degree in Environmental Science")},
                    { new DDList("Associate Degree in Environmental Studies", "Associate Degree in Environmental Studies")},
                    { new DDList("Associate Degree in General Psychology", "Associate Degree in General Psychology")},
                    { new DDList("Associate Degree in History and Information", "Associate Degree in History and Information")},
                    { new DDList("Associate Degree in Interdisciplinary Studies", "Associate Degree in Interdisciplinary Studies")},
                    { new DDList("Associate Degree in International Relations", "Associate Degree in International Relations")},
                    { new DDList("Associate Degree in Landscape Architecture", "Associate Degree in Landscape Architecture")},
                    { new DDList("Associate Degree in Landscaping Design", "Associate Degree in Landscaping Design")},
                    { new DDList("Associate Degree in Library Science", "Associate Degree in Library Science")},
                    { new DDList("Associate Degree in Music", "Associate Degree in Music")},
                    { new DDList("Associate Degree in Wildlife Management", "Associate Degree in Wildlife Management")},
                    { new DDList("Associate Degree in Education", "Associate Degree in Education")},
                    { new DDList("Associate of Applied Science (AAS) in Accelerated Culinary Arts", "Associate of Applied Science (AAS) in Accelerated Culinary Arts")},
                    { new DDList("Associate of Applied Science (AAS) in Accounting Specialist", "Associate of Applied Science (AAS) in Accounting Specialist")},
                    { new DDList("Associate of Applied Science (AAS) in Administrative Support", "Associate of Applied Science (AAS) in Administrative Support")},
                    { new DDList("Associate of Applied Science (AAS) in Baking and Pastry", "Associate of Applied Science (AAS) in Baking and Pastry")},
                    { new DDList("Associate of Applied Science (AAS) in Business Administration", "Associate of Applied Science (AAS) in Business Administration")},
                    { new DDList("Associate of Applied Science (AAS) in Business Administration - Finance", "Associate of Applied Science (AAS) in Business Administration - Finance")},
                    { new DDList("Associate of Applied Science (AAS) in Business Information Systems", "Associate of Applied Science (AAS) in Business Information Systems")},
                    { new DDList("Associate of Applied Science (AAS) in Civil Justice - Law Enforcement", "Associate of Applied Science (AAS) in Civil Justice - Law Enforcement")},
                    { new DDList("Associate of Applied Science (AAS) in Clinical Medical Assisting", "Associate of Applied Science (AAS) in Clinical Medical Assisting")},
                    { new DDList("Associate of Applied Science (AAS) in Computer Applications", "Associate of Applied Science (AAS) in Computer Applications")},
                    { new DDList("Associate of Applied Science (AAS) in Computer Electronics", "Associate of Applied Science (AAS) in Computer Electronics")},
                    { new DDList("Associate of Applied Science (AAS) in Computer Game Design", "Associate of Applied Science (AAS) in Computer Game Design")},
                    { new DDList("Associate of Applied Science (AAS) in Computer Information Systems", "Associate of Applied Science (AAS) in Computer Information Systems")},
                    { new DDList("Associate of Applied Science (AAS) in Culinary Arts", "Associate of Applied Science (AAS) in Culinary Arts")},
                    { new DDList("Associate of Applied Science (AAS) in Digital Media Communications", "Associate of Applied Science (AAS) in Digital Media Communications")},
                    { new DDList("Associate of Applied Science (AAS) in Digital Photography", "Associate of Applied Science (AAS) in Digital Photography")},
                    { new DDList("Associate of Applied Science (AAS) in Electronic Engineering", "Associate of Applied Science (AAS) in Electronic Engineering")},
                    { new DDList("Associate of Applied Science (AAS) in Emergency Medical Services", "Associate of Applied Science (AAS) in Emergency Medical Services")},
                    { new DDList("Associate of Applied Science (AAS) in Health Care Management", "Associate of Applied Science (AAS) in Health Care Management")},
                    { new DDList("Associate of Applied Science (AAS) in Health Information Management", "Associate of Applied Science (AAS) in Health Information Management")},
                    { new DDList("Associate of Applied Science (AAS) in Healthcare Administration", "Associate of Applied Science (AAS) in Healthcare Administration")},
                    { new DDList("Associate of Applied Science (AAS) in Legal Office E-ministration", "Associate of Applied Science (AAS) in Legal Office E-ministration")},
                    { new DDList("Associate of Applied Science (AAS) in Telecommunications Technology", "Associate of Applied Science (AAS) in Telecommunications Technology")},
                    { new DDList("Associate of Applied Science (AAS) in Television Production", "Associate of Applied Science (AAS) in Television Production")},
                    { new DDList("Associate of Applied Science (AAS) in Visual Communications", "Associate of Applied Science (AAS) in Visual Communications")},
                    { new DDList("Associate of Arts (AA) in Computer Information Systems", "Associate of Arts (AA) in Computer Information Systems")},
                    { new DDList("Associate of Arts (AA) in Internetworking Technology", "Associate of Arts (AA) in Internetworking Technology")},
                    { new DDList("Associate of Arts (AA) in Psychology", "Associate of Arts (AA) in Psychology")},
                    { new DDList("Associate of Arts (AA) in Interior Architecture and Design", "Associate of Arts (AA) in Interior Architecture and Design")},
                    { new DDList("Associate of Biotechnology", "Associate of Biotechnology")},
                    { new DDList("Associate of Business Science (ABS) in Individualized Studies", "Associate of Business Science (ABS) in Individualized Studies")},
                    { new DDList("Associate of Early Childhood Education (AECE)", "Associate of Early Childhood Education (AECE)")},
                    { new DDList("Associate of Occupational Studies (AOS) in Legal Office Administration", "Associate of Occupational Studies (AOS) in Legal Office Administration")},
                    { new DDList("Associate of Science (AS) in Computer Information Science", "Associate of Science (AS) in Computer Information Science")},
                    { new DDList("Associate of Science (AS) in Computer Science", "Associate of Science (AS) in Computer Science")},
                    { new DDList("Associate of Science (AS) in Corrections, Probation, & Parole", "Associate of Science (AS) in Corrections, Probation, & Parole")},
                    { new DDList("Associate of Science (AS) in Electronics Engineering Technology", "Associate of Science (AS) in Electronics Engineering Technology")},
                    { new DDList("Associate of Science (AS) in Interactive & Graphic Art", "Associate of Science (AS) in Interactive & Graphic Art")},
                    { new DDList("Associate of Science (AS) in Industrial Maintenance Technology", "Associate of Science (AS) in Industrial Maintenance Technology")},
                    { new DDList("Associate of Arts and Science", "Associate of Arts and Science")},
                    { new DDList("Bachelor of Architecture", "Bachelor of Architecture")},
                    { new DDList("Bachelor of Biomedical Science", "Bachelor of Biomedical Science")},
                    { new DDList("Bachelor of Business Administration", "Bachelor of Business Administration")},
                    { new DDList("Bachelor of Clinical Science", "Bachelor of Clinical Science")},
                    { new DDList("Bachelor of Commerce", "Bachelor of Commerce")},
                    { new DDList("Bachelor of Computer Applications", "Bachelor of Computer Applications")},
                    { new DDList("Bachelor of Community Health", "Bachelor of Community Health")},
                    { new DDList("Bachelor of Computer Information Systems", "Bachelor of Computer Information Systems")},
                    { new DDList("Bachelor of Science in Construction Technology", "Bachelor of Science in Construction Technology")},
                    { new DDList("Bachelor of Criminal Justice", "Bachelor of Criminal Justice")},
                    { new DDList("Bachelor of Divinity", "Bachelor of Divinity")},
                    { new DDList("Bachelor of Economics", "Bachelor of Economics")},
                    { new DDList("Bachelor of Education", "Bachelor of Education")},
                    { new DDList("Bachelor of Engineering", "Bachelor of Engineering")},
                    { new DDList("Bachelor of Fine Arts", "Bachelor of Fine Arts")},
                    { new DDList("Bachelor of Letters", "Bachelor of Letters")},
                    { new DDList("Bachelor of Information Systems", "Bachelor of Information Systems")},
                    { new DDList("Bachelor of Management", "Bachelor of Management")},
                    { new DDList("Bachelor of Music", "Bachelor of Music")},
                    { new DDList("Bachelor of Pharmacy", "Bachelor of Pharmacy")},
                    { new DDList("Bachelor of Philosophy", "Bachelor of Philosophy")},
                    { new DDList("Bachelor of Social Work", "Bachelor of Social Work")},
                    { new DDList("Bachelor of Technology", "Bachelor of Technology")},
                    { new DDList("Bachelor of Accountancy", "Bachelor of Accountancy")},
                    { new DDList("Bachelor of Arts in American Studies", "Bachelor of Arts in American Studies")},
                    { new DDList("Bachelor of Arts in American Indian Studies", "Bachelor of Arts in American Indian Studies")},
                    { new DDList("Bachelor of Arts in Applied Psychology", "Bachelor of Arts in Applied Psychology")},
                    { new DDList("Bachelor of Arts in Biology", "Bachelor of Arts in Biology")},
                    { new DDList("Bachelor of Arts in Anthropology", "Bachelor of Arts in Anthropology")},
                    { new DDList("Bachelor of Arts in Child Advocacy", "Bachelor of Arts in Child Advocacy")},
                    { new DDList("Bachelor of Arts in Clinical Psychology", "Bachelor of Arts in Clinical Psychology")},
                    { new DDList("Bachelor of Arts in Forensic Psychology", "Bachelor of Arts in Forensic Psychology")},
                    { new DDList("Bachelor of Arts in Organizational Psychology", "Bachelor of Arts in Organizational Psychology")},
                    { new DDList("Bachelor of Science in Aerospace Engineering", "Bachelor of Science in Aerospace Engineering")},
                    { new DDList("Bachelor of Science in Actuarial", "Bachelor of Science in Actuarial")},
                    { new DDList("Bachelor of Science in Agriculture", "Bachelor of Science in Agriculture")},
                    { new DDList("Bachelor of Science in Architecture", "Bachelor of Science in Architecture")},
                    { new DDList("Bachelor of Science in Architectural Engineering", "Bachelor of Science in Architectural Engineering")},
                    { new DDList("Bachelor of Science in Athletic Training", "Bachelor of Science in Athletic Training")},
                    { new DDList("Bachelor of Science in Biology", "Bachelor of Science in Biology")},
                    { new DDList("Bachelor of Science in Biomedical Engineering", "Bachelor of Science in Biomedical Engineering")},
                    { new DDList("Bachelor of Science in Bible", "Bachelor of Science in Bible")},
                    { new DDList("Bachelor of Science in Business Administration", "Bachelor of Science in Business Administration")},
                    { new DDList("Bachelor of Science in Business and Technology", "Bachelor of Science in Business and Technology")},
                    { new DDList("Bachelor of Science in Chemical Engineering", "Bachelor of Science in Chemical Engineering")},
                    { new DDList("Bachelor of Science in Chemistry", "Bachelor of Science in Chemistry")},
                    { new DDList("Bachelor of Science in Civil Engineering", "Bachelor of Science in Civil Engineering")},
                    { new DDList("Bachelor of Science in Clinical Laboratory Science", "Bachelor of Science in Clinical Laboratory Science")},
                    { new DDList("Bachelor of Science in Cognitive Science", "Bachelor of Science in Cognitive Science")},
                    { new DDList("Bachelor of Science in Computer Engineering", "Bachelor of Science in Computer Engineering")},
                    { new DDList("Bachelor of Science in Computer Science", "Bachelor of Science in Computer Science")},
                    { new DDList("Bachelor of Science in Construction Engineering", "Bachelor of Science in Construction Engineering")},
                    { new DDList("Bachelor of Science in Construction Management", "Bachelor of Science in Construction Management")},
                    { new DDList("Bachelor of Science in Criminal Justice", "Bachelor of Science in Criminal Justice")},
                    { new DDList("Bachelor of Science in Criminology", "Bachelor of Science in Criminology")},
                    { new DDList("Bachelor of Science in Diagnostic Radiography", "Bachelor of Science in Diagnostic Radiography")},
                    { new DDList("Bachelor of Science in Education", "Bachelor of Science in Education")},
                    { new DDList("Bachelor of Science in Electrical Engineering", "Bachelor of Science in Electrical Engineering")},
                    { new DDList("Bachelor of Science in Engineering Physics", "Bachelor of Science in Engineering Physics")},
                    { new DDList("Bachelor of Science in Engineering Science", "Bachelor of Science in Engineering Science")},
                    { new DDList("Bachelor of Science in Engineering Technology", "Bachelor of Science in Engineering Technology")},
                    { new DDList("Bachelor of Science in English Literature", "Bachelor of Science in English Literature")},
                    { new DDList("Bachelor of Science in Environmental Engineering", "Bachelor of Science in Environmental Engineering")},
                    { new DDList("Bachelor of Science in Environmental Science", "Bachelor of Science in Environmental Science")},
                    { new DDList("Bachelor of Science in Environmental Studies", "Bachelor of Science in Environmental Studies")},
                    { new DDList("Bachelor of Science in Food Science", "Bachelor of Science in Food Science")},
                    { new DDList("Bachelor of Science in Foreign Service", "Bachelor of Science in Foreign Service")},
                    { new DDList("Bachelor of Science in Forensic Science", "Bachelor of Science in Forensic Science")},
                    { new DDList("Bachelor of Science in Forestry", "Bachelor of Science in Forestry")},
                    { new DDList("Bachelor of Science in History", "Bachelor of Science in History")},
                    { new DDList("Bachelor of Science in Hospitality Management", "Bachelor of Science in Hospitality Management")},
                    { new DDList("Bachelor of Science in Human Resources Management", "Bachelor of Science in Human Resources Management")},
                    { new DDList("Bachelor of Science in Industrial Engineering", "Bachelor of Science in Industrial Engineering")},
                    { new DDList("Bachelor of Science in Information Technology", "Bachelor of Science in Information Technology")},
                    { new DDList("Bachelor of Science in Information Systems", "Bachelor of Science in Information Systems")},
                    { new DDList("Bachelor of Science in Integrated Science", "Bachelor of Science in Integrated Science")},
                    { new DDList("Bachelor of Science in International Relations", "Bachelor of Science in International Relations")},
                    { new DDList("Bachelor of Science in Journalism", "Bachelor of Science in Journalism")},
                    { new DDList("Bachelor of Science in Legal Management", "Bachelor of Science in Legal Management")},
                    { new DDList("Bachelor of Science in Management", "Bachelor of Science in Management")},
                    { new DDList("Bachelor of Science in Manufacturing Engineering", "Bachelor of Science in Manufacturing Engineering")},
                    { new DDList("Bachelor of Science in Marketing", "Bachelor of Science in Marketing")},
                    { new DDList("Bachelor of Science in Mathematics", "Bachelor of Science in Mathematics")},
                    { new DDList("Bachelor of Science in Mechanical Engineering", "Bachelor of Science in Mechanical Engineering")},
                    { new DDList("Bachelor of Science in Medical Technology", "Bachelor of Science in Medical Technology")},
                    { new DDList("Bachelor of Science in Metallurgical Engineering", "Bachelor of Science in Metallurgical Engineering")},
                    { new DDList("Bachelor of Science in Meteorology", "Bachelor of Science in Meteorology")},
                    { new DDList("Bachelor of Science in Microbiology", "Bachelor of Science in Microbiology")},
                    { new DDList("Bachelor of Science in Mining Engineering", "Bachelor of Science in Mining Engineering")},
                    { new DDList("Bachelor of Science in Molecular Biology", "Bachelor of Science in Molecular Biology")},
                    { new DDList("Bachelor of Science in Neuroscience", "Bachelor of Science in Neuroscience")},
                    { new DDList("Bachelor of Science in Nursing", "Bachelor of Science in Nursing")},
                    { new DDList("Bachelor of Science in Nutrition science", "Bachelor of Science in Nutrition science")},
                    { new DDList("Bachelor of Science in Software Engineering", "Bachelor of Science in Software Engineering")},
                    { new DDList("Bachelor of Science in Petroleum Engineering", "Bachelor of Science in Petroleum Engineering")},
                    { new DDList("Bachelor of Science in Podiatry", "Bachelor of Science in Podiatry")},
                    { new DDList("Bachelor of Science in Pharmacology", "Bachelor of Science in Pharmacology")},
                    { new DDList("Bachelor of Science in Pharmacy", "Bachelor of Science in Pharmacy")},
                    { new DDList("Bachelor of Science in Physical Therapy", "Bachelor of Science in Physical Therapy")},
                    { new DDList("Bachelor of Science in Physics", "Bachelor of Science in Physics")},
                    { new DDList("Bachelor of Science in Plant Science", "Bachelor of Science in Plant Science")},
                    { new DDList("Bachelor of Science in Politics", "Bachelor of Science in Politics")},
                    { new DDList("Bachelor of Science in Psychology", "Bachelor of Science in Psychology")},
                    { new DDList("Bachelor of Science in Public Safety", "Bachelor of Science in Public Safety")},
                    { new DDList("Bachelor of Science in Physiology", "Bachelor of Science in Physiology")},
                    { new DDList("Bachelor of Science in Quantity Surveying Engineering", "Bachelor of Science in Quantity Surveying Engineering")},
                    { new DDList("Bachelor of Science in Radiologic Technology", "Bachelor of Science in Radiologic Technology")},
                    { new DDList("Bachelor of Science in Real-Time Interactive Simulation", "Bachelor of Science in Real-Time Interactive Simulation")},
                    { new DDList("Bachelor of Science in Religion", "Bachelor of Science in Religion")},
                    { new DDList("Bachelor of Science in Respiratory Therapy", "Bachelor of Science in Respiratory Therapy")},
                    { new DDList("Bachelor of Science in Risk Management and Insurance", "Bachelor of Science in Risk Management and Insurance")},
                    { new DDList("Bachelor of Science in Science Education", "Bachelor of Science in Science Education")},
                    { new DDList("Bachelor of Science in Sports Management", "Bachelor of Science in Sports Management")},
                    { new DDList("Bachelor of Science in Systems Engineering", "Bachelor of Science in Systems Engineering")},
                    { new DDList("Bachelor of Music in Jazz Studies", "Bachelor of Music in Jazz Studies")},
                    { new DDList("Bachelor of Music in Composition", "Bachelor of Music in Composition")},
                    { new DDList("Bachelor of Music in Performance", "Bachelor of Music in Performance")},
                    { new DDList("Bachelor of Music in Theory", "Bachelor of Music in Theory")},
                    { new DDList("Bachelor of Music in Music Education", "Bachelor of Music in Music Education")},
                    { new DDList("Bachelor of Science in Veterinary Technology", "Bachelor of Science in Veterinary Technology")},
                    { new DDList("Master of Accountancy", "Master of Accountancy")},
                    { new DDList("Master of Accounting and Information Systems", "Master of Accounting and Information Systems")},
                    { new DDList("Master of Advanced Study", "Master of Advanced Study")},
                    { new DDList("Master of Applied Finance", "Master of Applied Finance")},
                    { new DDList("Master of Applied Mathematical Sciences", "Master of Applied Mathematical Sciences")},
                    { new DDList("Master of Applied Psychology", "Master of Applied Psychology")},
                    { new DDList("Master of Applied Science", "Master of Applied Science")},
                    { new DDList("Master of Architecture", "Master of Architecture")},
                    { new DDList("Master of Arts", "Master of Arts")},
                    { new DDList("Master of Arts in Archives and Records Management", "Master of Arts in Archives and Records Management")},
                    { new DDList("Master of Arts in Bioethics", "Master of Arts in Bioethics")},
                    { new DDList("Master of Arts in Liberal Studies", "Master of Arts in Liberal Studies")},
                    { new DDList("Master of Arts in Museum Studies", "Master of Arts in Museum Studies")},
                    { new DDList("Master of Arts in Strategic Communication Management", "Master of Arts in Strategic Communication Management")},
                    { new DDList("Master of Arts in Teaching", "Master of Arts in Teaching")},
                    { new DDList("Master of Athletic Training", "Master of Athletic Training")},
                    { new DDList("Master of Bioethics", "Master of Bioethics")},
                    { new DDList("Master of Bioinformatics", "Master of Bioinformatics")},
                    { new DDList("Master of Biotechnology", "Master of Biotechnology")},
                    { new DDList("Master of Business Administration", "Master of Business Administration")},
                    { new DDList("Master of Business Administration Management of Technology", "Master of Business Administration Management of Technology")},
                    { new DDList("Master of Business", "Master of Business")},
                    { new DDList("Master of Business Economics", "Master of Business Economics")},
                    { new DDList("Master of Business Engineering", "Master of Business Engineering")},
                    { new DDList("Master of Business Informatics", "Master of Business Informatics")},
                    { new DDList("Master of Chemistry", "Master of Chemistry")},
                    { new DDList("Master of City Planning", "Master of City Planning")},
                    { new DDList("Master of Commerce", "Master of Commerce")},
                    { new DDList("Master of Community Health", "Master of Community Health")},
                    { new DDList("Master of Computational Finance", "Master of Computational Finance")},
                    { new DDList("Master of Computer Applications", "Master of Computer Applications")},
                    { new DDList("Master of Computer Science", "Master of Computer Science")},
                    { new DDList("Master of Communication", "Master of Communication")},
                    { new DDList("Master of Counseling", "Master of Counseling")},
                    { new DDList("Master of Criminal Justice", "Master of Criminal Justice")},
                    { new DDList("Master in Creative Technologies", "Master in Creative Technologies")},
                    { new DDList("Master of Design", "Master of Design")},
                    { new DDList("Master of Divinity", "Master of Divinity")},
                    { new DDList("Master of Economics", "Master of Economics")},
                    { new DDList("Master of Education", "Master of Education")},
                    { new DDList("Master of Educational Technology", "Master of Educational Technology")},
                    { new DDList("Master of Engineering", "Master of Engineering")},
                    { new DDList("Master of Engineering Management", "Master of Engineering Management")},
                    { new DDList("Master of Enterprise", "Master of Enterprise")},
                    { new DDList("Master of European Law", "Master of European Law")},
                    { new DDList("Master of Finance", "Master of Finance")},
                    { new DDList("Master of Financial Economics", "Master of Financial Economics")},
                    { new DDList("Master of Financial Engineering", "Master of Financial Engineering")},
                    { new DDList("Master of Financial Mathematics", "Master of Financial Mathematics")},
                    { new DDList("Master of Fine Arts", "Master of Fine Arts")},
                    { new DDList("Master of Geospatial Science & Technology", "Master of Geospatial Science & Technology")},
                    { new DDList("Master of Health Administration", "Master of Health Administration")},
                    { new DDList("Master of Health Science", "Master of Health Science")},
                    { new DDList("Master of Humanities", "Master of Humanities")},
                    { new DDList("Master of Industrial and Labor Relations", "Master of Industrial and Labor Relations")},
                    { new DDList("Master of International Affairs", "Master of International Affairs")},
                    { new DDList("Master of International Business", "Master of International Business")},
                    { new DDList("Master of International Economics", "Master of International Economics")},
                    { new DDList("Master of International Public Policy", "Master of International Public Policy")},
                    { new DDList("Master of International Studies", "Master of International Studies")},
                    { new DDList("Master of Information", "Master of Information")},
                    { new DDList("Master of Information Management", "Master of Information Management")},
                    { new DDList("Master of Information Systems", "Master of Information Systems")},
                    { new DDList("Master of Information System Management", "Master of Information System Management")},
                    { new DDList("Master of Islamic Studies", "Master of Islamic Studies")},
                    { new DDList("Master of IT", "Master of IT")},
                    { new DDList("Master of Jurisprudence", "Master of Jurisprudence")},
                    { new DDList("Master of Laws", "Master of Laws")},
                    { new DDList("Master of Studies in Law", "Master of Studies in Law")},
                    { new DDList("Master of Landscape Architecture", "Master of Landscape Architecture")},
                    { new DDList("Master of Letters", "Master of Letters")},
                    { new DDList("Master of Liberal Arts", "Master of Liberal Arts")},
                    { new DDList("Master of Library and Information Science", "Master of Library and Information Science")},
                    { new DDList("Master of Management", "Master of Management")},
                    { new DDList("Master of Mass Communication and Journalism", "Master of Mass Communication and Journalism")},
                    { new DDList("Master of Mathematical Finance", "Master of Mathematical Finance")},
                    { new DDList("Master of Mathematics", "Master of Mathematics")},
                    { new DDList("Master of Mathematics and Computer Science", "Master of Mathematics and Computer Science")},
                    { new DDList("Master of Mathematics and Philosophy", "Master of Mathematics and Philosophy")},
                    { new DDList("Master of Medical Science", "Master of Medical Science")},
                    { new DDList("Master of Medicine", "Master of Medicine")},
                    { new DDList("Master of Military Art and Science", "Master of Military Art and Science")},
                    { new DDList("Master of Music", "Master of Music")},
                    { new DDList("Master of Network and Communications Management", "Master of Network and Communications Management")},
                    { new DDList("Master of Occupational Therapy", "Master of Occupational Therapy")},
                    { new DDList("Master of Pharmacy", "Master of Pharmacy")},
                    { new DDList("Master of Philosophy", "Master of Philosophy")},
                    { new DDList("Master of Physician Assistant Studies", "Master of Physician Assistant Studies")},
                    { new DDList("Master of Physics", "Master of Physics")},
                    { new DDList("Master of Political Science", "Master of Political Science")},
                    { new DDList("Master of Professional Studies", "Master of Professional Studies")},
                    { new DDList("Master of Psychology", "Master of Psychology")},
                    { new DDList("Master of Public Administration", "Master of Public Administration")},
                    { new DDList("Master of Public Affairs", "Master of Public Affairs")},
                    { new DDList("Master of Public Diplomacy", "Master of Public Diplomacy")},
                    { new DDList("Master of Public Health", "Master of Public Health")},
                    { new DDList("Master of Public Management", "Master of Public Management")},
                    { new DDList("Master of Public Policy", "Master of Public Policy")},
                    { new DDList("Master of Public Relations", "Master of Public Relations")},
                    { new DDList("Master of Public Service", "Master of Public Service")},
                    { new DDList("Master of Quantitative Finance", "Master of Quantitative Finance")},
                    { new DDList("Master of Rabbinic Studies", "Master of Rabbinic Studies")},
                    { new DDList("Master of Real Estate Development", "Master of Real Estate Development")},
                    { new DDList("Master of Religious Education", "Master of Religious Education")},
                    { new DDList("Master of Research", "Master of Research")},
                    { new DDList("Master of Sacred Music", "Master of Sacred Music")},
                    { new DDList("Master of Sacred Theology", "Master of Sacred Theology")},
                    { new DDList("Master of Science", "Master of Science")},
                    { new DDList("Master of Science in Applied Cognition and Neuroscience", "Master of Science in Applied Cognition and Neuroscience")},
                    { new DDList("Master of Science in Athletic Training", "Master of Science in Athletic Training")},
                    { new DDList("Master of Science in Bioinformatics", "Master of Science in Bioinformatics")},
                    { new DDList("Master of Science in Clinical Epidemiology", "Master of Science in Clinical Epidemiology")},
                    { new DDList("Master of Science in Computing Research", "Master of Science in Computing Research")},
                    { new DDList("Master of Science in Cyber Security", "Master of Science in Cyber Security")},
                    { new DDList("Master of Science in Education", "Master of Science in Education")},
                    { new DDList("Master of Science in Engineering", "Master of Science in Engineering")},
                    { new DDList("Master of Science in Development Administration", "Master of Science in Development Administration")},
                    { new DDList("Master of Science in Finance", "Master of Science in Finance")},
                    { new DDList("Master of Science in Governance & Organizational Sciences", "Master of Science in Governance & Organizational Sciences")},
                    { new DDList("Master of Science in Government Contracts", "Master of Science in Government Contracts")},
                    { new DDList("Master of Science in Health Informatics", "Master of Science in Health Informatics")},
                    { new DDList("Master of Science in Human Resource Development", "Master of Science in Human Resource Development")},
                    { new DDList("Master of Science in Information Assurance", "Master of Science in Information Assurance")},
                    { new DDList("Master of Science in Information Systems", "Master of Science in Information Systems")},
                    { new DDList("Master of Science in Information Technology", "Master of Science in Information Technology")},
                    { new DDList("Master of Science in Leadership", "Master of Science in Leadership")},
                    { new DDList("Master of Science in Management", "Master of Science in Management")},
                    { new DDList("Master of Science in Nursing", "Master of Science in Nursing")},
                    { new DDList("Master of Science in Project Management", "Master of Science in Project Management")},
                    { new DDList("Master of Science in Quality Assurance", "Master of Science in Quality Assurance")},
                    { new DDList("Master of Science in Risk Management", "Master of Science in Risk Management")},
                    { new DDList("Master of Science in Supply Chain Management", "Master of Science in Supply Chain Management")},
                    { new DDList("Master of Science in Teaching", "Master of Science in Teaching")},
                    { new DDList("Master of Science in Taxation", "Master of Science in Taxation")},
                    { new DDList("Master of Social Science", "Master of Social Science")},
                    { new DDList("Master of Social Work", "Master of Social Work")},
                    { new DDList("Master of Statistics", "Master of Statistics")},
                    { new DDList("Master of Strategic Studies", "Master of Strategic Studies")},
                    { new DDList("Master of Studies", "Master of Studies")},
                    { new DDList("Master of Surgery", "Master of Surgery")},
                    { new DDList("Master of Theological Studies", "Master of Theological Studies")},
                    { new DDList("Master of Technology", "Master of Technology")},
                    { new DDList("Master of Theology", "Master of Theology")},
                    { new DDList("Master of Urban Planning", "Master of Urban Planning")},
                    { new DDList("Master of Veterinary Science", "Master of Veterinary Science")},
                    { new DDList("Doctor of Audiology", "Doctor of Audiology")},
                    { new DDList("Doctor of Aviation (Av.D.)", "Doctor of Aviation (Av.D.)")},
                    { new DDList("Doctor of Business Administration", "Doctor of Business Administration")},
                    { new DDList("Doctor of Canon Law", "Doctor of Canon Law")},
                    { new DDList("Doctor of Chiropractic", "Doctor of Chiropractic")},
                    { new DDList("Doctor of Commerce", "Doctor of Commerce")},
                    { new DDList("Doctor of Community Health", "Doctor of Community Health")},
                    { new DDList("Doctor of Dental Surgery", "Doctor of Dental Surgery")},
                    { new DDList("Doctor of Divinity", "Doctor of Divinity")},
                    { new DDList("Doctor of Education", "Doctor of Education")},
                    { new DDList("Doctor of Engineering", "Doctor of Engineering")},
                    { new DDList("Doctor of Health Administration", "Doctor of Health Administration")},
                    { new DDList("Doctor of Health Science", "Doctor of Health Science")},
                    { new DDList("Doctor of Juridical Science; Juris Doctor", "Doctor of Juridical Science; Juris Doctor")},
                    { new DDList("Doctor of Law; Legum Doctor", "Doctor of Law; Legum Doctor")},
                    { new DDList("Doctor of Liberal Studies", "Doctor of Liberal Studies")},
                    { new DDList("Doctor of Management", "Doctor of Management")},
                    { new DDList("Doctor of Medicine (M.D.)", "Doctor of Medicine (M.D.)")},
                    { new DDList("Doctor of Ministry", "Doctor of Ministry")},
                    { new DDList("Doctor of Modern Languages", "Doctor of Modern Languages")},
                    { new DDList("Doctor of Musical Arts", "Doctor of Musical Arts")},
                    { new DDList("Doctor of Naturopathic Medicine", "Doctor of Naturopathic Medicine")},
                    { new DDList("Doctor of Optometry", "Doctor of Optometry")},
                    { new DDList("Doctor of Osteopathic Medicine", "Doctor of Osteopathic Medicine")},
                    { new DDList("Doctor of Pharmacy", "Doctor of Pharmacy")},
                    { new DDList("Doctor of Philosophy", "Doctor of Philosophy")},
                    { new DDList("Doctor of Public Administration", "Doctor of Public Administration")},
                    { new DDList("Doctor of Science", "Doctor of Science")},
                    { new DDList("Doctor of Theology", "Doctor of Theology")},
                    { new DDList("Doctor of Veterinary Medicine", "Doctor of Veterinary Medicine")},
                    { new DDList("Doctor of Radio oncology", "Doctor of Radio oncology")},
                    {new DDList("Other","Other") }

            };
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
        public static List<DDList>lstNationality { get; set; }
        public static void setNationality()
        {
            lstNationality = new List<DDList>()
            {
                        { new DDList("","-- select one --")},
                        { new DDList("afghan","Afghan")},
                        { new DDList("albanian","Albanian")},
                        { new DDList("algerian","Algerian")},
                        { new DDList("american","American")},
                        { new DDList("andorran","Andorran")},
                        { new DDList("angolan","Angolan")},
                        { new DDList("antiguans","Antiguans")},
                        { new DDList("argentinean","Argentinean")},
                        { new DDList("armenian","Armenian")},
                        { new DDList("australian","Australian")},
                        { new DDList("austrian","Austrian")},
                        { new DDList("azerbaijani","Azerbaijani")},
                        { new DDList("bahamian","Bahamian")},
                        { new DDList("bahraini","Bahraini")},
                        { new DDList("bangladeshi","Bangladeshi")},
                        {  new DDList("barbadian","Barbadian")},
                         { new DDList("barbudans","Barbudans")},
                          {new DDList("batswana","Batswana")},
                          {new DDList("belarusian","Belarusian")},
                          {new DDList("belgian","Belgian")},
                          {new DDList("belizean","Belizean")},
                          {new DDList("beninese","Beninese")},
                          {new DDList("bhutanese","Bhutanese")},
                          {new DDList("bolivian","Bolivian")},
                          {new DDList("bosnian","Bosnian")},
                          {new DDList("brazilian","Brazilian")},
                          {new DDList("british","British")},
                          {new DDList("bruneian","Bruneian")},
                          {new DDList("bulgarian","Bulgarian")},
                          {new DDList("burkinabe","Burkinabe")},
                          {new DDList("burmese","Burmese")},
                          {new DDList("burundian","Burundian")},
                          {new DDList("cambodian","Cambodian")},
                          {new DDList("cameroonian","Cameroonian")},
                          {new DDList("canadian","Canadian")},
                          {new DDList("cape verdean","Cape Verdean")},
                          {new DDList("central african","Central African")},
                          {new DDList("chadian","Chadian")},
                          {new DDList("chilean","Chilean")},
                          {new DDList("chinese","Chinese")},
                          {new DDList("colombian","Colombian")},
                          {new DDList("comoran","Comoran")},
                          {new DDList("congolese","Congolese")},
                          {new DDList("costa rican","Costa Rican")},
                          {new DDList("croatian","Croatian")},
                          {new DDList("cuban","Cuban")},
                          {new DDList("cypriot","Cypriot")},
                          {new DDList("czech","Czech")},
                          {new DDList("danish","Danish")},
                          {new DDList("djibouti","Djibouti")},
                          {new DDList("dominican","Dominican")},
                          {new DDList("dutch","Dutch")},
                          {new DDList("east timorese","East Timorese")},
                          {new DDList("ecuadorean","Ecuadorean")},
                          {new DDList("egyptian","Egyptian")},
                          {new DDList("emirian","Emirian")},
                          {new DDList("equatorial guinean","Equatorial Guinean")},
                          {new DDList("eritrean","Eritrean")},
                          {new DDList("estonian","Estonian")},
                          {new DDList("ethiopian","Ethiopian")},
                          {new DDList("fijian","Fijian")},
                        { new DDList("filipino","Filipino")},
                         { new DDList("finnish","Finnish")},
                         { new DDList("french","French")},
                          {new DDList("gabonese","Gabonese")},
                          {new DDList("gambian","Gambian")},
                          {new DDList("georgian","Georgian")},
                          {new DDList("german","German")},
                          {new DDList("ghanaian","Ghanaian")},
                          {new DDList("greek","Greek")},
                          {new DDList("grenadian","Grenadian")},
                          {new DDList("guatemalan","Guatemalan")},
                          {new DDList("guinea-bissauan","Guinea-Bissauan")},
                          {new DDList("guinean","Guinean")},
                          {new DDList("guyanese","Guyanese")},
                          {new DDList("haitian","Haitian")},
                          {new DDList("herzegovinian","Herzegovinian")},
                          {new DDList("honduran","Honduran")},
                          {new DDList("hungarian","Hungarian")},
                          {new DDList("icelander","Icelander")},
                          {new DDList("indian","Indian")},
                          {new DDList("indonesian","Indonesian")},
                          {new DDList("iranian","Iranian")},
                          {new DDList("iraqi","Iraqi")},
                          {new DDList("irish","Irish")},
                          {new DDList("israeli","Israeli")},
                          {new DDList("italian","Italian")},
                          {new DDList("ivorian","Ivorian")},
                          {new DDList("jamaican","Jamaican")},
                          {new DDList("japanese","Japanese")},
                          {new DDList("jordanian","Jordanian")},
                          {new DDList("kazakhstani","Kazakhstani")},
                          {new DDList("kenyan","Kenyan")},
                          {new DDList("kittian and nevisian","Kittian and Nevisian")},
                          {new DDList("kuwaiti","Kuwaiti")},
                          {new DDList("kyrgyz","Kyrgyz")},
                          {new DDList("laotian","Laotian")},
                          {new DDList("latvian","Latvian")},
                          {new DDList("lebanese","Lebanese")},
                          {new DDList("liberian","Liberian")},
                         { new DDList("libyan","Libyan")},
                          {new DDList("liechtensteiner","Liechtensteiner")},
                          {new DDList("lithuanian","Lithuanian")},
                          {new DDList("luxembourger","Luxembourger")},
                         { new DDList("macedonian","Macedonian")},
                         { new DDList("malagasy","Malagasy")},
                         { new DDList("malawian","Malawian")},
                         { new DDList("malaysian","Malaysian")},
                          {new DDList("maldivan","Maldivan")},
                          {new DDList("malian","Malian")},
                          {new DDList("maltese","Maltese")},
                          {new DDList("marshallese","Marshallese")},
                          {new DDList("mauritanian","Mauritanian")},
                          {new DDList("mauritian","Mauritian")},
                          {new DDList("mexican","Mexican")},
                          {new DDList("micronesian","Micronesian")},
                          {new DDList("moldovan","Moldovan")},
                          {new DDList("monacan","Monacan")},
                          {new DDList("mongolian","Mongolian")},
                          {new DDList("moroccan","Moroccan")},
                          {new DDList("mosotho","Mosotho")},
                          {new DDList("motswana","Motswana")},
                          {new DDList("mozambican","Mozambican")},
                          {new DDList("namibian","Namibian")},
                          {new DDList("nauruan","Nauruan")},
                          {new DDList("nepalese","Nepalese")},
                          {new DDList("new zealander","New Zealander")},
                          {new DDList("ni-vanuatu","Ni-Vanuatu")},
                          {new DDList("nicaraguan","Nicaraguan")},
                          {new DDList("nigerien","Nigerien")},
                          {new DDList("north korean","North Korean")},
                         { new DDList("northern irish","Northern Irish")},
                         { new DDList("norwegian","Norwegian")},
                         { new DDList("omani","Omani")},
                         { new DDList("pakistani","Pakistani")},
                         { new DDList("palauan","Palauan")},
                         { new DDList("panamanian","Panamanian")},
                         { new DDList("papua new guinean","Papua New Guinean")},
                         { new DDList("paraguayan","Paraguayan")},
                         { new DDList("peruvian","Peruvian")},
                         { new DDList("polish","Polish")},
                         { new DDList("portuguese","Portuguese")},
                         { new DDList("qatari","Qatari")},
                         { new DDList("romanian","Romanian")},
                        {  new DDList("russian","Russian")},
                        {  new DDList("rwandan","Rwandan")},
                        {  new DDList("saint lucian","Saint Lucian")},
                        {  new DDList("salvadoran","Salvadoran")},
                        {  new DDList("samoan","Samoan")},
                        {  new DDList("san marinese","San Marinese")},
                        {  new DDList("sao tomean","Sao Tomean")},
                        {  new DDList("saudi","Saudi")},
                        {  new DDList("scottish","Scottish")},
                         { new DDList("senegalese","Senegalese")},
                         { new DDList("serbian","Serbian")},
                         { new DDList("seychellois","Seychellois")},
                         { new DDList("sierra leonean","Sierra Leonean")},
                         { new DDList("singaporean","Singaporean")},
                         { new DDList("slovakian","Slovakian")},
                         { new DDList("slovenian","Slovenian")},
                         { new DDList("solomon islander","Solomon Islander")},
                        {  new DDList("somali","Somali")},
                         { new DDList("south african","South African")},
                         { new DDList("south korean","South Korean")},
                         { new DDList("spanish","Spanish")},
                         { new DDList("sri lankan","Sri Lankan")},
                         { new DDList("sudanese","Sudanese")},
                         { new DDList("surinamer","Surinamer")},
                          {new DDList("swazi","Swazi")},
                          {new DDList("swedish","Swedish")},
                          {new DDList("swiss","Swiss")},
                         { new DDList("syrian","Syrian")},
                         { new DDList("taiwanese","Taiwanese")},
                         { new DDList("tajik","Tajik")},
                         { new DDList("tanzanian","Tanzanian")},
                         { new DDList("thai","Thai")},
                         { new DDList("togolese","Togolese")},
                         { new DDList("tongan","Tongan")},
                         { new DDList("trinidadian or tobagonian","Trinidadian or Tobagonian")},
                         { new DDList("tunisian","Tunisian")},
                          {new DDList("turkish","Turkish")},
                          {new DDList("tuvaluan","Tuvaluan")},
                          {new DDList("ugandan","Ugandan")},
                          {new DDList("ukrainian","Ukrainian")},
                          {new DDList("uruguayan","Uruguayan")},
                          {new DDList("uzbekistani","Uzbekistani")},
                          {new DDList("venezuelan","Venezuelan")},
                         { new DDList("vietnamese","Vietnamese")},
                         { new DDList("welsh","Welsh")},
                         { new DDList("yemenite","Yemenite")},
                         { new DDList("zambian","Zambian")},
                         { new DDList("zimbabwean","Zimbabwean")}


            };
        }
        public static List<DDList> getNationality()
        {
            return  new List<DDList>()
            {
                        { new DDList("","-- Select Nationality --")},
                        { new DDList("afghan","Afghan")},
                        { new DDList("albanian","Albanian")},
                        { new DDList("algerian","Algerian")},
                        { new DDList("american","American")},
                        { new DDList("andorran","Andorran")},
                        { new DDList("angolan","Angolan")},
                        { new DDList("antiguans","Antiguans")},
                        { new DDList("argentinean","Argentinean")},
                        { new DDList("armenian","Armenian")},
                        { new DDList("australian","Australian")},
                        { new DDList("austrian","Austrian")},
                        { new DDList("azerbaijani","Azerbaijani")},
                        { new DDList("bahamian","Bahamian")},
                        { new DDList("bahraini","Bahraini")},
                        { new DDList("bangladeshi","Bangladeshi")},
                        {  new DDList("barbadian","Barbadian")},
                         { new DDList("barbudans","Barbudans")},
                          {new DDList("batswana","Batswana")},
                          {new DDList("belarusian","Belarusian")},
                          {new DDList("belgian","Belgian")},
                          {new DDList("belizean","Belizean")},
                          {new DDList("beninese","Beninese")},
                          {new DDList("bhutanese","Bhutanese")},
                          {new DDList("bolivian","Bolivian")},
                          {new DDList("bosnian","Bosnian")},
                          {new DDList("brazilian","Brazilian")},
                          {new DDList("british","British")},
                          {new DDList("bruneian","Bruneian")},
                          {new DDList("bulgarian","Bulgarian")},
                          {new DDList("burkinabe","Burkinabe")},
                          {new DDList("burmese","Burmese")},
                          {new DDList("burundian","Burundian")},
                          {new DDList("cambodian","Cambodian")},
                          {new DDList("cameroonian","Cameroonian")},
                          {new DDList("canadian","Canadian")},
                          {new DDList("cape verdean","Cape Verdean")},
                          {new DDList("central african","Central African")},
                          {new DDList("chadian","Chadian")},
                          {new DDList("chilean","Chilean")},
                          {new DDList("chinese","Chinese")},
                          {new DDList("colombian","Colombian")},
                          {new DDList("comoran","Comoran")},
                          {new DDList("congolese","Congolese")},
                          {new DDList("costa rican","Costa Rican")},
                          {new DDList("croatian","Croatian")},
                          {new DDList("cuban","Cuban")},
                          {new DDList("cypriot","Cypriot")},
                          {new DDList("czech","Czech")},
                          {new DDList("danish","Danish")},
                          {new DDList("djibouti","Djibouti")},
                          {new DDList("dominican","Dominican")},
                          {new DDList("dutch","Dutch")},
                          {new DDList("east timorese","East Timorese")},
                          {new DDList("ecuadorean","Ecuadorean")},
                          {new DDList("egyptian","Egyptian")},
                          {new DDList("emirian","Emirian")},
                          {new DDList("equatorial guinean","Equatorial Guinean")},
                          {new DDList("eritrean","Eritrean")},
                          {new DDList("estonian","Estonian")},
                          {new DDList("ethiopian","Ethiopian")},
                          {new DDList("fijian","Fijian")},
                        { new DDList("filipino","Filipino")},
                         { new DDList("finnish","Finnish")},
                         { new DDList("french","French")},
                          {new DDList("gabonese","Gabonese")},
                          {new DDList("gambian","Gambian")},
                          {new DDList("georgian","Georgian")},
                          {new DDList("german","German")},
                          {new DDList("ghanaian","Ghanaian")},
                          {new DDList("greek","Greek")},
                          {new DDList("grenadian","Grenadian")},
                          {new DDList("guatemalan","Guatemalan")},
                          {new DDList("guinea-bissauan","Guinea-Bissauan")},
                          {new DDList("guinean","Guinean")},
                          {new DDList("guyanese","Guyanese")},
                          {new DDList("haitian","Haitian")},
                          {new DDList("herzegovinian","Herzegovinian")},
                          {new DDList("honduran","Honduran")},
                          {new DDList("hungarian","Hungarian")},
                          {new DDList("icelander","Icelander")},
                          {new DDList("indian","Indian")},
                          {new DDList("indonesian","Indonesian")},
                          {new DDList("iranian","Iranian")},
                          {new DDList("iraqi","Iraqi")},
                          {new DDList("irish","Irish")},
                          {new DDList("israeli","Israeli")},
                          {new DDList("italian","Italian")},
                          {new DDList("ivorian","Ivorian")},
                          {new DDList("jamaican","Jamaican")},
                          {new DDList("japanese","Japanese")},
                          {new DDList("jordanian","Jordanian")},
                          {new DDList("kazakhstani","Kazakhstani")},
                          {new DDList("kenyan","Kenyan")},
                          {new DDList("kittian and nevisian","Kittian and Nevisian")},
                          {new DDList("kuwaiti","Kuwaiti")},
                          {new DDList("kyrgyz","Kyrgyz")},
                          {new DDList("laotian","Laotian")},
                          {new DDList("latvian","Latvian")},
                          {new DDList("lebanese","Lebanese")},
                          {new DDList("liberian","Liberian")},
                         { new DDList("libyan","Libyan")},
                          {new DDList("liechtensteiner","Liechtensteiner")},
                          {new DDList("lithuanian","Lithuanian")},
                          {new DDList("luxembourger","Luxembourger")},
                         { new DDList("macedonian","Macedonian")},
                         { new DDList("malagasy","Malagasy")},
                         { new DDList("malawian","Malawian")},
                         { new DDList("malaysian","Malaysian")},
                          {new DDList("maldivan","Maldivan")},
                          {new DDList("malian","Malian")},
                          {new DDList("maltese","Maltese")},
                          {new DDList("marshallese","Marshallese")},
                          {new DDList("mauritanian","Mauritanian")},
                          {new DDList("mauritian","Mauritian")},
                          {new DDList("mexican","Mexican")},
                          {new DDList("micronesian","Micronesian")},
                          {new DDList("moldovan","Moldovan")},
                          {new DDList("monacan","Monacan")},
                          {new DDList("mongolian","Mongolian")},
                          {new DDList("moroccan","Moroccan")},
                          {new DDList("mosotho","Mosotho")},
                          {new DDList("motswana","Motswana")},
                          {new DDList("mozambican","Mozambican")},
                          {new DDList("namibian","Namibian")},
                          {new DDList("nauruan","Nauruan")},
                          {new DDList("nepalese","Nepalese")},
                          {new DDList("new zealander","New Zealander")},
                          {new DDList("ni-vanuatu","Ni-Vanuatu")},
                          {new DDList("nicaraguan","Nicaraguan")},
                          {new DDList("nigerien","Nigerien")},
                          {new DDList("north korean","North Korean")},
                         { new DDList("northern irish","Northern Irish")},
                         { new DDList("norwegian","Norwegian")},
                         { new DDList("omani","Omani")},
                         { new DDList("pakistani","Pakistani")},
                         { new DDList("palauan","Palauan")},
                         { new DDList("panamanian","Panamanian")},
                         { new DDList("papua new guinean","Papua New Guinean")},
                         { new DDList("paraguayan","Paraguayan")},
                         { new DDList("peruvian","Peruvian")},
                         { new DDList("polish","Polish")},
                         { new DDList("portuguese","Portuguese")},
                         { new DDList("qatari","Qatari")},
                         { new DDList("romanian","Romanian")},
                        {  new DDList("russian","Russian")},
                        {  new DDList("rwandan","Rwandan")},
                        {  new DDList("saint lucian","Saint Lucian")},
                        {  new DDList("salvadoran","Salvadoran")},
                        {  new DDList("samoan","Samoan")},
                        {  new DDList("san marinese","San Marinese")},
                        {  new DDList("sao tomean","Sao Tomean")},
                        {  new DDList("saudi","Saudi")},
                        {  new DDList("scottish","Scottish")},
                         { new DDList("senegalese","Senegalese")},
                         { new DDList("serbian","Serbian")},
                         { new DDList("seychellois","Seychellois")},
                         { new DDList("sierra leonean","Sierra Leonean")},
                         { new DDList("singaporean","Singaporean")},
                         { new DDList("slovakian","Slovakian")},
                         { new DDList("slovenian","Slovenian")},
                         { new DDList("solomon islander","Solomon Islander")},
                        {  new DDList("somali","Somali")},
                         { new DDList("south african","South African")},
                         { new DDList("south korean","South Korean")},
                         { new DDList("spanish","Spanish")},
                         { new DDList("sri lankan","Sri Lankan")},
                         { new DDList("sudanese","Sudanese")},
                         { new DDList("surinamer","Surinamer")},
                          {new DDList("swazi","Swazi")},
                          {new DDList("swedish","Swedish")},
                          {new DDList("swiss","Swiss")},
                         { new DDList("syrian","Syrian")},
                         { new DDList("taiwanese","Taiwanese")},
                         { new DDList("tajik","Tajik")},
                         { new DDList("tanzanian","Tanzanian")},
                         { new DDList("thai","Thai")},
                         { new DDList("togolese","Togolese")},
                         { new DDList("tongan","Tongan")},
                         { new DDList("trinidadian or tobagonian","Trinidadian or Tobagonian")},
                         { new DDList("tunisian","Tunisian")},
                          {new DDList("turkish","Turkish")},
                          {new DDList("tuvaluan","Tuvaluan")},
                          {new DDList("ugandan","Ugandan")},
                          {new DDList("ukrainian","Ukrainian")},
                          {new DDList("uruguayan","Uruguayan")},
                          {new DDList("uzbekistani","Uzbekistani")},
                          {new DDList("venezuelan","Venezuelan")},
                         { new DDList("vietnamese","Vietnamese")},
                         { new DDList("welsh","Welsh")},
                         { new DDList("yemenite","Yemenite")},
                         { new DDList("zambian","Zambian")},
                         { new DDList("zimbabwean","Zimbabwean")}


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
                { new DDList("PhD", "6") },
                { new DDList("Other Certification","6") }
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
                { new DDList("PhD", "6") },
                { new DDList("Other Certification","6") }
           };
        }
        public static List<DDList> lstCurrency { get; set; }
        public static void getCurrency()
        {
            lstCurrency = new List<DDList>()
            {               {new DDList("--Select Currency--","")},
                            {new DDList("America (United States) Dollars - USD","USD")},
                            {new DDList("Afghanistan Afghanis - AFN","AFN")},
                            {new DDList("Albania Leke - ALL","ALL")},
                            {new DDList("Algeria Dinars - DZD","DZD")},
                            {new DDList("Argentina Pesos - ARS","ARS")},
                            {new DDList("Australia Dollars - AUD","AUD")},
                            {new DDList("Austria Schillings - ATS","ATS")},

                            {new DDList("Bahamas Dollars - BSD","BSD")},
                            {new DDList("Bahrain Dinars - BHD","BHD")},
                            {new DDList("Bangladesh Taka - BDT","BDT")},
                            {new DDList("Barbados Dollars - BBD","BBD")},
                            {new DDList("Belgium Francs - BEF","BEF")},
                            {new DDList("Bermuda Dollars - BMD","BMD")},

                            {new DDList("Brazil Reais - BRL","BRL")},
                            {new DDList("Bulgaria Leva - BGN","BGN")},
                            {new DDList("Canada Dollars - CAD","CAD")},
                            {new DDList("CFA BCEAO Francs - XOF","XOF")},
                            {new DDList("CFA BEAC Francs - XAF","XAF")},
                            {new DDList("Chile Pesos - CLP","CLP")},

                            {new DDList("China Yuan Renminbi - CNY","CNY")},
                            {new DDList("RMB (China Yuan Renminbi) - CNY","RMB")},
                            {new DDList("Colombia Pesos - COP","COP")},
                            {new DDList("CFP Francs - XPF","XPF")},
                            {new DDList("Costa Rica Colones - CRC","CRC")},
                            {new DDList("Croatia Kuna - HRK","HRK")},

                            {new DDList("Cyprus Pounds - CYP","CYP")},
                            {new DDList("Czech Republic Koruny - CZK","CZK")},
                            {new DDList("Denmark Kroner - DKK","DKK")},
                            {new DDList("Deutsche (Germany) Marks - DEM","DEM")},
                            {new DDList("Dominican Republic Pesos - DOP","DOP")},
                            {new DDList("Dutch (Netherlands) Guilders - NLG","NLG")},

                            {new DDList("Eastern Caribbean Dollars - XCD","XCD")},
                            {new DDList("Egypt Pounds - EGP","EGP")},
                            {new DDList("Estonia Krooni - EEK","EEK")},
                            {new DDList("Euro - EUR","EUR")},
                            {new DDList("Fiji Dollars - FJD","FJD")},
                            {new DDList("Finland Markkaa - FIM","FIM")},

                            {new DDList("France Francs - FRF*","FRF*")},
                            {new DDList("Germany Deutsche Marks - DEM","DEM")},
                            {new DDList("Gold Ounces - XAU","XAU")},
                            {new DDList("Greece Drachmae - GRD","GRD")},
                            {new DDList("Guatemalan Quetzal - GTQ","GTQ")},
                            {new DDList("Holland (Netherlands) Guilders - NLG","NLG")},
                            {new DDList("Hong Kong Dollars - HKD","HKD")},

                            {new DDList("Hungary Forint - HUF","HUF")},
                            {new DDList("Iceland Kronur - ISK","ISK")},
                            {new DDList("IMF Special Drawing Right - XDR","XDR")},
                            {new DDList("India Rupees - INR","INR")},
                            {new DDList("Indonesia Rupiahs - IDR","IDR")},
                            {new DDList("Iran Rials - IRR","IRR")},

                            {new DDList("Iraq Dinars - IQD","IQD")},
                            {new DDList("Ireland Pounds - IEP*","IEP*")},
                            {new DDList("Israel New Shekels - ILS","ILS")},
                            {new DDList("Italy Lire - ITL*","ITL*")},
                            {new DDList("Jamaica Dollars - JMD","JMD")},
                            {new DDList("Japan Yen - JPY","JPY")},

                            {new DDList("Jordan Dinars - JOD","JOD")},
                            {new DDList("Kenya Shillings - KES","KES")},
                            {new DDList("Korea (South) Won - KRW","KRW")},
                            {new DDList("Kuwait Dinars - KWD","KWD")},
                            {new DDList("Lebanon Pounds - LBP","LBP")},
                            {new DDList("Luxembourg Francs - LUF","LUF")},

                            {new DDList("Malaysia Ringgits - MYR","MYR")},
                            {new DDList("Malta Liri - MTL","MTL")},
                            {new DDList("Mauritius Rupees - MUR","MUR")},
                            {new DDList("Mexico Pesos - MXN","MXN")},
                            {new DDList("Morocco Dirhams - MAD","MAD")},
                            {new DDList("Netherlands Guilders - NLG","NLG")},

                            {new DDList("New Zealand Dollars - NZD","NZD")},
                            {new DDList("Norway Kroner - NOK","NOK")},
                            {new DDList("Oman Rials - OMR","OMR")},
                            {new DDList("Pakistan Rupees - PKR","PKR")},
                            {new DDList("Palladium Ounces - XPD","XPD")},
                            {new DDList("Peru Nuevos Soles - PEN","PEN")},

                            {new DDList("Philippines Pesos - PHP","PHP")},
                            {new DDList("Platinum Ounces - XPT","XPT")},
                            {new DDList("Poland Zlotych - PLN","PLN")},
                            {new DDList("Portugal Escudos - PTE","PTE")},
                            {new DDList("Qatar Riyals - QAR","QAR")},
                            {new DDList("Romania New Lei - RON","RON")},

                            {new DDList("Romania Lei - ROL","ROL")},
                            {new DDList("Russia Rubles - RUB","RUB")},
                            {new DDList("Saudi Arabia Riyals - SAR","SAR")},
                            {new DDList("Silver Ounces - XAG","XAG")},
                            {new DDList("Singapore Dollars - SGD","SGD")},
                            {new DDList("Slovakia Koruny - SKK","SKK")},

                            {new DDList("Slovenia Tolars - SIT","SIT")},
                            {new DDList("South Africa Rand - ZAR","ZAR")},
                            {new DDList("South Korea Won - KRW","KRW")},
                            {new DDList("Spain Pesetas - ESP","ESP")},
                            {new DDList("Special Drawing Rights (IMF) - XDR","XDR")},
                            {new DDList("Sri Lanka Rupees - LKR","LKR")},

                            {new DDList("Sudan Dinars - SDD","SDD")},
                            {new DDList("Sweden Kronor - SEK","SEK")},
                            {new DDList("Switzerland Francs - CHF","CHF")},
                            {new DDList("Taiwan New Dollars - TWD","TWD")},
                            {new DDList("Thailand Baht - THB","THB")},
                            {new DDList("Trinidad and Tobago Dollars - TTD","TTD")},

                            {new DDList("Tunisia Dinars - TND","TND")},
                            {new DDList("Turkey New Lira - TRY","TRY")},
                            {new DDList("United Arab Emirates Dirhams - AED","AED")},
                            {new DDList("United Kingdom Pounds - GBP","GBP")},
                            {new DDList("United States Dollars - USD","USD")},
                            {new DDList("Venezuela Bolivares - VEB","VEB")},

                            {new DDList("Vietnam Dong - VND","VND")},
                            {new DDList("Zambia Kwacha - ZMK","ZMK")}

            };
        }

        public static List<DDList> getNewCurreny()
        {
            return new List<DDList>()
            {               {new DDList("--Select Currency--","")},
                            {new DDList("America (United States) Dollars - USD","USD")},
                            {new DDList("Afghanistan Afghanis - AFN","AFN")},
                            {new DDList("Albania Leke - ALL","ALL")},
                            {new DDList("Algeria Dinars - DZD","DZD")},
                            {new DDList("Argentina Pesos - ARS","ARS")},
                            {new DDList("Australia Dollars - AUD","AUD")},
                            {new DDList("Austria Schillings - ATS","ATS")},

                            {new DDList("Bahamas Dollars - BSD","BSD")},
                            {new DDList("Bahrain Dinars - BHD","BHD")},
                            {new DDList("Bangladesh Taka - BDT","BDT")},
                            {new DDList("Barbados Dollars - BBD","BBD")},
                            {new DDList("Belgium Francs - BEF","BEF")},
                            {new DDList("Bermuda Dollars - BMD","BMD")},

                            {new DDList("Brazil Reais - BRL","BRL")},
                            {new DDList("Bulgaria Leva - BGN","BGN")},
                            {new DDList("Canada Dollars - CAD","CAD")},
                            {new DDList("CFA BCEAO Francs - XOF","XOF")},
                            {new DDList("CFA BEAC Francs - XAF","XAF")},
                            {new DDList("Chile Pesos - CLP","CLP")},

                            {new DDList("China Yuan Renminbi - CNY","CNY")},
                            {new DDList("RMB (China Yuan Renminbi) - CNY","RMB")},
                            {new DDList("Colombia Pesos - COP","COP")},
                            {new DDList("CFP Francs - XPF","XPF")},
                            {new DDList("Costa Rica Colones - CRC","CRC")},
                            {new DDList("Croatia Kuna - HRK","HRK")},

                            {new DDList("Cyprus Pounds - CYP","CYP")},
                            {new DDList("Czech Republic Koruny - CZK","CZK")},
                            {new DDList("Denmark Kroner - DKK","DKK")},
                            {new DDList("Deutsche (Germany) Marks - DEM","DEM")},
                            {new DDList("Dominican Republic Pesos - DOP","DOP")},
                            {new DDList("Dutch (Netherlands) Guilders - NLG","NLG")},

                            {new DDList("Eastern Caribbean Dollars - XCD","XCD")},
                            {new DDList("Egypt Pounds - EGP","EGP")},
                            {new DDList("Estonia Krooni - EEK","EEK")},
                            {new DDList("Euro - EUR","EUR")},
                            {new DDList("Fiji Dollars - FJD","FJD")},
                            {new DDList("Finland Markkaa - FIM","FIM")},

                            {new DDList("France Francs - FRF*","FRF*")},
                            {new DDList("Germany Deutsche Marks - DEM","DEM")},
                            {new DDList("Gold Ounces - XAU","XAU")},
                            {new DDList("Greece Drachmae - GRD","GRD")},
                            {new DDList("Guatemalan Quetzal - GTQ","GTQ")},
                            {new DDList("Holland (Netherlands) Guilders - NLG","NLG")},
                            {new DDList("Hong Kong Dollars - HKD","HKD")},

                            {new DDList("Hungary Forint - HUF","HUF")},
                            {new DDList("Iceland Kronur - ISK","ISK")},
                            {new DDList("IMF Special Drawing Right - XDR","XDR")},
                            {new DDList("India Rupees - INR","INR")},
                            {new DDList("Indonesia Rupiahs - IDR","IDR")},
                            {new DDList("Iran Rials - IRR","IRR")},

                            {new DDList("Iraq Dinars - IQD","IQD")},
                            {new DDList("Ireland Pounds - IEP*","IEP*")},
                            {new DDList("Israel New Shekels - ILS","ILS")},
                            {new DDList("Italy Lire - ITL*","ITL*")},
                            {new DDList("Jamaica Dollars - JMD","JMD")},
                            {new DDList("Japan Yen - JPY","JPY")},

                            {new DDList("Jordan Dinars - JOD","JOD")},
                            {new DDList("Kenya Shillings - KES","KES")},
                            {new DDList("Korea (South) Won - KRW","KRW")},
                            {new DDList("Kuwait Dinars - KWD","KWD")},
                            {new DDList("Lebanon Pounds - LBP","LBP")},
                            {new DDList("Luxembourg Francs - LUF","LUF")},

                            {new DDList("Malaysia Ringgits - MYR","MYR")},
                            {new DDList("Malta Liri - MTL","MTL")},
                            {new DDList("Mauritius Rupees - MUR","MUR")},
                            {new DDList("Mexico Pesos - MXN","MXN")},
                            {new DDList("Morocco Dirhams - MAD","MAD")},
                            {new DDList("Netherlands Guilders - NLG","NLG")},

                            {new DDList("New Zealand Dollars - NZD","NZD")},
                            {new DDList("Norway Kroner - NOK","NOK")},
                            {new DDList("Oman Rials - OMR","OMR")},
                            {new DDList("Pakistan Rupees - PKR","PKR")},
                            {new DDList("Palladium Ounces - XPD","XPD")},
                            {new DDList("Peru Nuevos Soles - PEN","PEN")},

                            {new DDList("Philippines Pesos - PHP","PHP")},
                            {new DDList("Platinum Ounces - XPT","XPT")},
                            {new DDList("Poland Zlotych - PLN","PLN")},
                            {new DDList("Portugal Escudos - PTE","PTE")},
                            {new DDList("Qatar Riyals - QAR","QAR")},
                            {new DDList("Romania New Lei - RON","RON")},

                            {new DDList("Romania Lei - ROL","ROL")},
                            {new DDList("Russia Rubles - RUB","RUB")},
                            {new DDList("Saudi Arabia Riyals - SAR","SAR")},
                            {new DDList("Silver Ounces - XAG","XAG")},
                            {new DDList("Singapore Dollars - SGD","SGD")},
                            {new DDList("Slovakia Koruny - SKK","SKK")},

                            {new DDList("Slovenia Tolars - SIT","SIT")},
                            {new DDList("South Africa Rand - ZAR","ZAR")},
                            {new DDList("South Korea Won - KRW","KRW")},
                            {new DDList("Spain Pesetas - ESP","ESP")},
                            {new DDList("Special Drawing Rights (IMF) - XDR","XDR")},
                            {new DDList("Sri Lanka Rupees - LKR","LKR")},

                            {new DDList("Sudan Dinars - SDD","SDD")},
                            {new DDList("Sweden Kronor - SEK","SEK")},
                            {new DDList("Switzerland Francs - CHF","CHF")},
                            {new DDList("Taiwan New Dollars - TWD","TWD")},
                            {new DDList("Thailand Baht - THB","THB")},
                            {new DDList("Trinidad and Tobago Dollars - TTD","TTD")},

                            {new DDList("Tunisia Dinars - TND","TND")},
                            {new DDList("Turkey New Lira - TRY","TRY")},
                            {new DDList("United Arab Emirates Dirhams - AED","AED")},
                            {new DDList("United Kingdom Pounds - GBP","GBP")},
                            {new DDList("United States Dollars - USD","USD")},
                            {new DDList("Venezuela Bolivares - VEB","VEB")},

                            {new DDList("Vietnam Dong - VND","VND")},
                            {new DDList("Zambia Kwacha - ZMK","ZMK")}

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
                //{ new DDList("Both Male and Female", "MF") },
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
