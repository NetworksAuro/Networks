using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MasterDataLayer;
using System.Data;
using System.Globalization;
using System.Threading;
namespace NTOS
{
    public partial class State : System.Web.UI.Page
    {
        MasterData objData = new MasterData();
        DataTable dtstate = new DataTable();
        CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblerrmsg.Text = "";
            divmsg.InnerHtml = "";

            if (!IsPostBack)
            {
                (this.Master as Site1).HideNewbutton();
                Label lbl_msg;
                lbl_msg = (Label)this.Master.FindControl("lbl_headersite");
                lbl_msg.Text = "State List";
                grdfill();
                (this.Master as Site1).hide();
            }
        }
        protected void grdState_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdState.EditIndex = -1;
            grdfill();
        }

        protected void grdState_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                ImageButton lnkDelete = grdState.Rows[e.RowIndex].FindControl("lnkDelete") as ImageButton;
                HiddenField hdfStateid = grdState.Rows[e.RowIndex].FindControl("hdfStateid") as HiddenField;

                if (lnkDelete.CommandArgument.ToString().Trim().ToUpper() == "Inactive".Trim().ToUpper())
                {

                    int afctrows;
                    objData = new MasterData();
                    afctrows = objData.State_Delete(Convert.ToInt32(hdfStateid.Value.Trim()));
                    if (afctrows == 103)
                    {
                        lblerrmsg.Text = "State can’t be deleted as is in use by an city";
                        return;
                    }
                    grdfill();
                    divmsg.InnerHtml = "Record deleted successfully.";
                    (this.Master as Site1).ClearModifyStatus();
                }
                else
                {
                    int afctrows;
                    objData = new MasterData();
                    afctrows = objData.State_Activate(Convert.ToInt32(hdfStateid.Value.Trim()));
                    grdfill();
                    divmsg.InnerHtml = "Record activated successfully.";
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void grdState_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grdState.EditIndex = e.NewEditIndex;
                grdfill();
                grdState.FooterRow.Visible = false;
                DropDownList ddlCountryE = grdState.Rows[e.NewEditIndex].FindControl("ddlCountryE") as DropDownList;
                HiddenField hdfCountryid = grdState.Rows[e.NewEditIndex].FindControl("hdfCountryid") as HiddenField;
                CheckBox chkStatus = grdState.Rows[e.NewEditIndex].FindControl("chkStatus") as CheckBox;
                HiddenField hdfStatus = grdState.Rows[e.NewEditIndex].FindControl("hdfStatus") as HiddenField;
                if (hdfStatus.Value.Trim().ToUpper() == "Active".Trim().ToUpper())
                {
                    chkStatus.Checked = true;
                    // chkStatus.Enabled = false;
                }
                else
                {
                    chkStatus.Checked = false;
                }
                getcountry(ddlCountryE);
                ddlCountryE.SelectedIndex = ddlCountryE.Items.IndexOf(ddlCountryE.Items.FindByValue(hdfCountryid.Value.ToString()));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void grdState_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox txtStateEdit = grdState.Rows[e.RowIndex].FindControl("txtStateEdit") as TextBox;
                HiddenField hdfStateid = grdState.Rows[e.RowIndex].FindControl("hdfStateid") as HiddenField;
                DropDownList ddlCountryE = grdState.Rows[e.RowIndex].FindControl("ddlCountryE") as DropDownList;
                CheckBox chkStatus = grdState.Rows[e.RowIndex].FindControl("chkStatus") as CheckBox;
                if (ddlCountryE.SelectedIndex == 0)
                {
                    lblerrmsg.Text = "Please select country";
                    return;
                }
                if (txtStateEdit.Text.Length == 0)
                {
                    lblerrmsg.Text = "State should not be empty";
                    return;
                }
                string status;
                if (chkStatus.Checked == true)
                {
                    status = "Y";
                }
                else
                {
                    status = "N";
                }

                int afctrows;
                objData = new MasterData();
                TextInfo textInfo = cultureInfo.TextInfo;
                afctrows = objData.State_Update(textInfo.ToTitleCase(txtStateEdit.Text.Trim()), Convert.ToInt32(hdfStateid.Value.Trim()), Convert.ToInt32(ddlCountryE.SelectedItem.Value.Trim()), status);
                if (afctrows == 102)
                {
                    lblerrmsg.Text = "State already exists.";
                    return;
                }
                else if (afctrows == 103)
                {
                    lblerrmsg.Text = "State can’t be deleted as is in use by an city";
                    return;
                }
                grdState.EditIndex = -1;
                grdfill();

                divmsg.InnerHtml = "Record updated successfully.";
                (this.Master as Site1).ClearModifyStatus();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void grdState_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Save")
                {
                    DropDownList ddlCountry = grdState.FooterRow.FindControl("ddlCountry") as DropDownList;
                    TextBox txtStateName = grdState.FooterRow.FindControl("txtStateName") as TextBox;
                    int afctrows;
                    objData = new MasterData();
                    TextInfo textInfo = cultureInfo.TextInfo;
                    afctrows = objData.State_Insert(textInfo.ToTitleCase(txtStateName.Text.Trim()), Convert.ToInt32(ddlCountry.SelectedItem.Value));
                    if (afctrows == 101)
                    {
                        lblerrmsg.Text = "State already exists.";
                        return;
                    }
                    grdfill();
                    divmsg.InnerHtml = "Record inserted successfully.";
                    (this.Master as Site1).ClearModifyStatus();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void grdfill()
        {
            try
            {

                dtstate.Clear();
                dtstate = new DataTable();
                objData = new MasterData();
                dtstate = objData.Getstate();
                if (dtstate.Rows.Count > 0)
                {
                    grdState.DataSource = dtstate;
                    grdState.DataBind();
                }
                else
                {
                    foreach (DataColumn cl in dtstate.Columns)
                    {
                        cl.AllowDBNull = true;
                    }
                    dtstate.Rows.Add(dtstate.NewRow());
                    grdState.DataSource = dtstate;
                    grdState.DataBind();
                    int columncount = grdState.Rows[0].Cells.Count;
                    grdState.Rows[0].Cells.Clear();
                    grdState.Rows[0].Cells.Add(new TableCell());
                    grdState.Rows[0].Cells[0].ColumnSpan = columncount - 2;
                    grdState.Rows[0].Cells[0].Text = "No Records Found";
                    grdState.Columns[columncount - 2].Visible = false;
                }
                grdState.FooterRow.Visible = true;
                DropDownList ddlcountry = grdState.FooterRow.FindControl("ddlCountry") as DropDownList;
                getcountry(ddlcountry);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void getcountry(DropDownList ddl)
        {
            DataTable dtcountry = new DataTable();
            objData = new MasterData();
            dtcountry = objData.Getcountry("Y");
            ddl.DataSource = dtcountry;
            ddl.DataTextField = "country_name";
            ddl.DataValueField = "country_id";
            ddl.DataBind();
            ddl.Items.Insert(0, "-Select-");
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText("USA"));
        }

        protected void grdState_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdState.PageIndex = e.NewPageIndex;
            grdfill();
        }

        protected void grdState_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((HiddenField)this.Master.FindControl("hdnreqlist")).Value = e.Row.FindControl("txtStateName").ClientID + "," + e.Row.FindControl("ddlCountry").ClientID;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                    ImageButton lnkDelete = (ImageButton)e.Row.FindControl("lnkDelete");
                    AjaxControlToolkit.ConfirmButtonExtender cnfm = (AjaxControlToolkit.ConfirmButtonExtender)e.Row.FindControl("cnfm");
                    if (lblStatus.Text.Trim().ToUpper() == "Active".Trim().ToUpper())
                    {
                        lnkDelete.ImageUrl = "~/Images/delete-gdv.png";
                        lnkDelete.ToolTip = "Inactive";
                        lnkDelete.CommandArgument = "Inactive";
                        cnfm.ConfirmText = "Are you sure to Inactive this record?";
                    }
                    else
                    {
                        lnkDelete.ImageUrl = "~/Images/Active.png";
                        lnkDelete.ToolTip = "Active";
                        lnkDelete.CommandArgument = "Active";
                        cnfm.ConfirmText = "Are you sure to Activate this record?";
                    }
                }
            }



        }
    }
}
