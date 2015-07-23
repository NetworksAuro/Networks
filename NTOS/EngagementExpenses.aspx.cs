using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ExpenseDataLayer;
using BoxOfficeLayer;
using System.Threading;
using System.Globalization;
using MasterDataLayer;
using EngagementDataLayer;
namespace NTOS
{
    public partial class EngagementExpenses : System.Web.UI.Page, MasterPageSaveInterface
    {
        Label lbl_msg;
        MasterData mdl = new MasterData();
        ExpenseData objexp = new ExpenseData();
        BoxOfficeData objbo = new BoxOfficeData();
        DataTable dt = new DataTable();
        EngagementData edl = new EngagementData();
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                ddlOtherfill();
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;

                Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencyGroupSeparator = ",";

                String Test = 123456789.ToString("C");
                hdn_expenseid.Value = "0";
               
            }
        }
        private void ResetFormControlValues(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c.Controls.Count > 0)
                {

                    ResetFormControlValues(c);
                }
                else
                {
                    switch (c.GetType().ToString())
                    {
                        case "System.Web.UI.WebControls.TextBox":
                            ((TextBox)c).Text = "";
                            break;
                        case "System.Web.UI.WebControls.CheckBox":
                            ((CheckBox)c).Checked = false;
                            break;
                        case "System.Web.UI.WebControls.RadioButton":
                            ((RadioButton)c).Checked = false;
                            break;
                        case "System.Web.UI.WebControls.DropDownList":
                            ((DropDownList)c).SelectedIndex = 0;
                            break;

                    }
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            int engagementid = Convert.ToInt32(Request.QueryString["engmtid"]);
            Session["search_engmt"] = engagementid.ToString();
            string schcount = Request.QueryString["schcount"];
            (this.Master as EngagementMaster).SetActiveTab("liexp");
            //((ImageButton)this.Master.FindControl("imgbtnex")).ImageUrl = "~/Images/tabb-ex.png";
            hdn_engagementid.Value = engagementid.ToString();
            hdn_schedulecount.Value = schcount;
            if (engagementid > 0)
            {
                dt = objexp.Get_Expense_details(Convert.ToInt32(hdn_engagementid.Value));
                if (dt.Rows.Count > 0)
                {
                   
                    hdn_expenseid.Value = dt.Rows[0]["EXP_ID"].ToString();
                }
            }
            ValidationSummary mastersummary = (ValidationSummary)this.Master.FindControl("val_summarymaster");
            mastersummary.Enabled = false;
            lbl_msg = (Label)this.Master.FindControl("lbl_message");
            lbl_msg.Text = "";
            (this.Master as EngagementMaster).hidesummary();
            if (!Page.IsPostBack)
            {

                DataTable dtlookup = mdl.GetLookupList("");
                if (dtlookup.Rows.Count > 0)
                {
                    DataTable dtexpense = dtlookup.Select("lkup_group = 'expensestatus'").CopyToDataTable();

                    drp_expense.DataSource = dtexpense;
                    drp_expense.DataTextField = "lkup_desc";
                    drp_expense.DataValueField = "lkup_desc";
                    drp_expense.DataBind();
                }
                drp_expense.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
                if (engagementid > 0)
                {
                    lbl_ex.Visible = false;
                    div_ex.Visible = true;
                    loadexpensedetails();
                    //txtinsurance_act.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtinsurance_bud.Attributes.Add("ReadOnly", "ReadOnly");
                    //txtticketprint_de_act.Attributes.Add("ReadOnly", "ReadOnly");
                    lblsubtotal_de_bud.Attributes.Add("ReadOnly", "ReadOnly");
                    lblsubtotal_de_act.Attributes.Add("ReadOnly", "ReadOnly");
                    lblsubtotal_le_bud.Attributes.Add("ReadOnly", "ReadOnly");
                    lblsubtotal_le_act.Attributes.Add("ReadOnly", "ReadOnly");
                    lbltotal_eng_bud.Attributes.Add("ReadOnly", "ReadOnly");
                    lbltotal_eng_act.Attributes.Add("ReadOnly", "ReadOnly");
                    if (hdn_engagementid.Value != "0" && hdn_engagementid.Value != "")
                    {
                        GetPerformanctCount();
                    }
                }
               
            }



        }

        public void ddlOtherfill()
        {
            DataTable dtDocumentOthers = new DataTable();
            //DataTable dtLocalOthers = new DataTable();
            dtDocumentOthers = mdl.GetLookupList("localdocumentedothers");
            //dtLocalOthers = mdl.GetLookupList("LocalOthers");
            ddlDocumentother1.DataSource = dtDocumentOthers;
            ddlDocumentother1.DataTextField = "lkup_desc";
            ddlDocumentother1.DataValueField = "lkup_desc";
            ddlDocumentother1.DataBind();
            ddlDocumentother1.Items.Insert(0, "--Select--");

            ddlDocumentOther2.DataSource = dtDocumentOthers;
            ddlDocumentOther2.DataTextField = "lkup_desc";
            ddlDocumentOther2.DataValueField = "lkup_desc";
            ddlDocumentOther2.DataBind();
            ddlDocumentOther2.Items.Insert(0, "--Select--");

            ddlLocalOther1.DataSource = dtDocumentOthers;
            ddlLocalOther1.DataTextField = "lkup_desc";
            ddlLocalOther1.DataValueField = "lkup_desc";
            ddlLocalOther1.DataBind();
            ddlLocalOther1.Items.Insert(0, "--Select--");

            ddlLocalOther2.DataSource = dtDocumentOthers;
            ddlLocalOther2.DataTextField = "lkup_desc";
            ddlLocalOther2.DataValueField = "lkup_desc";
            ddlLocalOther2.DataBind();
            ddlLocalOther2.Items.Insert(0, "--Select--");

            ddlLocalOther3.DataSource = dtDocumentOthers;
            ddlLocalOther3.DataTextField = "lkup_desc";
            ddlLocalOther3.DataValueField = "lkup_desc";
            ddlLocalOther3.DataBind();
            ddlLocalOther3.Items.Insert(0, "--Select--");

            ddlLocalOther4.DataSource = dtDocumentOthers;
            ddlLocalOther4.DataTextField = "lkup_desc";
            ddlLocalOther4.DataValueField = "lkup_desc";
            ddlLocalOther4.DataBind();
            ddlLocalOther4.Items.Insert(0, "--Select--");

            ddlLocalOther5.DataSource = dtDocumentOthers;
            ddlLocalOther5.DataTextField = "lkup_desc";
            ddlLocalOther5.DataValueField = "lkup_desc";
            ddlLocalOther5.DataBind();
            ddlLocalOther5.Items.Insert(0, "--Select--");
        }
        public void loadexpensedetails()
        {
            dt = objexp.Get_Expense_details(Convert.ToInt32(hdn_engagementid.Value));
            if (dt.Rows.Count > 0)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
                //Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencyGroupSeparator = ",";
                String Test = 123456789.ToString("C");

                hdn_expenseid.Value = dt.Rows[0]["EXP_ID"].ToString();
                txtadvt_bud.Text = (dt.Rows[0]["exp_d_ad_gross_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_ad_gross_bgt"]).ToString() : "";
                txtadvt_act.Text = (dt.Rows[0]["exp_d_ad_gross_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_ad_gross_act"]).ToString("C") : "";
                txtstatehandin_bud.Text = (dt.Rows[0]["exp_d_stghand_loadin_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_stghand_loadin_bgt"]).ToString("C") : "";
                txtstatehandin_act.Text = (dt.Rows[0]["exp_d_stghand_loadin_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_stghand_loadin_act"]).ToString("C") : "";
                txtstatehandout_bud.Text = (dt.Rows[0]["exp_d_stghand_loadout_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_stghand_loadout_bgt"]).ToString("C") : "";
                txtstatehandout_act.Text = (dt.Rows[0]["exp_d_stghand_loadout_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_stghand_loadout_act"]).ToString("C") : "";
                txtstatehandsrun_bud.Text = (dt.Rows[0]["exp_d_stghand_running_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_stghand_running_bgt"]).ToString("C") : "";
                txtstatehandsrun_act.Text = (dt.Rows[0]["exp_d_stghand_running_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_stghand_running_act"]).ToString("C") : "";
                txtwardrobehairin_bud.Text = (dt.Rows[0]["exp_d_wardrobe_loadin_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_wardrobe_loadin_bgt"]).ToString("C") : "";
                txtwardrobehairin_act.Text = (dt.Rows[0]["exp_d_wardrobe_loadin_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_wardrobe_loadin_act"]).ToString("C") : "";
                txtwardrobehairout_bud.Text = (dt.Rows[0]["exp_d_wardrobe_loadout_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_wardrobe_loadout_bgt"]).ToString("C") : "";
                txtwardrobehairout_act.Text = (dt.Rows[0]["exp_d_wardrobe_loadout_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_wardrobe_loadout_act"]).ToString("C") : "";
                txtwardrobehairrun_bud.Text = (dt.Rows[0]["exp_d_wardrobe_running_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_wardrobe_running_bgt"]).ToString("C") : "";
                txtwardrobehairrun_act.Text = (dt.Rows[0]["exp_d_wardrobe_running_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_wardrobe_running_act"]).ToString("C") : "";
                txtlabourcatering_bud.Text = (dt.Rows[0]["exp_d_labor_catering_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_labor_catering_bgt"]).ToString("C") : "";
                txtlabourcatering_act.Text = (dt.Rows[0]["exp_d_labor_catering_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_labor_catering_act"]).ToString("C") : "";
                txtmusicians_bud.Text = (dt.Rows[0]["exp_d_musician_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_musician_bgt"]).ToString("C") : "";
                txtmusicians_act.Text = (dt.Rows[0]["exp_d_musician_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_musician_act"]).ToString("C") : "";
                txtinsurnace_de_unit.Text = (dt.Rows[0]["exp_d_insurance_per_unit"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_insurance_per_unit"]).ToString("C") : "";
              

                txtinsurance_bud.Text = (dt.Rows[0]["exp_d_insurance_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_insurance_bgt"]).ToString() : "";
                txtinsurance_act.Text = (dt.Rows[0]["exp_d_insurance_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_insurance_act"]).ToString() : "";

                txtticketprint_de_unit.Text = (dt.Rows[0]["exp_d_ticket_print_per_unit"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_ticket_print_per_unit"]).ToString("C") : "";
                txtticketprint_de_bud.Text = (dt.Rows[0]["exp_d_ticket_print_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_ticket_print_bgt"]).ToString("") : "";
                txtticketprint_de_act.Text = (dt.Rows[0]["exp_d_ticket_print_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_ticket_print_act"]).ToString("") : "";
                ddlDocumentother1.SelectedIndex = ddlDocumentother1.Items.IndexOf(ddlDocumentother1.Items.FindByText(dt.Rows[0]["exp_d_other_1_desc"].ToString()));
                txtother1_de_bud.Text = (dt.Rows[0]["exp_d_other_1_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_other_1_bgt"]).ToString("C") : "";
                txtother1_de_act.Text = (dt.Rows[0]["exp_d_other_1_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_other_1_act"]).ToString("C") : "";
              
                ddlDocumentOther2.SelectedIndex = ddlDocumentOther2.Items.IndexOf(ddlDocumentOther2.Items.FindByText(dt.Rows[0]["exp_d_other_2_desc"].ToString()));
                txtother2_de_bud.Text = (dt.Rows[0]["exp_d_other_2_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_other_2_bgt"]).ToString("C") : "";
                txtother2_de_act.Text = (dt.Rows[0]["exp_d_other_2_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_d_other_2_act"]).ToString("C") : "";
                txtadaexp_bud.Text = (dt.Rows[0]["exp_l_ada_expense_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_ada_expense_bgt"]).ToString("C") : "";
                txtadaexp_act.Text = (dt.Rows[0]["exp_l_ada_expense_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_ada_expense_act"]).ToString("C") : "";
                txtboxoff_bud.Text = (dt.Rows[0]["exp_l_bo_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_bo_bgt"]).ToString("C") : "";
                txtboxoff_act.Text = (dt.Rows[0]["exp_l_bo_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_bo_act"]).ToString("C") : "";
                txtcatering_bud.Text = (dt.Rows[0]["exp_l_catering_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_catering_bgt"]).ToString("C") : "";
                txtcatering_act.Text = (dt.Rows[0]["exp_l_catering_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_catering_act"]).ToString("C") : "";
                txteqiprental_bud.Text = (dt.Rows[0]["exp_l_equip_rental_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_equip_rental_bgt"]).ToString("C") : "";
                txteqiprental_act.Text = (dt.Rows[0]["exp_l_equip_rental_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_equip_rental_act"]).ToString("C") : "";
                txtgrpsaleexp_bud.Text = (dt.Rows[0]["exp_l_grp_sales_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_grp_sales_bgt"]).ToString("C") : "";
                txtgrpsaleexp_act.Text = (dt.Rows[0]["exp_l_grp_sales_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_grp_sales_act"]).ToString("C") : "";
                txthousestaff_bud.Text = (dt.Rows[0]["exp_l_house_staff_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_house_staff_bgt"]).ToString("C") : "";
                txthousestaff_act.Text = (dt.Rows[0]["exp_l_house_staff_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_house_staff_act"]).ToString("C") : "";
                txtleaguefee_bud.Text = (dt.Rows[0]["exp_l_league_fee_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_league_fee_bgt"]).ToString("C") : "";
                txtleaguefee_act.Text = (dt.Rows[0]["exp_l_league_fee_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_league_fee_act"]).ToString("C") : "";
                txtlicpermits_bud.Text = (dt.Rows[0]["exp_l_license_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_license_bgt"]).ToString("C") : "";
                txtlicpermits_act.Text = (dt.Rows[0]["exp_l_license_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_license_act"]).ToString("C") : "";
                txtlimosauto_bud.Text = (dt.Rows[0]["exp_l_limo_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_limo_bgt"]).ToString("C") : "";
                txtlimosauto_act.Text = (dt.Rows[0]["exp_l_limo_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_limo_act"]).ToString("C") : "";
                txtorchestrashellrml_bud.Text = (dt.Rows[0]["exp_l_orchestra_sh_remove_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_orchestra_sh_remove_bgt"]).ToString("C") : "";
                txtorchestrashellrml_act.Text = (dt.Rows[0]["exp_l_orchestra_sh_remove_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_orchestra_sh_remove_act"]).ToString("C") : "";
                txtpresenterprofit_bud.Text = (dt.Rows[0]["exp_l_presenter_profit_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_presenter_profit_bgt"]).ToString("C") : "";
                txtpresenterprofit_act.Text = (dt.Rows[0]["exp_l_presenter_profit_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_presenter_profit_act"]).ToString("C") : "";
                txtpol_sec_fire_mar_bud.Text = (dt.Rows[0]["exp_l_police_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_police_bgt"]).ToString("C") : "";
                txtpol_sec_fire_mar_act.Text = (dt.Rows[0]["exp_l_police_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_police_act"]).ToString("C") : "";
                txtprogram_bud.Text = (dt.Rows[0]["exp_l_program_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_program_bgt"]).ToString("C") : "";
                txtprogram_act.Text = (dt.Rows[0]["exp_l_program_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_program_act"]).ToString("C") : "";
                txtrent_bud.Text = (dt.Rows[0]["exp_l_rent_btg"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_rent_btg"]).ToString("C") : "";
                txtrent_act.Text = (dt.Rows[0]["exp_l_rent_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_rent_act"]).ToString("C") : "";
                txtsoundlignt_bud.Text = (dt.Rows[0]["exp_l_sound_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_sound_bgt"]).ToString("C") : "";
                txtsoundlignt_act.Text = (dt.Rows[0]["exp_l_sound_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_sound_act"]).ToString("C") : "";
                txtticketprint_le_bud.Text = (dt.Rows[0]["exp_l_ticket_print_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_ticket_print_bgt"]).ToString("C") : "";
                txtticketprint_le_act.Text = (dt.Rows[0]["exp_l_ticket_print_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_ticket_print_act"]).ToString("C") : "";
                txttel_internet_bud.Text = (dt.Rows[0]["exp_l_phone_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_phone_bgt"]).ToString("C") : "";
                txttel_internet_act.Text = (dt.Rows[0]["exp_l_phone_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_phone_act"]).ToString("C") : "";
                txtdryice_bud.Text = (dt.Rows[0]["exp_l_dryice_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_dryice_bgt"]).ToString("C") : "";
                txtdryice_act.Text = (dt.Rows[0]["exp_l_dryice_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_dryice_act"]).ToString("C") : "";
                ddlLocalOther1.SelectedIndex = ddlLocalOther1.Items.IndexOf(ddlLocalOther1.Items.FindByText(dt.Rows[0]["exp_l_other1_desc"].ToString()));
                txtother1_le_bud.Text = (dt.Rows[0]["exp_l_other1_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_other1_bgt"]).ToString("C") : "";
                txtother1_le_act.Text = (dt.Rows[0]["exp_l_other1_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_other1_act"]).ToString("C") : "";
                ddlLocalOther2.SelectedIndex = ddlLocalOther2.Items.IndexOf(ddlLocalOther2.Items.FindByText(dt.Rows[0]["exp_l_other2_desc"].ToString()));
                txtother2_le_bud.Text = (dt.Rows[0]["exp_l_other2_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_other2_bgt"]).ToString("C") : "";
                txtother2_le_act.Text = (dt.Rows[0]["exp_l_other2_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_other2_act"]).ToString("C") : "";
                ddlLocalOther3.SelectedIndex = ddlLocalOther3.Items.IndexOf(ddlLocalOther3.Items.FindByText(dt.Rows[0]["exp_l_other3_desc"].ToString()));
                txtother3_le_bud.Text = (dt.Rows[0]["exp_l_other3_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_other3_bgt"]).ToString("C") : "";
                txtother3_le_act.Text = (dt.Rows[0]["exp_l_other3_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_other3_act"]).ToString("C") : "";
                ddlLocalOther4.SelectedIndex = ddlLocalOther4.Items.IndexOf(ddlLocalOther4.Items.FindByText(dt.Rows[0]["exp_l_other4_desc"].ToString()));
                txtother4_le_bud.Text = (dt.Rows[0]["exp_l_other4_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_other4_bgt"]).ToString("C") : "";
                txtother4_le_act.Text = (dt.Rows[0]["exp_l_other4_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_other4_act"]).ToString("C") : "";
                ddlLocalOther5.SelectedIndex = ddlLocalOther5.Items.IndexOf(ddlLocalOther5.Items.FindByText(dt.Rows[0]["exp_l_other5_desc"].ToString()));
                txtother5_le_bud.Text = (dt.Rows[0]["exp_l_other5_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_other5_bgt"]).ToString("C") : "";
                txtother5_le_act.Text = (dt.Rows[0]["exp_l_other5_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_other5_act"]).ToString("C") : "";
                txtlocalfixed_bud.Text = (dt.Rows[0]["exp_l_local_fixed_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_local_fixed_bgt"]).ToString("C") : "";
                txtlocalfixed_act.Text = (dt.Rows[0]["exp_l_local_fixed_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_l_local_fixed_act"]).ToString("C") : "";
                //lblsubtotal_de_bud.Text = (dt.Rows[0]["exp_subtotal_var_expenses_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_subtotal_var_expenses_bgt"]).ToString("C") : "";
                //lblsubtotal_de_act.Text = (dt.Rows[0]["exp_subtotal_var_expenses_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_subtotal_var_expenses_act"]).ToString("C") : "";
                //lblsubtotal_le_bud.Text = (dt.Rows[0]["exp_subtotal_local_expenses_bgt"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_subtotal_local_expenses_bgt"]).ToString("C") : "";
                //lblsubtotal_le_act.Text = (dt.Rows[0]["exp_subtotal_local_expenses_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["exp_subtotal_local_expenses_act"]).ToString("C") : "";
                //lbltotal_eng_bud.Text = (dt.Rows[0]["engag_gtotal_bud"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["engag_gtotal_bud"]).ToString("C") : "";
                //lbltotal_eng_act.Text = (dt.Rows[0]["engag_gtotal_act"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[0]["engag_gtotal_act"]).ToString("C") : "";
                DataTable engagementtable;
                engagementtable = edl.GetEngagementDetailsById(Convert.ToInt32(hdn_engagementid.Value));
                if (engagementtable.Rows.Count > 0)
                {
                    drp_expense.SelectedValue = engagementtable.Rows[0]["engt_expenses"].ToString();

                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "cal_total();", true);
            }
        }
        public void GetPerformanctCount()
        {
            dt = objbo.GetPerformancelist(Convert.ToInt32(hdn_engagementid.Value));
            hdnnoofshows.Value = "0";
            if (dt.Rows.Count > 0)
            {
                hdn_bodropcount.Value = dt.Rows[0]["BO_DROP_COUNT"].ToString();
                hdn_pricescale_cap.Value = dt.Rows[0]["PS_SEATS_SINGLE"].ToString();
                hdn_attendance.Value = dt.Rows[0]["BO_PAID_ATTENDANCE"].ToString();
                hdnnoofshows.Value = dt.Rows[0]["BORecCount"].ToString();
            }
        }
        public void SaveData()
        {            
            EngagementMaster EngmtMaster = (EngagementMaster)Page.Master;

            if (hdn_engagementid.Value != "0" && hdn_engagementid.Value != "")
            {
                //if (hdn_schedulecount.Value != "0" && hdn_schedulecount.Value != "")
                //{
                    Int32 engagementid = Convert.ToInt32(hdn_engagementid.Value);
                    Nullable<decimal> de_ad_gross_act = null, de_ad_gross_bgt = null, de_insurance_act = null, de_insurance_bgt = null;
                    Nullable<decimal> de_insurance_per_unit = null, de_labor_catering_act = null, de_labor_catering_bgt = null;
                    Nullable<decimal> de_musician_act = null, de_musician_bgt = null, de_other_1_act = null, de_other_1_bgt = null;
                    Nullable<decimal> de_other_2_act = null, de_other_2_bgt = null, de_stghand_loadin_act = null;
                    Nullable<decimal> de_stghand_loadin_bgt = null, de_stghand_loadout_act = null, de_stghand_loadout_bgt = null;
                    Nullable<decimal> de_stghand_running_act = null, de_stghand_running_bgt = null, de_ticket_print_act = null;
                    Nullable<decimal> de_ticket_print_bgt = null, de_ticket_print_per_unit = null, de_wardrobe_loadin_act = null;
                    Nullable<decimal> de_wardrobe_loadin_bgt = null, de_wardrobe_loadout_act = null, de_wardrobe_loadout_bgt = null;
                    Nullable<decimal> de_wardrobe_running_act = null, de_wardrobe_running_bgt = null, le_adaexpense_act = null;
                    Nullable<decimal> le_adaexpense_bgt = null, le_bo_act = null, le_bo_bgt = null, le_catering_act = null;
                    Nullable<decimal> le_catering_bgt = null, le_dryice_act = null, le_dryice_bgt = null, le_equip_rental_act = null;
                    Nullable<decimal> le_equip_rental_bgt = null, le_grp_sales_act = null, le_grp_sales_bgt = null;
                    Nullable<decimal> le_house_staff_act = null, le_house_staff_bgt = null, le_league_fee_act = null;
                    Nullable<decimal> le_league_fee_bgt = null, le_license_act = null, le_license_bgt = null, le_limo_act = null;
                    Nullable<decimal> le_limo_bgt = null, le_local_fixed_act = null, le_local_fixed_bgt = null;
                    Nullable<decimal> le_orchestra_sh_remove_act = null, le_orchestra_sh_remove_bgt = null, le_other1_act = null, le_other1_bgt = null;
                    Nullable<decimal> le_other2_act = null, le_other2_bgt = null, le_other3_act = null, le_other3_bgt = null;
                    Nullable<decimal> le_other4_act = null, le_other4_bgt = null, le_other5_act = null, le_other5_bgt = null;
                    Nullable<decimal> le_phone_act = null, le_phone_bgt = null, le_police_act = null, le_police_bgt = null;
                    Nullable<decimal> le_presenter_profit_act = null, le_presenter_profit_bgt = null, le_program_act = null;
                    Nullable<decimal> le_program_bgt = null, le_rent_act = null, le_rent_btg = null, le_sound_act = null;
                    Nullable<decimal> le_sound_bgt = null, le_ticket_print_act = null, le_ticket_print_bgt = null, subtotal_localexpenses_act = null;
                    Nullable<decimal> subtotal_localexpenses_bgt = null, subtotal_varexpenses_act = null, subtotal_varexpenses_bgt = null;
                    string de_other_1_desc, de_other_2_desc, le_other1_desc, le_other2_desc, le_other3_desc, le_other4_desc, le_other5_desc;
                    char[] chDlr = { '$', ',', ' ' };
                    #region documentexp
                    de_ad_gross_act = txtadvt_act.Text.AutoformatDecimal();
                    de_ad_gross_bgt = txtadvt_bud.Text.AutoformatDecimal();
                    de_stghand_loadin_bgt = txtstatehandin_bud.Text.AutoformatDecimal();
                    de_stghand_loadin_act = txtstatehandin_act.Text.AutoformatDecimal();
                    de_stghand_loadout_bgt = txtstatehandout_bud.Text.AutoformatDecimal();
                    de_stghand_loadout_act = txtstatehandout_act.Text.AutoformatDecimal();
                    de_stghand_running_bgt = txtstatehandsrun_bud.Text.AutoformatDecimal();
                    de_stghand_running_act = txtstatehandsrun_act.Text.AutoformatDecimal();
                    de_wardrobe_loadin_bgt = txtwardrobehairin_bud.Text.AutoformatDecimal();
                    de_wardrobe_loadin_act = txtwardrobehairin_act.Text.AutoformatDecimal();
                    de_wardrobe_loadout_bgt = txtwardrobehairout_bud.Text.AutoformatDecimal();
                    de_wardrobe_loadout_act = txtwardrobehairout_act.Text.AutoformatDecimal();
                    de_wardrobe_running_bgt = txtwardrobehairrun_bud.Text.AutoformatDecimal();
                    de_wardrobe_running_act = txtwardrobehairrun_act.Text.AutoformatDecimal();
                    de_labor_catering_bgt = txtlabourcatering_bud.Text.AutoformatDecimal();
                    de_labor_catering_act = txtlabourcatering_act.Text.AutoformatDecimal();
                    de_musician_bgt = txtmusicians_bud.Text.AutoformatDecimal();
                    de_musician_act = txtmusicians_act.Text.AutoformatDecimal();
                    de_insurance_per_unit = txtinsurnace_de_unit.Text.AutoformatDecimal();
                    de_insurance_bgt = txtinsurance_bud.Text.AutoformatDecimal();
                    de_insurance_act = txtinsurance_act.Text.AutoformatDecimal();
                    de_ticket_print_per_unit = txtticketprint_de_unit.Text.AutoformatDecimal();
                    de_ticket_print_bgt = txtticketprint_de_bud.Text.AutoformatDecimal();
                    de_ticket_print_act = txtticketprint_de_act.Text.AutoformatDecimal();
                    de_other_1_bgt = txtother1_de_bud.Text.AutoformatDecimal();
                    de_other_1_act = txtother1_de_act.Text.AutoformatDecimal();
                    de_other_2_bgt = txtother2_de_bud.Text.AutoformatDecimal();
                    de_other_2_act = txtother2_de_act.Text.AutoformatDecimal();
                    //subtotal_varexpenses_act = (lblsubtotal_de_act.Text != "") ? Convert.ToDecimal(lblsubtotal_de_act.Text.Trim(chDlr)) : subtotal_localexpenses_act;
                    //subtotal_varexpenses_bgt = (lblsubtotal_de_bud.Text != "") ? Convert.ToDecimal(lblsubtotal_de_bud.Text.Trim(chDlr)) : subtotal_localexpenses_bgt;
                    de_other_1_desc = ddlDocumentother1.SelectedIndex == 0 ? "" : ddlDocumentother1.SelectedItem.Text.Trim();
                    de_other_2_desc = ddlDocumentOther2.SelectedIndex == 0 ? "" : ddlDocumentOther2.SelectedItem.Text.Trim();
                    #endregion

                    #region localexp

                    le_adaexpense_bgt = txtadaexp_bud.Text.AutoformatDecimal();
                    le_adaexpense_act = txtadaexp_act.Text.AutoformatDecimal();
                    le_bo_bgt = txtboxoff_bud.Text.AutoformatDecimal();
                    le_bo_act = txtboxoff_act.Text.AutoformatDecimal();
                    le_catering_bgt = txtcatering_bud.Text.AutoformatDecimal();
                    le_catering_act = txtcatering_act.Text.AutoformatDecimal();
                    le_equip_rental_bgt = txteqiprental_bud.Text.AutoformatDecimal();
                    le_equip_rental_act = txteqiprental_act.Text.AutoformatDecimal();
                    le_grp_sales_bgt = txtgrpsaleexp_bud.Text.AutoformatDecimal();
                    le_grp_sales_act = txtgrpsaleexp_act.Text.AutoformatDecimal();
                    le_house_staff_bgt = txthousestaff_bud.Text.AutoformatDecimal();
                    le_house_staff_act = txthousestaff_act.Text.AutoformatDecimal();
                    le_league_fee_bgt = txtleaguefee_bud.Text.AutoformatDecimal();
                    le_league_fee_act = txtleaguefee_act.Text.AutoformatDecimal();
                    le_license_bgt = txtlicpermits_bud.Text.AutoformatDecimal();
                    le_license_act = txtlicpermits_act.Text.AutoformatDecimal();
                    le_limo_bgt = txtlimosauto_bud.Text.AutoformatDecimal();
                    le_limo_act = txtlimosauto_act.Text.AutoformatDecimal();
                    le_orchestra_sh_remove_bgt = txtorchestrashellrml_bud.Text.AutoformatDecimal();
                    le_orchestra_sh_remove_act = txtorchestrashellrml_act.Text.AutoformatDecimal();
                    le_presenter_profit_bgt = txtpresenterprofit_bud.Text.AutoformatDecimal();
                    le_presenter_profit_act = txtpresenterprofit_act.Text.AutoformatDecimal();
                    le_police_bgt = txtpol_sec_fire_mar_bud.Text.AutoformatDecimal();
                    le_police_act = txtpol_sec_fire_mar_act.Text.AutoformatDecimal();
                    le_program_bgt = txtprogram_bud.Text.AutoformatDecimal();
                    le_program_act = txtprogram_act.Text.AutoformatDecimal();
                    le_rent_btg = txtrent_bud.Text.AutoformatDecimal();
                    le_rent_act = txtrent_act.Text.AutoformatDecimal();
                    le_sound_bgt = txtsoundlignt_bud.Text.AutoformatDecimal();
                    le_sound_act = txtsoundlignt_act.Text.AutoformatDecimal();
                    le_ticket_print_bgt = txtticketprint_le_bud.Text.AutoformatDecimal();
                    le_ticket_print_act = txtticketprint_le_act.Text.AutoformatDecimal();
                    le_phone_bgt = txttel_internet_bud.Text.AutoformatDecimal();
                    le_phone_act = txttel_internet_act.Text.AutoformatDecimal();
                    le_dryice_bgt = txtdryice_bud.Text.AutoformatDecimal();
                    le_dryice_act = txtdryice_act.Text.AutoformatDecimal();
                    le_other1_bgt = txtother1_le_bud.Text.AutoformatDecimal();
                    le_other1_act = txtother1_le_act.Text.AutoformatDecimal();
                    le_other2_bgt = txtother2_le_bud.Text.AutoformatDecimal();
                    le_other2_act = txtother2_le_act.Text.AutoformatDecimal();
                    le_other3_bgt = txtother3_le_bud.Text.AutoformatDecimal();
                    le_other3_act = txtother3_le_act.Text.AutoformatDecimal();
                    le_other4_bgt = txtother4_le_bud.Text.AutoformatDecimal();
                    le_other4_act = txtother4_le_act.Text.AutoformatDecimal();
                    le_other5_bgt = txtother5_le_bud.Text.AutoformatDecimal();
                    le_other5_act = txtother5_le_act.Text.AutoformatDecimal();
                    le_local_fixed_bgt = txtlocalfixed_bud.Text.AutoformatDecimal();
                    le_local_fixed_act = txtlocalfixed_act.Text.AutoformatDecimal();
                    //subtotal_localexpenses_act = (lblsubtotal_le_act.Text != "") ? Convert.ToDecimal(lblsubtotal_le_act.Text.Trim(chDlr)) : subtotal_varexpenses_act;
                    //subtotal_localexpenses_bgt = (lblsubtotal_le_bud.Text != "") ? Convert.ToDecimal(lblsubtotal_le_bud.Text.Trim(chDlr)) : subtotal_varexpenses_bgt;
                    le_other1_desc = ddlLocalOther1.SelectedIndex == 0 ? "" : ddlLocalOther1.SelectedItem.Text.Trim();
                    le_other2_desc = ddlLocalOther2.SelectedIndex == 0 ? "" : ddlLocalOther2.SelectedItem.Text.Trim();
                    le_other3_desc = ddlLocalOther3.SelectedIndex == 0 ? "" : ddlLocalOther3.SelectedItem.Text.Trim();
                    le_other4_desc = ddlLocalOther4.SelectedIndex == 0 ? "" : ddlLocalOther4.SelectedItem.Text.Trim();
                    le_other5_desc = ddlLocalOther5.SelectedIndex == 0 ? "" : ddlLocalOther5.SelectedItem.Text.Trim();

                    #endregion
                    if (hdn_expenseid.Value == "0")
                    {
                        objexp.Engagement_Expense_Insert(engagementid, de_ad_gross_act, de_ad_gross_bgt, de_insurance_act, de_insurance_bgt, de_insurance_per_unit, de_labor_catering_act,
                            de_labor_catering_bgt, de_musician_act, de_musician_bgt, de_other_1_act, de_other_1_bgt, de_other_2_act, de_other_2_bgt, de_stghand_loadin_act,
                            de_stghand_loadin_bgt, de_stghand_loadout_act, de_stghand_loadout_bgt, de_stghand_running_act, de_stghand_running_bgt, de_ticket_print_act, de_ticket_print_bgt,
                            de_ticket_print_per_unit, de_wardrobe_loadin_act, de_wardrobe_loadin_bgt, de_wardrobe_loadout_act, de_wardrobe_loadout_bgt, de_wardrobe_running_act,
                            de_wardrobe_running_bgt, le_adaexpense_act, le_adaexpense_bgt, le_bo_act, le_bo_bgt, le_catering_act, le_catering_bgt, le_dryice_act, le_dryice_bgt,
                            le_equip_rental_act, le_equip_rental_bgt, le_grp_sales_act, le_grp_sales_bgt, le_house_staff_act, le_house_staff_bgt, le_league_fee_act, le_league_fee_bgt,
                            le_license_act, le_license_bgt, le_limo_act, le_limo_bgt, le_local_fixed_act, le_local_fixed_bgt, le_orchestra_sh_remove_act, le_orchestra_sh_remove_bgt,
                            le_other1_act, le_other1_bgt, le_other2_act, le_other2_bgt, le_other3_act, le_other3_bgt, le_other4_act, le_other4_bgt, le_other5_act, le_other5_bgt, le_phone_act,
                            le_phone_bgt, le_police_act, le_police_bgt, le_presenter_profit_act, le_presenter_profit_bgt, le_program_act, le_program_bgt, le_rent_act, le_rent_btg,
                            le_sound_act, le_sound_bgt, le_ticket_print_act, le_ticket_print_bgt, subtotal_localexpenses_act, subtotal_localexpenses_bgt, subtotal_varexpenses_act,
                            subtotal_varexpenses_bgt, de_other_1_desc, de_other_2_desc, le_other1_desc, le_other2_desc, le_other3_desc, le_other4_desc, le_other5_desc);
                        lbl_msg.Text = "Expense details submitted successfully!";
                        lbl_msg.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (hdn_expenseid.Value != "0")
                    {
                        Int32 expid = Convert.ToInt32(hdn_expenseid.Value);
                        objexp.Engagement_Expense_Update(expid, engagementid, de_ad_gross_act, de_ad_gross_bgt, de_insurance_act, de_insurance_bgt, de_insurance_per_unit, de_labor_catering_act,
    de_labor_catering_bgt, de_musician_act, de_musician_bgt, de_other_1_act, de_other_1_bgt, de_other_2_act, de_other_2_bgt, de_stghand_loadin_act,
    de_stghand_loadin_bgt, de_stghand_loadout_act, de_stghand_loadout_bgt, de_stghand_running_act, de_stghand_running_bgt, de_ticket_print_act, de_ticket_print_bgt,
    de_ticket_print_per_unit, de_wardrobe_loadin_act, de_wardrobe_loadin_bgt, de_wardrobe_loadout_act, de_wardrobe_loadout_bgt, de_wardrobe_running_act,
    de_wardrobe_running_bgt, le_adaexpense_act, le_adaexpense_bgt, le_bo_act, le_bo_bgt, le_catering_act, le_catering_bgt, le_dryice_act, le_dryice_bgt,
    le_equip_rental_act, le_equip_rental_bgt, le_grp_sales_act, le_grp_sales_bgt, le_house_staff_act, le_house_staff_bgt, le_league_fee_act, le_league_fee_bgt,
    le_license_act, le_license_bgt, le_limo_act, le_limo_bgt, le_local_fixed_act, le_local_fixed_bgt, le_orchestra_sh_remove_act, le_orchestra_sh_remove_bgt,
    le_other1_act, le_other1_bgt, le_other2_act, le_other2_bgt, le_other3_act, le_other3_bgt, le_other4_act, le_other4_bgt, le_other5_act, le_other5_bgt, le_phone_act,
    le_phone_bgt, le_police_act, le_police_bgt, le_presenter_profit_act, le_presenter_profit_bgt, le_program_act, le_program_bgt, le_rent_act, le_rent_btg,
    le_sound_act, le_sound_bgt, le_ticket_print_act, le_ticket_print_bgt, subtotal_localexpenses_act, subtotal_localexpenses_bgt, subtotal_varexpenses_act,
    subtotal_varexpenses_bgt, de_other_1_desc, de_other_2_desc, le_other1_desc, le_other2_desc, le_other3_desc, le_other4_desc, le_other5_desc);
                        lbl_msg.Text = "Expense details updated successfully!";
                        lbl_msg.ForeColor = System.Drawing.Color.Green;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "cal_total();", true);
                    //if (hdn_dealid.Value != "0")
                    //{
                    //    EngagementDeal1.SaveDealData("updatedeal", 0, 0, Convert.ToInt32(hdn_dealid.Value));
                    //    lbl_msg.Text = "Engagement Deal updated successfully";
                    //    lbl_msg.ForeColor = System.Drawing.Color.Green;
                    //}
                    //else
                    //{
                    //    EngagementDeal1.SaveDealData("createdeal", 0, Convert.ToInt32(hdn_engagementid.Value), 0);
                    //    lbl_msg.Text = "Engagement Deal created successfully";
                    //    lbl_msg.ForeColor = System.Drawing.Color.Green;
                    //}
                //}
                //else
                //{
                //    lbl_msg.Text = "Please create Engagement Schedule first";
                //    lbl_msg.ForeColor = System.Drawing.Color.Red;
                //}
                    mdl.Engmt_update(engagementid,"0", drp_expense.SelectedValue,"",0,"" ,"E");
                    
            }
        }
        public void DeleteData(string flag)
        {
            EngagementMaster EngmtMaster = (EngagementMaster)Page.Master;
            int delflag = EngmtMaster.DeleteEngagement(flag);
            lbl_msg.Text = "Engagement Deleted successfully";
            lbl_msg.ForeColor = System.Drawing.Color.Green;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            //clearall();
        }
        public void Reset()
        {
            clearall();
        }
        public void clearall()
        {
            txtadaexp_act.Text = "";
            txtadaexp_bud.Text = "";
            txtadvt_act.Text = "";
            txtadvt_bud.Text = "";
            txtboxoff_act.Text = "";
            txtboxoff_bud.Text = "";
            txtcatering_act.Text = "";
            txtcatering_bud.Text = "";
            txtdryice_act.Text = "";
            txtdryice_bud.Text = "";
            txteqiprental_act.Text = "";
            txteqiprental_bud.Text = "";
            txtgrpsaleexp_act.Text = "";
            txtgrpsaleexp_bud.Text = "";
            txthousestaff_act.Text = "";
            txthousestaff_bud.Text = "";
            txtinsurance_act.Text = "";
            txtinsurance_bud.Text = "";
            txtinsurnace_de_unit.Text = "";
            txtlabourcatering_act.Text = "";
            txtlabourcatering_bud.Text = "";
            txtleaguefee_act.Text = "";
            txtleaguefee_bud.Text = "";
            txtlicpermits_act.Text = "";
            txtlicpermits_bud.Text = "";
            txtlimosauto_act.Text = "";
            txtlimosauto_bud.Text = "";
            txtlocalfixed_act.Text = "";
            txtlocalfixed_bud.Text = "";
            txtmusicians_act.Text = "";
            txtmusicians_bud.Text = "";
            txtorchestrashellrml_act.Text = "";
            txtorchestrashellrml_bud.Text = "";
            txtother1_de_act.Text = "";
            txtother1_de_bud.Text = "";
            ddlDocumentother1.SelectedIndex = 0;
            txtother1_le_act.Text = "";
            txtother1_le_bud.Text = "";
            ddlLocalOther1.SelectedIndex = 0;
            txtother2_de_act.Text = "";
            txtother2_de_bud.Text = "";
            ddlDocumentOther2.SelectedIndex = 0;
            txtother2_le_act.Text = "";
            txtother2_le_bud.Text = "";
            ddlLocalOther2.SelectedIndex = 0;
            txtother3_le_act.Text = "";
            txtother3_le_bud.Text = "";
            ddlLocalOther3.SelectedIndex = 0;
            txtother4_le_act.Text = "";
            txtother4_le_bud.Text = "";
            ddlLocalOther4.SelectedIndex = 0;
            txtother5_le_act.Text = "";
            txtother5_le_bud.Text = "";
            ddlLocalOther5.SelectedIndex = 0;
            txtpol_sec_fire_mar_act.Text = "";
            txtpol_sec_fire_mar_bud.Text = "";
            txtpresenterprofit_act.Text = "";
            txtpresenterprofit_bud.Text = "";
            txtprogram_act.Text = "";
            txtprogram_bud.Text = "";
            txtrent_act.Text = "";
            txtrent_bud.Text = "";
            txtsoundlignt_act.Text = "";
            txtsoundlignt_bud.Text = "";
            txtstatehandin_act.Text = "";
            txtstatehandin_bud.Text = "";
            txtstatehandout_act.Text = "";
            txtstatehandout_bud.Text = "";
            txtstatehandsrun_act.Text = "";
            txtstatehandsrun_bud.Text = "";
            lblsubtotal_de_act.Text = "";
            lblsubtotal_de_bud.Text = "";
            lblsubtotal_le_act.Text = "";
            lblsubtotal_le_bud.Text = "";
            txttel_internet_act.Text = "";
            txttel_internet_bud.Text = "";
            txtticketprint_de_act.Text = "";
            txtticketprint_de_bud.Text = "";
            txtticketprint_de_unit.Text = "";
            txtticketprint_le_act.Text = "";
            txtticketprint_le_bud.Text = "";
            lbltotal_eng_act.Text = "";
            lbltotal_eng_bud.Text = "";
            txtwardrobehairin_act.Text = "";
            txtwardrobehairin_bud.Text = "";
            txtwardrobehairout_act.Text = "";
            txtwardrobehairout_bud.Text = "";
            txtwardrobehairrun_act.Text = "";
            txtwardrobehairrun_bud.Text = "";
        }
    }
}