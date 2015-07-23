using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DealDataLayer;
using MasterDataLayer;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using EngagementDataLayer;
namespace NTOS
{
    public partial class EngagementDeal1 : System.Web.UI.UserControl
    {
        DealData ddl = new DealData();
        MasterData mdl = new MasterData();
        DataTable dealtable = null;
        EngagementData edl = new EngagementData();
        protected void Page_Load(object sender, EventArgs e)
        {

            lbl_message.Text = "";
        //   drpfill();
            if (hdn_ucengagementid.Value != "0" && hdn_ucengagementid.Value != "")
            {
                if (hdn_ucengagementid.Value == "-1")
                {
                    if (!Page.IsPostBack)
                    {
                        drpfill();
                        lblcontract.Visible = false;
                        lbldealdemo.Visible = false;
                        lblExchangerate.Visible = false;

                        drp_contract.Visible = false;
                        drp_dealmemo.Visible = false;
                        txt_exchange.Visible = false;
                      

                        txt_tax.Text = "0.00";
                        txt_tax2.Text = "0.00";
                        txt_taxover.Text = "0.00";
                        txt_miscellaneous1.Text = txt_miscellaneous2.Text = "0.00";
                        txt_miscellaneous3.Text = txt_miscellaneous4.Text = "0.00";
                        txt_miscellaneous5.Text = "0.00";
                        txt_facilityfee.Text = "0.00";
                        txt_boxoffice.Text = "0.00";
                        ddlmisotherfill();
                        drp_show.DataSource = mdl.Getshows("0");
                        drp_show.DataTextField = "show_name";
                        drp_show.DataValueField = "show_id";
                        drp_show.DataBind();
                        drp_show.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
                        drp_dealtype.DataSource = ddl.BindDealTypeList(0);
                        drp_dealtype.DataTextField = "deal_tmpt_type";
                        drp_dealtype.DataValueField = "deal_tmpt_type";
                        drp_dealtype.DataBind();
                        drp_dealtype.Items.Insert(0, new ListItem { Value = "0", Text = "--Select OR Enter--", Selected = true });
                        txt_createdate.Text = DateTime.Now.ToShortDateString();
                        drp_updatedate.Items.Insert(0, new ListItem { Value = DateTime.Now.ToShortDateString(), Text = DateTime.Now.ToShortDateString(), Selected = true });

                        try
                        {
                            dealtable = ddl.GetDealTemplateDetailsById(Convert.ToInt32(hdn_ucdealid.Value));
                            if (dealtable.Rows.Count > 0)
                            {
                                drp_show.SelectedValue = dealtable.Rows[0]["deal_tmpt_show_id"].ToString();
                                drp_dealtype.SelectedValue = dealtable.Rows[0]["deal_tmpt_type"].ToString();
                                txt_createdate.Text = Convert.ToDateTime(dealtable.Rows[0]["deal_tmpt_cr_date"]).ToShortDateString();
                                drp_updatedate.SelectedItem.Text = Convert.ToDateTime(dealtable.Rows[0]["deal_tmpt_upd_date"]).ToShortDateString();
                                drp_updatedate.SelectedItem.Value = Convert.ToDateTime(dealtable.Rows[0]["deal_tmpt_upd_date"]).ToShortDateString();
                                BindDealTemplateFieldData();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showhide_ff();", true);
                            }
                            if (!string.IsNullOrEmpty(Request.QueryString["showidnew"]))
                            {
                                drp_show.SelectedIndex = drp_show.Items.IndexOf(drp_show.Items.FindByValue(Request.QueryString["showidnew"]));
                                drp_show.Enabled = false;
                            }
                            // ((HiddenField)this.Page.Master.FindControl("hdnreqlist")).Value = (drp_show.ClientID + "," + drp_updatedate.ClientID + "," + txt_createdate.ClientID);
                        }
                        catch (Exception ex)
                        {
                            lbl_message.Text = "Error: " + ex.Message.ToString();
                            lbl_message.ForeColor = System.Drawing.Color.Red;
                        }
                    }


                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        drpfill();
                        lbl_show.Visible = false;
                        drp_show.Visible = false;
                        lbl_createdate.Visible = false;
                        txt_createdate.Visible = false;
                        hdfdealdate.Visible = false;

                      
                        //imgbtncalendar.Visible = false;
                        //drp_updatedate.DropDownStyle = AjaxControlToolkit.ComboBoxStyle.DropDownList;
                        ddlmisotherfill();
                        drp_updatedate.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });

                        drp_dealtype.DropDownStyle = AjaxControlToolkit.ComboBoxStyle.DropDownList;
                        if (hdn_ucshowid.Value != "0" && hdn_ucshowid.Value != "")
                        {
                            drp_dealtype.DataSource = ddl.BindDealTypeList(Convert.ToInt16(hdn_ucshowid.Value));
                            drp_dealtype.DataTextField = "deal_tmpt_type";
                            drp_dealtype.DataValueField = "deal_tmpt_type";
                            drp_dealtype.DataBind();
                        }
                        drp_dealtype.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
                        try
                        {
                            dealtable = ddl.GetDealDetails(Convert.ToInt32(hdn_ucengagementid.Value));
                            if (dealtable.Rows.Count > 0)
                            {
                                drp_dealtype.SelectedValue = dealtable.Rows[0]["deal_type"].ToString();
                                hdn_ucdealid.Value = dealtable.Rows[0]["deal_id"].ToString();

                                drp_updatedate.DataSource = ddl.GetTemplateRevisionDates(Convert.ToInt16(hdn_ucshowid.Value), drp_dealtype.SelectedValue);
                                drp_updatedate.DataTextField = "deal_tmpt_upd_date";
                                drp_updatedate.DataValueField = "deal_tmpt_upd_date";
                                drp_updatedate.DataBind();
                                drp_updatedate.SelectedValue = dealtable.Rows[0]["deal_type_upd_date"].ToString();

                                BindDealFieldData();

                                DataTable engagementtable;
                                engagementtable = edl.GetEngagementDetailsById(Convert.ToInt32(hdn_ucengagementid.Value));
                                if (engagementtable.Rows.Count > 0)
                                {
                                    //SetButtonVisible(engagementtable.Rows[0]["engt_active_flag"].ToString().ToLower());
                                    //BindEngagementFieldData();

                                    drp_dealmemo.SelectedValue = engagementtable.Rows[0]["engt_deal_memo"].ToString();
                                    drp_contract.SelectedValue = engagementtable.Rows[0]["engt_contract"].ToString();
                                    if (engagementtable.Rows[0]["engt_exchange_rate"] != DBNull.Value)
                                        txt_exchange.Text = engagementtable.Rows[0]["engt_exchange_rate"].ToString();

                                                                   }


                                // ddltaxptg_ff.Visible = false;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showhide_ff();", true);
                            }
                            else
                            {
                                hdn_ucdealid.Value = "0";
                            }
                        }
                        catch (Exception ex)
                        {
                            lbl_message.Text = "Error: " + ex.Message.ToString();
                            lbl_message.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                 

                }




            }
        }
        public void drpfill()
        {

            DataTable dtlookup = mdl.GetLookupList("");
            if (dtlookup.Rows.Count > 0)
            {
                DataTable dtcontract = dtlookup.Select("lkup_group = 'contractstatus'").CopyToDataTable();

                drp_contract.DataSource = dtcontract;
                drp_contract.DataTextField = "lkup_desc";
                drp_contract.DataValueField = "lkup_desc";
                drp_contract.DataBind();
            }
            drp_contract.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
            if (dtlookup.Rows.Count > 0)
            {
                DataTable dtdealmemo = dtlookup.Select("lkup_group = 'memostatus'").CopyToDataTable();

                drp_dealmemo.DataSource = dtdealmemo;
                drp_dealmemo.DataTextField = "lkup_desc";
                drp_dealmemo.DataValueField = "lkup_desc";
                drp_dealmemo.DataBind();
            }
            drp_dealmemo.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
        }
        public void ddlmisotherfill()
        {
            DataTable dtmisother = new DataTable();
            dtmisother = mdl.GetLookupList("DealOthers");
            ddlMisOthers1.DataSource = dtmisother;
            ddlMisOthers1.DataTextField = "lkup_desc";
            ddlMisOthers1.DataValueField = "lkup_desc";
            ddlMisOthers1.DataBind();
            ddlMisOthers1.Items.Insert(0, "--Select--");

            ddlMisOthers2.DataSource = dtmisother;
            ddlMisOthers2.DataTextField = "lkup_desc";
            ddlMisOthers2.DataValueField = "lkup_desc";
            ddlMisOthers2.DataBind();
            ddlMisOthers2.Items.Insert(0, "--Select--");


            ddlMisOthers3.DataSource = dtmisother;
            ddlMisOthers3.DataTextField = "lkup_desc";
            ddlMisOthers3.DataValueField = "lkup_desc";
            ddlMisOthers3.DataBind();
            ddlMisOthers3.Items.Insert(0, "--Select--");

            ddlMisOthers4.DataSource = dtmisother;
            ddlMisOthers4.DataTextField = "lkup_desc";
            ddlMisOthers4.DataValueField = "lkup_desc";
            ddlMisOthers4.DataBind();
            ddlMisOthers4.Items.Insert(0, "--Select--");


            ddlMisOthers5.DataSource = dtmisother;
            ddlMisOthers5.DataTextField = "lkup_desc";
            ddlMisOthers5.DataValueField = "lkup_desc";
            ddlMisOthers5.DataBind();
            ddlMisOthers5.Items.Insert(0, "--Select--");

        }
        public void BindDetails()
        {
            if (hdn_ucengagementid.Value == "-1")
            {
                dealtable = ddl.GetDealTemplateDetails(Convert.ToInt16(drp_show.SelectedValue), drp_dealtype.SelectedValue, Convert.ToDateTime(drp_updatedate.SelectedValue));
                hdn_ucdealid.Value = dealtable.Rows[0]["deal_template_id"].ToString();
            }
            else
            {
                dealtable = ddl.GetDealTemplateDetails(Convert.ToInt16(hdn_ucshowid.Value), drp_dealtype.SelectedValue, Convert.ToDateTime(drp_updatedate.SelectedValue));

            }
            if (dealtable.Rows.Count > 0)
            {
                BindDealTemplateFieldData();
            }
        }
        protected void drp_updatedate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drp_updatedate.SelectedValue != "0")
            {
                try
                {
                    BindDetails();
                }
                catch (Exception ex)
                {
                    lbl_message.Text = "Error: " + ex.Message.ToString();
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void drp_dealtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drp_dealtype.SelectedValue != "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "changemodifystatus();", true);
                if (hdn_ucengagementid.Value == "-1")
                {
                    dealtable = ddl.GetTemplateRevisionDates(Convert.ToInt16(drp_show.SelectedValue), drp_dealtype.SelectedValue);
                    if (dealtable.Compute("min(deal_tmpt_cr_date)", null) != DBNull.Value)
                        txt_createdate.Text = Convert.ToDateTime(dealtable.Compute("min(deal_tmpt_cr_date)", null)).ToShortDateString();

                }
                else
                {
                    dealtable = ddl.GetTemplateRevisionDates(Convert.ToInt16(hdn_ucshowid.Value), drp_dealtype.SelectedValue);
                }

                drp_updatedate.Items.Clear();
                drp_updatedate.DataSource = dealtable;
                drp_updatedate.DataTextField = "deal_tmpt_upd_date";
                drp_updatedate.DataValueField = "deal_tmpt_upd_date";
                drp_updatedate.DataBind();
                drp_updatedate.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
                if (dealtable.Rows.Count == 1)
                {
                    drp_updatedate.SelectedIndex = 1;
                    BindDetails();
                }
            }
            else
            {
                drp_updatedate.Items.Clear();
                drp_updatedate.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
            }
        }

        //protected void imgbtncalendar_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (Session["calbtnflag1"].ToString() == "0")
        //    {
        //        cal_updatedate.Visible = true;
        //        Session["calbtnflag1"] = "1";
        //    }
        //    else
        //    {
        //        cal_updatedate.Visible = false;
        //        Session["calbtnflag1"] = "0";
        //    }
        //}

        protected void cal_updatedate_SelectionChanged(object sender, EventArgs e)
        {
            drp_updatedate.SelectedItem.Text = hdfdealdate.Value;
            drp_updatedate.SelectedItem.Value = hdfdealdate.Value;
            //cal_updatedate.Visible = false;
            //Session["calbtnflag1"] = "0";
        }

        public void BindDealFieldData()
        {
            txt_royalty.Text = dealtable.Rows[0]["deal_royalty_income"].ToString();
            drp_tax.SelectedValue = dealtable.Rows[0]["deal_tax_ptg_include"].ToString();
            txt_tax.Text = dealtable.Rows[0]["deal_tax_ptg"].ToString();
            drp_facilityfee1.SelectedValue = dealtable.Rows[0]["deal_facility_fee_inlcude"].ToString();
            if (dealtable.Rows[0]["deal_facility_fee_amt"] != DBNull.Value)
                txt_facilityfee.Text = Convert.ToDecimal(dealtable.Rows[0]["deal_facility_fee_amt"]).ToString("N2", new CultureInfo("en-US"));
            drp_facilityfee2.SelectedValue = dealtable.Rows[0]["deal_facility_fee_unit"].ToString();
            if (dealtable.Rows[0]["deal_guarantee_income"] != DBNull.Value)
                txt_guarantee.Text = dealtable.Rows[0]["deal_guarantee_income"].ToString();// Convert.ToDecimal(dealtable.Rows[0]["deal_guarantee_income"]).ToString("N2", new CultureInfo("en-US"));
            if (dealtable.Rows[0]["deal_tax_amt_over"] != DBNull.Value)
                txt_taxover.Text = Convert.ToDecimal(dealtable.Rows[0]["deal_tax_amt_over"]).ToString("N2", new CultureInfo("en-US"));
            drp_boxoffice1.SelectedValue = dealtable.Rows[0]["deal_tax_ff_bo_include"].ToString();
            if (dealtable.Rows[0]["deal_tax_ff_bo_comm"] != DBNull.Value)
                txt_boxoffice.Text = Convert.ToDecimal(dealtable.Rows[0]["deal_tax_ff_bo_comm"]).ToString("N2", new CultureInfo("en-US"));
            //drp_boxoffice2.SelectedValue = dealtable.Rows[0]["deal_othr_bo_unit"].ToString();

            txt_companymonies.Text = dealtable.Rows[0]["deal_cmpny_mid_monies_ptg"].ToString();
            txt_subscriptionsale.Text = dealtable.Rows[0]["deal_sub_sales_comm"].ToString();
            drp_miscellaneous11.SelectedValue = dealtable.Rows[0]["deal_misc_othr_include_1"].ToString();
            if (dealtable.Rows[0]["deal_misc_othr_amt_1"] != DBNull.Value)
                txt_miscellaneous1.Text = Convert.ToDecimal(dealtable.Rows[0]["deal_misc_othr_amt_1"]).ToString("N2", new CultureInfo("en-US"));
            drp_miscellaneous12.SelectedValue = dealtable.Rows[0]["deal_misc_othr_unit_1"].ToString();

            txt_presentermonies.Text = dealtable.Rows[0]["deal_presenter_mid_monies_ptg"].ToString();
            txt_phonecommission.Text = dealtable.Rows[0]["deal_ph_sales_comm"].ToString();
            drp_miscellaneous21.SelectedValue = dealtable.Rows[0]["deal_misc_othr_include_2"].ToString();
            if (dealtable.Rows[0]["deal_misc_othr_amt_2"] != DBNull.Value)
                txt_miscellaneous2.Text = Convert.ToDecimal(dealtable.Rows[0]["deal_misc_othr_amt_2"]).ToString("N2", new CultureInfo("en-US"));
            drp_miscellaneous22.SelectedValue = dealtable.Rows[0]["deal_misc_othr_unit_2"].ToString();

            drp_miscellaneous31.SelectedValue = dealtable.Rows[0]["deal_misc_othr_include_3"].ToString();
            if (dealtable.Rows[0]["deal_misc_othr_amt_3"] != DBNull.Value)
                txt_miscellaneous3.Text = Convert.ToDecimal(dealtable.Rows[0]["deal_misc_othr_amt_3"]).ToString("N2", new CultureInfo("en-US"));
            drp_miscellaneous32.SelectedValue = dealtable.Rows[0]["deal_misc_othr_unit_3"].ToString();

            drp_miscellaneous41.SelectedValue = dealtable.Rows[0]["deal_misc_othr_include_4"].ToString();
            if (dealtable.Rows[0]["deal_misc_othr_amt_4"] != DBNull.Value)
                txt_miscellaneous4.Text = Convert.ToDecimal(dealtable.Rows[0]["deal_misc_othr_amt_4"]).ToString("N2", new CultureInfo("en-US"));
            drp_miscellaneous42.SelectedValue = dealtable.Rows[0]["deal_misc_othr_unit_4"].ToString();

            drp_miscellaneous51.SelectedValue = dealtable.Rows[0]["deal_misc_othr_include_5"].ToString();
            if (dealtable.Rows[0]["deal_misc_othr_amt_5"] != DBNull.Value)
                txt_miscellaneous5.Text = Convert.ToDecimal(dealtable.Rows[0]["deal_misc_othr_amt_5"]).ToString("N2", new CultureInfo("en-US"));
            drp_miscellaneous52.SelectedValue = dealtable.Rows[0]["deal_misc_othr_unit_5"].ToString();

            if (dealtable.Rows[0]["deal_mid_monies_cap"] != DBNull.Value)
                txt_middlecap.Text = Convert.ToDecimal(dealtable.Rows[0]["deal_mid_monies_cap"]).ToString("N2", new CultureInfo("en-US"));
            txt_internetsale.Text = dealtable.Rows[0]["deal_web_sales_comm"].ToString();
            txt_remotesale.Text = dealtable.Rows[0]["deal_remote_sales_comm"].ToString();

            txt_producershare.Text = dealtable.Rows[0]["deal_producer_share_split_ptg"].ToString();
            txt_cardsale.Text = dealtable.Rows[0]["deal_cc_sales_comm"].ToString();
            txt_singleticket.Text = dealtable.Rows[0]["deal_single_tix_comm"].ToString();

            txt_presentershare.Text = dealtable.Rows[0]["deal_presenter_share_split_ptg"].ToString();
            txt_groupsale1.Text = dealtable.Rows[0]["deal_grp_sales_comm1"].ToString();
            txt_groupsale2.Text = dealtable.Rows[0]["deal_grp_sales_comm2"].ToString();

            txt_starroyalty.Text = dealtable.Rows[0]["deal_star_royalty_ptg"].ToString();
            drp_taxbudget.SelectedValue = dealtable.Rows[0]["deal_incm_wthd_tax_bgt_unit"].ToString();
            if (dealtable.Rows[0]["deal_incm_wthd_tax_bgt_amt"] != DBNull.Value)
                txt_taxbudget.Text = Convert.ToDecimal(dealtable.Rows[0]["deal_incm_wthd_tax_bgt_amt"]).ToString("N2", new CultureInfo("en-US"));
            drp_taxactual.SelectedValue = dealtable.Rows[0]["deal_incm_wthd_tax_act_unit"].ToString();
            if (dealtable.Rows[0]["deal_incm_wthd_tax_act_amt"] != DBNull.Value)
                txt_taxactual.Text = Convert.ToDecimal(dealtable.Rows[0]["deal_incm_wthd_tax_act_amt"]).ToString("N2", new CultureInfo("en-US"));
            txt_tax2.Text = dealtable.Rows[0]["DEAL_TAX2_PTG"].ToString();
            ddlMisOthers1.SelectedIndex = ddlMisOthers1.Items.IndexOf(ddlMisOthers1.Items.FindByText(dealtable.Rows[0]["DEAL_MISC_OTHR_DESC_1"].ToString()));
            ddlMisOthers2.SelectedIndex = ddlMisOthers2.Items.IndexOf(ddlMisOthers2.Items.FindByText(dealtable.Rows[0]["DEAL_MISC_OTHR_DESC_2"].ToString()));
            ddlMisOthers3.SelectedIndex = ddlMisOthers3.Items.IndexOf(ddlMisOthers3.Items.FindByText(dealtable.Rows[0]["DEAL_MISC_OTHR_DESC_3"].ToString()));
            ddlMisOthers4.SelectedIndex = ddlMisOthers4.Items.IndexOf(ddlMisOthers4.Items.FindByText(dealtable.Rows[0]["DEAL_MISC_OTHR_DESC_4"].ToString()));
            ddlMisOthers5.SelectedIndex = ddlMisOthers5.Items.IndexOf(ddlMisOthers5.Items.FindByText(dealtable.Rows[0]["DEAL_MISC_OTHR_DESC_5"].ToString()));
            ddltaxptg_ff.SelectedIndex = ddltaxptg_ff.Items.IndexOf(ddltaxptg_ff.Items.FindByValue(dealtable.Rows[0]["DEAL_TAX_PTG_FF"].ToString()));
        }

        public void BindDealTemplateFieldData()
        {
            txt_royalty.Text = dealtable.Rows[0]["DEAL_TMPT_royalty_income"].ToString();
            drp_tax.SelectedValue = dealtable.Rows[0]["DEAL_TMPT_tax_ptg_include"].ToString();
            txt_tax.Text = dealtable.Rows[0]["DEAL_TMPT_tax_ptg"].ToString();
            drp_facilityfee1.SelectedValue = dealtable.Rows[0]["DEAL_TMPT_facility_fee_include"].ToString();
            if (dealtable.Rows[0]["DEAL_TMPT_fee_amt"] != DBNull.Value)
                txt_facilityfee.Text = dealtable.Rows[0]["DEAL_TMPT_fee_amt"].ToString();
            drp_facilityfee2.SelectedValue = dealtable.Rows[0]["DEAL_TMPT_facility_fee_unit"].ToString();

            if (dealtable.Rows[0]["DEAL_TMPT_gaurantee_income"] != DBNull.Value)
                txt_guarantee.Text = dealtable.Rows[0]["DEAL_TMPT_gaurantee_income"].ToString();
            if (dealtable.Rows[0]["DEAL_TMPT_amt_over"] != DBNull.Value)
                txt_taxover.Text = dealtable.Rows[0]["DEAL_TMPT_amt_over"].ToString();
            drp_boxoffice1.SelectedValue = dealtable.Rows[0]["deal_tmpt_tax_ff_bo_include"].ToString();
            if (dealtable.Rows[0]["deal_tmpt_tax_ff_bo_comm"] != DBNull.Value)
                txt_boxoffice.Text = dealtable.Rows[0]["deal_tmpt_tax_ff_bo_comm"].ToString();
            //drp_boxoffice2.SelectedValue = dealtable.Rows[0]["DEAL_TMPT_bo_unit"].ToString();

            txt_companymonies.Text = dealtable.Rows[0]["DEAL_TMPT_cmpny_mid_monies_ptg"].ToString();
            txt_subscriptionsale.Text = dealtable.Rows[0]["DEAL_TMPT_sub_sales_comm"].ToString();
            drp_miscellaneous11.SelectedValue = dealtable.Rows[0]["DEAL_TMPT_misc_othr_include_1"].ToString();
            if (dealtable.Rows[0]["DEAL_TMPT_misc_othr_amt_1"] != DBNull.Value)
                txt_miscellaneous1.Text = dealtable.Rows[0]["DEAL_TMPT_misc_othr_amt_1"].ToString();
            drp_miscellaneous12.SelectedValue = dealtable.Rows[0]["DEAL_TMPT_misc_othr_unit_1"].ToString();

            txt_presentermonies.Text = dealtable.Rows[0]["DEAL_TMPT_presenter_mid_monies_ptg"].ToString();
            txt_phonecommission.Text = dealtable.Rows[0]["DEAL_TMPT_ph_sales_comm"].ToString();
            drp_miscellaneous21.SelectedValue = dealtable.Rows[0]["DEAL_TMPT_misc_othr_include_2"].ToString();
            if (dealtable.Rows[0]["DEAL_TMPT_misc_othr_amt_2"] != DBNull.Value)
                txt_miscellaneous2.Text = dealtable.Rows[0]["DEAL_TMPT_misc_othr_amt_2"].ToString();
            drp_miscellaneous22.SelectedValue = dealtable.Rows[0]["DEAL_TMPT_misc_othr_unit_2"].ToString();

            drp_miscellaneous31.SelectedValue = dealtable.Rows[0]["DEAL_TMPT_misc_othr_include_3"].ToString();
            if (dealtable.Rows[0]["DEAL_TMPT_misc_othr_amt_3"] != DBNull.Value)
                txt_miscellaneous3.Text = dealtable.Rows[0]["DEAL_TMPT_misc_othr_amt_3"].ToString();
            drp_miscellaneous32.SelectedValue = dealtable.Rows[0]["DEAL_TMPT_misc_othr_unit_3"].ToString();

            drp_miscellaneous41.SelectedValue = dealtable.Rows[0]["DEAL_TMPT_misc_othr_include_4"].ToString();
            if (dealtable.Rows[0]["DEAL_TMPT_misc_othr_amt_4"] != DBNull.Value)
                txt_miscellaneous2.Text = dealtable.Rows[0]["DEAL_TMPT_misc_othr_amt_4"].ToString();
            drp_miscellaneous42.SelectedValue = dealtable.Rows[0]["DEAL_TMPT_misc_othr_unit_4"].ToString();

            drp_miscellaneous51.SelectedValue = dealtable.Rows[0]["DEAL_TMPT_misc_othr_include_5"].ToString();
            if (dealtable.Rows[0]["DEAL_TMPT_misc_othr_amt_5"] != DBNull.Value)
                txt_miscellaneous5.Text = dealtable.Rows[0]["DEAL_TMPT_misc_othr_amt_5"].ToString();
            drp_miscellaneous52.SelectedValue = dealtable.Rows[0]["DEAL_TMPT_misc_othr_unit_5"].ToString();

            if (dealtable.Rows[0]["DEAL_TMPT_mid_monies_cap"] != DBNull.Value)
                txt_middlecap.Text = dealtable.Rows[0]["DEAL_TMPT_mid_monies_cap"].ToString();
            txt_internetsale.Text = dealtable.Rows[0]["DEAL_TMPT_web_sales_comm"].ToString();
            txt_remotesale.Text = dealtable.Rows[0]["DEAL_TMPT_remote_sales_comm"].ToString();

            txt_producershare.Text = dealtable.Rows[0]["DEAL_TMPT_producer_share_split_ptg"].ToString();
            txt_cardsale.Text = dealtable.Rows[0]["DEAL_TMPT_cc_sales_comm"].ToString();
            txt_singleticket.Text = dealtable.Rows[0]["DEAL_TMPT_single_tix_comm"].ToString();

            txt_presentershare.Text = dealtable.Rows[0]["DEAL_TMPT_presenter_share_split_ptg"].ToString();
            txt_groupsale1.Text = dealtable.Rows[0]["DEAL_TMPT_grp_sales_comm1"].ToString();
            txt_groupsale2.Text = dealtable.Rows[0]["DEAL_TMPT_grp_sales_comm2"].ToString();

            txt_starroyalty.Text = dealtable.Rows[0]["DEAL_TMPT_star_royalty_ptg"].ToString();
            drp_taxbudget.SelectedValue = dealtable.Rows[0]["DEAL_TMPT_incm_wthd_tax_bgt_unit"].ToString();
            if (dealtable.Rows[0]["DEAL_TMPT_incm_wthd_tax_bgt_amt"] != DBNull.Value)
                txt_taxbudget.Text = dealtable.Rows[0]["DEAL_TMPT_incm_wthd_tax_bgt_amt"].ToString();
            drp_taxactual.SelectedValue = dealtable.Rows[0]["DEAL_TMPT_incm_wthd_tax_act_unit"].ToString();
            if (dealtable.Rows[0]["DEAL_TMPT_incm_wthd_tax_act_amt"] != DBNull.Value)
                txt_taxactual.Text = dealtable.Rows[0]["DEAL_TMPT_incm_wthd_tax_act_amt"].ToString();
            txt_tax2.Text = dealtable.Rows[0]["DEAL_TMPT_TAX2_PTG"].ToString();
            ddlMisOthers1.SelectedIndex = ddlMisOthers1.Items.IndexOf(ddlMisOthers1.Items.FindByText(dealtable.Rows[0]["DEAL_TMPT_MISC_OTHR_DESC_1"].ToString()));
            ddlMisOthers2.SelectedIndex = ddlMisOthers2.Items.IndexOf(ddlMisOthers2.Items.FindByText(dealtable.Rows[0]["DEAL_TMPT_MISC_OTHR_DESC_2"].ToString()));
            ddlMisOthers3.SelectedIndex = ddlMisOthers3.Items.IndexOf(ddlMisOthers3.Items.FindByText(dealtable.Rows[0]["DEAL_TMPT_MISC_OTHR_DESC_3"].ToString()));
            ddlMisOthers4.SelectedIndex = ddlMisOthers4.Items.IndexOf(ddlMisOthers4.Items.FindByText(dealtable.Rows[0]["DEAL_TMPT_MISC_OTHR_DESC_4"].ToString()));
            ddlMisOthers5.SelectedIndex = ddlMisOthers5.Items.IndexOf(ddlMisOthers5.Items.FindByText(dealtable.Rows[0]["DEAL_TMPT_MISC_OTHR_DESC_5"].ToString()));
            ddltaxptg_ff.SelectedIndex = ddltaxptg_ff.Items.IndexOf(ddltaxptg_ff.Items.FindByValue(dealtable.Rows[0]["DEAL_TMPT_TAX_PTG_FF"].ToString()));
        }
        public bool checkvalid()
        {
            Nullable<decimal> producershare = null, presentershare = null, starroyalty = null;
            producershare = (txt_producershare.Text.Trim() == "") ? producershare : Convert.ToDecimal(Regex.Replace(txt_producershare.Text, @"\$|\,|\%", ""));
            presentershare = (txt_presentershare.Text.Trim() == "") ? presentershare : Convert.ToDecimal(Regex.Replace(txt_presentershare.Text, @"\$|\,|\%", ""));
            starroyalty = (txt_starroyalty.Text.Trim() == "") ? starroyalty : Convert.ToDecimal(Regex.Replace(txt_starroyalty.Text, @"\$|\,|\%", ""));
            int sumofproducershare = Convert.ToInt32(producershare) + Convert.ToInt32(presentershare) + Convert.ToInt32(starroyalty);
            if (sumofproducershare != 0 && Convert.ToInt32(sumofproducershare) != 100)
            {
                return false;
            }
            return true;
        }
        public void SaveDealData(string mode, int dealtemplateid, int engagementid, int dealid)
        {
            Nullable<decimal> royalty = null, guarantee = null, producershare = null, middlecap = null, presentershare = null, starroyalty = null, taxbudget = null, taxactual = null, taxover = null,
            companymonies = null, presentermonies = null, tax = null, subscriptionsale = null, phonecommission = null, internetsale = null, cardsale = null, facilityfee = null, boxoffice = null,
            miscellaneous1 = null, miscellaneous2 = null,miscellaneous3 = null,miscellaneous4 = null,miscellaneous5 = null, remotesale = null, singleticket = null, groupsale1 = null, groupsale2 = null, tax2 = null,exchange=null;
            string misc_other_desc1 = "", misc_other_desc2 = "", misc_other_desc3 = "", misc_other_desc4 = "", misc_other_desc5 = "", taxptg_ff = "", dealdemo = "", contract = "";


            royalty = (txt_royalty.Text.Trim() == "") ? royalty : Convert.ToDecimal(Regex.Replace(txt_royalty.Text, @"\$|\,|\%", ""));
            guarantee = txt_guarantee.Text.AutoformatDecimal();
            companymonies = (txt_companymonies.Text.Trim() == "") ? companymonies : Convert.ToDecimal(Regex.Replace(txt_companymonies.Text, @"\$|\,|\%", ""));
            presentermonies = (txt_presentermonies.Text.Trim() == "") ? presentermonies : Convert.ToDecimal(Regex.Replace(txt_presentermonies.Text, @"\$|\,|\%", ""));
            middlecap = txt_middlecap.Text.AutoformatDecimal();
            producershare = (txt_producershare.Text.Trim() == "") ? producershare : Convert.ToDecimal(Regex.Replace(txt_producershare.Text, @"\$|\,|\%", ""));
            presentershare = (txt_presentershare.Text.Trim() == "") ? presentershare : Convert.ToDecimal(Regex.Replace(txt_presentershare.Text, @"\$|\,|\%", ""));
            starroyalty = (txt_starroyalty.Text.Trim() == "") ? starroyalty : Convert.ToDecimal(Regex.Replace(txt_starroyalty.Text, @"\$|\,|\%", ""));
            taxbudget = (txt_taxbudget.Text.Trim() == "") ? taxbudget : Convert.ToDecimal(Regex.Replace(txt_taxbudget.Text, @"\$|\,|\%", ""));
            taxactual = (txt_taxactual.Text.Trim() == "") ? taxactual : Convert.ToDecimal(Regex.Replace(txt_taxactual.Text, @"\$|\,|\%", ""));
            tax = (txt_tax.Text.Trim() == "") ? tax : Convert.ToDecimal(Regex.Replace(txt_tax.Text, @"\$|\,|\%", ""));
            taxover = txt_taxover.Text.AutoformatDecimal();
            subscriptionsale = (txt_subscriptionsale.Text.Trim() == "") ? subscriptionsale : Convert.ToDecimal(Regex.Replace(txt_subscriptionsale.Text, @"\$|\,|\%", ""));
            phonecommission = (txt_phonecommission.Text.Trim() == "") ? phonecommission : Convert.ToDecimal(Regex.Replace(txt_phonecommission.Text, @"\$|\,|\%", ""));
            internetsale = (txt_internetsale.Text.Trim() == "") ? internetsale : Convert.ToDecimal(Regex.Replace(txt_internetsale.Text, @"\$|\,|\%", ""));
            cardsale = (txt_cardsale.Text.Trim() == "") ? cardsale : Convert.ToDecimal(Regex.Replace(txt_cardsale.Text, @"\$|\,|\%", ""));
            facilityfee = (txt_facilityfee.Text.Trim() == "") ? facilityfee : Convert.ToDecimal(Regex.Replace(txt_facilityfee.Text, @"\$|\,|\%", ""));
            boxoffice = (txt_boxoffice.Text.Trim() == "") ? boxoffice : Convert.ToDecimal(Regex.Replace(txt_boxoffice.Text, @"\$|\,|\%", ""));
            miscellaneous1 = (txt_miscellaneous1.Text.Trim() == "") ? miscellaneous1 : Convert.ToDecimal(Regex.Replace(txt_miscellaneous1.Text, @"\$|\,|\%", ""));
            miscellaneous2 = (txt_miscellaneous2.Text.Trim() == "") ? miscellaneous2 : Convert.ToDecimal(Regex.Replace(txt_miscellaneous2.Text, @"\$|\,|\%", ""));
            miscellaneous3 = (txt_miscellaneous3.Text.Trim() == "") ? miscellaneous3 : Convert.ToDecimal(Regex.Replace(txt_miscellaneous3.Text, @"\$|\,|\%", ""));
            miscellaneous4 = (txt_miscellaneous4.Text.Trim() == "") ? miscellaneous4 : Convert.ToDecimal(Regex.Replace(txt_miscellaneous4.Text, @"\$|\,|\%", ""));
            miscellaneous5 = (txt_miscellaneous5.Text.Trim() == "") ? miscellaneous5 : Convert.ToDecimal(Regex.Replace(txt_miscellaneous5.Text, @"\$|\,|\%", ""));
            remotesale = (txt_remotesale.Text.Trim() == "") ? remotesale : Convert.ToDecimal(Regex.Replace(txt_remotesale.Text, @"\$|\,|\%", ""));
            singleticket = (txt_singleticket.Text.Trim() == "") ? singleticket : Convert.ToDecimal(Regex.Replace(txt_singleticket.Text, @"\$|\,|\%", ""));
            groupsale1 = (txt_groupsale1.Text.Trim() == "") ? groupsale1 : Convert.ToDecimal(Regex.Replace(txt_groupsale1.Text, @"\$|\,|\%", ""));
            groupsale2 = (txt_groupsale2.Text.Trim() == "") ? groupsale2 : Convert.ToDecimal(Regex.Replace(txt_groupsale2.Text, @"\$|\,|\%", ""));
            tax2 = (txt_tax2.Text.Trim() == "") ? tax2 : Convert.ToDecimal(Regex.Replace(txt_tax2.Text, @"\$|\,|\%", ""));
            misc_other_desc1 = ddlMisOthers1.SelectedIndex == 0 ? "" : ddlMisOthers1.SelectedItem.Text.Trim();
            misc_other_desc2 = ddlMisOthers2.SelectedIndex == 0 ? "" : ddlMisOthers2.SelectedItem.Text.Trim();
            misc_other_desc3 = ddlMisOthers3.SelectedIndex == 0 ? "" : ddlMisOthers3.SelectedItem.Text.Trim();
            misc_other_desc4 = ddlMisOthers4.SelectedIndex == 0 ? "" : ddlMisOthers4.SelectedItem.Text.Trim();
            misc_other_desc5 = ddlMisOthers5.SelectedIndex == 0 ? "" : ddlMisOthers5.SelectedItem.Text.Trim();
            taxptg_ff = (drp_tax.SelectedItem.Value.ToLower() == "i") ? ddltaxptg_ff.SelectedItem.Value : "";
            TextInfo textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
            dealdemo = drp_dealmemo.SelectedValue;
            exchange = txt_exchange.Text.AutoformatDecimal();
            contract = drp_contract.SelectedValue;
            if (mode == "createtemplate")
            {
                DataTable recordcheck = ddl.GetDealTemplateDetails(Convert.ToInt16(drp_show.SelectedValue), drp_dealtype.SelectedItem.Text, Convert.ToDateTime(txt_createdate.Text));
                if (recordcheck.Rows.Count > 0)
                {
                    lbl_message.Text = "Deal Template exists for the same Show, Deal Type and Update date. Please check and correct";
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    int inserttemplatedata = ddl.CreateDealTemplate(Convert.ToInt16(drp_show.SelectedValue), textInfo.ToTitleCase(drp_dealtype.SelectedItem.Text), Convert.ToDateTime(txt_createdate.Text),
                            Convert.ToDateTime(System.DateTime.Now), royalty, guarantee, companymonies, presentermonies, middlecap,
                            producershare, presentershare, starroyalty, drp_taxbudget.SelectedValue, taxbudget, drp_taxactual.SelectedValue, taxactual, drp_tax.SelectedValue, tax,
                            taxover, subscriptionsale, phonecommission, internetsale, cardsale, drp_facilityfee1.SelectedValue,
                            facilityfee, drp_facilityfee2.SelectedValue, drp_boxoffice1.SelectedValue, boxoffice,
                            drp_miscellaneous11.SelectedValue, miscellaneous1, drp_miscellaneous12.SelectedValue,
                           drp_miscellaneous21.SelectedValue, miscellaneous2, drp_miscellaneous22.SelectedValue,
                           drp_miscellaneous31.SelectedValue, miscellaneous3, drp_miscellaneous32.SelectedValue,
                           drp_miscellaneous41.SelectedValue, miscellaneous4, drp_miscellaneous42.SelectedValue,
                           drp_miscellaneous51.SelectedValue, miscellaneous5, drp_miscellaneous52.SelectedValue,
                           remotesale, singleticket, groupsale1, groupsale2, tax2, misc_other_desc1,
                            misc_other_desc2, misc_other_desc3, misc_other_desc4, misc_other_desc5, taxptg_ff);
                    if (string.IsNullOrEmpty(Request.QueryString["engmtidnew"]))
                    { Response.Redirect("~/Deal.aspx?dealid=" + inserttemplatedata + "&status=1"); }
                    else
                    {
                        dealtable = ddl.GetDealDetails(Convert.ToInt32(Request.QueryString["engmtidnew"]));
                        if (dealtable.Rows.Count == 0)
                        {
                            int insertdealdata = ddl.CreateDeal(Convert.ToInt32(Request.QueryString["engmtidnew"]), textInfo.ToTitleCase(drp_dealtype.SelectedItem.Text), Convert.ToDateTime(txt_createdate.Text), royalty, guarantee, companymonies, presentermonies, middlecap,
                                producershare, presentershare, starroyalty, drp_taxbudget.SelectedValue, taxbudget, drp_taxactual.SelectedValue, taxactual, drp_tax.SelectedValue, tax,
                                taxover, subscriptionsale, phonecommission, internetsale, cardsale, drp_facilityfee1.SelectedValue,
                                facilityfee, drp_facilityfee2.SelectedValue, drp_boxoffice1.SelectedValue, boxoffice,
                                drp_miscellaneous11.SelectedValue, miscellaneous1, drp_miscellaneous12.SelectedValue,
                                drp_miscellaneous21.SelectedValue, miscellaneous2, drp_miscellaneous22.SelectedValue,
                                drp_miscellaneous31.SelectedValue, miscellaneous3, drp_miscellaneous32.SelectedValue,
                           drp_miscellaneous41.SelectedValue, miscellaneous4, drp_miscellaneous42.SelectedValue,
                           drp_miscellaneous51.SelectedValue, miscellaneous5, drp_miscellaneous52.SelectedValue,
                           remotesale, singleticket, groupsale1, groupsale2, tax2, misc_other_desc1, misc_other_desc2, misc_other_desc3, misc_other_desc4, misc_other_desc5, taxptg_ff); ;

                            mdl.Engmt_update(Convert.ToInt32(hdn_ucengagementid.Value), "0", "0", dealdemo, exchange, contract, "D");
                        }
                        Response.Redirect("~/EngagementDeal.aspx?engmtid=" + Request.QueryString["engmtidnew"] + "&showid=" + Request.QueryString["showidnew"] + "&schcount=1");
                    }
                }
            }
            else if (mode == "updatetemplate")
            {
                int updatetemplatedata = ddl.UpdateDealTemplate(dealtemplateid, Convert.ToInt16(drp_show.SelectedValue), textInfo.ToTitleCase(drp_dealtype.SelectedItem.Text), Convert.ToDateTime(txt_createdate.Text),
                    Convert.ToDateTime(drp_updatedate.SelectedItem.Text), royalty, guarantee, companymonies, presentermonies, middlecap,
                    producershare, presentershare, starroyalty, drp_taxbudget.SelectedValue, taxbudget, drp_taxactual.SelectedValue, taxactual, drp_tax.SelectedValue, tax,
                    taxover, subscriptionsale, phonecommission, internetsale, cardsale, drp_facilityfee1.SelectedValue,
                    facilityfee, drp_facilityfee2.SelectedValue, drp_boxoffice1.SelectedValue, boxoffice,
                    drp_miscellaneous11.SelectedValue, miscellaneous1, drp_miscellaneous12.SelectedValue,
                    drp_miscellaneous21.SelectedValue, miscellaneous2, drp_miscellaneous22.SelectedValue,
                    drp_miscellaneous31.SelectedValue, miscellaneous3, drp_miscellaneous32.SelectedValue,
                    drp_miscellaneous41.SelectedValue, miscellaneous4, drp_miscellaneous42.SelectedValue,
                    drp_miscellaneous51.SelectedValue, miscellaneous5, drp_miscellaneous52.SelectedValue,
                    remotesale, singleticket, groupsale1, groupsale2, tax2,
                    misc_other_desc1, misc_other_desc2, misc_other_desc3, misc_other_desc4, misc_other_desc5, taxptg_ff);
            }

            else if (mode == "createdeal")
            {
                int insertdealdata = ddl.CreateDeal(engagementid, textInfo.ToTitleCase(drp_dealtype.SelectedItem.Text), Convert.ToDateTime(System.DateTime.Today), royalty, guarantee, companymonies, presentermonies, middlecap,
                    producershare, presentershare, starroyalty, drp_taxbudget.SelectedValue, taxbudget, drp_taxactual.SelectedValue, taxactual, drp_tax.SelectedValue, tax,
                    taxover, subscriptionsale, phonecommission, internetsale, cardsale, drp_facilityfee1.SelectedValue,
                    facilityfee, drp_facilityfee2.SelectedValue, drp_boxoffice1.SelectedValue, boxoffice,
                    drp_miscellaneous11.SelectedValue, miscellaneous1, drp_miscellaneous12.SelectedValue,
                    drp_miscellaneous21.SelectedValue, miscellaneous2, drp_miscellaneous22.SelectedValue,
                    drp_miscellaneous31.SelectedValue, miscellaneous3, drp_miscellaneous32.SelectedValue,
                    drp_miscellaneous41.SelectedValue, miscellaneous4, drp_miscellaneous42.SelectedValue,
                    drp_miscellaneous51.SelectedValue, miscellaneous5, drp_miscellaneous52.SelectedValue,
                    remotesale, singleticket, groupsale1, groupsale2, tax2,
                    misc_other_desc1, misc_other_desc2, misc_other_desc3, misc_other_desc4, misc_other_desc5, taxptg_ff);

                hdn_ucdealid.Value = insertdealdata.ToString();
                mdl.Engmt_update(Convert.ToInt32(hdn_ucengagementid.Value), "0", "0", dealdemo, exchange, contract, "D");
            }

            else
            {
                int updatedealdata = ddl.UpdateDeal(dealid, textInfo.ToTitleCase(drp_dealtype.SelectedItem.Text), Convert.ToDateTime(drp_updatedate.SelectedItem.Text), royalty, guarantee, companymonies, presentermonies, middlecap,
                    producershare, presentershare, starroyalty, drp_taxbudget.SelectedValue, taxbudget, drp_taxactual.SelectedValue, taxactual, drp_tax.SelectedValue, tax,
                    taxover, subscriptionsale, phonecommission, internetsale, cardsale, drp_facilityfee1.SelectedValue,
                    facilityfee, drp_facilityfee2.SelectedValue, drp_boxoffice1.SelectedValue, boxoffice,
                    drp_miscellaneous11.SelectedValue, miscellaneous1, drp_miscellaneous12.SelectedValue,
                    drp_miscellaneous21.SelectedValue, miscellaneous2, drp_miscellaneous22.SelectedValue,
                    drp_miscellaneous31.SelectedValue, miscellaneous3, drp_miscellaneous32.SelectedValue,
                    drp_miscellaneous41.SelectedValue, miscellaneous4, drp_miscellaneous42.SelectedValue,
                    drp_miscellaneous51.SelectedValue, miscellaneous5, drp_miscellaneous52.SelectedValue,
                    remotesale, singleticket, groupsale1, groupsale2, tax2, misc_other_desc1, misc_other_desc2, misc_other_desc3, misc_other_desc4, misc_other_desc5, taxptg_ff);
                mdl.Engmt_update(Convert.ToInt32(hdn_ucengagementid.Value), "0", "0", dealdemo, exchange, contract, "D");
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "showhide_ff();", true);
        }

        public void DeleteDealTemplate(int dealtemplateid, string delflag)
        {
            int deletetemplatedata = ddl.DeleteDealTemplate(dealtemplateid, delflag);
        }

        public void Reset()
        {
            txt_boxoffice.Text = "";
            txt_cardsale.Text = "";
            txt_companymonies.Text = "";
            txt_facilityfee.Text = "";
            txt_groupsale1.Text = "";
            txt_groupsale2.Text = "";
            txt_guarantee.Text = "";
            txt_internetsale.Text = "";
            txt_middlecap.Text = "";
            txt_miscellaneous1.Text = "";
            txt_miscellaneous2.Text = "";
            txt_miscellaneous3.Text = "";
            txt_miscellaneous4.Text = "";
            txt_miscellaneous5.Text = "";
            txt_phonecommission.Text = "";
            txt_presentermonies.Text = "";
            txt_presentershare.Text = "";
            txt_producershare.Text = "";
            txt_remotesale.Text = "";
            txt_royalty.Text = "";
            txt_singleticket.Text = "";
            txt_starroyalty.Text = "";
            txt_subscriptionsale.Text = "";
            txt_tax.Text = "";
            txt_taxactual.Text = "";
            txt_taxbudget.Text = "";
            txt_taxover.Text = "";
            txt_tax2.Text = string.Empty;
            drp_dealtype.SelectedIndex = 0;
            drp_updatedate.Items.Clear();
            ddlMisOthers1.SelectedIndex = 0;
            ddlMisOthers2.SelectedIndex = 0;
            ddlMisOthers3.SelectedIndex = 0;
            ddlMisOthers4.SelectedIndex = 0;
            ddlMisOthers5.SelectedIndex = 0;
            drp_updatedate.Items.Add(new ListItem("--Select--", "0"));
            if (hdn_ucshowid.Value != "0")
            {
                ddl.DeleteDeal(Convert.ToInt32(hdn_ucengagementid.Value));
                //rfv_dealtype.Enabled = false;
                //rfv_updatedate.Enabled = false;
                //val_summary_deal.Enabled = false;
                //rfv_dealtype1.Enabled = false;
            }
        }

        
    }
}