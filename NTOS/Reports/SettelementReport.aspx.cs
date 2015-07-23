using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShowDataLayer;
using MasterDataLayer;
using ReportDataLayer;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Net;
namespace NTOS.Reports
{
    public partial class SettelementReport : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        MasterData objshow = new MasterData();
        ReportData objrpt = new ReportData();
        CommonFunction.CommonFun objcf = new CommonFunction.CommonFun();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadShow();
                ((ImageButton)this.Master.FindControl("imgbtnsave")).Visible = false;
                ((HiddenField)this.Master.FindControl("hdnreqlist")).Value = (ddlShow.ClientID + "," + ddlCity.ClientID + "," + ddlCreateddate.ClientID + "," + ddlweek.ClientID);
            }
        }
        public void loadShow()
        {
            try
            {
                dt = objrpt.GetEngtReportParameters("S", null, null, null);
                objcf.FillDropDownList(ddlShow, dt, "show_name", "show_id");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void loadCities()
        {
            try
            {
                clear("c");
                dt = objrpt.GetEngtReportParameters("C", Convert.ToInt32(ddlShow.SelectedItem.Value), null, null);
                objcf.FillDropDownList(ddlCity, dt, "City_Name", "City_id");
                if (dt.Rows.Count == 1)
                {
                    ddlCity.SelectedIndex = 1;
                    loadVenues();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void loadVenues()
        {
            objshow = new MasterData();
            DataTable dt = new DataTable();
            dt = objrpt.GetEngtReportParameters("V", Convert.ToInt32(ddlShow.SelectedItem.Value), Convert.ToInt32(ddlCity.SelectedItem.Value), null);
            objcf.FillDropDownList(ddlVenue, dt, "VENUE_NAME", "VENUE_ID");
            if (dt.Rows.Count == 1)
            {
                ddlVenue.SelectedIndex = 1;
            }
            loadCreateddate();
        }
        public void loadCreateddate()
        {

            objshow = new MasterData();
            objrpt = new ReportData();
            DataTable dt = new DataTable();
            Nullable<int> venueid = null;
            venueid = (ddlVenue.SelectedIndex > 0) ? Convert.ToInt32(ddlVenue.SelectedItem.Value) : venueid;
            dt = objrpt.GetEngtReportParameters("D", Convert.ToInt32(ddlShow.SelectedItem.Value), Convert.ToInt32(ddlCity.SelectedItem.Value), venueid);
            objcf.FillDropDownList(ddlCreateddate, dt, "ENGTSTARTDATE", "ENGAGEMENTID");
            if (dt.Rows.Count == 1)
            {
                ddlCreateddate.SelectedIndex = 1;
                LoadEnddate();

            }
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clearddl(ddlweek);
            loadVenues();

        }

        protected void ddlVenue_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCreateddate();
        }

        protected void ddlShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear("s");
            loadCities();
        }
        public void Clearddl(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
        }
        public void clear(string type)
        {
            lblEngtEndDate.Text = string.Empty;
            Clearddl(ddlweek);
            switch (type)
            {
                case "s":
                    {
                        Clearddl(ddlCreateddate);
                        Clearddl(ddlVenue);
                        break;
                    }
                case "c":
                    {
                        Clearddl(ddlCreateddate);
                        Clearddl(ddlVenue);
                        break;
                    }
            }
        }
        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            ShowReport();
            trexcel.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "anc();", true);
        }
        public void LoadEnddate()
        {
            lblEngtEndDate.Text = string.Empty;
            objshow = new MasterData();
            objrpt = new ReportData();
            DataTable dt = new DataTable();
            Nullable<int> venueid = null;
            venueid = (ddlVenue.SelectedIndex > 0) ? Convert.ToInt32(ddlVenue.SelectedItem.Value) : venueid;
            dt = objrpt.GetEngtReportParameters("E", Convert.ToInt32(ddlCreateddate.SelectedItem.Value), venueid, venueid);
            if (dt.Rows.Count > 0)
            {
                lblEngtEndDate.Text = dt.Rows[0]["ENGTENDDATE"].ToString();
            }
            if (dt.Rows.Count == 1)
            {
                LoadWeek();
            }
        }
        public void LoadWeek()
        {
            PriceScaleDataLayer.PriceScaleData objps = new PriceScaleDataLayer.PriceScaleData();
            dt = objps.GetScheduleDayTime(Convert.ToInt32(ddlCreateddate.SelectedItem.Value));
            //int nofweeks = NumberOfWeeks(Convert.ToDateTime(ddlCreateddate.SelectedItem.Text), Convert.ToDateTime(lblEngtEndDate.Text));
            int nofweeks = 0;
            if (dt.Rows.Count > 0)
            {
                nofweeks = Convert.ToInt32(dt.Rows[0]["WeekCount"]);
            }
            ddlweek.Items.Clear();
            ddlweek.Items.Add(new ListItem("--Select-- ", "0"));
            for (int i = 0; i < nofweeks; i++)
            {
                ddlweek.Items.Add(new ListItem("Week " + (i + 1).ToString(), (i + 1).ToString()));
            }
            if (nofweeks == 1)
            {
                ddlweek.SelectedIndex = 1;
            }
        }
        protected void ddlCreateddate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEnddate(); LoadWeek();
        }
        public static int NumberOfWeeks(DateTime dateFrom, DateTime dateTo)
        {
            TimeSpan Span = dateTo.Subtract(dateFrom);

            if (Span.Days <= 7)
            {
                if ((int)dateFrom.DayOfWeek > (int)dateTo.DayOfWeek)
                {
                    return 2;
                }

                return 1;
            }

            int Days = Span.Days - 7 + (int)dateFrom.DayOfWeek;
            int WeekCount = 1;
            int DayCount = 0;

            for (WeekCount = 1; DayCount < Days; WeekCount++)
            {
                DayCount += 7;
            }

            return WeekCount;
        }


        public void ShowReport()
        {
            DataSet ds = new DataSet();
            int wkcount = Convert.ToInt32(ddlweek.Items.Count - 1);
            ds = objrpt.GetSettlementReport_records(Convert.ToInt32(ddlCreateddate.SelectedItem.Value), Convert.ToInt32(ddlweek.SelectedItem.Value), wkcount);
            Template(ds);
            //string serverpath = this.MapPath(".");
            //string newfile = @"" + serverpath + "\\ExcelReports\\Settlement_Reports_" + System.DateTime.Now.ToString("ddmmyyyy_hh_mm_ss") + ".xlsx";
            //if (File.Exists(newfile) == false)
            //{
            //    File.Copy(@"" + serverpath + "\\ExcelTemplates\\Settlement_Reports_Template.xlsx", newfile);
            //}
        }
        public void DeleteCreatedFiles(string cur_file, string folderpath)
        {
            string[] filePaths = Directory.GetFiles(folderpath);

            foreach (string filePath in filePaths)
            {
                try
                {

                    File.Delete(filePath);
                }
                catch (Exception)
                {

                    GC.Collect(); //kill object that keep the file. I think dispose will do the trick as well.
                    System.Threading.Thread.Sleep(500); //Wait for object to be killed.
                    //File.Delete(filePath); //File can be now deleted
                }
            }
        }
        public void Template(DataSet ds)
        {
            dt = new DataTable();
            dt = ds.Tables[0];
            string serverpath = this.MapPath(".");
            //string filename = "Settlement_Reports_" + System.DateTime.Now.ToString("ddmmyyyy_hh_mm_ss") + ".xls";
            string filename = "Settlement_Reports_" + System.DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".xls";
            //DeleteCreatedFiles(filename, @"" + serverpath + "\\ExcelReports\\");
            string newfile = @"" + serverpath + "\\ExcelReports\\" + filename;
            String ExcelPath = System.Configuration.ConfigurationManager.AppSettings["ExcelPath"].ToString();
            string newpath = ExcelPath + filename;
            //string newpath = "\\Reports\\ExcelReports\\" + filename;
            if (File.Exists(newfile) == false)
            {
                File.Copy(@"" + serverpath + "\\ExcelTemplates\\Settlement_Reports_Template.xlsx", newfile);


                Microsoft.Office.Interop.Excel.Application xlApp = null;
                Excel.Workbook xlWorkbook = null;
                Excel.Sheets xlSheets = null;
                Excel.Worksheet xlNewSheet = null;
                Excel.Worksheet xlCoverSheet = null;

                try
                {
                    xlApp = new Microsoft.Office.Interop.Excel.Application();

                    if (xlApp == null)
                        return;
                    xlWorkbook = xlApp.Workbooks.Open(newfile, 0, false, 5, "", "",
                            false, Excel.XlPlatform.xlWindows, "",
                            true, false, 0, true, false, false);

                    xlSheets = xlWorkbook.Sheets as Excel.Sheets;
                    xlNewSheet = (Excel.Worksheet)xlSheets[5];
                    xlCoverSheet = (Excel.Worksheet)xlSheets[1];
                    int rowCount = 2;
                    int colstart = dt.Columns["Schedule_Type"].Ordinal;
                    int i;
                    foreach (DataRow dr in dt.Rows)
                    {
                        rowCount += 1;
                        for (i = 1; i < dt.Columns.Count + 1; i++)
                        {
                            xlNewSheet.Cells[rowCount, (i + 1)] = dr[i - 1].ToString();
                        }
                        break;
                    }
                    int k = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (k == 0)
                        {
                            k++;
                            continue;
                        }
                        rowCount += 1;
                        for (i = colstart; i <= (dt.Columns.Count); i++)
                        {
                            xlNewSheet.Cells[rowCount, (i + 1)] = dr[i - 1].ToString();
                        }

                    }

                    rowCount = 17;
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        xlNewSheet.Cells[rowCount, (2)] = ds.Tables[1].Rows[0]["TOTALGROSSPERWK"].ToString();
                        xlNewSheet.Cells[rowCount, (3)] = ds.Tables[1].Rows[0]["SUM_PER_PS_SCALE"].ToString();
                    }
                   // objcf.HideDBData_XLSheets(xlSheets, "5");
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        write_coversheet_data(xlCoverSheet, ds.Tables[2]);
                    }
                    xlWorkbook.Save();
                    xlWorkbook.Close(Type.Missing, Type.Missing, Type.Missing);
                    xlApp.Quit();
                    GC.Collect();

                    foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
                    {
                        if (proc.MainWindowTitle.ToString() == "")
                        {
                            // proc.Kill();
                        }

                    }

                    //Response.Redirect(newfilepath);
                    string fName = newfile;
                    hdnfilepath.Value = newpath;
                    lnkexcel.HRef = newpath;
                }
                finally
                {

                    xlApp = null;
                }
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "anc();", true);
            }
        }
        public void write_coversheet_data(Excel.Worksheet cvrsheet, DataTable dtcvr)
        {
            cvrsheet.Cells[1, 1] = dtcvr.Rows[0]["Show_Name"].ToString();
            cvrsheet.Cells[3, 2] = dtcvr.Rows[0]["City_Name"].ToString();
            cvrsheet.Cells[4, 2] = dtcvr.Rows[0]["Venue_Name"].ToString();
            cvrsheet.Cells[5, 2] = dtcvr.Rows[0]["Presenter_Name"].ToString();
            cvrsheet.Cells[8, 1] = dtcvr.Rows[0]["EngtStartDate"].ToString();
            cvrsheet.Cells[8, 2] = dtcvr.Rows[0]["EngtEndDate"].ToString();
            cvrsheet.Cells[8, 3] = dtcvr.Rows[0]["NoOfPerformance"].ToString();
            cvrsheet.Cells[9, 9] = dtcvr.Rows[0]["VENUE_ADDRESS1"].ToString();
            cvrsheet.Cells[14, 9] = dtcvr.Rows[0]["VENUE_DELIVERY_DIRECTIONS"].ToString();
            int swk = 11, l = 0, rwid = 0;
            string[] conper = new string[7] { "pr", "vif", "BO", "MK", "om", "gm", "hd" };
            rwid = 20;
            for (l = 0; l < conper.Length; l++)
            {
                cvrsheet.Cells[rwid, 2] = dtcvr.Rows[0][conper[l] + "_pname"].ToString();
                cvrsheet.Cells[rwid, 8] = dtcvr.Rows[0][conper[l] + "_phone"].ToString();
                cvrsheet.Cells[rwid, 9] = dtcvr.Rows[0][conper[l] + "_email"].ToString();
                rwid++;
            }

            string colname = "", rec_flg_mark = "";
            string[] wkname = new string[7] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            #region write show schedule weeks
            for (int j = 1; j <= 6; j++)
            {
                for (int k = 0; k < 7; k++)
                {
                    colname = "W" + j.ToString() + "_" + wkname[k];
                    cvrsheet.Cells[swk + (k), j + 1] = dtcvr.Rows[0][colname].ToString();
                }

            }
            #endregion
            string[] flgnames = "cvr_s_cover_flag,cvr_grnty_flag,cvr_royalty_flag,cvr_ovrg_flag,cvr_s_summary_flag,cvr_venue_sett_flag,cvr_bo_sheet_flag,cvr_bo_statements_flag,cvr_lbr_bills_flag,cvr_musician_bills_flag,cvr_local_exp_invoice_flag,cvr_ad_flag,cvr_contact_flag".Split(',');
            string[] notenames = "cvr_s_cover_notes,cvr_grnty_notes,cvr_royalty_notes,cvr_ovrg_notes,cvr_s_summary_notes,cvr_venue_sett_notes,cvr_bo_sheet_notes,cvr_bo_statements_notes,cvr_lbr_bills_notes,cvr_musician_bills_notes,cvr_local_exp_invoice_notes,cvr_ad_notes,cvr_contact_notes".Split(',');
            rwid = 29;
            for (l = 0; l < 13; l++)
            {
                rec_flg_mark = (Convert.ToString(dtcvr.Rows[0][flgnames[l]]).ToLower() == "n") ? "NA" : "X";
                cvrsheet.Cells[rwid + l, 4] = rec_flg_mark.ToString();
                cvrsheet.Cells[rwid + l, 5] = dtcvr.Rows[0][notenames[l]].ToString();
            }
            DataTable dtch = new DataTable();

            if (dtcvr.Select("cvr_type='c'").Length > 0)
            {
                rwid = 44;
                dtch = dtcvr.Select("cvr_type='c'").CopyToDataTable();
                for (l = 0; l < dtch.Rows.Count && l < 15; l++)
                {
                    cvrsheet.Cells[rwid, 1] = dtch.Rows[l]["CVR_CHGS_DESC"].ToString();
                    cvrsheet.Cells[rwid, 4] = dtch.Rows[l]["CVR_CHGS_AMT"].ToString();
                    cvrsheet.Cells[rwid, 5] = dtch.Rows[l]["CVR_CHGS_CHECK"].ToString();
                    cvrsheet.Cells[rwid, 6] = dtch.Rows[l]["CVR_CHGS_NOTES"].ToString();
                    rwid++;
                }
            }
            if (dtcvr.Select("cvr_type='r'").Length > 0)
            {
                rwid = 62;
                dtch = dtcvr.Select("cvr_type='r'").CopyToDataTable();
                for (l = 0; l < dtch.Rows.Count && l < 15; l++)
                {
                    cvrsheet.Cells[rwid, 1] = dtch.Rows[l]["CVR_CHGS_DESC"].ToString();
                    cvrsheet.Cells[rwid, 4] = dtch.Rows[l]["CVR_CHGS_AMT"].ToString();
                    cvrsheet.Cells[rwid, 5] = dtch.Rows[l]["CVR_CHGS_NOTES"].ToString();
                    rwid++;
                }
            }
            cvrsheet.Cells[74, 1] = dtcvr.Rows[0]["EMAILLIST"].ToString();
        }
        public static string ServerMapPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }
        public static HttpResponse GetHttpResponse()
        {
            return HttpContext.Current.Response;
        }
        public static void DownLoadFileFromServer(string fileName)
        {
            //This is used to get Project Location.
            string filePath = ServerMapPath("ExcelReports\\" + fileName);
            //This is used to get the current response.
            HttpResponse res = GetHttpResponse();
            res.Clear();
            res.AppendHeader("content-disposition", "attachment; filename=" + fileName);
            res.ContentType = "application/octet-stream";
            res.WriteFile(filePath);
            res.Flush();
            res.End();
        }
        protected void imgExcel_Click(object sender, ImageClickEventArgs e)
        {
            DownLoadFileFromServer(hdnfilepath.Value);
        }

        protected void ddlweek_SelectedIndexChanged(object sender, EventArgs e)
        {
            trexcel.Visible = false;
        }
    }
}