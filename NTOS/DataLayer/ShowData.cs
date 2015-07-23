using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ShowDataLayer
{
    public class ShowData
    {
        SqlConnection dbconn = new SqlConnection(DBConnectionLayer.DBConnection.GetDBConnection());
        public List<string> searchdata(string searchtext, string Activeflag)
        {
            DataTable dt = new DataTable();
            List<string> show = new List<string>();
            SqlCommand cmd = new SqlCommand("spShownamesearch", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SHOW_NAME", searchtext);
            cmd.Parameters.AddWithValue("@ActiveFlag", Activeflag);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    show.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                }
                return show;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }


        }
        public Int32 Showdata_Insert(string showname, string showfedid, string showcorpname, string showcorpstreet, System.Nullable<Decimal> overheadnut, Nullable<Int32> cityid, Nullable<Decimal> wklyopexp, string royalties, DateTime showbegindate, string zipcode, Nullable<Int32> cmpmgr_id)
        {
            SqlCommand cmd = new SqlCommand("spShowInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@SHOW_NAME", showname);
            cmd.Parameters.AddWithValue("@SHOW_FED_ID", showfedid);
            cmd.Parameters.AddWithValue("@SHOW_CORP_NAME", showcorpname);
            cmd.Parameters.AddWithValue("@SHOW_CORP_STREET", showcorpstreet);
            cmd.Parameters.AddWithValue("@SHOW_OVERHEAD_NUT", overheadnut);
            cmd.Parameters.AddWithValue("@SHOW_CORP_CITY_ID", cityid);
            cmd.Parameters.AddWithValue("@SHOW_WKLY_OPERATING_EXPENSE", wklyopexp);
            cmd.Parameters.AddWithValue("@ROYALTIES", royalties);
            cmd.Parameters.AddWithValue("@SHOW_BEGIN_DT", showbegindate);
            cmd.Parameters.AddWithValue("@SHOW_CORP_ZIP", zipcode);
            cmd.Parameters.AddWithValue("@COMPANYMGR_ID", cmpmgr_id);
            SqlParameter pvNewId = new SqlParameter();
            Int32 newid = 0;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                pvNewId.ParameterName = "@NewShowID";
                pvNewId.DbType = DbType.Int32;
                pvNewId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvNewId);
                int rows;
                rows = cmd.ExecuteNonQuery();
                newid = Convert.ToInt32(cmd.Parameters["@NewShowID"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
            return newid;
        }
        public Int32 Showdata_Update(string showid, string showname, string showfedid, string showcorpname, string showcorpstreet, Nullable<Decimal> overheadnut, Nullable<Int32> cityid, Nullable<Decimal> wklyopexp, string royalties, DateTime showbegindate, string zipcode, Nullable<Int32> cmpmgr_id)
        {
            SqlCommand cmd = new SqlCommand("spShowUpdate", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@SHOW_ID", showid);
            cmd.Parameters.AddWithValue("@SHOW_NAME", showname);
            cmd.Parameters.AddWithValue("@SHOW_FED_ID", showfedid);
            cmd.Parameters.AddWithValue("@SHOW_CORP_NAME", showcorpname);
            cmd.Parameters.AddWithValue("@SHOW_CORP_STREET", showcorpstreet);
            cmd.Parameters.AddWithValue("@SHOW_OVERHEAD_NUT", overheadnut);
            cmd.Parameters.AddWithValue("@SHOW_CORP_CITY_ID", cityid);
            cmd.Parameters.AddWithValue("@SHOW_WKLY_OPERATING_EXPENSE", wklyopexp);
            cmd.Parameters.AddWithValue("@ROYALTIES", royalties);
            cmd.Parameters.AddWithValue("@SHOW_BEGIN_DT", showbegindate);
            cmd.Parameters.AddWithValue("@SHOW_CORP_ZIP", zipcode);
            cmd.Parameters.AddWithValue("@COMPANYMGR_ID", cmpmgr_id);
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
        public DataTable Getshowdetails(string showid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spShowSelect", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SHOWID", showid);
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
        public string Show_Delete(int ShowID,string delflag)
        {
            SqlCommand cmd = new SqlCommand("spShowDelete", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ShowID", ShowID);
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

        public void PreferenceShowDML(int prolist_id, int showid, int show_pro_id, int show_pro_order, string type, string delidlist)
        {
            SqlCommand cmd = new SqlCommand("SPPreferenceShowInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PRO_LIST_ID", prolist_id);
            cmd.Parameters.AddWithValue("@show_id", showid);
            cmd.Parameters.AddWithValue("@show_pro_id", show_pro_id);
            cmd.Parameters.AddWithValue("@show_pro_preference", show_pro_order);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@DelIDlist", delidlist);
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
        public DataTable Get_Prefshowdetails(string showid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SPGetPreferenceShowDetails", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SHOW_ID", showid);
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
    }
}