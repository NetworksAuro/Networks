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
using System.DirectoryServices.AccountManagement;
using System.Collections;
namespace NTOS
{

    public partial class Users : System.Web.UI.Page
    {

        MasterData objData = new MasterData();
        DataTable dtuser = new DataTable();
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
                lbl_msg.Text = "User List";
                grdfill();
                (this.Master as Site1).hide();
            }
        }
        public void grdfill()
        {
            try
            {
                dtuser.Clear();
                dtuser = new DataTable();
                objData = new MasterData();
                string userrole = Convert.ToString(Session["userrole"]);
                dtuser = objData.Users_Get(userrole);
                if (dtuser.Rows.Count > 0)
                {
                    grdUsers.DataSource = dtuser;
                    grdUsers.DataBind();
                }
                else
                {
                    foreach (DataColumn cl in dtuser.Columns)
                    {
                        cl.AllowDBNull = true;
                    }
                    dtuser.Rows.Add(dtuser.NewRow());
                    grdUsers.DataSource = dtuser;
                    grdUsers.DataBind();
                    int columncount = grdUsers.Rows[0].Cells.Count;
                    grdUsers.Rows[0].Cells.Clear();
                    grdUsers.Rows[0].Cells.Add(new TableCell());
                    grdUsers.Rows[0].Cells[0].ColumnSpan = columncount - 2;
                    grdUsers.Rows[0].Cells[0].Text = "No Records Found";
                    grdUsers.Columns[columncount - 2].Visible = false;
                }
                grdUsers.FooterRow.Visible = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void grdUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdUsers.EditIndex = -1;
            grdfill();
        }
        public string GetDomainUserName(string username)
        {
            UserPrincipal up = null;
            string uname = "";
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {

                up = UserPrincipal.FindByIdentity(context, username);
                if (up != null)
                    uname = up.Name;
            }
            return uname;
        }
        protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Save")
                {
                    DropDownList ddlUserRoleF = grdUsers.FooterRow.FindControl("ddlUserRoleF") as DropDownList;
                    TextBox txtUserF = grdUsers.FooterRow.FindControl("txtUserF") as TextBox;

                    //if (IsUserExisiting(txtUserF.Text.ToString().Trim()) == false)
                    //{
                    //    lblerrmsg.Text = "Username does not exists in server membergroup.";
                    //    return;
                    //}

                    int afctrows;
                    objData = new MasterData();
                    TextInfo textInfo = cultureInfo.TextInfo;
                    string domain_username = GetDomainUserName(txtUserF.Text.Trim());
                    afctrows = objData.User_Insert(textInfo.ToTitleCase(txtUserF.Text.Trim()), Convert.ToString(ddlUserRoleF.SelectedItem.Value.Trim()), domain_username);
                    if (afctrows == 101)
                    {
                        lblerrmsg.Text = "User already exists.";
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
        //public bool IsUserExisiting(string sUserName)
        //{
        //    if (GetUser(sUserName) == null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
        //public UserPrincipal GetUser(string UserName)
        //{
        //    //const string Domain = ContextType.Domain;
        //    //const string Username = "sanderso";

        //    PrincipalContext principalContext = new PrincipalContext(ContextType.Domain);
        //    UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(principalContext, UserName);
        //    //UserPrincipal oUserPrincipal = UserPrincipal.FindByIdentity(oPrincipalContext, sUserName);
        //    return userPrincipal;
        //}
        protected void grdUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                ImageButton lnkDelete = grdUsers.Rows[e.RowIndex].FindControl("lnkDelete") as ImageButton;
                HiddenField hdfusersid = grdUsers.Rows[e.RowIndex].FindControl("hdfusersid") as HiddenField;

                if (lnkDelete.CommandArgument.ToString().Trim().ToUpper() == "Inactive".Trim().ToUpper())
                {

                    int afctrows;
                    objData = new MasterData();
                    afctrows = objData.User_Delete(Convert.ToInt32(hdfusersid.Value.Trim()));
                    grdfill();
                    divmsg.InnerHtml = "Record deleted successfully.";
                    (this.Master as Site1).ClearModifyStatus();
                }
                else
                {
                    int afctrows;
                    objData = new MasterData();
                    afctrows = objData.User_Activate(Convert.ToInt32(hdfusersid.Value.Trim()));
                    grdfill();
                    divmsg.InnerHtml = "Record activated successfully.";
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void grdUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grdUsers.EditIndex = e.NewEditIndex;
                grdfill();
                grdUsers.FooterRow.Visible = false;
                DropDownList ddlUserRole = grdUsers.Rows[e.NewEditIndex].FindControl("ddlUserRole") as DropDownList;
                HiddenField hdfUserRole = grdUsers.Rows[e.NewEditIndex].FindControl("hdfUserRole") as HiddenField;
                CheckBox chkStatus = grdUsers.Rows[e.NewEditIndex].FindControl("chkStatus") as CheckBox;
                HiddenField hdfStatus = grdUsers.Rows[e.NewEditIndex].FindControl("hdfStatus") as HiddenField;
                if (hdfStatus.Value.Trim().ToUpper() == "Active".Trim().ToUpper())
                {
                    chkStatus.Checked = true;
                    // chkStatus.Enabled = false;
                }
                else
                {
                    chkStatus.Checked = false;
                }
                string userrole = Convert.ToString(Session["userrole"]);
                if (userrole.ToLower() == "admin")
                {
                    ddlUserRole.Items.RemoveAt(ddlUserRole.Items.IndexOf(ddlUserRole.Items.FindByValue("superadmin")));
                }
                ddlUserRole.SelectedIndex = ddlUserRole.Items.IndexOf(ddlUserRole.Items.FindByValue(hdfUserRole.Value.ToString().Trim().Replace(" ", "").ToLower()));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void grdUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox txtUserE = grdUsers.Rows[e.RowIndex].FindControl("txtUserE") as TextBox;
                HiddenField hdfusersid = grdUsers.Rows[e.RowIndex].FindControl("hdfusersid") as HiddenField;
                DropDownList ddlUserRole = grdUsers.Rows[e.RowIndex].FindControl("ddlUserRole") as DropDownList;
                CheckBox chkStatus = grdUsers.Rows[e.RowIndex].FindControl("chkStatus") as CheckBox;
                if (txtUserE.Text.Length == 0)
                {
                    lblerrmsg.Text = "User Name should not be empty";
                    return;
                }
                if (ddlUserRole.SelectedIndex == 0)
                {
                    lblerrmsg.Text = "Please select User Role";
                    return;
                }

                //if (IsUserExisiting(txtUserE.Text.ToString().Trim()) == false)
                //{
                //    lblerrmsg.Text = "Username does not exists in server membergroup.";
                //    return;
                //}
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
                string domain_username = GetDomainUserName(txtUserE.Text.Trim());
                afctrows = objData.User_Update(textInfo.ToTitleCase(txtUserE.Text.Trim()), Convert.ToInt32(hdfusersid.Value.Trim()), Convert.ToString(ddlUserRole.SelectedItem.Value.Trim()), status, domain_username);
                if (afctrows == 101)
                {
                    lblerrmsg.Text = "Username already exists.";
                    return;
                }
                grdUsers.EditIndex = -1;
                grdfill();
                divmsg.InnerHtml = "Record updated successfully.";
                (this.Master as Site1).ClearModifyStatus();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void grdUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUsers.PageIndex = e.NewPageIndex;
            grdfill();
        }
        protected void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((HiddenField)this.Master.FindControl("hdnreqlist")).Value = e.Row.FindControl("txtUserF").ClientID + "," + e.Row.FindControl("ddlUserRoleF").ClientID;
                string userrole = Convert.ToString(Session["userrole"]);
                if (userrole.ToLower() == "admin")
                {
                    DropDownList ddlUserRoleF = (DropDownList)e.Row.FindControl("ddlUserRoleF");
                    ddlUserRoleF.Items.RemoveAt(ddlUserRoleF.Items.IndexOf(ddlUserRoleF.Items.FindByValue("superadmin")));
                }
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