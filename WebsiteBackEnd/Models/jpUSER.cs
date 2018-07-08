using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;

namespace WebsiteBackEnd.Models
{
	public class jpUSER
	{
		public int userID { get; set; }
		public string email { get; set; }
		public string password { get; set; }
		public string username { get; set; }
	}
	public class jpUSERManager : BaseManager
    {
        public static List<jpUSER> GetjpUSER(string whereclause, MySqlConnection conn = null)
        {
            jpUSER objjpUSER = null;
            List<jpUSER> lstjpUSER = new List<jpUSER>();
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
                                objjpUSER = ReaderDatajpUSER(reader);
                                lstjpUSER.Add(objjpUSER);
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

            return lstjpUSER;
        }

        private static jpUSER ReaderDatajpUSER(MySqlDataReader reader)
        {
            jpUSER objjpUSER = new jpUSER();
            objjpUSER.userID = Utility.IsValidInt(reader["userID"]);
            objjpUSER.email = Utility.IsValidString(reader["email"]);
            objjpUSER.password = Utility.IsValidString(reader["password"]);
            objjpUSER.username = Utility.IsValidString(reader["username"]);
            return objjpUSER;
        }

        public static string SavejpUSER(jpUSER objjpUSER, MySqlConnection conn = null)
        {
            string returnMessage = "";
            string suserID = "";
            suserID = objjpUSER.userID.ToString();
            var templstjpUSER = GetjpUSER("userID = '" + suserID + "'", conn);
            try
            {
                bool isConnArgNull = (conn != null) ? false : true;
                MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
                tryOpenConnection(connection);
                using (MySqlCommand command = new MySqlCommand())
                {
                    string sql;
                    bool isEdit = true;
                    if (templstjpUSER.Count <= 0)
                    {
                        isEdit = false;
                        sql = @"INSERT INTO jpUSER(
    email,
    password,
    username
    )
    VALUES(
    @email,
    @password,
    @username
    )";
                    }
                    else
                    {
                        sql = @"Update jpUSER set
    userID=@userID,
    email=@email,
    password=@password,
    username=@username

    Where userID=@userID";
                    }

                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    if (isEdit)
                    {
                        command.Parameters.AddWithValue("@userID", objjpUSER.userID);
                    }

                    command.Parameters.AddWithValue("@email", objjpUSER.email);
                    command.Parameters.AddWithValue("@password", objjpUSER.password);
                    command.Parameters.AddWithValue("@username", objjpUSER.username);
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

        public static string DeletejpUSER(string userID, MySqlConnection conn = null)
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
                    sql = @"DELETE from jpUSER Where userID = @userID";
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@userID", userID);
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

