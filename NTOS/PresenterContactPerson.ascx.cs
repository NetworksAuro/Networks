using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MasterDataLayer;
using PresenterDataLayer;
using PersonalDataLayer;
using System.Web.UI.HtmlControls;
using CommonFunction;
namespace NTOS
{
    public partial class PresenterContactPerson : System.Web.UI.UserControl
    {
        DataTable dt = new DataTable();
        CommonFun objcf = new CommonFun();
        Label lblerrmsg;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnvenueid.Value = "0";
                hdnpresenterid.Value = "0";
                if (hdnvenueid.Value == "0" && hdnpresenterid.Value == "0")
                {
                    try
                    {
                        createtemptable();
                        bindcontactperson();
                        //DropDownList ddltitle = (DropDownList)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("ddltitle");
                        //AjaxControlToolkit.AutoCompleteExtender acefirstname = (AjaxControlToolkit.AutoCompleteExtender)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("acefirstname");
                        //acefirstname.ContextKey = ddltitle.SelectedValue;
                    }
                    catch (Exception ex)
                    {

                        lblerrmsg.Text = ex.Message;
                    }

                }
            }
        }


     

        protected void Page_Load(object sender, EventArgs e)
        {
            ContentPlaceHolder MainContent = (ContentPlaceHolder)this.Page.Form.FindControl("MainContent");
            lblerrmsg = (Label)MainContent.FindControl("lblerrmsg");
          
        }
        public void loadPersonalFirstName()
        {
            //DropDownList ddltitle = (DropDownList)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("ddltitle");
            DropDownList ddlfirstname = (DropDownList)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("ddlfirstname");
            PresenterData objpres = new PresenterData();
            dt = objpres.GetPersonalName(null, "", "F", "Y");
            objcf.FillDropDownList(ddlfirstname, dt, "PERSONAL_FNAME", "PERSONAL_ID");
        }
        public void bindcontactperson()
        {
            DataTable dt = (DataTable)ViewState["temptable"];
            // dt = new DataTable();
            MasterData objmst = new MasterData();
            //dt=objmst.GetContactPersonList(hdnpresenteridlist.Value);
            RepDetails.DataSource = dt;
            RepDetails.DataBind();
            hdnfirstnameid.Value = "0";
            hdnfirstname.Value = "";
            //DropDownList ddltitle = (DropDownList)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("ddltitle");
            //AjaxControlToolkit.AutoCompleteExtender acefirstname = (AjaxControlToolkit.AutoCompleteExtender)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("acefirstname");
            //acefirstname.ContextKey = ddltitle.SelectedValue;
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
            dt.Columns.Add("titleid", typeof(Int32));
            dt.Columns.Add("personalid", typeof(Int32));
            dt.Columns.Add("title", typeof(string));
            dt.Columns.Add("firstname", typeof(string));
            dt.Columns.Add("lastname", typeof(string));
            dt.Columns.Add("phone", typeof(string));
            dt.Columns.Add("fax", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("contactpersonid", typeof(Int32));
            dt.Columns["contactpersonid"].DefaultValue = 0;
            ViewState["temptable"] = dt;
        }
        protected void RepDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                CommonFun objcf = new CommonFun();
                //  DropDownList ddltitle = (DropDownList)e.Item.FindControl("ddltitle");
                DropDownList ddlfirstname = (DropDownList)e.Item.FindControl("ddlfirstname");
                //MasterData objmst = new MasterData();
                // DataTable dt = objmst.Gettitle();
                // objcf.FillDropDownList(ddltitle, dt, "title_name", "title_id");
                // ddltitle.Items.RemoveAt(0);
                loadPersonalFirstName();
            }
        }
        public void saveTempcontactperson()
        {
            Label lblTitle = (Label)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("lblTitle");
            DropDownList ddlfirstname = (DropDownList)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("ddlfirstname");
            DropDownList ddllastname = (DropDownList)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("ddllastname");
            Label lblfax = (Label)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("lblfax");
            Label lblemail = (Label)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("lblemail");
            Label lblphone = (Label)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("lblphone");
            TextBox txtfirstname = (TextBox)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("txtfirstname");
            if (hdnfirstnameid.Value.Trim() != "" && hdnfirstnameid.Value.Trim() != "0")
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["temptable"];
                DataRow dr;
                dr = dt.NewRow();
                dr["titleid"] = 0;
                dr["personalid"] = hdnfirstnameid.Value;
                dr["title"] = lblTitle.Text;
                dr["firstname"] = ddlfirstname.SelectedItem.Text;
                dr["lastname"] = ddllastname.SelectedItem.Text;
                dr["fax"] = lblfax.Text;
                dr["email"] = lblemail.Text;
                dr["phone"] = lblphone.Text;
                dr["contactpersonid"] = 0;
                dt.Rows.Add(dr);
                ViewState["temptable"] = dt;
                bindcontactperson();
            }
        }
        protected void RepDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

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

        //protected void ddltitle_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList ddlfirstname = (DropDownList)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("ddlfirstname");
        //    DropDownList ddltitle = (DropDownList)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("ddltitle");
        //    Label lblfax = (Label)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("lblfax");
        //    Label lblemail = (Label)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("lblemail");
        //    Label lblphone = (Label)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("lblphone");
        //    TextBox txtfirstname = (TextBox)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("txtfirstname");
        //    DropDownList ddllastname = (DropDownList)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("ddllastname");
        //    AjaxControlToolkit.AutoCompleteExtender acefirstname = (AjaxControlToolkit.AutoCompleteExtender)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("acefirstname");
        //    acefirstname.ContextKey = ddltitle.SelectedValue;
        //    txtfirstname.Text = string.Empty;
        //    ListItem li = ddllastname.Items[0];
        //    ddllastname.Items.Clear();
        //    ddllastname.Items.Add(li);
        //    lblfax.Text = string.Empty;
        //    lblemail.Text = string.Empty;
        //    lblphone.Text = string.Empty;
        //    loadPersonalFirstName();

        //}

        protected void txtfirstname_TextChanged(object sender, EventArgs e)
        {
            DropDownList ddllastname = (DropDownList)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("ddllastname");
            TextBox txtfirstname = (TextBox)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("txtfirstname");
            ddllastname.Focus();
            PresenterData objpres = new PresenterData();
            DataTable dt = objpres.GetPersonalName(txtfirstname.Text, "", "L", "Y");
            CommonFun objcf = new CommonFun();
            objcf.FillDropDownList(ddllastname, dt, "PERSONAL_FNAME", "PERSONAL_ID");
            if (dt.Rows.Count == 1)
            {
                ddllastname.SelectedIndex = 1;
                hdnfirstnameid.Value = ddllastname.SelectedItem.Value;
                loadphonedetails();
                setneweditlink();
            }
            setneweditlink();
        }
        public void setneweditlink()
        {
            LinkButton lnkeditper = (LinkButton)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("lnkEditperson");
            LinkButton lnknewper = (LinkButton)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("lnkAddnewperson");
            if (hdnfirstnameid.Value != "" && hdnfirstnameid.Value != "0")
            {
                lnkeditper.Visible = true;
                lnknewper.Visible = false;
            }
            else
            {
                lnkeditper.Visible = false;
                lnknewper.Visible = true;
            }
        }
        public void loadphonedetails()
        {
            Label lblfax = (Label)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("lblfax");
            Label lblemail = (Label)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("lblemail");
            Label lblphone = (Label)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("lblphone");
            Label lblTitle = (Label)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("lblTitle");
            PersonalData objper = new PersonalData();
            string phone = "";
            DataTable dt = objper.GetPersonalDetails(hdnfirstnameid.Value);
            if (dt.Rows.Count > 0)
            {
                lblfax.Text = dt.Rows[0]["personal_fax"].ToString();
                lblemail.Text = dt.Rows[0]["personal_email"].ToString();
                phone = dt.Rows[0]["personal_phone_land"].ToString();
                lblTitle.Text = dt.Rows[0]["title_name"].ToString();

            }
            else
            {
                lblfax.Text = string.Empty;
                lblemail.Text = string.Empty;
            }
            lblphone.Text = phone;
            lblphone.Focus();
        }
        public void SavePresenterContactPerson(Int32 presenterid)
        {
            saveTempcontactperson();
            DataTable dt = (DataTable)ViewState["temptable"];
            Int32 personalid;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["contactpersonid"]) == 0)
                    {
                        personalid = Convert.ToInt32(dt.Rows[i]["personalid"]);
                        PresenterData objpres = new PresenterData();
                        objpres.PresenterContactperson_Insert(presenterid, personalid);
                    }
                }
            }

        }
        public void SaveVenueContactPerson(Int32 VenueID)
        {
            saveTempcontactperson();
            DataTable dt = (DataTable)ViewState["temptable"];
            Int32 personalid;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["contactpersonid"]) == 0)
                    {
                        personalid = Convert.ToInt32(dt.Rows[i]["personalid"]);
                        VenueDataLayer.VenueData objven = new VenueDataLayer.VenueData();
                        objven.VenueContactperson_Insert(VenueID, personalid);
                    }
                }
            }

        }
        public void LoadContactpersondetails(Int32 presenterid)
        {
            hdntype.Value = "PRESENTER";
            hdnpresenterid.Value = presenterid.ToString();
            hdnvenueid.Value = "0";
            PresenterData objpres = new PresenterData();
            DataTable dt = objpres.GetPresenterPersonalDetails(presenterid);
            DataColumn dc = new DataColumn();
            dc.ColumnName = "tempid";
            dc.DataType = typeof(int);
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            dt.Columns.Add(dc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["tempid"] = i + 1;
            }
            ViewState["temptable"] = dt;
            bindcontactperson();
            showhidefooter(false);
        }

        public void LoadVenueContactpersondetails(Int32 VenueID)
        {
            hdntype.Value = "VENUE";
            hdnvenueid.Value = VenueID.ToString();
            hdnpresenterid.Value = "0";
            VenueDataLayer.VenueData objven = new VenueDataLayer.VenueData();
            DataTable dt = objven.GetVenuePersonalDetails(VenueID);
            DataColumn dc = new DataColumn();
            dc.ColumnName = "tempid";
            dc.DataType = typeof(int);
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            dt.Columns.Add(dc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["tempid"] = i + 1;
            }
            ViewState["temptable"] = dt;
            bindcontactperson();
            Panel pnlfooter = RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("pnlfooter") as Panel;
            pnlfooter.Visible = false;

        }
        public void showhidefooter(bool flg)
        {
            Panel pnlfooter = RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("pnlfooter") as Panel;
            pnlfooter.Visible = flg;
        }
        protected void lnkgoto_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/personal.aspx?personalid=" + hdnfirstnameid.Value + "");
        }

        protected void ddllastname_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddllastname = (DropDownList)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("ddllastname");
            hdnfirstnameid.Value = ddllastname.SelectedItem.Value;
            loadphonedetails();
            setneweditlink();
        }

        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                saveTempcontactperson();
                DropDownList ddlfirstname = RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("ddlfirstname") as DropDownList;
                Panel pnlfooter = RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("pnlfooter") as Panel;
                pnlfooter.Visible = true;
                //DropDownList ddltitle = (DropDownList)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("ddltitle");
                ddlfirstname.ClearSelection();
            }
            catch (Exception ex)
            {
                lblerrmsg.Text = ex.Message;
            }
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["temptable"];
            try
            {
                foreach (RepeaterItem gr in RepDetails.Items)
                {
                    CheckBox chkdelete = (CheckBox)gr.FindControl("chkdelete");
                    HiddenField hdntempid = (HiddenField)gr.FindControl("hdntempid");
                    HiddenField contactpersonid = (HiddenField)gr.FindControl("htncontactpersonid");
                    if (chkdelete.Checked == true)
                    {
                        DataRow[] dr;
                        dr = dt.Select("tempid='" + hdntempid.Value + "'", "");
                        dt.Rows.Remove(dr[0]);
                        if (hdnpresenterid.Value != "0")
                        {
                            MasterData objmst = new MasterData();
                            objmst.ContactPerson_Delete(Convert.ToInt32(contactpersonid.Value), "PRESENTER");
                        }
                        if (hdnvenueid.Value != "0")
                        {
                            MasterData objmst = new MasterData();
                            objmst.ContactPerson_Delete(Convert.ToInt32(contactpersonid.Value), "VENUE");
                        }
                    }
                }
                ViewState["temptable"] = dt;
                bindcontactperson();
                showhidefooter(false);
            }
            catch (Exception ex)
            {
                lblerrmsg.Text = "Error: " + ex.Message;
            }


        }

        protected void ddlfirstname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                DropDownList ddlfirstname = (DropDownList)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("ddlfirstname");
                Label lblTitle = (Label)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("lblTitle");
                Label lblphone = (Label)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("lblphone");
                Label lblfax = (Label)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("lblfax");
                Label lblemail = (Label)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("lblemail");
                DropDownList ddllastname = (DropDownList)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("ddllastname");
                if (ddlfirstname.SelectedIndex > 0)
                {
                    loadPersonalLastName();
                }
                else
                {
                    lblTitle.Text=""; 
                    lblphone.Text=""; 
                    lblfax.Text = "";
                    lblemail.Text = "";
                    ddllastname.SelectedIndex = 0;
                    hdnfirstnameid.Value = "0";

                }
                setneweditlink();
            }
            catch (Exception ex)
            {
                lblerrmsg.Text = "Error: " + ex.Message;
            }
        }
        public void loadPersonalLastName()
        {
            DropDownList ddlfirstname = (DropDownList)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("ddlfirstname");
            DropDownList ddllastname = (DropDownList)RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("ddllastname");
            PresenterData objpres = new PresenterData();
            DataTable dt = objpres.GetPersonalName(ddlfirstname.SelectedItem.Text, "", "L", "Y");

            CommonFun objcf = new CommonFun();
            objcf.FillDropDownList(ddllastname, dt, "PERSONAL_FNAME", "PERSONAL_ID");
            if (dt.Rows.Count == 1)
            {
                ddllastname.SelectedIndex = 1;
                hdnfirstnameid.Value = ddllastname.SelectedItem.Value;
            }
            loadphonedetails();
            ddllastname.Focus();

        }

        protected void lnkAddnewperson_Click(object sender, EventArgs e)
        {
            Presenter objpre = new Presenter();
            ((IMethod)Page).SaveDataFromContactPerson("0");
            
        }

        public interface IMethod
        {
            void SaveDataFromContactPerson(string a);
        }

        protected void lnkEditperson_Click(object sender, EventArgs e)
        {
            ((IMethod)Page).SaveDataFromContactPerson(hdnfirstnameid.Value.ToString());
        }

        
    }
}