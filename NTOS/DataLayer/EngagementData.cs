using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EngagementDataLayer
{
    public class EngagementData
    {
        SqlConnection dbconn = new SqlConnection(DBConnectionLayer.DBConnection.GetDBConnection());

        public DataTable GetEngagementShows(string status)
        {
            SqlCommand cmd = new SqlCommand("spGetEngagementShows", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ENGT_ACTIVE_FLAG", status);
            DataTable dt = new DataTable();

            try
            {
                dbconn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

            return dt;
        }
        public DataSet GetSettlementReport(int engtid)
        {
            

            SqlCommand cmd = new SqlCommand("spEngagementSettlementReport", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EngtID", engtid);

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
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

        public DataTable GetEngagementCities(int showid, string activestatus)
        {
            SqlCommand cmd = new SqlCommand("spGetEngagementCities", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ENGT_SHOW_ID", showid);
            cmd.Parameters.AddWithValue("@ActiveStatus", activestatus);
            DataTable dt = new DataTable();

            try
            {
                dbconn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

            return dt;
        }
        public DataTable GetEngagementDates(int showid, int cityid, string activestatus)
        {
            SqlCommand cmd = new SqlCommand("spGetEngagementDates", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ENGT_SHOW_ID", showid);
            cmd.Parameters.AddWithValue("@ENGT_CITY_ID", cityid);
            cmd.Parameters.AddWithValue("@ActiveStatus", activestatus);
            DataTable dt = new DataTable();

            try
            {
                dbconn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

            return dt;
        }
        public DataTable GetEngagementDetailsById(int engagementid)
        {
            Int32 UserID = (string.IsNullOrEmpty(HttpContext.Current.Session["userid"].ToString()) == false) ? Convert.ToInt32(HttpContext.Current.Session["userid"]) : 0;
            SqlCommand cmd = new SqlCommand("spGetEngagementDetailsById", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ENGT_ID", engagementid);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            DataTable dt = new DataTable();

            try
            {
                dbconn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

            return dt;
        }
        public DataTable GetEngagementDetails(int showid, int cityid, DateTime createdate)
        {
            Int32 UserID = (string.IsNullOrEmpty(HttpContext.Current.Session["userid"].ToString()) == false) ? Convert.ToInt32(HttpContext.Current.Session["userid"]) : 0;
            SqlCommand cmd = new SqlCommand("spGetEngagementDetails", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ENGT_SHOW_ID", showid);
            cmd.Parameters.AddWithValue("@ENGT_CITY_ID", cityid);
            cmd.Parameters.AddWithValue("@ENGT_CR_DATE", createdate);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            DataTable dt = new DataTable();

            try
            {
                dbconn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

            return dt;
        }
        public int CreateEngagement(int showid, int presenterid, DateTime createdate, Nullable<decimal> mileage, Nullable<int> venueid, Nullable<int> contatct, Nullable<DateTime> revisiondate,
            Nullable<decimal> traveltime, string status, string pricestatus, string subscriptionflag, Nullable<decimal> subscriptionamt, string contract, string offer,
            string repeat, string expenses, string dealmemo, int cityid, Nullable<int> metroid, Nullable<decimal> exchange)
        {
            SqlCommand cmd = new SqlCommand("spInsertEngagement", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            SqlParameter engtid = new SqlParameter("@ENGT_ID", SqlDbType.Int);
            engtid.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(engtid);
            cmd.Parameters.AddWithValue("@ENGT_SHOW_ID", showid);
            cmd.Parameters.AddWithValue("@ENGT_VENUE_ID", venueid);
            cmd.Parameters.AddWithValue("@ENGT_CR_DATE", createdate);
            cmd.Parameters.AddWithValue("@ENGT_MILEAGE", mileage);
            cmd.Parameters.AddWithValue("@ENGT_PRESENTER_ID", presenterid);
            cmd.Parameters.AddWithValue("@engt_personal_id", contatct);
            cmd.Parameters.AddWithValue("@ENGT_REVISION_DT", revisiondate);
            cmd.Parameters.AddWithValue("@ENGT_TRAVEL_TIME", traveltime);
            cmd.Parameters.AddWithValue("@ENGT_STATUS", status);
            cmd.Parameters.AddWithValue("@ENGT_PRICE_SCALE_STATUS", pricestatus);
            cmd.Parameters.AddWithValue("@ENGT_SUBSCRIPTION_FLAG", subscriptionflag);
            cmd.Parameters.AddWithValue("@ENGT_SUBSCRIPTION_AMT", subscriptionamt);
            cmd.Parameters.AddWithValue("@ENGT_CONTRACT", contract);
            cmd.Parameters.AddWithValue("@ENGT_OFFER", offer);
            cmd.Parameters.AddWithValue("@ENGT_REPEAT", repeat);
            cmd.Parameters.AddWithValue("@ENGT_EXPENSES", expenses);
            cmd.Parameters.AddWithValue("@ENGT_DEAL_MEMO", dealmemo);
            cmd.Parameters.AddWithValue("@ENGT_CITY_ID", cityid);
            cmd.Parameters.AddWithValue("@METRO_CITY_ID", metroid);
            cmd.Parameters.AddWithValue("@engt_exchange_rate", exchange);
            int retvalue;

            try
            {
                dbconn.Open();
                retvalue = cmd.ExecuteNonQuery();
                retvalue = Convert.ToInt32(engtid.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

            return retvalue;
        }

        public int UpdateEngagement(int engagementid, int showid, int presenterid, DateTime createdate, Nullable<decimal> mileage, Nullable<int> venueid, Nullable<int> contatct, Nullable<DateTime> revisiondate,
            Nullable<decimal> traveltime, string status, string pricestatus, string subscriptionflag, Nullable<decimal> subscriptionamt, string contract, string offer,
            string repeat, string expenses, string dealmemo, int cityid, Nullable<int> metroid, Nullable<decimal> exchange)
        {
            SqlCommand cmd = new SqlCommand("spUpdateEngagement", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ENGT_ID", engagementid);
            cmd.Parameters.AddWithValue("@ENGT_SHOW_ID", showid);
            cmd.Parameters.AddWithValue("@ENGT_VENUE_ID", venueid);
            cmd.Parameters.AddWithValue("@ENGT_CR_DATE", createdate);
            cmd.Parameters.AddWithValue("@ENGT_MILEAGE", mileage);
            cmd.Parameters.AddWithValue("@ENGT_PRESENTER_ID", presenterid);
            cmd.Parameters.AddWithValue("@engt_personal_id", contatct);
            cmd.Parameters.AddWithValue("@ENGT_REVISION_DT", revisiondate);
            cmd.Parameters.AddWithValue("@ENGT_TRAVEL_TIME", traveltime);
            cmd.Parameters.AddWithValue("@ENGT_STATUS", status);
            cmd.Parameters.AddWithValue("@ENGT_PRICE_SCALE_STATUS", pricestatus);
            cmd.Parameters.AddWithValue("@ENGT_SUBSCRIPTION_FLAG", subscriptionflag);
            cmd.Parameters.AddWithValue("@ENGT_SUBSCRIPTION_AMT", subscriptionamt);
            cmd.Parameters.AddWithValue("@ENGT_CONTRACT", contract);
            cmd.Parameters.AddWithValue("@ENGT_OFFER", offer);
            cmd.Parameters.AddWithValue("@ENGT_REPEAT", repeat);
            cmd.Parameters.AddWithValue("@ENGT_EXPENSES", expenses);
            cmd.Parameters.AddWithValue("@ENGT_DEAL_MEMO", dealmemo);
            cmd.Parameters.AddWithValue("@ENGT_CITY_ID", cityid);
            cmd.Parameters.AddWithValue("@METRO_CITY_ID", metroid);
            cmd.Parameters.AddWithValue("@engt_exchange_rate", exchange);
            int retvalue;

            try
            {
                dbconn.Open();
                retvalue = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

            return retvalue;
        }

        public int DeleteEngagement(int engagementid, string delflag)
        {
            SqlCommand cmd = new SqlCommand("spDeleteEngagement", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ENGT_ID", engagementid);
            cmd.Parameters.AddWithValue("@DelFlag", delflag);

            int retvalue;

            try
            {
                dbconn.Open();
                retvalue = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

            return retvalue;
        }

        public DataTable GetVenueCity(int venueid)
        {
            SqlCommand cmd = new SqlCommand("spGetVenueDetails", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@VENUEID", venueid);
            DataTable dt = new DataTable();

            try
            {
                dbconn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

            return dt;
        }

        public DataTable GetContact()
        {
            SqlCommand cmd = new SqlCommand("SPPERSONALSEARCH", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PERSON_NAME", "");
            cmd.Parameters.AddWithValue("@ACTIVEFLAG", "y");
            DataTable dt = new DataTable();

            try
            {
                dbconn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

            return dt;
        }

        public DateTime GetMinScheduleDate(int engtid)
        {
            SqlCommand cmd = new SqlCommand("SPGETMINSCHEDULEDATE", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SCHEDULE_ENGT_ID", engtid);
            DateTime retvalue;
            try
            {
                dbconn.Open();
                retvalue = Convert.ToDateTime(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

            return retvalue;
        }
    }
}