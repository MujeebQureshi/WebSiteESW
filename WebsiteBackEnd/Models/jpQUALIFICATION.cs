using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace WebsiteBackEnd.Models
{
	public class jpqualification
	{
        [JsonIgnore]
        public int QUALIFICATIONID { get; set; }
        [JsonIgnore]
        public int PROFILEID { get; set; }
        [JsonProperty(PropertyName = "area")]
        public string DEGREENAME { get; set; }
        [JsonIgnore]
        public DateTime? PASSINGYEAR { get; set; }
        [JsonProperty(PropertyName = "gpa")]
        public string GRADES { get; set; }
        [JsonProperty(PropertyName = "institution")]
        public string INSTITUTIONNAME { get; set; }

        //other vars:
        [JsonIgnore]
        public string DegreeType { get; set; }
        [JsonIgnore]
        public bool isNewRecord { get; set; }
        public string endDate {
            get { return (PASSINGYEAR != null) ? PASSINGYEAR.Value.ToString(Constants.DATE_MONTH_YEAR_FORMAT) : ""; } set { }
        }
    }
	public class jpqualificationManager : BaseManager
{
    public static List<jpqualification> Getjpqualification(string whereclause, MySqlConnection conn = null)
    {
        jpqualification objjpqualification = null;
        List<jpqualification> lstjpqualification = new List<jpqualification>();
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            string sql = "select * from jpqualification ";
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
                            objjpqualification = ReaderDatajpqualification(reader);
                            lstjpqualification.Add(objjpqualification);
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

        return lstjpqualification;
    }

    private static jpqualification ReaderDatajpqualification(MySqlDataReader reader)
    {
        jpqualification objjpqualification = new jpqualification();
        objjpqualification.QUALIFICATIONID = Utility.IsValidInt(reader["QUALIFICATIONID"]);
        objjpqualification.PROFILEID = Utility.IsValidInt(reader["PROFILEID"]);
        objjpqualification.DEGREENAME = Utility.IsValidString(reader["DEGREENAME"]);
        objjpqualification.PASSINGYEAR = Utility.IsValidDateTime(reader["PASSINGYEAR"]);
        objjpqualification.GRADES = Utility.IsValidString(reader["GRADES"]);
        objjpqualification.INSTITUTIONNAME = Utility.IsValidString(reader["INSTITUTIONNAME"]);
        return objjpqualification;
    }

    public static string Savejpqualification(jpqualification objjpqualification, MySqlConnection conn = null, MySqlTransaction trans = null)
    {
        string returnMessage = "";
        string sQUALIFICATIONID = "";
        sQUALIFICATIONID = objjpqualification.QUALIFICATIONID.ToString();
        var templstjpqualification = Getjpqualification("QUALIFICATIONID = '" + sQUALIFICATIONID + "'", conn);
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            using (MySqlCommand command = new MySqlCommand())
            {
                string sql;
                bool isEdit = true;
                if (templstjpqualification.Count <= 0)
                {
                    isEdit = false;
                    sql = @"INSERT INTO jpqualification(
PROFILEID,
DEGREENAME,
PASSINGYEAR,
GRADES,
INSTITUTIONNAME
)
VALUES(
@PROFILEID,
@DEGREENAME,
@PASSINGYEAR,
@GRADES,
@INSTITUTIONNAME
)";
                }
                else
                {
                    sql = @"Update jpqualification set
QUALIFICATIONID=@QUALIFICATIONID,
PROFILEID=@PROFILEID,
DEGREENAME=@DEGREENAME,
PASSINGYEAR=@PASSINGYEAR,
GRADES=@GRADES,
INSTITUTIONNAME=@INSTITUTIONNAME

Where QUALIFICATIONID=@QUALIFICATIONID";
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
                    command.Parameters.AddWithValue("@QUALIFICATIONID", objjpqualification.QUALIFICATIONID);
                }

                command.Parameters.AddWithValue("@PROFILEID", objjpqualification.PROFILEID);
                command.Parameters.AddWithValue("@DEGREENAME", objjpqualification.DEGREENAME);
                command.Parameters.AddWithValue("@PASSINGYEAR", objjpqualification.PASSINGYEAR);
                command.Parameters.AddWithValue("@GRADES", objjpqualification.GRADES);
                command.Parameters.AddWithValue("@INSTITUTIONNAME", objjpqualification.INSTITUTIONNAME);
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

    public static string Deletejpqualification(int QUALIFICATIONID, MySqlConnection conn = null)
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
                sql = @"DELETE from jpqualification Where QUALIFICATIONID = @QUALIFICATIONID";
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@QUALIFICATIONID", QUALIFICATIONID);
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

