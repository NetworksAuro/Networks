using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Diagnostics;
using MasterDataLayer;
namespace NTOS
{
    public partial class Docx : System.Web.UI.UserControl
    {
        MasterData objmst = new MasterData();
        Label lblerrmsg;



        protected void Page_Load(object sender, EventArgs e)
        {
            ContentPlaceHolder MainContent = (ContentPlaceHolder)this.Page.Form.FindControl("MainContent");
            lblerrmsg = (Label)MainContent.FindControl("lblerrmsg");
            
          
            if (!IsPostBack && ((Request.QueryString.Count == 0) || string.IsNullOrEmpty(Request.QueryString["personalid"])))
            {
                try
                {
                    if (hdntablename.Value.ToUpper() == "A".ToUpper())
                    {
                        createtemptable();
                        binddocx();
                    }
                }
                catch (Exception ex)
                {
                    lblerrmsg.Text = "Error: " + ex.Message;
                }
            }
        }
        public void createtemptable()
        {
            if (Request.QueryString.Count == 0)
            {
                hdntableid.Value = "0";
                hdntablename.Value = "A";
            }
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn();
            dc.ColumnName = "tempid";
            dc.DataType = typeof(int);
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            dt.Columns.Add(dc);
            dt.Columns.Add("DocFilename", typeof(string));
            dt.Columns.Add("DocFilepath", typeof(string));
            dt.Columns.Add("docx_id", typeof(string));
            dt.Columns["docx_id"].DefaultValue = "0";
            ViewState["tempdocx"] = dt;
        }
        public void binddocx()
        {
            DataTable dt = (DataTable)ViewState["tempdocx"];
            RepDetails.DataSource = dt;
            RepDetails.DataBind();
            Panel pnlfooter = RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("pnlfooter") as Panel;
            if (dt.Rows.Count > 0)
            {
                pnlfooter.Visible = false;
                //RepDetails.FooterTemplate = null;
            }
        }
        public string GetNetworkpath(string drivename)
        {
            objmst = new MasterData();
            DataTable dt = new DataTable();
            dt = objmst.GetLookupList("folderpath");

            if (dt.Rows.Count > 1)
            {
                dt = dt.Select("lkup_desc like '" + drivename + "%'").CopyToDataTable();
            }


            return dt.Rows[0]["lkup_desc"].ToString().Split('|').GetValue(1).ToString();
        }
        protected void imgbtnAttach_Click(object sender, ImageClickEventArgs e)
        {
            string filepath = "", network_path = "";
            try
            {
                FileUpload fupdocx = RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("fupdocx") as FileUpload;
                if (fupdocx != null)
                {
                    DataTable dt = (DataTable)ViewState["tempdocx"];
                    DataRow dr;
                    dr = dt.NewRow();
                    dr["DocFilename"] = fupdocx.FileName;
                    if (fupdocx.PostedFile.FileName.ToString().StartsWith("\\") != true)
                    {
                        string drivename = fupdocx.PostedFile.FileName.Split(':').GetValue(0).ToString();
                        if (drivename.ToUpper() != "U")
                            network_path = "\\NWPFS\\public";
                        else
                            network_path = GetNetworkpath(drivename);
                        // string filepath = "file:\\\\NWPFS\\NTOS Database Project" + fupdocx.PostedFile.FileName.Split(':').GetValue(1).ToString();

                        filepath = "file:\\" + network_path + fupdocx.PostedFile.FileName.Split(':').GetValue(1).ToString();
                    }
                    else
                    {
                        filepath = fupdocx.PostedFile.FileName.ToString();
                    }
                    dr["DocFilepath"] = filepath;
                    dr["docx_id"] = "0";
                    dt.Rows.Add(dr);
                    ViewState["tempdocx"] = dt;
                    binddocx();
                }
            }
            catch (Exception ex)
            {
                lblerrmsg.Text = "Error: " + ex.Message;
            }
        }
        protected void lnkbtndocxname_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem itm = (RepeaterItem)lnk.Parent;
            HiddenField hdnpath = (HiddenField)itm.FindControl("hdnpath");
            Response.ContentType = "image/jpeg";
            Response.AppendHeader("Content-Disposition", "attachment; filename=MyFile.jpeg");
            Response.TransmitFile(hdnpath.Value);

            Response.End();
            string path = hdnpath.Value;
            if (File.Exists(path))
            {
                Process.Start(@"explorer.exe", path);

            }
        }
        public void SaveDocx(Int32 tableid, string tablename)
        {
            DataTable dt = (DataTable)ViewState["tempdocx"];
            RepDetails.DataSource = dt;
            MasterData objmst = new MasterData();
            string filename, filepath;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["docx_id"]) == 0)
                {
                    filename = Convert.ToString(dt.Rows[i]["DocFilename"]);
                    filepath = Convert.ToString(dt.Rows[i]["DocFilepath"]);
                    objmst.Docx_Insert(tablename, tableid, filename, filepath);
                }
            }
        }
        public void GetDocxDetails(Int32 tableid, string tablename)
        {
            hdntableid.Value = tableid.ToString();
            hdntablename.Value = tablename;
            MasterData objmst = new MasterData();
            DataTable dt = new DataTable();
            dt = objmst.GetDocxDetails(tableid, tablename);
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
            ViewState["tempdocx"] = dt;
            binddocx();
        }
        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            Panel pnlfooter = RepDetails.Controls[RepDetails.Controls.Count - 1].FindControl("pnlfooter") as Panel;
            pnlfooter.Visible = true;
            pnlfooter.Focus();
        }
        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["tempdocx"];
                foreach (RepeaterItem gr in RepDetails.Items)
                {
                    CheckBox chkdelete = (CheckBox)gr.FindControl("chkdelete");
                    HiddenField hdntempid = (HiddenField)gr.FindControl("hdntempid");
                    HiddenField hdndocxid = (HiddenField)gr.FindControl("hdndocxid");
                    if (chkdelete.Checked == true)
                    {
                        DataRow[] dr;
                        dr = dt.Select("tempid='" + hdntempid.Value + "'", "");
                        dt.Rows.Remove(dr[0]); ;

                        if (hdntableid.Value != "0")
                        {
                            MasterData objmst = new MasterData();
                            objmst.Docx_Delete(Convert.ToInt32(hdndocxid.Value));
                        }
                    }
                }

                ViewState["tempdocx"] = dt;
                binddocx();
            }
            catch (Exception ex)
            {
                lblerrmsg.Text = "Error: " + ex.Message;
            }
        }
    }
}