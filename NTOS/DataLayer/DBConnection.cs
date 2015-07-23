using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DBConnectionLayer
{
    public static class DBConnection
    {
        static string servername = System.Configuration.ConfigurationManager.AppSettings["dbservername"].ToString();
        public static string GetDBConnection()
        {
            string constr = "Data Source=" + servername + ";Initial Catalog=ntos_prod_qa;User Id=sa;Password=ai@123";
            return constr;
        }
    }
}