using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShowDataLayer;
using System.Configuration;
using EngagementDataLayer;
using MasterDataLayer;
using System.Data;
using System.Globalization;
using System.Threading;
namespace NTOS
{



  
    public partial class Show : System.Web.UI.Page, MasterPageSaveInterface, Search1.searchdata
    {
        ShowData objShowData;
        MasterData objmst;
        DataTable dt;
        CommonFunction.CommonFun objcf  = new CommonFunction.CommonFun();
        Label lbl_msg;
        string showid;


        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }  


        protected void Page_Load(object sender, EventArgs e)
        {
            divmsg.InnerHtml = "";
            lblerrmsg.Text = "";
            if (!Page.IsPostBack)
            {
                showid = Request.QueryString["showid"];
                hdnshowid.Value = "0";
                hdncityid.Value = "0";
                txtshowbegindate.Text = System.DateTime.Now.ToString("MM/dd/yyyy");
                LoadCompanyMgr();
                lbl_msg = (Label)this.Master.FindControl("lbl_headersite");
                lbl_msg.Text = "Create Show";
                (this.Master as Site1).SetfNewbutton("Show");
               
                   
                if (!string.IsNullOrEmpty(Request.QueryString["showid"]))
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["search"]))
                    {

                       
                        lbl_msg.Text = "";
                        DataTable dtengdetails =  BindGridView();
                        if (dtengdetails != null)
                        {
                            if (dtengdetails.Rows.Count > 0)
                            {
                                gvrEngagement.DataSource = dtengdetails;
                                gvrEngagement.DataBind();
                                lblEnggrid.Visible = true;
                            }
                        }

                             
                        
                        }
                    else
                        lbl_msg.Text = "Modify Show";
                    hdnshowid.Value = Request.QueryString["showid"].ToString();
                    loadshowdetails();
                    LoadPrefShow_Details();
                   // lblhead.Text = "Modify Show";
                }
                else
                {
                    LoadPreferenceShows();
                }
                if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                {
                    divmsg.InnerHtml = "Record submitted successfully!";
                }
                ClearPrefShow();
                ((HiddenField)this.Master.FindControl("hdnreqlist")).Value = (txtshowname.ClientID + "," + txtshowbegindate.ClientID + "," + txtname.ClientID);
            }
        }

        private DataTable BindGridView()
        {
            DataTable dtengdetails = objmst.GetEVPDetails(Convert.ToInt32(showid), 5);
            return dtengdetails;
           
        }
        public void LoadCompanyMgr()
        {
            objmst = new MasterData();
            dt = objmst.GetUserList("compmanager");
            objcf.FillDropDownList(ddlcompanymgr, dt, "users_name", "users_id");
            ddlcompanymgr.SelectedIndex = ddlcompanymgr.Items.IndexOf(ddlcompanymgr.Items.FindByValue(Convert.ToString(Session["userid"].ToString().ToLower())));
            
        }
        public void SaveData()
        {
            if (validate_prefshow() == true)
            {
                char[] chDlr = { '$', ',', ' ' };
                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;
                TextBox txtaddress = (TextBox)uccity.FindControl("txtaddress");
                string showname, showfedid, showcorpname, showcorpstreet = "", royalties, zipcodecorp;
                Nullable<Int32> cityid = null,companymgr_id=null;
                cityid = uccity.InsertCitydetails();
                Nullable<Decimal> overheadnut = null, wklyopexp = null;
                DateTime showbegindate;
                showname = textInfo.ToTitleCase(txtshowname.Text.Trim());
                showfedid = Convert.ToString(txtfederalid.Text.Trim());
                showcorpname = textInfo.ToTitleCase(txtname.Text.Trim());
                wklyopexp = (txtwklyopexp.Text.Trim() == "") ? wklyopexp : Convert.ToDecimal(txtwklyopexp.Text.Trim(chDlr));
                overheadnut = (txtoverheadnut.Text.Trim() == "") ? overheadnut : Convert.ToDecimal(txtoverheadnut.Text.Trim(chDlr));
                royalties = Convert.ToString(txtvariableroyalities.Text.Trim());
                showbegindate = Convert.ToDateTime(txtshowbegindate.Text);
                showcorpstreet = textInfo.ToTitleCase(txtaddress.Text.Trim());
                //zipcodecorp = (uccity.FindControl("txtzipcode") as TextBox).Text;
                zipcodecorp = uccity.Get_child_zipcode();
                companymgr_id = (ddlcompanymgr.SelectedIndex > 0) ? Convert.ToInt32(ddlcompanymgr.SelectedItem.Value) : companymgr_id;
                objShowData = new ShowData();
                if (string.IsNullOrEmpty(Request.QueryString["showid"]))
                {
                    if (hdnshowid.Value != "0")
                    {
                        lblerrmsg.Text = "Show name already exists!";
                    }
                    else
                    {
                        Int32 newshowid;
                        newshowid = objShowData.Showdata_Insert(showname, showfedid, showcorpname, showcorpstreet, overheadnut, cityid, wklyopexp, royalties, showbegindate, zipcodecorp, companymgr_id);
                        if (newshowid == 0)
                        {
                            lblerrmsg.Text = "Show name already exist!";
                        }
                        else
                        {
                            
                            SavePrefShowDetails(newshowid);
                            ucdocx.SaveDocx(newshowid, "SHOW");
                            InsertPrefShowFooter(newshowid);
                            Response.Redirect("~/show.aspx?showid=" + newshowid.ToString() + "&type=I");
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(Request.QueryString["showid"]))
                {
                    hdnshowid.Value = Request.QueryString["showid"].ToString();
                    int existid = 0;
                    existid = objShowData.Showdata_Update(hdnshowid.Value.ToString(), showname, showfedid, showcorpname, showcorpstreet, overheadnut, cityid, wklyopexp, royalties, showbegindate, zipcodecorp, companymgr_id);
                    SavePrefShowDetails(Convert.ToInt32(hdnshowid.Value));
                    LoadPrefShow_Details();
                    if (existid == 0)
                    {
                        Int32 sid = Convert.ToInt32(hdnshowid.Value);
                        divmsg.InnerHtml = "Record updated successfully!";
                        ucdocx.SaveDocx(sid, "SHOW");
                    }
                    else { lblerrmsg.Text = "Show name already exist!"; }
                }
            }
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getshownames(string prefixText)
        {
            ShowData edl = new ShowData();
            List<string> shows = edl.searchdata(prefixText, "0");
            return shows;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getcityname(string prefixText)
        {

            MasterData edl = new MasterData();
            List<string> CountryNames = edl.searchcity(prefixText, "Y");
            return CountryNames;

        }
        protected void txtshowname_TextChanged(object sender, EventArgs e)
        {
            if (hdnshowid.Value != "0" && hdnshowid.Value != "")
            {
                loadshowdetails();
            }
            txtfederalid.Focus();

        }
        public void loadshowdetails()
        {
            try
            {
                DataTable dt = new DataTable();
                ShowData objshow = new ShowData();
                dt = objshow.Getshowdetails(hdnshowid.Value);
                if (dt.Rows.Count > 0)
                {
                    
                        lblHeader.Visible = true;
                        lblHeader.Text = dt.Rows[0]["show_name"].ToString();
                    
                    txtshowname.Text = dt.Rows[0]["show_name"].ToString();
                    txtfederalid.Text = dt.Rows[0]["show_fed_id"].ToString();
                    if (dt.Rows[0]["show_wkly_operating_expense"] != DBNull.Value)
                        txtwklyopexp.Text = Convert.ToDecimal(dt.Rows[0]["show_wkly_operating_expense"]).ToString("N2", new CultureInfo("en-US"));
                    if (dt.Rows[0]["show_overhead_nut"] != DBNull.Value)
                        txtoverheadnut.Text = Convert.ToDecimal(dt.Rows[0]["show_overhead_nut"]).ToString("N2", new CultureInfo("en-US"));
                    txtname.Text = dt.Rows[0]["show_corp_name"].ToString();
                    hdncityid.Value = dt.Rows[0]["CITY_ID"].ToString();
                    txtvariableroyalities.Text = dt.Rows[0]["show_var_rolyalties"].ToString();
                    txtshowbegindate.Text = dt.Rows[0]["show_begin_dt"].ToString();
                    (uccity.FindControl("txtzipcode") as TextBox).Text = dt.Rows[0]["zip"].ToString().Trim();
                    string active_flag = dt.Rows[0]["SHOW_ACTIVE_FLAG"].ToString();
                    string show_beast_flag = dt.Rows[0]["show_beast_flag"].ToString();
                    string City_Zip = dt.Rows[0]["zip"].ToString();
                    (this.Master as Site1).show_control(active_flag, pnlshow, show_beast_flag);
                    uccity.fillcitydetails(Convert.ToInt32(dt.Rows[0]["CITY_ID"]), dt.Rows[0]["show_corp_street"].ToString(), City_Zip);
                    hdnshowname.Value = dt.Rows[0]["show_name"].ToString();
                    ddlcompanymgr.SelectedIndex = ddlcompanymgr.Items.IndexOf(ddlcompanymgr.Items.FindByValue(Convert.ToString(dt.Rows[0]["COMPANYMGR_ID"].ToString().ToLower())));
                    if (!string.IsNullOrEmpty(Request.QueryString["showid"]))
                    {
                        Int32 showid = Convert.ToInt32(hdnshowid.Value);
                        ucdocx.GetDocxDetails(showid, "SHOW");
                       
                    }
                   
                }
            }
            catch (Exception ex)
            {
                lblerrmsg.Text = "Error: " + ex.Message.ToString();
                lblerrmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        public void DeleteData(string flag)
        {
            ShowData objshow = new ShowData();
            Int32 showid = Convert.ToInt32(hdnshowid.Value);
            string msg = objshow.Show_Delete(showid, flag);
            if (msg == "")
            {
                divmsg.InnerHtml = (flag == "n") ? "Record deleted successfully!" : "Record undeleted successfully!";
                (this.Master as Site1).show_control(flag, pnlshow);
            }
            else
            {
                lblerrmsg.Text = msg;
            }


        }
        public void Reset()
        { }

        #region Preference Shows

        public void LoadPreferenceShows()
        {
            objmst = new MasterData();
            dt = new DataTable();
            dt = objmst.Getshows("0");
            DataColumn dc = new DataColumn("Show_pro_preference", typeof(string));
            dc.DefaultValue = "";
            dt.Columns.Add(dc);
            repbind.DataSource = dt;
            repbind.DataBind();
        }
        public void LoadPrefShow_Details()
        {
            if (hdnshowid.Value != "0")
            {
                dt = new DataTable();
                objShowData = new ShowData();
                dt = objShowData.Get_Prefshowdetails(hdnshowid.Value);
                repbind.DataSource = dt;
                repbind.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "CheckAll();", true);
            }
            ClearPrefShow();
        }
        public void ClearPrefShow()
        {
            //txtpreforder.Text = (repprefshow.Items.Count + 1).ToString();
            //ddlprefshow.ClearSelection();
        }
        public void InsertPrefShowFooter(Int32 cshowid)
        {
            //if (ddlprefshow.SelectedIndex > 0 && string.IsNullOrEmpty(txtpreforder.Text) == false)
            //{
            //    objShowData.PreferenceShowDML(0, cshowid, Convert.ToInt32(ddlprefshow.SelectedItem.Value), Convert.ToInt32(txtpreforder.Text), "I", "");
            //    hdnshow_orderidlist.Value += "," + txtpreforder.Text;
            //}
        }


        #endregion
        public bool validate_prefshow()
        {
            string list = "";
            foreach (RepeaterItem ri in repbind.Items)
            {
                CheckBox chk = (CheckBox)ri.FindControl("chk");
                TextBox txtid = (TextBox)ri.FindControl("txtid");
                if (chk.Checked == true)
                {
                    txtid.Style.Add("display", "block");
                    if (string.IsNullOrEmpty(txtid.Text))
                    {
                        lblerrmsg.Text = "Enter show preference no.!";
                        // txtid.Focus();
                        return false;
                    }
                    if (list.IndexOf(txtid.Text) > 0)
                    {
                        lblerrmsg.Text = "Preference show id already exists!";
                        txtid.Focus();
                        return false;
                    }
                    list += txtid.Text + ",";
                }
            }
            return true;
        }
        public void SavePrefShowDetails(int _showid)
        {
            int k = 0;
            foreach (RepeaterItem li in repbind.Items)
            {
                HiddenField hdnprefshowid = (HiddenField)li.FindControl("hdnrepshowid");
                CheckBox chk_dcc_delete = (CheckBox)li.FindControl("chk");
                TextBox txtid = (TextBox)li.FindControl("txtid");

                if (chk_dcc_delete.Checked == true && !string.IsNullOrEmpty(txtid.Text))
                {
                    objShowData.PreferenceShowDML(k, _showid, Convert.ToInt32(hdnprefshowid.Value), Convert.ToInt32(txtid.Text), "I", "");
                    k = 1;
                }
            }
        }

        protected void repbind_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex == 0)
            {
                txtshowlist.Text = string.Empty;
            }
            Label lblrepshowname = (Label)e.Item.FindControl("lblrepshowname");
            TextBox txtid = (TextBox)e.Item.FindControl("txtid");
            CheckBox chk = (CheckBox)e.Item.FindControl("chk");
            chk.Checked = (string.IsNullOrEmpty(txtid.Text) ? false : true);
            string dis = (string.IsNullOrEmpty(txtid.Text) ? "none" : "block");
            if (chk.Checked)
            {
                txtshowlist.Text = (txtshowlist.Text + "," + lblrepshowname.Text).TrimStart(',');
            }
            //txtid.Style.Add("display", dis);

        }
        public DataTable getdata()
        {
            DataTable dt = new DataTable();
            objmst = new MasterData();
           
            //dt = objmst.GetSearchDataNew("Show",txtshowname.Text);
            return dt;
        }

        protected void gvrEngagement_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "130px");
            }
        }




        protected void gvrEngagement_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (direction == SortDirection.Ascending)
            {
                direction = SortDirection.Descending;
                sortingDirection = "Desc";

            }
            else
            {
                direction = SortDirection.Ascending;
                sortingDirection = "Asc";

            }
            DataView sortedView = new DataView(BindGridView());
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;
            gvrEngagement.DataSource = sortedView;
            gvrEngagement.DataBind();
        }  

    }
}