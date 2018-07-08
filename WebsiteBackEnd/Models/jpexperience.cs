using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;

namespace WebsiteBackEnd.Models
{
	public class jpexperience
	{
		public int experienceID { get; set; }
		public int cvID { get; set; }
		public string organizationName { get; set; }
		public DateTime? startingYear { get; set; }
		public DateTime? endingYear { get; set; }
		public string designation { get; set; }
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
        }

        return lstjpexperience;
    }

    private static jpexperience ReaderDatajpexperience(MySqlDataReader reader)
    {
        jpexperience objjpexperience = new jpexperience();
        objjpexperience.experienceID = Utility.IsValidInt(reader["experienceID"]);
        objjpexperience.cvID = Utility.IsValidInt(reader["cvID"]);
        objjpexperience.organizationName = Utility.IsValidString(reader["organizationName"]);
        objjpexperience.startingYear = Utility.IsValidDateTime(reader["startingYear"]);
        objjpexperience.endingYear = Utility.IsValidDateTime(reader["endingYear"]);
        objjpexperience.designation = Utility.IsValidString(reader["designation"]);
        return objjpexperience;
    }

    public static string Savejpexperience(jpexperience objjpexperience, MySqlConnection conn = null)
    {
        string returnMessage = "";
        string sexperienceID = "";
        sexperienceID = objjpexperience.experienceID.ToString();
        var templstjpexperience = Getjpexperience("experienceID = '" + sexperienceID + "'", conn);
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
cvID,
organizationName,
startingYear,
endingYear,
designation
)
VALUES(
@cvID,
@organizationName,
@startingYear,
@endingYear,
@designation
)";
                }
                else
                {
                    sql = @"Update jpexperience set
experienceID=@experienceID,
cvID=@cvID,
organizationName=@organizationName,
startingYear=@startingYear,
endingYear=@endingYear,
designation=@designation

Where experienceID=@experienceID";
                }

                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                if (isEdit)
                {
                    command.Parameters.AddWithValue("@experienceID", objjpexperience.experienceID);
                }

                command.Parameters.AddWithValue("@cvID", objjpexperience.cvID);
                command.Parameters.AddWithValue("@organizationName", objjpexperience.organizationName);
                command.Parameters.AddWithValue("@startingYear", objjpexperience.startingYear);
                command.Parameters.AddWithValue("@endingYear", objjpexperience.endingYear);
                command.Parameters.AddWithValue("@designation", objjpexperience.designation);
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

    public static string Deletejpexperience(string experienceID, MySqlConnection conn = null)
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
                sql = @"DELETE from jpexperience Where experienceID = @experienceID";
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@experienceID", experienceID);
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

