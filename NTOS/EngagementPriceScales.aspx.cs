using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PriceScaleDataLayer;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web.UI.HtmlControls;
namespace NTOS
{
    public partial class EngagementPriceScales : System.Web.UI.Page, MasterPageSaveInterface
    {
        Label lbl_msg;

        PriceScaleData psd = new PriceScaleData();
        protected void Demo1_ButtonClickDemo(object sender, EventArgs e)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            // ( (Panel)this.Master.FindControl("pnl_engagement")).Visible = false;           
            //uc_ps1.ButtonClickDemo += new EventHandler(Demo1_ButtonClickDemo);
            int engagementid = Convert.ToInt32(Request.QueryString["engmtid"]);
            Session["search_engmt"] = engagementid.ToString();
            string schcount = Request.QueryString["schcount"];
            //((ImageButton)this.Master.FindControl("imgbtnps")).ImageUrl = "~/Images/tabb-ps.png";
            (this.Master as EngagementMaster).SetActiveTab("liprice");
            (this.Master as EngagementMaster).hidesummary();
            hdn_engagementid.Value = engagementid.ToString();
            hdn_schedulecount.Value = schcount;
            ValidationSummary mastersummary = (ValidationSummary)this.Master.FindControl("val_summarymaster");
            mastersummary.Enabled = false;
            ((Label)this.Master.FindControl("lbl_message")).Visible = true;
            lbl_msg = (Label)this.Master.FindControl("lbl_message");
            lbl_msg.Text = "";

            if (!Page.IsPostBack)
            {
                hdnintialseat_total.Value = "0";
              //  lbl_pstotal.Text = "$0.00";
              //  lbl_psshows.Text = "0";
               // lbl_pssubtotal.Text = "$0.00";
                decimal dealtax = psd.GetDealTax(engagementid);
                lbl_pstax.Text = dealtax.ToString();
                //if (engagementid > 0 && schcount != "0")
                if (engagementid > 0)
                {
                    divmainpnl.Visible = true;
                    lbl_ps.Visible = false;
                    div_ps.Visible = true;
                    loadGrid();
                }
            }

        }
        public void loadGrid1()
        {
            ContentPlaceHolder contentid = (ContentPlaceHolder)this.Master.FindControl("MainContent");
            UserControl uc_ps = (UserControl)contentid.FindControl("uc_ps1");
            GridView gdv_ps1 = (GridView)uc_ps.FindControl("gdv_ps1");
            int engagementid = Convert.ToInt32(Request.QueryString["engmtid"]);
            Int32 reccount = 0;
            string ps_scale = "A";
            DataTable dt = psd.GetPriceScales(engagementid, reccount, ps_scale);
            DataTable pdaytime = psd.GetScheduleDayTime(engagementid);

            DataTable pfcount = dt.DefaultView.ToTable(true, "ps_scale");
            div_ps1.Visible = true;
            if (pfcount.Rows.Count > 0)
            {
                //lbl_ps1.Text = pfcount.Rows[0]["ps_scale"].ToString();

            }
            chk_ps1.DataSource = pdaytime;
            chk_ps1.DataTextField = "pdaytime";
            chk_ps1.DataValueField = "schedule_id";
            chk_ps1.DataBind();
            int wkcount = 0;
            string weeklist = "", schdaylist = "";
            wkcount = Convert.ToInt32(pdaytime.Rows[0]["WeekCount"]);
            for (int i = 0; i < wkcount; i++)
            {
                chklistweek1.Items.Add(new ListItem("Week " + (i + 1), (i + 1).ToString(), true));
            }

            if (dt.Rows.Count > 0)
            {
                gdv_ps1.DataSource = dt;
                gdv_ps1.DataBind();
                weeklist = dt.Rows[0]["WeekList"].ToString();
                schdaylist = dt.Rows[0]["SCHEDULEDAYLIST"].ToString();

                //lbl_ps1total.Text = Convert.ToDecimal(dt.Compute("sum(ps_sale_amount)", null)).ToString("C2", new CultureInfo("en-US"));
            }
            else
            {
                AddEmptyRow(gdv_ps1, dt);
            }
            if (pfcount.Rows.Count > 0)
            {
                setGridTotal(pfcount.Rows.Count, dt);
            }
            //lbl_ps1shows.Text = "0";
            //lbl_ps1subtotal.Text = "0";
            if (!string.IsNullOrEmpty(weeklist))
            {
                for (int i = 0; i < weeklist.Split(',').Length; i++)
                {
                    chklistweek1.Items.FindByValue(weeklist.Split(',').GetValue(i).ToString()).Selected = true;
                }
            }
            if (!string.IsNullOrEmpty(schdaylist))
            {
                for (int i = 0; i < schdaylist.Split(',').Length; i++)
                {
                    chk_ps1.Items.FindByValue(schdaylist.Split(',').GetValue(i).ToString()).Selected = true;
                }
            }
            //  uc_ps1.cal();
        }
        public void loadGrid()
        {
            DataTable dtweek = new DataTable();
            DataRow dr;
            dtweek.Columns.Add("WKID", typeof(int)).AutoIncrement = true;
            dtweek.Columns["WKID"].AutoIncrementSeed = 1;
            dtweek.Columns.Add("WKName", typeof(string));
            int engagementid = Convert.ToInt32(Request.QueryString["engmtid"]);
            Int32 reccount = 0;
            string ps_scale = "A";
            DataTable dt = psd.GetPriceScales(engagementid, reccount, ps_scale);
            DataTable pdaytime = psd.GetScheduleDayTime(engagementid);
            int wkcount = 0;
            if (pdaytime.Rows.Count > 0)
            {
                wkcount = Convert.ToInt32(pdaytime.Rows[0]["WeekCount"]);
                for (int i = 0; i < wkcount; i++)
                {
                    dr = dtweek.NewRow();
                    dr["WKName"] = "Week " + (i + 1).ToString();
                    dtweek.Rows.Add(dr);
                }
            }
            FillTemplate(pdaytime, dt, dtweek);
        }
        public void FillTemplate(DataTable performance, DataTable mydt, DataTable week)
        {
            int engagementid = Convert.ToInt32(Request.QueryString["engmtid"]);
            DataTable dtsch;
            int arrvalue = 0;
            char[] chDlr = { '$', ',', ' ' };
            string weeklist = "", schdaylist = "";
            string[] pslist = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" };
            string[] ps = new string[performance.Rows.Count + 10];
            ContentPlaceHolder contentid = (ContentPlaceHolder)this.Master.FindControl("MainContent");
            DataTable dtdummy;
            string psname = "";
            int showcount = 0, Pricescale_tbl_count = 0;
            decimal total_gross = 0;
            DataTable dtps;
            for (int i = 0; i < 20; i++)
            {
                weeklist = ""; schdaylist = "";
                psname = pslist[i].ToString();
                dtps = new DataTable();
                if (mydt.Select("PS_Scale='" + psname + "'", "").Length > 0)
                    dtps = mydt.Select("PS_Scale='" + psname + "'", "").CopyToDataTable();
                else
                    dtps = mydt.Clone();
                Pricescale_tbl_count = dtps.Rows.Count;
                dtsch = new DataTable();
                dtsch = psd.GetPriceScalesSchedule(engagementid, psname);
                HtmlGenericControl div = (HtmlGenericControl)contentid.FindControl("div_ps" + (i + 1));
                Label lbl_ps1 = (Label)contentid.FindControl("lbl_ps" + (i + 1));
                UserControl uc_ps = (UserControl)contentid.FindControl("uc_ps" + (i + 1));
                CheckBoxList chk_ps1 = (CheckBoxList)contentid.FindControl("chk_ps" + (i + 1));
                CheckBoxList chklistweek = (CheckBoxList)contentid.FindControl("chklistweek" + (i + 1));
                GridView gdv = (GridView)uc_ps.FindControl("gdv_ps1");
                HiddenField hdnps_scale = (HiddenField)uc_ps.FindControl("hdnps_scale");
                Label lbl_ps1total = (Label)uc_ps.FindControl("lbl_ps1total");
                Label lbl_seattotal = (Label)uc_ps.FindControl("lbl_seattotal");
                Label lbl_ps1shows = (Label)uc_ps.FindControl("lbl_ps1shows");
                Label lbl_ps1subtotal = (Label)uc_ps.FindControl("lbl_ps1subtotal");
                hdnps_scale.Value = psname;
                lbl_ps1.Text = psname;
                //div.Visible = (i == 0 || Pricescale_tbl_count > 0) ? true : false;
                string dis = (i == 0 || Pricescale_tbl_count > 0) ? "block" : "none";
                div.Style.Add("display", dis);
                if (dtps.Rows.Count > 0)
                {
                    if (i == 0)
                    {
                        hdnintialseat_total.Value = dtps.Compute("sum(ps_seats_single)", null).ToString();
                    }
                    lbl_ps1total.Text = Convert.ToDecimal(dtps.Compute("sum(ps_sale_amount)", null)).ToString("C2", new CultureInfo("en-US"));
                    lbl_seattotal.Text = dtps.Compute("sum(ps_seats_single)", null).ToString();
                    lbl_ps1shows.Text = dtsch.Rows.Count.ToString();
                    showcount += Convert.ToInt32(lbl_ps1shows.Text);
                    lbl_ps1subtotal.Text = (Convert.ToInt16(lbl_ps1shows.Text) * Convert.ToDecimal(Regex.Replace(Convert.ToString(lbl_ps1total.Text.AutoformatDecimal()), @"\$|\,", ""))).ToString("C2", new CultureInfo("en-US"));
                    total_gross += Convert.ToDecimal(lbl_ps1subtotal.Text.AutoformatDecimal());
                    weeklist = dtps.Rows[0]["WeekList"].ToString();
                    schdaylist = dtps.Rows[0]["SCHEDULEDAYLIST"].ToString();
                    gdv.DataSource = dtps;
                    gdv.DataBind();
                }
                else
                {
                    lbl_ps1total.Text = "$0.00";
                    lbl_ps1shows.Text = "0";
                    lbl_ps1subtotal.Text = "0";
                    dtdummy = new DataTable();
                    dtdummy = dtps.Clone();
                    AddEmptyRow(gdv, dtdummy);
                }
                chk_ps1.DataSource = performance;
                chk_ps1.DataTextField = "pdaytime";
                chk_ps1.DataValueField = "schedule_id";
                chk_ps1.DataBind();
                chklistweek.DataSource = week;
                chklistweek.DataTextField = "WKName";
                chklistweek.DataValueField = "WkID";
                chklistweek.DataBind();
                for (int k = 0; k < dtsch.Rows.Count; k++)
                {
                    if (!string.IsNullOrEmpty(dtsch.Rows[k]["ps_schedule_weeks"].ToString()))
                    {
                        chklistweek.Items.FindByValue(dtsch.Rows[k]["ps_schedule_weeks"].ToString()).Selected = true;
                    }
                }
                for (int k = 0; k < dtsch.Rows.Count; k++)
                {
                    if (!string.IsNullOrEmpty(dtsch.Rows[k]["PS_SCHEDULE_DAYS"].ToString()))
                    {
                        chk_ps1.Items.FindByValue(dtsch.Rows[k]["PS_SCHEDULE_DAYS"].ToString()).Selected = true;
                        ps[arrvalue] = dtsch.Rows[k]["PS_SCHEDULE_DAYS"].ToString();
                        arrvalue++;
                    }
                }
            }
            lbl_psshows.Text = showcount.ToString();
            lbl_pstotal.Text = Convert.ToDecimal(total_gross).ToString("C2", new CultureInfo("en-US"));
            lbl_pssubtotal.Text = (Convert.ToDecimal(Regex.Replace(Convert.ToString(lbl_pstotal.Text.AutoformatDecimal()), @"\$|\,", "")) * (Convert.ToDecimal(lbl_pstax.Text) / 100)).ToString("C2", new CultureInfo("en-US"));
            lblgrossafttax.Text = Convert.ToDecimal(lbl_pstotal.Text.AutoformatDecimal() - lbl_pssubtotal.Text.AutoformatDecimal()).ToString("C2", new CultureInfo("en-US"));
            set_chkps_enablestatus(ps);
        }
        public void set_chkps_enablestatus(string[] pscale)
        {
            int k = Array.IndexOf(pscale, null);
            ContentPlaceHolder contentid = (ContentPlaceHolder)this.Master.FindControl("MainContent");
            for (int i = 0; i < 20; i++)
            {
                CheckBoxList chk_ps = (CheckBoxList)contentid.FindControl("chk_ps" + (i + 1));
                for (int j = 0; j < k; j++)
                {
                    //chk_ps.Items.FindByValue(schdaylist.Split(',').GetValue(i).ToString()).Selected 
                    if (chk_ps.Items.FindByValue(pscale[j].ToString()).Selected != true)
                    {
                        chk_ps.Items.FindByValue(pscale[j].ToString()).Enabled = false;
                    }

                }
            }
        }
        public void setGridTotal(int count, DataTable mydt)
        {
            string[] pslist = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" };
            for (int i = 0; i < count; i++)
            {
                ContentPlaceHolder contentid = (ContentPlaceHolder)this.Master.FindControl("MainContent");
                UserControl uc = (UserControl)contentid.FindControl("uc_ps" + (i + 1));
                Label lbl_ps1total = (Label)uc.FindControl("lbl_ps1total");
                Label lbl_ps1shows = (Label)uc.FindControl("lbl_ps1shows");
                Label lbl_ps1subtotal = (Label)uc.FindControl("lbl_ps1subtotal");
                if (mydt.Compute("sum(ps_sale_amount)", "Ps_Scale='" + pslist[i] + "'") != DBNull.Value)
                {
                    lbl_ps1total.Text = Convert.ToDecimal(mydt.Compute("sum(ps_sale_amount)", null)).ToString("C2", new CultureInfo("en-US"));
                    lbl_ps1shows.Text = mydt.DefaultView.ToTable(true, "scheduledaylist").Rows[0][0].ToString().Split(',').Length.ToString();
                    lbl_ps1subtotal.Text = (Convert.ToInt16(lbl_ps1shows.Text) * Convert.ToDecimal(Regex.Replace(lbl_ps1total.Text, @"\$|\,", ""))).ToString("C2", new CultureInfo("en-US"));
                }
                else
                {
                    lbl_ps1total.Text = "$0.00";
                }
            }
        }
        public void AddEmptyRow(GridView gv, DataTable dt1)
        {
            foreach (DataColumn cl in dt1.Columns)
            {
                cl.AllowDBNull = true;
            }
            dt1.Rows.Add(dt1.NewRow());
            gv.DataSource = dt1;
            gv.DataBind();
            int columncount = gv.Rows[0].Cells.Count;
            gv.Rows[0].Visible = false;
            //gv.Rows[0].Cells.Clear();
            //gv.Rows[0].Cells.Add(new TableCell());
            //gv.Rows[0].Cells[0].ColumnSpan = columncount;
            //gv.Rows[0].Cells[0].Text = "-----";

        }

        public void SavePriceScaleDetails(CheckBoxList chkweeks, CheckBoxList chkdays, GridView gvid, string pricescale_name, int delflg)
        {

            int engtid = Convert.ToInt32(hdn_engagementid.Value);
            for (int i = 0; i < chkweeks.Items.Count; i++)
            {
                if (chkweeks.Items[i].Selected == true)
                    for (int j = 0; j < chkdays.Items.Count; j++)
                    {
                        if (chkdays.Items[j].Selected == true)
                            foreach (GridViewRow row in gvid.Rows)
                            {
                                if (row.RowType == DataControlRowType.DataRow)
                                {
                                    if (!string.IsNullOrEmpty(row.Cells[1].Controls.OfType<TextBox>().FirstOrDefault().Text))
                                    {
                                        string a = row.Cells[10].Controls.OfType<Label>().FirstOrDefault().Text;

                                        int up = psd.UpdatePriceScales(engtid, Convert.ToInt32(0), chkdays.Items[j].Value, row.Cells[1].Controls.OfType<TextBox>().FirstOrDefault().Text,
                                            Convert.ToString(row.Cells[2].Controls.OfType<TextBox>().FirstOrDefault().Text.AutoformatDecimal()),
                                            Convert.ToString(row.Cells[3].Controls.OfType<Label>().FirstOrDefault().Text.AutoformatDecimal()),
                                            row.Cells[4].Controls.OfType<TextBox>().FirstOrDefault().Text.Trim(),
                                            row.Cells[5].Controls.OfType<DropDownList>().FirstOrDefault().Text,
                                            Convert.ToString(row.Cells[6].Controls.OfType<TextBox>().FirstOrDefault().Text.AutoformatDecimal()),
                                            Convert.ToString(row.Cells[7].Controls.OfType<Label>().FirstOrDefault().Text.AutoformatDecimal()),
                                            row.Cells[8].Controls.OfType<DropDownList>().FirstOrDefault().Text,
                                            Convert.ToString(row.Cells[9].Controls.OfType<TextBox>().FirstOrDefault().Text.AutoformatDecimal()),
                                            Convert.ToString(row.Cells[10].Controls.OfType<Label>().FirstOrDefault().Text.AutoformatDecimal()),
                                            chkweeks.Items[i].Value, pricescale_name,
                                            row.Cells[0].Controls.OfType<Label>().FirstOrDefault().Text, delflg);
                                        delflg = 1;
                                    }
                                }
                            }
                    }
            }
        }

        public bool check_seattotal()
        {
            ContentPlaceHolder contentid = (ContentPlaceHolder)this.Master.FindControl("MainContent");
            EngagementMaster EngmtMaster = (EngagementMaster)Page.Master;
            Int32 seattotal = 0, cur_seattotal = 0;
            if (hdn_engagementid.Value != "0" && hdn_engagementid.Value != "")
            {
                if (hdn_schedulecount.Value != "0" && hdn_schedulecount.Value != "")
                {
                    for (int i = 0; i < 20; i++)
                    {
                        HtmlGenericControl div = (HtmlGenericControl)contentid.FindControl("div_ps" + (i + 1));
                        if (div.Style["display"].ToLower() == "block")
                        {
                            UserControl uc = (UserControl)contentid.FindControl("uc_ps" + (i + 1));
                            Label lbl_seattotal = (Label)uc.FindControl("lbl_seattotal");
                            if (i == 0)
                            {
                                seattotal = (string.IsNullOrEmpty(lbl_seattotal.Text)) ? 0 : Convert.ToInt32(lbl_seattotal.Text);
                            }
                            cur_seattotal = (string.IsNullOrEmpty(lbl_seattotal.Text)) ? 0 : Convert.ToInt32(lbl_seattotal.Text);
                            if (seattotal != cur_seattotal)
                            {
                                lbl_msg.Text = "All Price scale seat total should be same!";
                                lbl_msg.ForeColor = System.Drawing.Color.Orange;
                                return false;
                            }

                        }
                    }
                }
            }
            return true;
        }

        public void SaveData()
        {
            Label lbl_seattotal=null;
            if (check_seattotal() == false)
            { return; }
            int del_flg = 0;
            string[] pslist = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" };
            ContentPlaceHolder contentid = (ContentPlaceHolder)this.Master.FindControl("MainContent");
            EngagementMaster EngmtMaster = (EngagementMaster)Page.Master;
            
            if (hdn_engagementid.Value != "0" && hdn_engagementid.Value != "")
            {
                if (hdn_schedulecount.Value != "0" && hdn_schedulecount.Value != "")
                {
                   
                    for (int i = 0; i < 20; i++)
                    {
                        string pricescale = "";
                        HtmlGenericControl div = (HtmlGenericControl)contentid.FindControl("div_ps" + (i + 1));
                        if (div.Style["display"].ToLower() == "block")
                        {
                            CheckBoxList chkps = (CheckBoxList)contentid.FindControl("chk_ps" + (i + 1));
                            CheckBoxList chklist = (CheckBoxList)contentid.FindControl("chklistweek" + (i + 1));
                            UserControl uc = (UserControl)contentid.FindControl("uc_ps" + (i + 1));
                            GridView gv = (GridView)uc.FindControl("gdv_ps1");
                            pricescale = pslist[i].ToString();
                            SavePriceScaleDetails(chklist, chkps, gv, pricescale, del_flg);
                            if (lbl_seattotal == null) 
                            lbl_seattotal = (Label)uc.FindControl("lbl_seattotal");
                        }
                    }
                    lbl_msg.Text = "Price Scales saved successfully";
                    lbl_msg.ForeColor = System.Drawing.Color.Green;
                    if (lbl_seattotal != null)
                    {
                        if (lbl_seattotal.Text.AutoformatInt() != hdnintialseat_total.Value.AutoformatInt())
                        {
                            psd.UpdateExpenseInsurance(Convert.ToInt32(hdn_engagementid.Value));
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "JQalertbox('Seats got changed, please check the insurance budget!');", true);
                        }
                    }
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



        public string GetCheckBoxDetails(string flag, CheckBoxList chkbox)
        {
            int selectedcount = 0;
            string selectedtext = "";
            CheckBoxList chkbox1 = new CheckBoxList();
            chkbox1 = chkbox;
            IEnumerable<string> CheckedItems = chkbox1.Items.Cast<ListItem>()
                                   .Where(i => i.Selected)
                                   .Select(i => i.Value);
            if (flag == "cnt")
            {
                foreach (string i in CheckedItems)
                {
                    //selectedcount += Convert.ToInt16(i);
                    selectedcount += 1;
                }
                return selectedcount.ToString();
            }
            else
            {
                foreach (string i in CheckedItems)
                {
                    selectedtext += i + "|";
                }
                return selectedtext;
            }
        }
        public void ShowCheckBoxValue(int indx, CheckBoxList chkbox)
        {
            CheckBoxList chkbox1 = new CheckBoxList();
            chkbox1 = chkbox;
            foreach (ListItem itm in chkbox1.Items)
            {
                if (itm.Value == indx.ToString())
                {
                    itm.Selected = true;
                }
            }
        }
        protected void chk_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBoxList chk = (CheckBoxList)sender;
            string id = chk.ToolTip;
            ContentPlaceHolder contentid = (ContentPlaceHolder)this.Master.FindControl("MainContent");
            UserControl uc_ps = (UserControl)contentid.FindControl("uc_ps" + id);

            Label lbl_ps1shows = (Label)uc_ps.FindControl("lbl_ps1shows");
            Label lbl_ps1subtotal = (Label)uc_ps.FindControl("lbl_ps1subtotal");
            Label lbl_ps1total = (Label)uc_ps.FindControl("lbl_ps1total");
            lbl_ps1shows.Text = GetCheckBoxDetails("cnt", chk);
            lbl_ps1subtotal.Text = (Convert.ToInt16(lbl_ps1shows.Text) * Convert.ToDecimal(Regex.Replace(lbl_ps1total.Text, @"\$|\,", ""))).ToString("C2", new CultureInfo("en-US"));
            ShowGrandTotal(chk);
        }
        public void ShowGrandTotal(CheckBoxList chklist)
        {
            int psshows = 0, selindex = -1;
            decimal pstotal = 0;
            ContentPlaceHolder contentid = (ContentPlaceHolder)this.Master.FindControl("MainContent");
            string[] control = Request.Form.Get("__EVENTTARGET").Split('$');
            int idx = control.Length - 1;
            string sel = chklist.Items[Int32.Parse(control[idx])].Value;
            int secid = Convert.ToInt32(chklist.ToolTip);
            selindex = chklist.Items.IndexOf(chklist.Items.FindByValue(sel));
            bool flg = chklist.Items[selindex].Selected;
            for (int i = 0; i < 20; i++)
            {
                HtmlGenericControl div = (HtmlGenericControl)contentid.FindControl("div_ps" + (i + 1));
                CheckBoxList chklistweek = (CheckBoxList)contentid.FindControl("chklistweek" + (i + 1));
                CheckBoxList chk_ps = (CheckBoxList)contentid.FindControl("chk_ps" + (i + 1));
                chk_ps.Items[selindex].Enabled = ((i + 1) != secid) ? !flg : true;
                chk_ps.Items[selindex].Selected = ((i + 1) != secid) ? false : flg;
                if (div.Style["display"].ToLower() == "block")
                {
                    UserControl uc_ps = (UserControl)contentid.FindControl("uc_ps" + (i + 1));
                    Label lbl_ps1shows = (Label)uc_ps.FindControl("lbl_ps1shows");
                    Label lbl_ps1subtotal = (Label)uc_ps.FindControl("lbl_ps1subtotal");
                    psshows += Convert.ToInt16(lbl_ps1shows.Text);
                    pstotal += Convert.ToDecimal(Regex.Replace(lbl_ps1subtotal.Text, @"\$|\,", ""));
                }
            }
            lbl_psshows.Text = psshows.ToString();
            lbl_pstotal.Text = pstotal.ToString("C2", new CultureInfo("en-US"));
            lbl_pssubtotal.Text = (Convert.ToDecimal(Regex.Replace(lbl_pstotal.Text, @"\$|\,", "")) * (Convert.ToDecimal(lbl_pstax.Text) / 100)).ToString("C2", new CultureInfo("en-US"));
        }

        protected void chk_ps1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBoxList list = (CheckBoxList)sender;
            string[] control = Request.Form.Get("__EVENTTARGET").Split('$');
            int idx = control.Length - 1;
            string sel = list.Items[Int32.Parse(control[idx])].Value;
        }

        protected void btnNewPriceScale_Click(object sender, EventArgs e)
        {
            ContentPlaceHolder contentid = (ContentPlaceHolder)this.Master.FindControl("MainContent");
            for (int i = 0; i < 20; i++)
            {
                HtmlGenericControl div = (HtmlGenericControl)contentid.FindControl("div_ps" + (i + 1));
                // if (div.Visible == false)
                if (div.Style.Value == "margin-top:20px;clear:both;display:none;")
                {
                    //div.Visible = true;
                    div.Style.Add("display", "block");
                    uc_ps2.Visible = true;
                    Addnewrow(i + 1);
                    break;
                }

            }
        }
        public void Addnewrow(int arg)
        {
            switch (arg)
            {
                case 1: { uc_ps1.AddNewRow(); break; }
                case 2: { uc_ps2.AddNewRow(); break; }
                case 3: { uc_ps3.AddNewRow(); break; }
                case 4: { uc_ps4.AddNewRow(); break; }
                case 5: { uc_ps5.AddNewRow(); break; }
                case 6: { uc_ps6.AddNewRow(); break; }
                case 7: { uc_ps7.AddNewRow(); break; }
                case 8: { uc_ps8.AddNewRow(); break; }
                case 9: { uc_ps9.AddNewRow(); break; }
                case 10: { uc_ps10.AddNewRow(); break; }
                case 11: { uc_ps11.AddNewRow(); break; }
                case 12: { uc_ps12.AddNewRow(); break; }
                case 13: { uc_ps13.AddNewRow(); break; }
                case 14: { uc_ps14.AddNewRow(); break; }
                case 15: { uc_ps15.AddNewRow(); break; }
                case 16: { uc_ps16.AddNewRow(); break; }
                case 17: { uc_ps17.AddNewRow(); break; }
                case 18: { uc_ps18.AddNewRow(); break; }
                case 19: { uc_ps19.AddNewRow(); break; }
                case 20: { uc_ps20.AddNewRow(); break; }


            }
        }
        protected void btnshowhidesec_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            ContentPlaceHolder contentid = (ContentPlaceHolder)this.Master.FindControl("MainContent");
            HtmlControl tab = (HtmlControl)contentid.FindControl(btn.CommandArgument);
            string flg;
            string txt = "";
            flg = (btn.Text == " + ") ? "block" : "none";
            txt = (btn.Text == " + ") ? " - " : " + ";
            tab.Style.Add("display", flg);
            btn.Text = txt;
        }
        public void Reset()
        {
            ContentPlaceHolder contentid = (ContentPlaceHolder)this.Master.FindControl("MainContent");
            for (int i = 1; 21 > i; i++)
            {
                UserControl usercontrol = (UserControl)contentid.FindControl("uc_ps" + i.ToString() + "");

                GridView gv = usercontrol.FindControl("gdv_ps1") as GridView;
                if (gv != null)
                {
                    foreach (GridViewRow row in gv.Rows)
                    {
                        TextBox txt_ps1_seats = row.FindControl("txt_ps1_seats") as TextBox;
                        TextBox txt_ps1_ticketprice = row.FindControl("txt_ps1_ticketprice") as TextBox;
                        TextBox txt_ps1_seatingdetail = row.FindControl("txt_ps1_seatingdetail") as TextBox;
                        TextBox txt_ps1_sdiscount = row.FindControl("txt_ps1_sdiscount") as TextBox;
                        TextBox txt_ps1_gdiscount = row.FindControl("txt_ps1_gdiscount") as TextBox;
                        txt_ps1_seats.Text = "";
                        txt_ps1_ticketprice.Text = "";
                        txt_ps1_seatingdetail.Text = "";
                        txt_ps1_sdiscount.Text = "";
                        txt_ps1_gdiscount.Text = "";

                    }
                }

            }

        }
    }
}