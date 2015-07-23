using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MasterDataLayer;
using PersonalDataLayer;
using MetroDataLayer;
using VenueDataLayer;
using PresenterDataLayer;
using CommonFunction;
using System.Globalization;
using System.Threading;
namespace NTOS
{
    public partial class Venue : System.Web.UI.Page, MasterPageSaveInterface, PresenterContactPerson.IMethod, Search1.searchdata
    {
        DataTable dt;
        CommonFun objcf = new CommonFun();
        bool from_metro = false;
        Label lbl_msg;
        MasterData objmst;
        protected void Page_Init(object sender, EventArgs e)
        {
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            divmsg.InnerHtml = string.Empty;
            lblerrmsg.Text = string.Empty;
            objmst = new MasterData();
            if (!IsPostBack)
            {
                hdnvenueid.Value = "0";
                hdnmetrocityid.Value = "0";
                fillmetrocity();
               
                lbl_msg = (Label)this.Master.FindControl("lbl_headersite");
                lbl_msg.Text = "Create Venue";
                (this.Master as Site1).SetfNewbutton("Venue");

                if (!string.IsNullOrEmpty(Request.QueryString["venueid"]))
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["search"]))
                    {
                        lbl_msg.Text = "";
                        string venueid = Request.QueryString["venueid"];
                        lbl_msg.Text = "";
                        DataTable dtengdetails = objmst.GetEVPDetails(Convert.ToInt32(venueid), 3);
                        DataTable dtvenuedetails = objmst.GetEVPDetails(Convert.ToInt32(venueid), 4);
                        if (dtengdetails != null)
                        {
                            if (dtengdetails.Rows.Count > 0)
                            {
                                gvrEngagement.DataSource = dtengdetails;
                                gvrEngagement.DataBind();
                                lblEnggrid.Visible = true;
                            }
                        }
                        if (dtvenuedetails != null)
                        {
                            if (dtvenuedetails.Rows.Count > 0)
                            {

                                grdVenue.DataSource = dtvenuedetails;
                                grdVenue.DataBind();
                                lblPresenter.Visible = true;
                            }
                        }
                        


                    }
                    else
                        lbl_msg.Text = "Modify Venue";
                   // lnknew.Visible = true;
                  //  lblhead.Text = "Modify Venue";
                    pnluc.Enabled = true;
                    pnluc.CssClass = "";
                    uccity.filldropdown();
                    hdnvenueid.Value = Request.QueryString["venueid"];
                    loadVenueDetails();

                }
                if (Page.PreviousPage != null)
                {
                    ContentPlaceHolder contentid = (ContentPlaceHolder)Page.PreviousPage.Form.FindControl("MainContent");
                    //txtmetrocity.Text = ((TextBox)contentid.FindControl("txtmetrocityname")).Text;
                    lblmetrostate.Text = ((DropDownList)contentid.FindControl("ddlstate")).SelectedItem.Text;
                    ddlmetrocity.SelectedIndex = ddlmetrocity.Items.IndexOf(ddlmetrocity.Items.FindByValue(((HiddenField)contentid.FindControl("hdnmetrocityid")).Value));
                    hdnmetrocityid.Value = ((HiddenField)contentid.FindControl("hdnmetrocityid")).Value;
                    hdnfrommetro.Value = "1";
                }
                if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                {
                    divmsg.InnerHtml = (Request.QueryString["type"].ToString() == "I") ? "Record submitted successfully!" : "Record updated Successfully!";
                }
                DropDownList ddcity = (DropDownList)uccity.FindControl("ddlcity");
                ((HiddenField)this.Master.FindControl("hdnreqlist")).Value = (txtvenuname.ClientID + "," + ddcity.ClientID);
            }
        }
        public void fillmetrocity()
        {
            MasterData objmst = new MasterData();
            dt = new DataTable();
           /// dt = objmst.GetMetroCityStates("Y");
            dt = objmst.GetCityStates();
            objcf.FillDropDownList(ddlmetrocity, dt, "CITY_NAME", "CITY_ID");
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getpersonalname(string prefixText, string contextKey)
        {
            PresenterData objpres = new PresenterData();
            List<string> personal = objpres.searchpersonal(contextKey, prefixText, "F", "Y");
            return personal;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getlastname(string prefixText, string contextKey)
        {
            PresenterData objpres = new PresenterData();
            List<string> personal = objpres.searchpersonal(contextKey, prefixText, "L", "Y");
            return personal;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getcityname(string prefixText)
        {

            MasterData edl = new MasterData();
            List<string> CountryNames = edl.searchcity(prefixText, "Y");
            return CountryNames;

        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetMetrocityname(string prefixText)
        {

            MasterData edl = new MasterData();
            List<string> CountryNames = edl.SearchMetrocity(prefixText);
            return CountryNames;

        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetVenueName(string prefixText)
        {
            VenueData objven = new VenueData();
            List<string> VenueNames = objven.SearchVenuename(prefixText, "Y");
            return VenueNames;
        }

        public void SaveData()
        {
            char[] chDlr = { '$', ',', ' ' };
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            TextBox txtaddress = (TextBox)uccity.FindControl("txtaddress");

            string venuename, venueaddress1, venueaddress2 = "", venuedellocation, venuenotes, zip;
            Nullable<Int32> cityid = null, metrocityid = null;
            Nullable<Int32> venuecapacity = null;
            cityid = uccity.InsertCitydetails();
            venuename = textInfo.ToTitleCase(txtvenuname.Text.Trim());
            venuedellocation = txtdeliveryDirection.Text.Trim();
            venuenotes = txtnotes.Text.Trim();
            //venuecapacity = (txtcapacity.Text != "") ? Convert.ToInt32(Convert.ToDecimal(txtcapacity.Text.Trim(chDlr))) : venuecapacity;
            venuecapacity = txtcapacity.Text.AutoformatInt();
            venueaddress1 = txtaddress.Text.Trim();
            metrocityid = (ddlmetrocity.SelectedIndex > 0) ? Convert.ToInt32(ddlmetrocity.SelectedItem.Value) : metrocityid;
            VenueData objven = new VenueData();
            Int32 VenueID = 0;
           // zip = (uccity.FindControl("txtzipcode") as TextBox).Text;
            zip = uccity.Get_child_zipcode();
            if (string.IsNullOrEmpty(Request.QueryString["venueid"]))
            {
                if (hdnvenueid.Value == "0")
                {
                    VenueID = objven.Venuedata_Insert(metrocityid, venuename, venueaddress1, venueaddress2, venuecapacity, venuedellocation, venuenotes, cityid, zip);
                    if (VenueID == 0)
                    {
                        lblerrmsg.Text = "Venue name already exist!";
                    }
                    else
                    {
                        hdnvenueid.Value = VenueID.ToString();
                        uccontactperson.SaveVenueContactPerson(VenueID);
                        ucdocx.SaveDocx(VenueID, "VENUE");
                        if (Convert.ToString(ViewState["status"]) == "")
                        {
                            if (hdnfrommetro.Value == "0")
                                Response.Redirect("~/venue.aspx?venueid=" + VenueID.ToString() + "&type=I");
                            else
                                Response.Redirect("~/metro.aspx?mertorcityid=" + ddlmetrocity.SelectedItem.Value);
                        }
                    }
                }
                else
                {
                    lblerrmsg.Text = "Venue name already exist!";
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["venueid"]))
            {
                VenueID = Convert.ToInt32(Request.QueryString["venueid"]);
                hdnvenueid.Value = VenueID.ToString();
                int existid = 0;
                existid = objven.Venuedata_Update(VenueID, metrocityid, venuename, venueaddress1, venueaddress2, venuecapacity, venuedellocation, venuenotes, cityid, zip);
                if (existid == 0)
                {
                    uccontactperson.SaveVenueContactPerson(VenueID);
                    ucdocx.SaveDocx(VenueID, "VENUE");
                    if (Convert.ToString(ViewState["status"]) == "")
                        Response.Redirect("~/venue.aspx?venueid=" + VenueID.ToString() + "&type=U");
                }
                else { lblerrmsg.Text = "Venue name already exist!"; }
            }
        }
        public void DeleteData(string flag)
        {
            VenueData objven = new VenueData();
            Int32 venueid = Convert.ToInt32(hdnvenueid.Value);
            string msg = objven.Venue_Delete(venueid, flag);
            if (msg == "")
            {
                divmsg.InnerHtml = (flag == "n") ? "Record deleted successfully!" : "Record undeleted successfully!";
                (this.Master as Site1).show_control(flag, pnlvenue);
            }
            else
            {
                lblerrmsg.Text = msg;
            }
        }

        protected void txtmetrocity_TextChanged(object sender, EventArgs e)
        {

        }
        public void loadVenueDetails()
        {
            try
            {
                Int32 venueid = Convert.ToInt32(hdnvenueid.Value);
                VenueData objven = new VenueData();
                DataTable dt = objven.GetVenueDetails(venueid);
                if (dt.Rows.Count > 0)
                {
                   
                       lblHeader.Visible = true;
                       lblHeader.Text= dt.Rows[0]["VENUE_NAME"].ToString();
                    
                    txtvenuname.Text = dt.Rows[0]["VENUE_NAME"].ToString();
                    txtcapacity.Text = dt.Rows[0]["VENUE_CAPACITY"].ToString();
                    txtdeliveryDirection.Text = dt.Rows[0]["VENUE_DELIVERY_DIRECTIONS"].ToString();
                    txtnotes.Text = dt.Rows[0]["VENUE_NOTES"].ToString();
                    hdnmetrocityid.Value = dt.Rows[0]["METROCITYID"].ToString();
                    ddlmetrocity.SelectedIndex = ddlmetrocity.Items.IndexOf(ddlmetrocity.Items.FindByValue(dt.Rows[0]["METROCITYID"].ToString()));
                    lblmetrostate.Text = dt.Rows[0]["METROSATENAME"].ToString();
                    string active_flag = dt.Rows[0]["venue_active_flag"].ToString();
                    (uccity.FindControl("txtzipcode") as TextBox).Text = dt.Rows[0]["zip"].ToString().Trim();
                    string beast_flag = dt.Rows[0]["venue_beast_flag"].ToString();
                    (this.Master as Site1).show_control(active_flag, pnlvenue, beast_flag);
                    string City_Zip = dt.Rows[0]["zip"].ToString();
                    uccity.fillcitydetails(Convert.ToInt32(dt.Rows[0]["VENUE_CITY_ID"]), dt.Rows[0]["VENUE_ADDRESS1"].ToString(), City_Zip);
                    if (!string.IsNullOrEmpty(Request.QueryString["venueid"]))
                    {
                        uccontactperson.LoadVenueContactpersondetails(venueid);
                        ucdocx.GetDocxDetails(venueid, "VENUE");
                    }
                }
            }
            catch (Exception ex)
            {
                lblerrmsg.Text = "Error: " + ex.Message.ToString();
                lblerrmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void txtvenuname_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["venueid"]))
            {
                uccontactperson.createtemptable();
                uccontactperson.bindcontactperson();
                ucdocx.createtemptable();
                ucdocx.binddocx();
            }
            if (hdnvenueid.Value != "0" && hdnvenueid.Value != "")
            {
                loadVenueDetails();
            }
            TextBox txtaddress = (TextBox)uccity.FindControl("txtaddress");
            txtaddress.Focus();
        }

        protected void ddlmetrocity_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnmetrocityid.Value = ddlmetrocity.SelectedItem.Value;
            if (ddlmetrocity.SelectedIndex > 0)
            {
                MasterData objmst = new MasterData();
                DataTable dt = objmst.Getcitydetails(Convert.ToInt32(hdnmetrocityid.Value));
                lblmetrostate.Text = dt.Rows[0]["State_Name"].ToString();
            }
            else
            { lblmetrostate.Text = string.Empty; }

        }
        public void Reset() { }
        public void SaveDataFromContactPerson(string id)
        {
            ViewState["status"] = "movepersonal";
            SaveData();
            if (divmsg.InnerHtml == "" && lblerrmsg.Text == "")
            {
                if (id == "0")
                    Response.Redirect("Personal.aspx?form=v_" + hdnvenueid.Value);
                else
                    Response.Redirect("Personal.aspx?personalid=" + id + "&form=v_" + hdnvenueid.Value);

            }

        }
        public DataTable getdata()
        {
            DataTable dt = new DataTable();
            objmst = new MasterData();
            Search1 obj = new Search1();
            obj.Code = txtvenuname.Text;
           // dt = objmst.GetSearchDataNew("Venue", txtvenuname.Text);
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

        protected void grdVenue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "130px");
            }
        }
    }
}