using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoxOfficeLayer;
using System.Data;
using MasterDataLayer;
namespace NTOS
{
    public partial class EngagementDiscount : System.Web.UI.Page, MasterPageSaveInterface
    {
        Label lbl_msg;
        BoxOfficeData ofcobj = new BoxOfficeData();
        MasterData objmst = new MasterData();
        private DataTable dtperformancelist = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            lbl_msg = (Label)this.Master.FindControl("lbl_message");
           (this.Master as EngagementMaster).hidesummary();
            if (!IsPostBack)
            {
                //ddlPerformance.Attributes.Add("onchange", "return chk();");
                int engagementid = Convert.ToInt32(Request.QueryString["engmtid"]);
                Session["search_engmt"] = engagementid.ToString();
                string schcount = Request.QueryString["schcount"];
                //((ImageButton)this.Master.FindControl("imgbtnds")).ImageUrl = "~/Images/tabb-ds.png";
                (this.Master as EngagementMaster).SetActiveTab("lidiscount");
                hdn_engagementid.Value = engagementid.ToString();
                hdn_schedulecount.Value = schcount;
                ValidationSummary mastersummary = (ValidationSummary)this.Master.FindControl("val_summarymaster");
                mastersummary.Enabled = false;
                lbl_msg.Text = "";

                //if (engagementid > 0 && schcount != "0")
                if (engagementid > 0)
                {
                    lbl_bo.Visible = false;
                    loadperformanceddl(engagementid);
                   ddlPerformancesload(engagementid);
                }
                else
                {
                    divDiscount.Visible = false;
                }
                tdcopyfrom.Visible = false;
            }
        }
        public void Reset()
        {
            txtSubs1Perc.Text = ""; txtSubs2Perc.Text = ""; txtSubs3Perc.Text = ""; txtSubs4Perc.Text = ""; txtSubs5Perc.Text = "";
            txtSubs6Perc.Text = ""; txtSubs7Perc.Text = ""; txtSubs8Perc.Text = ""; txtSubs9Perc.Text = ""; txtSubs10Perc.Text = "";
            txtSubs1Tickets.Text = ""; txtSubs2Tickets.Text = ""; txtSubs3Tickets.Text = ""; txtSubs4Tickets.Text = "";
            txtSubs5Tickets.Text = ""; txtSubs6Tickets.Text = ""; txtSubs7Tickets.Text = ""; txtSubs8Tickets.Text = "";
            txtSubs9Tickets.Text = ""; txtSubs10Tickets.Text = ""; txtgrouplessPerc1.Text = ""; txtgrouplessPerc2.Text = "";
            txtgrouplessPerc3.Text = ""; txtgrouplessPerc4.Text = ""; txtgrouplessPerc5.Text = ""; txtgrouplessPerc6.Text = "";
            txtgrouplessPerc7.Text = ""; txtgrouplessPerc8.Text = ""; txtgrouplessPerc9.Text = ""; txtgrouplessPerc10.Text = "";
            txtGrouplessTickets1.Text = ""; txtGrouplessTickets2.Text = ""; txtGrouplessTickets3.Text = ""; txtGrouplessTickets4.Text = "";
            txtGrouplessTickets5.Text = ""; txtGrouplessTickets6.Text = ""; txtGrouplessTickets7.Text = ""; txtGrouplessTickets8.Text = "";
            txtGrouplessTickets9.Text = ""; txtGrouplessTickets10.Text = ""; txtMiscellaneousPerc1.Text = ""; txtMiscellaneousPerc2.Text = "";
            txtMiscellaneousPerc3.Text = ""; txtMiscellaneousPerc4.Text = ""; txtMiscellaneousPerc5.Text = ""; txtMiscellaneousPerc6.Text = "";
            txtMiscellaneousPerc7.Text = ""; txtMiscellaneousPerc8.Text = ""; txtMiscellaneousPerc9.Text = ""; txtMiscellaneousPerc10.Text = "";
            txtMiscellaneousTickets1.Text = ""; txtMiscellaneousTickets2.Text = ""; txtMiscellaneousTickets3.Text = ""; txtMiscellaneousTickets4.Text = "";
            txtMiscellaneousTickets5.Text = ""; txtMiscellaneousTickets6.Text = ""; txtMiscellaneousTickets7.Text = ""; txtMiscellaneousTickets8.Text = "";
            txtMiscellaneousTickets9.Text = ""; txtMiscellaneousTickets10.Text = ""; txtDemandPricing.Text = ""; txtnotes.Text = "";

            int a = 2, b = 2, c = 2;
            for (int i = 1; i < 10; i++)
            {
                TextBox txtsubsPerc = divDiscount.FindControl("txtSubs" + (i + 1).ToString() + "Perc") as TextBox;
                TextBox txtSubsTicks = divDiscount.FindControl("txtSubs" + (i + 1).ToString() + "Tickets") as TextBox;
                Label txtSubslbl = divDiscount.FindControl("lblsubs" + (i + 1).ToString() + "") as Label;
                CheckBox chkbxSubsTk = divDiscount.FindControl("chkbxSubsTk" + (i + 1).ToString() + "") as CheckBox;

                TextBox txtGroupPerc = divDiscount.FindControl("txtgrouplessPerc" + (i + 1).ToString() + "") as TextBox;
                TextBox txtGroupTicks = divDiscount.FindControl("txtGrouplessTickets" + (i + 1).ToString() + "") as TextBox;
                CheckBox chkbxGrouplessTk = divDiscount.FindControl("chkbxGrouplessTk" + (i + 1).ToString() + "") as CheckBox;
                Label txtGrouplbl = divDiscount.FindControl("lblSale" + (i + 1).ToString() + "") as Label;

                TextBox txtMiscPerc = divDiscount.FindControl("txtMiscellaneousPerc" + (i + 1).ToString() + "") as TextBox;
                TextBox txtMiscTicks = divDiscount.FindControl("txtMiscellaneousTickets" + (i + 1).ToString() + "") as TextBox;
                Label txtMisclbl = divDiscount.FindControl("lblMiscellaneous" + (i + 1).ToString() + "") as Label;
                CheckBox chkbxMiscPerc = divDiscount.FindControl("chkbxMiscPerc" + (i + 1).ToString() + "") as CheckBox;
                if (txtsubsPerc.Text.Trim() != "" || txtSubsTicks.Text.Trim() != "")
                {
                    txtsubsPerc.Visible = true;
                    txtSubsTicks.Visible = true;
                    txtSubslbl.Visible = true;
                    chkbxSubsTk.Visible = true;
                    txtSubslbl.Text = Convert.ToString(a);
                    a++;

                }
                else
                {
                    txtsubsPerc.Visible = false;
                    txtSubsTicks.Visible = false;
                    txtSubslbl.Visible = false;
                    chkbxSubsTk.Visible = false;
                }
                if (txtGroupPerc.Text.Trim() != "" || txtGroupTicks.Text.Trim() != "")
                {
                    txtGroupPerc.Visible = true;
                    txtGroupTicks.Visible = true;
                    txtGrouplbl.Visible = true;
                    chkbxGrouplessTk.Visible = true;
                    txtGrouplbl.Text = Convert.ToString(b);
                    b++;
                }
                else
                {
                    txtGroupPerc.Visible = false;
                    txtGroupTicks.Visible = false;
                    txtGrouplbl.Visible = false;
                    chkbxGrouplessTk.Visible = false;
                }
                if (txtMiscPerc.Text.Trim() != "" || txtMiscTicks.Text.Trim() != "")
                {
                    txtMiscPerc.Visible = true;
                    txtMiscTicks.Visible = true;
                    txtMisclbl.Visible = true;
                    chkbxMiscPerc.Visible = true;
                    txtMisclbl.Text = Convert.ToString(c);
                    c++;
                }
                else
                {
                    txtMiscPerc.Visible = false;
                    txtMiscTicks.Visible = false;
                    txtMisclbl.Visible = false;
                    chkbxMiscPerc.Visible = false;
                }

            }

        }
        public void SaveData()
        {
            if (ddlPerformance.Visible == false)
            {
                List<ListItem> items = chklstPerformance.Items.Cast<ListItem>().Where(n => n.Selected).ToList();
                if (items.Count == 0)
                {
                    lbl_message.Text = "Select at least one performance!";
                    return;
                }

            }
            char[] chDlr = { '$', '%', ',', ' ' };
            EngagementMaster EngmtMaster = (EngagementMaster)Page.Master;

            if (hdn_engagementid.Value != "0" && hdn_engagementid.Value != "")
            {
                if (hdn_schedulecount.Value != "0" && hdn_schedulecount.Value != "")
                {
                    Nullable<decimal> dsct_sub1_per = null, dsct_sub2_per = null,
dsct_sub3_per = null,
dsct_sub4_per = null,
dsct_sub5_per = null,
dsct_sub6_per = null,
dsct_sub7_per = null,
dsct_sub8_per = null,
dsct_sub9_per = null,
dsct_sub10_per = null,
dsct_sub1_tickets = null,
dsct_sub2_tickets = null,
dsct_sub3_tickets = null,
dsct_sub4_tickets = null,
dsct_sub5_tickets = null,
dsct_sub6_tickets = null,
dsct_sub7_tickets = null,
dsct_sub8_tickets = null,
dsct_sub9_tickets = null,
dsct_sub10_tickets = null,
dsct_sml_grp_per = null,
dsct_lrg_grp_per = null,
dsct_grp3_per = null,
dsct_grp4_per = null,
dsct_grp5_per = null,
dsct_grp6_per = null,
dsct_grp7_per = null,
dsct_grp8_per = null,
dsct_grp9_per = null,
dsct_grp10_per = null,
dsct_sml_grp_tickets = null,
dsct_lrg_grp_tickets = null,
dsct_grp3_tickets = null,
dsct_grp4_tickets = null,
dsct_grp5_tickets = null,
dsct_grp6_tickets = null,
dsct_grp7_tickets = null,
dsct_grp8_tickets = null,
dsct_grp9_tickets = null,
dsct_grp10_tickets = null,
dsct_misc1_per = null,
dsct_misc2_per = null,
dsct_misc3_per = null,
dsct_misc4_per = null,
dsct_misc5_per = null,
dsct_misc6_per = null,
dsct_misc7_per = null,
dsct_misc8_per = null,
dsct_misc9_per = null,
dsct_misc10_per = null,
dsct_misc1_tickets = null,
dsct_misc2_tickets = null,
dsct_misc3_tickets = null,
dsct_misc4_tickets = null,
dsct_misc5_tickets = null,
dsct_misc6_tickets = null,
dsct_misc7_tickets = null,
dsct_misc8_tickets = null,
dsct_misc9_tickets = null,
dsct_misc10_tickets = null,
dsct_demand_price = null;

                    dsct_sub1_per = (txtSubs1Perc.Text != "") ? Convert.ToDecimal(txtSubs1Perc.Text.Trim(chDlr)) : dsct_sub1_per;
                    dsct_sub2_per = (txtSubs2Perc.Text != "") ? Convert.ToDecimal(txtSubs2Perc.Text.Trim(chDlr)) : dsct_sub2_per;
                    dsct_sub3_per = (txtSubs3Perc.Text != "") ? Convert.ToDecimal(txtSubs3Perc.Text.Trim(chDlr)) : dsct_sub3_per;
                    dsct_sub4_per = (txtSubs4Perc.Text != "") ? Convert.ToDecimal(txtSubs4Perc.Text.Trim(chDlr)) : dsct_sub4_per;
                    dsct_sub5_per = (txtSubs5Perc.Text != "") ? Convert.ToDecimal(txtSubs5Perc.Text.Trim(chDlr)) : dsct_sub5_per;
                    dsct_sub6_per = (txtSubs6Perc.Text != "") ? Convert.ToDecimal(txtSubs6Perc.Text.Trim(chDlr)) : dsct_sub6_per;
                    dsct_sub7_per = (txtSubs7Perc.Text != "") ? Convert.ToDecimal(txtSubs7Perc.Text.Trim(chDlr)) : dsct_sub7_per;
                    dsct_sub8_per = (txtSubs8Perc.Text != "") ? Convert.ToDecimal(txtSubs8Perc.Text.Trim(chDlr)) : dsct_sub8_per;
                    dsct_sub9_per = (txtSubs9Perc.Text != "") ? Convert.ToDecimal(txtSubs9Perc.Text.Trim(chDlr)) : dsct_sub9_per;
                    dsct_sub10_per = (txtSubs10Perc.Text != "") ? Convert.ToDecimal(txtSubs10Perc.Text.Trim(chDlr)) : dsct_sub10_per;
                    dsct_sub1_tickets = (txtSubs1Tickets.Text != "") ? Convert.ToDecimal(txtSubs1Tickets.Text.Trim(chDlr)) : dsct_sub1_tickets;
                    dsct_sub2_tickets = (txtSubs2Tickets.Text != "") ? Convert.ToDecimal(txtSubs2Tickets.Text.Trim(chDlr)) : dsct_sub2_tickets;
                    dsct_sub3_tickets = (txtSubs3Tickets.Text != "") ? Convert.ToDecimal(txtSubs3Tickets.Text.Trim(chDlr)) : dsct_sub3_tickets;
                    dsct_sub4_tickets = (txtSubs4Tickets.Text != "") ? Convert.ToDecimal(txtSubs4Tickets.Text.Trim(chDlr)) : dsct_sub4_tickets;
                    dsct_sub5_tickets = (txtSubs5Tickets.Text != "") ? Convert.ToDecimal(txtSubs5Tickets.Text.Trim(chDlr)) : dsct_sub5_tickets;
                    dsct_sub6_tickets = (txtSubs6Tickets.Text != "") ? Convert.ToDecimal(txtSubs6Tickets.Text.Trim(chDlr)) : dsct_sub6_tickets;
                    dsct_sub7_tickets = (txtSubs7Tickets.Text != "") ? Convert.ToDecimal(txtSubs7Tickets.Text.Trim(chDlr)) : dsct_sub7_tickets;
                    dsct_sub8_tickets = (txtSubs8Tickets.Text != "") ? Convert.ToDecimal(txtSubs8Tickets.Text.Trim(chDlr)) : dsct_sub8_tickets;
                    dsct_sub9_tickets = (txtSubs9Tickets.Text != "") ? Convert.ToDecimal(txtSubs9Tickets.Text.Trim(chDlr)) : dsct_sub9_tickets;
                    dsct_sub10_tickets = (txtSubs10Tickets.Text != "") ? Convert.ToDecimal(txtSubs10Tickets.Text.Trim(chDlr)) : dsct_sub10_tickets;
                    dsct_sml_grp_per = (txtgrouplessPerc1.Text != "") ? Convert.ToDecimal(txtgrouplessPerc1.Text.Trim(chDlr)) : dsct_sml_grp_per;
                    dsct_lrg_grp_per = (txtgrouplessPerc2.Text != "") ? Convert.ToDecimal(txtgrouplessPerc2.Text.Trim(chDlr)) : dsct_lrg_grp_per;
                    dsct_grp3_per = (txtgrouplessPerc3.Text != "") ? Convert.ToDecimal(txtgrouplessPerc3.Text.Trim(chDlr)) : dsct_grp3_per;
                    dsct_grp4_per = (txtgrouplessPerc4.Text != "") ? Convert.ToDecimal(txtgrouplessPerc4.Text.Trim(chDlr)) : dsct_grp4_per;
                    dsct_grp5_per = (txtgrouplessPerc5.Text != "") ? Convert.ToDecimal(txtgrouplessPerc5.Text.Trim(chDlr)) : dsct_grp5_per;
                    dsct_grp6_per = (txtgrouplessPerc6.Text != "") ? Convert.ToDecimal(txtgrouplessPerc6.Text.Trim(chDlr)) : dsct_grp6_per;
                    dsct_grp7_per = (txtgrouplessPerc7.Text != "") ? Convert.ToDecimal(txtgrouplessPerc7.Text.Trim(chDlr)) : dsct_grp7_per;
                    dsct_grp8_per = (txtgrouplessPerc8.Text != "") ? Convert.ToDecimal(txtgrouplessPerc8.Text.Trim(chDlr)) : dsct_grp8_per;
                    dsct_grp9_per = (txtgrouplessPerc9.Text != "") ? Convert.ToDecimal(txtgrouplessPerc9.Text.Trim(chDlr)) : dsct_grp9_per;
                    dsct_grp10_per = (txtgrouplessPerc10.Text != "") ? Convert.ToDecimal(txtgrouplessPerc10.Text.Trim(chDlr)) : dsct_grp10_per;
                    dsct_sml_grp_tickets = (txtGrouplessTickets1.Text != "") ? Convert.ToDecimal(txtGrouplessTickets1.Text.Trim(chDlr)) : dsct_sml_grp_tickets;
                    dsct_lrg_grp_tickets = (txtGrouplessTickets2.Text != "") ? Convert.ToDecimal(txtGrouplessTickets2.Text.Trim(chDlr)) : dsct_lrg_grp_tickets;
                    dsct_grp3_tickets = (txtGrouplessTickets3.Text != "") ? Convert.ToDecimal(txtGrouplessTickets3.Text.Trim(chDlr)) : dsct_grp3_tickets;
                    dsct_grp4_tickets = (txtGrouplessTickets4.Text != "") ? Convert.ToDecimal(txtGrouplessTickets4.Text.Trim(chDlr)) : dsct_grp4_tickets;
                    dsct_grp5_tickets = (txtGrouplessTickets5.Text != "") ? Convert.ToDecimal(txtGrouplessTickets5.Text.Trim(chDlr)) : dsct_grp5_tickets;
                    dsct_grp6_tickets = (txtGrouplessTickets6.Text != "") ? Convert.ToDecimal(txtGrouplessTickets6.Text.Trim(chDlr)) : dsct_grp6_tickets;
                    dsct_grp7_tickets = (txtGrouplessTickets7.Text != "") ? Convert.ToDecimal(txtGrouplessTickets7.Text.Trim(chDlr)) : dsct_grp7_tickets;
                    dsct_grp8_tickets = (txtGrouplessTickets8.Text != "") ? Convert.ToDecimal(txtGrouplessTickets8.Text.Trim(chDlr)) : dsct_grp8_tickets;
                    dsct_grp9_tickets = (txtGrouplessTickets9.Text != "") ? Convert.ToDecimal(txtGrouplessTickets9.Text.Trim(chDlr)) : dsct_grp9_tickets;
                    dsct_grp10_tickets = (txtGrouplessTickets10.Text != "") ? Convert.ToDecimal(txtGrouplessTickets10.Text.Trim(chDlr)) : dsct_grp10_tickets;
                    dsct_misc1_per = (txtMiscellaneousPerc1.Text != "") ? Convert.ToDecimal(txtMiscellaneousPerc1.Text.Trim(chDlr)) : dsct_misc1_per;
                    dsct_misc2_per = (txtMiscellaneousPerc2.Text != "") ? Convert.ToDecimal(txtMiscellaneousPerc2.Text.Trim(chDlr)) : dsct_misc2_per;
                    dsct_misc3_per = (txtMiscellaneousPerc3.Text != "") ? Convert.ToDecimal(txtMiscellaneousPerc3.Text.Trim(chDlr)) : dsct_misc3_per;
                    dsct_misc4_per = (txtMiscellaneousPerc4.Text != "") ? Convert.ToDecimal(txtMiscellaneousPerc4.Text.Trim(chDlr)) : dsct_misc4_per;
                    dsct_misc5_per = (txtMiscellaneousPerc5.Text != "") ? Convert.ToDecimal(txtMiscellaneousPerc5.Text.Trim(chDlr)) : dsct_misc5_per;
                    dsct_misc6_per = (txtMiscellaneousPerc6.Text != "") ? Convert.ToDecimal(txtMiscellaneousPerc6.Text.Trim(chDlr)) : dsct_misc6_per;
                    dsct_misc7_per = (txtMiscellaneousPerc7.Text != "") ? Convert.ToDecimal(txtMiscellaneousPerc7.Text.Trim(chDlr)) : dsct_misc7_per;
                    dsct_misc8_per = (txtMiscellaneousPerc8.Text != "") ? Convert.ToDecimal(txtMiscellaneousPerc8.Text.Trim(chDlr)) : dsct_misc8_per;
                    dsct_misc9_per = (txtMiscellaneousPerc9.Text != "") ? Convert.ToDecimal(txtMiscellaneousPerc9.Text.Trim(chDlr)) : dsct_misc9_per;
                    dsct_misc10_per = (txtMiscellaneousPerc10.Text != "") ? Convert.ToDecimal(txtMiscellaneousPerc10.Text.Trim(chDlr)) : dsct_misc10_per;
                    dsct_misc1_tickets = (txtMiscellaneousTickets1.Text != "") ? Convert.ToDecimal(txtMiscellaneousTickets1.Text.Trim(chDlr)) : dsct_misc1_tickets;
                    dsct_misc2_tickets = (txtMiscellaneousTickets2.Text != "") ? Convert.ToDecimal(txtMiscellaneousTickets2.Text.Trim(chDlr)) : dsct_misc2_tickets;
                    dsct_misc3_tickets = (txtMiscellaneousTickets3.Text != "") ? Convert.ToDecimal(txtMiscellaneousTickets3.Text.Trim(chDlr)) : dsct_misc3_tickets;
                    dsct_misc4_tickets = (txtMiscellaneousTickets4.Text != "") ? Convert.ToDecimal(txtMiscellaneousTickets4.Text.Trim(chDlr)) : dsct_misc4_tickets;
                    dsct_misc5_tickets = (txtMiscellaneousTickets5.Text != "") ? Convert.ToDecimal(txtMiscellaneousTickets5.Text.Trim(chDlr)) : dsct_misc5_tickets;
                    dsct_misc6_tickets = (txtMiscellaneousTickets6.Text != "") ? Convert.ToDecimal(txtMiscellaneousTickets6.Text.Trim(chDlr)) : dsct_misc6_tickets;
                    dsct_misc7_tickets = (txtMiscellaneousTickets7.Text != "") ? Convert.ToDecimal(txtMiscellaneousTickets7.Text.Trim(chDlr)) : dsct_misc7_tickets;
                    dsct_misc8_tickets = (txtMiscellaneousTickets8.Text != "") ? Convert.ToDecimal(txtMiscellaneousTickets8.Text.Trim(chDlr)) : dsct_misc8_tickets;
                    dsct_misc9_tickets = (txtMiscellaneousTickets9.Text != "") ? Convert.ToDecimal(txtMiscellaneousTickets9.Text.Trim(chDlr)) : dsct_misc9_tickets;
                    dsct_misc10_tickets = (txtMiscellaneousTickets10.Text != "") ? Convert.ToDecimal(txtMiscellaneousTickets10.Text.Trim(chDlr)) : dsct_misc10_tickets;
                    dsct_demand_price = (txtDemandPricing.Text != "") ? Convert.ToDecimal(txtDemandPricing.Text.Trim(chDlr)) : dsct_demand_price;
                    ofcobj = new BoxOfficeData();
                    int output;
                    if (ddlPerformance.Visible == true)
                    {
                        output = ofcobj.Discount_Insert(Convert.ToInt32(hdn_engagementid.Value), Convert.ToInt32(ddlPerformance.SelectedItem.Value),
                            dsct_sub1_per, dsct_sub2_per, dsct_sub3_per, dsct_sub4_per, dsct_sub5_per, dsct_sub6_per, dsct_sub7_per, dsct_sub8_per, dsct_sub9_per, dsct_sub10_per,
                            dsct_sub1_tickets, dsct_sub2_tickets, dsct_sub3_tickets,
    dsct_sub4_tickets,
    dsct_sub5_tickets,
    dsct_sub6_tickets, dsct_sub7_tickets, dsct_sub8_tickets, dsct_sub9_tickets, dsct_sub10_tickets,
    dsct_sml_grp_per,
    dsct_lrg_grp_per, dsct_grp3_per, dsct_grp4_per, dsct_grp5_per, dsct_grp6_per, dsct_grp7_per, dsct_grp8_per, dsct_grp9_per, dsct_grp10_per,
    dsct_sml_grp_tickets,
    dsct_lrg_grp_tickets, dsct_grp3_tickets, dsct_grp4_tickets, dsct_grp5_tickets, dsct_grp6_tickets, dsct_grp7_tickets, dsct_grp8_tickets, dsct_grp9_tickets, dsct_grp10_tickets,
    dsct_misc1_per,
    dsct_misc2_per,
    dsct_misc3_per,
    dsct_misc4_per, dsct_misc5_per, dsct_misc6_per, dsct_misc7_per, dsct_misc8_per, dsct_misc9_per, dsct_misc10_per,
    dsct_misc1_tickets,
    dsct_misc2_tickets,
    dsct_misc3_tickets,
    dsct_misc4_tickets, dsct_misc5_tickets, dsct_misc6_tickets, dsct_misc7_tickets, dsct_misc8_tickets, dsct_misc9_tickets, dsct_misc10_tickets,
    dsct_demand_price, txtnotes.Text.Trim());
                        if (output == 1001)
                        {
                            lbl_msg.Text = "Engagement Discount created successfully";
                            lbl_msg.ForeColor = System.Drawing.Color.Green;
                        }
                        else if (output == 1002)
                        {
                            lbl_msg.Text = "Engagement Discount updated successfully";
                            lbl_msg.ForeColor = System.Drawing.Color.Green;
                        }

                    }
                    else
                    {
                        foreach (ListItem list in chklstPerformance.Items)
                        {
                            if (list.Selected == true && list.Value != "0")
                            {

                                output = ofcobj.Discount_Insert(Convert.ToInt32(hdn_engagementid.Value), Convert.ToInt32(list.Value),
                           dsct_sub1_per, dsct_sub2_per, dsct_sub3_per, dsct_sub4_per, dsct_sub5_per, dsct_sub6_per, dsct_sub7_per, dsct_sub8_per, dsct_sub9_per, dsct_sub10_per,
                            dsct_sub1_tickets, dsct_sub2_tickets, dsct_sub3_tickets,
    dsct_sub4_tickets,
    dsct_sub5_tickets,
    dsct_sub6_tickets, dsct_sub7_tickets, dsct_sub8_tickets, dsct_sub9_tickets, dsct_sub10_tickets,
    dsct_sml_grp_per,
    dsct_lrg_grp_per, dsct_grp3_per, dsct_grp4_per, dsct_grp5_per, dsct_grp6_per, dsct_grp7_per, dsct_grp8_per, dsct_grp9_per, dsct_grp10_per,
    dsct_sml_grp_tickets,
    dsct_lrg_grp_tickets, dsct_grp3_tickets, dsct_grp4_tickets, dsct_grp5_tickets, dsct_grp6_tickets, dsct_grp7_tickets, dsct_grp8_tickets, dsct_grp9_tickets, dsct_grp10_tickets,
    dsct_misc1_per,
    dsct_misc2_per,
    dsct_misc3_per,
    dsct_misc4_per, dsct_misc5_per, dsct_misc6_per, dsct_misc7_per, dsct_misc8_per, dsct_misc9_per, dsct_misc10_per,
    dsct_misc1_tickets,
    dsct_misc2_tickets,
    dsct_misc3_tickets,
    dsct_misc4_tickets, dsct_misc5_tickets, dsct_misc6_tickets, dsct_misc7_tickets, dsct_misc8_tickets, dsct_misc9_tickets, dsct_misc10_tickets,
    dsct_demand_price, txtnotes.Text.Trim());
                                if (output == 1001)
                                {
                                    lbl_msg.Text = "Engagement Discount created successfully";
                                    lbl_msg.ForeColor = System.Drawing.Color.Green;
                                }
                                else if (output == 1002)
                                {
                                    lbl_msg.Text = "Engagement Discount updated successfully";
                                    lbl_msg.ForeColor = System.Drawing.Color.Green;
                                }
                            }
                        }
                    }
                    ucdocx.SaveDocx(Convert.ToInt32(ddlPerformance.SelectedItem.Value), "Schedule");
                    // int index = ddlPerformance.SelectedIndex;
                    ddlPerformancesload(Convert.ToInt32(hdn_engagementid.Value));
                    //LoadPerformanceCopy(Convert.ToInt32(hdn_engagementid.Value));
                    // ddlPerformance.SelectedIndex = index;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "sysmbols();", true);
                }
                else
                {
                    lbl_msg.Text = "Please create Engagement Schedule first";
                    lbl_msg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        public void DeleteData(string flag)
        {
            EngagementMaster EngmtMaster = (EngagementMaster)Page.Master;
            int delflag = EngmtMaster.DeleteEngagement(flag);
            lbl_msg.Text = "Engagement Deleted successfully";
            lbl_msg.ForeColor = System.Drawing.Color.Green;
        }
        protected void ddlPerformance_SelectedIndexChanged(object sender, EventArgs e)
        {
            Reset();
            ofcobj = new BoxOfficeData();
            DataTable ds = new DataTable();
            ds = ofcobj.LoadDiscountdata(Convert.ToInt32(hdn_engagementid.Value), Convert.ToInt32(ddlPerformance.SelectedItem.Value.ToString()));
            if (ds.Rows.Count > 0)
            {
                filldatacalculations(ds);
            }
            else
            {
                LoadPerformanceCopy(Convert.ToInt32(hdn_engagementid.Value));
            }
            //if (hdn_engagementid.Value != "" && hdn_schedulecount.Value != "0")
            //{
            //    if (ViewState["dtperformance"] == null)
            //    {
            //        dtperformancelist = ofcobj.GetPerformancelist(Convert.ToInt32(hdn_engagementid.Value));
            //    }
            //    else
            //    {
            //        dtperformancelist = (ViewState["dtperformance"]) as DataTable;
            //    }
            //    DataTable value = performanceddlvalue(dtperformancelist);
            //    if (value.Rows.Count > 0)
            //    {
            //        filldatacalculations(value);
            //    }
            //}
        }
        public void showhidecopyfrom(bool flg)
        {
            lblcopyfrom.Visible = flg;
            pnlCity.Visible = flg;
            lnkcopy.Visible = flg;
            tdcopyfrom.Visible = flg;
        }
        public void LoadPerformanceCopy(Int32 engtid)
        {
            dtperformancelist = new DataTable();
            dtperformancelist = objmst.GetPerformance_Copy(engtid, "DC");
            rbtnlstperformance.DataSource = dtperformancelist;
            rbtnlstperformance.DataTextField = "SCHEDULE_TYPE";
            rbtnlstperformance.DataValueField = "SCHEDULE_ID";
            rbtnlstperformance.DataBind();
            showhidecopyfrom(true);
        }
        public void loadperformanceddl(int engmntid)
        {
            ofcobj = new BoxOfficeData();
            //dtperformancelist = ofcobj.GetPerformancelist(engmntid);
            dtperformancelist = ofcobj.LoadPerformancelist(engmntid);
            ViewState["dtperformance"] = dtperformancelist;
            ddlPerformance.DataSource = dtperformancelist;
            ddlPerformance.DataTextField = "schedule_type";
            ddlPerformance.DataValueField = "schedule_id";
            ddlPerformance.DataBind();
            if (dtperformancelist != null)
            {
                if (ddlPerformance.Items.Count > 0)
                    ddlPerformance.SelectedIndex = 0;
            }
          
            chklstPerformance.DataSource = dtperformancelist;
            chklstPerformance.DataTextField = "schedule_type";
            chklstPerformance.DataValueField = "schedule_id";
            chklstPerformance.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "All";
            lst.Value = "0";
            lst.Attributes.Add("Class", "YellowSpan");
            chklstPerformance.Items.Insert(0, lst);
        
        }
        public void ddlPerformancesload(int engmntid)
        {
            if (ddlPerformance.SelectedIndex > -1)
            {
                string ddlval = ddlPerformance.SelectedItem.Value;
                dtperformancelist = new DataTable();
                ofcobj = new BoxOfficeData();
                DataTable ds = new DataTable();
                ds = ofcobj.LoadDiscountdata(Convert.ToInt32(hdn_engagementid.Value), Convert.ToInt32(ddlPerformance.SelectedItem.Value.ToString()));
                if (ds.Rows.Count > 0)
                {
                    if (Convert.ToInt32(Convert.ToString(ds.Rows[0]["ScheduleCount"])) > 0)
                    {
                        lbl_staticmsg.Text = "Please enter data for all Performances, otherwise the reports will display the incorrect values.";
                    }
                    else
                    {
                        lbl_staticmsg.Text = string.Empty;
                    }
                    if (Convert.ToInt32(Convert.ToString(ds.Rows[0]["discount"])) > 0)
                    {
                        pnlcolumn.Visible = false;
                        ddlPerformance.Visible = true;
                        txtPerformance.Visible = false;
                    }
                    else
                    {
                        pnlcolumn.Visible = true;
                        ddlPerformance.Visible = false;
                        txtPerformance.Visible = false;
                        lblcopyfrom.Visible = false;
                        pnlCity.Visible = false;
                    }
                    filldatacalculations(ds);
                    ucdocx.GetDocxDetails(Convert.ToInt32(ddlPerformance.SelectedItem.Value.ToString()), "Schedule");
                    tdcopyfrom.Visible = true;
                }
                else
                {
                    tdcopyfrom.Visible = false;
                    pnlcolumn.Visible = true;
                    ddlPerformance.Visible = false;
                    txtPerformance.Visible = false;
                    lblcopyfrom.Visible = false;
                    pnlCity.Visible = false;
                    LoadPerformanceCopy(Convert.ToInt32(hdn_engagementid.Value));
                    showhidecopyfrom(false);
                }
            }
        }
        public void filldatacalculations(DataTable value)
        {
            showhidecopyfrom(true);
            LoadPerformanceCopy(Convert.ToInt32(hdn_engagementid.Value));
            txtSubs1Perc.Text = value.Rows[0]["DSCT_SUB1_PER"].ToString();
            txtSubs2Perc.Text = value.Rows[0]["DSCT_SUB2_PER"].ToString();
            txtSubs3Perc.Text = value.Rows[0]["DSCT_SUB3_PER"].ToString();
            txtSubs4Perc.Text = value.Rows[0]["DSCT_SUB4_PER"].ToString();
            txtSubs5Perc.Text = value.Rows[0]["DSCT_SUB5_PER"].ToString();
            txtSubs6Perc.Text = value.Rows[0]["DSCT_SUB6_PER"].ToString();
            txtSubs7Perc.Text = value.Rows[0]["DSCT_SUB7_PER"].ToString();
            txtSubs8Perc.Text = value.Rows[0]["DSCT_SUB8_PER"].ToString();
            txtSubs9Perc.Text = value.Rows[0]["DSCT_SUB9_PER"].ToString();
            txtSubs10Perc.Text = value.Rows[0]["DSCT_SUB10_PER"].ToString();

            txtSubs1Tickets.Text = value.Rows[0]["DSCT_SUB1_TICKETS"].ToString();
            txtSubs2Tickets.Text = value.Rows[0]["DSCT_SUB2_TICKETS"].ToString();
            txtSubs3Tickets.Text = value.Rows[0]["DSCT_SUB3_TICKETS"].ToString();
            txtSubs4Tickets.Text = value.Rows[0]["DSCT_SUB4_TICKETS"].ToString();
            txtSubs5Tickets.Text = value.Rows[0]["DSCT_SUB5_TICKETS"].ToString();
            txtSubs6Tickets.Text = value.Rows[0]["DSCT_SUB6_TICKETS"].ToString();
            txtSubs7Tickets.Text = value.Rows[0]["DSCT_SUB7_TICKETS"].ToString();
            txtSubs8Tickets.Text = value.Rows[0]["DSCT_SUB8_TICKETS"].ToString();
            txtSubs9Tickets.Text = value.Rows[0]["DSCT_SUB9_TICKETS"].ToString();
            txtSubs10Tickets.Text = value.Rows[0]["DSCT_SUB10_TICKETS"].ToString();

            txtgrouplessPerc1.Text = value.Rows[0]["DSCT_SML_GRP_PER"].ToString();
            txtgrouplessPerc2.Text = value.Rows[0]["DSCT_LRG_GRP_PER"].ToString();
            txtgrouplessPerc3.Text = value.Rows[0]["DSCT_GRP3_PER"].ToString();
            txtgrouplessPerc4.Text = value.Rows[0]["DSCT_GRP4_PER"].ToString();
            txtgrouplessPerc5.Text = value.Rows[0]["DSCT_GRP5_PER"].ToString();
            txtgrouplessPerc6.Text = value.Rows[0]["DSCT_GRP6_PER"].ToString();
            txtgrouplessPerc7.Text = value.Rows[0]["DSCT_GRP7_PER"].ToString();
            txtgrouplessPerc8.Text = value.Rows[0]["DSCT_GRP8_PER"].ToString();
            txtgrouplessPerc9.Text = value.Rows[0]["DSCT_GRP9_PER"].ToString();
            txtgrouplessPerc10.Text = value.Rows[0]["DSCT_GRP10_PER"].ToString();

            txtGrouplessTickets1.Text = value.Rows[0]["DSCT_SML_GRP_TICKETS"].ToString();
            txtGrouplessTickets2.Text = value.Rows[0]["DSCT_LRG_GRP_TICKETS"].ToString();
            txtGrouplessTickets3.Text = value.Rows[0]["DSCT_GRP3_TICKETS"].ToString();
            txtGrouplessTickets4.Text = value.Rows[0]["DSCT_GRP4_TICKETS"].ToString();
            txtGrouplessTickets5.Text = value.Rows[0]["DSCT_GRP5_TICKETS"].ToString();
            txtGrouplessTickets6.Text = value.Rows[0]["DSCT_GRP6_TICKETS"].ToString();
            txtGrouplessTickets7.Text = value.Rows[0]["DSCT_GRP7_TICKETS"].ToString();
            txtGrouplessTickets8.Text = value.Rows[0]["DSCT_GRP8_TICKETS"].ToString();
            txtGrouplessTickets9.Text = value.Rows[0]["DSCT_GRP9_TICKETS"].ToString();
            txtGrouplessTickets10.Text = value.Rows[0]["DSCT_GRP10_TICKETS"].ToString();

            txtMiscellaneousPerc1.Text = value.Rows[0]["DSCT_MISC1_PER"].ToString();
            txtMiscellaneousPerc2.Text = value.Rows[0]["DSCT_MISC2_PER"].ToString();
            txtMiscellaneousPerc3.Text = value.Rows[0]["DSCT_MISC3_PER"].ToString();
            txtMiscellaneousPerc4.Text = value.Rows[0]["DSCT_MISC4_PER"].ToString();
            txtMiscellaneousPerc5.Text = value.Rows[0]["DSCT_MISC5_PER"].ToString();
            txtMiscellaneousPerc6.Text = value.Rows[0]["DSCT_MISC6_PER"].ToString();
            txtMiscellaneousPerc7.Text = value.Rows[0]["DSCT_MISC7_PER"].ToString();
            txtMiscellaneousPerc8.Text = value.Rows[0]["DSCT_MISC8_PER"].ToString();
            txtMiscellaneousPerc9.Text = value.Rows[0]["DSCT_MISC9_PER"].ToString();
            txtMiscellaneousPerc10.Text = value.Rows[0]["DSCT_MISC10_PER"].ToString();

            txtMiscellaneousTickets1.Text = value.Rows[0]["DSCT_MISC1_TICKETS"].ToString();
            txtMiscellaneousTickets2.Text = value.Rows[0]["DSCT_MISC2_TICKETS"].ToString();
            txtMiscellaneousTickets3.Text = value.Rows[0]["DSCT_MISC3_TICKETS"].ToString();
            txtMiscellaneousTickets4.Text = value.Rows[0]["DSCT_MISC4_TICKETS"].ToString();
            txtMiscellaneousTickets5.Text = value.Rows[0]["DSCT_MISC5_TICKETS"].ToString();
            txtMiscellaneousTickets6.Text = value.Rows[0]["DSCT_MISC6_TICKETS"].ToString();
            txtMiscellaneousTickets7.Text = value.Rows[0]["DSCT_MISC7_TICKETS"].ToString();
            txtMiscellaneousTickets8.Text = value.Rows[0]["DSCT_MISC8_TICKETS"].ToString();
            txtMiscellaneousTickets9.Text = value.Rows[0]["DSCT_MISC9_TICKETS"].ToString();
            txtMiscellaneousTickets10.Text = value.Rows[0]["DSCT_MISC10_TICKETS"].ToString();
            txtDemandPricing.Text = value.Rows[0]["DSCT_DEMAND_PRICE"].ToString();
            txtnotes.Text = value.Rows[0]["dsct_notes"].ToString();

            int a = 2, b = 2, c = 2;
            for (int i = 1; i < 10; i++)
            {
                TextBox txtsubsPerc = divDiscount.FindControl("txtSubs" + (i + 1).ToString() + "Perc") as TextBox;
                TextBox txtSubsTicks = divDiscount.FindControl("txtSubs" + (i + 1).ToString() + "Tickets") as TextBox;
                Label txtSubslbl = divDiscount.FindControl("lblsubs" + (i + 1).ToString() + "") as Label;
                CheckBox chkbxSubsTk = divDiscount.FindControl("chkbxSubsTk" + (i + 1).ToString() + "") as CheckBox;

                TextBox txtGroupPerc = divDiscount.FindControl("txtgrouplessPerc" + (i + 1).ToString() + "") as TextBox;
                TextBox txtGroupTicks = divDiscount.FindControl("txtGrouplessTickets" + (i + 1).ToString() + "") as TextBox;
                CheckBox chkbxGrouplessTk = divDiscount.FindControl("chkbxGrouplessTk" + (i + 1).ToString() + "") as CheckBox;
                Label txtGrouplbl = divDiscount.FindControl("lblSale" + (i + 1).ToString() + "") as Label;

                TextBox txtMiscPerc = divDiscount.FindControl("txtMiscellaneousPerc" + (i + 1).ToString() + "") as TextBox;
                TextBox txtMiscTicks = divDiscount.FindControl("txtMiscellaneousTickets" + (i + 1).ToString() + "") as TextBox;
                Label txtMisclbl = divDiscount.FindControl("lblMiscellaneous" + (i + 1).ToString() + "") as Label;
                CheckBox chkbxMiscPerc = divDiscount.FindControl("chkbxMiscPerc" + (i + 1).ToString() + "") as CheckBox;
                if (txtsubsPerc.Text.Trim() != "" || txtSubsTicks.Text.Trim() != "")
                {
                    txtsubsPerc.Visible = true;
                    txtSubsTicks.Visible = true;
                    txtSubslbl.Visible = true;
                    chkbxSubsTk.Visible = true;
                    txtSubslbl.Text = Convert.ToString(a);
                    a++;

                }
                else
                {
                    txtsubsPerc.Visible = false;
                    txtSubsTicks.Visible = false;
                    txtSubslbl.Visible = false;
                    chkbxSubsTk.Visible = false;
                }
                if (txtGroupPerc.Text.Trim() != "" || txtGroupTicks.Text.Trim() != "")
                {
                    txtGroupPerc.Visible = true;
                    txtGroupTicks.Visible = true;
                    txtGrouplbl.Visible = true;
                    chkbxGrouplessTk.Visible = true;
                    txtGrouplbl.Text = Convert.ToString(b);
                    b++;
                }
                else
                {
                    txtGroupPerc.Visible = false;
                    txtGroupTicks.Visible = false;
                    txtGrouplbl.Visible = false;
                    chkbxGrouplessTk.Visible = false;
                }
                if (txtMiscPerc.Text.Trim() != "" || txtMiscTicks.Text.Trim() != "")
                {
                    txtMiscPerc.Visible = true;
                    txtMiscTicks.Visible = true;
                    txtMisclbl.Visible = true;
                    chkbxMiscPerc.Visible = true;
                    txtMisclbl.Text = Convert.ToString(c);
                    c++;
                }
                else
                {
                    txtMiscPerc.Visible = false;
                    txtMiscTicks.Visible = false;
                    txtMisclbl.Visible = false;
                    chkbxMiscPerc.Visible = false;
                }

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "sysmbols();", true);
        }
        protected void rbtnlstperformance_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ds = ofcobj.LoadDiscountdata(Convert.ToInt32(hdn_engagementid.Value), Convert.ToInt32(rbtnlstperformance.SelectedItem.Value.ToString()));
            if (ds.Rows.Count > 0)
            {
                filldatacalculations(ds);
                showhidecopyfrom(true);
            }
        }
        protected void lnkbtnSubsAdd_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i < 10; i++)
                {
                    TextBox txtsubsPerc = divDiscount.FindControl("txtSubs" + (i + 1).ToString() + "Perc") as TextBox;
                    TextBox txtSubsTicks = divDiscount.FindControl("txtSubs" + (i + 1).ToString() + "Tickets") as TextBox;
                    Label txtSubslbl = divDiscount.FindControl("lblsubs" + (i + 1).ToString() + "") as Label;
                    CheckBox chkbxSubsTk = divDiscount.FindControl("chkbxSubsTk" + (i + 1).ToString() + "") as CheckBox;
                    if (txtsubsPerc.Visible == false)
                    {
                        txtsubsPerc.Visible = true;
                        txtSubsTicks.Visible = true;
                        txtSubslbl.Visible = true;
                        chkbxSubsTk.Visible = true;
                        Sno("lblsubs");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        protected void lnkbtnSubsDelete_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i < 11; i++)
                {
                    CheckBox chkbxSubsTk1 = divDiscount.FindControl("chkbxSubsTk" + i + "") as CheckBox;
                    if (chkbxSubsTk1.Checked == true)
                    {
                        if (i != 1)
                        {
                            TextBox txtsubsPerc = divDiscount.FindControl("txtSubs" + i + "Perc") as TextBox;
                            TextBox txtSubsTicks = divDiscount.FindControl("txtSubs" + i + "Tickets") as TextBox;
                            Label txtSubslbl = divDiscount.FindControl("lblsubs" + i + "") as Label;
                            if (txtsubsPerc.Visible == true)
                            {
                                txtsubsPerc.Visible = false;
                                txtSubsTicks.Visible = false;
                                txtSubslbl.Visible = false;
                                chkbxSubsTk1.Checked = false;
                                chkbxSubsTk1.Visible = false;
                                txtsubsPerc.Text = "";
                                txtSubsTicks.Text = "";
                                Sno("lblsubs");
                            }
                        }
                        else
                        {
                            txtSubs1Perc.Text = "";
                            txtSubs1Tickets.Text = "";
                            chkbxSubsTk1.Checked = false;

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void lnkGroupAdd_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i < 10; i++)
                {
                    TextBox txtGroupPerc = divDiscount.FindControl("txtgrouplessPerc" + (i + 1).ToString() + "") as TextBox;
                    TextBox txtGroupTicks = divDiscount.FindControl("txtGrouplessTickets" + (i + 1).ToString() + "") as TextBox;
                    CheckBox chkbxGrouplessTk = divDiscount.FindControl("chkbxGrouplessTk" + (i + 1).ToString() + "") as CheckBox;
                    Label txtGrouplbl = divDiscount.FindControl("lblSale" + (i + 1).ToString() + "") as Label;
                    if (txtGroupPerc.Visible == false)
                    {
                        txtGroupPerc.Visible = true;
                        txtGroupTicks.Visible = true;
                        txtGrouplbl.Visible = true;
                        chkbxGrouplessTk.Visible = true;
                        Sno("lblSale");
                        return;
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        protected void lnkGroupDelete_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i < 11; i++)
                {
                    CheckBox chkbxGrouplessTk = divDiscount.FindControl("chkbxGrouplessTk" + i + "") as CheckBox;
                    if (chkbxGrouplessTk.Checked == true)
                    {
                        if (i != 1)
                        {
                            TextBox txtGroupPerc = divDiscount.FindControl("txtgrouplessPerc" + i + "") as TextBox;
                            TextBox txtGroupTicks = divDiscount.FindControl("txtGrouplessTickets" + i + "") as TextBox;
                            Label txtGrouplbl = divDiscount.FindControl("lblSale" + i + "") as Label;
                            if (txtGroupPerc.Visible == true)
                            {
                                txtGroupPerc.Visible = false;
                                txtGroupTicks.Visible = false;
                                txtGrouplbl.Visible = false;
                                chkbxGrouplessTk.Checked = false;
                                chkbxGrouplessTk.Visible = false;
                                txtGroupPerc.Text = "";
                                txtGroupTicks.Text = "";
                                Sno("lblSale");
                            }
                        }
                        else
                        {
                            txtgrouplessPerc1.Text = "";
                            txtGrouplessTickets1.Text = "";
                            chkbxGrouplessTk1.Checked = false;

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        protected void lnkbtnMiscAdd_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i < 10; i++)
                {
                    TextBox txtMiscPerc = divDiscount.FindControl("txtMiscellaneousPerc" + (i + 1).ToString() + "") as TextBox;
                    TextBox txtMiscTicks = divDiscount.FindControl("txtMiscellaneousTickets" + (i + 1).ToString() + "") as TextBox;
                    Label txtMisclbl = divDiscount.FindControl("lblMiscellaneous" + (i + 1).ToString() + "") as Label;
                    CheckBox chkbxMiscPerc = divDiscount.FindControl("chkbxMiscPerc" + (i + 1).ToString() + "") as CheckBox;
                    if (txtMiscPerc.Visible == false)
                    {
                        txtMiscPerc.Visible = true;
                        txtMiscTicks.Visible = true;
                        txtMisclbl.Visible = true;
                        chkbxMiscPerc.Visible = true;
                        Sno("lblMiscellaneous");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void lnkbtnMiscDelete_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i < 11; i++)
                {
                    CheckBox chkbxMiscPerc = divDiscount.FindControl("chkbxMiscPerc" + i + "") as CheckBox;
                    if (chkbxMiscPerc.Checked == true)
                    {
                        if (i != 1)
                        {
                            TextBox txtMiscPerc = divDiscount.FindControl("txtMiscellaneousPerc" + i + "") as TextBox;
                            TextBox txtMiscTicks = divDiscount.FindControl("txtMiscellaneousTickets" + i + "") as TextBox;
                            Label txtMisclbl = divDiscount.FindControl("lblMiscellaneous" + i + "") as Label;
                            if (txtMiscPerc.Visible == true)
                            {
                                txtMiscPerc.Visible = false;
                                txtMiscTicks.Visible = false;
                                txtMisclbl.Visible = false;
                                chkbxMiscPerc.Checked = false;
                                chkbxMiscPerc.Visible = false;
                                txtMiscPerc.Text = "";
                                txtMiscTicks.Text = "";
                                Sno("lblMiscellaneous");
                            }
                        }
                        else
                        {
                            txtMiscellaneousPerc1.Text = "";
                            txtMiscellaneousTickets1.Text = "";
                            chkbxMiscPerc.Checked = false;

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void chkbxSubsTkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i < 11; i++)
                {
                    CheckBox chkbxSubsTk = divDiscount.FindControl("chkbxSubsTk" + i + "") as CheckBox;
                    if (chkbxSubsTk.Visible == true)
                    {
                        chkbxSubsTk.Checked = chkbxSubsTkAll.Checked == true ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void chkbxGrouplessTkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i < 11; i++)
                {
                    CheckBox chkbxGrouplessTk = divDiscount.FindControl("chkbxGrouplessTk" + i + "") as CheckBox;
                    if (chkbxGrouplessTk.Visible == true)
                    {
                        chkbxGrouplessTk.Checked = chkbxGrouplessTkAll.Checked == true ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void chkbxMiscPercAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i < 11; i++)
                {
                    CheckBox chkbxMiscPerc = divDiscount.FindControl("chkbxMiscPerc" + i + "") as CheckBox;
                    if (chkbxMiscPerc.Visible == true)
                    {
                        chkbxMiscPerc.Checked = chkbxMiscPercAll.Checked == true ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void Sno(string name)
        {
            int a = 2;
            for (int i = 1; i < 10; i++)
            {
                Label txtlbl = divDiscount.FindControl("" + name + "" + (i + 1).ToString() + "") as Label;
                if (txtlbl.Visible == true)
                {
                    txtlbl.Text = Convert.ToString(a);
                    a++;
                }
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
    }
}