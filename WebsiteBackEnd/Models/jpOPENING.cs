using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;

namespace WebsiteBackEnd.Models
{
	public class jpopening
	{
		public int OPENINGID { get; set; }
		public string JOBTITLE { get; set; }
		public string COMPANYNAME { get; set; }
		public string CITYNAME { get; set; }
		public string COUNTRYNAME { get; set; }
		public string DEPARTMENT { get; set; }
		public string SHORTDESC { get; set; }
		public string JOBTYPE { get; set; }
		public int SALARYRANGE { get; set; }
		public string MINIMUMEDUCATION { get; set; }
		public string MINIMUMEXPERIENCE { get; set; }
		public string GENDER { get; set; }
		public string JOBSTATUS { get; set; }
        public string ALLOWLONGCV { get; set; }
        public DateTime? JOBPOSTDATE { get; set; }
        public string MAXIMUMEDUCATION { get; set; }
        public string MAXIMUMEXPERIENCE { get; set; }
        public string CURRENCY { get; set; }
        //other vars
        public string ExpectedSalary { get; set; }
        public DateTime? APPLIEDDATE { get; set; }

    }
	public class jpopeningManager : BaseManager
{
    public static List<jpopening> Getjpopening(string whereclause, MySqlConnection conn = null)
    {
        jpopening objjpopening = null;
        List<jpopening> lstjpopening = new List<jpopening>();
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            string sql = "select * from jpopening ";
            if (!string.IsNullOrEmpty(whereclause))
                sql += " where " + whereclause;
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Connection = connection;
                command.CommandText = sql;
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objjpopening = ReaderDatajpopening(reader);
                            lstjpopening.Add(objjpopening);
                        }
                    }
                    else
                    {
                    }
                }
            }

            if (isConnArgNull == true)
            {
                connection.Dispose();
            }
        }
        catch (Exception ex)
        {
            Logger._log.Error(ex.Message + "\n" + ex.StackTrace);
        }

        return lstjpopening;
    }

    private static jpopening ReaderDatajpopening(MySqlDataReader reader)
    {
        jpopening objjpopening = new jpopening();
        objjpopening.OPENINGID = Utility.IsValidInt(reader["OPENINGID"]);
        objjpopening.JOBTITLE = Utility.IsValidString(reader["JOBTITLE"]);
        objjpopening.COMPANYNAME = Utility.IsValidString(reader["COMPANYNAME"]);
        objjpopening.CITYNAME = Utility.IsValidString(reader["CITYNAME"]);
        objjpopening.COUNTRYNAME = Utility.IsValidString(reader["COUNTRYNAME"]);
        objjpopening.DEPARTMENT = Utility.IsValidString(reader["DEPARTMENT"]);
        objjpopening.SHORTDESC = Utility.IsValidString(reader["SHORTDESC"]);
        objjpopening.JOBTYPE = Utility.IsValidString(reader["JOBTYPE"]);
        objjpopening.SALARYRANGE = Utility.IsValidInt(reader["SALARYRANGE"]);
        objjpopening.MINIMUMEDUCATION = Utility.IsValidString(reader["MINIMUMEDUCATION"]);
        objjpopening.MINIMUMEXPERIENCE = Utility.IsValidString(reader["MINIMUMEXPERIENCE"]);
        objjpopening.GENDER = Utility.IsValidString(reader["GENDER"]);
        objjpopening.JOBSTATUS = Utility.IsValidString(reader["JOBSTATUS"]);
        objjpopening.ALLOWLONGCV = Utility.IsValidString(reader["ALLOWLONGCV"]);
        objjpopening.JOBPOSTDATE = Utility.IsValidDateTime(reader["JOBPOSTDATE"]);
            objjpopening.MAXIMUMEDUCATION = Utility.IsValidString(reader["MAXIMUMEDUCATION"]);
            objjpopening.MAXIMUMEXPERIENCE = Utility.IsValidString(reader["MAXIMUMEXPERIENCE"]);
            objjpopening.CURRENCY = Utility.IsValidString(reader["CURRENCY"]);
          

        return objjpopening;
    }

    public static string Savejpopening(jpopening objjpopening, MySqlConnection conn = null)
    {
        string returnMessage = "";
        string sOPENINGID = "";
        sOPENINGID = objjpopening.OPENINGID.ToString();
        var templstjpopening = Getjpopening("OPENINGID = '" + sOPENINGID + "'", conn);
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            using (MySqlCommand command = new MySqlCommand())
            {
                string sql;
                bool isEdit = true;
                if (templstjpopening.Count <= 0)
                {
                    isEdit = false;
                    sql = @"INSERT INTO jpopening(
JOBTITLE,
COMPANYNAME,
CITYNAME,
COUNTRYNAME,
DEPARTMENT,
SHORTDESC,
JOBTYPE,
SALARYRANGE,
MINIMUMEDUCATION,
MINIMUMEXPERIENCE,
GENDER,
JOBSTATUS,
ALLOWLONGCV,
JOBPOSTDATE,
MAXIMUMEDUCATION,
MAXIMUMEXPERIENCE,
CURRENCY
)
VALUES(
@JOBTITLE,
@COMPANYNAME,
@CITYNAME,
@COUNTRYNAME,
@DEPARTMENT,
@SHORTDESC,
@JOBTYPE,
@SALARYRANGE,
@MINIMUMEDUCATION,
@MINIMUMEXPERIENCE,
@GENDER,
@JOBSTATUS,
@ALLOWLONGCV,
@JOBPOSTDATE,
@MAXIMUMEDUCATION,
@MAXIMUMEXPERIENCE,
@CURRENCY
)";
                }
                else
                {
                    sql = @"Update jpopening set
OPENINGID=@OPENINGID,
JOBTITLE=@JOBTITLE,
COMPANYNAME=@COMPANYNAME,
CITYNAME=@CITYNAME,
COUNTRYNAME=@COUNTRYNAME,
DEPARTMENT=@DEPARTMENT,
SHORTDESC=@SHORTDESC,
JOBTYPE=@JOBTYPE,
SALARYRANGE=@SALARYRANGE,
MINIMUMEDUCATION=@MINIMUMEDUCATION,
MINIMUMEXPERIENCE=@MINIMUMEXPERIENCE,
GENDER=@GENDER,
JOBSTATUS=@JOBSTATUS,
ALLOWLONGCV=@ALLOWLONGCV,
JOBPOSTDATE=@JOBPOSTDATE,
MAXIMUMEDUCATION=@MAXIMUMEDUCATION,
MAXIMUMEXPERIENCE=@MAXIMUMEXPERIENCE,
CURRENCY=@CURRENCY
Where OPENINGID=@OPENINGID";
                }

                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                if (isEdit)
                {
                    command.Parameters.AddWithValue("@OPENINGID", objjpopening.OPENINGID);
                }

                command.Parameters.AddWithValue("@JOBTITLE", objjpopening.JOBTITLE);
                command.Parameters.AddWithValue("@COMPANYNAME", objjpopening.COMPANYNAME);
                command.Parameters.AddWithValue("@CITYNAME", objjpopening.CITYNAME);
                command.Parameters.AddWithValue("@COUNTRYNAME", objjpopening.COUNTRYNAME);
                command.Parameters.AddWithValue("@DEPARTMENT", objjpopening.DEPARTMENT);
                command.Parameters.AddWithValue("@SHORTDESC", objjpopening.SHORTDESC);
                command.Parameters.AddWithValue("@JOBTYPE", objjpopening.JOBTYPE);
                command.Parameters.AddWithValue("@SALARYRANGE", objjpopening.SALARYRANGE);
                command.Parameters.AddWithValue("@MINIMUMEDUCATION", objjpopening.MINIMUMEDUCATION);
                command.Parameters.AddWithValue("@MINIMUMEXPERIENCE", objjpopening.MINIMUMEXPERIENCE);
                command.Parameters.AddWithValue("@GENDER", objjpopening.GENDER);
                command.Parameters.AddWithValue("@JOBSTATUS", objjpopening.JOBSTATUS);
                command.Parameters.AddWithValue("@ALLOWLONGCV", objjpopening.ALLOWLONGCV);
                command.Parameters.AddWithValue("@JOBPOSTDATE", objjpopening.JOBPOSTDATE);
                    command.Parameters.AddWithValue("@MAXIMUMEDUCATION", objjpopening.MAXIMUMEDUCATION);
                    command.Parameters.AddWithValue("@MAXIMUMEXPERIENCE", objjpopening.MAXIMUMEXPERIENCE);
                    command.Parameters.AddWithValue("CURRENCY", objjpopening.CURRENCY);

                    int affectedRows = command.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    returnMessage = "OK";
                }
                else
                {
                    returnMessage = "Unable to save, Please contact ISD";
                }
            }

            if (isConnArgNull == true)
            {
                connection.Dispose();
            }
        }
        catch (Exception ex)
        {
            Logger._log.Error(ex.Message + "\n" + ex.StackTrace);
        }

        return returnMessage;
    }

    public static string Deletejpopening(string OPENINGID, MySqlConnection conn = null)
    {
        string returnMessage = "";
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            using (MySqlCommand command = new MySqlCommand())
            {
                string sql;
                sql = @"DELETE from jpopening Where OPENINGID = @OPENINGID";
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@OPENINGID", OPENINGID);
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    returnMessage = "OK";
                }
                else
                {
                    returnMessage = "Unable to save, Please contact ISD";
                }
            }

            if (isConnArgNull == true)
            {
                connection.Dispose();
            }
        }
        catch (Exception ex)
        {
            Logger._log.Error(ex.Message + "\n" + ex.StackTrace);
        }

        return returnMessage;
    }

        public static List<jpopening> GetjpopeningApplicationDetail(string profileid, MySqlConnection conn = null)
        {
            jpopening objjpopening = null;
            List<jpopening> lstjpopening = new List<jpopening>();
            try
            {
                bool isConnArgNull = (conn != null) ? false : true;
                MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
                tryOpenConnection(connection);
                string sql = "SELECT P.PROFILEID, O.JOBTITLE, O.COMPANYNAME, O.JOBPOSTDATE, P.APPLIEDDATE FROM jpopeningapplication P INNER JOIN jpopening O ON P.OPENINGID = O.OPENINGID WHERE (P.PROFILEID = "+(profileid)+")";
                //if (!string.IsNullOrEmpty(whereclause))
                //    sql += " where " + whereclause;
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                objjpopening = ReaderDatajpopeningApplicationDetail(reader);
                                lstjpopening.Add(objjpopening);
                            }
                        }
                        else
                        {
                        }
                    }
                }

                if (isConnArgNull == true)
                {
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger._log.Error(ex.Message + "\n" + ex.StackTrace);
            }

            return lstjpopening;
        }

        private static jpopening ReaderDatajpopeningApplicationDetail(MySqlDataReader reader)
        {
            jpopening objjpopening = new jpopening();
            objjpopening.JOBTITLE = Utility.IsValidString(reader["JOBTITLE"]);
            objjpopening.COMPANYNAME = Utility.IsValidString(reader["COMPANYNAME"]);
            objjpopening.JOBPOSTDATE = Utility.IsValidDateTime(reader["JOBPOSTDATE"]);
            objjpopening.APPLIEDDATE = Utility.IsValidDateTime(reader["APPLIEDDATE"]);

            return objjpopening;
        }


    }
}

