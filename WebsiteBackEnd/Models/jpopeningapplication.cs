using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;

namespace WebsiteBackEnd.Models
{
    public class jpopeningapplication
    {
        public int OPENINGID { get; set; }
        public int PROFILEID { get; set; }
        public string EXPECTEDSALARY { get; set; }
        public DateTime? APPLIEDDATE { get; set; }


        //EXTRA VARIABLE
        //public string JOBTITLE {get;set;}
        //public string COMPANYNAME { get; set; }
        //public DateTime? JOBPOSTDATE { get; set; }
       
    }

	public class jpopeningapplicationManager : BaseManager
{
        //public static List<jpopeningapplication> GetJpopeningapplicationsmultiple(string whereclause, MySqlConnection conn = null)
        //{
        //    jpopeningapplication objjpopeningapplication = null;
        //    List<jpopeningapplication> lstjpopeningapplication = new List<jpopeningapplication>();
        //    try
        //    {
        //        bool isConnArgNull = (conn != null) ? false : true;
        //        MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
        //        tryOpenConnection(connection);
        //        string sql = "SELECT  O.JOBTITLE, O.COMPANYNAME, O.JOBPOSTDATE, P.APPLIEDDATE FROM jpopeningapplication P INNER JOIN jpopening O ON P.OPENINGID = O.OPENINGID";
        //        if (!string.IsNullOrEmpty(whereclause))
        //            sql += " where " + whereclause;
        //        using (MySqlCommand command = new MySqlCommand())
        //        {
        //            command.Connection = connection;
        //            command.CommandText = sql;
        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.HasRows)
        //                {
        //                    while (reader.Read())
        //                    {
        //                        objjpopeningapplication = ReaderDatajpopeningapplicationmultiple(reader);
        //                        lstjpopeningapplication.Add(objjpopeningapplication);
        //                    }
        //                }
        //                else
        //                {
        //                }
        //            }
        //        }

        //        if (isConnArgNull == true)
        //        {
        //            connection.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return lstjpopeningapplication;
            

        //}
        public static List<jpopeningapplication> Getjpopeningapplication(string whereclause, MySqlConnection conn = null)
    {
        jpopeningapplication objjpopeningapplication = null;
        List<jpopeningapplication> lstjpopeningapplication = new List<jpopeningapplication>();
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            string sql = "select * from jpopeningapplication ";
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
                            objjpopeningapplication = ReaderDatajpopeningapplication(reader);
                            lstjpopeningapplication.Add(objjpopeningapplication);
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

        return lstjpopeningapplication;
    }

    private static jpopeningapplication ReaderDatajpopeningapplication(MySqlDataReader reader)
    {
        jpopeningapplication objjpopeningapplication = new jpopeningapplication();
        objjpopeningapplication.OPENINGID = Utility.IsValidInt(reader["OPENINGID"]);
        objjpopeningapplication.PROFILEID = Utility.IsValidInt(reader["PROFILEID"]);
        objjpopeningapplication.EXPECTEDSALARY = Utility.IsValidString(reader["EXPECTEDSALARY"]);
        objjpopeningapplication.APPLIEDDATE = Utility.IsValidDateTime(reader["APPLIEDDATE"]);
        return objjpopeningapplication;
    }
        //private static jpopeningapplication ReaderDatajpopeningapplicationmultiple(MySqlDataReader reader)
        //{
        //    jpopeningapplication objjpopeningapplication = new jpopeningapplication();
        //  //  objjpopeningapplication.OPENINGID = Utility.IsValidInt(reader["OPENINGID"]);
        //   // objjpopeningapplication.PROFILEID = Utility.IsValidInt(reader["PROFILEID"]);
        //    //objjpopeningapplication.EXPECTEDSALARY = Utility.IsValidString(reader["EXPECTEDSALARY"]);
        //    objjpopeningapplication.APPLIEDDATE = Utility.IsValidDateTime(reader["APPLIEDDATE"]);
        //    objjpopeningapplication.COMPANYNAME = Utility.IsValidString(reader["COMPANYNAME"]);
        //    objjpopeningapplication.JOBTITLE = Utility.IsValidString(reader["JOBTITLE"]);
        //    objjpopeningapplication.JOBPOSTDATE = Utility.IsValidDateTime(reader["JOBPOSTDATE"]);
        //    return objjpopeningapplication;
            
        //}

        public static string Savejpopeningapplication(jpopeningapplication objjpopeningapplication, MySqlConnection conn = null)
    {
        string returnMessage = "";
        //string sOPENINGID = "";
        //sOPENINGID = objjpopeningapplication.OPENINGID.ToString();
        //var templstjpopeningapplication = Getjpopeningapplication("OPENINGID = '" + sOPENINGID + "'", conn);
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            using (MySqlCommand command = new MySqlCommand())
            {
                string sql;
                bool isEdit = true;
                if (true)
                {
                    isEdit = false;
                    sql = @"INSERT INTO jpopeningapplication(
OPENINGID,
PROFILEID,
EXPECTEDSALARY,
APPLIEDDATE
)
VALUES(
@OPENINGID,
@PROFILEID,
@EXPECTEDSALARY,
@APPLIEDDATE
)";
                }
                else
                {
                    sql = @"Update jpopeningapplication set
OPENINGID=@OPENINGID,
PROFILEID=@PROFILEID,
EXPECTEDSALARY=@EXPECTEDSALARY,
APPLIEDDATE=@APPLIEDDATE

Where OPENINGID=@OPENINGID";
                }

                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                if (isEdit)
                {
                    command.Parameters.AddWithValue("@OPENINGID", objjpopeningapplication.OPENINGID);
                }

                command.Parameters.AddWithValue("@OPENINGID", objjpopeningapplication.OPENINGID);
                command.Parameters.AddWithValue("@PROFILEID", objjpopeningapplication.PROFILEID);
                command.Parameters.AddWithValue("@EXPECTEDSALARY", objjpopeningapplication.EXPECTEDSALARY);
                command.Parameters.AddWithValue("@APPLIEDDATE", objjpopeningapplication.APPLIEDDATE);
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

    public static string Deletejpopeningapplication(string OPENINGID, MySqlConnection conn = null)
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
                sql = @"DELETE from jpopeningapplication Where OPENINGID = @OPENINGID";
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
}}

