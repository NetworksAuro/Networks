using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ScheuleDataLayer;
using MasterDataLayer;
using System.Threading;
using System.Globalization;
namespace NTOS
{
    public partial class EngagementSchedule : System.Web.UI.Page, MasterPageSaveInterface
    {
        int engagementid;
        Label lbl_msg;
        DropDownList drp_createdate;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hdn_schnextdate.Value = "";
            }



        }
      

        protected void Page_Load(object sender, EventArgs e)
      {
            if (!Page.IsPostBack)
            { setactivetabs(); }
            engagementid = Convert.ToInt32(Request.QueryString["engmtid"]);
            Session["search_engmt"] = engagementid.ToString ();
            //  ((LinkButton )this.Master.FindControl("imgbtnschedule")).ImageUrl = "~/Images/tabb-sc.png";
            ((Panel)this.Master.FindControl("pnl_engagement")).Enabled = true;
            (this.Master as EngagementMaster).SetActiveTab("lischedule");
            lbl_msg = (Label)this.Master.FindControl("lbl_message");
            drp_createdate = (DropDownList)this.Master.FindControl("drp_createdate");
            lbl_msg.Text = "";
            if (!string.IsNullOrEmpty(Request.QueryString["isNavigated"]))
            {
                string isnav = Request.QueryString["isNavigated"];
                if (isnav == "1")
                    Session["search"] = null;
            }
            if (Request.QueryString["status"] == "1")
            {
                lbl_msg.Text = "Engagement created successfully. Please enter the schedules below";
                lbl_msg.ForeColor = System.Drawing.Color.Green;
             
            }

            if (engagementid == 0)
            {
                createtemptable();
                //BindSchedule();
            }
            else
            {
              //  lbl_schedule.Visible = false;
                div_schedule.Visible = true;
                if (!Page.IsPostBack)
                {
                    bindScheduleDetails(Convert.ToInt32(engagementid));

                    trfooter.Visible = false;
                }
            }
        }
        public void setactivetabs()
        {
            System.Web.UI.HtmlControls.HtmlGenericControl ll = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Master.FindControl("lischedule");

            ll.Attributes.Add("class", "nav-act");

        }
        public void Reset()
        {
            ddlscheduletype.SelectedIndex = 0;
            txtscheduedate.Text = "";
            txtschedule_sttime.Text = "";
            txtschedule_endtime.Text = "";
            txtschedulenotes.Text = "";
            lblscheduleday.Text = "";

        }
        public void createtemptable()
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn();
            dc.ColumnName = "tempid";
            dc.DataType = typeof(int);
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            dt.Columns.Add(dc);
            dt.Columns.Add("schedule_type", typeof(string));
            dt.Columns.Add("schedule_date", typeof(string));
            dt.Columns.Add("schedule_day", typeof(string));
            dt.Columns.Add("schedule_st_time", typeof(string));
            dt.Columns.Add("schedule_end_time", typeof(string));
            dt.Columns.Add("schedule_notes", typeof(string));
            dt.Columns.Add("schedule_id", typeof(int)).DefaultValue = 0;
            ViewState["temptable"] = dt;
        }
        public void BindSchedule()
        {
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["temptable"];
            RepDetails.DataSource = dt;
            RepDetails.DataBind();

        }
        public void SaveTempScheduleDetails()
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            if (trfooter.Visible == true)
            {
                string schedule_type, schedule_days, schedule_stime, schedule_endtime, notes;
                Nullable<DateTime> schedulde_date;
                schedulde_date = Convert.ToDateTime(txtscheduedate.Text);
                schedule_type = ddlscheduletype.SelectedItem.Text;
                schedule_days = textInfo.ToTitleCase(Convert.ToDateTime(txtscheduedate.Text).DayOfWeek.ToString().Substring(0, 3));
                schedule_stime = Convert.ToString(txtschedule_sttime.Text);
                schedule_endtime = Convert.ToString(txtschedule_endtime.Text);
                notes = txtschedulenotes.Text;
                ScheduleData objsch = new ScheduleData();
                string msg = objsch.Engagementschedule_Insert(engagementid, schedule_type, schedulde_date, schedule_days, schedule_stime, schedule_endtime, notes);
                if (msg == "")
                {
                    bindScheduleDetails(Convert.ToInt32(engagementid));
                }
                else
                {
                    lbl_msg.Text = msg;
                    lbl_msg.ForeColor = System.Drawing.Color.Orange;
                }

            }

        }

        protected void RepDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "cancel")
            {
                bindScheduleDetails(engagementid);
            }
            if (e.CommandName.ToLower() == "edit")
            {
                e.Item.FindControl("tredit").Visible = true;
                DropDownList ddlscheduletypeE = (DropDownList)e.Item.FindControl("ddlscheduletypeE");
                HiddenField hdnscheduletypeE = (HiddenField)e.Item.FindControl("hdnscheduletypeE");
                MasterData objmst = new MasterData();
                ddlscheduletypeE.DataSource = objmst.GetLookupList("scheduletype");
                ddlscheduletypeE.DataTextField = "lkup_desc";
                ddlscheduletypeE.DataBind();
                ddlscheduletypeE.SelectedIndex = ddlscheduletypeE.Items.IndexOf(ddlscheduletypeE.Items.FindByText(hdnscheduletypeE.Value));
                e.Item.FindControl("tr_crow").Visible = false;
            }
            if (e.CommandName.ToLower() == "update")
            {
                UpdateSchedule(e.Item);
            }
            trfooter.Visible = false;

        }
        public void UpdateSchedule(RepeaterItem ri)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            DropDownList drp_scheduletype = (DropDownList)ri.FindControl("ddlscheduletypeE");
            Label lblscheduleday = (Label)ri.FindControl("lblscheduledayE");
            TextBox txtscheduedate = (TextBox)ri.FindControl("txtscheduedateE");
            //if (checkedit_scheduledate(txtscheduedate.Text) == true)
            //{
            TextBox txtschedule_sttime = (TextBox)ri.FindControl("txtschedule_sttimeE");
            TextBox txtschedule_endtime = (TextBox)ri.FindControl("txtschedule_endtimeE");
            TextBox txtschedulenotes = (TextBox)ri.FindControl("txtschedulenotesE");
            HiddenField hdnscheduleid = (HiddenField)ri.FindControl("hdnscheduleid");
            string schedule_type, schedule_days, schedule_stime, schedule_endtime, notes;
            Nullable<DateTime> schedulde_date;
            schedulde_date = Convert.ToDateTime(txtscheduedate.Text);
            schedule_type = drp_scheduletype.SelectedItem.Text;
            schedule_days = textInfo.ToTitleCase(Convert.ToDateTime(txtscheduedate.Text).DayOfWeek.ToString().Substring(0, 3));
            schedule_stime = Convert.ToString(txtschedule_sttime.Text);
            schedule_endtime = Convert.ToString(txtschedule_endtime.Text);
            notes = txtschedulenotes.Text;
            ScheduleData objsch = new ScheduleData();
            string msg = objsch.Engagementschedule_Update(engagementid, Convert.ToInt32(hdnscheduleid.Value), schedule_type, schedulde_date, schedule_days, schedule_stime, schedule_endtime, notes);
            if (msg == "")
            {
                lbl_msg.Text = "Schedule updated successfully!";
                lbl_msg.ForeColor = System.Drawing.Color.Green;
                bindScheduleDetails(Convert.ToInt32(engagementid));
            }
            else
            {
                lbl_msg.Text = msg;
                lbl_msg.ForeColor = System.Drawing.Color.Orange;
            }
            // }
        }
        public bool checkedit_scheduledate(string editdate)
        {
            DateTime temp, createddate;
            bool datevalid = DateTime.TryParse(editdate, out temp);
            bool crdatevalid = DateTime.TryParse(drp_createdate.SelectedItem.Text, out createddate);
            if (temp < createddate && datevalid == true)
            {
                lbl_msg.Text = "Schedule date should be greater than created date!";
                lbl_msg.ForeColor = System.Drawing.Color.Orange;
                //txtscheduedate.Text = string.Empty;
                txtscheduedate.Focus();
                return false;
            }
            return true;
        }
        public bool checkscheduledate()
        {
            //DateTime temp, createddate;
            //bool datevalid = DateTime.TryParse(txtscheduedate.Text, out temp);
            //bool crdatevalid = DateTime.TryParse(drp_createdate.SelectedItem.Text, out createddate);
            //if (temp < createddate && datevalid == true)
            //{
            //    lbl_msg.Text = "Schedule date should be greater than created date!";
            //    lbl_msg.ForeColor = System.Drawing.Color.Orange;
            //    //txtscheduedate.Text = string.Empty;
            //    txtscheduedate.Focus();
            //    return false;
            //}
            return true;
        }
        protected void txtscheduedate_TextChanged(object sender, EventArgs e)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            TextBox txtscheduedate = (TextBox)sender;
            System.Web.UI.HtmlControls.HtmlTableRow ri = (System.Web.UI.HtmlControls.HtmlTableRow)txtscheduedate.Parent.Parent;

            if (ri.ID.ToString().ToLower() != "trfooter")
                lblscheduleday = (Label)ri.FindControl("lblscheduledayE");
            DateTime temp, createddate;
            bool datevalid = DateTime.TryParse(txtscheduedate.Text, out temp);
            bool crdatevalid = DateTime.TryParse(drp_createdate.SelectedItem.Text, out createddate);
            //if (temp < createddate)
            //{
            //    lbl_msg.Text = "Schedule date should be greater than created date!";
            //    lbl_msg.ForeColor = System.Drawing.Color.Orange;
            //    //txtscheduedate.Text = string.Empty;
            //    txtscheduedate.Focus();
            //    return;
            //}
            if (datevalid == true)
            {
                if (txtscheduedate.Text != "")
                {
                    lblscheduleday.Text = textInfo.ToTitleCase(Convert.ToDateTime(txtscheduedate.Text).DayOfWeek.ToString().Substring(0, 3));
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Enter valid date format (mm/dd/yyyy));", true);
                txtscheduedate.Text = string.Empty;
                txtscheduedate.Focus();
            }
            txtscheduedate.Focus();
        }

        public void Save_EngagementSchedule(string type, int schedule_engid)
        {
            SaveTempScheduleDetails();
            DataTable dt = (DataTable)ViewState["temptable"];
            string schedule_type, schedule_days, schedule_stime, schedule_endtime, notes;
            Nullable<DateTime> schedulde_date;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                schedulde_date = Convert.ToDateTime(dt.Rows[i]["schedule_date"]);
                schedule_type = dt.Rows[i]["schedule_type"].ToString();
                schedule_days = dt.Rows[i]["schedule_day"].ToString();
                schedule_stime = dt.Rows[i]["schedule_st_time"].ToString();
                schedule_endtime = dt.Rows[i]["schedule_end_time"].ToString();
                notes = dt.Rows[i]["schedule_notes"].ToString();
                ScheduleData objsch = new ScheduleData();
                if (Convert.ToInt32(dt.Rows[i]["schedule_id"]) == 0)
                {
                    objsch.Engagementschedule_Insert(schedule_engid, schedule_type, schedulde_date, schedule_days, schedule_stime, schedule_endtime, notes);
                }

                //    objsch.Engagementschedule_Update(schedule_id, schedule_engid, schedule_type, schedulde_date, schedule_days, schedule_stime, schedule_endtime, notes);

            }
            //Response.Redirect("~/Engagement.aspx?engmtid=" + schedule_engid.ToString() + "&status=1");
        }

        protected void RepDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            if (e.Item.ItemType == ListItemType.Footer)
            {
                MasterData objmst = new MasterData();
                ddlscheduletype.DataSource = objmst.GetLookupList("scheduletype");
                ddlscheduletype.DataTextField = "lkup_desc";
                ddlscheduletype.DataBind();
                int CurrentIndex = ddlscheduletype.Items.IndexOf(ddlscheduletype.Items.FindByText(hdfPerf.Value));
                ddlscheduletype.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
                if ((CurrentIndex + 2) != ddlscheduletype.Items.Count)
                {
                    ddlscheduletype.SelectedIndex = CurrentIndex + 2;
                }
                string schdate = System.DateTime.Now.ToString("MM/dd/yyyy");
                if (!string.IsNullOrEmpty(hdn_schnextdate.Value))
                {
                    schdate = Convert.ToDateTime(hdn_schnextdate.Value, CultureInfo.CurrentUICulture.DateTimeFormat).AddDays(1).ToString("MM/dd/yyyy");
                }
                txtscheduedate.Text = schdate;
                lblscheduleday.Text = textInfo.ToTitleCase(Convert.ToDateTime(txtscheduedate.Text, CultureInfo.CurrentUICulture.DateTimeFormat).DayOfWeek.ToString().Substring(0, 3));
            }
        }
        public void bindScheduleDetails(int engagemetnid)
        {
            string nextdate = "";
            ScheduleData objsch = new ScheduleData();
            DataTable dt = objsch.GetScheduleDetails(engagemetnid);
            if (dt.Rows.Count > 0)
            {
                nextdate = dt.Compute("Max(Schedule_Date)", "").ToString();
                hdfPerf.Value = dt.Rows[dt.Rows.Count - 1]["SCHEDULE_TYPE"].ToString().Trim();
            }
            else
            {
                hdfPerf.Value = "0";
            }
            hdn_schnextdate.Value = nextdate;
            HiddenField hdn_schedulecount = (HiddenField)this.Page.Form.FindControl("hdn_schedulecount");
            hdn_schedulecount.Value = dt.Rows.Count.ToString();
            RepDetails.DataSource = dt;
            RepDetails.DataBind();
        }

        public void SaveData()
        {
            if (checkscheduledate() == true)
            {
                EngagementMaster EngmtMaster = (EngagementMaster)Page.Master;
                if (RepDetails.Controls.Count > 0)
                {
                    if (Convert.ToDateTime(txtschedule_sttime.Text) > Convert.ToDateTime(txtschedule_endtime.Text))
                    {
                        lbl_msg.Text = "End time should be greater than start time!";
                        lbl_msg.ForeColor = System.Drawing.Color.Orange;
                        return;
                    }
                }
                if (engagementid == 0)
                {
                    int insertengagementid = EngmtMaster.SaveEngagement("create");
                    if (insertengagementid > 0)
                    {
                        //SaveSchedule("create");
                        Response.Redirect("~/EngagementSchedule.aspx?engmtid=" + insertengagementid + "&status=1");
                    }
                    else
                    {
                        lbl_msg.Text = "Engagement exists for the same Show, City and settlement date. Please check and correct";
                        lbl_msg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    SaveTempScheduleDetails();
                    trfooter.Visible = false;
                    trfooter.Visible = false;
                    int updateengagement = EngmtMaster.SaveEngagement("update");
                    if (updateengagement != 0)
                    {
                        lbl_msg.Text = "Engagement Schedule updated successfully";
                        lbl_msg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbl_msg.Text = "Engagement settlement date should be less than the schedule dates. Please check and correct";
                        lbl_msg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        public void DeleteData(string flag)
        {
            EngagementMaster EngmtMaster = (EngagementMaster)Page.Master;
            int delflag = EngmtMaster.DeleteEngagement(flag);
            ((Panel)this.Master.FindControl("pnl_engagement")).Enabled = false;
            lbl_msg.Text = "Engagement Deleted successfully";
            lbl_msg.ForeColor = System.Drawing.Color.Green;
        }

        public void SaveSchedule(string type)
        {
            Save_EngagementSchedule(type, engagementid);
        }

        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            if (checkscheduledate() == true)
            {
                SaveTempScheduleDetails();
                trfooter.Visible = true;
                HiddenField hdn = (HiddenField)this.Master.FindControl("hdn_modify_status");
                hdn.Value = "1";
            }
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = "";
                foreach (RepeaterItem gr in RepDetails.Items)
                {
                    CheckBox chkdelete = (CheckBox)gr.FindControl("chkdelete");
                    HiddenField hdnscheduleid = (HiddenField)gr.FindControl("hdnscheduleid");

                    if (chkdelete.Checked == true)
                    {
                        if (hdnscheduleid.Value != "0")
                        {
                            ScheduleData objsch = new ScheduleData();
                            msg = objsch.Engagementschedule_Delete(Convert.ToInt32(hdnscheduleid.Value));
                        }
                    }
                }
                bindScheduleDetails(engagementid);
                trfooter.Visible = false;
                lbl_msg.Text = msg;
                lbl_msg.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                lbl_msg.Text = "Error: " + ex.Message.ToString();
                lbl_msg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void txtschedule_sttime_TextChanged(object sender, EventArgs e)
        {
            SetEndTime(txtschedule_sttime, txtschedule_endtime);

        }
        protected bool CheckDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void txtschedule_sttimeE_TextChanged(object sender, EventArgs e)
        {
            TextBox txtst = (TextBox)sender;
            RepeaterItem ri = (RepeaterItem)txtst.Parent.Parent.Parent;
            TextBox endtime = (TextBox)ri.FindControl("txtschedule_endtimeE");
            SetEndTime(txtst, endtime);
        }
        public void SetEndTime(TextBox txtst_time, TextBox txtend_time)
        {
            bool b = CheckDate(txtst_time.Text);
            if (b == false)
            {
                return;
            }
            DateTime stime = Convert.ToDateTime(txtst_time.Text);
            TimeSpan t = new TimeSpan(3, 0, 0);
            DateTime etime = stime + t;

            txtend_time.Text = etime.ToString("t");
            txtend_time.Focus();
        }

    }
}