using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;

namespace WebsiteBackEnd.Models
{
    public class jpadmin
    {
        public int ADMINID { get; set; }
        public string PASSWORD { get; set; }
        public string ADMINUSERNAME { get; set; }
        
    }
    public class jpadminManager : BaseManager
    {
        public static List<jpadmin> Getjpadmin(string whereclause, MySqlConnection conn = null)
        {
            jpadmin objjpadmin = null;
            List<jpadmin> lstjpadmin = new List<jpadmin>();
            try
            {
                bool isConnArgNull = (conn != null) ? false : true;
                MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
                tryOpenConnection(connection);
                string sql = "select * from jpadmin";
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
                                objjpadmin = ReaderDatajpadmin(reader);
                                lstjpadmin.Add(objjpadmin);
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

            return lstjpadmin;
        }

        private static jpadmin ReaderDatajpadmin(MySqlDataReader reader)
        {
            jpadmin objjpadmin = new jpadmin();
            objjpadmin.ADMINID = Utility.IsValidInt(reader["ADMINID"]);
            objjpadmin.PASSWORD = Utility.IsValidString(reader["PASSWORD"]);
            objjpadmin.ADMINUSERNAME = Utility.IsValidString(reader["ADMINUSERNAME"]);
            return objjpadmin;
        }

        public static string Savejpadmin(jpadmin objjpadmin, MySqlConnection conn = null, MySqlTransaction trans = null)
        {
            string returnMessage = "";
            string sADMINID = "";
            sADMINID = objjpadmin.ADMINID.ToString();
            var templstjpadmin = Getjpadmin("ADMINID = '" + sADMINID + "'", conn);
            try
            {
                bool isConnArgNull = (conn != null) ? false : true;
                MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
                tryOpenConnection(connection);
                using (MySqlCommand command = new MySqlCommand())
                {
                    string sql;
                    bool isEdit = true;
                    if (templstjpadmin.Count <= 0)
                    {
                        isEdit = false;
                        sql = @"INSERT INTO jpadmin(
PASSWORD,
ADMINUSERNAME
)
VALUES(
@PASSWORD,
@ADMINUSERNAME
)";
                    }
                    else
                    {
                        sql = @"Update jpadmin set
ADMINID=@ADMINID,
PASSWORD=@PASSWORD,
ADMINUSERNAME=@ADMINUSERNAME

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
                        command.Parameters.AddWithValue("@ADMINID", objjpadmin.ADMINID);
                    }

                    command.Parameters.AddWithValue("@PASSWORD", objjpadmin.PASSWORD);
                    command.Parameters.AddWithValue("@ADMINUSERNAME", objjpadmin.ADMINUSERNAME);
                    int affectedRows = command.ExecuteNonQuery();
                    var lastInsertID = command.LastInsertedId;
                    if (affectedRows > 0)
                    {
                        if (!isEdit)
                        {
                            returnMessage = lastInsertID.ToString();
                        }
                        else
                        {
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
            }

            return returnMessage;
        }

        public static string Deletejpadmin(int ADMINID, MySqlConnection conn = null)
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
                    sql = @"DELETE from jpadmin Where ADMINID = @ADMINID";
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@ADMINID", ADMINID);
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
            }

            return returnMessage;
        }
    }
}

