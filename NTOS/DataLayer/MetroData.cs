using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MetroDataLayer
{
    public class MetroData
    {
        SqlConnection dbconn = new SqlConnection(DBConnectionLayer.DBConnection.GetDBConnection());
        public void Metro_Insert(Int32 metrocityid, Int32 nearbycityid, Nullable<decimal> metrotax, string Type, Int32 countryid, Int32 stateid, string statename, Nullable<int> timezoneid)
        {
            SqlCommand cmd = new SqlCommand("spMetroInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@METRO_CITY_ID", metrocityid);
            cmd.Parameters.AddWithValue("@METRO_NEARBY_CITY_ID", nearbycityid);
            cmd.Parameters.AddWithValue("@METRO_TAX", metrotax);
            cmd.Parameters.AddWithValue("@TimeZoneID", timezoneid);
            cmd.Parameters.AddWithValue("@Type", Type);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
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

        }

        public DataSet GetMetroDetails(Int32 metrocityid)
        {

            SqlCommand cmd = new SqlCommand("spGetMetroDetails", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@METRO_CITY_ID", metrocityid);
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
        public string Metro_Delete(Int32 metroid,string delflag)
        {
            SqlCommand cmd = new SqlCommand("spMetroDelete", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@METROCITY_ID", metroid);
            cmd.Parameters.AddWithValue("@DelFlag", delflag);
            SqlParameter result = new SqlParameter("@RetMsg", SqlDbType.VarChar, 500);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
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
        public string NearbyMetro_Delete(Int32 NearbyMetroid)
        {
            SqlCommand cmd = new SqlCommand("spNearByMetroDelete", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@MetroID", NearbyMetroid);
            SqlParameter result = new SqlParameter("@RetMsg", SqlDbType.VarChar, 500);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
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