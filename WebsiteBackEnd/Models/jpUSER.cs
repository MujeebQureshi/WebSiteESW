using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;

namespace WebsiteBackEnd.Models
{
	public class jpuser
	{
		public int USERID { get; set; }
		public string EMAIL { get; set; }
		public string PASSWORD { get; set; }
		public string USERNAME { get; set; }
        public string ACTIVE { get; set; }

        public jpprofile objjpprofile { get; set; }

        //other vars
        public bool isVerifyLinkSent { get; set; }
        public bool isProfileComplete { get; set; }
	}
	public class jpuserManager : BaseManager
{
    public static List<jpuser> Getjpuser(string whereclause, MySqlConnection conn = null)
    {
        jpuser objjpuser = null;
        List<jpuser> lstjpuser = new List<jpuser>();
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            string sql = "select * from jpuser";
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
                            objjpuser = ReaderDatajpuser(reader);
                            lstjpuser.Add(objjpuser);
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

        return lstjpuser;
    }

    private static jpuser ReaderDatajpuser(MySqlDataReader reader)
    {
        jpuser objjpuser = new jpuser();
        objjpuser.USERID = Utility.IsValidInt(reader["USERID"]);
        objjpuser.EMAIL = Utility.IsValidString(reader["EMAIL"]);
        objjpuser.PASSWORD = Utility.IsValidString(reader["PASSWORD"]);
        objjpuser.USERNAME = Utility.IsValidString(reader["USERNAME"]);
        objjpuser.ACTIVE = Utility.IsValidString(reader["ACTIVE"]);
        return objjpuser;
    }

    public static string Savejpuser(jpuser objjpuser, MySqlConnection conn = null, MySqlTransaction trans = null)
    {
        string returnMessage = "";
        string sUSERID = "";
        sUSERID = objjpuser.USERID.ToString();
        var templstjpuser = Getjpuser("USERID = '" + sUSERID + "'", conn);
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            using (MySqlCommand command = new MySqlCommand())
            {
                string sql;
                bool isEdit = true;
                if (templstjpuser.Count <= 0)
                {
                    isEdit = false;
                    sql = @"INSERT INTO jpuser(
EMAIL,
PASSWORD,
USERNAME,
ACTIVE
)
VALUES(
@EMAIL,
@PASSWORD,
@USERNAME,
@ACTIVE
)";
                }
                else
                {
                    sql = @"Update jpuser set
USERID=@USERID,
EMAIL=@EMAIL,
PASSWORD=@PASSWORD,
USERNAME=@USERNAME,
ACTIVE=@ACTIVE

Where USERID=@USERID";
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
                    command.Parameters.AddWithValue("@USERID", objjpuser.USERID);
                }

                command.Parameters.AddWithValue("@EMAIL", objjpuser.EMAIL);
                command.Parameters.AddWithValue("@PASSWORD", objjpuser.PASSWORD);
                command.Parameters.AddWithValue("@USERNAME", objjpuser.USERNAME);
                command.Parameters.AddWithValue("@ACTIVE", objjpuser.ACTIVE);
                int affectedRows = command.ExecuteNonQuery();
                var lastInsertID = command.LastInsertedId;
                if (affectedRows > 0)
                {
                    if (!isEdit)
                    {
                        returnMessage = lastInsertID.ToString();
                    }
                    else {
                        returnMessage = "OK";
                    }
                    
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
            Logger._log.Error(ex.Message + "\n" + ex.StackTrace);
        }

        return returnMessage;
    }

    public static string Deletejpuser(string USERID, MySqlConnection conn = null)
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
                sql = @"DELETE from jpuser Where USERID = @USERID";
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@USERID", USERID);
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

