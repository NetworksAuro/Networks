using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace PriceScaleDataLayer
{
    public class PriceScaleData
    {
        SqlConnection dbconn = new SqlConnection(DBConnectionLayer.DBConnection.GetDBConnection());
        public DataTable GetPriceScales(int engtid, int rec, string ps_scale)
        {
            SqlCommand cmd = new SqlCommand("SPGETPRICESCALEDETAILS", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PS_ENGT_ID", engtid);
            cmd.Parameters.AddWithValue("@rec_count", rec);
            cmd.Parameters.AddWithValue("@ps_scale", ps_scale);
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
        public DataTable GetPriceScalesSchedule(int engtid, string ps_scale)
        {
            SqlCommand cmd = new SqlCommand("SPGETPRICESCALESchedule", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PS_ENGT_ID", engtid);
            
            cmd.Parameters.AddWithValue("@ps_scale", ps_scale);
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

        public int UpdatePriceScales(int engtid, int psid, string psscale_days, string seats, string prices, string saleamount, string details, string sunit, string sdiscount, string sprice,
string gunit, string gdiscount, string gprice, string psweek, string psscale, string psscale_lvl, int delflg)
        {
            Nullable<int> seats1 = null;
            Nullable<decimal> prices1 = null, sdiscount1 = null, sprice1 = null, gdiscount1 = null, gprice1 = null, saleamount1 = null;

            seats1 = (seats.Trim() == "") ? seats1 : Convert.ToInt32(seats);
            prices1 = (prices.Trim() == "") ? prices1 : Convert.ToDecimal(Regex.Replace(prices, @"\$|\,", ""));
            saleamount1 = (saleamount.Trim() == "") ? saleamount1 : Convert.ToDecimal(Regex.Replace(saleamount, @"\$|\,", ""));
            sdiscount1 = (sdiscount.Trim() == "") ? sdiscount1 : Convert.ToDecimal(Regex.Replace(sdiscount, @"\$|\,", ""));
            sprice1 = (sprice.Trim() == "") ? sprice1 : Convert.ToDecimal(Regex.Replace(sprice, @"\$|\,", ""));
            gdiscount1 = (gdiscount.Trim() == "") ? gdiscount1 : Convert.ToDecimal(Regex.Replace(gdiscount, @"\$|\,", ""));
            gprice1 = (gprice.Trim() == "") ? gprice1 : Convert.ToDecimal(Regex.Replace(gprice, @"\$|\,", ""));

            SqlCommand cmd = new SqlCommand("SPUPDATEPRICESCALE", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EngtID", engtid);
            cmd.Parameters.AddWithValue("@PS_ID", psid);
            cmd.Parameters.AddWithValue("@ps_schedule_days", psscale_days);
            cmd.Parameters.AddWithValue("@PS_SEATS_SINGLE", seats1);
            cmd.Parameters.AddWithValue("@PS_T_PRICE_SINGLE", prices1);
            cmd.Parameters.AddWithValue("@PS_SALE_AMOUNT", saleamount1);
            cmd.Parameters.AddWithValue("@PS_SEAT_DETAIL_SINGLE", details);
            cmd.Parameters.AddWithValue("@PS_DISCOUNT_UNIT_SUB", sunit);
            cmd.Parameters.AddWithValue("@PS_DISCOUNT_SUB", sdiscount1);
            cmd.Parameters.AddWithValue("@PS_T_PRICE_SUB", sprice1);
            cmd.Parameters.AddWithValue("@PS_DISCOUNT_UNIT_GRP", gunit);
            cmd.Parameters.AddWithValue("@PS_DISCOUNT_GRP", gdiscount1);
            cmd.Parameters.AddWithValue("@PS_T_PRICE_GRP", gprice1);
            cmd.Parameters.AddWithValue("@PS_SCHEDULE_WEEKS", psweek);
            cmd.Parameters.AddWithValue("@PS_SCALE", psscale);
            cmd.Parameters.AddWithValue("@PS_PRICELEVEL", psscale_lvl);
            cmd.Parameters.AddWithValue("@DelFlg", delflg);
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

        public decimal GetDealTax(int engtid)
        {
            SqlCommand cmd = new SqlCommand("SPGETDEALTAX", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PS_ENGT_ID", engtid);
            decimal retvalue;
            try
            {
                dbconn.Open();
                retvalue = Convert.ToDecimal(cmd.ExecuteScalar());
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

        public DataTable GetScheduleDayTime(int engtid)
        {
            SqlCommand cmd = new SqlCommand("SPGETSCHEDULEDAYS", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SCHEDULE_ENGT_ID", engtid);
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
        public void PriceScaleDelete(string ps_scale, string ps_scale_level, int engtid)
        {
            SqlCommand cmd = new SqlCommand("spPriceScaleDelete", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PriceScale", ps_scale);
            cmd.Parameters.AddWithValue("@PriceScaleLevel", ps_scale_level);
            cmd.Parameters.AddWithValue("@EngtID", engtid);
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
        public void UpdateExpenseInsurance(int ps_engtid)
        {
            SqlCommand cmd = new SqlCommand("spUpdateExpenseInsurance", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ENGMNTID", ps_engtid);
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
    }
}