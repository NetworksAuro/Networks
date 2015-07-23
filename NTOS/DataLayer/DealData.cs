using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DealDataLayer
{
    public class DealData
    {
        SqlConnection dbconn = new SqlConnection(DBConnectionLayer.DBConnection.GetDBConnection());

        public DataTable BindDealTypeList(int showid)
        {
            SqlCommand cmd = new SqlCommand("spGetDealTypeList", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@SHOW_ID", showid);
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

        public DataTable GetTemplateRevisionDates(int showid, string dealtype)
        {
            SqlCommand cmd = new SqlCommand("spGetDealTemplateRevisionDates", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@DEAL_TMPT_SHOW_ID", showid);
            cmd.Parameters.AddWithValue("@DEAL_TMPT_TYPE", dealtype);

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

        public DataTable GetDealTemplateDetails(int showid, string dealtype, DateTime revisiondate)
        {
            SqlCommand cmd = new SqlCommand("spGetDealTemplateDetails", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@DEAL_TMPT_SHOW_ID", showid);
            cmd.Parameters.AddWithValue("@DEAL_TMPT_TYPE", dealtype);
            cmd.Parameters.AddWithValue("@DEAL_TMPT_UPD_DATE", revisiondate);

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
        public DataTable GetDealTemplateDetailsById(int dealtemplateid)
        {
            SqlCommand cmd = new SqlCommand("spGetDealTemplateDetailsById", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@DEAL_TEMPLATE_ID", dealtemplateid);
            
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
        public DataTable GetDealTemplateShows(string status)
        {
            SqlCommand cmd = new SqlCommand("spGetDealTemplateShows", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@DEAL_TMPT_ACTIVE_FLAG", status);

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
        public DataTable GetDealDetails(int engagementid)
        {
            SqlCommand cmd = new SqlCommand("spGetDealDetails", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@DEAL_ENGT_ID", engagementid);

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
       
        public int CreateDealTemplate(int showid, string dealtype, DateTime createdate, DateTime updatedate, Nullable<decimal> royalty,
           Nullable<decimal> guarantee, Nullable<decimal> compmonies, Nullable<decimal> presentermonies, Nullable<decimal> middlemonies, Nullable<decimal> producershare,
           Nullable<decimal> presentershare, Nullable<decimal> starroyalty, string taxbudgetflag, Nullable<decimal> taxbudget, string taxactualflag, Nullable<decimal> taxactual, string taxflag, Nullable<decimal> tax,
           Nullable<decimal> taxover, Nullable<decimal> subsale, Nullable<decimal> phonesale, Nullable<decimal> internetsale, Nullable<decimal> cardsale, string facilityflag1,
           Nullable<decimal> facility, string facilityflag2, string boxofficeflag1, Nullable<decimal> boxoffice,
           string misc1flag1, Nullable<decimal> misc1, string misc1flag2, string misc2flag1, Nullable<decimal> misc2, string misc2flag2,
            string misc3flag1, Nullable<decimal> misc3, string misc3flag2, string misc4flag1, Nullable<decimal> misc4, string misc4flag2,
            string misc5flag1, Nullable<decimal> misc5, string misc5flag2,
           Nullable<decimal> remotesale, Nullable<decimal> single, Nullable<decimal> group1, Nullable<decimal> group2, Nullable<decimal> tax2, string miscotherdesc1,
           string miscotherdesc2, string miscotherdesc3, string miscotherdesc4, string miscotherdesc5, string deal_tax_ptg_ff)
        {
            SqlCommand cmd = new SqlCommand("spInsertDealTemplate", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            SqlParameter dealtemplateid = new SqlParameter("@DEAL_TEMPLATE_ID", SqlDbType.Int);
            dealtemplateid.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(dealtemplateid);
            cmd.Parameters.AddWithValue("@DEAL_SHOW_ID", showid);
            cmd.Parameters.AddWithValue("@DEAL_TYPE", dealtype);
            cmd.Parameters.AddWithValue("@DEAL_CR_DATE", createdate);
            cmd.Parameters.AddWithValue("@DEAL_TYPE_UPD_DATE", updatedate);
            cmd.Parameters.AddWithValue("@DEAL_ROYALTY_INCOME", royalty);
            cmd.Parameters.AddWithValue("@DEAL_GUARANTEE_INCOME", guarantee);
            cmd.Parameters.AddWithValue("@DEAL_CMPNY_MID_MONIES_PTG", compmonies);
            cmd.Parameters.AddWithValue("@DEAL_PRESENTER_MID_MONIES_PTG", presentermonies);
            cmd.Parameters.AddWithValue("@DEAL_MID_MONIES_CAP", middlemonies);
            cmd.Parameters.AddWithValue("@DEAL_PRODUCER_SHARE_SPLIT_PTG", producershare);
            cmd.Parameters.AddWithValue("@DEAL_PRESENTER_SHARE_SPLIT_PTG", presentershare);
            cmd.Parameters.AddWithValue("@DEAL_STAR_ROYALTY_PTG", starroyalty);
            cmd.Parameters.AddWithValue("@DEAL_INCM_WTHD_TAX_BGT_UNIT", taxbudgetflag);
            cmd.Parameters.AddWithValue("@DEAL_INCM_WTHD_TAX_BGT_AMT", taxbudget);
            cmd.Parameters.AddWithValue("@DEAL_INCM_WTHD_TAX_ACT_UNIT", taxactualflag);
            cmd.Parameters.AddWithValue("@DEAL_INCM_WTHD_TAX_ACT_AMT", taxactual);
            cmd.Parameters.AddWithValue("@DEAL_TAX_PTG_INCLUDE", taxflag);
            cmd.Parameters.AddWithValue("@DEAL_TAX_PTG", tax);
            cmd.Parameters.AddWithValue("@DEAL_TAX_AMT_OVER", taxover);
            cmd.Parameters.AddWithValue("@DEAL_SUB_SALES_COMM", subsale);
            cmd.Parameters.AddWithValue("@DEAL_PH_SALES_COMM", phonesale);
            cmd.Parameters.AddWithValue("@DEAL_WEB_SALES_COMM", internetsale);
            cmd.Parameters.AddWithValue("@DEAL_CC_SALES_COMM", cardsale);
            cmd.Parameters.AddWithValue("@DEAL_FACILITY_FEE_INLCUDE", facilityflag1);
            cmd.Parameters.AddWithValue("@DEAL_FACILITY_FEE_AMT", facility);
            cmd.Parameters.AddWithValue("@DEAL_FACILITY_FEE_UNIT", facilityflag2);
            cmd.Parameters.AddWithValue("@DEAL_OTHR_BO_INCLUDE", boxofficeflag1);
            cmd.Parameters.AddWithValue("@deal_tax_ff_bo_comm", boxoffice);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_1", misc1flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_1", misc1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_1", misc1flag2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_2", misc2flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_2", misc2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_2", misc2flag2);

            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_3", misc3flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_3", misc3);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_3", misc3flag2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_4", misc4flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_4", misc4);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_4", misc4flag2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_5", misc5flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_5", misc5);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_5", misc5flag2);

            cmd.Parameters.AddWithValue("@DEAL_REMOTE_SALES_COMM", remotesale);
            cmd.Parameters.AddWithValue("@DEAL_SINGLE_TIX_COMM", single);
            cmd.Parameters.AddWithValue("@DEAL_GRP_SALES_COMM1", group1);
            cmd.Parameters.AddWithValue("@DEAL_GRP_SALES_COMM2", group2);
            cmd.Parameters.AddWithValue("@DEAL_TMPT_TAX2_PTG", tax2);
            cmd.Parameters.AddWithValue("@DEAL_TMPT_MISC_OTHR_DESC_1", miscotherdesc1);
            cmd.Parameters.AddWithValue("@DEAL_TMPT_MISC_OTHR_DESC_2", miscotherdesc2);
            cmd.Parameters.AddWithValue("@DEAL_TMPT_MISC_OTHR_DESC_3", miscotherdesc3);
            cmd.Parameters.AddWithValue("@DEAL_TMPT_MISC_OTHR_DESC_4", miscotherdesc4);
            cmd.Parameters.AddWithValue("@DEAL_TMPT_MISC_OTHR_DESC_5", miscotherdesc5);
            cmd.Parameters.AddWithValue("@DEAL_TMPT_TAX_PTG_FF", deal_tax_ptg_ff);
          
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
            retvalue = Convert.ToInt32(dealtemplateid.Value);
            return retvalue;
        }


        public int CreateDeal(int engagementid, string dealtype, DateTime updatedate, Nullable<decimal> royalty,
            Nullable<decimal> guarantee, Nullable<decimal> compmonies, Nullable<decimal> presentermonies, Nullable<decimal> middlemonies, Nullable<decimal> producershare,
            Nullable<decimal> presentershare, Nullable<decimal> starroyalty, string taxbudgetflag, Nullable<decimal> taxbudget, string taxactualflag, Nullable<decimal> taxactual, string taxflag, Nullable<decimal> tax,
            Nullable<decimal> taxover, Nullable<decimal> subsale, Nullable<decimal> phonesale, Nullable<decimal> internetsale, Nullable<decimal> cardsale, string facilityflag1,
            Nullable<decimal> facility, string facilityflag2, string boxofficeflag1, Nullable<decimal> boxoffice,
            string misc1flag1, Nullable<decimal> misc1, string misc1flag2, string misc2flag1, Nullable<decimal> misc2, string misc2flag2,
            string misc3flag1, Nullable<decimal> misc3, string misc3flag2, string misc4flag1, Nullable<decimal> misc4, string misc4flag2,
            string misc5flag1, Nullable<decimal> misc5, string misc5flag2,
            Nullable<decimal> remotesale, Nullable<decimal> single, Nullable<decimal> group1, Nullable<decimal> group2, Nullable<decimal> tax2, string miscotherdesc1,
            string miscotherdesc2, string miscotherdesc3, string miscotherdesc4, string miscotherdesc5, string deal_tax_ptg_ff)
            // string dealdemo, Nullable<decimal> exchange, string contract
        {
            SqlCommand cmd = new SqlCommand("spInsertDeal", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            SqlParameter dealid = new SqlParameter("@DEAL_ID", SqlDbType.Int);
            dealid.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(dealid);
            cmd.Parameters.AddWithValue("@DEAL_ENGT_ID", engagementid);
            cmd.Parameters.AddWithValue("@DEAL_TYPE", dealtype);
            cmd.Parameters.AddWithValue("@DEAL_TYPE_UPD_DATE", updatedate);
            cmd.Parameters.AddWithValue("@DEAL_ROYALTY_INCOME", royalty);
            cmd.Parameters.AddWithValue("@DEAL_GUARANTEE_INCOME", guarantee);
            cmd.Parameters.AddWithValue("@DEAL_CMPNY_MID_MONIES_PTG", compmonies);
            cmd.Parameters.AddWithValue("@DEAL_PRESENTER_MID_MONIES_PTG", presentermonies);
            cmd.Parameters.AddWithValue("@DEAL_MID_MONIES_CAP", middlemonies);
            cmd.Parameters.AddWithValue("@DEAL_PRODUCER_SHARE_SPLIT_PTG", producershare);
            cmd.Parameters.AddWithValue("@DEAL_PRESENTER_SHARE_SPLIT_PTG", presentershare);
            cmd.Parameters.AddWithValue("@DEAL_STAR_ROYALTY_PTG", starroyalty);
            cmd.Parameters.AddWithValue("@DEAL_INCM_WTHD_TAX_BGT_UNIT", taxbudgetflag);
            cmd.Parameters.AddWithValue("@DEAL_INCM_WTHD_TAX_BGT_AMT", taxbudget);
            cmd.Parameters.AddWithValue("@DEAL_INCM_WTHD_TAX_ACT_UNIT", taxactualflag);
            cmd.Parameters.AddWithValue("@DEAL_INCM_WTHD_TAX_ACT_AMT", taxactual);
            cmd.Parameters.AddWithValue("@DEAL_TAX_PTG_INCLUDE", taxflag);
            cmd.Parameters.AddWithValue("@DEAL_TAX_PTG", tax);
            cmd.Parameters.AddWithValue("@DEAL_TAX_AMT_OVER", taxover);
            cmd.Parameters.AddWithValue("@DEAL_SUB_SALES_COMM", subsale);
            cmd.Parameters.AddWithValue("@DEAL_PH_SALES_COMM", phonesale);
            cmd.Parameters.AddWithValue("@DEAL_WEB_SALES_COMM", internetsale);
            cmd.Parameters.AddWithValue("@DEAL_CC_SALES_COMM", cardsale);
            cmd.Parameters.AddWithValue("@DEAL_FACILITY_FEE_INLCUDE", facilityflag1);
            cmd.Parameters.AddWithValue("@DEAL_FACILITY_FEE_AMT", facility);
            cmd.Parameters.AddWithValue("@DEAL_FACILITY_FEE_UNIT", facilityflag2);
            cmd.Parameters.AddWithValue("@DEAL_OTHR_BO_INCLUDE", boxofficeflag1);
            cmd.Parameters.AddWithValue("@deal_tax_ff_bo_comm", boxoffice);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_1", misc1flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_1", misc1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_1", misc1flag2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_2", misc2flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_2", misc2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_2", misc2flag2);

            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_3", misc3flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_3", misc3);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_3", misc3flag2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_4", misc4flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_4", misc4);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_4", misc4flag2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_5", misc5flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_5", misc5);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_5", misc5flag2);

            cmd.Parameters.AddWithValue("@DEAL_REMOTE_SALES_COMM", remotesale);
            cmd.Parameters.AddWithValue("@DEAL_SINGLE_TIX_COMM", single);
            cmd.Parameters.AddWithValue("@DEAL_GRP_SALES_COMM1", group1);
            cmd.Parameters.AddWithValue("@DEAL_GRP_SALES_COMM2", group2);
            cmd.Parameters.AddWithValue("@DEAL_TAX2_PTG", tax2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_DESC_1", miscotherdesc1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_DESC_2", miscotherdesc2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_DESC_3", miscotherdesc3);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_DESC_4", miscotherdesc4);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_DESC_5", miscotherdesc5);
            cmd.Parameters.AddWithValue("@DEAL_TAX_PTG_FF", deal_tax_ptg_ff);
       
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
            retvalue = Convert.ToInt32(dealid.Value);
            return retvalue;
        }

        public int UpdateDealTemplate(int dealtemplateid, int showid, string dealtype, DateTime createdate, DateTime updatedate, Nullable<decimal> royalty,
            Nullable<decimal> guarantee, Nullable<decimal> compmonies, Nullable<decimal> presentermonies, Nullable<decimal> middlemonies, Nullable<decimal> producershare,
            Nullable<decimal> presentershare, Nullable<decimal> starroyalty, string taxbudgetflag, Nullable<decimal> taxbudget, string taxactualflag, Nullable<decimal> taxactual, string taxflag, Nullable<decimal> tax,
            Nullable<decimal> taxover, Nullable<decimal> subsale, Nullable<decimal> phonesale, Nullable<decimal> internetsale, Nullable<decimal> cardsale, string facilityflag1,
            Nullable<decimal> facility, string facilityflag2, string boxofficeflag1, Nullable<decimal> boxoffice,
            string misc1flag1, Nullable<decimal> misc1, string misc1flag2, string misc2flag1, Nullable<decimal> misc2, string misc2flag2,
            string misc3flag1, Nullable<decimal> misc3, string misc3flag2, string misc4flag1, Nullable<decimal> misc4, string misc4flag2,
            string misc5flag1, Nullable<decimal> misc5, string misc5flag2,
            Nullable<decimal> remotesale, Nullable<decimal> single, Nullable<decimal> group1, Nullable<decimal> group2, Nullable<decimal> tax2, string miscotherdesc1,
            string miscotherdesc2, string miscotherdesc3, string miscotherdesc4, string miscotherdesc5, string deal_tax_ptg_ff)
        {
            SqlCommand cmd = new SqlCommand("spUpdateDealTemplate", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@DEAL_TEMPLATE_ID", dealtemplateid);
            cmd.Parameters.AddWithValue("@DEAL_SHOW_ID", showid);
            cmd.Parameters.AddWithValue("@DEAL_TYPE", dealtype);
            cmd.Parameters.AddWithValue("@DEAL_CR_DATE", createdate);
            cmd.Parameters.AddWithValue("@DEAL_TYPE_UPD_DATE", updatedate);
            cmd.Parameters.AddWithValue("@DEAL_ROYALTY_INCOME", royalty);
            cmd.Parameters.AddWithValue("@DEAL_GUARANTEE_INCOME", guarantee);
            cmd.Parameters.AddWithValue("@DEAL_CMPNY_MID_MONIES_PTG", compmonies);
            cmd.Parameters.AddWithValue("@DEAL_PRESENTER_MID_MONIES_PTG", presentermonies);
            cmd.Parameters.AddWithValue("@DEAL_MID_MONIES_CAP", middlemonies);
            cmd.Parameters.AddWithValue("@DEAL_PRODUCER_SHARE_SPLIT_PTG", producershare);
            cmd.Parameters.AddWithValue("@DEAL_PRESENTER_SHARE_SPLIT_PTG", presentershare);
            cmd.Parameters.AddWithValue("@DEAL_STAR_ROYALTY_PTG", starroyalty);
            cmd.Parameters.AddWithValue("@DEAL_INCM_WTHD_TAX_BGT_UNIT", taxbudgetflag);
            cmd.Parameters.AddWithValue("@DEAL_INCM_WTHD_TAX_BGT_AMT", taxbudget);
            cmd.Parameters.AddWithValue("@DEAL_INCM_WTHD_TAX_ACT_UNIT", taxactualflag);
            cmd.Parameters.AddWithValue("@DEAL_INCM_WTHD_TAX_ACT_AMT", taxactual);
            cmd.Parameters.AddWithValue("@DEAL_TAX_PTG_INCLUDE", taxflag);
            cmd.Parameters.AddWithValue("@DEAL_TAX_PTG", tax);
            cmd.Parameters.AddWithValue("@DEAL_TAX_AMT_OVER", taxover);
            cmd.Parameters.AddWithValue("@DEAL_SUB_SALES_COMM", subsale);
            cmd.Parameters.AddWithValue("@DEAL_PH_SALES_COMM", phonesale);
            cmd.Parameters.AddWithValue("@DEAL_WEB_SALES_COMM", internetsale);
            cmd.Parameters.AddWithValue("@DEAL_CC_SALES_COMM", cardsale);
            cmd.Parameters.AddWithValue("@DEAL_FACILITY_FEE_INLCUDE", facilityflag1);
            cmd.Parameters.AddWithValue("@DEAL_FACILITY_FEE_AMT", facility);
            cmd.Parameters.AddWithValue("@DEAL_FACILITY_FEE_UNIT", facilityflag2);
            cmd.Parameters.AddWithValue("@DEAL_OTHR_BO_INCLUDE", boxofficeflag1);
            cmd.Parameters.AddWithValue("@deal_tax_ff_bo_comm", boxoffice);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_1", misc1flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_1", misc1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_1", misc1flag2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_2", misc2flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_2", misc2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_2", misc2flag2);

            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_3", misc3flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_3", misc3);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_3", misc3flag2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_4", misc4flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_4", misc4);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_4", misc4flag2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_5", misc5flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_5", misc5);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_5", misc5flag2);

            cmd.Parameters.AddWithValue("@DEAL_REMOTE_SALES_COMM", remotesale);
            cmd.Parameters.AddWithValue("@DEAL_SINGLE_TIX_COMM", single);
            cmd.Parameters.AddWithValue("@DEAL_GRP_SALES_COMM1", group1);
            cmd.Parameters.AddWithValue("@DEAL_GRP_SALES_COMM2", group2);
            cmd.Parameters.AddWithValue("@DEAL_TMPT_TAX2_PTG", tax2);
            cmd.Parameters.AddWithValue("@DEAL_TMPT_MISC_OTHR_DESC_1", miscotherdesc1);
            cmd.Parameters.AddWithValue("@DEAL_TMPT_MISC_OTHR_DESC_2", miscotherdesc2);
            cmd.Parameters.AddWithValue("@DEAL_TMPT_MISC_OTHR_DESC_3", miscotherdesc3);
            cmd.Parameters.AddWithValue("@DEAL_TMPT_MISC_OTHR_DESC_4", miscotherdesc4);
            cmd.Parameters.AddWithValue("@DEAL_TMPT_MISC_OTHR_DESC_5", miscotherdesc5);
            cmd.Parameters.AddWithValue("@DEAL_TMPT_TAX_PTG_FF", deal_tax_ptg_ff);
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

        public int DeleteDealTemplate(int dealtemplateid,string delflag)
        {
            SqlCommand cmd = new SqlCommand("spDeleteDealTemplate", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@DEAL_TEMPLATE_ID", dealtemplateid);
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

        public int UpdateDeal(int dealid, string dealtype, DateTime updatedate, Nullable<decimal> royalty,
                  Nullable<decimal> guarantee, Nullable<decimal> compmonies, Nullable<decimal> presentermonies, Nullable<decimal> middlemonies, Nullable<decimal> producershare,
                  Nullable<decimal> presentershare, Nullable<decimal> starroyalty, string taxbudgetflag, Nullable<decimal> taxbudget, string taxactualflag, Nullable<decimal> taxactual,
                  string taxflag, Nullable<decimal> tax, Nullable<decimal> taxover, Nullable<decimal> subsale, Nullable<decimal> phonesale, Nullable<decimal> internetsale,
                  Nullable<decimal> cardsale, string facilityflag1, Nullable<decimal> facility, string facilityflag2, string boxofficeflag1, Nullable<decimal> boxoffice,
                  string misc1flag1, Nullable<decimal> misc1, string misc1flag2, string misc2flag1, Nullable<decimal> misc2, string misc2flag2,
                  string misc3flag1, Nullable<decimal> misc3, string misc3flag2, string misc4flag1, Nullable<decimal> misc4, string misc4flag2,
                  string misc5flag1, Nullable<decimal> misc5, string misc5flag2,
                  Nullable<decimal> remotesale, Nullable<decimal> single, Nullable<decimal> group1, Nullable<decimal> group2, Nullable<decimal> tax2, string miscotherdesc1,
                  string miscotherdesc2, string miscotherdesc3, string miscotherdesc4, string miscotherdesc5, string deal_tax_ptg_ff)
        {
            SqlCommand cmd = new SqlCommand("spUpdateDeal", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@DEAL_ID", dealid);
            cmd.Parameters.AddWithValue("@DEAL_TYPE", dealtype);
            cmd.Parameters.AddWithValue("@DEAL_TYPE_UPD_DATE", updatedate);
            cmd.Parameters.AddWithValue("@DEAL_ROYALTY_INCOME", royalty);
            cmd.Parameters.AddWithValue("@DEAL_GUARANTEE_INCOME", guarantee);
            cmd.Parameters.AddWithValue("@DEAL_CMPNY_MID_MONIES_PTG", compmonies);
            cmd.Parameters.AddWithValue("@DEAL_PRESENTER_MID_MONIES_PTG", presentermonies);
            cmd.Parameters.AddWithValue("@DEAL_MID_MONIES_CAP", middlemonies);
            cmd.Parameters.AddWithValue("@DEAL_PRODUCER_SHARE_SPLIT_PTG", producershare);
            cmd.Parameters.AddWithValue("@DEAL_PRESENTER_SHARE_SPLIT_PTG", presentershare);
            cmd.Parameters.AddWithValue("@DEAL_STAR_ROYALTY_PTG", starroyalty);
            cmd.Parameters.AddWithValue("@DEAL_INCM_WTHD_TAX_BGT_UNIT", taxbudgetflag);
            cmd.Parameters.AddWithValue("@DEAL_INCM_WTHD_TAX_BGT_AMT", taxbudget);
            cmd.Parameters.AddWithValue("@DEAL_INCM_WTHD_TAX_ACT_UNIT", taxactualflag);
            cmd.Parameters.AddWithValue("@DEAL_INCM_WTHD_TAX_ACT_AMT", taxactual);
            cmd.Parameters.AddWithValue("@DEAL_TAX_PTG_INCLUDE", taxflag);
            cmd.Parameters.AddWithValue("@DEAL_TAX_PTG", tax);
            cmd.Parameters.AddWithValue("@DEAL_TAX_AMT_OVER", taxover);
            cmd.Parameters.AddWithValue("@DEAL_SUB_SALES_COMM", subsale);
            cmd.Parameters.AddWithValue("@DEAL_PH_SALES_COMM", phonesale);
            cmd.Parameters.AddWithValue("@DEAL_WEB_SALES_COMM", internetsale);
            cmd.Parameters.AddWithValue("@DEAL_CC_SALES_COMM", cardsale);
            cmd.Parameters.AddWithValue("@DEAL_FACILITY_FEE_INLCUDE", facilityflag1);
            cmd.Parameters.AddWithValue("@DEAL_FACILITY_FEE_AMT", facility);
            cmd.Parameters.AddWithValue("@DEAL_FACILITY_FEE_UNIT", facilityflag2);
            cmd.Parameters.AddWithValue("@DEAL_OTHR_BO_INCLUDE", boxofficeflag1);
            cmd.Parameters.AddWithValue("@deal_tax_ff_bo_comm", boxoffice);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_1", misc1flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_1", misc1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_1", misc1flag2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_2", misc2flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_2", misc2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_2", misc2flag2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_3", misc3flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_3", misc3);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_3", misc3flag2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_4", misc4flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_4", misc4);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_4", misc4flag2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_INCLUDE_5", misc5flag1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_5", misc5);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_UNIT_5", misc5flag2);
            cmd.Parameters.AddWithValue("@DEAL_REMOTE_SALES_COMM", remotesale);
            cmd.Parameters.AddWithValue("@DEAL_SINGLE_TIX_COMM", single);
            cmd.Parameters.AddWithValue("@DEAL_GRP_SALES_COMM1", group1);
            cmd.Parameters.AddWithValue("@DEAL_GRP_SALES_COMM2", group2);
            cmd.Parameters.AddWithValue("@DEAL_TAX2_PTG", tax2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_DESC_1", miscotherdesc1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_DESC_2", miscotherdesc2);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_DESC_3", miscotherdesc3);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_DESC_4", miscotherdesc4);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_DESC_5", miscotherdesc5);
            cmd.Parameters.AddWithValue("@DEAL_TAX_PTG_FF", deal_tax_ptg_ff);
           
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

        public int DeleteDeal(int engtid)
        {
            SqlCommand cmd = new SqlCommand("spdeletedeal", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ENGATEMENTID", engtid);

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
    }
}