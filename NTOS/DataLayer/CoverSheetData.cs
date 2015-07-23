using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace CoverSheetDataLayer
{
    public class CoverSheetData
    {
        SqlConnection dbconn = new SqlConnection(DBConnectionLayer.DBConnection.GetDBConnection());
        public Int32 Cvr_DocumentInsert(Int32 cvr_id, string cvr_cover_flag, string cvr_grnty_flag, string cvr_royalty_flag, string cvr_ovrg_flag, string cvr_s_summary_flag, string cvr_venue_sett_flag,
            string cvr_bo_sheet_flag, string cvr_bo_statements_flag, string cvr_lbr_bills_flag, string cvr_musician_bills_flag, string cvr_local_exp_invoice_flag, string cvr_ad_flag,
            string cvr_contact_flag, string cvr_s_cover_notes, string cvr_grnty_notes, string cvr_royalty_notes, string cvr_ovrg_notes, string cvr_s_summary_notes, string cvr_venue_sett_notes,
           string cvr_bo_sheet_notes, string cvr_bo_statements_notes, string cvr_lbr_bills_notes, string cvr_musician_bills_notes, string cvr_local_exp_invoice_notes, string cvr_ad_notes, string cvr_contact_notes)
        {
            SqlCommand cmd = new SqlCommand("spCvrDocumentsInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CVR_ID", cvr_id);
            cmd.Parameters.AddWithValue("@CVR_S_COVER_FLAG", cvr_cover_flag);
            cmd.Parameters.AddWithValue("@CVR_GRNTY_FLAG", cvr_grnty_flag);
            cmd.Parameters.AddWithValue("@CVR_ROYALTY_FLAG", cvr_royalty_flag);
            cmd.Parameters.AddWithValue("@CVR_OVRG_FLAG", cvr_ovrg_flag);
            cmd.Parameters.AddWithValue("@CVR_S_SUMMARY_FLAG", cvr_s_summary_flag);
            cmd.Parameters.AddWithValue("@CVR_VENUE_SETT_FLAG", cvr_venue_sett_flag);
            cmd.Parameters.AddWithValue("@CVR_BO_SHEET_FLAG", cvr_bo_sheet_flag);
            cmd.Parameters.AddWithValue("@CVR_BO_STATEMENTS_FLAG", cvr_bo_statements_flag);
            cmd.Parameters.AddWithValue("@CVR_LBR_BILLS_FLAG", cvr_lbr_bills_flag);
            cmd.Parameters.AddWithValue("@CVR_MUSICIAN_BILLS_FLAG", cvr_musician_bills_flag);
            cmd.Parameters.AddWithValue("@CVR_LOCAL_EXP_INVOICE_FLAG", cvr_local_exp_invoice_flag);
            cmd.Parameters.AddWithValue("@CVR_AD_FLAG", cvr_ad_flag);
            cmd.Parameters.AddWithValue("@CVR_CONTACT_FLAG", cvr_contact_flag);
            cmd.Parameters.AddWithValue("@CVR_S_COVER_NOTES", cvr_s_cover_notes);
            cmd.Parameters.AddWithValue("@CVR_GRNTY_NOTES", cvr_grnty_notes);
            cmd.Parameters.AddWithValue("@CVR_ROYALTY_NOTES", cvr_royalty_notes);
            cmd.Parameters.AddWithValue("@CVR_OVRG_NOTES", cvr_ovrg_notes);
            cmd.Parameters.AddWithValue("@CVR_S_SUMMARY_NOTES", cvr_s_summary_notes);
            cmd.Parameters.AddWithValue("@CVR_VENUE_SETT_NOTES", cvr_venue_sett_notes);
            cmd.Parameters.AddWithValue("@CVR_BO_SHEET_NOTES", cvr_bo_sheet_notes);
            cmd.Parameters.AddWithValue("@CVR_BO_STATEMENTS_NOTES", cvr_bo_statements_notes);
            cmd.Parameters.AddWithValue("@CVR_LBR_BILLS_NOTES", cvr_lbr_bills_notes);
            cmd.Parameters.AddWithValue("@CVR_MUSICIAN_BILLS_NOTES", cvr_musician_bills_notes);
            cmd.Parameters.AddWithValue("@CVR_LOCAL_EXP_INVOICE_NOTES", cvr_local_exp_invoice_notes);
            cmd.Parameters.AddWithValue("@CVR_AD_NOTES", cvr_ad_notes);
            cmd.Parameters.AddWithValue("@CVR_CONTACT_NOTES", cvr_contact_notes);
            SqlParameter pvNewId = new SqlParameter();
            Int32 newid = 0;
            try
            {
                dbconn.Open();
                pvNewId.ParameterName = "@NewId";
                pvNewId.DbType = DbType.Int32;
                pvNewId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvNewId);
                cmd.ExecuteNonQuery();
                newid = Convert.ToInt32(cmd.Parameters["@NewId"].Value);
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
        public string Cvr_ReceivablesInsert(Int32 cvr_id, string cvr_cvabls_desc, Nullable<decimal> cvr_cvabls_charge, string cvr_cvabls_notes)
        {
            SqlCommand cmd = new SqlCommand("spCvrReceivablesInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CVR_ID", cvr_id);
            cmd.Parameters.AddWithValue("@CVR_CVABLS_DESC", cvr_cvabls_desc);
            cmd.Parameters.AddWithValue("@CVR_CVABLS_CHARGE", cvr_cvabls_charge);
            cmd.Parameters.AddWithValue("@CVR_CVABLS_NOTES", cvr_cvabls_notes);
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
        public string Cvr_ChargesInsert(Int32 cvr_id, string cvr_chgs_desc, Nullable<decimal> cvr_chgs_amt, string cvr_chgs_check, string cvr_chgs_notes)
        {
            SqlCommand cmd = new SqlCommand("spCvrChargesInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CVR_ID", cvr_id);
            cmd.Parameters.AddWithValue("@CVR_CHGS_DESC", cvr_chgs_desc);
            cmd.Parameters.AddWithValue("@CVR_CHGS_AMT", cvr_chgs_amt);
            cmd.Parameters.AddWithValue("@CVR_CHGS_CHECK", cvr_chgs_check);
            cmd.Parameters.AddWithValue("@CVR_CHGS_NOTES", cvr_chgs_notes);
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

        public void Cvr_EmailInsert(Int32 cvr_engt_id, string emailid, int del_flg)
        {
            SqlCommand cmd = new SqlCommand("spCvrEmailInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CVR_ID", cvr_engt_id);
            cmd.Parameters.AddWithValue("@cvr_email_desc", emailid);
            cmd.Parameters.AddWithValue("@del_flg", del_flg);
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
        }
        public void Cvr_SheetDelete(Int32 cvr_id, string type)
        {
            SqlCommand cmd = new SqlCommand("spGetCoverSheetDelete", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@cvr_id", cvr_id);
            cmd.Parameters.AddWithValue("@Type", type);
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
        }

        public DataTable GetCoverSheetDetails(int Engagementid, string type)
        {
            SqlCommand cmd = new SqlCommand("spGetCoverSheetDetails", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EngagementID", Engagementid);
            cmd.Parameters.AddWithValue("@Type", type);
            DataTable dt = new DataTable();
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
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

        public string Cvr_ChargesUpdate(Int32 cvr_chgs_id, Int32 cvr_engt_id, string cvr_chgs_desc, Nullable<decimal> cvr_chgs_amt, string cvr_chgs_check, string cvr_chgs_notes)
        {
            SqlCommand cmd = new SqlCommand("spCvrChargesUpdate", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CVR_CHGS_ID", cvr_chgs_id);
            cmd.Parameters.AddWithValue("@CVR_ENGT_ID", cvr_engt_id);
            cmd.Parameters.AddWithValue("@CVR_CHGS_DESC", cvr_chgs_desc);
            cmd.Parameters.AddWithValue("@CVR_CHGS_AMT", cvr_chgs_amt);
            cmd.Parameters.AddWithValue("@CVR_CHGS_CHECK", cvr_chgs_check);
            cmd.Parameters.AddWithValue("@CVR_CHGS_NOTES", cvr_chgs_notes);
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
        public string Cvr_ReceivablesUpdate(Int32 cvr_rcvabls_id, Int32 cvr_engt_id, string cvr_cvabls_desc, Nullable<decimal> cvr_cvabls_charge, string cvr_cvabls_notes)
        {
            SqlCommand cmd = new SqlCommand("spCvrReceivablesUpdate", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CVR_RCVABLS_ID", cvr_rcvabls_id);
            cmd.Parameters.AddWithValue("@CVR_ENGT_ID", cvr_engt_id);
            cmd.Parameters.AddWithValue("@CVR_CVABLS_DESC", cvr_cvabls_desc);
            cmd.Parameters.AddWithValue("@CVR_CVABLS_CHARGE", cvr_cvabls_charge);
            cmd.Parameters.AddWithValue("@CVR_CVABLS_NOTES", cvr_cvabls_notes);
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