using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;

namespace WebsiteBackEnd.Models
{
	public class jpCV
	{
		public int cvID { get; set; }
		public string name { get; set; }
		public string address { get; set; }
		public int age { get; set; }
		public string gender { get; set; }
		public string email { get; set; }
		public string contact { get; set; }
		public string extraCurricularActivites { get; set; }
		public int userID { get; set; }
		public string currentSalary { get; set; }
	}
	public class jpCVManager : BaseManager
{
    public static List<jpCV> GetjpCV(string whereclause, MySqlConnection conn = null)
    {
        jpCV objjpCV = null;
        List<jpCV> lstjpCV = new List<jpCV>();
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
                            objjpCV = ReaderDatajpCV(reader);
                            lstjpCV.Add(objjpCV);
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

        return lstjpCV;
    }

    private static jpCV ReaderDatajpCV(MySqlDataReader reader)
    {
        jpCV objjpCV = new jpCV();
        objjpCV.cvID = Utility.IsValidInt(reader["cvID"]);
        objjpCV.name = Utility.IsValidString(reader["name"]);
        objjpCV.address = Utility.IsValidString(reader["address"]);
        objjpCV.age = Utility.IsValidInt(reader["age"]);
        objjpCV.gender = Utility.IsValidString(reader["gender"]);
        objjpCV.email = Utility.IsValidString(reader["email"]);
        objjpCV.contact = Utility.IsValidString(reader["contact"]);
        objjpCV.extraCurricularActivites = Utility.IsValidString(reader["extraCurricularActivites"]);
        objjpCV.userID = Utility.IsValidInt(reader["userID"]);
        objjpCV.currentSalary = Utility.IsValidString(reader["currentSalary"]);
        return objjpCV;
    }

    public static string SavejpCV(jpCV objjpCV, MySqlConnection conn = null)
    {
        string returnMessage = "";
        string scvID = "";
        scvID = objjpCV.cvID.ToString();
        var templstjpCV = GetjpCV("cvID = '" + scvID + "'", conn);
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            using (MySqlCommand command = new MySqlCommand())
            {
                string sql;
                bool isEdit = true;
                if (templstjpCV.Count <= 0)
                {
                    isEdit = false;
                    sql = @"INSERT INTO jpCV(
name,
address,
age,
gender,
email,
contact,
extraCurricularActivites,
userID,
currentSalary
)
VALUES(
@name,
@address,
@age,
@gender,
@email,
@contact,
@extraCurricularActivites,
@userID,
@currentSalary
)";
                }
                else
                {
                    sql = @"Update jpCV set
cvID=@cvID,
name=@name,
address=@address,
age=@age,
gender=@gender,
email=@email,
contact=@contact,
extraCurricularActivites=@extraCurricularActivites,
userID=@userID,
currentSalary=@currentSalary

Where cvID=@cvID";
                }

                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                if (isEdit)
                {
                    command.Parameters.AddWithValue("@cvID", objjpCV.cvID);
                }

                command.Parameters.AddWithValue("@name", objjpCV.name);
                command.Parameters.AddWithValue("@address", objjpCV.address);
                command.Parameters.AddWithValue("@age", objjpCV.age);
                command.Parameters.AddWithValue("@gender", objjpCV.gender);
                command.Parameters.AddWithValue("@email", objjpCV.email);
                command.Parameters.AddWithValue("@contact", objjpCV.contact);
                command.Parameters.AddWithValue("@extraCurricularActivites", objjpCV.extraCurricularActivites);
                command.Parameters.AddWithValue("@userID", objjpCV.userID);
                command.Parameters.AddWithValue("@currentSalary", objjpCV.currentSalary);
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

    public static string DeletejpCV(string cvID, MySqlConnection conn = null)
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
                sql = @"DELETE from jpCV Where cvID = @cvID";
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@cvID", cvID);
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

