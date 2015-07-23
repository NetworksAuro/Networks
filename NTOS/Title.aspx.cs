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
    public partial class Title : System.Web.UI.Page
    {
        MasterData objData = new MasterData();
        DataTable dtTitle = new DataTable();
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
                    lbl_msg.Text = "Title List";
                    grdfill();
                    (this.Master as Site1).hide();
                }
                catch (Exception ex)
                {
                    showerror(ex);
                }

            }
        }

        protected void grdTitle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdTitle.PageIndex = e.NewPageIndex;
            try
            {
                grdfill();
            }
            catch (Exception ex)
            {
                showerror(ex);
            }
        }

        protected void grdTitle_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdTitle.EditIndex = -1;
            try
            {
                grdfill();
            }
            catch (Exception ex)
            {
                showerror(ex);
            }
        }

        protected void grdTitle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Save")
                {
                    TextBox txtTitleName = grdTitle.FooterRow.FindControl("txtTitleName") as TextBox;
                    int afctrows;
                    objData = new MasterData();
                    TextInfo textInfo = cultureInfo.TextInfo;
                    afctrows = objData.Title_Insert(textInfo.ToTitleCase(txtTitleName.Text.Trim()));
                    if (afctrows == 101)
                    {
                        lblerrmsg.Text = "Title already exists.";
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

        protected void grdTitle_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                ImageButton lnkDelete = grdTitle.Rows[e.RowIndex].FindControl("lnkDelete") as ImageButton;
                HiddenField hdfStateid = grdTitle.Rows[e.RowIndex].FindControl("hdfStateid") as HiddenField;

                if (lnkDelete.CommandArgument.ToString().Trim().ToUpper() == "Inactive".Trim().ToUpper())
                {
                    HiddenField hdftitle_id = grdTitle.Rows[e.RowIndex].FindControl("hdftitle_id") as HiddenField;
                    int afctrows;
                    objData = new MasterData();
                    afctrows = objData.Title_Delete(Convert.ToInt32(hdftitle_id.Value.Trim()));
                    if (afctrows == 103)
                    {
                        lblerrmsg.Text = "Title can’t be deleted as is in use by an Personal";
                        return;
                    }
                    grdfill();
                    divmsg.InnerHtml = "Record deleted successfully.";
                }
                else
                {
                    HiddenField hdftitle_id = grdTitle.Rows[e.RowIndex].FindControl("hdftitle_id") as HiddenField;
                    int afctrows;
                    objData = new MasterData();
                    afctrows = objData.Title_Activate(Convert.ToInt32(hdftitle_id.Value.Trim()));
                    grdfill();
                    divmsg.InnerHtml = "Record activated successfully.";
                }

            }
            catch (Exception ex)
            {

                showerror(ex);
            }
        }

        protected void grdTitle_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grdTitle.EditIndex = e.NewEditIndex;
                grdfill();
                grdTitle.FooterRow.Visible = false;
                CheckBox chkStatus = grdTitle.Rows[e.NewEditIndex].FindControl("chkStatus") as CheckBox;
                HiddenField hdfStatus = grdTitle.Rows[e.NewEditIndex].FindControl("hdfStatus") as HiddenField;
                if (hdfStatus.Value.Trim().ToUpper() == "Active".Trim().ToUpper())
                {
                    chkStatus.Checked = true;
                    // chkStatus.Enabled = false;
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

        protected void grdTitle_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox txtTitleNameEdit = grdTitle.Rows[e.RowIndex].FindControl("txtTitleNameEdit") as TextBox;
                HiddenField hdftitle_id = grdTitle.Rows[e.RowIndex].FindControl("hdftitle_id") as HiddenField;
                CheckBox chkStatus = grdTitle.Rows[e.RowIndex].FindControl("chkStatus") as CheckBox;

                if (txtTitleNameEdit.Text.Length == 0)
                {
                    lblerrmsg.Text = "Country name should not be empty";
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
                afctrows = objData.Title_Update(textInfo.ToTitleCase(txtTitleNameEdit.Text.Trim()), Convert.ToInt32(hdftitle_id.Value.Trim()), status);
                if (afctrows == 102)
                {
                    lblerrmsg.Text = "Title already exists.";
                    return;
                }
                else if (afctrows == 103)
                {
                    lblerrmsg.Text = "Title can’t be deleted as is in use by an Personal";
                    return;
                }
                grdTitle.EditIndex = -1;
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
                DataTable dt = new DataTable();
                dt.Columns.Add("Country_name");
                dt.Rows.Add("No Records Found!");

                objData = new MasterData();
                dtTitle = new DataTable();
                dtTitle = objData.Gettitle();
                if (dtTitle.Rows.Count > 0)
                {
                    grdTitle.DataSource = dtTitle;
                    grdTitle.DataBind();
                }
                else
                {
                    foreach (DataColumn cl in dtTitle.Columns)
                    {
                        cl.AllowDBNull = true;
                    }
                    dtTitle.Rows.Add(dtTitle.NewRow());
                    grdTitle.DataSource = dtTitle;
                    grdTitle.DataBind();
                    int columncount = grdTitle.Rows[0].Cells.Count;
                    grdTitle.Rows[0].Cells.Clear();
                    grdTitle.Rows[0].Cells.Add(new TableCell());
                    grdTitle.Rows[0].Cells[0].ColumnSpan = columncount - 2;
                    grdTitle.Rows[0].Cells[0].Text = "No Records Found";
                    grdTitle.Columns[columncount - 2].Visible = false;
                }
                grdTitle.FooterRow.Visible = true;
            }
            catch (Exception ex)
            {

                lblerrmsg.Text = ex.ToString();
            }
        }
        public void showerror(Exception ex)
        {
            lblerrmsg.ForeColor = System.Drawing.Color.Red;
            lblerrmsg.Text = "Error: " + ex.Message;
        }

        protected void grdTitle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((HiddenField)this.Master.FindControl("hdnreqlist")).Value = e.Row.FindControl("txtTitleName").ClientID;
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