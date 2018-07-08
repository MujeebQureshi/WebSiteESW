using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;

namespace WebsiteBackEnd.Models
{
	public class jpOPENING
	{
		public int openingID { get; set; }
		public string openingType { get; set; }
		public int cvID { get; set; }
	}
	public class jpOPENINGManager : BaseManager
{
    public static List<jpOPENING> GetjpOPENING(string whereclause, MySqlConnection conn = null)
    {
        jpOPENING objjpOPENING = null;
        List<jpOPENING> lstjpOPENING = new List<jpOPENING>();
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
                            objjpOPENING = ReaderDatajpOPENING(reader);
                            lstjpOPENING.Add(objjpOPENING);
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

        return lstjpOPENING;
    }

    private static jpOPENING ReaderDatajpOPENING(MySqlDataReader reader)
    {
        jpOPENING objjpOPENING = new jpOPENING();
        objjpOPENING.openingID = Utility.IsValidInt(reader["openingID"]);
        objjpOPENING.openingType = Utility.IsValidString(reader["openingType"]);
        objjpOPENING.cvID = Utility.IsValidInt(reader["cvID"]);
        return objjpOPENING;
    }

    public static string SavejpOPENING(jpOPENING objjpOPENING, MySqlConnection conn = null)
    {
        string returnMessage = "";
        string sopeningID = "";
        sopeningID = objjpOPENING.openingID.ToString();
        var templstjpOPENING = GetjpOPENING("openingID = '" + sopeningID + "'", conn);
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            using (MySqlCommand command = new MySqlCommand())
            {
                string sql;
                bool isEdit = true;
                if (templstjpOPENING.Count <= 0)
                {
                    isEdit = false;
                    sql = @"INSERT INTO jpOPENING(
openingType,
cvID
)
VALUES(
@openingType,
@cvID
)";
                }
                else
                {
                    sql = @"Update jpOPENING set
openingID=@openingID,
openingType=@openingType,
cvID=@cvID

Where openingID=@openingID";
                }

                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                if (isEdit)
                {
                    command.Parameters.AddWithValue("@openingID", objjpOPENING.openingID);
                }

                command.Parameters.AddWithValue("@openingType", objjpOPENING.openingType);
                command.Parameters.AddWithValue("@cvID", objjpOPENING.cvID);
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

    public static string DeletejpOPENING(string openingID, MySqlConnection conn = null)
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
                sql = @"DELETE from jpOPENING Where openingID = @openingID";
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@openingID", openingID);
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

