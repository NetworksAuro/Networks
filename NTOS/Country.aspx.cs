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
    public partial class Country : System.Web.UI.Page
    {
        MasterData objData = new MasterData();
        DataTable dtcounty = new DataTable();
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
                    lbl_msg.Text = "Country List";
                    grdfill();
                    (this.Master as Site1).hide();
                }
                catch (Exception ex)
                {
                    showerror(ex);
                }

            }

        }

        protected void grdCountry_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdCountry.EditIndex = -1;
            try
            {
                grdfill();
            }
            catch (Exception ex)
            {
                showerror(ex);
            }
        }

        protected void grdCountry_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                ImageButton lnkDelete = grdCountry.Rows[e.RowIndex].FindControl("lnkDelete") as ImageButton;
                HiddenField hdfStateid = grdCountry.Rows[e.RowIndex].FindControl("hdfStateid") as HiddenField;

                if (lnkDelete.CommandArgument.ToString().Trim().ToUpper() == "Inactive".Trim().ToUpper())
                {
                    HiddenField hdfCountryid = grdCountry.Rows[e.RowIndex].FindControl("hdfCountryid") as HiddenField;
                    int afctrows;
                    objData = new MasterData();
                    afctrows = objData.Country_Delete(Convert.ToInt32(hdfCountryid.Value.Trim()));
                    if (afctrows == 103)
                    {
                        lblerrmsg.Text = "Country can’t be deleted as is in use by an state";
                        return;
                    }
                    grdfill();
                    divmsg.InnerHtml = "Record deleted successfully.";

                }
                else
                {
                    HiddenField hdfCountryid = grdCountry.Rows[e.RowIndex].FindControl("hdfCountryid") as HiddenField;
                    int afctrows;
                    objData = new MasterData();
                    afctrows = objData.Country_Activate(Convert.ToInt32(hdfCountryid.Value.Trim()));
                    grdfill();
                    divmsg.InnerHtml = "Record activated successfully.";
                }
            }
            catch (Exception ex)
            {
                showerror(ex);
            }

        }

        protected void grdCountry_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grdCountry.EditIndex = e.NewEditIndex;
                grdfill();
                grdCountry.FooterRow.Visible = false;
                CheckBox chkStatus = grdCountry.Rows[e.NewEditIndex].FindControl("chkStatus") as CheckBox;
                HiddenField hdfStatus = grdCountry.Rows[e.NewEditIndex].FindControl("hdfStatus") as HiddenField;
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

        protected void grdCountry_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox txtCountryEdit = grdCountry.Rows[e.RowIndex].FindControl("txtCountryEdit") as TextBox;
                HiddenField hdfCountryid = grdCountry.Rows[e.RowIndex].FindControl("hdfCountryid") as HiddenField;
                CheckBox chkStatus = grdCountry.Rows[e.RowIndex].FindControl("chkStatus") as CheckBox;
                if (txtCountryEdit.Text.Length == 0)
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
                afctrows = objData.Country_Update(textInfo.ToTitleCase(txtCountryEdit.Text.Trim()), Convert.ToInt32(hdfCountryid.Value.Trim()), status);
                if (afctrows == 102)
                {
                    lblerrmsg.Text = "Country already exists.";
                    return;
                }
                else if (afctrows == 103)
                {
                    lblerrmsg.Text = "Country can’t be deleted as is in use by an state";
                    return;
                }
                grdCountry.EditIndex = -1;
                grdfill();
                divmsg.InnerHtml = "Record updated successfully!";
                (this.Master as Site1).ClearModifyStatus();
            }
            catch (Exception ex)
            {
                showerror(ex);
            }
        }

        protected void grdCountry_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Save")
                {
                    TextBox txtContryName = grdCountry.FooterRow.FindControl("txtCountryName") as TextBox;
                    int afctrows;
                    objData = new MasterData();
                    TextInfo textInfo = cultureInfo.TextInfo;
                    afctrows = objData.Country_Insert(textInfo.ToTitleCase(txtContryName.Text.Trim()));
                    if (afctrows == 101)
                    {
                        lblerrmsg.Text = "Country already exists.";
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

        public void grdfill()
        {
            try
            {
                // DataTable dt = new DataTable();

                objData = new MasterData();
                DataTable dtcounty = new DataTable();
                dtcounty = objData.Getcountry();

                if (dtcounty.Rows.Count > 0)
                {
                    grdCountry.DataSource = dtcounty;
                    grdCountry.DataBind();
                }
                else
                {
                    foreach (DataColumn cl in dtcounty.Columns)
                    {
                        cl.AllowDBNull = true;
                    }
                    dtcounty.Rows.Add(dtcounty.NewRow());
                    grdCountry.DataSource = dtcounty;
                    grdCountry.DataBind();
                    int columncount = grdCountry.Rows[0].Cells.Count;
                    grdCountry.Rows[0].Cells.Clear();
                    grdCountry.Rows[0].Cells.Add(new TableCell());
                    grdCountry.Rows[0].Cells[0].ColumnSpan = columncount - 2;
                    grdCountry.Rows[0].Cells[0].Text = "No Records Found";
                    grdCountry.Columns[columncount - 2].Visible = false;
                }
                grdCountry.FooterRow.Visible = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void grdCountry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCountry.PageIndex = e.NewPageIndex;
            try
            {
                grdfill();
            }
            catch (Exception ex)
            {
                showerror(ex);
            }

        }
        public void showerror(Exception ex)
        {
            lblerrmsg.ForeColor = System.Drawing.Color.Red;
            lblerrmsg.Text = "Error: " + ex.Message;
        }

        protected void grdCountry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
               ((HiddenField)this.Master.FindControl("hdnreqlist")).Value = e.Row.FindControl("txtCountryName").ClientID;
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