using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoxOfficeLayer;
using System.Data;
using MasterDataLayer;
using System.Globalization;
using NTOS.DataLayer;
using CommonFunction;
//using System.Threading;
//using System.Globalization;
namespace NTOS
{
    public partial class EngagementBoxOffice : System.Web.UI.Page, MasterPageSaveInterface
    {
        Label lbl_msg;
        int boid = 0;
        BoxOfficeData ofcobj = new BoxOfficeData();
        MasterData objmst = new MasterData();
        CommonFun objcf = new CommonFun();
        private DataTable dtperformancelist = new DataTable();
        public bool isOverride = false;
        //private int engmentid;
        //private string schcount;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "setcss();", true);
            }
        }
        public void Reset()
        {
            txtComps.Text = "";
            txtCreditcrdTcktSold.Text = "";
            txtCreditGrossReceipts.Text = "";
            //txtCreditPerc.Text = "";
            txtDropcount.Text = "";
            txtgrossales.Text = "";
            txtGroup1grossReceipts.Text = "";
            //txtGroup1Perc.Text = "";
            txtGroup1TicktSold.Text = "";
            txtGroupGrossReceipts.Text = "";
            //txtGroupPerc.Text = "";
            txtGroupTicktSold.Text = "";
            txtInternetGrossRecpt.Text = "";
            //txtInternetPerc.Text = "";
            txtInternetTcktSold.Text = "";
            txtOtherDollGrossReceipts.Text = "";
            //txtOtherDollPerc.Text = "";
            txtOtherDollTcktSold.Text = "";
            txtOtherPercGrossReceipts.Text = "";
            //txtOtherPercPerc.Text = "";
            txtOtherPercTcktSold.Text = "";
            txtPaidAttendance.Text = "";
            txtPhoneGrossReceipts.Text = "";
            //txtPhonePerc.Text = "";
            txtPhoneTcktSold.Text = "";
            txtRemoteGrossReceipts.Text = "";
            txtRemoteoutletTcktSold.Text = "";
            //txtRemotePerc.Text = "";
            //txtSingletixPerc.Text = "";
            txtSingletixtcktSold.Text = "";
            txtSingletixtGrossReceipts.Text = "";
            txtSubscriptionreceipts.Text = "";
            txtSubscriptionsold.Text = "";
            //txtSubsSalesPerc.Text = "";
            txtTotalGrossReceipts.Text = "";
            txtTotalTcktSold.Text = "";

            txtOther3TcktSold.Text = "";
            txtOther3GrossReceipts.Text = "";
            txtOther4TcktSold.Text = "";
            txtOther4GrossReceipts.Text = "";
            txtOther5TcktSold.Text = "";
            txtOther5GrossReceipts.Text = "";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           (this.Master as EngagementMaster).hidesummary();
            lbl_msg = (Label)this.Master.FindControl("lbl_message");
            lbl_msg.Text = "";
            if (!IsPostBack)
            {
                //txtFacilityfeeoneachticket.Attributes.Add("ReadOnly", "ReadOnly");
                //txtTax1.Attributes.Add("ReadOnly", "ReadOnly");
                //txtTaxAmountOver.Attributes.Add("ReadOnly", "ReadOnly");
                //txtTaxFacilityfeecoms.Attributes.Add("ReadOnly", "ReadOnly");
                //txtSubsSalesPerc.Attributes.Add("ReadOnly", "ReadOnly");
                //txtPhonePerc.Attributes.Add("ReadOnly", "ReadOnly");
                //txtInternetPerc.Attributes.Add("ReadOnly", "ReadOnly");
                //txtCreditPerc.Attributes.Add("ReadOnly", "ReadOnly");
                //txtRemotePerc.Attributes.Add("ReadOnly", "ReadOnly");
                //txtGroup1Perc.Attributes.Add("ReadOnly", "ReadOnly");
                //txtGroupPerc.Attributes.Add("ReadOnly", "ReadOnly");
                //txtOtherDollPerc.Attributes.Add("ReadOnly", "ReadOnly");
                //txtOtherPercPerc.Attributes.Add("ReadOnly", "ReadOnly");
                //txtSingletixPerc.Attributes.Add("ReadOnly", "ReadOnly");
                //lblScheduleDate.Text = Convert.ToString(System.DateTime.Now.ToString("MMM d,yyyy"));
                //lblScheduleTime.Text = Convert.ToString(System.DateTime.Now.ToString("m:ss"));
                int engmentid = Convert.ToInt32(Request.QueryString["engmtid"]);
                Session["search_engmt"] = engmentid.ToString();
                //engmentid = engagementid;
                string schcount = Request.QueryString["schcount"];
                (this.Master as EngagementMaster).SetActiveTab("libox");
                //((ImageButton)this.Master.FindControl("imgbtnbo")).ImageUrl = "~/Images/tabb-bo.png";
                hdn_engagementid.Value = engmentid.ToString();
                hdn_schedulecount.Value = schcount;
                ValidationSummary mastersummary = (ValidationSummary)this.Master.FindControl("val_summarymaster");
                mastersummary.Enabled = false;

                lbl_msg.Text = "";


                if (engmentid > 0 && schcount != "0")
                {
                    if (dealdata().Rows.Count > 0)
                    {
                        ddlPerformancesload(engmentid);

                    }
                    else
                    {
                        lbl_bo.Text = "Please create Engagement Deal first";
                        lbl_bo.ForeColor = System.Drawing.Color.Red;
                        diveboxofc.Visible = false;
                    }

                    //div_bo.Visible = true;
                }
                else
                {
                    diveboxofc.Visible = false;
                }
            }
        }
        public DataTable dealdata()
        {
            DataTable dtdeal = new DataTable();
            ofcobj = new BoxOfficeData();
            dtdeal = ofcobj.Getboxofcdatafromdeal(Convert.ToInt32(hdn_engagementid.Value));
            return dtdeal;
        }
        public bool checkvalid()
        {
            char[] trimdlr = { '$', '%', ',', ' ' };
            Decimal paid_atten = 0, comp = 0, ps_seat = 0;
            paid_atten = (txtPaidAttendance.Text != "") ? Convert.ToDecimal(txtPaidAttendance.Text.Trim(trimdlr)) : 0;
            comp = (txtComps.Text != "") ? Convert.ToDecimal(txtComps.Text.Trim(trimdlr)) : 0;
            ps_seat = (lblCapacityValue.Text != "") ? Convert.ToDecimal(lblCapacityValue.Text.Trim(trimdlr)) : 0;
            if ((paid_atten + comp) > ps_seat)
            {
                lbl_msg.Text = "Sum of Paid attendence and Comps should be less than or equal to Capacity!";
                lbl_msg.ForeColor = System.Drawing.Color.Orange;
                return false;
            }
            return true;
        }
        public void SaveData()
        {
            try
            {
                
                if (checkvalid() == false)
                {
                    txtPaidAttendance.Focus();
                    return;
                }
                bool expflg = false;
                if (hdninitial_paidattn.Value.AutoformatInt() != txtPaidAttendance.Text.AutoformatInt() || hdninitial_dropcount.Value.AutoformatInt() != txtDropcount.Text.AutoformatInt())
                {
                    expflg = true;
                }
                if (expflg == true)
                {
                    string alertmsg = "Expense insurance actual or ticket printing actual updated due to Attendance or Drop Count change. Please check";
                  
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "Subscriptioncalc();JQalertbox('" + alertmsg + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "Subscriptioncalc();", true);
                }

               
                //if (hidfval.Value.Length > 0)
                //{
                //    lbl_msg.Text = "Negative value not allowed";
                //    lbl_msg.ForeColor = System.Drawing.Color.Red;
                //    return;
                //}

                char[] chDlr = { '$', '%', ',', ' ' };
                EngagementMaster EngmtMaster = (EngagementMaster)Page.Master;

                if (hdn_engagementid.Value != "0" && hdn_engagementid.Value != "")
                {
                    if (hdn_schedulecount.Value != "0" && hdn_schedulecount.Value != "")
                    {
                        Nullable<decimal> bo_drop_count = null, bo_gross_sales = null, bo_facility_fee = null, bo_tax1 = null, bo_tax_amount_over = null,
                            bo_tax_facility_comm = null, bo_sub_gross_rcpt = null, bo_sub_comm_ptg = null, bo_ph_gross_rcpt = null, bo_ph_comm_ptg = null, bo_web_gross_rcpt = null,
                            bo_web_comm_ptg = null, bo_cc_gross_rcpt = null, bo_cc_comm_ptg = null, bo_outlet_gross_rcpt = null, bo_outlet_comm_ptg = null, bo_single_tix_gross_rcpt = null,
                            bo_single_tix_comm_ptg = null, bo_small_group_gross_rcpt = null, bo_small_group_comm_ptg = null, bo_large_group_gross_rcpt = null,
                            bo_large_group_comm_ptg = null, bo_other_per_gross_rcpt = null, bo_other_per_comm_ptg = null, bo_other_usd_gross_rcpt = null, bo_other_usd_comm_ptg = null;
                        Nullable<decimal> bo_paid_attendance = null, bo_comps = null, bo_sub_t_sold = null, bo_ph_t_sold = null, bo_web_t_sold = null, bo_cc_t_sold = null,
                            bo_outlet_t_sold = null, bo_single_tix_t_sold = null, bo_small_group_t_sold = null, bo_large_group_t_sold = null,
                            bo_other_per_t_sold = null, bo_other_usd_t_sold = null, bo_other_3_t_sold = null, bo_other_3_gross_rcpt = null
                            , bo_other_4_t_sold = null, bo_other_4_gross_rcpt = null, bo_other_5_t_sold = null, bo_other_5_gross_rcpt = null,
                            bo_other_3_comm_ptg = null, bo_other_4_comm_ptg = null, bo_other_5_comm_ptg = null;

                        bo_gross_sales = txtgrossales.Text.AutoformatDecimal();
                        bo_facility_fee = txtFacilityfeeoneachticket.Text.AutoformatDecimal();
                        bo_tax1 = decimal.Parse(txtTax1.Text, CultureInfo.InvariantCulture);
                        bo_tax_amount_over = txtTaxAmountOver.Text.AutoformatDecimal();
                        bo_tax_facility_comm = txtTaxFacilityfeecoms.Text.AutoformatDecimal();
                        bo_sub_gross_rcpt = txtSubscriptionreceipts.Text.AutoformatDecimal();
                        bo_sub_comm_ptg = txtSubsSalesPerc.Text.AutoformatDecimal();

                        bo_ph_gross_rcpt = txtPhoneGrossReceipts.Text.AutoformatDecimal();
                        bo_ph_comm_ptg = txtPhonePerc.Text.AutoformatDecimal();

                        bo_web_gross_rcpt = txtInternetGrossRecpt.Text.AutoformatDecimal();
                        bo_web_comm_ptg = txtInternetPerc.Text.AutoformatDecimal();

                        bo_cc_gross_rcpt = txtCreditGrossReceipts.Text.AutoformatDecimal();
                        bo_cc_comm_ptg = txtCreditPerc.Text.AutoformatDecimal();

                        bo_outlet_gross_rcpt = txtRemoteGrossReceipts.Text.AutoformatDecimal();
                        bo_outlet_comm_ptg = txtRemotePerc.Text.AutoformatDecimal();

                        bo_single_tix_gross_rcpt = txtSingletixtGrossReceipts.Text.AutoformatDecimal();
                        bo_single_tix_comm_ptg = txtSingletixPerc.Text.AutoformatDecimal();

                        bo_small_group_gross_rcpt = txtGroupGrossReceipts.Text.AutoformatDecimal();
                        bo_small_group_comm_ptg = txtGroupPerc.Text.AutoformatDecimal();

                        bo_large_group_gross_rcpt = txtGroup1grossReceipts.Text.AutoformatDecimal();
                        bo_large_group_comm_ptg = txtGroup1Perc.Text.AutoformatDecimal();

                        bo_other_per_gross_rcpt = txtOtherPercGrossReceipts.Text.AutoformatDecimal();
                        bo_other_per_comm_ptg = txtOtherPercPerc.Text.AutoformatDecimal();

                        bo_other_usd_gross_rcpt = txtOtherDollGrossReceipts.Text.AutoformatDecimal();
                        bo_other_usd_comm_ptg = txtOtherDollPerc.Text.AutoformatDecimal();

                        bo_drop_count = txtDropcount.Text.AutoformatDecimal();
                        bo_paid_attendance = txtPaidAttendance.Text.AutoformatDecimal();
                        bo_comps = txtComps.Text.AutoformatDecimal();
                        bo_sub_t_sold = txtSubscriptionsold.Text.AutoformatDecimal();
                        bo_ph_t_sold = txtPhoneTcktSold.Text.AutoformatDecimal();
                        bo_web_t_sold = txtInternetTcktSold.Text.AutoformatDecimal();
                        bo_cc_t_sold = txtCreditcrdTcktSold.Text.AutoformatDecimal();
                        bo_outlet_t_sold = txtRemoteoutletTcktSold.Text.AutoformatDecimal();
                        bo_single_tix_t_sold = txtSingletixtcktSold.Text.AutoformatDecimal();
                        bo_small_group_t_sold = txtGroupTicktSold.Text.AutoformatDecimal();
                        bo_large_group_t_sold = txtGroup1TicktSold.Text.AutoformatDecimal();
                        bo_other_per_t_sold = txtOtherPercTcktSold.Text.AutoformatDecimal();
                        bo_other_usd_t_sold = txtOtherDollTcktSold.Text.AutoformatDecimal();

                        bo_other_3_t_sold = txtOther3TcktSold.Text.AutoformatDecimal();
                        bo_other_3_gross_rcpt = txtOther3GrossReceipts.Text.AutoformatDecimal();
                        bo_other_3_comm_ptg = txtOther3Perc.Text.AutoformatDecimal();
                        bo_other_4_t_sold = txtOther4TcktSold.Text.AutoformatDecimal();
                        bo_other_4_gross_rcpt = txtOther4GrossReceipts.Text.AutoformatDecimal();
                        bo_other_4_comm_ptg = txtOther4Perc.Text.AutoformatDecimal();
                        bo_other_5_t_sold = txtOther5TcktSold.Text.AutoformatDecimal();
                        bo_other_5_gross_rcpt = txtOther5GrossReceipts.Text.AutoformatDecimal();
                        bo_other_5_comm_ptg = txtOther5Perc.Text.AutoformatDecimal();

                        ofcobj = new BoxOfficeData();
                        int output;
                        if (ddlPerformance.Visible == true)
                        {
                            output = ofcobj.BoxOffice_Insert(out boid,Convert.ToInt32(hdn_engagementid.Value),
                            Convert.ToInt32(ddlPerformance.SelectedItem.Value),
                            bo_gross_sales, bo_drop_count, bo_paid_attendance, bo_comps, bo_sub_t_sold,
                            bo_sub_gross_rcpt, bo_ph_t_sold, bo_ph_gross_rcpt, bo_web_t_sold, bo_web_gross_rcpt,
                             bo_cc_t_sold, bo_cc_gross_rcpt, bo_outlet_t_sold, bo_outlet_gross_rcpt,
                             bo_single_tix_t_sold, bo_single_tix_gross_rcpt, bo_small_group_t_sold,
                             bo_small_group_gross_rcpt, bo_large_group_t_sold, bo_large_group_gross_rcpt,
                             bo_other_per_t_sold, bo_other_per_gross_rcpt, bo_other_usd_t_sold,
                            bo_other_usd_gross_rcpt,bo_other_3_t_sold, bo_other_3_gross_rcpt,
                            bo_other_4_t_sold, bo_other_4_gross_rcpt,
                            bo_other_5_t_sold, bo_other_5_gross_rcpt
                            );
                            if (output == 1001)
                            {
                                lbl_msg.Text = "Engagement Box Office created successfully";
                                lbl_msg.ForeColor = System.Drawing.Color.Green;
                            }
                            else if (output == 1002)
                            {
                                lbl_msg.Text = "Engagement Box Office updated successfully";
                                lbl_msg.ForeColor = System.Drawing.Color.Green;
                            }
                        }
                        else
                        {
                            foreach (ListItem list in chklstPerformance.Items)
                            {
                                if (list.Selected == true && list.Value != "0")
                                {
                                    output = ofcobj.BoxOffice_Insert(out boid,Convert.ToInt32(hdn_engagementid.Value),
                                        Convert.ToInt32(list.Value),
                                        bo_gross_sales, bo_drop_count, bo_paid_attendance, bo_comps, bo_sub_t_sold,
                                        bo_sub_gross_rcpt, bo_ph_t_sold, bo_ph_gross_rcpt, bo_web_t_sold, bo_web_gross_rcpt,
                                         bo_cc_t_sold, bo_cc_gross_rcpt, bo_outlet_t_sold, bo_outlet_gross_rcpt,
                                         bo_single_tix_t_sold, bo_single_tix_gross_rcpt, bo_small_group_t_sold,
                                         bo_small_group_gross_rcpt, bo_large_group_t_sold, bo_large_group_gross_rcpt,
                                         bo_other_per_t_sold, bo_other_per_gross_rcpt, bo_other_usd_t_sold,
                                        bo_other_usd_gross_rcpt);
                                    if (output == 1001)
                                    {
                                        lbl_msg.Text = "Engagement Box Office created successfully";
                                        lbl_msg.ForeColor = System.Drawing.Color.Green;
                                    }
                                    else if (output == 1002)
                                    {
                                        lbl_msg.Text = "Engagement Box Office updated successfully";
                                        lbl_msg.ForeColor = System.Drawing.Color.Green;
                                    }
                                }
                            }
                        }

                        if (chkboxOverride.Checked)
                        {
                            SaveOverride(boid); 
                        }

                        ddlPerformancesload(Convert.ToInt32(hdn_engagementid.Value));
                        //ddlPerformancesload(Convert.ToInt32(hdn_engagementid.Value));
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "Subscriptioncalc();", true);

                    }
                    else
                    {
                        lbl_msg.Text = "Please create Engagement Schedule first";
                        lbl_msg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "Subscriptioncalc();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "JQalertbox('actual!');", true);
            }
        }
        public void DeleteData(string flag)
        {
            EngagementMaster EngmtMaster = (EngagementMaster)Page.Master;
            int delflag = EngmtMaster.DeleteEngagement(flag);
            lbl_msg.Text = "Engagement Deleted successfully";
            lbl_msg.ForeColor = System.Drawing.Color.Green;
        }
        public void ddlPerformancesload(int engmntid)
        {
            string ddlval = ddlPerformance.SelectedItem.Value;
            dtperformancelist = new DataTable();
            ofcobj = new BoxOfficeData();
            //dtperformancelist = ofcobj.GetPerformancelist(engmntid);
            dtperformancelist = ofcobj.LoadPerformancelist(engmntid);
            ViewState["dtperformance"] = dtperformancelist;
            ddlPerformance.DataSource = dtperformancelist;
            ddlPerformance.DataTextField = "schedule_type";
            ddlPerformance.DataValueField = "schedule_id";
            ddlPerformance.DataBind();
            chklstPerformance.DataSource = dtperformancelist;
            chklstPerformance.DataTextField = "schedule_type";
            chklstPerformance.DataValueField = "schedule_id";
            chklstPerformance.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "All";
            lst.Value = "0";
            chklstPerformance.Items.Insert(0, lst);

            DataTable ds = new DataTable();
            ofcobj = new BoxOfficeData();
            ddlPerformance.SelectedIndex = ddlPerformance.Items.IndexOf(ddlPerformance.Items.FindByValue(ddlval));
            if (ddlPerformance.SelectedIndex == -1)
                ddlPerformance.SelectedIndex = 0;
            //ds = ofcobj.Loadboxofficedata(Convert.ToInt32(hdn_engagementid.Value), Convert.ToInt32(ddlPerformance.SelectedItem.Value.ToString()));
            ds = ofcobj.Loadboxofficedata(Convert.ToInt32(hdn_engagementid.Value), Convert.ToInt32(ddlPerformance.SelectedItem.Value.ToString()));            
            if (ds.Rows.Count > 0)
            {
                if (Convert.ToInt32(Convert.ToString(ds.Rows[0]["BOCount"])) > 0)
                {
                    pnlcolumn.Visible = false;
                    ddlPerformance.Visible = true;
                    txtPerformance.Visible = false;

                }
                else
                {
                    tdcopyfrom.Visible = false;
                    pnlcolumn.Visible = true;
                    ddlPerformance.Visible = false;
                    txtPerformance.Visible = true;
                    lblcopyfrom.Visible = false;
                    pnlperf.Visible = false;
                }


                filldatacalculations(ds, "n");
                rewriteOverrideValues((Convert.ToInt32(Convert.ToString(ds.Rows[0]["BO_ID"]))));
            }
            else
            {
                tdcopyfrom.Visible = false;
                pnlcolumn.Visible = true;
                ddlPerformance.Visible = false;
                txtPerformance.Visible = true;
                lblcopyfrom.Visible = false;
                pnlperf.Visible = false;
                LoadPerformanceCopy(Convert.ToInt32(hdn_engagementid.Value));
              
            }
        }

        public void filldatacalculations(DataTable value, string copyfrom)
        {
            hdn_deal_ff_amt.Value = value.Rows[0]["deal_facility_fee_amt"].ToString();
            hdn_deal_ff_unit.Value = value.Rows[0]["deal_facility_fee_unit"].ToString();
            hdn_deal_ff_IO.Value = value.Rows[0]["deal_facility_fee_inlcude"].ToString();
            hdn_dealtaxptg_ff.Value = value.Rows[0]["deal_tax_ptg_ff"].ToString();
            hdn_deal_tax_ptg_include.Value = value.Rows[0]["deal_tax_ptg_include"].ToString();
            if (value.Rows[0]["BO_Schedule_Status"].ToString().ToLower() == "n")
            {
                tdcopyfrom.Visible=(Convert.ToInt32(Convert.ToString(value.Rows[0]["BOCount"])) == 0)?false:true;
                lblcopyfrom.Visible = true;
                pnlperf.Visible = true;
                LoadPerformanceCopy(Convert.ToInt32(hdn_engagementid.Value));
            }
            else if (copyfrom != "y")
            {
                lblcopyfrom.Visible = true;
                pnlperf.Visible = true;
                tdcopyfrom.Visible = false;
            }
            //string msg = "Please Enter Tax, Tax Amount Over, Facility Fee On Each Ticket, Tax/Facility Fee, Subscription Sales, Phone Sales, Internet Sales, Credit Card Sales, Remote Sales, Single Tickets, Group 1, Group 2,Other 1,Other 2 in Engagement Deal first";
            if (Convert.ToInt32(value.Rows[0]["BoScheduleCount"]) > 0)
            {
                lbl_staticmsg.Text = "Please enter data for all Performances, otherwise the reports will display the incorrect values.";
            }
            else
            {
                lbl_staticmsg.Text = string.Empty;
            }
            lblScheduleDate.Text = Convert.ToDateTime(value.Rows[0]["SCHEDULE_DATE"].ToString()).ToString("dddd,MMM dd, yyyy");
            lblScheduleTime.Text = value.Rows[0]["SCHEDULE_ST_TIME"].ToString();
            lblCapacityValue.Text = value.Rows[0]["PS_SEATS_SINGLE"].ToString();
            //txtgrossales.Text =Convert.ToInt32(value[0].ItemArray[10]).ToString("C");
            txtgrossales.Text = value.Rows[0]["BO_GROSS_SALES"].ToString();
            txtDropcount.Text = value.Rows[0]["DROP_COUNT"].ToString();
           

            hdninitial_dropcount.Value = value.Rows[0]["DROP_COUNT"].ToString();
            txtPaidAttendance.Text = value.Rows[0]["PAID_ATTENDANCE"].ToString();
            hdninitial_paidattn.Value = value.Rows[0]["PAID_ATTENDANCE"].ToString();


            txtComps.Text = value.Rows[0]["BO_COMPS"].ToString();
            //txtFacilityfeeoneachticket.Text =Convert.ToInt32(value[0].ItemArray[14]).ToString("C");
            txtSubscriptionsold.Text = value.Rows[0]["BO_SUB_T_SOLD"].ToString();
            txtSubscriptionreceipts.Text = value.Rows[0]["BO_SUB_GROSS_RCPT"].ToString();
            txtPhoneTcktSold.Text = value.Rows[0]["BO_PH_T_SOLD"].ToString();
            txtPhoneGrossReceipts.Text = value.Rows[0]["BO_PH_GROSS_RCPT"].ToString();
            txtInternetTcktSold.Text = value.Rows[0]["BO_WEB_T_SOLD"].ToString();
            txtInternetGrossRecpt.Text = value.Rows[0]["BO_WEB_GROSS_RCPT"].ToString();
            txtCreditcrdTcktSold.Text = value.Rows[0]["BO_CC_T_SOLD"].ToString();
            txtCreditGrossReceipts.Text = value.Rows[0]["BO_CC_GROSS_RCPT"].ToString();
            txtRemoteoutletTcktSold.Text = value.Rows[0]["BO_OUTLET_T_SOLD"].ToString();
            txtRemoteGrossReceipts.Text = value.Rows[0]["BO_OUTLET_GROSS_RCPT"].ToString();
            txtSingletixtcktSold.Text = value.Rows[0]["BO_SINGLE_TIX_T_SOLD"].ToString();
            txtSingletixtGrossReceipts.Text = value.Rows[0]["BO_SINGLE_TIX_GROSS_RCPT"].ToString();
            txtGroupTicktSold.Text = value.Rows[0]["BO_SMALL_GROUP_T_SOLD"].ToString();
            txtGroupGrossReceipts.Text = value.Rows[0]["BO_SMALL_GROUP_GROSS_RCPT"].ToString();
            txtGroup1TicktSold.Text = value.Rows[0]["BO_LARGE_GROUP_T_SOLD"].ToString();
            txtGroup1grossReceipts.Text = value.Rows[0]["BO_LARGE_GROUP_GROSS_RCPT"].ToString();
            txtOtherPercTcktSold.Text = value.Rows[0]["BO_OTHER_PER_T_SOLD"].ToString();
            txtOtherPercGrossReceipts.Text = value.Rows[0]["BO_OTHER_PER_GROSS_RCPT"].ToString();
            txtOtherDollTcktSold.Text = value.Rows[0]["BO_OTHER_USD_T_SOLD"].ToString();
            txtOtherDollGrossReceipts.Text = value.Rows[0]["BO_OTHER_USD_GROSS_RCPT"].ToString();

            txtOther3TcktSold.Text = value.Rows[0]["BO_OTHER_3_T_SOLD"].ToString();
            txtOther3GrossReceipts.Text = value.Rows[0]["BO_OTHER_3_GROSS_RCPT"].ToString();
            txtOther4TcktSold.Text = value.Rows[0]["BO_OTHER_4_T_SOLD"].ToString();
            txtOther4GrossReceipts.Text = value.Rows[0]["BO_OTHER_4_GROSS_RCPT"].ToString();
            txtOther5TcktSold.Text = value.Rows[0]["BO_OTHER_5_T_SOLD"].ToString();
            txtOther5GrossReceipts.Text = value.Rows[0]["BO_OTHER_5_GROSS_RCPT"].ToString();

            lbltax2DollPerc.Text = value.Rows[0]["DEAL_TAX2_PTG"].ToString();

            txtTaxFacilityfeecoms.Text = (String.IsNullOrEmpty(value.Rows[0]["deal_othr_bo_amt"].ToString())) ? "0" : value.Rows[0]["deal_othr_bo_amt"].ToString();

            if (value.Rows[0]["deal_facility_fee_unit"].ToString().Length > 0)
            {
                string ffeachtik = (String.IsNullOrEmpty(value.Rows[0]["deal_facility_fee_unit"].ToString())) ? "0" : value.Rows[0]["deal_facility_fee_unit"].ToString();
                hidFdollar.Value = "";
                txtFacilityfeeoneachticket.Text = ffeachtik;
                hidFPerc.Value = ffeachtik;
                txtFacilityfeeoneachticket.CssClass = "Percentage";
            }
            else if (value.Rows[0]["deal_facility_fee_amt"].ToString().Length > 0)
            {
                string ffamt = (String.IsNullOrEmpty(value.Rows[0]["deal_facility_fee_amt"].ToString())) ? "0" : value.Rows[0]["deal_facility_fee_amt"].ToString();
                txtFacilityfeeoneachticket.Text = ffamt;
                hidFdollar.Value = ffamt;
                hidFPerc.Value = "";
                txtFacilityfeeoneachticket.CssClass = "Dollar";
            }
            else
            {
                txtFacilityfeeoneachticket.CssClass = "Percentage";
                //                
            }
            txtTax1.Text = (string.IsNullOrEmpty(value.Rows[0]["deal_tax_ptg"].ToString())) ? "0" : value.Rows[0]["deal_tax_ptg"].ToString();
            txtTaxAmountOver.Text = (string.IsNullOrEmpty(value.Rows[0]["deal_tax_amt_over"].ToString())) ? "0" : value.Rows[0]["deal_tax_amt_over"].ToString();
            txtSubsSalesPerc.Text = (string.IsNullOrEmpty(value.Rows[0]["deal_sub_sales_comm"].ToString())) ? "0" : value.Rows[0]["deal_sub_sales_comm"].ToString();
            txtPhonePerc.Text = (string.IsNullOrEmpty(value.Rows[0]["deal_ph_sales_comm"].ToString())) ? "0" : value.Rows[0]["deal_ph_sales_comm"].ToString();
            txtInternetPerc.Text = (string.IsNullOrEmpty(value.Rows[0]["deal_web_sales_comm"].ToString())) ? "0" : value.Rows[0]["deal_web_sales_comm"].ToString();
            txtCreditPerc.Text = (string.IsNullOrEmpty(value.Rows[0]["deal_cc_sales_comm"].ToString())) ? "0" : value.Rows[0]["deal_cc_sales_comm"].ToString();
            txtRemotePerc.Text = (string.IsNullOrEmpty(value.Rows[0]["deal_remote_sales_comm"].ToString())) ? "0" : value.Rows[0]["deal_remote_sales_comm"].ToString();
            txtSingletixPerc.Text = (string.IsNullOrEmpty(value.Rows[0]["deal_single_tix_comm"].ToString())) ? "0" : value.Rows[0]["deal_single_tix_comm"].ToString();
            txtGroupPerc.Text = (string.IsNullOrEmpty(value.Rows[0]["deal_grp_sales_comm1"].ToString())) ? "0" : value.Rows[0]["deal_grp_sales_comm1"].ToString();
            txtGroup1Perc.Text = (string.IsNullOrEmpty(value.Rows[0]["deal_grp_sales_comm2"].ToString())) ? "0" : value.Rows[0]["deal_grp_sales_comm2"].ToString();

            if (value.Rows[0]["deal_misc_othr_unit_2"].ToString().Length > 0)
            {
                string miscotherunit2 = (String.IsNullOrEmpty(value.Rows[0]["deal_misc_othr_unit_2"].ToString())) ? "0" : value.Rows[0]["deal_misc_othr_unit_2"].ToString();
                hidOther2Doll.Value = "";
                hidOther2Perc.Value = miscotherunit2;
                txtOtherDollPerc.CssClass = "Percentage";
                txtOtherDollPerc.Text = miscotherunit2;
            }
            else if (value.Rows[0]["deal_misc_othr_amt_2"].ToString().Length > 0)
            {
                string miscotheramt2 = (String.IsNullOrEmpty(value.Rows[0]["deal_misc_othr_amt_2"].ToString())) ? "0" : value.Rows[0]["deal_misc_othr_amt_2"].ToString();
                hidOther2Perc.Value = "";
                txtOtherDollPerc.CssClass = "Dollar";
                hidOther2Doll.Value = miscotheramt2;
                txtOtherDollPerc.Text = miscotheramt2;

            }
            else
            {
                txtOtherDollPerc.CssClass = "Percentage";
                // txtOtherDollPerc.Text = "0";
            }
            if (value.Rows[0]["deal_misc_othr_unit_1"].ToString().Length > 0)
            {
                string miscotherunit1 = (String.IsNullOrEmpty(value.Rows[0]["deal_misc_othr_unit_1"].ToString())) ? "0" : value.Rows[0]["deal_misc_othr_unit_1"].ToString();
                hidOther1Doll.Value = "";
                hidOther1Perc.Value = miscotherunit1;
                txtOtherPercPerc.CssClass = "Percentage";
                txtOtherPercPerc.Text = miscotherunit1;

            }
            else if (value.Rows[0]["deal_misc_othr_amt_1"].ToString().Length > 0)
            {
                string miscotheramt1 = (String.IsNullOrEmpty(value.Rows[0]["deal_misc_othr_amt_1"].ToString())) ? "0" : value.Rows[0]["deal_misc_othr_amt_1"].ToString();
                hidOther1Perc.Value = "";
                txtOtherPercPerc.CssClass = "Dollar";
                hidOther1Doll.Value = miscotheramt1;
                txtOtherPercPerc.Text = miscotheramt1;
            }
            else
            {
                // txtOtherPercPerc.Text = "0";
                txtOtherPercPerc.CssClass = "Percentage";
            }

            if (value.Rows[0]["deal_misc_othr_unit_3"].ToString().Length > 0)
            {
                string miscotherunit3 = (String.IsNullOrEmpty(value.Rows[0]["deal_misc_othr_unit_3"].ToString())) ? "0" : value.Rows[0]["deal_misc_othr_unit_3"].ToString();
                hidOther3Doll.Value = "";
                hidOther3Perc.Value = miscotherunit3;
                txtOther3Perc.CssClass = "Percentage";
                txtOther3Perc.Text = miscotherunit3;

            }
            else if (value.Rows[0]["deal_misc_othr_amt_3"].ToString().Length > 0)
            {
                string miscotheramt3 = (String.IsNullOrEmpty(value.Rows[0]["deal_misc_othr_amt_3"].ToString())) ? "0" : value.Rows[0]["deal_misc_othr_amt_3"].ToString();
                hidOther3Perc.Value = "";
                txtOther3Perc.CssClass = "Dollar";
                hidOther3Doll.Value = miscotheramt3;
                txtOther3Perc.Text = miscotheramt3;
            }
            else
            {
                txtOther3Perc.CssClass = "Percentage";
            }

            if (value.Rows[0]["deal_misc_othr_unit_4"].ToString().Length > 0)
            {
                string miscotherunit4 = (String.IsNullOrEmpty(value.Rows[0]["deal_misc_othr_unit_4"].ToString())) ? "0" : value.Rows[0]["deal_misc_othr_unit_4"].ToString();
                hidOther4Doll.Value = "";
                hidOther4Perc.Value = miscotherunit4;
                txtOther4Perc.CssClass = "Percentage";
                txtOther4Perc.Text = miscotherunit4;

            }
            else if (value.Rows[0]["deal_misc_othr_amt_4"].ToString().Length > 0)
            {
                string miscotheramt4 = (String.IsNullOrEmpty(value.Rows[0]["deal_misc_othr_amt_4"].ToString())) ? "0" : value.Rows[0]["deal_misc_othr_amt_4"].ToString();
                hidOther4Perc.Value = "";
                txtOther4Perc.CssClass = "Dollar";
                hidOther4Doll.Value = miscotheramt4;
                txtOther4Perc.Text = miscotheramt4;
            }
            else
            {
                txtOther4Perc.CssClass = "Percentage";
            }

            if (value.Rows[0]["deal_misc_othr_unit_5"].ToString().Length > 0)
            {
                string miscotherunit5 = (String.IsNullOrEmpty(value.Rows[0]["deal_misc_othr_unit_5"].ToString())) ? "0" : value.Rows[0]["deal_misc_othr_unit_5"].ToString();
                hidOther5Doll.Value = "";
                hidOther5Perc.Value = miscotherunit5;
                txtOther5Perc.CssClass = "Percentage";
                txtOther5Perc.Text = miscotherunit5;

            }
            else if (value.Rows[0]["deal_misc_othr_amt_5"].ToString().Length > 0)
            {
                string miscotheramt5 = (String.IsNullOrEmpty(value.Rows[0]["deal_misc_othr_amt_5"].ToString())) ? "0" : value.Rows[0]["deal_misc_othr_amt_5"].ToString();
                hidOther5Perc.Value = "";
                txtOther5Perc.CssClass = "Dollar";
                hidOther5Doll.Value = miscotheramt5;
                txtOther5Perc.Text = miscotheramt5;
            }
            else
            {
                txtOther5Perc.CssClass = "Percentage";
            }
            //if (String.IsNullOrEmpty(value.Rows[0]["deal_misc_othr_amt_1"].ToString()) && String.IsNullOrEmpty(value.Rows[0]["deal_misc_othr_unit_1"].ToString()))
            //{
            //    lbl_bo.Text = msg;
            //    lbl_bo.ForeColor = System.Drawing.Color.Red;
            //    diveboxofc.Visible = false;
            //    return;
            //}
            lbl_bo.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "Subscriptioncalc();", true);
        }
        protected void ddlPerformance_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtComps.Text = "";
            txtDropcount.Text = "";
            txtPaidAttendance.Text = "";
            txtgrossales.Text = "";
            DataTable ds = new DataTable();
            ofcobj = new BoxOfficeData();
            ds = ofcobj.Loadboxofficedata(Convert.ToInt32(hdn_engagementid.Value), Convert.ToInt32(ddlPerformance.SelectedItem.Value.ToString()));
            if (ds.Rows.Count > 0)
            {
                filldatacalculations(ds, "n");
            }
            else
            {
                LoadPerformanceCopy(Convert.ToInt32(hdn_engagementid.Value));
            }
        }
        public void LoadPerformanceCopy(Int32 engtid)
        {
            dtperformancelist = new DataTable();
            dtperformancelist = objmst.GetPerformance_Copy(engtid, "BO");
            rbtnlstperformance.DataSource = dtperformancelist;
            rbtnlstperformance.DataTextField = "SCHEDULE_TYPE";
            rbtnlstperformance.DataValueField = "SCHEDULE_ID";
            rbtnlstperformance.DataBind();
        }

        protected void rbtnlstperformance_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ds = ofcobj.Loadboxofficedata(Convert.ToInt32(hdn_engagementid.Value), Convert.ToInt32(rbtnlstperformance.SelectedItem.Value.ToString()));
            if (ds.Rows.Count > 0)
            {
                filldatacalculations(ds, "y");
            }
        }

        protected void chklstPerformance_DataBound(object sender, EventArgs e)
        {

            int i = chklstPerformance.Items.Count;
            int j = 0;
            while (j < i)
            {
                if (j % 2 == 0)
                {
                    chklstPerformance.Items[j].Attributes.Add("class", "BlueSpan");
                }
                else
                {
                    chklstPerformance.Items[j].Attributes.Add("class", "YellowSpan");
                   
                }
                j++;
            
            }

           

        }

        protected void chkboxOverride_CheckedChanged(object sender, EventArgs e)
        {
            if (chkboxOverride.Checked)
            {
                isCheckOverride(false);
                hdn_checkoverride.Value = "y";
            }
            else
            {
                isCheckOverride(true);
                hdn_checkoverride.Value = "n";
            }


        }




        private void isCheckOverride(bool isOver)
        {
            
            /*****Facility Fee *********************/
            lblSubscriptionfacilityfee.ReadOnly = isOver;
            lblPhoneFacilityfee.ReadOnly = isOver;
            lblInternetFacltyFee.ReadOnly = isOver;
            lblCreditfacilityfee.ReadOnly = isOver;
            lblRemotefacilityfee.ReadOnly = isOver;
            lblSingletixfacilityfee.ReadOnly = isOver;
            lblGroupsfacilityfee.ReadOnly = isOver;
            lblGroup1facilityfee.ReadOnly = isOver;
            lblOtherPercfacilityfee.ReadOnly = isOver;
            lblOtherDollfacilityfee.ReadOnly = isOver;
            lblOther3facilityfee.ReadOnly = isOver;
            lblOther4facilityfee.ReadOnly = isOver;
            lblOther5facilityfee.ReadOnly = isOver;

            /***Tax **************************/
            lblSubscriptionAmusementtax.ReadOnly = isOver;
            lblPhoneAmusementtax.ReadOnly = isOver;
            lblInternetAmusemntTax.ReadOnly = isOver;
            lblCreditAmusementtax.ReadOnly = isOver;
            lblRemoteAmusementtax.ReadOnly = isOver;
            lblSingletixAmusementtax.ReadOnly = isOver;
            lblGroupAmusementtax.ReadOnly = isOver;
            lblGroup1Amusementtax.ReadOnly = isOver;
            lblOtherPercAmusementtax.ReadOnly = isOver;
            lblOtherDollAmusementtax.ReadOnly = isOver;
            lblOther3Amusementtax.ReadOnly = isOver;
            lblOther4Amusementtax.ReadOnly = isOver;
            lblOther5Amusementtax.ReadOnly = isOver;

            /*****Net Tax ****************************/
            lblSubsSalesNetcommission.ReadOnly = isOver;
            lblPhoneNetCommsn.ReadOnly = isOver;
            lblInternetNetCommsn.ReadOnly = isOver;
            lblCreditNetCommsn.ReadOnly = isOver;
            lblRemoteNetCommsn.ReadOnly = isOver;
            lblSingletixNetCommsn.ReadOnly = isOver;
            lblNetCommsn.ReadOnly = isOver;
            lblGroup1NetCommsn.ReadOnly = isOver;
            lblOtherPercNetCommsn.ReadOnly = isOver;
            lblOOtherDollNetCommsn.ReadOnly = isOver;
            lblOOther3NetCommsn.ReadOnly = isOver;
            lblOOther4NetCommsn.ReadOnly = isOver;
            lblOOther5NetCommsn.ReadOnly = isOver;            

            /********** Net Facility Fee ***************/

            lblSubsTaxfacltycomsn.ReadOnly = isOver;
            lblTaxfacltyfeecmssn.ReadOnly = isOver;
            lblInternetFacltyfeecmssn.ReadOnly = isOver;
            lblCreditTaxFacltyfeecmssn.ReadOnly = isOver;
            lblRemoteTaxFacltyfeecmssn.ReadOnly = isOver;
            lblSingletixtaxfacltyfeecmssn.ReadOnly = isOver;
            lblGroupfeecmssn.ReadOnly = isOver;
            lblGroup1facltyfeecmssn.ReadOnly = isOver;
            lblOtherPercfacltyfeecmssn.ReadOnly = isOver;
            lblOtherDollfacltyfeecmssn.ReadOnly = isOver;
            lblOther3facltyfeecmssn.ReadOnly = isOver;
            lblOther4facltyfeecmssn.ReadOnly = isOver;
            lblOther5facltyfeecmssn.ReadOnly = isOver;            


        
        }

        private void SaveOverride(int boid)
        {
            BoxofficeOvrr bo = new BoxofficeOvrr();
            bo.bo_override = "Y";
            // Subscription
              
                bo.bo_sub_ff =objcf.ToDecimal(lblSubscriptionfacilityfee.Text);   
                bo.bo_sub_tax1 =objcf.ToDecimal(lblSubscriptionAmusementtax.Text);
                bo.bo_sub_net_comm  =objcf.ToDecimal(lblSubsSalesNetcommission.Text);  
                bo.bo_sub_tax_ff_comm =objcf.ToDecimal(lblSubsTaxfacltycomsn.Text);

          // Phone
                bo.bo_ph_ff   =objcf.ToDecimal(lblPhoneFacilityfee.Text);
                bo.bo_ph_tax1  =objcf.ToDecimal(lblPhoneAmusementtax.Text);
                bo.bo_ph_net_comm  =objcf.ToDecimal(lblPhoneNetCommsn.Text);
                bo.bo_ph_tax_ff_comm  =objcf.ToDecimal(lblTaxfacltyfeecmssn.Text);

         // Internet
                bo.bo_web_ff   =objcf.ToDecimal(lblInternetFacltyFee.Text);
                bo.bo_web_tax1 =objcf.ToDecimal(lblInternetAmusemntTax.Text);
                bo.bo_web_net_comm  =objcf.ToDecimal(lblInternetNetCommsn.Text);
                bo.bo_web_tax_ff_comm=objcf.ToDecimal(lblInternetFacltyfeecmssn.Text);

          // Credit Card
                bo.bo_cc_ff     =objcf.ToDecimal(lblCreditfacilityfee.Text);
                bo.bo_cc_tax1 =objcf.ToDecimal(lblCreditAmusementtax.Text);
                bo.bo_cc_net_comm  =objcf.ToDecimal(lblCreditNetCommsn.Text);  
                bo.bo_cc_tax_ff_comm=objcf.ToDecimal(lblCreditTaxFacltyfeecmssn.Text);
          
            // Remote/Outlet
                bo.bo_outlet_ff     =objcf.ToDecimal(lblRemotefacilityfee.Text);
                bo.bo_outlet_tax1 =objcf.ToDecimal(lblRemoteAmusementtax.Text);
                bo.bo_outlet_net_comm  =objcf.ToDecimal(lblRemoteNetCommsn.Text);   
                bo.bo_outlet_tax_ff_comm=objcf.ToDecimal(lblRemoteTaxFacltyfeecmssn.Text);
            
            // Single Ticket
                bo.bo_single_tix_ff    =objcf.ToDecimal(lblSingletixfacilityfee.Text);
                bo.bo_single_tix_tax1 =objcf.ToDecimal(lblSingletixAmusementtax.Text);
                bo.bo_single_tix_net_comm  =objcf.ToDecimal(lblSingletixNetCommsn.Text);  
                bo.bo_single_tax_ff_comm=objcf.ToDecimal( lblSingletixtaxfacltyfeecmssn.Text);

           // Group 1
                bo.bo_small_group_ff     =objcf.ToDecimal(lblGroupsfacilityfee.Text);
                bo.bo_small_group_tax1 =objcf.ToDecimal(lblGroupAmusementtax.Text);
                bo.bo_small_group_net_comm  =objcf.ToDecimal(lblNetCommsn.Text);
                bo.bo_small_tax_ff_comm = objcf.ToDecimal(lblGroupfeecmssn.Text);
             
           // Group 2
                bo.bo_large_group_ff    =objcf.ToDecimal(lblGroup1facilityfee.Text);
                bo.bo_large_group_tax1 =objcf.ToDecimal(lblGroup1Amusementtax.Text);
                bo.bo_large_group_net_comm=objcf.ToDecimal(lblGroup1NetCommsn.Text);
                bo.bo_large_tax_ff_comm=objcf.ToDecimal(lblGroup1facltyfeecmssn.Text);
               // bo.bo_large_tax_ff_tot_comm=objcf.ToDecimal(

                bo.bo_other_per_ff=objcf.ToDecimal(lblOtherPercfacilityfee.Text);
                bo.bo_other_per_tax1 =objcf.ToDecimal(lblOtherPercAmusementtax.Text);
                bo.bo_other_per_net_comm=objcf.ToDecimal(lblOtherPercNetCommsn.Text);
                bo.bo_other_usd_ff=objcf.ToDecimal(lblOtherDollfacilityfee.Text);
                bo.bo_other_usd_tax1=objcf.ToDecimal(lblOtherDollAmusementtax.Text);
                bo.bo_other_usd_net_comm=objcf.ToDecimal(lblOOtherDollNetCommsn.Text);

                bo.bo_other3_t_sold		=(txtOther3TcktSold.Text != "") ? Convert.ToInt32(txtOther3TcktSold.Text) :  0;
                bo.bo_other3_gross_rcpt = (txtOther3GrossReceipts.Text != "") ? objcf.ToDecimal(txtOther3GrossReceipts.Text)  : 0;
                bo.bo_other3_ff = (lblOther3facilityfee.Text!= "") ? objcf.ToDecimal(lblOther3facilityfee.Text) : 0;
                bo.bo_other3_tax1 = (lblOther3Amusementtax.Text != "") ? objcf.ToDecimal(lblOther3Amusementtax.Text) : 0;
                bo.bo_other3_net_comm = (lblOOther3NetCommsn.Text != "") ? objcf.ToDecimal(lblOOther3NetCommsn.Text) : 0;
                bo.bo_other4_t_sold = (txtOther4TcktSold.Text != "") ? Convert.ToInt32(txtOther4TcktSold.Text) : 0;
                bo.bo_other4_gross_rcpt = (txtOther4GrossReceipts.Text != "") ? objcf.ToDecimal(txtOther4GrossReceipts.Text) : 0;
                bo.bo_other4_ff = (lblOther4facilityfee.Text != "") ? objcf.ToDecimal(lblOther4facilityfee.Text) :0 ;
                bo.bo_other4_tax1 = (lblOther4Amusementtax.Text != "") ? objcf.ToDecimal(lblOther4Amusementtax.Text) :0;
                bo.bo_other4_net_comm = (lblOOther4NetCommsn.Text != "") ? objcf.ToDecimal(lblOOther4NetCommsn.Text) : 0;
                bo.bo_other5_t_sold = (txtOther5TcktSold.Text !="") ? Convert.ToInt32(txtOther5TcktSold.Text) : 0;
                bo.bo_other5_gross_rcpt = (txtOther5GrossReceipts.Text != "") ? objcf.ToDecimal(txtOther5GrossReceipts.Text) : 0;
                bo.bo_other5_ff = (lblOther5facilityfee.Text != "") ? objcf.ToDecimal(lblOther5facilityfee.Text) : 0;
                bo.bo_other5_tax1 = (lblOther5Amusementtax.Text != "" ) ? objcf.ToDecimal(lblOther5Amusementtax.Text) : 0;
                bo.bo_other5_net_comm = (lblOOther5NetCommsn.Text != "") ? objcf.ToDecimal(lblOOther5NetCommsn.Text) : 0;

                ofcobj.BoxOfficeOverrde_Insert(boid, bo);

        }

        private void rewriteOverrideValues(int boid)
        {

            BoxofficeOvrr bo = new BoxofficeOvrr ();
            bo = ofcobj.Getboxofcoverridedat(boid);
            if (bo.bo_override == "Y")
            {
                chkboxOverride.Checked = true;
                isCheckOverride(false);
                hdn_checkoverride.Value = "y";
                lblSubscriptionfacilityfee.Text = Convert.ToString(bo.bo_sub_ff);
                lblSubscriptionAmusementtax.Text = Convert.ToString(bo.bo_sub_tax1);
                lblSubsSalesNetcommission.Text = Convert.ToString(bo.bo_sub_net_comm);
                lblSubsTaxfacltycomsn.Text = Convert.ToString(bo.bo_sub_tax_ff_comm);
                lblPhoneFacilityfee.Text = Convert.ToString(bo.bo_ph_ff);
                lblPhoneAmusementtax.Text = Convert.ToString(bo.bo_ph_tax1);
                lblPhoneNetCommsn.Text = Convert.ToString(bo.bo_ph_net_comm);
                lblTaxfacltyfeecmssn.Text = Convert.ToString(bo.bo_ph_tax_ff_comm);
                lblInternetFacltyFee.Text = Convert.ToString(bo.bo_web_ff);
                lblInternetAmusemntTax.Text = Convert.ToString(bo.bo_web_tax1);
                lblInternetNetCommsn.Text = Convert.ToString(bo.bo_web_net_comm);
                lblInternetFacltyfeecmssn.Text = Convert.ToString(bo.bo_web_tax_ff_comm);
                lblCreditfacilityfee.Text = Convert.ToString(bo.bo_cc_ff);
                lblCreditAmusementtax.Text = Convert.ToString(bo.bo_cc_tax1);
                lblCreditNetCommsn.Text = Convert.ToString(bo.bo_cc_net_comm);
                lblCreditTaxFacltyfeecmssn.Text = Convert.ToString(bo.bo_cc_tax_ff_comm);
                lblRemotefacilityfee.Text = Convert.ToString(bo.bo_outlet_ff);
                lblRemoteAmusementtax.Text = Convert.ToString(bo.bo_outlet_tax1);
                lblRemoteNetCommsn.Text = Convert.ToString(bo.bo_outlet_net_comm);
                lblRemoteTaxFacltyfeecmssn.Text = Convert.ToString(bo.bo_outlet_tax_ff_comm);
                lblSingletixfacilityfee.Text = Convert.ToString(bo.bo_single_tix_ff);
                lblSingletixAmusementtax.Text = Convert.ToString(bo.bo_single_tix_tax1);
                lblSingletixNetCommsn.Text = Convert.ToString(bo.bo_single_tix_net_comm);
                lblSingletixtaxfacltyfeecmssn.Text = Convert.ToString(bo.bo_single_tax_ff_comm);
                lblGroupsfacilityfee.Text = Convert.ToString(bo.bo_small_group_ff);
                lblGroupAmusementtax.Text = Convert.ToString(bo.bo_small_group_tax1);
                lblNetCommsn.Text = Convert.ToString(bo.bo_small_group_net_comm);
                lblGroupfeecmssn.Text = Convert.ToString(bo.bo_small_tax_ff_comm);
                lblGroup1facilityfee.Text = Convert.ToString(bo.bo_large_group_ff);
                lblGroup1Amusementtax.Text = Convert.ToString(bo.bo_large_group_tax1);
                lblGroup1NetCommsn.Text = Convert.ToString(bo.bo_large_group_net_comm);
                lblGroup1facltyfeecmssn.Text = Convert.ToString(bo.bo_large_tax_ff_comm);
                


            }


            lblOtherPercfacilityfee.Text = Convert.ToString(bo.bo_other_per_ff);
            lblOtherPercAmusementtax.Text = Convert.ToString(bo.bo_other_per_tax1);
            lblOtherPercNetCommsn.Text = Convert.ToString(bo.bo_other_per_net_comm);
            lblOtherDollfacilityfee.Text = Convert.ToString(bo.bo_other_usd_ff);
            lblOtherDollAmusementtax.Text = Convert.ToString(bo.bo_other_usd_tax1);
            lblOOtherDollNetCommsn.Text = Convert.ToString(bo.bo_other_usd_net_comm);
            txtOther3TcktSold.Text = Convert.ToString(bo.bo_other3_t_sold);
            txtOther3GrossReceipts.Text = Convert.ToString(bo.bo_other3_gross_rcpt);
            lblOther3facilityfee.Text = Convert.ToString(bo.bo_other3_ff);
            lblOther3Amusementtax.Text = Convert.ToString(bo.bo_other3_tax1);
            lblOOther3NetCommsn.Text = Convert.ToString(bo.bo_other3_net_comm);
            txtOther4TcktSold.Text = Convert.ToString(bo.bo_other4_t_sold);
            txtOther4GrossReceipts.Text = Convert.ToString(bo.bo_other4_gross_rcpt);
            lblOther4facilityfee.Text = Convert.ToString(bo.bo_other4_ff);
            lblOther4Amusementtax.Text = Convert.ToString(bo.bo_other4_tax1);
            lblOOther4NetCommsn.Text = Convert.ToString(bo.bo_other4_net_comm);
            txtOther5TcktSold.Text = Convert.ToString(bo.bo_other5_t_sold);
            txtOther5GrossReceipts.Text = Convert.ToString(bo.bo_other5_gross_rcpt);
            lblOther5facilityfee.Text = Convert.ToString(bo.bo_other5_ff);
            lblOther5Amusementtax.Text = Convert.ToString(bo.bo_other5_tax1);
            lblOOther5NetCommsn.Text = Convert.ToString(bo.bo_other5_net_comm);

        }

       //// protected void lnkbtnAdd_Click(object sender, ImageClickEventArgs e)
       //// {
       //     if (trother1.Style["display"] == "block")
       //     {
       //         if (trother2.Style["display"] == "block")
       //         {

       //             if (trother3.Style["display"] == "block")
       //             {

       //                 if (trother4.Style["display"] == "block")
       //                 {
       //                     if (trother5.Style["display"] == "block")
       //                     {

       //                     }
       //                     else
       //                     {

       //                         trother5.Style.Add("display", "block");
       //                     }

       //                 }
       //                 else
       //                 {
       //                     trother4.Style.Add("display", "block");
       //                 }
       //             }
       //             else
       //             {
       //                 trother3.Style.Add("display", "block");
       //             }
       //         }
       //         else
       //         {
       //             trother2.Style.Add("display", "block");
       //         }

       //     }
       //     else
       //     {
       //         trother1.Style.Add("display", "block");
       //     }

            


       // }


       //// protected void lnkbtnDelete_Click(object sender, ImageClickEventArgs e)
       // {
       //     if (cbother1.Checked == true)
       //     {

       //         txtOtherPercTcktSold.Text = "";
       //         txtOtherPercGrossReceipts.Text = "";
       //         lblOtherPercfacilityfee.Text = "";
       //         lblOtherPercAmusementtax.Text = "";
       //         lblOtherPercNetCommsn.Text = "";
       //         lblOtherPercfacltyfeecmssn.Text="";
       //         trother1.Visible = false;
       //         cbother1.Checked = false;
       //     }
        

       //     if (cbother2.Checked == true)
       //     {
       //         txtOtherDollTcktSold.Text = "";
       //         txtOtherDollGrossReceipts.Text = "";
       //         lblOtherDollfacilityfee.Text = "";
       //         lblOtherDollAmusementtax.Text = "";
       //         lblOOtherDollNetCommsn.Text = "";
       //         lblOtherDollfacltyfeecmssn.Text = "";
       //         trother2.Visible = false;
       //         cbother2.Checked = false;
       //     }

       //     if (cbother3.Checked == true)
       //     {
       //         txtOther3TcktSold.Text = "";
       //         txtOther3GrossReceipts.Text = "";
       //         lblOther3facilityfee.Text = "";
       //         lblOther3Amusementtax.Text = "";
       //         lblOOther3NetCommsn.Text = "";
       //         lblOther3facltyfeecmssn.Text = "";
       //         trother3.Visible = false;
       //         cbother3.Checked = false;

       //     }


       //     if (cbother4.Checked == true)
       //     {
       //         txtOther4TcktSold.Text = "";
       //         txtOther4GrossReceipts.Text = "";
       //         lblOther4facilityfee.Text = "";
       //         lblOther4Amusementtax.Text = "";
       //         lblOOther4NetCommsn.Text = "";
       //         lblOther4facltyfeecmssn.Text = "";
       //         trother4.Visible = false;
       //         cbother4.Checked = false;
       //     }

       //     if (cbother5.Checked == true)
       //     {
       //         txtOther5TcktSold.Text = "";
       //         txtOther5GrossReceipts.Text = "";
       //         lblOther5facilityfee.Text = "";
       //         lblOOther5NetCommsn.Text = "";
       //         trother5.Visible = false;
       //         lblOther5facltyfeecmssn.Text = "";
       //         cbother5.Checked = false;
       //     }


       // }






    }
}