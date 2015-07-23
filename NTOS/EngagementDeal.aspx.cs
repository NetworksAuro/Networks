using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ScheuleDataLayer;
using MasterDataLayer;
using EngagementDataLayer;
using System.Configuration;

namespace NTOS
{
    public partial class EngagementDeal : System.Web.UI.Page, MasterPageSaveInterface
    {
        MasterData mdl = new MasterData();
        EngagementData edl = new EngagementData();
        ScheduleData sdl = new ScheduleData();
        Label lbl_msg;

        protected void Page_Load(object sender, EventArgs e)
        {    
            int showid=0; 
            int engagementid = Convert.ToInt32(Request.QueryString["engmtid"]);
            if (Request.QueryString["showid"] != null && Request.QueryString["showid"] != "")
                showid = Convert.ToInt32(Request.QueryString["showid"]);
            
            string schcount = Request.QueryString["schcount"];
            Session["search_engmt"] = engagementid.ToString();
            (this.Master as EngagementMaster).SetActiveTab("lideal");
           (this.Master as EngagementMaster).hidesummary();
            //((ImageButton)this.Master.FindControl("imgbtndeal")).ImageUrl = "~/Images/tabb-dl.png";
            hdn_engagementid.Value = engagementid.ToString();
            hdn_showid.Value = showid.ToString();
            hdn_schedulecount.Value = schcount;
            ValidationSummary mastersummary = (ValidationSummary)this.Master.FindControl("val_summarymaster");
            mastersummary.Enabled = false;
            lbl_msg = (Label)this.Master.FindControl("lbl_message");
            lbl_msg.Text = "";

            if (engagementid > 0)
            {
                lbl_deal.Visible = false;
                HiddenField hdn_ucengagementid = (HiddenField)EngagementDeal1.FindControl("hdn_ucengagementid");
                hdn_ucengagementid.Value = hdn_engagementid.Value;
                HiddenField hdn_ucshowid = (HiddenField)EngagementDeal1.FindControl("hdn_ucshowid");
                hdn_ucshowid.Value = hdn_showid.Value;
                div_deal.Visible = true;
            }
        }
        public void Reset()
        {
            EngagementDeal1.Reset();
        }
        public void SaveData()
        {

            //ValidationSummary dealvalsummary = (ValidationSummary)EngagementDeal1.FindControl("val_summary_deal");
            RequiredFieldValidator rfvcreatedate = (RequiredFieldValidator)EngagementDeal1.FindControl("rfv_createdate");
          //  CompareValidator comupdatedate = (CompareValidator)EngagementDeal1.FindControl("com_updatedate");
            HiddenField hdn_ucdealid = (HiddenField)EngagementDeal1.FindControl("hdn_ucdealid");
            hdn_dealid.Value = hdn_ucdealid.Value;
            EngagementMaster EngmtMaster = (EngagementMaster)Page.Master;

            if (hdn_engagementid.Value != "0" && hdn_engagementid.Value != "")
            {
                //if (hdn_schedulecount.Value != "0" && hdn_schedulecount.Value != "")
                //{
                    rfvcreatedate.Enabled = false;
                //    comupdatedate.Enabled = false;
                    if (EngagementDeal1.checkvalid() == false)
                    {
                        lbl_msg.Text = "Total share of Producer, Presenter and Star Royalty should be 100%. ";
                        lbl_msg.ForeColor = System.Drawing.Color.Orange;
                        return;
                    }
                    if (hdn_dealid.Value != "0")
                    {

                        EngagementDeal1.SaveDealData("updatedeal", 0, 0, Convert.ToInt32(hdn_dealid.Value));
                        lbl_msg.Text = "Engagement Deal updated successfully";
                        lbl_msg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        //rfvcreatedate.Enabled = false;
                        //comupdatedate.Enabled = false;
                        EngagementDeal1.SaveDealData("createdeal", 0, Convert.ToInt32(hdn_engagementid.Value), 0);
                        lbl_msg.Text = "Engagement Deal created successfully";
                        lbl_msg.ForeColor = System.Drawing.Color.Green;
                    }
                //}
                //else
                //{
                //    lbl_msg.Text = "Please create Engagement Schedule first";
                //    lbl_msg.ForeColor = System.Drawing.Color.Red;
                //}
            }
        }

        public void DeleteData(string flag)
        {
            EngagementMaster EngmtMaster = (EngagementMaster)Page.Master;
            int delflag = EngmtMaster.DeleteEngagement(flag);
            lbl_msg.Text = "Engagement Deleted successfully";
            lbl_msg.ForeColor = System.Drawing.Color.Green;
        }

        protected void lnknewdealtemplate_Click(object sender, EventArgs e)
        {

            string showid = Convert.ToString(hdn_showid.Value);
            Response.Redirect("Deal.aspx?engmtidnew=" + Request.QueryString["engmtid"] + "&showidnew=" + showid);
        }
    }
}