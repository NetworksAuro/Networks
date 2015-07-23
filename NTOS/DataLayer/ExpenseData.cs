using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ExpenseDataLayer
{
    public class ExpenseData
    {
        SqlConnection dbconn = new SqlConnection(DBConnectionLayer.DBConnection.GetDBConnection());

        public void Engagement_Expense_Insert(Int32 exp_engt_id, Nullable<decimal> de_ad_gross_act, Nullable<decimal> de_ad_gross_bgt, Nullable<decimal> de_insurance_act,
            Nullable<decimal> de_insurance_bgt, Nullable<decimal> de_insurance_per_unit, Nullable<decimal> de_labor_catering_act, Nullable<decimal> de_labor_catering_bgt,
            Nullable<decimal> de_musician_act, Nullable<decimal> de_musician_bgt, Nullable<decimal> de_other_1_act, Nullable<decimal> de_other_1_bgt, Nullable<decimal> de_other_2_act,
            Nullable<decimal> de_other_2_bgt, Nullable<decimal> de_stghand_loadin_act, Nullable<decimal> de_stghand_loadin_bgt, Nullable<decimal> de_stghand_loadout_act,
            Nullable<decimal> de_stghand_loadout_bgt, Nullable<decimal> de_stghand_running_act, Nullable<decimal> de_stghand_running_bgt, Nullable<decimal> de_ticket_print_act,
            Nullable<decimal> de_ticket_print_bgt, Nullable<decimal> de_ticket_print_per_unit, Nullable<decimal> de_wardrobe_loadin_act, Nullable<decimal> de_wardrobe_loadin_bgt,
            Nullable<decimal> de_wardrobe_loadout_act, Nullable<decimal> de_wardrobe_loadout_bgt, Nullable<decimal> de_wardrobe_running_act, Nullable<decimal> de_wardrobe_running_bgt,
            Nullable<decimal> le_adaexpense_act, Nullable<decimal> le_adaexpense_bgt, Nullable<decimal> le_bo_act, Nullable<decimal> le_bo_bgt, Nullable<decimal> le_catering_act,
            Nullable<decimal> le_catering_bgt, Nullable<decimal> le_dryice_act, Nullable<decimal> le_dryice_bgt, Nullable<decimal> le_equip_rental_act, Nullable<decimal> le_equip_rental_bgt,
            Nullable<decimal> le_grp_sales_act, Nullable<decimal> le_grp_sales_bgt, Nullable<decimal> le_house_staff_act, Nullable<decimal> le_house_staff_bgt,
            Nullable<decimal> le_league_fee_act, Nullable<decimal> le_league_fee_bgt, Nullable<decimal> le_license_act, Nullable<decimal> le_license_bgt, Nullable<decimal> le_limo_act,
            Nullable<decimal> le_limo_bgt, Nullable<decimal> le_local_fixed_act, Nullable<decimal> le_local_fixed_bgt, Nullable<decimal> le_orchestra_sh_remove_act,
            Nullable<decimal> le_orchestra_sh_remove_bgt, Nullable<decimal> le_other1_act, Nullable<decimal> le_other1_bgt, Nullable<decimal> le_other2_act, Nullable<decimal> le_other2_bgt,
            Nullable<decimal> le_other3_act, Nullable<decimal> le_other3_bgt, Nullable<decimal> le_other4_act, Nullable<decimal> le_other4_bgt, Nullable<decimal> le_other5_act,
            Nullable<decimal> le_other5_bgt, Nullable<decimal> le_phone_act, Nullable<decimal> le_phone_bgt, Nullable<decimal> le_police_act, Nullable<decimal> le_police_bgt,
            Nullable<decimal> le_presenter_profit_act, Nullable<decimal> le_presenter_profit_bgt, Nullable<decimal> le_program_act, Nullable<decimal> le_program_bgt,
            Nullable<decimal> le_rent_act, Nullable<decimal> le_rent_btg, Nullable<decimal> le_sound_act, Nullable<decimal> le_sound_bgt, Nullable<decimal> le_ticket_print_act,
            Nullable<decimal> le_ticket_print_bgt, Nullable<decimal> subtotal_localexpenses_act, Nullable<decimal> subtotal_localexpenses_bgt, Nullable<decimal> subtotal_varexpenses_act,
            Nullable<decimal> subtotal_varexpenses_bgt, string de_other_1_desc, string de_other_2_desc, string le_other1_desc, string le_other2_desc, string le_other3_desc,
            string le_other4_desc, string le_other5_desc)
        {
            SqlCommand cmd = new SqlCommand("spExpenseInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EXP_ENGT_ID", exp_engt_id);
            cmd.Parameters.AddWithValue("@EXP_D_AD_GROSS_BGT", de_ad_gross_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_AD_GROSS_ACT", de_ad_gross_act);
            cmd.Parameters.AddWithValue("@EXP_D_STGHAND_LOADIN_BGT", de_stghand_loadin_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_STGHAND_LOADIN_ACT", de_stghand_loadin_act);
            cmd.Parameters.AddWithValue("@EXP_D_STGHAND_LOADOUT_BGT", de_stghand_loadout_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_STGHAND_LOADOUT_ACT", de_stghand_loadout_act);
            cmd.Parameters.AddWithValue("@EXP_D_STGHAND_RUNNING_BGT", de_stghand_running_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_STGHAND_RUNNING_ACT", de_stghand_running_act);
            cmd.Parameters.AddWithValue("@EXP_D_WARDROBE_LOADIN_BGT", de_wardrobe_loadin_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_WARDROBE_LOADIN_ACT", de_wardrobe_loadin_act);
            cmd.Parameters.AddWithValue("@EXP_D_WARDROBE_LOADOUT_BGT", de_wardrobe_loadout_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_WARDROBE_LOADOUT_ACT", de_wardrobe_loadout_act);
            cmd.Parameters.AddWithValue("@EXP_D_WARDROBE_RUNNING_BGT", de_wardrobe_running_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_WARDROBE_RUNNING_ACT", de_wardrobe_running_act);
            cmd.Parameters.AddWithValue("@EXP_D_LABOR_CATERING_BGT", de_labor_catering_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_LABOR_CATERING_ACT", de_labor_catering_act);
            cmd.Parameters.AddWithValue("@EXP_D_MUSICIAN_BGT", de_musician_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_MUSICIAN_ACT", de_musician_act);
            cmd.Parameters.AddWithValue("@EXP_D_INSURANCE_PER_UNIT", de_insurance_per_unit);
            cmd.Parameters.AddWithValue("@EXP_D_INSURANCE_BGT", de_insurance_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_INSURANCE_ACT", de_insurance_act);
            cmd.Parameters.AddWithValue("@EXP_D_TICKET_PRINT_PER_UNIT", de_ticket_print_per_unit);
            cmd.Parameters.AddWithValue("@EXP_D_TICKET_PRINT_BGT", de_ticket_print_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_TICKET_PRINT_ACT", de_ticket_print_act);
            cmd.Parameters.AddWithValue("@EXP_D_OTHER_1_DESC", de_other_1_desc);
            cmd.Parameters.AddWithValue("@EXP_D_OTHER_1_BGT", de_other_1_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_OTHER_1_ACT", de_other_1_act);
            cmd.Parameters.AddWithValue("@EXP_D_OTHER_2_DESC", de_other_2_desc);
            cmd.Parameters.AddWithValue("@EXP_D_OTHER_2_BGT", de_other_2_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_OTHER_2_ACT", de_other_2_act);
            cmd.Parameters.AddWithValue("@EXP_L_ADA_EXPENSE_BGT", le_adaexpense_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_ADA_EXPENSE_ACT", le_adaexpense_act);
            cmd.Parameters.AddWithValue("@EXP_L_BO_BGT", le_bo_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_BO_ACT", le_bo_act);
            cmd.Parameters.AddWithValue("@EXP_L_CATERING_BGT", le_catering_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_CATERING_ACT", le_catering_act);
            cmd.Parameters.AddWithValue("@EXP_L_EQUIP_RENTAL_BGT", le_equip_rental_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_EQUIP_RENTAL_ACT", le_equip_rental_act);
            cmd.Parameters.AddWithValue("@EXP_L_GRP_SALES_BGT", le_grp_sales_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_GRP_SALES_ACT", le_grp_sales_act);
            cmd.Parameters.AddWithValue("@EXP_L_HOUSE_STAFF_BGT", le_house_staff_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_HOUSE_STAFF_ACT", le_house_staff_act);
            cmd.Parameters.AddWithValue("@EXP_L_LEAGUE_FEE_BGT", le_league_fee_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_LEAGUE_FEE_ACT", le_league_fee_act);
            cmd.Parameters.AddWithValue("@EXP_L_LICENSE_BGT", le_license_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_LICENSE_ACT", le_license_act);
            cmd.Parameters.AddWithValue("@EXP_L_LIMO_BGT", le_limo_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_LIMO_ACT", le_limo_act);
            cmd.Parameters.AddWithValue("@EXP_L_ORCHESTRA_SH_REMOVE_BGT", le_orchestra_sh_remove_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_ORCHESTRA_SH_REMOVE_ACT", le_orchestra_sh_remove_act);
            cmd.Parameters.AddWithValue("@EXP_L_PRESENTER_PROFIT_BGT", le_presenter_profit_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_PRESENTER_PROFIT_ACT", le_presenter_profit_act);
            cmd.Parameters.AddWithValue("@EXP_L_POLICE_BGT", le_police_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_POLICE_ACT", le_police_act);
            cmd.Parameters.AddWithValue("@EXP_L_PROGRAM_BGT", le_program_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_PROGRAM_ACT", le_program_act);
            cmd.Parameters.AddWithValue("@EXP_L_RENT_BTG", le_rent_btg);
            cmd.Parameters.AddWithValue("@EXP_L_RENT_ACT", le_rent_act);
            cmd.Parameters.AddWithValue("@EXP_L_SOUND_BGT", le_sound_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_SOUND_ACT", le_sound_act);
            cmd.Parameters.AddWithValue("@EXP_L_TICKET_PRINT_BGT", le_ticket_print_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_TICKET_PRINT_ACT", le_ticket_print_act);
            cmd.Parameters.AddWithValue("@EXP_L_PHONE_BGT", le_phone_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_PHONE_ACT", le_phone_act);
            cmd.Parameters.AddWithValue("@EXP_L_DRYICE_BGT", le_dryice_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_DRYICE_ACT", le_dryice_act);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER1_DESC", le_other1_desc);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER1_BGT", le_other1_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER1_ACT", le_other1_act);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER2_DESC", le_other2_desc);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER2_BGT", le_other2_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER2_ACT", le_other2_act);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER3_DESC", le_other3_desc);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER3_BGT", le_other3_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER3_ACT", le_other3_act);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER4_DESC", le_other4_desc);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER4_BGT", le_other4_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER4_ACT", le_other4_act);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER5_DESC", le_other5_desc);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER5_BGT", le_other5_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER5_ACT", le_other5_act);
            cmd.Parameters.AddWithValue("@EXP_L_LOCAL_FIXED_BGT", le_local_fixed_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_LOCAL_FIXED_ACT", le_local_fixed_act);
            cmd.Parameters.AddWithValue("@EXP_SUBTOTAL_VAR_EXPENSES_BGT", subtotal_varexpenses_bgt);
            cmd.Parameters.AddWithValue("@EXP_SUBTOTAL_VAR_EXPENSES_ACT", subtotal_varexpenses_act);
            cmd.Parameters.AddWithValue("@EXP_SUBTOTAL_LOCAL_EXPENSES_BGT", subtotal_localexpenses_bgt);
            cmd.Parameters.AddWithValue("@EXP_SUBTOTAL_LOCAL_EXPENSES_ACT", subtotal_localexpenses_act);

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

        public void Engagement_Expense_Update(Int32 expid,Int32 exp_engt_id, Nullable<decimal> de_ad_gross_act, Nullable<decimal> de_ad_gross_bgt, Nullable<decimal> de_insurance_act,
           Nullable<decimal> de_insurance_bgt, Nullable<decimal> de_insurance_per_unit, Nullable<decimal> de_labor_catering_act, Nullable<decimal> de_labor_catering_bgt,
           Nullable<decimal> de_musician_act, Nullable<decimal> de_musician_bgt, Nullable<decimal> de_other_1_act, Nullable<decimal> de_other_1_bgt, Nullable<decimal> de_other_2_act,
           Nullable<decimal> de_other_2_bgt, Nullable<decimal> de_stghand_loadin_act, Nullable<decimal> de_stghand_loadin_bgt, Nullable<decimal> de_stghand_loadout_act,
           Nullable<decimal> de_stghand_loadout_bgt, Nullable<decimal> de_stghand_running_act, Nullable<decimal> de_stghand_running_bgt, Nullable<decimal> de_ticket_print_act,
           Nullable<decimal> de_ticket_print_bgt, Nullable<decimal> de_ticket_print_per_unit, Nullable<decimal> de_wardrobe_loadin_act, Nullable<decimal> de_wardrobe_loadin_bgt,
           Nullable<decimal> de_wardrobe_loadout_act, Nullable<decimal> de_wardrobe_loadout_bgt, Nullable<decimal> de_wardrobe_running_act, Nullable<decimal> de_wardrobe_running_bgt,
           Nullable<decimal> le_adaexpense_act, Nullable<decimal> le_adaexpense_bgt, Nullable<decimal> le_bo_act, Nullable<decimal> le_bo_bgt, Nullable<decimal> le_catering_act,
           Nullable<decimal> le_catering_bgt, Nullable<decimal> le_dryice_act, Nullable<decimal> le_dryice_bgt, Nullable<decimal> le_equip_rental_act, Nullable<decimal> le_equip_rental_bgt,
           Nullable<decimal> le_grp_sales_act, Nullable<decimal> le_grp_sales_bgt, Nullable<decimal> le_house_staff_act, Nullable<decimal> le_house_staff_bgt,
           Nullable<decimal> le_league_fee_act, Nullable<decimal> le_league_fee_bgt, Nullable<decimal> le_license_act, Nullable<decimal> le_license_bgt, Nullable<decimal> le_limo_act,
           Nullable<decimal> le_limo_bgt, Nullable<decimal> le_local_fixed_act, Nullable<decimal> le_local_fixed_bgt, Nullable<decimal> le_orchestra_sh_remove_act,
           Nullable<decimal> le_orchestra_sh_remove_bgt, Nullable<decimal> le_other1_act, Nullable<decimal> le_other1_bgt, Nullable<decimal> le_other2_act, Nullable<decimal> le_other2_bgt,
           Nullable<decimal> le_other3_act, Nullable<decimal> le_other3_bgt, Nullable<decimal> le_other4_act, Nullable<decimal> le_other4_bgt, Nullable<decimal> le_other5_act,
           Nullable<decimal> le_other5_bgt, Nullable<decimal> le_phone_act, Nullable<decimal> le_phone_bgt, Nullable<decimal> le_police_act, Nullable<decimal> le_police_bgt,
           Nullable<decimal> le_presenter_profit_act, Nullable<decimal> le_presenter_profit_bgt, Nullable<decimal> le_program_act, Nullable<decimal> le_program_bgt,
           Nullable<decimal> le_rent_act, Nullable<decimal> le_rent_btg, Nullable<decimal> le_sound_act, Nullable<decimal> le_sound_bgt, Nullable<decimal> le_ticket_print_act,
           Nullable<decimal> le_ticket_print_bgt, Nullable<decimal> subtotal_localexpenses_act, Nullable<decimal> subtotal_localexpenses_bgt, Nullable<decimal> subtotal_varexpenses_act,
           Nullable<decimal> subtotal_varexpenses_bgt, string de_other_1_desc, string de_other_2_desc, string le_other1_desc, string le_other2_desc, string le_other3_desc,
           string le_other4_desc, string le_other5_desc)
        {
            SqlCommand cmd = new SqlCommand("spExpenseUpdate", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EXP_ID", expid);
            cmd.Parameters.AddWithValue("@EXP_ENGT_ID", exp_engt_id);
            cmd.Parameters.AddWithValue("@EXP_D_AD_GROSS_BGT", de_ad_gross_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_AD_GROSS_ACT", de_ad_gross_act);
            cmd.Parameters.AddWithValue("@EXP_D_STGHAND_LOADIN_BGT", de_stghand_loadin_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_STGHAND_LOADIN_ACT", de_stghand_loadin_act);
            cmd.Parameters.AddWithValue("@EXP_D_STGHAND_LOADOUT_BGT", de_stghand_loadout_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_STGHAND_LOADOUT_ACT", de_stghand_loadout_act);
            cmd.Parameters.AddWithValue("@EXP_D_STGHAND_RUNNING_BGT", de_stghand_running_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_STGHAND_RUNNING_ACT", de_stghand_running_act);
            cmd.Parameters.AddWithValue("@EXP_D_WARDROBE_LOADIN_BGT", de_wardrobe_loadin_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_WARDROBE_LOADIN_ACT", de_wardrobe_loadin_act);
            cmd.Parameters.AddWithValue("@EXP_D_WARDROBE_LOADOUT_BGT", de_wardrobe_loadout_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_WARDROBE_LOADOUT_ACT", de_wardrobe_loadout_act);
            cmd.Parameters.AddWithValue("@EXP_D_WARDROBE_RUNNING_BGT", de_wardrobe_running_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_WARDROBE_RUNNING_ACT", de_wardrobe_running_act);
            cmd.Parameters.AddWithValue("@EXP_D_LABOR_CATERING_BGT", de_labor_catering_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_LABOR_CATERING_ACT", de_labor_catering_act);
            cmd.Parameters.AddWithValue("@EXP_D_MUSICIAN_BGT", de_musician_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_MUSICIAN_ACT", de_musician_act);
            cmd.Parameters.AddWithValue("@EXP_D_INSURANCE_PER_UNIT", de_insurance_per_unit);
            cmd.Parameters.AddWithValue("@EXP_D_INSURANCE_BGT", de_insurance_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_INSURANCE_ACT", de_insurance_act);
            cmd.Parameters.AddWithValue("@EXP_D_TICKET_PRINT_PER_UNIT", de_ticket_print_per_unit);
            cmd.Parameters.AddWithValue("@EXP_D_TICKET_PRINT_BGT", de_ticket_print_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_TICKET_PRINT_ACT", de_ticket_print_act);
            cmd.Parameters.AddWithValue("@EXP_D_OTHER_1_DESC", de_other_1_desc);
            cmd.Parameters.AddWithValue("@EXP_D_OTHER_1_BGT", de_other_1_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_OTHER_1_ACT", de_other_1_act);
            cmd.Parameters.AddWithValue("@EXP_D_OTHER_2_DESC", de_other_2_desc);
            cmd.Parameters.AddWithValue("@EXP_D_OTHER_2_BGT", de_other_2_bgt);
            cmd.Parameters.AddWithValue("@EXP_D_OTHER_2_ACT", de_other_2_act);
            cmd.Parameters.AddWithValue("@EXP_L_ADA_EXPENSE_BGT", le_adaexpense_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_ADA_EXPENSE_ACT", le_adaexpense_act);
            cmd.Parameters.AddWithValue("@EXP_L_BO_BGT", le_bo_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_BO_ACT", le_bo_act);
            cmd.Parameters.AddWithValue("@EXP_L_CATERING_BGT", le_catering_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_CATERING_ACT", le_catering_act);
            cmd.Parameters.AddWithValue("@EXP_L_EQUIP_RENTAL_BGT", le_equip_rental_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_EQUIP_RENTAL_ACT", le_equip_rental_act);
            cmd.Parameters.AddWithValue("@EXP_L_GRP_SALES_BGT", le_grp_sales_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_GRP_SALES_ACT", le_grp_sales_act);
            cmd.Parameters.AddWithValue("@EXP_L_HOUSE_STAFF_BGT", le_house_staff_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_HOUSE_STAFF_ACT", le_house_staff_act);
            cmd.Parameters.AddWithValue("@EXP_L_LEAGUE_FEE_BGT", le_league_fee_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_LEAGUE_FEE_ACT", le_league_fee_act);
            cmd.Parameters.AddWithValue("@EXP_L_LICENSE_BGT", le_license_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_LICENSE_ACT", le_license_act);
            cmd.Parameters.AddWithValue("@EXP_L_LIMO_BGT", le_limo_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_LIMO_ACT", le_limo_act);
            cmd.Parameters.AddWithValue("@EXP_L_ORCHESTRA_SH_REMOVE_BGT", le_orchestra_sh_remove_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_ORCHESTRA_SH_REMOVE_ACT", le_orchestra_sh_remove_act);
            cmd.Parameters.AddWithValue("@EXP_L_PRESENTER_PROFIT_BGT", le_presenter_profit_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_PRESENTER_PROFIT_ACT", le_presenter_profit_act);
            cmd.Parameters.AddWithValue("@EXP_L_POLICE_BGT", le_police_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_POLICE_ACT", le_police_act);
            cmd.Parameters.AddWithValue("@EXP_L_PROGRAM_BGT", le_program_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_PROGRAM_ACT", le_program_act);
            cmd.Parameters.AddWithValue("@EXP_L_RENT_BTG", le_rent_btg);
            cmd.Parameters.AddWithValue("@EXP_L_RENT_ACT", le_rent_act);
            cmd.Parameters.AddWithValue("@EXP_L_SOUND_BGT", le_sound_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_SOUND_ACT", le_sound_act);
            cmd.Parameters.AddWithValue("@EXP_L_TICKET_PRINT_BGT", le_ticket_print_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_TICKET_PRINT_ACT", le_ticket_print_act);
            cmd.Parameters.AddWithValue("@EXP_L_PHONE_BGT", le_phone_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_PHONE_ACT", le_phone_act);
            cmd.Parameters.AddWithValue("@EXP_L_DRYICE_BGT", le_dryice_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_DRYICE_ACT", le_dryice_act);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER1_DESC", le_other1_desc);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER1_BGT", le_other1_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER1_ACT", le_other1_act);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER2_DESC", le_other2_desc);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER2_BGT", le_other2_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER2_ACT", le_other2_act);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER3_DESC", le_other3_desc);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER3_BGT", le_other3_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER3_ACT", le_other3_act);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER4_DESC", le_other4_desc);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER4_BGT", le_other4_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER4_ACT", le_other4_act);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER5_DESC", le_other5_desc);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER5_BGT", le_other5_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_OTHER5_ACT", le_other5_act);
            cmd.Parameters.AddWithValue("@EXP_L_LOCAL_FIXED_BGT", le_local_fixed_bgt);
            cmd.Parameters.AddWithValue("@EXP_L_LOCAL_FIXED_ACT", le_local_fixed_act);
            cmd.Parameters.AddWithValue("@EXP_SUBTOTAL_VAR_EXPENSES_BGT", subtotal_varexpenses_bgt);
            cmd.Parameters.AddWithValue("@EXP_SUBTOTAL_VAR_EXPENSES_ACT", subtotal_varexpenses_act);
            cmd.Parameters.AddWithValue("@EXP_SUBTOTAL_LOCAL_EXPENSES_BGT", subtotal_localexpenses_bgt);
            cmd.Parameters.AddWithValue("@EXP_SUBTOTAL_LOCAL_EXPENSES_ACT", subtotal_localexpenses_act);

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

        public DataTable Get_Expense_details(Int32 engagementid)
        {
            SqlCommand cmd = new SqlCommand("spGetExpenseDetails", dbconn);
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
    }
}