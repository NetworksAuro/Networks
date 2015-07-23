using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MasterDataLayer;
using PersonalDataLayer;
using System.Globalization;
using System.Threading;
using CommonFunction;
namespace NTOS
{
    public partial class Personal : System.Web.UI.Page, MasterPageSaveInterface, Search1.searchdata
    {
        Label lbl_msg;
        CommonFun objcf = new CommonFun();
        MasterData objmst;
        PresenterDataLayer.PresenterData objpre = new PresenterDataLayer.PresenterData();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblerrmsg.Text = "";
            divmsg.InnerHtml = "";
            Response.Cache.SetExpires(DateTime.Now.AddMinutes(-1));
            if (!Page.IsPostBack)
            {
                objmst = new MasterData();
                lbl_msg = (Label)this.Master.FindControl("lbl_headersite");
                lbl_msg.Text = "Create Contact";
                (this.Master as Site1).SetfNewbutton("Contact");
                filldropdown();
                if (!string.IsNullOrEmpty(Request.QueryString["search"]))
                {
                    lbl_msg.Text = "";
                    string personalid = Request.QueryString["personalid"];
                    lbl_msg.Text = "";
                    DataTable dtengdetails = objmst.GetEVPDetails(Convert.ToInt32(personalid), 6);
                   
                    if (dtengdetails != null)
                    {
                        if (dtengdetails.Rows.Count > 0)
                        {
                            gvrEngagement.DataSource = dtengdetails;
                            gvrEngagement.DataBind();
                        }
                    }
                }


                string scr = "javascript:return comparedate('" + txtdateifhire.ClientID + "','" + txttermanitationdate.ClientID + "','Hire date should be less than termination date!');";
                txtdateifhire.Attributes.Add("onblur", scr);
                txttermanitationdate.Attributes.Add("onblur", scr);
                txtdob.Attributes.Add("onchange", "return checkdatemax('" + txtdob.ClientID + "','date of birth should be less than current date!');");
                if (Request.QueryString["personalid"] != "" && Request.QueryString["personalid"] != null)
                {
                    lbl_msg.Text = "Modify Contact";
                    //lnknew.Visible = true;
                    //lblhead.Text = "Modify Personnel";
                    hdnpersonalid.Value = Request.QueryString["personalid"];
                    loadpersonaldetails();
                }
                if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                {
                    divmsg.InnerHtml = (Request.QueryString["type"].ToString() == "I") ? "Record submitted successfully!" : "Record updated Successfully!";
                }
                ((HiddenField)this.Master.FindControl("hdnreqlist")).Value = (txtfirstname.ClientID + "," + txtcompany.ClientID + "," + txtlastname.ClientID);
            }
        }
        public void Reset()
        {
        }
        public void filldropdown()
        {
            filltitle();
            fillemployeetype();
        }
        protected void fillemployeetype()
        {
            MasterData objmsd = new MasterData();
            DataTable dt = new DataTable();
            dt = objmsd.GetLookupList("employeetype");
            objcf.Fillddl_noselect(ddlemployeetype, dt, "lkup_desc", "lkup_id");
            dt = objmsd.GetLookupList("employeestatus");
            objcf.Fillddl_noselect(ddlemployeestatus, dt, "lkup_desc", "lkup_id");
            string def_status = "";
            if (dt.Select("lkup_desc='active'").Length > 0)
            {
                def_status = dt.Select("lkup_desc='active'").CopyToDataTable().Rows[0]["lkup_desc"].ToString();
            }
            ddlemployeestatus.SelectedIndex = ddlemployeestatus.Items.IndexOf(ddlemployeestatus.Items.FindByText(def_status));

        }
        protected void filltitle()
        {
            MasterData objmsd = new MasterData();
            DataTable dt = new DataTable();
            dt = objmsd.Gettitle();
            ddltitle.DataSource = dt;
            ddltitle.DataTextField = "title_name";
            ddltitle.DataValueField = "title_id";
            ddltitle.DataBind();
            ListItem li = new ListItem("-Select-", "0");
            ddltitle.Items.Insert(0, li);

        }
        public void SaveData()
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            PersonalData objper = new PersonalData();
            string fname, mname, lname, company, employeetype, materialstatus, empstatus, phoneland, phoncell, phoneother, fax, email, webpage, facebook, twitter;
            string residstaddress, otherstaddress, homezip, otherzip;
            Nullable<Int32> residcityid = null, othercityid = null;
            Nullable<Int16> titleid = null;
            Nullable<DateTime> dateifhire = null, terminationdate = null, dob = null;
            othercityid = ucothercity.InsertCitydetails();
            residcityid = ucresidcity.InsertCitydetails();
            fname = textInfo.ToTitleCase(txtfirstname.Text.Trim());
            mname = textInfo.ToTitleCase(txtmiddlename.Text.Trim());
            lname = textInfo.ToTitleCase(txtlastname.Text.Trim());
            company = textInfo.ToTitleCase(txtcompany.Text.Trim());
            employeetype = ddlemployeetype.SelectedItem.Text.Trim();
            materialstatus = txtmaterialstatus.Text.Trim();
            empstatus = ddlemployeestatus.SelectedItem.Text;// txtemployeestatus.Text.Trim();
            phoneland = txtphoneland.Text.Trim();
            phoncell = txtphonecell.Text.Trim();
            phoneother = txtphoneother.Text.Trim();
            fax = txtfax.Text.Trim();
            email = txtemail.Text.Trim();
            webpage = txtwebpage.Text.Trim();
            facebook = txtfacebook.Text.Trim();
            twitter = txttwitter.Text.Trim();


            titleid = (ddltitle.SelectedIndex == 0) ? titleid : Convert.ToInt16(ddltitle.SelectedItem.Value);

            dateifhire = (txtdateifhire.Text.Trim() == "") ? dateifhire : Convert.ToDateTime(txtdateifhire.Text);
            terminationdate = (txttermanitationdate.Text.Trim() == "") ? terminationdate : Convert.ToDateTime(txttermanitationdate.Text);
            dob = (txtdob.Text.Trim() == "") ? dob : Convert.ToDateTime(txtdob.Text);
            TextBox txtresidaddress = (TextBox)ucresidcity.FindControl("txtaddress");
            TextBox txtotheraddress = (TextBox)ucothercity.FindControl("txtaddress");
            residstaddress = textInfo.ToTitleCase(txtresidaddress.Text.Trim());
            otherstaddress = textInfo.ToTitleCase(txtotheraddress.Text.Trim());
            //homezip = (ucresidcity.FindControl("txtzipcode") as TextBox).Text;
            //otherzip = (ucothercity.FindControl("txtzipcode") as TextBox).Text;
            homezip = ucresidcity.Get_child_zipcode();
            otherzip = ucothercity.Get_child_zipcode();
            
            Int32 personalid = 0;
            //string msg = "", 
            string type = "I";
            if (string.IsNullOrEmpty(Request.QueryString["personalid"]))
            {
                personalid = objper.Personaldata_Insert(fname, mname, lname, company, employeetype, materialstatus, empstatus, phoneland, phoncell, phoneother, fax, email, webpage, facebook, twitter, residstaddress, residcityid, otherstaddress, othercityid, titleid, dateifhire, terminationdate, dob, homezip, otherzip);
            }
            else if (Request.QueryString["personalid"] != "" && Request.QueryString["personalid"] != null)
            {
                personalid = Convert.ToInt32(Request.QueryString["personalid"]);
                objper.Personaldata_Update(personalid, fname, mname, lname, company, employeetype, materialstatus, empstatus, phoneland, phoncell, phoneother, fax, email, webpage, facebook, twitter, residstaddress, residcityid, otherstaddress, othercityid, titleid, dateifhire, terminationdate, dob, homezip, otherzip);
                type = "U";
            }
            if (personalid < 0)
            {
                lblerrmsg.Text = "Error: Data did not submit. Please contact system administrator!";
                lblerrmsg.ForeColor = System.Drawing.Color.Red;
            }
            else if (personalid > 0)
            {
                ucshow.SavePersonalShow(personalid);
                ucdocx.SaveDocx(personalid, "PERSONAL");
                ucshow.showhidefooter(false);
                if (string.IsNullOrEmpty(Request.QueryString["form"]) == true)
                {
                    Response.Redirect("~/personal.aspx?personalid=" + personalid.ToString() + "&type=" + type + "");
                }
                else
                {
                    string prev_form = Request.QueryString["form"];
                    Int32 pre_or_ven_id = Convert.ToInt32(prev_form.Split('_').GetValue(1));
                    string pre_or_ven_flg = Convert.ToString(prev_form.Split('_').GetValue(0));
                    if (pre_or_ven_flg == "p")
                    {
                        objpre.PresenterContactperson_Insert(pre_or_ven_id, personalid);
                        Response.Redirect("~/presenter.aspx?presenterid=" + pre_or_ven_id.ToString() + "&type=I");
                    }
                    if (pre_or_ven_flg == "v")
                    {
                        VenueDataLayer.VenueData objven = new VenueDataLayer.VenueData();
                        objven.VenueContactperson_Insert(pre_or_ven_id, personalid);
                        Response.Redirect("~/venue.aspx?venueid=" + pre_or_ven_id.ToString() + "&type=I");
                    }

                }
            }
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getpersonalname(string prefixText)
        {
            PersonalData objper = new PersonalData();
            List<string> personal = objper.searchpersonal(prefixText, "Y");
            return personal;
        }
        protected void txtfirstname_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["personalid"]))
            {
                ucdocx.createtemptable();
                ucshow.createtemptable();
                ucshow.createtemptable();
                ucshow.BindShow();
                ucdocx.createtemptable();
                ucdocx.binddocx();
            }
            if (hdnpersonalid.Value != "0" && hdnpersonalid.Value != "")
            {
                loadpersonaldetails();
            }
            txtmiddlename.Focus();

        }
        public void loadpersonaldetails()
        {
            try
            {
                DataTable dt = new DataTable();
                PersonalData objper = new PersonalData();
                dt = objper.GetPersonalDetails(hdnpersonalid.Value);
                if (dt.Rows.Count > 0)
                {
                    if (Request.QueryString["personalid"] != "" && Request.QueryString["personalid"] != null)
                    {
                        txtfirstname.Text = dt.Rows[0]["PERSONAL_FNAME"].ToString();
                    }
                    lblHeader.Visible = true;
                    lblHeader1.Visible = true;
                    lblHeader.Text = dt.Rows[0]["PERSONAL_FNAME"].ToString() +" " + dt.Rows[0]["PERSONAL_LNAME"].ToString();
                    lblHeader1.Text = dt.Rows[0]["PERSONAL_TITLE_ID"].ToString();
                    txtfirstname.Text = dt.Rows[0]["PERSONAL_FNAME"].ToString();
                    txtlastname.Text = dt.Rows[0]["PERSONAL_LNAME"].ToString();
                    txtmiddlename.Text = dt.Rows[0]["PERSONAL_MNAME"].ToString();
                    txtphoneland.Text = dt.Rows[0]["PERSONAL_PHONE_LAND"].ToString();
                    txtphonecell.Text = dt.Rows[0]["PERSONAL_PHONE_CELL"].ToString();
                    txtphoneother.Text = dt.Rows[0]["PERSONAL_PHONE_OTHER"].ToString();
                    ddltitle.SelectedIndex = ddltitle.Items.IndexOf(ddltitle.Items.FindByValue(dt.Rows[0]["PERSONAL_TITLE_ID"].ToString()));
                    txtfax.Text = dt.Rows[0]["PERSONAL_FAX"].ToString();
                    txtemail.Text = dt.Rows[0]["PERSONAL_EMAIL"].ToString();
                    txtcompany.Text = dt.Rows[0]["PERSONAL_COMPANY"].ToString();
                    ddlemployeetype.SelectedIndex = ddlemployeetype.Items.IndexOf(ddlemployeetype.Items.FindByText(dt.Rows[0]["PERSONAL_EMP_TYPE"].ToString()));
                    txtdateifhire.Text = dt.Rows[0]["PERSONAL_HIRE_DATE"].ToString();
                    txttermanitationdate.Text = dt.Rows[0]["PERSONAL_TERM_DATE"].ToString();
                    ddlemployeestatus.SelectedIndex = ddlemployeestatus.Items.IndexOf(ddlemployeestatus.Items.FindByText(dt.Rows[0]["PERSONAL_EMP_STATUS"].ToString()));
                    txtdob.Text = dt.Rows[0]["PERSONAL_DOB"].ToString();
                    txtmaterialstatus.Text = dt.Rows[0]["PERSONAL_MARRIED_STATUS"].ToString();
                    txtwebpage.Text = dt.Rows[0]["PERSONAL_WEBPAGE"].ToString();
                    txtfacebook.Text = dt.Rows[0]["PERSONAL_FACEBOOK"].ToString();
                    txttwitter.Text = dt.Rows[0]["PERSONAL_TWITTER"].ToString();
                    (ucresidcity.FindControl("txtzipcode") as TextBox).Text = dt.Rows[0]["HOMEZIP"].ToString();
                    (ucothercity.FindControl("txtzipcode") as TextBox).Text = dt.Rows[0]["OTHERZIP"].ToString();
                    string City_Zip_H = dt.Rows[0]["HOMEZIP"].ToString();
                    string City_Zip_O = dt.Rows[0]["OTHERZIP"].ToString();
                    ucresidcity.fillcitydetails(Convert.ToInt32(dt.Rows[0]["PERSONAL_HOME_CITY_ID"]), dt.Rows[0]["PERSONAL_HOME_STREET"].ToString(), City_Zip_H);
                    ucothercity.fillcitydetails(Convert.ToInt32(dt.Rows[0]["PERSONAL_OTHER_CITY_ID"]), dt.Rows[0]["PERSONAL_OTHER_STREET"].ToString(), City_Zip_O);
                    string active_flag = dt.Rows[0]["PERSONAL_ACTIVE_FLAG"].ToString();
                    string beast_flag = dt.Rows[0]["personal_beast_flag"].ToString();
                    (this.Master as Site1).show_control(active_flag, pnlpersonal, beast_flag);
                    if (Request.QueryString["personalid"] != "" && Request.QueryString["personalid"] != null)
                    {
                        ucshow.GetPersonalShow(Convert.ToInt32(hdnpersonalid.Value));
                        ucdocx.GetDocxDetails(Convert.ToInt32(hdnpersonalid.Value), "PERSONAL");
                    }
                }
            }
            catch (Exception ex)
            {
                lblerrmsg.Text = "Error: " + ex.Message.ToString();
                lblerrmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void txttermanitationdate_TextChanged(object sender, EventArgs e)
        {
            DateTime cdate = System.DateTime.Now;
            DateTime terdate;
            TimeSpan datediff;
            bool validdate = DateTime.TryParse(txttermanitationdate.Text, out terdate);
            if (validdate == true)
            {
                if (txttermanitationdate.Text != "")
                {
                    terdate = Convert.ToDateTime(txttermanitationdate.Text);
                    datediff = terdate.Subtract(cdate);

                    if (datediff.Days > 60)
                    {
                        //txtemployeestatus.Text = string.Empty;
                        //txtemployeestatus.ReadOnly = false;
                    }
                    else if (txtdateifhire.Text != "")
                    {
                        DateTime datehire = Convert.ToDateTime(txtdateifhire.Text);
                        if (datehire > terdate)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Hire date should be less than termination date!');", true);
                            // txtemployeestatus.Text = string.Empty;
                            txtdateifhire.Text = string.Empty;
                            txttermanitationdate.Text = string.Empty;
                            //  txtemployeestatus.ReadOnly = false;
                        }
                        // txtemployeestatus.Text = "INACTIVE";
                        // txtemployeestatus.ReadOnly = true;
                    }
                    else
                    {
                        //  txtemployeestatus.Text = string.Empty;
                        // txtemployeestatus.ReadOnly = false;
                    }
                }
            }
        }
        public void DeleteData(string flag)
        {
            PersonalData objper = new PersonalData();
            Int32 personalid = Convert.ToInt32(hdnpersonalid.Value);
            string msg = objper.Personal_Delete(personalid, flag);
            if (msg == "")
            {
                divmsg.InnerHtml = (flag == "n") ? "Record deleted successfully!" : "Record undeleted successfully!";
                (this.Master as Site1).show_control(flag, pnlpersonal);
            }
            else
            {
                lblerrmsg.Text = msg;
            }
        }
        protected void Page_UnLoad(object sender, EventArgs e)
        {

        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getcompanyname(string prefixText)
        {
            PersonalData objper = new PersonalData();
            List<string> company = objper.GetCompanyName(prefixText);
            return company;
        }

        protected void gvrEngagement_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "130px");
            }
        }


        public DataTable getdata()
        {
            DataTable dt = new DataTable();
            objmst = new MasterData();

           // dt = objmst.GetSearchDataNew("Personal", txtfirstname.Text, txtlastname.Text, txtmiddlename.Text, ddltitle.SelectedValue, ddlemployeestatus.SelectedItem .Text ,txtcompany .Text , txtphoneland .Text ,ddlemployeetype.SelectedItem.Text ,txtphonecell .Text);
            return dt;
        }
    }
}