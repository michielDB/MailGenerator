using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace MailGenerator.Helper
{
    public static class Logging
    {
        private static string _exUrl;
        static SqlConnection _con;

        private static void OpenConnection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnMailGenerator"].ToString();
            _con = new SqlConnection(constr);
            _con.Open();
        }

        public static void LogException(Exception exdb)
        {
            OpenConnection();
            _exUrl = HttpContext.Current.Request.Url.ToString();
            SqlCommand com =
                new SqlCommand("spLogging", _con) {CommandType = CommandType.StoredProcedure};
            com.Parameters.AddWithValue("@ExceptionMsg", exdb.Message);
            com.Parameters.AddWithValue("@ExceptionType", exdb.GetType().Name);
            com.Parameters.AddWithValue("@ExceptionURL", _exUrl);
            com.Parameters.AddWithValue("@ExceptionSource", exdb.StackTrace);
            com.ExecuteNonQuery();
        }

        public static void LogMessage(string message)
        {
            OpenConnection();
            _exUrl = HttpContext.Current.Request.Url.ToString();
            SqlCommand com =
                new SqlCommand("spLogging", _con) { CommandType = CommandType.StoredProcedure };
            com.Parameters.AddWithValue("@ExceptionMsg", message);
            com.Parameters.AddWithValue("@ExceptionURL", _exUrl);
            com.ExecuteNonQuery();
        }
    }
}