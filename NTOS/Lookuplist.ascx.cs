using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using MasterDataLayer;
using System.Data;
namespace NTOS
{
    public partial class Lookuplist : System.Web.UI.UserControl
    {
        MasterData objData = new MasterData();
        DataTable dtlookup = new DataTable();
        CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        protected string group = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lblerrmsg.Text = "";
            divmsg.InnerHtml = "";

            if (!IsPostBack)
            {
                try
                {
                    grdfill();
                }
                catch (Exception ex)
                {
                    showerror(ex);
                }
            }
        }
        protected void grdLookup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdLookup.PageIndex = e.NewPageIndex;
            try
            {
                grdfill();
            }
            catch (Exception ex)
            {
                showerror(ex);
            }
        }

        protected void grdLookup_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdLookup.EditIndex = -1;
            try
            {
                grdfill();
            }
            catch (Exception ex)
            {
                showerror(ex);
            }
        }

        protected void grdLookup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Save")
                {
                    DropDownList ddllkup_group = grdLookup.FooterRow.FindControl("ddllkup_group") as DropDownList;
                    TextBox txtlkup_desc = grdLookup.FooterRow.FindControl("txtlkup_desc") as TextBox;
                    int afctrows;
                    objData = new MasterData();
                    string groupvalue = ddllkup_group.SelectedItem.Value.Split('|').GetValue(0).ToString();
                    int groupsize = Convert.ToInt32(ddllkup_group.SelectedItem.Value.Split('|').GetValue(1).ToString());
                    if (groupsize < txtlkup_desc.Text.ToString().Length)
                    {
                        lblerrmsg.Text = "Check lookup value length";
                        return;
                    }
                    TextInfo textInfo = cultureInfo.TextInfo;
                    afctrows = objData.Lookup_Insert(textInfo.ToTitleCase(txtlkup_desc.Text.Trim()), groupvalue);
                    if (afctrows == 101)
                    {
                        lblerrmsg.Text = "Lookup already exists.";
                        return;
                    }
                    grdfill();
                    divmsg.InnerHtml = "Record inserted successfully.";
                    (this.Page.Master as Site1).ClearModifyStatus();
                }

            }
            catch (Exception ex)
            {
                showerror(ex);
            }
        }

        protected void grdLookup_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                ImageButton lnkDelete = grdLookup.Rows[e.RowIndex].FindControl("lnkDelete") as ImageButton;
                HiddenField hdfStateid = grdLookup.Rows[e.RowIndex].FindControl("hdfStateid") as HiddenField;

                if (lnkDelete.CommandArgument.ToString().Trim().ToUpper() == "Inactive".Trim().ToUpper())
                {
                    HiddenField hdfLookupid = grdLookup.Rows[e.RowIndex].FindControl("hdfLookupid") as HiddenField;
                    Label lbllkup_desc = grdLookup.Rows[e.RowIndex].FindControl("lbllkup_desc") as Label;
                    int afctrows;
                    objData = new MasterData();
                    afctrows = objData.Lookup_Delete(Convert.ToInt32(hdfLookupid.Value.Trim()), lbllkup_desc.Text.Trim());
                    if (afctrows == 103)
                    {
                        lblerrmsg.Text = "Lookup can’t be deleted as is in use by an engagement or schedule";
                        return;
                    }
                    grdfill();
                    divmsg.InnerHtml = "Record deleted successfully.";
                }
                else
                {
                    HiddenField hdfLookupid = grdLookup.Rows[e.RowIndex].FindControl("hdfLookupid") as HiddenField;
                    Label lbllkup_desc = grdLookup.Rows[e.RowIndex].FindControl("lbllkup_desc") as Label;
                    int afctrows;
                    objData = new MasterData();
                    afctrows = objData.Lookup_Activate(Convert.ToInt32(hdfLookupid.Value.Trim()), lbllkup_desc.Text.Trim());
                    grdfill();
                    divmsg.InnerHtml = "Record activated successfully.";
                }
            }
            catch (Exception ex)
            {
                showerror(ex);
            }
        }

        protected void grdLookup_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grdLookup.EditIndex = e.NewEditIndex;
                grdfill();
                grdLookup.FooterRow.Visible = false;
                DropDownList ddllkup_groupE = grdLookup.Rows[e.NewEditIndex].FindControl("ddllkup_groupE") as DropDownList;
                HiddenField hdflkup_group = grdLookup.Rows[e.NewEditIndex].FindControl("hdflkup_group") as HiddenField;
                CheckBox chkStatus = grdLookup.Rows[e.NewEditIndex].FindControl("chkStatus") as CheckBox;
                HiddenField hdfStatus = grdLookup.Rows[e.NewEditIndex].FindControl("hdfStatus") as HiddenField;
                // HiddenField hdfLookupsize = grdLookup.Rows[e.NewEditIndex].FindControl("hdfLookupsize") as HiddenField;
                TextBox txtlkup_descEdit = grdLookup.Rows[e.NewEditIndex].FindControl("txtlkup_descEdit") as TextBox;
                ddllkup_groupE.SelectedIndex = ddllkup_groupE.Items.IndexOf(ddllkup_groupE.Items.FindByText(hdflkup_group.Value.Trim()));
                switchddl(ddllkup_groupE);
                //if (ddllkup_groupE.SelectedIndex > 0)
                //{
                //    string groupvalue = ddllkup_groupE.SelectedItem.Value.Split('|').GetValue(0).ToString();
                //    int groupsize = Convert.ToInt32(ddllkup_groupE.SelectedItem.Value.Split('|').GetValue(1).ToString());
                //    txtlkup_descEdit.MaxLength = groupsize;
                //}
                if (hdfStatus.Value.Trim().ToUpper() == "Active".Trim().ToUpper())
                {
                    chkStatus.Checked = true;
                    //chkStatus.Enabled = false;
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

        protected void grdLookup_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox txtlkup_descEdit = grdLookup.Rows[e.RowIndex].FindControl("txtlkup_descEdit") as TextBox;
                HiddenField hdflkup_id = grdLookup.Rows[e.RowIndex].FindControl("hdflkup_id") as HiddenField;
                DropDownList ddllkup_groupE = grdLookup.Rows[e.RowIndex].FindControl("ddllkup_groupE") as DropDownList;
                CheckBox chkStatus = grdLookup.Rows[e.RowIndex].FindControl("chkStatus") as CheckBox;
                //if (ddllkup_groupE.SelectedIndex == 0)
                //{
                //    lblerrmsg.Text = "Please select lookup group";
                //    return;
                //}
                if (txtlkup_descEdit.Text.Length == 0)
                {
                    lblerrmsg.Text = "Lookup should not be empty";
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
                string groupvalue = ddllkup_groupE.SelectedItem.Value.Split('|').GetValue(0).ToString();
                int groupsize = Convert.ToInt32(ddllkup_groupE.SelectedItem.Value.Split('|').GetValue(1).ToString());
                if (groupsize < txtlkup_descEdit.Text.ToString().Length)
                {
                    lblerrmsg.Text = "Check lookup value length";
                    return;
                }
                TextInfo textInfo = cultureInfo.TextInfo;
                afctrows = objData.Lookup_Update(textInfo.ToTitleCase(txtlkup_descEdit.Text.Trim()), Convert.ToInt32(hdflkup_id.Value.Trim()), groupvalue, status);
                if (afctrows == 102)
                {
                    lblerrmsg.Text = "Lookup already exists.";
                    return;
                }

                grdLookup.EditIndex = -1;
                grdfill();

                divmsg.InnerHtml = "Record updated successfully.";
                (this.Page.Master as Site1).ClearModifyStatus();

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


                string id = this.ID;
                vslist.ValidationGroup = id;

                switch (id)
                {

                    case "lst_scheduletype":
                        group = "scheduletype";

                        break;
                    case "lst_pricescalestatus":
                        group = "pricescalestatus";

                        break;
                    case "lst_contractstatus":
                        group = "contractstatus";
                        break;
                    case "lst_offerstatus":
                        group = "offerstatus";
                        break;
                    case "lst_expensestatus":
                        group = "expensestatus";
                        break;
                    case "lst_memostatus":
                        group = "memostatus";
                        break;
                    case "lst_engagementstatus":
                        group = "engagementstatus";
                        break;
                    case "lst_employeetype":
                        group = "employeetype";
                        break;
                    case "lst_employeestatus":
                        group = "employeestatus";
                        break;
                    case "lst_Dealothers":
                        group = "DealOthers";
                        break;
                    case "lst_LocalOthers":
                        group = "localdocumentedothers";
                        break;
                    //case "lst_DocumentOthers":
                    //    group = "localdocumentedothers";
                    //    break;
                }

                dtlookup.Clear();
                dtlookup = new DataTable();
                objData = new MasterData();
                dtlookup = objData.GetLookupList(group);
                if (dtlookup.Rows.Count > 0)
                {
                    grdLookup.DataSource = dtlookup;
                    grdLookup.DataBind();


                }
                else
                {
                    foreach (DataColumn cl in dtlookup.Columns)
                    {
                        cl.AllowDBNull = true;
                    }
                    dtlookup.Rows.Add(dtlookup.NewRow());
                    grdLookup.DataSource = dtlookup;
                    grdLookup.DataBind();
                    int columncount = grdLookup.Rows[0].Cells.Count;
                    grdLookup.Rows[0].Cells.Clear();
                    grdLookup.Rows[0].Cells.Add(new TableCell());
                    grdLookup.Rows[0].Cells[0].ColumnSpan = columncount - 2;
                    grdLookup.Rows[0].Cells[0].Text = "No Records Found";
                    grdLookup.Columns[columncount - 2].Visible = false;
                }

                DropDownList ddltype = grdLookup.FooterRow.FindControl("ddllkup_group") as DropDownList;
                switchddl(ddltype);
                //<asp:ListItem Value="scheduletype|30" Text="Engagement Schedule Type"></asp:ListItem>
                //               <asp:ListItem Value="pricescalestatus|10" Text="Engagement Pricescale Status"></asp:ListItem>
                //               <asp:ListItem Value="contractstatus|10" Text="Engagement Contract Status"></asp:ListItem>
                //               <asp:ListItem Value="offerstatus|10" Text="Engagement Offer Status"></asp:ListItem>
                //               <asp:ListItem Value="expensestatus|15" Text="Engagement Expense Status"></asp:ListItem>
                //               <asp:ListItem Value="memostatus|10" Text="Engagement Memo Status"></asp:ListItem>
                //               <asp:ListItem Value="engagementstatus|10" Text="Engagement Engagement Status"></asp:ListItem>


                grdLookup.FooterRow.Visible = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void switchddl(DropDownList ddl)
        {
            string id = this.ID;
            switch (id)
            {

                case "lst_scheduletype":
                    group = "scheduletype";
                    ddl.SelectedItem.Text = "Engagement Schedule Type";
                    ddl.SelectedItem.Value = "scheduletype|30";
                    break;
                case "lst_pricescalestatus":
                    ddl.SelectedItem.Text = "Engagement Pricescale Status";
                    ddl.SelectedItem.Value = "pricescalestatus|10";
                   
                    break;
                case "lst_contractstatus":
                    ddl.SelectedItem.Text = "Engagement Contract Status";
                    ddl.SelectedItem.Value = "contractstatus|30";
                    break;
                case "lst_offerstatus":
                    ddl.SelectedItem.Text = "Engagement Offer Status";
                    ddl.SelectedItem.Value = "offerstatus|10";
                    break;
                case "lst_expensestatus":
                    ddl.SelectedItem.Text = "Engagement Expense Status";
                    ddl.SelectedItem.Value = "expensestatus|15";
                    break;
                case "lst_memostatus":
                    ddl.SelectedItem.Text = "Engagement Memo Status";
                    ddl.SelectedItem.Value = "memostatus|30";
                    break;
                case "lst_engagementstatus":
                    ddl.SelectedItem.Text = "Engagement Engagement Status";
                    ddl.SelectedItem.Value = "engagementstatus|10";
                    break;
                case "lst_employeetype":
                    ddl.SelectedItem.Text = "Employee Type";
                    ddl.SelectedItem.Value = "employeetype|15";
                    break;
                case "lst_employeestatus":
                    ddl.SelectedItem.Text = "Employee Status";
                    ddl.SelectedItem.Value = "employeestatus|15";
                    break;
                case "lst_Dealothers":
                    ddl.SelectedItem.Text = "Deal Others";
                    ddl.SelectedItem.Value = "DealOthers|30";
                    break;
                case "lst_LocalOthers":
                    ddl.SelectedItem.Text = "Local & Documented Others";
                    ddl.SelectedItem.Value = "localdocumentedothers|30";
                    break;
                //case "lst_DocumentOthers":
                //    ddl.SelectedItem.Text = "Document Others";
                //    ddl.SelectedItem.Value = "DocumentOthers|30";
                //    ddl.CssClass = "my_select_box";
                //    break;
            }
        }

        protected void ddllkup_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtlkup_desc = grdLookup.FooterRow.FindControl("txtlkup_desc") as TextBox;
                DropDownList ddllkup_group = grdLookup.FooterRow.FindControl("ddllkup_group") as DropDownList;
                string prvstext = txtlkup_desc.Text.Trim();
                if (ddllkup_group.SelectedIndex > 0)
                {
                    int groupsize = Convert.ToInt32(ddllkup_group.SelectedItem.Value.Split('|').GetValue(1).ToString());
                    int prvtextlen = prvstext.Length;
                    if (prvtextlen > groupsize)
                    {
                        int diflen = prvtextlen - groupsize;
                        txtlkup_desc.Text = txtlkup_desc.Text.Remove(groupsize, diflen);

                    }
                    txtlkup_desc.MaxLength = groupsize;
                }
            }
            catch (Exception ex)
            {
                showerror(ex);
            }
        }

        protected void ddllkup_groupE_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.Parent.Parent;

                TextBox txtlkup_desc = row.FindControl("txtlkup_descEdit") as TextBox;
                DropDownList ddllkup_group = row.FindControl("ddllkup_groupE") as DropDownList;
                string prvstext = txtlkup_desc.Text.Trim();
                if (ddllkup_group.SelectedIndex > 0)
                {
                    int groupsize = Convert.ToInt32(ddllkup_group.SelectedItem.Value.Split('|').GetValue(1).ToString());
                    int prvtextlen = prvstext.Length;
                    if (prvtextlen > groupsize)
                    {
                        int diflen = prvtextlen - groupsize;
                        txtlkup_desc.Text = txtlkup_desc.Text.Remove(groupsize, diflen);

                    }
                    txtlkup_desc.MaxLength = groupsize;
                }
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

        protected void grdLookup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                HiddenField hf = (HiddenField)this.Page.Master.FindControl("hdnreqlist");
                //DropDownList ddllkup_group = (DropDownList)this.Page.Master.FindControl("ddllkup_group");
                //ddllkup_group.CssClass = "my_select_box";
                if (hf.Value.Split(',').Length < 10)
                    ((HiddenField)this.Page.Master.FindControl("hdnreqlist")).Value += "," + e.Row.FindControl("txtlkup_desc").ClientID.ToString();
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