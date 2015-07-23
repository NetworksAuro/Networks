using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MasterDataLayer;
using EngagementDataLayer;
using System.Data;
using ScheuleDataLayer;
using System.Globalization;
using System.Text.RegularExpressions;
using PersonalDataLayer;

using System.Xml;
using System.IO;
namespace NTOS
{
    public partial class EngagementSearchMaster : System.Web.UI.MasterPage
    {
        MasterData mdl = new MasterData();
        EngagementData edl = new EngagementData();
        ScheduleData sdl = new ScheduleData();

        int engagementid, UserID;
        DataTable engagementtable = null;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Context.Session["username"] == null)
            {
                Response.Redirect("~/Account/Login.aspx?url=" + HttpContext.Current.Request.Url.AbsolutePath.Replace("http://", "") + "");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
            engagementid = Convert.ToInt32(Request.QueryString["engmtid"]);
            if (Convert.ToString(Session["search_engmt"]) != "")
            {
                hdn_engagementid.Value = Convert.ToString(Session["search_engmt"]);
                engagementid = Convert.ToInt32(Session["search_engmt"]);
            }
            else
            {
                hdn_engagementid.Value = engagementid.ToString();
            }


            string url = HttpContext.Current.Request.Url.AbsoluteUri;


            //Panel1.Attributes.Add("class", "");
            if (!Page.IsPostBack)
            {


                drp_show.DataSource = mdl.Getshows("0");
                drp_show.DataTextField = "show_name";
                drp_show.DataValueField = "show_id";
                drp_show.DataBind();
                drp_show.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });

                drp_presenter.DataSource = mdl.GetPresenters(null);
                drp_presenter.DataTextField = "presenter_name";
                drp_presenter.DataValueField = "presenter_id";
                drp_presenter.DataBind();
                drp_presenter.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });

                drp_contact.DataSource = edl.GetContact();
                drp_contact.DataTextField = "fullname";
                drp_contact.DataValueField = "personal_id";
                drp_contact.DataBind();
                drp_contact.Items.Insert(0, new ListItem { Value = "0", Text = "--Contact--", Selected = true });

                drp_venue.DataSource = mdl.GetVenues(null);
                drp_venue.DataTextField = "venue_name";
                drp_venue.DataValueField = "venue_id";
                drp_venue.DataBind();
                drp_venue.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });

                drp_city.DataSource = mdl.GetCityStates();
                drp_city.DataTextField = "city_state";
                drp_city.DataValueField = "city_id";
                drp_city.DataBind();
                drp_city.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });

                //drp_metro.DataSource = mdl.GetMetroCityStates(null);
                //drp_metro.DataTextField = "metro_state";
                //drp_metro.DataValueField = "city_id";
                //drp_metro.DataBind();
                //drp_metro.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });

                DataTable dtlookup = mdl.GetLookupList("");

                #region As per changes on 06-Jun-14


                //if (dtlookup.Rows.Count > 0)
                //{
                //    DataTable dtpricescale = dtlookup.Select("lkup_group = 'pricescalestatus'").CopyToDataTable();

                //    drp_pricescale.DataSource = dtpricescale;
                //    drp_pricescale.DataTextField = "lkup_desc";
                //    drp_pricescale.DataValueField = "lkup_desc";
                //    drp_pricescale.DataBind();
                //}
                //drp_pricescale.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
                //if (dtlookup.Rows.Count > 0)
                //{
                //    DataTable dtcontract = dtlookup.Select("lkup_group = 'contractstatus'").CopyToDataTable();

                //    drp_contract.DataSource = dtcontract;
                //    drp_contract.DataTextField = "lkup_desc";
                //    drp_contract.DataValueField = "lkup_desc";
                //    drp_contract.DataBind();
                //}
                //drp_contract.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
                //if (dtlookup.Rows.Count > 0)
                //{
                //    DataTable dtoffer = dtlookup.Select("lkup_group = 'offerstatus'").CopyToDataTable();

                //    drp_offer.DataSource = dtoffer;
                //    drp_offer.DataTextField = "lkup_desc";
                //    drp_offer.DataValueField = "lkup_desc";
                //    drp_offer.DataBind();
                //}
                //drp_offer.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
                //if (dtlookup.Rows.Count > 0)
                //{
                //    DataTable dtexpense = dtlookup.Select("lkup_group = 'expensestatus'").CopyToDataTable();

                //    drp_expense.DataSource = dtexpense;
                //    drp_expense.DataTextField = "lkup_desc";
                //    drp_expense.DataValueField = "lkup_desc";
                //    drp_expense.DataBind();
                //}
                //drp_expense.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
                //if (dtlookup.Rows.Count > 0)
                //{
                //    DataTable dtdealmemo = dtlookup.Select("lkup_group = 'memostatus'").CopyToDataTable();

                //    drp_dealmemo.DataSource = dtdealmemo;
                //    drp_dealmemo.DataTextField = "lkup_desc";
                //    drp_dealmemo.DataValueField = "lkup_desc";
                //    drp_dealmemo.DataBind();
                //}
                //drp_dealmemo.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true }); 
                #endregion


                if (dtlookup.Rows.Count > 0)
                {
                    DataTable dtstatus = dtlookup.Select("lkup_group = 'engagementstatus'").CopyToDataTable();

                    drp_status.DataSource = dtstatus;
                    drp_status.DataTextField = "lkup_desc";
                    drp_status.DataValueField = "lkup_desc";
                    drp_status.DataBind();
                }
                drp_status.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
                drp_status.SelectedIndex = drp_status.Items.IndexOf(drp_status.Items.FindByText("Ghosted"));

                drp_createdate.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
                //===============================================
                string diarytitle = Request.QueryString["diaryshowid"];

                if (diarytitle != null && diarytitle != "0")
                {
                    string dtitle = Convert.ToString(diarytitle.Split('(').GetValue(0));
                    string ddate = Convert.ToString(diarytitle.Split('(').GetValue(1));
                    hdn_crdate.Value = ddate;
                    drp_createdate.Items.Add(ddate);
                    drp_createdate.SelectedIndex = drp_createdate.Items.IndexOf(drp_createdate.Items.FindByText(Convert.ToString(ddate)));
                    txt_revisiondate.Text = DateTime.Now.ToString();

                    DataTable dt = new DataTable();
                    dt = mdl.Getshowsidfromdiary(dtitle);
                    if (dt.Rows.Count > 0)
                    {
                        drp_show.SelectedIndex = drp_show.Items.IndexOf(drp_show.Items.FindByValue(Convert.ToString(dt.Rows[0]["title"])));
                    }
                }
                //===============================================
                if (engagementid > 0)
                {
                    // imgbtn_Reset.Visible = true;
                    lbl_header.Text = "Modify Engagement";
                    // link_new.Style.Add("visibility", "visible");
                    try
                    {
                        engagementtable = edl.GetEngagementDetailsById(engagementid);
                        if (engagementtable.Rows.Count > 0)
                        {
                            SetButtonVisible(engagementtable.Rows[0]["engt_active_flag"].ToString().ToLower());
                            BindEngagementFieldData();

                        }
                    }
                    catch (Exception ex)
                    {
                        lbl_message.Text = "Error: " + ex.Message.ToString();
                        lbl_message.ForeColor = System.Drawing.Color.Red;
                    }
                    string Enddate = "", stdate = drp_createdate.SelectedItem.Text;
                    DataTable recordcheck = sdl.GetScheduleDetails(engagementid);
                    if (recordcheck.Rows.Count > 0)
                    {
                        hdn_schedulecount.Value = "1";
                        Enddate = recordcheck.Compute("Max(Schedule_Date)", "").ToString();
                        stdate = recordcheck.Compute("Min(Schedule_Date)", "").ToString();
                    }
                    else
                    {
                        hdn_schedulecount.Value = "0";
                    }
                    engsummaryDiv.Visible = true;

                    string strhtml = "";
                    strhtml = "<div class='largefont'>" + drp_show.SelectedItem.Text + " </div>";
                    strhtml = strhtml + "<div class='sfont''>" + drp_city.SelectedItem.Text + "  | " + stdate + "- " + Enddate + "  |  " + drp_status.SelectedItem.Text + " </div>";
                    strhtml = strhtml + "<div class='sfont''>" + drp_presenter.SelectedItem.Text + " | " + drp_venue.SelectedItem.Text + " </div>";
                    engsummaryDiv.InnerHtml = strhtml;

                }
                else
                {
                    engsummaryDiv.Visible = false;
                    lbl_header.Text = "Create Engagement";
                    hdn_showid.Value = "0";
                    hdn_schedulecount.Value = "0";

                    txt_revisiondate.Text = DateTime.Now.ToShortDateString();
                }
                if (this.Page.Title == "Search")
                {
                    engsummaryDiv.Visible = false;
                    TdSummary.Visible = false;
                }
                if (Convert.ToString(Session["userrole"]) == "reader")
                {
                    imgbtnsave.Visible = false;
                    imgbtndelete.Visible = false;
                    pnl_engagement.Enabled = false;

                }

                //if (Convert.ToString(Session["userrole"]) == "compmanager")
                //{
                //    imgbtnsave.Visible = false;
                //    imgbtndelete.Visible = false;
                //    pnl_engagement.Enabled = false;
                //}


                hdnreqlist.Value = (drp_show.ClientID + "," + drp_presenter.ClientID + "," + drp_createdate.ClientID + "," + drp_city.ClientID);
                if (string.IsNullOrEmpty(Request.QueryString["search"]) == false)
                {
                    navpage.Visible = !string.IsNullOrEmpty(Request.QueryString["search"]);
                    btnlist.Visible = !string.IsNullOrEmpty(Request.QueryString["search"]);
                    btnsearch1.Visible = !string.IsNullOrEmpty(Request.QueryString["search"]);
                    lisearchlist.Visible = false;
                    lblrecindex.Text = Convert.ToString(Session["recindex"]);
                    DataTable dt = new DataTable();
                    dt = (DataTable)Session["search"];
                    if (dt.Rows.Count > 0)
                    {

                        if (drpdwnSearch.Items.Count == 0)
                        {
                            drpdwnSearch.Items.Add(new ListItem("--Search--", "0"));
                            drpdwnSearch.Items.Add(new ListItem("New", "1"));
                            drpdwnSearch.Items.Add(new ListItem("Constrain", "2"));
                            drpdwnSearch.Items.Add(new ListItem("Extended", "3"));
                            drpdwnSearch.DataBind();
                        }
                        drpdwnSearch.Visible = true;

                        ImageButton1.Visible = false;
                        btnsearch1.Visible = false;
                        lisearchlist.Visible = true;


                    }



                    lblrectot.Text = "of " + dt.Rows.Count.ToString();
                }
            }
            //modpop.Show();
        }
        public void SetButtonVisible(string engt_active_flag)
        {
            bool vis_flg;
            string role = Convert.ToString(Session["userrole"]);
            vis_flg = (engt_active_flag.ToLower() == "y") ? true : false;
            vis_flg = (role == "reader") ? false : vis_flg;

            imgbtndelete.Visible = vis_flg;
            imgbtnundelete.Visible = (role == "reader") ? vis_flg : !vis_flg;
            imgbtnsave.Visible = vis_flg;
            pnlcontent.Enabled = vis_flg;
            // pnl_engagement.Enabled = vis_flg;

            //if (engt_active_flag.ToLower() == "y")
            //{
            //    imgbtndelete.Visible = vis_flg;
            //    imgbtnundelete.Visible = !vis_flg;
            //    imgbtnsave.Visible = vis_flg;                
            //    pnlcontent.Enabled = vis_flg;
            //}
            //else
            //{                
            //    imgbtnsave.Visible = false;
            //    imgbtndelete.Visible = false;
            //    imgbtnundelete.Visible = true;
            //    pnlcontent.Enabled = false;
            //}
        }

        //protected void ddlHistory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ddlHistory.SelectedIndex > 0)
        //            Response.Redirect(ddlHistory.SelectedItem.Value);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        public void BindEngagementFieldData()
        {
            string role = Convert.ToString(Session["userrole"]);
            int assign_show_flag = Convert.ToInt32(engagementtable.Rows[0]["AssignFlag"]);
            if (role == "compmanager")
            {
                string[] p_title = new string[3] { "engagement deal", "engagement price scales", "engagement schedule" };
                int tflg = Array.IndexOf(p_title, this.Page.Title.ToLower());
                if (assign_show_flag == 0 || (tflg != -1 && assign_show_flag == 1))
                {
                    SetButtonVisible("n");
                    imgbtnundelete.Visible = false;
                }
                if (tflg > 0 && assign_show_flag == 1 && this.Page.Title.ToLower() == "engagement schedule")
                {
                    pnl_engagement.Enabled = true;
                }
            }
            string beast_flg = engagementtable.Rows[0]["ENGT_BEAST_FLAG"].ToString().ToLower();
            divbeast.Visible = (beast_flg == "y") ? true : false;

            drp_show.SelectedValue = engagementtable.Rows[0]["engt_show_id"].ToString();
            hdn_showid.Value = engagementtable.Rows[0]["engt_show_id"].ToString();
            hdn_engagementid.Value = engagementtable.Rows[0]["engt_id"].ToString();
            drp_presenter.SelectedValue = engagementtable.Rows[0]["engt_presenter_id"].ToString();
            drp_createdate.SelectedItem.Text = Convert.ToDateTime(engagementtable.Rows[0]["engt_cr_date"]).ToShortDateString();
            drp_createdate.SelectedItem.Value = Convert.ToDateTime(engagementtable.Rows[0]["engt_cr_date"]).ToShortDateString();
            // txt_mileage.Text = engagementtable.Rows[0]["engt_mileage"].ToString();
            if (engagementtable.Rows[0]["engt_venue_id"] != DBNull.Value)
            {
                drp_venue.SelectedValue = engagementtable.Rows[0]["engt_venue_id"].ToString();
            }
            else
            {
                drp_venue.SelectedValue = "0";
            }
            if (engagementtable.Rows[0]["engt_personal_id"] != DBNull.Value)
            {
                drp_contact.SelectedValue = engagementtable.Rows[0]["engt_personal_id"].ToString();
            }
            else
            {
                drp_contact.SelectedValue = "0";
            }
            if (string.IsNullOrEmpty(engagementtable.Rows[0]["engt_revision_dt"].ToString()))
            {
                txt_revisiondate.Text = Convert.ToString(engagementtable.Rows[0]["engt_revision_dt"]);
            }
            else
            {
                txt_revisiondate.Text = Convert.ToDateTime(engagementtable.Rows[0]["engt_revision_dt"]).ToShortDateString();
            }

            //   txt_traveltime.Text = engagementtable.Rows[0]["engt_travel_time"].ToString();

            drp_status.SelectedValue = engagementtable.Rows[0]["engt_status"].ToString();
            //    drp_pricescale.SelectedValue = engagementtable.Rows[0]["engt_price_scale_status"].ToString();
            drp_subscription.SelectedValue = engagementtable.Rows[0]["engt_subscription_flag"].ToString();
            if (drp_subscription.SelectedValue == "Y")
            {
                txt_subscription.Visible = true;
            }
            if (engagementtable.Rows[0]["engt_subscription_amt"] != DBNull.Value)
            {
                txt_subscription.Text = engagementtable.Rows[0]["engt_subscription_amt"].ToString();
            }
            //    drp_contract.SelectedValue = engagementtable.Rows[0]["engt_contract"].ToString();

            //    drp_offer.SelectedValue = engagementtable.Rows[0]["engt_offer"].ToString();
            drp_repeat.SelectedValue = engagementtable.Rows[0]["engt_repeat"].ToString();
            //     drp_expense.SelectedValue = engagementtable.Rows[0]["engt_expenses"].ToString();
            //   drp_dealmemo.SelectedValue = engagementtable.Rows[0]["engt_deal_memo"].ToString();

            drp_city.SelectedValue = engagementtable.Rows[0]["engt_city_id"].ToString();
            if (engagementtable.Rows[0]["metro_city_id"] != DBNull.Value)
            {
                // drp_metro.SelectedValue = engagementtable.Rows[0]["metro_city_id"].ToString();
            }
            else
            {
                //  drp_metro.SelectedValue = "0";
            }
            //    if (engagementtable.Rows[0]["engt_exchange_rate"] != DBNull.Value)
            //    txt_exchange.Text = engagementtable.Rows[0]["engt_exchange_rate"].ToString();

        }
        public int SaveEngagement(string mode)
        {
            Nullable<int> venu = null, metro = null, contact = null;
            DateTime createdate = Convert.ToDateTime(drp_createdate.SelectedItem.Text);
            Nullable<DateTime> revisiondate;
            Nullable<decimal> subscriptionamt = null, mileage = null, traveltime = null, exchange = null;

            mileage = 0;
            revisiondate = DateTime.Now;
            traveltime = 0;
            subscriptionamt = txt_subscription.Text.AutoformatDecimal();
            venu = (drp_venue.SelectedValue == "0") ? venu : Convert.ToInt16(drp_venue.SelectedValue);
            metro = 0;
            exchange = 0;
            contact = (drp_contact.SelectedValue == "0") ? contact : Convert.ToInt16(drp_contact.SelectedValue);

            if (mode == "create")
            {
                int insertflag = 0;
                DataTable checkrecord = edl.GetEngagementDetails(Convert.ToInt16(drp_show.SelectedValue), Convert.ToInt16(drp_city.SelectedValue), Convert.ToDateTime(drp_createdate.SelectedItem.Text));
                if (checkrecord.Rows.Count == 0)
                {
                    int insertengagement = edl.CreateEngagement(Convert.ToInt16(drp_show.SelectedValue), Convert.ToInt16(drp_presenter.SelectedValue), Convert.ToDateTime(drp_createdate.SelectedItem.Text),
                        mileage, venu, contact, revisiondate, traveltime,
                        drp_status.SelectedValue, "0", drp_subscription.SelectedValue, subscriptionamt, "0",
                        "0", drp_repeat.SelectedValue, "0", "0", Convert.ToInt16(drp_city.SelectedValue), metro, exchange);
                    hdn_engagementid.Value = insertengagement.ToString();
                    insertflag = insertengagement;
                }
                return insertflag;
            }
            else
            {
                int updateengagement = 0;
                //DateTime minscheduledate = edl.GetMinScheduleDate(engagementid);
                //if (Convert.ToDateTime(drp_createdate.SelectedItem.Text) <= minscheduledate)
                //{
                updateengagement = edl.UpdateEngagement(engagementid, Convert.ToInt16(drp_show.SelectedValue), Convert.ToInt16(drp_presenter.SelectedValue), Convert.ToDateTime(drp_createdate.SelectedItem.Text),
                    mileage, venu, contact, revisiondate, traveltime,
                    drp_status.SelectedValue, "0", drp_subscription.SelectedValue, subscriptionamt, "0",
                    "0", drp_repeat.SelectedValue, "0", "0", Convert.ToInt16(drp_city.SelectedValue), metro, exchange);
                //}
                return updateengagement;
            }
        }
        public int DeleteEngagement(string delflag)
        {
            int deleteflag = edl.DeleteEngagement(engagementid, delflag);
            imgbtndelete.Visible = false;
            imgbtnsave.Visible = false;
            return deleteflag;
        }
        protected void drp_subscription_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drp_subscription.SelectedValue == "Y")
            {
                lbl_subscription.Visible = true;
                txt_subscription.Visible = true;
            }
            else
            {
                lbl_subscription.Visible = false;
                txt_subscription.Visible = false;
                txt_subscription.Text = "";
            }
        }
        protected void drp_city_SelectedIndexChanged(object sender, EventArgs e)
        {
            string diarytitle = Request.QueryString["diaryshowid"];
            engagementtable = edl.GetEngagementDates(Convert.ToInt16(drp_show.SelectedValue), Convert.ToInt32(drp_city.SelectedValue), "0");
            drp_createdate.Items.Clear();
            drp_createdate.DataSource = engagementtable;
            drp_createdate.DataTextField = "engt_cr_date";
            drp_createdate.DataValueField = "engt_cr_date";
            drp_createdate.DataBind();

            drp_createdate.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
            if (engagementtable.Rows.Count == 1)
            {
                if (diarytitle != null && diarytitle != "0")
                {
                    drp_createdate.Items.Add(hdn_crdate.Value);
                    drp_createdate.SelectedIndex = drp_createdate.Items.IndexOf(drp_createdate.Items.FindByText(Convert.ToString(hdn_crdate.Value)));
                }
                else
                {
                    drp_createdate.SelectedIndex = 1;
                }
                engagementtable = edl.GetEngagementDetails(Convert.ToInt16(drp_show.SelectedValue), Convert.ToInt32(drp_city.SelectedValue), Convert.ToDateTime(drp_createdate.SelectedValue));
                if (engagementtable.Rows.Count > 0)
                {
                    BindEngagementFieldData();
                }
            }
            if (engagementtable.Rows.Count == 0)
            {
                string drpcdate;
                if (diarytitle != null && diarytitle != "0")
                {
                    drpcdate = hdn_crdate.Value;
                }
                else
                {
                    drpcdate = System.DateTime.Now.ToShortDateString();
                }
                drp_createdate.Items.Insert(1, new ListItem { Value = drpcdate, Text = drpcdate });
                drp_createdate.SelectedIndex = 1;
                //   txt_revisiondate.Text = drpcdate;
                txt_revisiondate.Text = DateTime.Today.ToString();
            }
        }
        protected void drp_createdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drp_createdate.SelectedValue != "0")
            {
                try
                {
                    engagementtable = edl.GetEngagementDetails(Convert.ToInt16(drp_show.SelectedValue), Convert.ToInt32(drp_city.SelectedValue), Convert.ToDateTime(drp_createdate.SelectedValue));
                    if (engagementtable.Rows.Count > 0)
                    {
                        BindEngagementFieldData();
                    }
                }
                catch (Exception ex)
                {
                    lbl_message.Text = "Error: " + ex.Message.ToString();
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        //protected void imgbtncalendar_Click(object sender, ImageClickEventArgs e)
        //{

        //    if (Session["calbtnflag"].ToString() == "0")
        //    {
        //        cal_createdate.Visible = true;
        //        Session["calbtnflag"] = "1";
        //    }
        //    else
        //    {
        //        cal_createdate.Visible = false;
        //        Session["calbtnflag"] = "0";
        //    }
        //}
        public void SetActiveTab(string ctlname)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl ll = (System.Web.UI.HtmlControls.HtmlGenericControl)this.FindControl(ctlname);
            ll.Attributes.Add("class", "nav-act");
        }
        protected void cal_createdate_SelectionChanged(object sender, EventArgs e)
        {
            drp_createdate.SelectedItem.Text = hdfdate.Value;
            drp_createdate.SelectedItem.Value = hdfdate.Value;
            //drp_createdate.SelectedItem.Text = cal_createdate.SelectedDate.ToShortDateString();
            //drp_createdate.SelectedItem.Value = cal_createdate.SelectedDate.ToShortDateString();
            //   if (lbl_header.Text == "Create Engagement")
            //  txt_revisiondate.Text = hdfdate.Value;
            //cal_createdate.Visible = false;
            //Session["calbtnflag"] = "0";
        }
        protected void btninsert_Click(object sender, EventArgs e)
        {
            try
            {
                MasterPageSaveInterface insertinterface = Page as MasterPageSaveInterface;
                if (insertinterface != null)
                {
                    insertinterface.SaveData();
                    hdn_modify_status.Value = "0";
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = "Error: " + ex.Message.ToString();
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }

        }
        protected void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                MasterPageSaveInterface updateinterface = Page as MasterPageSaveInterface;
                if (updateinterface != null)
                {
                    updateinterface.DeleteData("n");
                    SetButtonVisible("n");
                    lbl_message.Text = "Engagement Deleted successfully!";
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = "Error: " + ex.Message.ToString();
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btnundelete_Click(object sender, EventArgs e)
        {
            try
            {
                MasterPageSaveInterface updateinterface = Page as MasterPageSaveInterface;
                if (updateinterface != null)
                {
                    updateinterface.DeleteData("y");
                    SetButtonVisible("y");
                    lbl_message.Text = "Engagement Undeleted successfully!";
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = "Error: " + ex.Message.ToString();
                lbl_message.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void imgbtndiary_Click(object sender, ImageClickEventArgs e)
        {
            Server.Transfer("~/Diary.aspx");
        }
        protected void imgbtnschedule_Click(object sender, EventArgs e)
        {


            Response.Redirect("~/EngagementSchedule.aspx?engmtid=" + hdn_engagementid.Value);
        }
        protected void imgbtndeal_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/EngagementDeal.aspx?engmtid=" + hdn_engagementid.Value + "&showid=" + hdn_showid.Value + "&schcount=" + hdn_schedulecount.Value);
        }
        protected void imgbtnps_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EngagementPriceScales.aspx?engmtid=" + hdn_engagementid.Value + "&schcount=" + hdn_schedulecount.Value);
        }
        protected void imgbtnex_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EngagementExpenses.aspx?engmtid=" + hdn_engagementid.Value + "&schcount=" + hdn_schedulecount.Value);
        }
        protected void imgbtnbo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EngagementBoxOffice.aspx?engmtid=" + hdn_engagementid.Value + "&schcount=" + hdn_schedulecount.Value);
        }
        protected void imgbtnds_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EngagementDiscount.aspx?engmtid=" + hdn_engagementid.Value + "&schcount=" + hdn_schedulecount.Value);
        }
        protected void imgbtncs_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EngagementCoversheet.aspx?engmtid=" + hdn_engagementid.Value + "&schcount=" + hdn_schedulecount.Value);
        }
        protected void drp_venue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drp_venue.SelectedValue != "0")
            {
                DataTable venuecity = edl.GetVenueCity(Convert.ToInt32(drp_venue.SelectedValue));
                if (Convert.ToInt32(venuecity.Rows[0]["venue_city_id"]) > 0)
                {
                    drp_city.SelectedValue = venuecity.Rows[0]["venue_city_id"].ToString();
                }
                if (Convert.ToInt32(venuecity.Rows[0]["metro_city_id"]) > 0)
                {
                    //drp_metro.SelectedValue = venuecity.Rows[0]["metro_city_id"].ToString();
                }
            }
        }
        protected void imgbtn_Reset_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                MasterPageSaveInterface updateinterface = Page as MasterPageSaveInterface;
                if (updateinterface != null)
                {
                    updateinterface.Reset();
                }
            }
            catch (Exception)
            {
                Label lbl = (Label)MainContent.FindControl("lbl_message");
                lbl.Text = "Error: Data did not submit. Please contact system administrator!";
                lbl.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnredirect_Click(object sender, EventArgs e)
        {
            switch (hdn_modify_status.Value)
            {
                case "schedule":
                    {
                        Response.Redirect("~/EngagementSchedule.aspx?engmtid=" + hdn_engagementid.Value);
                        break;
                    }
                case "deal":
                    {
                        Response.Redirect("~/EngagementDeal.aspx?engmtid=" + hdn_engagementid.Value + "&showid=" + hdn_showid.Value + "&schcount=" + hdn_schedulecount.Value);
                        break;
                    }
                case "pricescale":
                    {
                        Response.Redirect("~/EngagementPriceScales.aspx?engmtid=" + hdn_engagementid.Value + "&schcount=" + hdn_schedulecount.Value);
                        break;
                    }
                case "expense":
                    {
                        Response.Redirect("~/EngagementExpenses.aspx?engmtid=" + hdn_engagementid.Value + "&schcount=" + hdn_schedulecount.Value);
                        break;
                    }
                case "boxoffice":
                    {
                        Response.Redirect("~/EngagementBoxOffice.aspx?engmtid=" + hdn_engagementid.Value + "&schcount=" + hdn_schedulecount.Value);
                        break;
                    }
                case "discount":
                    {
                        Response.Redirect("~/EngagementDiscount.aspx?engmtid=" + hdn_engagementid.Value + "&schcount=" + hdn_schedulecount.Value);
                        break;
                    }
                case "coversheet":
                    {
                        Response.Redirect("~/EngagementCoversheet.aspx?engmtid=" + hdn_engagementid.Value + "&schcount=" + hdn_schedulecount.Value);
                        break;
                    }
            }

        }


        protected void btncancel_Click(object sender, EventArgs e)
        {

        }
        protected void btnSearch1_Click(object sender, EventArgs e)
        {
            if (this.Page.Title != "Search")
            {
                Response.Redirect("Search.aspx", false);
            }
            ISearch insertinterface = Page as ISearch;
            if (insertinterface != null)
            {
                string url = HttpContext.Current.Request.Url.AbsoluteUri;

                if (url.Contains("Schedule") == true)
                    insertinterface.GetSearchData();
                if (url.Contains("Deal") == true)
                    insertinterface.GetSearchEngmtDealData();
                if (url.Contains("Price") == true)
                    insertinterface.GetSearchEngmtPriceData();
                if (url.Contains("Expenses") == true)
                    insertinterface.GetSearchEngmtExpensesData();
                if (url.Contains("Box") == true)
                    insertinterface.GetSearchEngmtBoxOFfice();
                if (url.Contains("Discount") == true)
                    insertinterface.GetSearchEngtDiscount();
            }


        }
        public void hidesummary()
        {
            TdSummary.Visible = false;
        }

        public void loadsearcheng(string a1, string a2, string a3, string a4, string a5)
        {
            DataTable dt = new DataTable();
            //  dt = mdl.GetSearchDataNew("Engagement", a1, a2, a3, a4, a5);
            // ucsearch.LoadSearchDataE(dt);

            Panel1.Attributes.Add("class", "popup");
            modpop.Show();
        }

        protected void imgbtnprint_Click(object sender, EventArgs e)
        {
            Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri, false);
        }

        #region Search
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            MoveRecord("f");

        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            MoveRecord("p");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            MoveRecord("n");
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            MoveRecord("l");
        }
        public void MoveRecord(string type)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["search"];
            Int32 idx = 0, dtcount = dt.Rows.Count;
            string keyid = "";
            switch (type)
            {
                case "f":
                    {
                        idx = 0;
                        keyid = dt.Rows[idx]["keyid"].ToString();
                        idx++;
                        break;
                    }
                case "p":
                    {
                        idx = Convert.ToInt32(lblrecindex.Text) - 1;
                        if (idx == 0)
                            break;
                        keyid = dt.Rows[idx]["keyid"].ToString(); break;
                    }
                case "n":
                    {
                        idx = Convert.ToInt32(lblrecindex.Text) + 1;
                        if (idx > dtcount)
                            break;
                        keyid = dt.Rows[idx]["keyid"].ToString(); break;
                    }
                case "l": { idx = dtcount; keyid = dt.Rows[dtcount - 1]["keyid"].ToString(); break; }
            }

            if (keyid != "")
            {
                string url = HttpContext.Current.Request.Url.AbsolutePath + "?engmtid=" + keyid + "&search=y";

                Session["recindex"] = idx.ToString();
                Response.Redirect(url);
            }
        }
        #endregion
        protected void btnlist_Click(object sender, EventArgs e)
        {

            //Response.Redirect("Search.aspx?mode=list", false);


            DataTable dt = new DataTable();
            dt = (DataTable)Session["search"];
            // ucsearch.LoadSearchDataE(dt);
            gvrep.DataSource = dt;
            gvrep.DataBind();
            gvrep.HeaderRow.Cells[0].Text = Convert.ToString(this.Page.Title).Contains("Engagement") == true ? "Show" : Convert.ToString(this.Page.Title).Contains("Personnel") == true ? "First name" : Convert.ToString(this.Page.Title);
            Panel1.Attributes.Add("class", "popup");
            modpop.Show();


        }
        protected void gvrep_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

            var title = this.Page.Title;
            //var a_tag = document.getElementById("A2");
            Response.Redirect("Search.aspx?title=" + title + "&engmt_id=" + engagementid + "&type=1", false);

        }
        protected void drpdwnSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = drpdwnSearch.SelectedItem.Value;
            if (Convert.ToInt16(type) > 0)
            {

                var title = this.Page.Title;
                //var a_tag = document.getElementById("A2");
                Response.Redirect("Search.aspx?title=" + title + "&type=" + type + "&engmt_id=" + engagementid, false);

            }
        }


    }

}