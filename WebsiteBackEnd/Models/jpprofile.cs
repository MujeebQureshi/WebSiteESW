using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace WebsiteBackEnd.Models
{   
    public class jsonProfileWrapper
    {
        public jpprofile basics { get; set; }
        public List<jpexperience> work { get; set; }
        public List<jpqualification> education { get; set; }

    }
	public class jpprofile
	{
        [JsonIgnore]
        public int PROFILEID { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string NAME { get; set; }
        [JsonIgnore]
        public string ADDRESS { get; set; }
        [JsonIgnore]
        public DateTime? DOB { get; set; }
        [JsonIgnore]
        public string GENDER { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public string CONTACT { get; set; }
        [JsonIgnore]
        public string PROFILEBIO { get; set; }
        [JsonIgnore]
        public int USERID { get; set; }
        [JsonIgnore]
        public string CURRENTSALARY { get; set; }
        [JsonIgnore]
        public string LONGCV { get; set; }
        //other vars
        [JsonIgnore]
        public List<jpexperience> lstjpexperience { get; set; }
        [JsonIgnore]
        public List<jpqualification> lstjpqualification { get; set; }
        [JsonIgnore]
        public string expectedSalary { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
	public class jpprofileManager : BaseManager
{
    public static List<jpprofile> Getjpprofile(string whereclause, MySqlConnection conn = null)
    {
        jpprofile objjpprofile = null;
        List<jpprofile> lstjpprofile = new List<jpprofile>();
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            string sql = "select * from jpprofile ";
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
                            objjpprofile = ReaderDatajpprofile(reader);
                            lstjpprofile.Add(objjpprofile);
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
        }

        return lstjpprofile;
    }

    private static jpprofile ReaderDatajpprofile(MySqlDataReader reader)
    {
        jpprofile objjpprofile = new jpprofile();
        objjpprofile.PROFILEID = Utility.IsValidInt(reader["PROFILEID"]);
        objjpprofile.NAME = Utility.IsValidString(reader["NAME"]);
        objjpprofile.ADDRESS = Utility.IsValidString(reader["ADDRESS"]);
        objjpprofile.DOB = Utility.IsValidDateTime(reader["DOB"]);
        objjpprofile.GENDER = Utility.IsValidString(reader["GENDER"]);
        objjpprofile.CONTACT = Utility.IsValidString(reader["CONTACT"]);
        objjpprofile.PROFILEBIO = Utility.IsValidString(reader["PROFILEBIO"]);
        objjpprofile.USERID = Utility.IsValidInt(reader["USERID"]);
        objjpprofile.CURRENTSALARY = Utility.IsValidString(reader["CURRENTSALARY"]);
        objjpprofile.LONGCV = Utility.IsValidString(reader["LONGCV"]);
            
        return objjpprofile;
    }

    public static string Savejpprofile(jpprofile objjpprofile, MySqlConnection conn = null, MySqlTransaction trans = null)
    {
        string returnMessage = "";
        string sPROFILEID = "";
        sPROFILEID = objjpprofile.PROFILEID.ToString();
        var templstjpprofile = Getjpprofile("PROFILEID = '" + sPROFILEID + "'", conn);
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            using (MySqlCommand command = new MySqlCommand())
            {
                string sql;
                bool isEdit = true;
                if (templstjpprofile.Count <= 0)
                {
                    isEdit = false;
                    sql = @"INSERT INTO jpprofile(
NAME,
ADDRESS,
DOB,
GENDER,
CONTACT,
PROFILEBIO,
USERID,
CURRENTSALARY,
LONGCV
)
VALUES(
@NAME,
@ADDRESS,
@DOB,
@GENDER,
@CONTACT,
@PROFILEBIO,
@USERID,
@CURRENTSALARY,
@LONGCV
)";
                }
                else
                {
                    sql = @"Update jpprofile set
PROFILEID=@PROFILEID,
NAME=@NAME,
ADDRESS=@ADDRESS,
DOB=@DOB,
GENDER=@GENDER,
CONTACT=@CONTACT,
PROFILEBIO=@PROFILEBIO,
USERID=@USERID,
CURRENTSALARY=@CURRENTSALARY,
LONGCV=@LONGCV

Where PROFILEID=@PROFILEID";
                }
                if (trans != null)
                {
                    command.Transaction = trans;
                }
                command.Connection = connection;

                    //command.CommandText = "SET GLOBAL max_allowed_packet=32*1024*1024;";
                    //command.ExecuteNonQuery();

                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                if (isEdit)
                {
                    command.Parameters.AddWithValue("@PROFILEID", objjpprofile.PROFILEID);
                }

                command.Parameters.AddWithValue("@NAME", objjpprofile.NAME);
                command.Parameters.AddWithValue("@ADDRESS", objjpprofile.ADDRESS);
                command.Parameters.AddWithValue("@DOB", objjpprofile.DOB);
                command.Parameters.AddWithValue("@GENDER", objjpprofile.GENDER);
                command.Parameters.AddWithValue("@CONTACT", objjpprofile.CONTACT);
                command.Parameters.AddWithValue("@PROFILEBIO", objjpprofile.PROFILEBIO);
                command.Parameters.AddWithValue("@USERID", objjpprofile.USERID);
                command.Parameters.AddWithValue("@CURRENTSALARY", objjpprofile.CURRENTSALARY);
                command.Parameters.AddWithValue("@LONGCV", objjpprofile.LONGCV);
                    
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    returnMessage = "OK";
                }
                else
                {
                    returnMessage = Constants.MSG_ERR_DBSAVE.Text;
                }
            }

            if (isConnArgNull == true)
            {
                connection.Dispose();
            }
        }
        catch (Exception ex)
        {
        }

        return returnMessage;
    }

    public static string Deletejpprofile(string PROFILEID, MySqlConnection conn = null)
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
                sql = @"DELETE from jpprofile Where PROFILEID = @PROFILEID";
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@PROFILEID", PROFILEID);
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    returnMessage = "OK";
                }
                else
                {
                    returnMessage = Shared.Constants.MSG_ERR_DBSAVE.Text;
                }
            }

            if (isConnArgNull == true)
            {
                connection.Dispose();
            }
        }
        catch (Exception ex)
        {
        }

        return returnMessage;
    }
}}

