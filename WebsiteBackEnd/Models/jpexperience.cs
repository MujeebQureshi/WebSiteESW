using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace WebsiteBackEnd.Models
{
	public class jpexperience
	{
        [JsonIgnore]
        public int EXPID { get; set; }
        [JsonIgnore]
        public int PROFILEID { get; set; }
        [JsonProperty(PropertyName = "company")]
        public string ORGANIZATIONNAME { get; set; }
        [JsonIgnore]
        public DateTime? STARTINGYEAR { get; set; }
        [JsonIgnore]
        public DateTime? ENDINGYEAR { get; set; }
        [JsonProperty(PropertyName = "position")]
        public string DESIGNATION { get; set; }
        
        //other vars
        [JsonIgnore]
        public string isNewRecord { get; set; }
        public string startDate {
            get{  return (STARTINGYEAR!=null)? STARTINGYEAR.Value.ToString(Constants.DATE_MONTH_YEAR_FORMAT):""; } set { }
        }
        public string endDate {
            get { return (ENDINGYEAR != null) ? ENDINGYEAR.Value.ToString(Constants.DATE_MONTH_YEAR_FORMAT) : ""; } set { }
        }
    }
	public class jpexperienceManager : BaseManager
{
    public static List<jpexperience> Getjpexperience(string whereclause, MySqlConnection conn = null)
    {
        jpexperience objjpexperience = null;
        List<jpexperience> lstjpexperience = new List<jpexperience>();
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            string sql = "select * from jpexperience ";
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
                            objjpexperience = ReaderDatajpexperience(reader);
                            lstjpexperience.Add(objjpexperience);
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

        return lstjpexperience;
    }

    private static jpexperience ReaderDatajpexperience(MySqlDataReader reader)
    {
        jpexperience objjpexperience = new jpexperience();
        objjpexperience.EXPID = Utility.IsValidInt(reader["EXPID"]);
        objjpexperience.PROFILEID = Utility.IsValidInt(reader["PROFILEID"]);
        objjpexperience.ORGANIZATIONNAME = Utility.IsValidString(reader["ORGANIZATIONNAME"]);
        objjpexperience.STARTINGYEAR = Utility.IsValidDateTime(reader["STARTINGYEAR"]);
        objjpexperience.ENDINGYEAR = Utility.IsValidDateTime(reader["ENDINGYEAR"]);
        objjpexperience.DESIGNATION = Utility.IsValidString(reader["DESIGNATION"]);
        return objjpexperience;
    }

    public static string Savejpexperience(jpexperience objjpexperience, MySqlConnection conn = null, MySqlTransaction trans = null)
    {
        string returnMessage = "";
        string sEXPID = "";
        sEXPID = objjpexperience.EXPID.ToString();
        var templstjpexperience = Getjpexperience("EXPID = '" + sEXPID + "'", conn);
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            using (MySqlCommand command = new MySqlCommand())
            {
                string sql;
                bool isEdit = true;
                if (templstjpexperience.Count <= 0)
                {
                    isEdit = false;
                    sql = @"INSERT INTO jpexperience(
PROFILEID,
ORGANIZATIONNAME,
STARTINGYEAR,
ENDINGYEAR,
DESIGNATION
)
VALUES(
@PROFILEID,
@ORGANIZATIONNAME,
@STARTINGYEAR,
@ENDINGYEAR,
@DESIGNATION
)";
                }
                else
                {
                    sql = @"Update jpexperience set
EXPID=@EXPID,
PROFILEID=@PROFILEID,
ORGANIZATIONNAME=@ORGANIZATIONNAME,
STARTINGYEAR=@STARTINGYEAR,
ENDINGYEAR=@ENDINGYEAR,
DESIGNATION=@DESIGNATION

Where EXPID=@EXPID";
                }

                if (trans != null)
                {
                    command.Transaction = trans;
                }
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                if (isEdit)
                {
                    command.Parameters.AddWithValue("@EXPID", objjpexperience.EXPID);
                }

                command.Parameters.AddWithValue("@PROFILEID", objjpexperience.PROFILEID);
                command.Parameters.AddWithValue("@ORGANIZATIONNAME", objjpexperience.ORGANIZATIONNAME);
                command.Parameters.AddWithValue("@STARTINGYEAR", objjpexperience.STARTINGYEAR);
                command.Parameters.AddWithValue("@ENDINGYEAR", objjpexperience.ENDINGYEAR);
                command.Parameters.AddWithValue("@DESIGNATION", objjpexperience.DESIGNATION);
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

    public static string Deletejpexperience(int EXPID, MySqlConnection conn = null)
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
                sql = @"DELETE from jpexperience Where EXPID = @EXPID";
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@EXPID", EXPID);
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
}}

