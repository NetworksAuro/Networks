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
    public partial class City1 : System.Web.UI.Page
    {
        MasterData objData = new MasterData();
        DataTable dtcity = new DataTable();
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
                    lbl_msg.Text = "City List";
                    grdfill();
                    (this.Master as Site1).hide();
                }
                catch (Exception ex)
                {
                    showerror(ex);
                    lblerrmsg.Text = "Error: " + ex.Message;
                }

            }
        }
        protected void grdCity_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdCity.EditIndex = -1;
            grdfill();
        }
        protected void grdCity_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                ImageButton lnkDelete = grdCity.Rows[e.RowIndex].FindControl("lnkDelete") as ImageButton;
                HiddenField hdfStateid = grdCity.Rows[e.RowIndex].FindControl("hdfStateid") as HiddenField;

                if (lnkDelete.CommandArgument.ToString().Trim().ToUpper() == "Inactive".Trim().ToUpper())
                {
                    HiddenField hdfCityid = grdCity.Rows[e.RowIndex].FindControl("hdfCityid") as HiddenField;
                    int afctrows;
                    objData = new MasterData();
                    afctrows = objData.City_Delete(Convert.ToInt32(hdfCityid.Value.Trim()));
                    if (afctrows == 103)
                    {
                        lblerrmsg.Text = "City can’t be deleted as is in use by an engagement";
                        return;
                    }

                    grdfill();
                    divmsg.InnerHtml = "Record deleted successfully.";
                }
                else
                {
                    HiddenField hdfCityid = grdCity.Rows[e.RowIndex].FindControl("hdfCityid") as HiddenField;
                    int afctrows;
                    objData = new MasterData();
                    afctrows = objData.City_Activate(Convert.ToInt32(hdfCityid.Value.Trim()));
                    grdfill();
                    divmsg.InnerHtml = "Record activated successfully.";

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
        protected void grdCity_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grdCity.EditIndex = e.NewEditIndex;
                grdfill();
                grdCity.FooterRow.Visible = false;
                DropDownList ddlStateE = grdCity.Rows[e.NewEditIndex].FindControl("ddlStateE") as DropDownList;
                DropDownList ddlCountryE = grdCity.Rows[e.NewEditIndex].FindControl("ddlCountryE") as DropDownList;
                HiddenField hdfStateid = grdCity.Rows[e.NewEditIndex].FindControl("hdfStateid") as HiddenField;
                HiddenField hdfCountryid = grdCity.Rows[e.NewEditIndex].FindControl("hdfCountryid") as HiddenField;
                CheckBox chkStatus = grdCity.Rows[e.NewEditIndex].FindControl("chkStatus") as CheckBox;
                HiddenField hdfStatus = grdCity.Rows[e.NewEditIndex].FindControl("hdfStatus") as HiddenField;
                TextBox txtZipEdit = grdCity.Rows[e.NewEditIndex].FindControl("txtZipEdit") as TextBox;
                if (hdfStatus.Value.Trim().ToUpper() == "Active".Trim().ToUpper())
                {
                    chkStatus.Checked = true;
                    //chkStatus.Enabled = false;
                }
                else
                {
                    chkStatus.Checked = false;
                }
                getcountry(ddlCountryE);
                ddlCountryE.SelectedIndex = ddlCountryE.Items.IndexOf(ddlCountryE.Items.FindByValue(hdfCountryid.Value.ToString()));
                getstate(ddlStateE, ddlCountryE);
                ddlStateE.SelectedIndex = ddlStateE.Items.IndexOf(ddlStateE.Items.FindByValue(hdfStateid.Value.ToString()));
                DataTable dtzip = new DataTable();
                dtzip.Columns.Add("ZipCode", typeof(string));
                string[] zip = txtZipEdit.Text.Split(',');
                string ziprow = "";
                for (int i = 0; i < 300; i++)
                {
                    ziprow = (i < zip.Length) ? zip[i] : "";
                    dtzip.Rows.Add(ziprow);
                }
                Repeater repzip = (Repeater)grdCity.Rows[e.NewEditIndex].FindControl("repzipE");
                repzip.DataSource = dtzip;
                repzip.DataBind();
            }
            catch (Exception ex)
            {
                showerror(ex);
            }
        }

        protected void grdCity_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox txtCityEdit = grdCity.Rows[e.RowIndex].FindControl("txtCityEdit") as TextBox;
                TextBox txtZipEdit = grdCity.Rows[e.RowIndex].FindControl("txtZipEdit") as TextBox;
                HiddenField hdfCityid = grdCity.Rows[e.RowIndex].FindControl("hdfCityid") as HiddenField;
                DropDownList ddlStateE = grdCity.Rows[e.RowIndex].FindControl("ddlStateE") as DropDownList;
                CheckBox chkStatus = grdCity.Rows[e.RowIndex].FindControl("chkStatus") as CheckBox;
                if (ddlStateE.SelectedIndex == 0)
                {
                    lblerrmsg.Text = "Please select state";
                    return;
                }
                if (txtCityEdit.Text.Length == 0)
                {
                    lblerrmsg.Text = "City should not be empty";
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
                Repeater repzipF = grdCity.Rows[e.RowIndex].FindControl("repzipE") as Repeater;
                string zipcodelist = GetZipList(repzipF);
                afctrows = objData.City_Update(textInfo.ToTitleCase(txtCityEdit.Text.Trim()), Convert.ToInt32(hdfCityid.Value.Trim()), Convert.ToInt32(ddlStateE.SelectedItem.Value.Trim()), zipcodelist, status);
                if (afctrows == 102)
                {
                    lblerrmsg.Text = "City already exists.";
                    return;
                }
                else if (afctrows == 103)
                {
                    lblerrmsg.Text = "City can’t be deleted as is in use by an engagement";
                    return;
                }
                grdCity.EditIndex = -1;
                grdfill();

                divmsg.InnerHtml = "Record updated successfully.";
                (this.Master as Site1).ClearModifyStatus();
            }
            catch (Exception ex)
            {
                showerror(ex);
            }
        }
        public string GetZipList(Repeater rep)
        {
            string Ziplist = "";
            foreach (RepeaterItem ri in rep.Items)
            {
                TextBox txtid = (TextBox)ri.FindControl("txtzipcode");
                Ziplist += txtid.Text + ",";
            }
            return Ziplist.TrimEnd(',');
        }
        protected void grdCity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Save")
                {
                    TextInfo textInfo = cultureInfo.TextInfo;
                    DropDownList ddlState = grdCity.FooterRow.FindControl("ddlState") as DropDownList;
                    TextBox txtCityName = grdCity.FooterRow.FindControl("txtCityName") as TextBox;
                    TextBox txtZip = grdCity.FooterRow.FindControl("txtZip") as TextBox;
                    Repeater repzipF = grdCity.FooterRow.FindControl("repzipF") as Repeater;
                    int afctrows;
                    objData = new MasterData();
                    string zipcodelist = GetZipList(repzipF);
                    afctrows = objData.City_Insert(textInfo.ToTitleCase(txtCityName.Text.Trim()), Convert.ToInt32(ddlState.SelectedItem.Value), zipcodelist);
                    if (afctrows == 101)
                    {
                        lblerrmsg.Text = "City already exists.";
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


                dtcity = new DataTable();
                objData = new MasterData();
                dtcity = objData.Getcitydetails(0, txtSearch.Text.Trim());
                if (dtcity.Rows.Count > 0)
                {
                    grdCity.DataSource = dtcity;
                    grdCity.DataBind();
                }
                else
                {
                    foreach (DataColumn cl in dtcity.Columns)
                    {
                        cl.AllowDBNull = true;
                    }
                    dtcity.Rows.Add(dtcity.NewRow());
                    grdCity.DataSource = dtcity;
                    grdCity.DataBind();
                    int columncount = grdCity.Rows[0].Cells.Count;
                    grdCity.Rows[0].Cells.Clear();
                    grdCity.Rows[0].Cells.Add(new TableCell());
                    grdCity.Rows[0].Cells[0].ColumnSpan = columncount - 2;
                    grdCity.Rows[0].Cells[0].Text = "No Records Found";
                    grdCity.Columns[columncount - 2].Visible = false;
                }
                grdCity.FooterRow.Visible = true;
                DropDownList ddlcountry = grdCity.FooterRow.FindControl("ddlCountry") as DropDownList;
                getcountry(ddlcountry);
                ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByText("USA"));
                DropDownList ddlState = grdCity.FooterRow.FindControl("ddlState") as DropDownList;
                getstate(ddlState, ddlcountry);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void getstate(DropDownList ddl, DropDownList ddl1)
        {
            if (ddl1.SelectedIndex > 0)
            {
                DataTable dtcountry = new DataTable();
                objData = new MasterData();
                dtcountry = objData.Getstatelist(Convert.ToInt32(ddl1.SelectedItem.Value));
                ddl.DataSource = dtcountry;
                ddl.DataTextField = "state_name";
                ddl.DataValueField = "state_id";
                ddl.DataBind();
            }
            ddl.Items.Insert(0, "-Select-");

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

            //DropDownList ddlState = grdCity.FooterRow.FindControl("ddlState") as DropDownList;
            //DropDownList ddlcountry = grdCity.FooterRow.FindControl("ddlCountry") as DropDownList;
            //getstate(ddlState, ddlcountry);
        }
        protected void grdCity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCity.PageIndex = e.NewPageIndex;
            try
            {
                grdfill();
            }
            catch (Exception ex)
            {
                showerror(ex);
            }

        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlState = grdCity.FooterRow.FindControl("ddlState") as DropDownList;
                DropDownList ddlcountry = grdCity.FooterRow.FindControl("ddlCountry") as DropDownList;
                if (ddlcountry.SelectedIndex == 0)
                {
                    ddlState.Items.Clear();
                    ddlState.Items.Insert(0, "-Select-");
                    return;
                }
                getstate(ddlState, ddlcountry);
            }
            catch (Exception ex)
            {
                showerror(ex);
            }

        }

        protected void ddlCountryE_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlState = grdCity.Rows[grdCity.EditIndex].FindControl("ddlStateE") as DropDownList;
                DropDownList ddlCountryE = grdCity.Rows[grdCity.EditIndex].FindControl("ddlCountryE") as DropDownList;
                if (ddlCountryE.SelectedIndex == 0)
                {
                    ddlState.Items.Clear();
                    ddlState.Items.Insert(0, "-Select-");
                    return;
                }
                getstate(ddlState, ddlCountryE);
            }
            catch (Exception ex)
            {
                showerror(ex);
            }

        }

        protected void grdCity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((HiddenField)this.Master.FindControl("hdnreqlist")).Value = e.Row.FindControl("ddlCountry").ClientID + "," + e.Row.FindControl("ddlState").ClientID + "," + e.Row.FindControl("txtCityName").ClientID;
                DataTable dtzip = new DataTable();
                dtzip.Columns.Add("ZipCode", typeof(string));
                for (int i = 0; i < 300; i++)
                {
                    dtzip.Rows.Add("");
                }
                Repeater repzip = (Repeater)e.Row.FindControl("repzipF");
                repzip.DataSource = dtzip;
                repzip.DataBind();
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

        //protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        //{
        //    dtcity = new DataTable();
        //    objData = new MasterData();
        //    List<string> lscity = new List<string>();
        //    lscity = objData.searchcity(txtSearch.Text.Trim());
        //    grdCity.DataSource = lscity;
        //    grdCity.DataBind();
        //}

         protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            //dtcity = new DataTable();
            //objData = new MasterData();
            //List<string> lscity = new List<string>();
            //lscity = objData.searchcity(txtSearch.Text.Trim());
            //grdCity.DataSource = lscity;
            //grdCity.DataBind();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            grdfill();
        }

        protected void btnclearsearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            grdfill();
        }

    }

    }
