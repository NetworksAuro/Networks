using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PresenterDataLayer
{
    public class PresenterData
    {
        SqlConnection dbconn = new SqlConnection(DBConnectionLayer.DBConnection.GetDBConnection());
        public List<string> searchpersonal(string titleid, string searchtext, string type, string activeflag)
        {
            DataTable dt = new DataTable();
            List<string> show = new List<string>();
            SqlCommand cmd = new SqlCommand("spGetContactPesons", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@TitleID", titleid);
            cmd.Parameters.AddWithValue("@FirstName", searchtext);
            cmd.Parameters.AddWithValue("@TYPE", type);
            cmd.Parameters.AddWithValue("@ActiveFlag", activeflag);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    show.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["PERSONAL_FNAME"].ToString(), dt.Rows[i]["PERSONAL_ID"].ToString()));
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
        public DataTable GetPersonalName(string titleid, string searchtext, string type,string activeflag)
        {
            DataTable dt = new DataTable();
            List<string> show = new List<string>();
            SqlCommand cmd = new SqlCommand("spGetContactPesons", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@TitleID", titleid);
            cmd.Parameters.AddWithValue("@FirstName", searchtext);
            cmd.Parameters.AddWithValue("@TYPE", type);
            cmd.Parameters.AddWithValue("@ActiveFlag", activeflag);
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
        public List<string> SearchPresentername(string presentername, string activeflag)
        {
            DataTable dt = new DataTable();
            List<string> show = new List<string>();
            SqlCommand cmd = new SqlCommand("spPresenterSearch", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PRESENTER_NAME", presentername);
            cmd.Parameters.AddWithValue("@ActiveFlag", activeflag);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    show.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["PRESENTER_NAME"].ToString(), dt.Rows[i]["PRESENTER_ID"].ToString()));
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
        public Int32 PresenterDetails_Insert(string presentername, string presenterstreet, Nullable<Int32> cityid, string notes,string zip)
        {
            SqlCommand cmd = new SqlCommand("spPresenterInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PRESENTER_NAME", presentername);
            cmd.Parameters.AddWithValue("@PRESENTER_STREET", presenterstreet);
            cmd.Parameters.AddWithValue("@PRESENTER_CITY_ID", cityid);
            cmd.Parameters.AddWithValue("@PRESENTER_NOTES", notes);
            cmd.Parameters.AddWithValue("@PRESENTER_ZIP", zip);
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
        public Int32 PresenterDetails_Update(Int32 presenterid, string presentername, string presenterstreet, Nullable<Int32> cityid, string notes,string zip)
        {
            SqlCommand cmd = new SqlCommand("spPresenterUpdate", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PRESENTER_ID", presenterid);
            cmd.Parameters.AddWithValue("@PRESENTER_NAME", presentername);
            cmd.Parameters.AddWithValue("@PRESENTER_STREET", presenterstreet);
            cmd.Parameters.AddWithValue("@PRESENTER_CITY_ID", cityid);
            cmd.Parameters.AddWithValue("@PRESENTER_NOTES", notes);
            cmd.Parameters.AddWithValue("@PRESENTER_ZIP", zip);
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
        public void PresenterContactperson_Insert(Int32 presenterid, Int32 personalid)
        {
            SqlCommand cmd = new SqlCommand("spPresenterPersonalInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PRESENTERID", presenterid);
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
        public DataTable GetPresenterDetails(Int32 PresenterID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetPresenterDetails", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PRESENTER_ID", PresenterID);
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
        public DataTable GetPresenterPersonalDetails(Int32 PresenterID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetPresenterPersonalDetails", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PRESENTER_ID", PresenterID);
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
        public string Presenter_Delete(int PresenterID, string delflag)
        {
            SqlCommand cmd = new SqlCommand("spPresenterDelete", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PresenterID", PresenterID);
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