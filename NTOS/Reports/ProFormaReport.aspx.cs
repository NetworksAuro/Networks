using MasterDataLayer;
using ReportDataLayer;
using System;
using System.Data;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Web.UI.WebControls;

namespace NTOS.Reports
{
    public partial class ProFormaReport : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        MasterData objshow = new MasterData();
        ReportData objrpt = new ReportData();
        CommonFunction.CommonFun objcf = new CommonFunction.CommonFun();
        Label lbl_msg;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblerror.Text = string.Empty;
            if (!Page.IsPostBack)
            {
                (this.Master as Site1).HideNewbutton();
                   lbl_msg = (Label)this.Master.FindControl("lbl_headersite");
                   lbl_msg.Text = "Pro-Forma Report";
                loadShow();
                txtDate.Text = GetMondayOfWeek(System.DateTime.Now).ToShortDateString();
                ((ImageButton)this.Master.FindControl("imgbtnsave")).Visible = false;
                ((HiddenField)this.Master.FindControl("hdnreqlist")).Value = (ddlShow.ClientID + "," + txtDate.ClientID);
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
        public void LoadReport()
        {
            objrpt = new ReportData();
            int _showid = Convert.ToInt32(ddlShow.SelectedItem.Value);
            int _Month = Convert.ToDateTime(txtDate.Text).Month;
            int _Year = Convert.ToDateTime(txtDate.Text).Year;
            _Year = (_Month < 7) ? --_Year : _Year;
            dt = objrpt.GetProFormaReportData(_showid, _Year);
            if (dt.Rows.Count > 0)
            {
                trexcel.Visible = true;
                Template(dt, _Year);
            }
            else
            {
                lblerror.Text = "No records found!";
                trexcel.Visible = false;

            }
        }
        public void Template(DataTable dt, int cyear)
        {
            string serverpath = this.MapPath(".");
            string filename = "Pro-Forma_Report_" + System.DateTime.Now.ToString("ddMMyyyy_hh_mm_ss") + ".xlsx";
            string newfile = @"" + serverpath + "\\ExcelReports\\" + filename;
            String ExcelPath = System.Configuration.ConfigurationManager.AppSettings["ExcelPath"].ToString();
            string newpath = ExcelPath + filename;
            DataTable dt_cs, dt_os;
            dt_cs = (dt.Select("Show_id=" + ddlShow.SelectedItem.Value, "").Length > 0) ? dt.Select("Show_id=" + ddlShow.SelectedItem.Value, "").CopyToDataTable() : dt.Clone();
            dt_os = (dt.Select("Show_id<>" + ddlShow.SelectedItem.Value, "").Length > 0) ? dt.Select("Show_id<>" + ddlShow.SelectedItem.Value, "").CopyToDataTable() : dt.Clone();
            DataTable dtunique = dt_os.DefaultView.ToTable(true, "Wk_No");
            int tot_row = dt_cs.Rows.Count + dtunique.Rows.Count;
            string cweeklist = "";
            if (File.Exists(newfile) == false)
            {
                File.Copy(@"" + serverpath + "\\ExcelTemplates\\Pro-Forma_Report_Template.xlsx", newfile);
                Microsoft.Office.Interop.Excel.Application xlApp = null;
                Excel.Workbook xlWorkbook = null;
                Excel.Sheets xlSheets = null;
                Excel.Worksheet xlNewSheet = null;
                try
                {
                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    if (xlApp == null)
                        return;
                    xlWorkbook = xlApp.Workbooks.Open(newfile, 0, false, 5, "", "",
                            false, Excel.XlPlatform.xlWindows, "",
                            true, false, 0, true, false, false);
                    xlSheets = xlWorkbook.Sheets as Excel.Sheets;
                    xlNewSheet = (Excel.Worksheet)xlSheets[1];
                    xlNewSheet.Cells[1, 1] = ddlShow.SelectedItem.Text;
                    xlNewSheet.Cells[1, 4] = cyear.ToString() + "-" + (cyear + 1).ToString();
                    xlNewSheet.Cells[4, 3] = dt.Rows[0]["WeekBeginDate"].ToString();
                    xlNewSheet = (Excel.Worksheet)xlSheets[2];
                    int rowCount = 3, WeekNo = 0;
                    int i, stcol = 2;
                    for (i = 0; i < dt_cs.Rows.Count; i++)
                    {
                        stcol = 2;
                        WeekNo = Convert.ToInt32(dt_cs.Rows[i]["WK_No"]);
                        if ((rowCount == Convert.ToInt32(dt_cs.Rows[i]["WK_No"]) + 2) && i != 0)
                        {
                            xlNewSheet = (Excel.Worksheet)xlSheets[xlNewSheet.Index + 1];
                        }
                        else
                        {
                            xlNewSheet = (Excel.Worksheet)xlSheets[2];
                        }
                        rowCount = Convert.ToInt32(dt_cs.Rows[i]["WK_No"]) + 2;
                        xlNewSheet.Cells[rowCount, stcol] = dt_cs.Rows[i]["show_name"].ToString();
                        xlNewSheet.Cells[rowCount, ++stcol] = dt_cs.Rows[i]["city_name"].ToString();
                        xlNewSheet.Cells[rowCount, ++stcol] = dt_cs.Rows[i]["venue_name"].ToString();
                        xlNewSheet.Cells[rowCount, ++stcol] = dt_cs.Rows[i]["NoOfShow"].ToString();
                        xlNewSheet.Cells[rowCount, ++stcol] = dt_cs.Rows[i]["TotalGrossPerWk"].ToString();
                        xlNewSheet.Cells[rowCount, ++stcol] = dt_cs.Rows[i]["SubcriptionTicketSales"].ToString();
                        xlNewSheet.Cells[rowCount, ++stcol] = dt_cs.Rows[i]["GroupTicketSales"].ToString();
                        xlNewSheet.Cells[rowCount, ++stcol] = dt_cs.Rows[i]["SingleTicketSales"].ToString();
                        xlNewSheet.Cells[rowCount, ++stcol] = dt_cs.Rows[i]["TopExpenses"].ToString();
                        xlNewSheet.Cells[rowCount, ++stcol] = dt_cs.Rows[i]["PromoterLocalExp"].ToString();
                        xlNewSheet.Cells[rowCount, ++stcol] = dt_cs.Rows[i]["Gurantee"].ToString();
                        cweeklist = cweeklist + WeekNo.ToString() + ",";
                    }
                    xlNewSheet = (Excel.Worksheet)xlSheets[2];
                    int show_pref_id = Convert.ToInt32(dt.Rows[0]["Show_Pref_ID"]);
                    for (i = 0; i < dtunique.Rows.Count; i++)
                    {
                        stcol = 2;
                        WeekNo = Convert.ToInt32(dtunique.Rows[i]["WK_No"]);

                        if (Array.IndexOf(cweeklist.Split(','), WeekNo.ToString()) == -1)
                        {
                            string lastengtNo = dt_os.Compute("Max(Schedule_Engt_ID)", "Wk_No='" + WeekNo.ToString() + "' and show_id=" + show_pref_id.ToString()).ToString();
                            if (string.IsNullOrEmpty(lastengtNo) == true)
                            {
                                lastengtNo = dt_os.Compute("Max(Schedule_Engt_ID)", "Wk_No='" + WeekNo.ToString() + "'").ToString();
                            }
                            DataTable dttemp = dt_os.Select("Schedule_Engt_ID='" + lastengtNo + "'", "").CopyToDataTable();
                            rowCount = WeekNo + 2;
                            xlNewSheet.Cells[rowCount, stcol] = dttemp.Rows[0]["show_name"].ToString();
                            xlNewSheet.Cells[rowCount, ++stcol] = dttemp.Rows[0]["city_name"].ToString();
                            xlNewSheet.Cells[rowCount, ++stcol] = dttemp.Rows[0]["venue_name"].ToString();
                            xlNewSheet.Cells[rowCount, ++stcol] = dttemp.Rows[0]["NoOfShow"].ToString();
                            xlNewSheet.Cells[rowCount, ++stcol] = dttemp.Rows[0]["TotalGrossPerWk"].ToString();
                            xlNewSheet.Cells[rowCount, ++stcol] = dttemp.Rows[0]["SubcriptionTicketSales"].ToString();
                            xlNewSheet.Cells[rowCount, ++stcol] = dttemp.Rows[0]["GroupTicketSales"].ToString();
                            xlNewSheet.Cells[rowCount, ++stcol] = dttemp.Rows[0]["SingleTicketSales"].ToString();
                            xlNewSheet.Cells[rowCount, ++stcol] = dttemp.Rows[0]["TopExpenses"].ToString();
                            xlNewSheet.Cells[rowCount, ++stcol] = dttemp.Rows[0]["PromoterLocalExp"].ToString();
                            xlNewSheet.Cells[rowCount, ++stcol] = dttemp.Rows[0]["Gurantee"].ToString();
                        }
                    }
                    //xlNewSheet.Visible = Excel.XlSheetVisibility.xlSheetVeryHidden;
                    objcf.HideDBData_XLSheets(xlSheets, "2,3,4,5,6,7,8");
                    xlWorkbook.Save();
                    xlWorkbook.Close(Type.Missing, Type.Missing, Type.Missing);
                    xlApp.Quit();
                    GC.Collect();

                    foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
                    {
                        if (proc.MainWindowTitle.ToString() == "")
                        {
                            //proc.Kill();
                        }
                    }
                    string fName = newfile;
                    hdnfilepath.Value = newpath;
                    lnkexcel.HRef = newpath;
                }
                finally
                {
                    xlApp = null;
                }
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "", "anc();", true);
            }
        }
        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            LoadReport();
        }
        public DateTime GetMondayOfWeek(DateTime sourceDateTime)
        {
            DateTime GivenDate = Convert.ToDateTime(sourceDateTime);
            int delta = (Convert.ToInt32(GivenDate.DayOfWeek) == 0) ? 6 : Convert.ToInt32(GivenDate.DayOfWeek) - 1;
            DateTime sunday = GivenDate.AddDays(-delta);
            return sunday;
        }
    }
}