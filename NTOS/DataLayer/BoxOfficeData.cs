using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using NTOS.DataLayer;
using CommonFunction;

namespace BoxOfficeLayer
{
    public class BoxOfficeData
    {
        SqlConnection dbconn = new SqlConnection(DBConnectionLayer.DBConnection.GetDBConnection());
        BoxofficeOvrr bovrr = new BoxofficeOvrr();
        public DataTable LoadPerformancelist(int engntid)
        {
            SqlCommand cmd = new SqlCommand("spLoadPerformances", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@engmntid", engntid);
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
        public DataTable LoadDiscountdata(int engntid, int scheduleid)
        {
            SqlCommand cmd = new SqlCommand("spGetdiscount", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@engmntid", engntid);
            cmd.Parameters.AddWithValue("@scheduleid", scheduleid);
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
        public DataTable Loadboxofficedata(int engntid,int scheduleid)
        {
            SqlCommand cmd = new SqlCommand("spGetboxoffice", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@engmntid", engntid);
            cmd.Parameters.AddWithValue("@scheduleid", scheduleid);
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
        public DataTable GetPerformancelist(int engmntid)
        {
            SqlCommand cmd = new SqlCommand("spGetPerformances", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@engmntid", engmntid);
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




        public void BoxOfficeOverrde_Insert(int boiid,BoxofficeOvrr bo)
        {
            SqlCommand cmd = new SqlCommand("spBoxOfficeOveride", dbconn);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.Clear();
                 cmd.Parameters.AddWithValue("@bo_Id", boiid);
                 cmd.Parameters.AddWithValue("@bo_override", bo.bo_override);
                 cmd.Parameters.AddWithValue( "@bo_sub_ff"  ,bo.bo_sub_ff     );
                 cmd.Parameters.AddWithValue( "@bo_sub_tax1"  ,bo.bo_sub_tax1 );
                 cmd.Parameters.AddWithValue( "@bo_sub_net_comm"  ,bo.bo_sub_net_comm     );
                 cmd.Parameters.AddWithValue( "@bo_sub_tax_ff_comm"  ,bo.bo_sub_tax_ff_comm );
                 cmd.Parameters.AddWithValue( "@bo_ph_ff"  ,bo.bo_ph_ff);
                 cmd.Parameters.AddWithValue( "@bo_ph_tax1"  ,bo.bo_ph_tax1  );
                 cmd.Parameters.AddWithValue( "@bo_ph_net_comm"  ,bo.bo_ph_net_comm  );
                 cmd.Parameters.AddWithValue( "@bo_ph_tax_ff_comm"  ,bo.bo_ph_tax_ff_comm  );
                 cmd.Parameters.AddWithValue( "@bo_web_ff"  ,bo.bo_web_ff);
                 cmd.Parameters.AddWithValue( "@bo_web_tax1"  ,bo.bo_web_tax1 );
                 cmd.Parameters.AddWithValue( "@bo_web_net_comm"  ,bo.bo_web_net_comm  );
                 cmd.Parameters.AddWithValue( "@bo_web_tax_ff_comm"  ,bo.bo_web_tax_ff_comm);
                 cmd.Parameters.AddWithValue( "@bo_cc_ff"  ,bo.bo_cc_ff     );
                 cmd.Parameters.AddWithValue( "@bo_cc_tax1"  ,bo.bo_cc_tax1 );
                 cmd.Parameters.AddWithValue( "@bo_cc_net_comm"  ,bo.bo_cc_net_comm     );
                 cmd.Parameters.AddWithValue( "@bo_cc_tax_ff_comm"  ,bo.bo_cc_tax_ff_comm);
                 cmd.Parameters.AddWithValue( "@bo_outlet_ff"  ,bo.bo_outlet_ff     );
                 cmd.Parameters.AddWithValue( "@bo_outlet_tax1"  ,bo.bo_outlet_tax1 );
                 cmd.Parameters.AddWithValue( "@bo_outlet_net_comm"  ,bo.bo_outlet_net_comm      );
                 cmd.Parameters.AddWithValue( "@bo_outlet_tax_ff_comm"  ,bo.bo_outlet_tax_ff_comm);
                 cmd.Parameters.AddWithValue( "@bo_single_tix_ff"  ,bo.bo_single_tix_ff);
                 cmd.Parameters.AddWithValue( "@bo_single_tix_tax1"  ,bo.bo_single_tix_tax1 );
                 cmd.Parameters.AddWithValue( "@bo_single_tix_net_comm"  ,bo.bo_single_tix_net_comm     );
                 cmd.Parameters.AddWithValue( "@bo_single_tax_ff_comm"  ,bo.bo_single_tax_ff_comm);
                 cmd.Parameters.AddWithValue( "@bo_small_group_ff"  ,bo.bo_small_group_ff     );
                 cmd.Parameters.AddWithValue( "@bo_small_group_tax1"  ,bo.bo_small_group_tax1 );
                 cmd.Parameters.AddWithValue( "@bo_small_group_net_comm"  ,bo.bo_small_group_net_comm     );
                 cmd.Parameters.AddWithValue( "@bo_small_tax_ff_comm"  ,bo.bo_small_tax_ff_comm);
                 cmd.Parameters.AddWithValue( "@bo_large_group_ff"  ,bo.bo_large_group_ff     );
                 cmd.Parameters.AddWithValue( "@bo_large_group_tax1"  ,bo.bo_large_group_tax1 );
                 cmd.Parameters.AddWithValue( "@bo_large_group_net_comm"  ,bo.bo_large_group_net_comm);
                 cmd.Parameters.AddWithValue( "@bo_large_tax_ff_comm"  ,bo.bo_large_tax_ff_comm);
                 cmd.Parameters.AddWithValue( "@bo_large_tax_ff_tot_comm"  ,bo.bo_large_tax_ff_tot_comm);
                 cmd.Parameters.AddWithValue( "@bo_other_per_ff"  ,bo.bo_other_per_ff);
                 cmd.Parameters.AddWithValue( "@bo_other_per_tax1"  ,bo.bo_other_per_tax1 );
                 cmd.Parameters.AddWithValue( "@bo_other_per_net_comm"  ,bo.bo_other_per_net_comm);
                 cmd.Parameters.AddWithValue( "@bo_other_usd_ff"  ,bo.bo_other_usd_ff);
                 cmd.Parameters.AddWithValue( "@bo_other_usd_tax1"  ,bo.bo_other_usd_tax1);
                 cmd.Parameters.AddWithValue( "@bo_other_usd_net_comm"  ,bo.bo_other_usd_net_comm);
                 cmd.Parameters.AddWithValue( "@bo_other3_t_sold"  ,bo.bo_other3_t_sold);
                 cmd.Parameters.AddWithValue( "@bo_other3_gross_rcpt"  ,bo.bo_other3_gross_rcpt );
                 cmd.Parameters.AddWithValue( "@bo_other3_ff"  ,bo.bo_other3_ff);
                 cmd.Parameters.AddWithValue( "@bo_other3_tax1"  ,bo.bo_other3_tax1);
                 cmd.Parameters.AddWithValue( "@bo_other3_net_comm"  ,bo.bo_other3_net_comm);
                 cmd.Parameters.AddWithValue( "@bo_other4_t_sold"  ,bo.bo_other4_t_sold);
                 cmd.Parameters.AddWithValue( "@bo_other4_gross_rcpt"  ,bo.bo_other4_gross_rcpt);
                 cmd.Parameters.AddWithValue( "@bo_other4_ff"  ,bo.bo_other4_ff);
                 cmd.Parameters.AddWithValue( "@bo_other4_tax1"  ,bo.bo_other4_tax1);
                 cmd.Parameters.AddWithValue( "@bo_other4_net_comm"  ,bo.bo_other4_net_comm);
                 cmd.Parameters.AddWithValue( "@bo_other5_t_sold"  ,bo.bo_other5_t_sold);
                 cmd.Parameters.AddWithValue( "@bo_other5_gross_rcpt"  ,bo.bo_other5_gross_rcpt);
                 cmd.Parameters.AddWithValue( "@bo_other5_ff"  ,bo.bo_other5_ff);
                 cmd.Parameters.AddWithValue( "@bo_other5_tax1"  ,bo.bo_other5_tax1);
                 cmd.Parameters.AddWithValue( "@bo_other5_net_comm"  ,bo.bo_other5_net_comm);


                 try
                 {
                     if (dbconn.State == ConnectionState.Closed)
                     {
                         dbconn.Open();
                     }

                   
                     cmd.ExecuteNonQuery();
                   
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 finally
                 {
                     if (dbconn.State == ConnectionState.Open)
                     {
                         dbconn.Close();
                     }
                 }


        }




        public int BoxOffice_Insert(out int BOID,Nullable<int> bo_engt_id, Nullable<int> bo_schedule_id,
Nullable<decimal> bo_gross_sales = null, Nullable<decimal> bo_drop_count = null, Nullable<decimal> bo_paid_attendance = null, Nullable<decimal> bo_comps = null, Nullable<decimal> bo_sub_t_sold = null
, Nullable<decimal> bo_sub_gross_rcpt = null, Nullable<decimal> bo_ph_t_sold = null, Nullable<decimal> bo_ph_gross_rcpt = null,
Nullable<decimal> bo_web_t_sold = null, Nullable<decimal> bo_web_gross_rcpt = null, Nullable<decimal> bo_cc_t_sold = null, Nullable<decimal> bo_cc_gross_rcpt = null,
Nullable<decimal> bo_outlet_t_sold = null, Nullable<decimal> bo_outlet_gross_rcpt = null,
Nullable<decimal> bo_single_tix_t_sold = null, Nullable<decimal> bo_single_tix_gross_rcpt = null, Nullable<decimal> bo_small_group_t_sold = null,
Nullable<decimal> bo_small_group_gross_rcpt = null, Nullable<decimal> bo_large_group_t_sold = null, Nullable<decimal> bo_large_group_gross_rcpt = null,
Nullable<decimal> bo_other_per_t_sold = null, Nullable<decimal> bo_other_per_gross_rcpt = null,
Nullable<decimal> bo_other_usd_t_sold = null, Nullable<decimal> bo_other_usd_gross_rcpt = null,
Nullable<decimal> bo_other_3_t_sold = null, Nullable<decimal> bo_other_3_gross_rcpt = null,
Nullable<decimal> bo_other_4_t_sold = null, Nullable<decimal> bo_other_4_gross_rcpt = null,
Nullable<decimal> bo_other_5_t_sold = null, Nullable<decimal> bo_other_5_gross_rcpt = null
            )
        {
            SqlCommand cmd = new SqlCommand("spBoxofficeInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@bo_engt_id", bo_engt_id);
            cmd.Parameters.AddWithValue("@bo_schedule_id", bo_schedule_id);
            cmd.Parameters.AddWithValue("@bo_gross_sales", bo_gross_sales);
            cmd.Parameters.AddWithValue("@bo_drop_count", bo_drop_count);
            cmd.Parameters.AddWithValue("@bo_paid_attendance", bo_paid_attendance);
            cmd.Parameters.AddWithValue("@bo_comps", bo_comps);
            cmd.Parameters.AddWithValue("@bo_sub_t_sold", bo_sub_t_sold);
            cmd.Parameters.AddWithValue("@bo_sub_gross_rcpt", bo_sub_gross_rcpt);
            cmd.Parameters.AddWithValue("@bo_ph_t_sold", bo_ph_t_sold);
            cmd.Parameters.AddWithValue("@bo_ph_gross_rcpt", bo_ph_gross_rcpt);
            cmd.Parameters.AddWithValue("@bo_web_t_sold", bo_web_t_sold);
            cmd.Parameters.AddWithValue("@bo_web_gross_rcpt", bo_web_gross_rcpt);
            cmd.Parameters.AddWithValue("@bo_cc_t_sold", bo_cc_t_sold);
            cmd.Parameters.AddWithValue("@bo_cc_gross_rcpt", bo_cc_gross_rcpt);
            cmd.Parameters.AddWithValue("@bo_outlet_t_sold", bo_outlet_t_sold);
            cmd.Parameters.AddWithValue("@bo_outlet_gross_rcpt", bo_outlet_gross_rcpt);
            cmd.Parameters.AddWithValue("@bo_single_tix_t_sold", bo_single_tix_t_sold);
            cmd.Parameters.AddWithValue("@bo_single_tix_gross_rcpt", bo_single_tix_gross_rcpt);
            cmd.Parameters.AddWithValue("@bo_small_group_t_sold", bo_small_group_t_sold);
            cmd.Parameters.AddWithValue("@bo_small_group_gross_rcpt", bo_small_group_gross_rcpt);
            cmd.Parameters.AddWithValue("@bo_large_group_t_sold", bo_large_group_t_sold);
            cmd.Parameters.AddWithValue("@bo_large_group_gross_rcpt", bo_large_group_gross_rcpt);
            cmd.Parameters.AddWithValue("@bo_other_per_t_sold", bo_other_per_t_sold);
            cmd.Parameters.AddWithValue("@bo_other_per_gross_rcpt", bo_other_per_gross_rcpt);
            cmd.Parameters.AddWithValue("@bo_other_usd_t_sold", bo_other_usd_t_sold);
            cmd.Parameters.AddWithValue("@bo_other_usd_gross_rcpt", bo_other_usd_gross_rcpt);
            cmd.Parameters.AddWithValue("@bo_other_3_t_sold", bo_other_3_t_sold);
            cmd.Parameters.AddWithValue("@bo_other_3_gross_rcpt", bo_other_3_gross_rcpt);
            cmd.Parameters.AddWithValue("@bo_other_4_t_sold", bo_other_4_t_sold);
            cmd.Parameters.AddWithValue("@bo_other_4_gross_rcpt", bo_other_4_gross_rcpt);
            cmd.Parameters.AddWithValue("@bo_other_5_t_sold", bo_other_5_t_sold);
            cmd.Parameters.AddWithValue("@bo_other_5_gross_rcpt", bo_other_5_gross_rcpt);

            cmd.Parameters.AddWithValue("@ACTION", 0);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);


                SqlParameter pvOUTPUTMSG1= new SqlParameter();
                pvOUTPUTMSG1.ParameterName = "@OUTBOID";
                pvOUTPUTMSG1.DbType = DbType.Int32;
                pvOUTPUTMSG1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG1);
                cmd.ExecuteNonQuery();
                BOID = Convert.ToInt32(cmd.Parameters["@OUTBOID"].Value);

                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }


        }

        public int Discount_Insert(int dsct_engt_id, int dsct_schedule_id, Nullable<decimal> dsct_sub1_per, Nullable<decimal> dsct_sub2_per,
Nullable<decimal> dsct_sub3_per, Nullable<decimal> dsct_sub4_per, Nullable<decimal> dsct_sub5_per, Nullable<decimal> dsct_sub6_per, Nullable<decimal> dsct_sub7_per, Nullable<decimal> dsct_sub8_per,
            Nullable<decimal> dsct_sub9_per, Nullable<decimal> dsct_sub10_per,
            Nullable<decimal> dsct_sub1_tickets,Nullable<decimal> dsct_sub2_tickets, Nullable<decimal> dsct_sub3_tickets, Nullable<decimal> dsct_sub4_tickets, Nullable<decimal> dsct_sub5_tickets, Nullable<decimal> dsct_sub6_tickets,
            Nullable<decimal> dsct_sub7_tickets, Nullable<decimal> dsct_sub8_tickets, Nullable<decimal> dsct_sub9_tickets, Nullable<decimal> dsct_sub10_tickets,
Nullable<decimal> dsct_sml_grp_per, Nullable<decimal> dsct_lrg_grp_per,
            Nullable<decimal> dsct_grp3_per, Nullable<decimal> dsct_grp4_per, Nullable<decimal> dsct_grp5_per, Nullable<decimal> dsct_grp6_per, Nullable<decimal> dsct_grp7_per, Nullable<decimal> dsct_grp8_per, Nullable<decimal> dsct_grp9_per, Nullable<decimal> dsct_grp10_per,
            Nullable<decimal> dsct_sml_grp_tickets, Nullable<decimal> dsct_lrg_grp_tickets, Nullable<decimal> dsct_grp3_tickets, Nullable<decimal> dsct_grp4_tickets, Nullable<decimal> dsct_grp5_tickets, Nullable<decimal> dsct_grp6_tickets, Nullable<decimal> dsct_grp7_tickets,
            Nullable<decimal> dsct_grp8_tickets, Nullable<decimal> dsct_grp9_tickets, Nullable<decimal> dsct_grp10_tickets, 
            Nullable<decimal> dsct_misc1_per,
Nullable<decimal> dsct_misc2_per, Nullable<decimal> dsct_misc3_per, Nullable<decimal> dsct_misc4_per, Nullable<decimal> dsct_misc5_per, Nullable<decimal> dsct_misc6_per, Nullable<decimal> dsct_misc7_per, Nullable<decimal> dsct_misc8_per, Nullable<decimal> dsct_misc9_per, Nullable<decimal> dsct_misc10_per,
            Nullable<decimal> dsct_misc1_tickets, Nullable<decimal> dsct_misc2_tickets,
Nullable<decimal> dsct_misc3_tickets, Nullable<decimal> dsct_misc4_tickets, Nullable<decimal> dsct_misc5_tickets, Nullable<decimal> dsct_misc6_tickets, Nullable<decimal> dsct_misc7_tickets, Nullable<decimal> dsct_misc8_tickets, Nullable<decimal> dsct_misc9_tickets, Nullable<decimal> dsct_misc10_tickets,
            Nullable<decimal> dsct_demand_price,string notes)
        {
            SqlCommand cmd = new SqlCommand("spDiscountInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@dsct_engt_id ", dsct_engt_id);
            cmd.Parameters.AddWithValue("@dsct_schedule_id ", dsct_schedule_id);
            cmd.Parameters.AddWithValue("@dsct_sub1_per ", dsct_sub1_per);
            cmd.Parameters.AddWithValue("@dsct_sub2_per ", dsct_sub2_per);
            cmd.Parameters.AddWithValue("@dsct_sub3_per ", dsct_sub3_per);
            cmd.Parameters.AddWithValue("@dsct_sub4_per ", dsct_sub4_per);
            cmd.Parameters.AddWithValue("@dsct_sub5_per ", dsct_sub5_per);
            cmd.Parameters.AddWithValue("@dsct_sub6_per ", dsct_sub6_per);
            cmd.Parameters.AddWithValue("@dsct_sub7_per ", dsct_sub7_per);
            cmd.Parameters.AddWithValue("@dsct_sub8_per ", dsct_sub8_per);
            cmd.Parameters.AddWithValue("@dsct_sub9_per ", dsct_sub9_per);
            cmd.Parameters.AddWithValue("@dsct_sub10_per ", dsct_sub10_per);
            cmd.Parameters.AddWithValue("@dsct_sub1_tickets ", dsct_sub1_tickets);
            cmd.Parameters.AddWithValue("@dsct_sub2_tickets ", dsct_sub2_tickets);
            cmd.Parameters.AddWithValue("@dsct_sub3_tickets ", dsct_sub3_tickets);
            cmd.Parameters.AddWithValue("@dsct_sub4_tickets ", dsct_sub4_tickets);
            cmd.Parameters.AddWithValue("@dsct_sub5_tickets ", dsct_sub5_tickets);
            cmd.Parameters.AddWithValue("@dsct_sub6_tickets ", dsct_sub6_tickets);
            cmd.Parameters.AddWithValue("@dsct_sub7_tickets ", dsct_sub7_tickets);
            cmd.Parameters.AddWithValue("@dsct_sub8_tickets ", dsct_sub8_tickets);
            cmd.Parameters.AddWithValue("@dsct_sub9_tickets ", dsct_sub9_tickets);
            cmd.Parameters.AddWithValue("@dsct_sub10_tickets ", dsct_sub10_tickets);
            cmd.Parameters.AddWithValue("@dsct_sml_grp_per ", dsct_sml_grp_per);
            cmd.Parameters.AddWithValue("@dsct_lrg_grp_per ", dsct_lrg_grp_per);
            cmd.Parameters.AddWithValue("@dsct_grp3_per ", dsct_grp3_per);
            cmd.Parameters.AddWithValue("@dsct_grp4_per ", dsct_grp4_per);
            cmd.Parameters.AddWithValue("@dsct_grp5_per ", dsct_grp5_per);
            cmd.Parameters.AddWithValue("@dsct_grp6_per ", dsct_grp6_per);
            cmd.Parameters.AddWithValue("@dsct_grp7_per ", dsct_grp7_per);
            cmd.Parameters.AddWithValue("@dsct_grp8_per ", dsct_grp8_per);
            cmd.Parameters.AddWithValue("@dsct_grp9_per ", dsct_grp9_per);
            cmd.Parameters.AddWithValue("@dsct_grp10_per ", dsct_grp10_per);
            cmd.Parameters.AddWithValue("@dsct_sml_grp_tickets ", dsct_sml_grp_tickets);
            cmd.Parameters.AddWithValue("@dsct_lrg_grp_tickets ", dsct_lrg_grp_tickets);
            cmd.Parameters.AddWithValue("@dsct_grp3_tickets ", dsct_grp3_tickets);
            cmd.Parameters.AddWithValue("@dsct_grp4_tickets ", dsct_grp4_tickets);
            cmd.Parameters.AddWithValue("@dsct_grp5_tickets ", dsct_grp5_tickets);
            cmd.Parameters.AddWithValue("@dsct_grp6_tickets ", dsct_grp6_tickets);
            cmd.Parameters.AddWithValue("@dsct_grp7_tickets ", dsct_grp7_tickets);
            cmd.Parameters.AddWithValue("@dsct_grp8_tickets ", dsct_grp8_tickets);
            cmd.Parameters.AddWithValue("@dsct_grp9_tickets ", dsct_grp9_tickets);
            cmd.Parameters.AddWithValue("@dsct_grp10_tickets ", dsct_grp10_tickets);
            cmd.Parameters.AddWithValue("@dsct_misc1_per ", dsct_misc1_per);
            cmd.Parameters.AddWithValue("@dsct_misc2_per ", dsct_misc2_per);
            cmd.Parameters.AddWithValue("@dsct_misc3_per ", dsct_misc3_per);
            cmd.Parameters.AddWithValue("@dsct_misc4_per ", dsct_misc4_per);
            cmd.Parameters.AddWithValue("@dsct_misc5_per ", dsct_misc5_per);
            cmd.Parameters.AddWithValue("@dsct_misc6_per ", dsct_misc6_per);
            cmd.Parameters.AddWithValue("@dsct_misc7_per ", dsct_misc7_per);
            cmd.Parameters.AddWithValue("@dsct_misc8_per ", dsct_misc8_per);
            cmd.Parameters.AddWithValue("@dsct_misc9_per ", dsct_misc9_per);
            cmd.Parameters.AddWithValue("@dsct_misc10_per ", dsct_misc10_per);
            cmd.Parameters.AddWithValue("@dsct_misc1_tickets ", dsct_misc1_tickets);
            cmd.Parameters.AddWithValue("@dsct_misc2_tickets ", dsct_misc2_tickets);
            cmd.Parameters.AddWithValue("@dsct_misc3_tickets ", dsct_misc3_tickets);
            cmd.Parameters.AddWithValue("@dsct_misc4_tickets ", dsct_misc4_tickets);
            cmd.Parameters.AddWithValue("@dsct_misc5_tickets ", dsct_misc5_tickets);
            cmd.Parameters.AddWithValue("@dsct_misc6_tickets ", dsct_misc6_tickets);
            cmd.Parameters.AddWithValue("@dsct_misc7_tickets ", dsct_misc7_tickets);
            cmd.Parameters.AddWithValue("@dsct_misc8_tickets ", dsct_misc8_tickets);
            cmd.Parameters.AddWithValue("@dsct_misc9_tickets ", dsct_misc9_tickets);
            cmd.Parameters.AddWithValue("@dsct_misc10_tickets ", dsct_misc10_tickets);
            cmd.Parameters.AddWithValue("@dsct_demand_price ", dsct_demand_price);
            cmd.Parameters.AddWithValue("@dsct_Notes ", notes);

            cmd.Parameters.AddWithValue("@ACTION", 0);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }
        }



        public BoxofficeOvrr Getboxofcoverridedat(int boid)
        {
            BoxofficeOvrr bo = new BoxofficeOvrr();
            CommonFun objcf = new CommonFun(); 
            SqlCommand cmd = new SqlCommand("sp_getBoxOfficeOverride", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID", boid);
            DataTable dt = new DataTable();

            try
            {
                dbconn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
 
                        for (int i = 0 ; i< dt.Rows.Count  ;i++)
                        {
                            bo.bo_override = (Convert.ToString(dt.Rows[i]["bo_override"]));
                            bo.bo_sub_ff = objcf.ToDecimal(Convert.ToString(dt.Rows[i][1]));
                            bo.bo_sub_tax1 = objcf.ToDecimal(Convert.ToString(dt.Rows[i][2]));
                            bo.bo_sub_net_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i][3]));
                            bo.bo_sub_tax_ff_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i][4]));
                            bo.bo_ph_ff = objcf.ToDecimal(Convert.ToString(dt.Rows[i][5]));
                            bo.bo_ph_tax1 = objcf.ToDecimal(Convert.ToString(dt.Rows[i][6]));
                            bo.bo_ph_net_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i][7]));
                            bo.bo_ph_tax_ff_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i][8]));
                            bo.bo_web_ff = objcf.ToDecimal(Convert.ToString(dt.Rows[i][9]));
                            bo.bo_web_tax1 = objcf.ToDecimal(Convert.ToString(dt.Rows[i][10]));
                            bo.bo_web_net_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i][11]));
                            bo.bo_web_tax_ff_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i][12]));
                            bo.bo_cc_ff = objcf.ToDecimal(Convert.ToString(dt.Rows[i][13]));
                            bo.bo_cc_tax1 = objcf.ToDecimal(Convert.ToString(dt.Rows[i][14]));
                            bo.bo_cc_net_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i][15]));
                            bo.bo_cc_tax_ff_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i][16]));
                            bo.bo_outlet_ff = objcf.ToDecimal(Convert.ToString(dt.Rows[i][17]));
                            bo.bo_outlet_tax1 = objcf.ToDecimal(Convert.ToString(dt.Rows[i][18]));
                            bo.bo_outlet_net_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i][19]));
                            bo.bo_outlet_tax_ff_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i][20]));
                            bo.bo_single_tix_ff = objcf.ToDecimal(Convert.ToString(dt.Rows[i][21]));
                            bo.bo_single_tix_tax1 = objcf.ToDecimal(Convert.ToString(dt.Rows[i][22]));
                            bo.bo_single_tix_net_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i][23]));
                            bo.bo_single_tax_ff_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i][24]));
                            bo.bo_small_group_ff = objcf.ToDecimal(Convert.ToString(dt.Rows[i][25]));
                            bo.bo_small_group_tax1 = objcf.ToDecimal(Convert.ToString(dt.Rows[i][26]));
                            bo.bo_small_group_net_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i][27]));
                            bo.bo_small_tax_ff_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i][28]));
                            bo.bo_large_group_ff = objcf.ToDecimal(Convert.ToString(dt.Rows[i][29]));
                            bo.bo_large_group_tax1 = objcf.ToDecimal(Convert.ToString(dt.Rows[i][30]));
                            bo.bo_large_group_net_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i][31]));
                            bo.bo_large_tax_ff_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i][32]));
                            bo.bo_other_per_ff = objcf.ToDecimal(Convert.ToString(dt.Rows[i]["bo_other_per_ff"]));
                            bo.bo_other_per_tax1 = objcf.ToDecimal(Convert.ToString(dt.Rows[i]["bo_other_per_tax1"]));
                            bo.bo_other_per_net_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i]["bo_other_per_net_comm"]));
                            bo.bo_other_usd_ff = objcf.ToDecimal(Convert.ToString(dt.Rows[i]["bo_other_usd_ff"]));
                            bo.bo_other_usd_tax1 = objcf.ToDecimal(Convert.ToString(dt.Rows[i]["bo_other_usd_tax1"]));
                            bo.bo_other_usd_net_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i]["bo_other_usd_net_comm"]));
                            bo.bo_other3_t_sold = Convert.ToInt16(dt.Rows[i]["bo_other3_t_sold"]);
                            bo.bo_other3_gross_rcpt = objcf.ToDecimal(Convert.ToString(dt.Rows[i]["bo_other3_gross_rcpt"]));
                            bo.bo_other3_ff = objcf.ToDecimal(Convert.ToString(dt.Rows[i]["bo_other3_ff"]));
                            bo.bo_other3_tax1 = objcf.ToDecimal(Convert.ToString(dt.Rows[i]["bo_other3_tax1"]));
                            bo.bo_other3_net_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i]["bo_other3_net_comm"]));
                            bo.bo_other4_t_sold = Convert.ToInt16(dt.Rows[i]["bo_other4_t_sold"]);
                            bo.bo_other4_gross_rcpt = objcf.ToDecimal(Convert.ToString(dt.Rows[i]["bo_other4_gross_rcpt"]));
                            bo.bo_other4_ff = objcf.ToDecimal(Convert.ToString(dt.Rows[i]["bo_other4_ff"]));
                            bo.bo_other4_tax1 = objcf.ToDecimal(Convert.ToString(dt.Rows[i]["bo_other4_tax1"]));
                            bo.bo_other4_net_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i]["bo_other4_net_comm"]));
                            bo.bo_other5_t_sold = Convert.ToInt16(dt.Rows[i]["bo_other5_t_sold"]);
                            bo.bo_other5_gross_rcpt = objcf.ToDecimal(Convert.ToString(dt.Rows[i]["bo_other5_gross_rcpt"]));
                            bo.bo_other5_ff = objcf.ToDecimal(Convert.ToString(dt.Rows[i]["bo_other5_ff"]));
                            bo.bo_other5_tax1 = objcf.ToDecimal(Convert.ToString(dt.Rows[i]["bo_other5_tax1"]));
                            bo.bo_other5_net_comm = objcf.ToDecimal(Convert.ToString(dt.Rows[i]["bo_other5_net_comm"]));

                        }
                    
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

            return bo;
 
        }


        public DataTable Getboxofcdatafromdeal(int engid)
        {
            SqlCommand cmd = new SqlCommand("spBoxofficedatafromdeal", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@engmntid", engid);
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
    }
}