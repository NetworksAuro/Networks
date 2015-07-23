using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Caching;

namespace DairyDataLayer
{
    public class DairyData
    {
        SqlConnection dbconn = new SqlConnection(DBConnectionLayer.DBConnection.GetDBConnection());
        public DataSet GetDiary(Nullable<DateTime> date, string type)
        {
            
 

           string cachekey = "Diary" +Convert.ToString(date) + type;
            if (HttpContext.Current.Cache[cachekey] != null)
            {
                DataSet cacheds = HttpContext.Current.Cache[cachekey] as DataSet;
                return cacheds;
            }
            else
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("spDiaryDetailsget", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DATE", date);
                cmd.Parameters.AddWithValue("@TYPE", type);
                cmd.CommandTimeout = 60;
                SqlCacheDependency dependency = new SqlCacheDependency(cmd);
                try
                {
                    if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter();
                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    HttpContext.Current.Cache.Insert(cachekey, ds, dependency);
                    return ds;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
                }
            }
        }
        public DataSet GetSeasonDates()
        {

            string cachekey = "SeasonDates";
            if (HttpContext.Current.Cache[cachekey] != null)
            {
                DataSet cacheds = HttpContext.Current.Cache[cachekey] as DataSet;
                return cacheds;
            }
            else
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SPDIARYSEASONDATE", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@DATE", date);
                cmd.CommandTimeout = 0;
                SqlCacheDependency dependency = new SqlCacheDependency(cmd);
                try
                {
                    if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter();
                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    HttpContext.Current.Cache.Insert(cachekey, ds, dependency);
                    return ds;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
                }
            }
        }
    }
}