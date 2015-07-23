using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DealDataLayer;
using System.Data;

namespace NTOS
{
    public partial class Deal : System.Web.UI.Page, MasterPageSaveInterface
    {
        int dealtemplateid;
        DealData ddl = new DealData();
        Label lbl_msg;
      
        protected void Page_Load(object sender, EventArgs e)
        {
            dealtemplateid = Convert.ToInt32(Request.QueryString["dealid"]);
            int status = Convert.ToInt32(Request.QueryString["status"]);
           // lbl_header.Text = "";
            lblerrmsg.Text = "";
            this.Master.FindControl("ImageButton1").Visible = false;
            if (!Page.IsPostBack) 
            {
                lbl_msg = (Label)this.Master.FindControl("lbl_headersite");
                if (status == 1)
                {
                    lblerrmsg.Text = "Deal template created successfully";
                    lblerrmsg.ForeColor = System.Drawing.Color.Green;
                }

                if (dealtemplateid > 0)
                {
                    DataTable statuscheck = ddl.GetDealTemplateDetailsById(dealtemplateid);
                    if (statuscheck.Rows.Count > 0)
                        (this.Master as Site1).show_control(statuscheck.Rows[0]["deal_tmpt_active_flag"].ToString().ToLower(), pnldealenable, statuscheck.Rows[0]["deal_tmpt_beast_flag"].ToString());
                    lbl_msg.Text = "Modify Deal Template";
                    //link_new.Style.Add("visibility", "visible");
                }
                else
                {
                    (this.Master as Site1).SetfNewbutton("Deal");
                    lbl_msg.Text = "Create Deal Template";
                }

                HiddenField hdn_ucengagementid = (HiddenField)EngagementDeal.FindControl("hdn_ucengagementid");
                HiddenField hdn_ucdealid = (HiddenField)EngagementDeal.FindControl("hdn_ucdealid");

                hdn_ucengagementid.Value = "-1";
                hdn_ucdealid.Value = dealtemplateid.ToString();
            }
        }
        public void Reset()
        {
        }
        public void SaveData()
        {
                HiddenField hdn_ucdealid = (HiddenField)EngagementDeal.FindControl("hdn_ucdealid");

            if (EngagementDeal.checkvalid() == false)
            {
                lblerrmsg.Text = "Total share of Producer, Presenter and Star Royalty should be 100%. ";
                lblerrmsg.ForeColor = System.Drawing.Color.Orange;
                return;
            }
            if (hdn_ucdealid.Value != null && hdn_ucdealid.Value !="" && hdn_ucdealid.Value !="0")
            {
                EngagementDeal.SaveDealData("updatetemplate", dealtemplateid, 0, 0);
                lblerrmsg.Text = "Deal template updated successfully";
                lblerrmsg.ForeColor = System.Drawing.Color.Green;
                
            }
            else
            {
                EngagementDeal.SaveDealData("createtemplate", 0, 0, 0);
               
            }

        }

        public void DeleteData(string flag)
        {
            EngagementDeal.DeleteDealTemplate(dealtemplateid, flag);
            //div_dealtemp.Disabled = true;
            ((LinkButton )this.Master.FindControl("imgbtndelete")).Visible = false;
            ((ImageButton)this.Master.FindControl("imgbtnsave")).Visible = false;
            //lblerrmsg.Text = "Deal template deleted successfully";
            lblerrmsg.Text = (flag == "n") ? "Deal template deleted successfully!" : "Deal template undeleted successfully!";
            lblerrmsg.ForeColor = System.Drawing.Color.Green;
            (this.Master as Site1).show_control(flag, pnldealenable);
        }

    }
}