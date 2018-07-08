using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;

namespace WebsiteBackEnd.Models
{
	public class jpQUALIFICATION
	{
		public int qualificationID { get; set; }
		public int cvID { get; set; }
		public string degreeName { get; set; }
		public string passingYear { get; set; }
		public string grades { get; set; }
		public string institutionName { get; set; }
	}
	public class jpQUALIFICATIONManager : BaseManager
{
    public static List<jpQUALIFICATION> GetjpQUALIFICATION(string whereclause, MySqlConnection conn = null)
    {
        jpQUALIFICATION objjpQUALIFICATION = null;
        List<jpQUALIFICATION> lstjpQUALIFICATION = new List<jpQUALIFICATION>();
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            string sql = "";
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
                            objjpQUALIFICATION = ReaderDatajpQUALIFICATION(reader);
                            lstjpQUALIFICATION.Add(objjpQUALIFICATION);
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

        return lstjpQUALIFICATION;
    }

    private static jpQUALIFICATION ReaderDatajpQUALIFICATION(MySqlDataReader reader)
    {
        jpQUALIFICATION objjpQUALIFICATION = new jpQUALIFICATION();
        objjpQUALIFICATION.qualificationID = Utility.IsValidInt(reader["qualificationID"]);
        objjpQUALIFICATION.cvID = Utility.IsValidInt(reader["cvID"]);
        objjpQUALIFICATION.degreeName = Utility.IsValidString(reader["degreeName"]);
        objjpQUALIFICATION.passingYear = Utility.IsValidString(reader["passingYear"]);
        objjpQUALIFICATION.grades = Utility.IsValidString(reader["grades"]);
        objjpQUALIFICATION.institutionName = Utility.IsValidString(reader["institutionName"]);
        return objjpQUALIFICATION;
    }

    public static string SavejpQUALIFICATION(jpQUALIFICATION objjpQUALIFICATION, MySqlConnection conn = null)
    {
        string returnMessage = "";
        string squalificationID = "";
        squalificationID = objjpQUALIFICATION.qualificationID.ToString();
        var templstjpQUALIFICATION = GetjpQUALIFICATION("qualificationID = '" + squalificationID + "'", conn);
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            using (MySqlCommand command = new MySqlCommand())
            {
                string sql;
                bool isEdit = true;
                if (templstjpQUALIFICATION.Count <= 0)
                {
                    isEdit = false;
                    sql = @"INSERT INTO jpQUALIFICATION(
cvID,
degreeName,
passingYear,
grades,
institutionName
)
VALUES(
@cvID,
@degreeName,
@passingYear,
@grades,
@institutionName
)";
                }
                else
                {
                    sql = @"Update jpQUALIFICATION set
qualificationID=@qualificationID,
cvID=@cvID,
degreeName=@degreeName,
passingYear=@passingYear,
grades=@grades,
institutionName=@institutionName

Where qualificationID=@qualificationID";
                }

                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                if (isEdit)
                {
                    command.Parameters.AddWithValue("@qualificationID", objjpQUALIFICATION.qualificationID);
                }

                command.Parameters.AddWithValue("@cvID", objjpQUALIFICATION.cvID);
                command.Parameters.AddWithValue("@degreeName", objjpQUALIFICATION.degreeName);
                command.Parameters.AddWithValue("@passingYear", objjpQUALIFICATION.passingYear);
                command.Parameters.AddWithValue("@grades", objjpQUALIFICATION.grades);
                command.Parameters.AddWithValue("@institutionName", objjpQUALIFICATION.institutionName);
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

    public static string DeletejpQUALIFICATION(string qualificationID, MySqlConnection conn = null)
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
                sql = @"DELETE from jpQUALIFICATION Where qualificationID = @qualificationID";
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@qualificationID", qualificationID);
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
}}

