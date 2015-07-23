using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PersonalDataLayer;
using PresenterDataLayer;
using MasterDataLayer;
using System.Data;
using System.Globalization;
using System.Threading;
namespace NTOS
{
    public partial class Presenter : System.Web.UI.Page, MasterPageSaveInterface, PresenterContactPerson.IMethod, Search1.searchdata
    {
        Label lbl_msg;
        MasterData objmst;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetExpires(DateTime.Now.AddMinutes(-1));
            divmsg.InnerHtml = "";
            lblerrmsg.Text = "";
            objmst = new MasterData();
            if (!Page.IsPostBack)
            {

     


                lbl_msg = (Label)this.Master.FindControl("lbl_headersite");
                lbl_msg.Text = "Create Presenter";
                (this.Master as Site1).SetfNewbutton("Presenter");
                if (!string.IsNullOrEmpty(Request.QueryString["presenterid"]))
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["search"]))
                    {
                        string presenterid  = Request.QueryString["presenterid"];
                        lbl_msg.Text = "";
                        DataTable dtengdetails = objmst.GetEVPDetails(Convert.ToInt32(presenterid), 1);
                        DataTable dtvenuedetails = objmst.GetEVPDetails(Convert.ToInt32(presenterid), 2);
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
                                lblVenuegrid.Visible = true;
                            }
                        }
                        

                    }
                    else
                        lbl_msg.Text = "Modify Presenter";
                  //  lnknew.Visible = true;
                  //  lblhead.Text = "Modify Presenter";
                    hdnpresenterid.Value = Request.QueryString["presenterid"].ToString();
                    loadpresenterdetails();
                }
                if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                {
                    divmsg.InnerHtml = "Record submitted successfully!";
                }
                ((HiddenField)this.Master.FindControl("hdnreqlist")).Value = (txtpresentername.ClientID);

               



            }
        }
        #region page method
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
        public static List<string> Getphone(string prefixText, string contextKey)
        {
            PresenterData objpres = new PresenterData();
            List<string> personal = objpres.searchpersonal(contextKey, prefixText, "P", "Y");
            return personal;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getpresentername(string prefixText)
      {
            PresenterData objpres = new PresenterData();
            List<string> Presentername = objpres.SearchPresentername(prefixText, "Y");
            return Presentername;

        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetZipcode(string prefixText)
        {

            MasterData edl = new MasterData();
            List<string> Zipcodelist = edl.serachZipcode(prefixText);
            return Zipcodelist;

        }
        #endregion

        public void SaveData()
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            string presentername = "", presenterstreet = "", notes = "", zip;
            Nullable<Int32> cityid = null;
            presentername = textInfo.ToTitleCase(txtpresentername.Text.Trim());
            TextBox txtaddress = (TextBox)uccity.FindControl("txtaddress");
            presenterstreet = textInfo.ToTitleCase(txtaddress.Text.Trim());
            notes = txtnotes.Text.Trim();
            cityid = uccity.InsertCitydetails();
            PresenterData objpres = new PresenterData();
            Int32 presenterid = 0;
            //zip = (uccity.FindControl("txtzipcode") as TextBox).Text;
            zip = uccity.Get_child_zipcode();
            //string msg = "";
            if (string.IsNullOrEmpty(Request.QueryString["presenterid"]))
            {
                if (hdnpresenterid.Value == "0")
                {
                    presenterid = objpres.PresenterDetails_Insert(presentername, presenterstreet, cityid, notes, zip);
                    if (presenterid == 0)
                    {
                        lblerrmsg.Text = "Presenter already exists!";
                    }
                    else
                   {
                        hdnpresenterid.Value = presenterid.ToString();
                        uccontactperson.SavePresenterContactPerson(presenterid);
                        ucdocx.SaveDocx(presenterid, "PRESENTER");
                        if (Convert.ToString(ViewState["status"]) == "")
                            Response.Redirect("~/presenter.aspx?presenterid=" + presenterid.ToString() + "&type=I");
                    }
                }
                else
                {
                    lblerrmsg.Text = "Presenter already exists!";
                }
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["presenterid"]))
            {
                presenterid = Convert.ToInt32(Request.QueryString["presenterid"]);
                hdnpresenterid.Value = presenterid.ToString();
                int existid = 0;
                existid = objpres.PresenterDetails_Update(presenterid, presentername, presenterstreet, cityid, notes, zip);
                if (existid == 0)
                {
                    uccontactperson.SavePresenterContactPerson(presenterid);
                    ucdocx.SaveDocx(presenterid, "PRESENTER");
                    if (Convert.ToString(ViewState["status"]) == "")
                    divmsg.InnerHtml = "Records updated successfully!";
                    uccontactperson.showhidefooter(false);
                }
                else { lblerrmsg.Text = "Presenter already exist!"; }
            }
        }
        public void DeleteData(string flag)
        {
            PresenterData objpres = new PresenterData();
            Int32 presenterid = Convert.ToInt32(hdnpresenterid.Value);
            string msg = objpres.Presenter_Delete(presenterid, flag);
            if (msg == "")
            {
                divmsg.InnerHtml = (flag == "n") ? "Record deleted successfully!" : "Record undeleted successfully!";
                (this.Master as Site1).show_control(flag, pnlpresenter);
            }
            else
            {
                lblerrmsg.Text = msg;
            }
        }
        protected void txtpresentername_TextChanged(object sender, EventArgs e)
        {

            if (hdnpresenterid.Value != "0" || !string.IsNullOrEmpty(Request.QueryString["presenterid"]))
            {
                loadpresenterdetails();
            }
            else
            {
                uccontactperson.createtemptable();
                uccontactperson.bindcontactperson();
                ucdocx.createtemptable();
                ucdocx.binddocx();
            }
            TextBox txtaddress = (TextBox)uccity.FindControl("txtaddress");
            txtaddress.Focus();
        }
        public void loadpresenterdetails()
        {
            try
            {
                Int32 presenterid = Convert.ToInt32(hdnpresenterid.Value);
                PresenterData objpres = new PresenterData();
                DataTable dt = objpres.GetPresenterDetails(presenterid);
                if (dt.Rows.Count > 0)
                {
                  
                       lblHeader.Visible = true;
                       lblHeader.Text= dt.Rows[0]["PRESENTER_NAME"].ToString();
                    
                    txtpresentername.Text = dt.Rows[0]["PRESENTER_NAME"].ToString();
                    txtnotes.Text = dt.Rows[0]["PRESENTER_NOTES"].ToString();
                    (uccity.FindControl("txtzipcode") as TextBox).Text = dt.Rows[0]["zip"].ToString().Trim();
                    string City_Zip = dt.Rows[0]["zip"].ToString();
                    uccity.fillcitydetails(Convert.ToInt32(dt.Rows[0]["PRESENTER_CITY_ID"]), dt.Rows[0]["PRESENTER_STREET"].ToString(), City_Zip);
                    string active_flag = dt.Rows[0]["PRESENTER_ACTIVE_FLAG"].ToString();
                    string beast_flag = dt.Rows[0]["presenter_beast_flag"].ToString();
                    (this.Master as Site1).show_control(active_flag, pnlpresenter, beast_flag);
                    if (!string.IsNullOrEmpty(Request.QueryString["presenterid"]))
                    {
                        ucdocx.GetDocxDetails(presenterid, "PRESENTER");
                        uccontactperson.LoadContactpersondetails(presenterid);
                        uccontactperson.showhidefooter(false);
                    }
                }
            }
            catch (Exception ex)
            {
                lblerrmsg.Text = "Error: " + ex.Message.ToString();
                lblerrmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        public void Reset() { }
        public void SaveDataFromContactPerson(string id)
        {
            ViewState["status"] = "movepersonal";
            SaveData();
            if (divmsg.InnerHtml == "")
            {
                if (id == "0")
                    Response.Redirect("Personal.aspx?form=p_" + hdnpresenterid.Value);
                else
                    Response.Redirect("Personal.aspx?personalid=" + id + "&form=p_" + hdnpresenterid.Value);
            }
        }
        public DataTable getdata()
        {
            DataTable dt = new DataTable();
            objmst = new MasterData();
     
           // dt = objmst.GetSearchDataNew("Presenter", txtpresentername.Text);
            
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