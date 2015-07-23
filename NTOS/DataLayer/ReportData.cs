using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ReportDataLayer
{
    public class ReportData
    {
        SqlConnection dbconn = new SqlConnection(DBConnectionLayer.DBConnection.GetDBConnection());
        public DataTable GetRouteReportdetails(int showid, DateTime fromdate, DateTime todate, string city)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spRoute_Report", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Show", showid);
            cmd.Parameters.AddWithValue("@fromdate", fromdate);
            cmd.Parameters.AddWithValue("@todate", todate);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.CommandTimeout = 1600;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { if (dbconn.State == ConnectionState.Open) { dbconn.Close(); } }

        }
        public DataTable GetEngtReportParameters(string type, Nullable<int> showid, Nullable<int> cityid, Nullable<int> venueid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SPGetEngtReportParameter", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@Showid", showid);
            cmd.Parameters.AddWithValue("@CityID", cityid);
            cmd.Parameters.AddWithValue("@VenueID", venueid);
            cmd.CommandTimeout = 0;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { if (dbconn.State == ConnectionState.Open) { dbconn.Close(); } }

        }

        #region Market History Report
        public DataSet GetMarketHistoryRpt_Parameter(string showidlist)
        {

            SqlCommand cmd = new SqlCommand("SPMARKETHISTORYREPORTPARAMETERS", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ENGT_SHOW_ID", showidlist);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                da.Fill(ds);
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
        public DataTable GetMarketHistoryReport(string engtshowidlist, string engtvenueidlist, string engtpresenteridlist, string engtcityidlist, DateTime fromdate, DateTime todate)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spRoute_Report", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ENGT_SHOW_ID", engtshowidlist);
            cmd.Parameters.AddWithValue("@ENGT_VENUE_ID", engtvenueidlist);
            cmd.Parameters.AddWithValue("@ENGT_PRESENTER_ID", engtpresenteridlist);
            cmd.Parameters.AddWithValue("@ENGT_CITY_ID", engtcityidlist);
            cmd.Parameters.AddWithValue("@FROMDATE", fromdate);
            cmd.Parameters.AddWithValue("@TODATE", todate);
            cmd.CommandTimeout = 0;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { if (dbconn.State == ConnectionState.Open) { dbconn.Close(); } }

        }

        #endregion

        #region BER
        public DataTable GetBEReportData(string q)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(q, dbconn);
            cmd.CommandTimeout = 0;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { if (dbconn.State == ConnectionState.Open) { dbconn.Close(); } }

        }
        public DataTable GetBreakevendata(int showid, int cityid, int venuid, DateTime from, DateTime to, decimal Discountcap, int engmtid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spbreakevengetvalues", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Showid", showid);
            cmd.Parameters.AddWithValue("@Cityid", cityid);
            cmd.Parameters.AddWithValue("@Venueid", venuid);
            cmd.Parameters.AddWithValue("@from", from);
            cmd.Parameters.AddWithValue("@to", to);
            cmd.Parameters.AddWithValue("@Discountcap", Discountcap);
            cmd.Parameters.AddWithValue("@engmtid", engmtid);
            cmd.CommandTimeout = 0;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { if (dbconn.State == ConnectionState.Open) { dbconn.Close(); } }

        }
        #endregion

        #region Settlement Report

        public DataSet GetSettlementReport_records(int EngtId,int Week,int WeekCount)
        {
            SqlCommand cmd = new SqlCommand("SPSettlementReport", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ENGTID", EngtId);
            cmd.Parameters.AddWithValue("@WK", Week);
            cmd.Parameters.AddWithValue("@WeekCount", WeekCount);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                da.Fill(ds);
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

        #endregion

        #region ProForma Report
        public DataTable GetProFormaReportData(int showid, int year)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spProFormaReport", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1420;
            cmd.Parameters.AddWithValue("@Showid", showid);
            cmd.Parameters.AddWithValue("@Year", year);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { if (dbconn.State == ConnectionState.Open) { dbconn.Close(); } }

        }
        #endregion
    }
}