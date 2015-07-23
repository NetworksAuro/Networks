using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace VenueDataLayer
{
    public class VenueData
    {
        SqlConnection dbconn = new SqlConnection(DBConnectionLayer.DBConnection.GetDBConnection());
        public List<string> SearchVenuename(string VenueName,string activeflag)
        {
            DataTable dt = new DataTable();
            List<string> show = new List<string>();
            SqlCommand cmd = new SqlCommand("spVenueNameSearch", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@VENUENAME", VenueName);
            cmd.Parameters.AddWithValue("@ActiveFlag", activeflag);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    show.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["Venue_name"].ToString(), dt.Rows[i]["Venue_id"].ToString()));
                }
                return show;
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
        public Int32 Venuedata_Insert(Nullable<Int32> metrocityid, string venuename, string venueaddress1, string venueaddress2, Nullable<Int32> venuecapacity, string venuedellocation, string venuenotes, Nullable<Int32> venuecityid, string zipcode)
        {
            SqlCommand cmd = new SqlCommand("spVenueInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@METRO_CITY_ID", metrocityid);
            cmd.Parameters.AddWithValue("@VENUE_NAME", venuename);
            cmd.Parameters.AddWithValue("@VENUE_ADDRESS1", venueaddress1);
            cmd.Parameters.AddWithValue("@VENUE_CAPACITY", venuecapacity);
            cmd.Parameters.AddWithValue("@VENUE_DELIVERY_DIRECTIONS", venuedellocation);
            cmd.Parameters.AddWithValue("@VENUE_NOTES", venuenotes);
            cmd.Parameters.AddWithValue("@VENUE_CITY_ID", venuecityid);
            cmd.Parameters.AddWithValue("@VENUE_ZIP", zipcode);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlParameter pvNewId = new SqlParameter();
                pvNewId.ParameterName = "@NewId";
                pvNewId.DbType = DbType.Int32;
                pvNewId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvNewId);
                int rows;
                rows = cmd.ExecuteNonQuery();
                Int32 newid = 0;
                newid = Convert.ToInt32(cmd.Parameters["@NewId"].Value);
                return newid;
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
        public Int32 Venuedata_Update(Int32 venueid, Nullable<Int32> metrocityid, string venuename, string venueaddress1, string venueaddress2, Nullable<Int32> venuecapacity, string venuedellocation, string venuenotes, Nullable<Int32> venuecityid, string zipcode)
        {
            SqlCommand cmd = new SqlCommand("spVenueUpdate", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@VENUE_ID", venueid);
            cmd.Parameters.AddWithValue("@METRO_CITY_ID", metrocityid);
            cmd.Parameters.AddWithValue("@VENUE_NAME", venuename);
            cmd.Parameters.AddWithValue("@VENUE_ADDRESS1", venueaddress1);
            cmd.Parameters.AddWithValue("@VENUE_CAPACITY", venuecapacity);
            cmd.Parameters.AddWithValue("@VENUE_DELIVERY_DIRECTIONS", venuedellocation);
            cmd.Parameters.AddWithValue("@VENUE_NOTES", venuenotes);
            cmd.Parameters.AddWithValue("@VENUE_CITY_ID", venuecityid);
            cmd.Parameters.AddWithValue("@VENUE_ZIP", zipcode);
            SqlParameter ExistID = new SqlParameter("@ExistID", SqlDbType.Int);
            ExistID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ExistID);
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
            return Convert.ToInt32(ExistID.Value);
        }
        public void VenueContactperson_Insert(Int32 venueid, Int32 personalid)
        {
            SqlCommand cmd = new SqlCommand("spVenuePersonalInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@VENUEID", venueid);
            cmd.Parameters.AddWithValue("@PERSONALID", personalid);
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
        public DataTable GetVenuePersonalDetails(Int32 Venueid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetVenuePersonalDetails", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@VenueID", Venueid);
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
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

        }
        public DataTable GetVenueDetails(Int32 Venueid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetVenueDetails", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@VenueID", Venueid);
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
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

        }
        public string Venue_Delete(int VenueID,string delflag)
        {
            SqlCommand cmd = new SqlCommand("spVenueDelete", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@VenueID", VenueID);
            cmd.Parameters.AddWithValue("@DelFlag", delflag);
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
    }
}