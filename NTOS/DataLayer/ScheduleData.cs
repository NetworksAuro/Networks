using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ScheuleDataLayer
{
    public class ScheduleData
    {
        SqlConnection dbconn = new SqlConnection(DBConnectionLayer.DBConnection.GetDBConnection());

        public string Engagementschedule_Insert(int schedule_engid, string schedule_type, Nullable<DateTime> schedulde_date, string schedule_days,
            string schedule_stime, string schedule_endtime, string notes)
        {
            SqlCommand cmd = new SqlCommand("spScheduleInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            SqlParameter result = new SqlParameter("@RetMsg", SqlDbType.VarChar, 500);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);
            cmd.Parameters.AddWithValue("@SCHEDULE_ENGT_ID", schedule_engid);
            cmd.Parameters.AddWithValue("@SCHEDULE_TYPE", schedule_type);
            cmd.Parameters.AddWithValue("@SCHEDULE_DATE", schedulde_date);
            cmd.Parameters.AddWithValue("@SCHEDULE_DAY", schedule_days);
            cmd.Parameters.AddWithValue("@SCHEDULE_ST_TIME", schedule_stime);
            cmd.Parameters.AddWithValue("@SCHEDULE_END_TIME", schedule_endtime);
            cmd.Parameters.AddWithValue("@SCHEDULE_NOTES", notes);

            try
            {
                dbconn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
            return Convert.ToString(cmd.Parameters["@RetMsg"].Value);
        }
        
        public DataTable GetScheduleDetails(int engagementid)
        {
            SqlCommand cmd = new SqlCommand("spGetScheduleDetails", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EngagementID", engagementid);

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
        public string Engagementschedule_Delete(int scheduleid)
        {
            SqlCommand cmd = new SqlCommand("spGetScheduleDelete", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ScheduleID", scheduleid);
            SqlParameter result = new SqlParameter("@RetMsg", SqlDbType.VarChar, 500);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);
            try
            {
                dbconn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
            return Convert.ToString(cmd.Parameters["@RetMsg"].Value);
        }
        public string Engagementschedule_Update(int schedule_engid,int schedule_id, string schedule_type, Nullable<DateTime> schedulde_date, string schedule_days,
          string schedule_stime, string schedule_endtime, string notes)
        {
            SqlCommand cmd = new SqlCommand("spScheduleUpdate", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            SqlParameter result = new SqlParameter("@RetMsg", SqlDbType.VarChar, 500);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);
            cmd.Parameters.AddWithValue("@SCHEDULE_ID", schedule_id);
            cmd.Parameters.AddWithValue("@SCHEDULE_ENGT_ID", schedule_engid);
            cmd.Parameters.AddWithValue("@SCHEDULE_TYPE", schedule_type);
            cmd.Parameters.AddWithValue("@SCHEDULE_DATE", schedulde_date);
            cmd.Parameters.AddWithValue("@SCHEDULE_DAY", schedule_days);
            cmd.Parameters.AddWithValue("@SCHEDULE_ST_TIME", schedule_stime);
            cmd.Parameters.AddWithValue("@SCHEDULE_END_TIME", schedule_endtime);
            cmd.Parameters.AddWithValue("@SCHEDULE_NOTES", notes);

            try
            {
                dbconn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
            return Convert.ToString(cmd.Parameters["@RetMsg"].Value);
        }
    }
}