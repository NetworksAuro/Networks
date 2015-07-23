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
    public partial class Timezone : System.Web.UI.Page
    {
        MasterData objData = new MasterData();
        DataTable dtTimezone = new DataTable();
        CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblerrmsg.Text = "";
            divmsg.InnerHtml = "";

            if (!IsPostBack)
            {
                try
                {
                    (this.Master as Site1).HideNewbutton();
                    Label lbl_msg;
                    lbl_msg = (Label)this.Master.FindControl("lbl_headersite");
                    lbl_msg.Text = "TimeZone List";
                    grdfill();
                    (this.Master as Site1).hide();
                }
                catch (Exception ex)
                {
                    showerror(ex);
                }

            }

        }

        protected void grdTimezone_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdTimezone.PageIndex = e.NewPageIndex;
            try
            {
                grdfill();
            }
            catch (Exception ex)
            {
                showerror(ex);
            }

        }

        protected void grdTimezone_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdTimezone.EditIndex = -1;
            try
            {
                grdfill();
            }
            catch (Exception ex)
            {
                showerror(ex);
            }
        }

        protected void grdTimezone_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Save")
                {
                    TextBox txttimezonedesc = grdTimezone.FooterRow.FindControl("txttimezonedesc") as TextBox;
                    int afctrows;
                    objData = new MasterData();
                    TextInfo textInfo = cultureInfo.TextInfo;
                    afctrows = objData.Timezone_Insert(textInfo.ToTitleCase(txttimezonedesc.Text.Trim()));
                    if (afctrows == 101)
                    {
                        lblerrmsg.Text = "Timezone already exists.";
                        return;
                    }
                    grdfill();
                    divmsg.InnerHtml = "Record inserted successfully.";
                    (this.Master as Site1).ClearModifyStatus();
                }

            }
            catch (Exception ex)
            {
                showerror(ex);
            }
        }

        protected void grdTimezone_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {  ImageButton lnkDelete = grdTimezone.Rows[e.RowIndex].FindControl("lnkDelete") as ImageButton;
            HiddenField hdfStateid = grdTimezone.Rows[e.RowIndex].FindControl("hdfStateid") as HiddenField;

                if (lnkDelete.CommandArgument.ToString().Trim().ToUpper() == "Inactive".Trim().ToUpper())
                {
                    HiddenField hdfTimezoneid = grdTimezone.Rows[e.RowIndex].FindControl("hdfTimezoneid") as HiddenField;
                    int afctrows;
                    objData = new MasterData();
                    afctrows = objData.Timezone_Delete(Convert.ToInt32(hdfTimezoneid.Value.Trim()));
                    if (afctrows == 103)
                    {
                        lblerrmsg.Text = "Timezone can’t be deleted as is in use by an city";
                        return;
                    }
                    grdfill();
                    divmsg.InnerHtml = "Record deleted successfully.";
                }
                else
                {
                    HiddenField hdfTimezoneid = grdTimezone.Rows[e.RowIndex].FindControl("hdfTimezoneid") as HiddenField;
                    int afctrows;
                    objData = new MasterData();
                    afctrows = objData.Timezone_Activate(Convert.ToInt32(hdfTimezoneid.Value.Trim()));
                    grdfill();
                    divmsg.InnerHtml = "Record activated successfully.";

                }
            }
            catch (Exception ex)
            {
                showerror(ex);
            }
        }

        protected void grdTimezone_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grdTimezone.EditIndex = e.NewEditIndex;
                grdfill();
                grdTimezone.FooterRow.Visible = false;
                CheckBox chkStatus = grdTimezone.Rows[e.NewEditIndex].FindControl("chkStatus") as CheckBox;
                HiddenField hdfStatus = grdTimezone.Rows[e.NewEditIndex].FindControl("hdfStatus") as HiddenField;
                if (hdfStatus.Value.Trim().ToUpper() == "Active".Trim().ToUpper())
                {
                    chkStatus.Checked = true;
                    //  chkStatus.Enabled = false;
                }
                else
                {
                    chkStatus.Checked = false;
                }

            }
            catch (Exception ex)
            {
                showerror(ex);
            }
        }

        protected void grdTimezone_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox txttimezonedescEdit = grdTimezone.Rows[e.RowIndex].FindControl("txttimezonedescEdit") as TextBox;
                HiddenField hdfTimezoneid = grdTimezone.Rows[e.RowIndex].FindControl("hdfTimezoneid") as HiddenField;
                CheckBox chkStatus = grdTimezone.Rows[e.RowIndex].FindControl("chkStatus") as CheckBox;

                if (txttimezonedescEdit.Text.Length == 0)
                {
                    lblerrmsg.Text = "Timezone name should not be empty";
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
                afctrows = objData.Timezone_Update(textInfo.ToTitleCase(txttimezonedescEdit.Text.Trim()), Convert.ToInt32(hdfTimezoneid.Value.Trim()), status);
                if (afctrows == 102)
                {
                    lblerrmsg.Text = "Timezone already exists.";
                    return;
                }
                else if (afctrows == 103)
                {
                    lblerrmsg.Text = "Timezone can’t be deleted as is in use by an city";
                    return;
                }
                grdTimezone.EditIndex = -1;
                grdfill();
                divmsg.InnerHtml = "Record updated successfully!";
                (this.Master as Site1).ClearModifyStatus();
            }
            catch (Exception ex)
            {
                showerror(ex);
            }
        }
        public void grdfill()
        {
            try
            {
                // DataTable dt = new DataTable();

                dtTimezone.Clear();
                objData = new MasterData();
                dtTimezone = new DataTable();
                dtTimezone = objData.GetTimezone(null);
                if (dtTimezone.Rows.Count > 0)
                {
                    grdTimezone.DataSource = dtTimezone;
                    grdTimezone.DataBind();
                }
                else
                {
                    foreach (DataColumn cl in dtTimezone.Columns)
                    {
                        cl.AllowDBNull = true;
                    }
                    dtTimezone.Rows.Add(dtTimezone.NewRow());
                    grdTimezone.DataSource = dtTimezone;
                    grdTimezone.DataBind();
                    int columncount = grdTimezone.Rows[0].Cells.Count;
                    grdTimezone.Rows[0].Cells.Clear();
                    grdTimezone.Rows[0].Cells.Add(new TableCell());
                    grdTimezone.Rows[0].Cells[0].ColumnSpan = columncount - 2;
                    grdTimezone.Rows[0].Cells[0].Text = "No Records Found";
                    grdTimezone.Columns[columncount - 2].Visible = false;
                }
                grdTimezone.FooterRow.Visible = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void showerror(Exception ex)
        {
            lblerrmsg.ForeColor = System.Drawing.Color.Red;
            lblerrmsg.Text = "Error: " + ex.Message;
        }

        protected void grdTimezone_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((HiddenField)this.Master.FindControl("hdnreqlist")).Value = e.Row.FindControl("txttimezonedesc").ClientID;
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