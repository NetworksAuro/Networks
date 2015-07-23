using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShowDataLayer;
using MasterDataLayer;
using PersonalDataLayer;
using CommonFunction;
namespace NTOS
{
    public partial class PersonalShow : System.Web.UI.UserControl
    {
        CommonFun objcf = new CommonFun();
        Label lblerrmsg;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hdnucpersonalid.Value = "0";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ContentPlaceHolder MainContent = (ContentPlaceHolder)this.Page.Form.FindControl("MainContent");
            lblerrmsg = (Label)MainContent.FindControl("lblerrmsg");
            if (!IsPostBack && ((Request.QueryString.Count == 0) || string.IsNullOrEmpty(Request.QueryString["personalid"])))
            {
                try
                {
                    createtemptable();
                    BindShow();
                    trfooter.Visible = true;

                }
                catch (Exception ex)
                {
                    lblerrmsg.Text = "Error: " + ex.Message;
                }
            }
        }
        public void createtemptable()
        {
            if (string.IsNullOrEmpty(Request.QueryString["personalid"])) { hdnucpersonalid.Value = "0"; }
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn();
            dc.ColumnName = "tempid";
            dc.DataType = typeof(int);
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            dt.Columns.Add(dc);
            dt.Columns.Add("showid", typeof(string));
            dt.Columns.Add("showname", typeof(string));
            dt.Columns.Add("assigndate", typeof(string));
            dt.Columns.Add("enddate", typeof(string));
            dt.Columns.Add("assignflag", typeof(string));
            dt.Columns.Add("assignflagtext", typeof(string));
            dt.Columns.Add("per_assign_id", typeof(int)).DefaultValue = 0;
            ViewState["temptable"] = dt;
        }
        public void BindShow()
        {
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["temptable"];
            RepDetails.DataSource = dt;
            RepDetails.DataBind();
        }
        protected void RepDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string msg = "Assignment date should be less than end date!";
                txtenddateF.Attributes.Add("onchange", "return comparedate('" + txtassignmentdateF.ClientID + "','" + txtenddateF.ClientID + "','" + msg + "');");
                txtassignmentdateF.Attributes.Add("onchange", "return comparedate('" + txtassignmentdateF.ClientID + "','" + txtenddateF.ClientID + "','" + msg + "');");
                MasterData objmas = new MasterData();
                try
                {
                    DataTable dt = objmas.Getshows("0");
                    objcf.FillDropDownList(ddlshownameF, dt, "show_name", "show_id");
                }
                catch (Exception ex)
                {
                    lblerrmsg.Text = "Error: " + ex.Message;
                }
            }
        }
        public void GetPersonalShow(Int32 personalid)
        {
            hdnucpersonalid.Value = personalid.ToString();
            PersonalData objper = new PersonalData();
            DataTable dt = new DataTable();
            dt = objper.GetPersonalShowAssignDetails(personalid);
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
            BindShow();
            showhidefooter(false);

        }
        public void showhidefooter(bool sh_flg)
        {
            trfooter.Visible = sh_flg;
        }
        public void SaveTempShowDetails()
        {
            if (ddlshownameF.SelectedIndex > 0)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["temptable"];
                DataRow dr;
                dr = dt.NewRow();
                dr["showid"] = ddlshownameF.SelectedItem.Value;
                dr["showname"] = ddlshownameF.SelectedItem.Text;
                dr["assigndate"] = Convert.ToString(txtassignmentdateF.Text);
                dr["enddate"] = Convert.ToString(txtenddateF.Text);
                dr["assignflagtext"] = ddlcurrentshowF.SelectedItem.Text;
                dr["assignflag"] = ddlcurrentshowF.SelectedItem.Value;
                dr["per_assign_id"] = 0;
                dt.Rows.Add(dr);
                ViewState["temptable"] = dt;
                BindShow();
            }
        }
        public void SavePersonalShow(Int32 personalid)
        {
            SaveTempShowDetails();
            DataTable dt;
            dt = (DataTable)ViewState["temptable"];
            PersonalData objper = new PersonalData();
            Int32 personalassignshowid;
            DateTime assigndate;
            Nullable<DateTime> enddate = null;
            string assingflag;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["per_assign_id"].ToString() == "0")
                {
                    personalassignshowid = Convert.ToInt32(dt.Rows[i]["showid"]);
                    assigndate = Convert.ToDateTime(dt.Rows[i]["assigndate"]);
                    enddate = (dt.Rows[i]["enddate"].ToString() != "") ? Convert.ToDateTime(dt.Rows[i]["enddate"]) : enddate;
                    assingflag = Convert.ToString(dt.Rows[i]["assignflag"]);
                    objper.PersonalShowAssign_Insert(personalid, personalassignshowid, assigndate, enddate, assingflag);
                }
            }
            GetPersonalShow(personalid);
        }
        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SaveTempShowDetails();
                trfooter.Visible = true;
            }
            catch (Exception ex)
            {
                lblerrmsg.Text = "Error: " + ex.Message;
            }
        }
        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["temptable"];
                foreach (RepeaterItem gr in RepDetails.Items)
                {
                    CheckBox chkdelete = (CheckBox)gr.FindControl("chkdelete");
                    HiddenField hdntempid = (HiddenField)gr.FindControl("hdntempid");
                    HiddenField hdnpersonalshowid = (HiddenField)gr.FindControl("hdnpersonalshowid");
                    if (chkdelete.Checked == true)
                    {
                        DataRow[] dr;
                        dr = dt.Select("tempid='" + hdntempid.Value + "'", "");
                        dt.Rows.Remove(dr[0]);
                        if (hdnpersonalshowid.Value != "0")
                        {
                            PersonalData objper = new PersonalData();
                            objper.PersonalShowAssign_Delete(Convert.ToInt32(hdnpersonalshowid.Value));
                        }
                    }
                }
                ViewState["temptable"] = dt;
                BindShow();

                trfooter.Visible = false;
            }
            catch (Exception ex)
            {
                lblerrmsg.Text = "Error: " + ex.Message;
            }
        }

        protected void ddlshownameF_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            bool flg = (ddl.SelectedIndex > 0) ? true : false;
            rfvassignmentdate.Enabled = flg;
            rfvcurrentflag.Enabled = flg;
        }

        protected void RepDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "edit")
            {
                e.Item.FindControl("tredit").Visible = true;
                e.Item.FindControl("trcrow").Visible = false;
                MasterData objmas = new MasterData();
                DataTable dt = objmas.Getshows("Y");
                DropDownList ddlshownameE = (DropDownList)e.Item.FindControl("ddlshownameE");
                HiddenField hdnshowid = (HiddenField)e.Item.FindControl("hdnshowid");
                objcf.FillDropDownList(ddlshownameE, dt, "show_name", "show_id");
                ddlshownameE.SelectedIndex = ddlshownameE.Items.IndexOf(ddlshownameE.Items.FindByValue(hdnshowid.Value));
            }
            if (e.CommandName.ToLower() == "cancel")
            {
                BindShow();
            }
            if (e.CommandName.ToLower() == "update")
            {
                UpdateShowDetails(e.Item);
                BindShow();
            }
            trfooter.Visible = false;
        }
        public void UpdateShowDetails(RepeaterItem ri)
        {
            TextBox txtassignmentdateE = (TextBox)ri.FindControl("txtassignmentdateE");
            TextBox txtenddateE = (TextBox)ri.FindControl("txtenddateE");
            DropDownList ddlshownameE = (DropDownList)ri.FindControl("ddlshownameE");
            DropDownList ddlcurrentshowE = (DropDownList)ri.FindControl("ddlcurrentshowE");
            HiddenField hdnPersonalAssignid = (HiddenField)ri.FindControl("hdnpersonalshowid");
            HiddenField hdntempid = (HiddenField)ri.FindControl("hdntempid");
            if (ddlshownameE.SelectedIndex > 0)
            {

                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["temptable"];
                dt.Columns["assigndate"].ReadOnly = false;
                dt.Columns["enddate"].ReadOnly = false;
                dt.Columns["assignflagtext"].ReadOnly = false;
                dt.Columns["assignflag"].ReadOnly = false;
                IEnumerable<DataRow> rows = dt.Rows.Cast<DataRow>().Where(r => r["tempid"].ToString() == hdntempid.Value);
                rows.ToList().ForEach(r => r.SetField("showname", ddlshownameE.SelectedItem.Text));
                rows.ToList().ForEach(r => r.SetField("assigndate", txtassignmentdateE.Text));
                rows.ToList().ForEach(r => r.SetField("enddate", Convert.ToString(txtenddateE.Text)));
                rows.ToList().ForEach(r => r.SetField("assignflagtext", ddlcurrentshowE.SelectedItem.Text));
                rows.ToList().ForEach(r => r.SetField("assignflag", ddlcurrentshowE.SelectedItem.Value));
                ViewState["temptable"] = dt;

                if (hdnPersonalAssignid.Value != "0")
                {
                    PersonalData objper = new PersonalData();
                    Int32 personalassignshowid;
                    DateTime assigndate;
                    Nullable<DateTime> enddate = null;
                    string assingflag;
                    personalassignshowid = Convert.ToInt32(ddlshownameE.SelectedItem.Value);
                    assigndate = Convert.ToDateTime(txtassignmentdateE.Text);
                    enddate = (txtenddateE.Text.ToString() != "") ? Convert.ToDateTime(txtenddateE.Text) : enddate;
                    assingflag = Convert.ToString(ddlcurrentshowE.SelectedItem.Value);
                    objper.PersonalShowAssign_Update(Convert.ToInt32(hdnPersonalAssignid.Value), Convert.ToInt32(hdnucpersonalid.Value), personalassignshowid, assigndate, enddate, assingflag);
                }
                BindShow();

            }

        }

        protected void ddlshownameE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            System.Web.UI.HtmlControls.HtmlTableRow tr = (System.Web.UI.HtmlControls.HtmlTableRow)ddl.Parent.Parent;
            RequiredFieldValidator rfvassignmentdateE = (RequiredFieldValidator)tr.FindControl("rfvassignmentdateE");
            RequiredFieldValidator rfvcurrentflagE = (RequiredFieldValidator)tr.FindControl("rfvcurrentflagE");
            bool flg = (ddl.SelectedIndex > 0) ? true : false;
            rfvassignmentdateE.Enabled = flg;
            rfvcurrentflagE.Enabled = flg;
        }

    }
}