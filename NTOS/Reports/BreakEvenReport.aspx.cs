using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ReportDataLayer;
using MasterDataLayer;
using Microsoft.Reporting.WebForms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;
using System.Net;
namespace NTOS.Reports
{
    public partial class BreakEvenReport : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        MasterData objshow = new MasterData();
        ReportData objrpt = new ReportData();
        CommonFunction.CommonFun objcf = new CommonFunction.CommonFun();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblmsg.Text = string.Empty;

            if (!IsPostBack)
            {

                imgExcel.Visible = false;
                //string scr = "javascript:return comparedate('" + txtAsofDate.ClientID + "','" + txtTodate.ClientID + "','As of date should be less than To date!');";
                //txtAsofDate.Attributes.Add("onblur", scr);
                //txtTodate.Attributes.Add("onblur", scr);
                loadShow();
                SetReadonly();
                //loadCities();
                //loadVenues();
                ((ImageButton)this.Master.FindControl("imgbtnsave")).Visible = false;
                ((HiddenField)this.Master.FindControl("hdnreqlist")).Value = (ddlShow.ClientID + "," + ddlCity.ClientID + "," + ddlCreateddate.ClientID + "," + txtDiscountcap.ClientID);
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
            clear("c");
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
        protected void btnShowRpt_Click(object sender, EventArgs e)
        {

            hfstatus.Value = "true";
            filldatas();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "calculate();", true);

            //imgExcel.Visible = true;
            //ReportData objrpt = new ReportData();
            //DataTable dt = new DataTable();
            //decimal dis = Convert.ToDecimal(txtDiscountcap.Text.Replace("%", ""));
            //dt = objrpt.GetBreakevendata(Convert.ToInt32(ddlShow.SelectedItem.Value), Convert.ToInt32(ddlCity.SelectedItem.Value), Convert.ToInt32(ddlVenue.SelectedItem.Value), Convert.ToDateTime(ddlCreateddate.SelectedItem.Text), Convert.ToDateTime(lblEngtEndDate.Text), dis, Convert.ToInt32(ddlCreateddate.SelectedValue));
            //if (dt.Rows.Count > 0)
            //{
            //    Template(dt);
            //}
            //imgExcel.Visible = true;
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
        }
        protected void ddlCreateddate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEnddate();
            imgExcel.Visible = false;

        }
        public void filldatas()
        {
            ReportData objrpt = new ReportData();
            DataTable dt = new DataTable();
            decimal dis = Convert.ToDecimal(txtDiscountcap.Text.Replace("%", ""));
            dt = objrpt.GetBreakevendata(Convert.ToInt32(ddlShow.SelectedItem.Value),
                Convert.ToInt32(ddlCity.SelectedItem.Value), Convert.ToInt32(ddlVenue.SelectedItem.Value),
                Convert.ToDateTime(ddlCreateddate.SelectedItem.Text), Convert.ToDateTime(lblEngtEndDate.Text),
                dis, Convert.ToInt32(ddlCreateddate.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                txtNoofshowsperweek.Text = string.IsNullOrEmpty(dt.Rows[0]["performance"].ToString()) ? "0" : dt.Rows[0]["performance"].ToString();
                txtNoofweeks.Text = string.IsNullOrEmpty(dt.Rows[0]["noofweeks"].ToString()) ? "0" : dt.Rows[0]["noofweeks"].ToString();
                txtSeatspershow.Text = string.IsNullOrEmpty(dt.Rows[0]["seatspershow"].ToString()) ? "0" : dt.Rows[0]["seatspershow"].ToString();
                txtWeeklygrospotential.Text = string.IsNullOrEmpty(dt.Rows[0]["weeklygrosspotential"].ToString()) ? "0" : dt.Rows[0]["weeklygrosspotential"].ToString();
                lblNetavgpertix.Text = string.IsNullOrEmpty(dt.Rows[0]["Netavg"].ToString()) ? "0" : dt.Rows[0]["Netavg"].ToString();
                txtExchangerate.Text = string.IsNullOrEmpty(dt.Rows[0]["exchangerate"].ToString()) ? "0" : dt.Rows[0]["exchangerate"].ToString();
                txtSubloadin.Text = string.IsNullOrEmpty(dt.Rows[0]["subloadin"].ToString()) ? "0" : dt.Rows[0]["subloadin"].ToString();
                lblSubloadin.Text = string.IsNullOrEmpty(dt.Rows[0]["subloadin"].ToString()) ? "0" : dt.Rows[0]["subloadin"].ToString();
                lblSubloadin1.Text = string.IsNullOrEmpty(dt.Rows[0]["subloadin"].ToString()) ? "0" : dt.Rows[0]["subloadin"].ToString();
                lblSubloadin2.Text = string.IsNullOrEmpty(dt.Rows[0]["subloadin"].ToString()) ? "0" : dt.Rows[0]["subloadin"].ToString();
                lblSubloadin3.Text = string.IsNullOrEmpty(dt.Rows[0]["subloadin"].ToString()) ? "0" : dt.Rows[0]["subloadin"].ToString();
                lblSubloadin4.Text = string.IsNullOrEmpty(dt.Rows[0]["subloadin"].ToString()) ? "0" : dt.Rows[0]["subloadin"].ToString();
                lblSubloadin5.Text = string.IsNullOrEmpty(dt.Rows[0]["subloadin"].ToString()) ? "0" : dt.Rows[0]["subloadin"].ToString();
                lblSubloadin6.Text = string.IsNullOrEmpty(dt.Rows[0]["subloadin"].ToString()) ? "0" : dt.Rows[0]["subloadin"].ToString();
                lblSubloadin7.Text = string.IsNullOrEmpty(dt.Rows[0]["subloadin"].ToString()) ? "0" : dt.Rows[0]["subloadin"].ToString();
                lblSubloadinBreak.Text = string.IsNullOrEmpty(dt.Rows[0]["subloadin"].ToString()) ? "0" : dt.Rows[0]["subloadin"].ToString();

                //lblNoofweeks.Text = dt.Rows[0]["noofweeks"].ToString())?"0":
                //lblNoofweeks1.Text = dt.Rows[0]["noofweeks"].ToString())?"0":
                //lblNoofweeks2.Text = dt.Rows[0]["noofweeks"].ToString())?"0":
                //lblNoofweeks3.Text = dt.Rows[0]["noofweeks"].ToString())?"0":
                //lblNoofweeks4.Text = dt.Rows[0]["noofweeks"].ToString())?"0":
                //lblNoofweeks5.Text = dt.Rows[0]["noofweeks"].ToString())?"0":
                //lblNoofweeks6.Text = dt.Rows[0]["noofweeks"].ToString())?"0":
                //lblNoofweeks7.Text = dt.Rows[0]["noofweeks"].ToString())?"0":

                lblBoxofcgrosssale.Text = string.IsNullOrEmpty(dt.Rows[0]["bo_gross_sales"].ToString()) ? "0" : dt.Rows[0]["bo_gross_sales"].ToString();
                txtLessDiscounts.Text = string.IsNullOrEmpty(dt.Rows[0]["LessDiscount"].ToString()) ? "0" : dt.Rows[0]["LessDiscount"].ToString();
                txtTax.Text = string.IsNullOrEmpty(dt.Rows[0]["deal_tax_ptg"].ToString()) ? "0" : dt.Rows[0]["deal_tax_ptg"].ToString();
                txtRestoration.Text = string.IsNullOrEmpty(dt.Rows[0]["deal_facility_fee_amt"].ToString()) ? "0" : dt.Rows[0]["deal_facility_fee_amt"].ToString();
                txtSubscriptioncharge.Text = string.IsNullOrEmpty(dt.Rows[0]["deal_sub_sales_comm"].ToString()) ? "0" : dt.Rows[0]["deal_sub_sales_comm"].ToString();
                txtCCothercommissions.Text = string.IsNullOrEmpty(dt.Rows[0]["creditcardothercommisn"].ToString()) ? "0" : dt.Rows[0]["creditcardothercommisn"].ToString();
                txtGuarantee.Text = string.IsNullOrEmpty(dt.Rows[0]["deal_guarantee_income"].ToString()) ? "0" : dt.Rows[0]["deal_guarantee_income"].ToString();
                txtRoyalty.Text = string.IsNullOrEmpty(dt.Rows[0]["deal_royalty_income"].ToString()) ? "0" : dt.Rows[0]["deal_royalty_income"].ToString();
                txtFixedCosts.Text = string.IsNullOrEmpty(dt.Rows[0]["exp_l_local_fixed_bgt"].ToString()) ? "0" : dt.Rows[0]["exp_l_local_fixed_bgt"].ToString();
                txtAdHouseEquipment.Text = string.IsNullOrEmpty(dt.Rows[0]["exp_l_equip_rental_act"].ToString()) ? "0" : dt.Rows[0]["exp_l_equip_rental_act"].ToString();
                txtAdvertising.Text = string.IsNullOrEmpty(dt.Rows[0]["exp_d_ad_gross_act"].ToString()) ? "0" : dt.Rows[0]["exp_d_ad_gross_act"].ToString();
                txtCatering.Text = string.IsNullOrEmpty(dt.Rows[0]["exp_l_catering_act"].ToString()) ? "0" : dt.Rows[0]["exp_l_catering_act"].ToString();
                txtInsurance.Text = string.IsNullOrEmpty(dt.Rows[0]["exp_d_insurance_act"].ToString()) ? "0" : dt.Rows[0]["exp_d_insurance_act"].ToString();
                txtFireWatch.Text = string.IsNullOrEmpty(dt.Rows[0]["firewatch"].ToString()) ? "0" : dt.Rows[0]["firewatch"].ToString();
                txtMusicians.Text = string.IsNullOrEmpty(dt.Rows[0]["exp_d_musician_act"].ToString()) ? "0" : dt.Rows[0]["exp_d_musician_act"].ToString();
                txtRent.Text = string.IsNullOrEmpty(dt.Rows[0]["exp_l_rent_act"].ToString()) ? "0" : dt.Rows[0]["exp_l_rent_act"].ToString();
                txtStagehandsloadin.Text = string.IsNullOrEmpty(dt.Rows[0]["exp_d_stghand_loadin_act"].ToString()) ? "0" : dt.Rows[0]["exp_d_stghand_loadin_act"].ToString();
                txtStagehandsrunning.Text = string.IsNullOrEmpty(dt.Rows[0]["exp_d_stghand_running_act"].ToString()) ? "0" : dt.Rows[0]["exp_d_stghand_running_act"].ToString();
                txtTicketprinting.Text = string.IsNullOrEmpty(dt.Rows[0]["exp_l_ticket_print_act"].ToString()) ? "0" : dt.Rows[0]["exp_l_ticket_print_act"].ToString();
                txtWardrobehairloadin.Text = string.IsNullOrEmpty(dt.Rows[0]["exp_d_wardrobe_loadin_act"].ToString()) ? "0" : dt.Rows[0]["exp_d_wardrobe_loadin_act"].ToString();
                txtWardrobehairrunning.Text = string.IsNullOrEmpty(dt.Rows[0]["exp_d_wardrobe_running_act"].ToString()) ? "0" : dt.Rows[0]["exp_d_wardrobe_running_act"].ToString();
                txtPF.Text = string.IsNullOrEmpty(dt.Rows[0]["exp_l_presenter_profit_act"].ToString()) ? "0" : dt.Rows[0]["exp_l_presenter_profit_act"].ToString();
                txtOther.Text = string.IsNullOrEmpty(dt.Rows[0]["other"].ToString()) ? "0" : dt.Rows[0]["other"].ToString();
                txtNMTP.Text = string.IsNullOrEmpty(dt.Rows[0]["NMTP"].ToString()) ? "0" : dt.Rows[0]["NMTP"].ToString();
                txtNMTPTR.Text = string.IsNullOrEmpty(dt.Rows[0]["NMTPTR"].ToString()) ? "0" : dt.Rows[0]["NMTPTR"].ToString();
                txtPShare.Text = string.IsNullOrEmpty(dt.Rows[0]["presentersharesplit"].ToString()) ? "0" : dt.Rows[0]["presentersharesplit"].ToString();
                //txtPShareofsplit.Text = string.IsNullOrEmpty(dt.Rows[0]["producersharesplit"].ToString()) ? "0" : dt.Rows[0]["producersharesplit"].ToString();
                txtPShareofsplit.Text = Convert.ToString((100) - Convert.ToDecimal(txtPShare.Text));
                txtLessTWS.Text = string.IsNullOrEmpty(dt.Rows[0]["deal_incm_wthd_tax_bgt_amt_per"].ToString()) ? "0" : dt.Rows[0]["deal_incm_wthd_tax_bgt_amt_per"].ToString();
                txtLessTWamt.Text = string.IsNullOrEmpty(dt.Rows[0]["deal_incm_wthd_tax_bgt_amt"].ToString()) ? "0" : dt.Rows[0]["deal_incm_wthd_tax_bgt_amt"].ToString();
                txtWOE.Text = string.IsNullOrEmpty(dt.Rows[0]["show_wkly_operating_expense"].ToString()) ? "0" : dt.Rows[0]["show_wkly_operating_expense"].ToString();
                txtVR.Text = string.IsNullOrEmpty(dt.Rows[0]["show_var_rolyalties"].ToString()) ? "0" : dt.Rows[0]["show_var_rolyalties"].ToString();
                ddlTerm.SelectedIndex = ddlTerm.Items.IndexOf(ddlTerm.Items.FindByValue(dt.Rows[0]["TermDeal"].ToString()));

                chkAddlHouseEquipment.Checked = true;
                chkAdvertising.Checked = true;
                chkCatering.Checked = true;
                chkFireWatch.Checked = true;
                chkFixedCosts.Checked = true;
                chkGuarantee.Checked = true;
                chkInsurance.Checked = true;
                chkMusicians.Checked = true;
                chkOther.Checked = true;
                chkPresenterProfit.Checked = true;
                chkRent.Checked = true;
                chkRoyalty.Checked = true;
                chkStagehandsLoadInOut.Checked = true;
                chkStagehandsRunning.Checked = true;
                chkTicketPrinting.Checked = true;
                chkWardrobeHairLoadInOut.Checked = true;
                chkWardrobeHairRunning.Checked = true;

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "calculate();saveconfirm();", true);


            }

        }
        public static string queryfieldform(string value)
        {
            char[] chDlr = { '$', '%', ',', ' ' };
            return string.IsNullOrEmpty("'" + value + "'") ? "''" : "'" + value + "'";
            //return string.IsNullOrEmpty("'" + value.Trim(chDlr) + "'") ? "''" : "'" + value.Trim(chDlr) + "'";
        }
        public void Template(DataTable dt)
        {

            string serverpath = this.MapPath(".");
            string date = System.DateTime.Now.ToString("ddMMyyyy_hhmmssm");

            string newfile = @"" + serverpath + "\\ExcelReports\\BreakEven_Report_" + date + ".xls";
            string filename = "BreakEven_Report_" + date + ".xls";
            String ExcelPath = System.Configuration.ConfigurationManager.AppSettings["ExcelPath"].ToString();
            string newpath = ExcelPath + "BreakEven_Report_" + date + ".xls";
            //string newpath = "\\Reports\\ExcelReports\\BreakEven_Report_" + date + ".xls";
            lnkexcel.HRef = newpath;
            if (File.Exists(newfile) == false)
            {
                File.Copy(@"" + serverpath + "\\ExcelTemplates\\BreakEven_Report_Template.xls", newfile);


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
                    xlNewSheet = (Excel.Worksheet)xlSheets[2];
                    int rowCount = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        rowCount += 1;
                        for (int i = 1; i < dt.Columns.Count + 1; i++)
                        {
                            xlNewSheet.Cells[rowCount, i] = dr[i - 1].ToString();
                        }
                    }
                    xlNewSheet = (Excel.Worksheet)xlSheets[1];
                    hfBevnval.Value = string.IsNullOrEmpty(hfBevnval.Value) ? "0.0" : hfBevnval.Value;
                    xlNewSheet.Cells[14, 13] = hfBevnval.Value + "%";
                    //Excel.Worksheet excelc = null;
                    //excelc=(Excel.Worksheet)xlSheets[2];
                    //excelc.Cells.Value2("M14", "M14");

                    //xcel.Range oRange;
                    // Resize the columns 
                    //oRange = xlNewSheet.get_Range(xlNewSheet.Cells[1, 1],
                    //              xlNewSheet.Cells[rowCount, dt.Columns.Count]);
                    //oRange.EntireColumn.AutoFit();
                    objcf.HideDBData_XLSheets(xlSheets, "2");
                    xlWorkbook.Save();
                    xlWorkbook.Close(Type.Missing, Type.Missing, Type.Missing);
                    xlApp.Quit();
                    foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
                    {
                        if (proc.MainWindowTitle.ToString() == "")
                        {
                            //proc.Kill();
                        }

                    }
                    //Response.Redirect(newfilepath);
                    string fName = newfile;

                    //DownLoadFileFromServer(newpath);
                }
                finally
                {

                    xlApp = null;
                }
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "", "anc();", true);
            }
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
            string filePath = ServerMapPath(fileName);
            //This is used to get the current response.
            HttpResponse res = GetHttpResponse();
            res.Clear();
            res.AppendHeader("content-disposition", "attachment; filename=" + fileName);
            res.ContentType = "application/octet-stream";
            res.WriteFile(filePath);
            res.Flush();
            res.End();
        }
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                filldatas();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "calculate();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "calculate1();", true);
                modpop.Show();
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        protected void imgExcel_Click(object sender, EventArgs e)
        {
            try
            {
                imgExcel.Visible = true;
                ReportData objrpt = new ReportData();
                DataTable dt = new DataTable();
                decimal dis = Convert.ToDecimal(txtDiscountcap.Text.Replace("%", ""));
                dt = objrpt.GetBreakevendata(Convert.ToInt32(ddlShow.SelectedItem.Value), Convert.ToInt32(ddlCity.SelectedItem.Value), Convert.ToInt32(ddlVenue.SelectedItem.Value), Convert.ToDateTime(ddlCreateddate.SelectedItem.Text), Convert.ToDateTime(lblEngtEndDate.Text), dis, Convert.ToInt32(ddlCreateddate.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    Template(dt);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        protected void btncalculation_Click(object sender, EventArgs e)
        {
            if (hfstatus.Value == "true")
            {
                ReportData objrpt = new ReportData();
                DataTable dt = new DataTable();
                decimal dis = Convert.ToDecimal(txtDiscountcap.Text.Replace("%", ""));
                dt = objrpt.GetBreakevendata(Convert.ToInt32(ddlShow.SelectedItem.Value), Convert.ToInt32(ddlCity.SelectedItem.Value), Convert.ToInt32(ddlVenue.SelectedItem.Value), Convert.ToDateTime(ddlCreateddate.SelectedItem.Text), Convert.ToDateTime(lblEngtEndDate.Text), dis, Convert.ToInt32(ddlCreateddate.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    Template(dt);
                }
                imgExcel.Visible = true;
                BEval();
            }
            hfstatus.Value = "";
            //txtSubloadin.CssClass = "Numeric";
        }
        public void BEval()
        {
            try
            {
                string query = "select 'House Capacity' rows, '0.00' field, " + queryfieldform(lblHousecapacity.Text) + " Week1, " + queryfieldform(lblHousecapacity1.Text)
                            + " Week2, " + queryfieldform(lblHousecapacity2.Text) + " Week3, " + queryfieldform(lblHousecapacity3.Text)
                            + " Week4, " + queryfieldform(lblHousecapacity4.Text) + " Run1, " + queryfieldform(lblHousecapacity5.Text)
                            + " Run2, " + queryfieldform(lblHousecapacity6.Text) + " Run3, " + queryfieldform(lblHousecapacity7.Text)
                            + " Run4, " + queryfieldform(lblHousecapacity8.Text) + " BreakEven union all ";
                query += "select 'Performance Capacity' rows, '0.00' field," + queryfieldform(txtPerformanceCapacity1.Text) + " Week1, " + queryfieldform(txtPerformanceCapacity2.Text)
                        + " Week2, " + queryfieldform(txtPerformanceCapacity3.Text) + " Week3, " + queryfieldform(txtPerformanceCapacity4.Text)
                        + " Week4, " + queryfieldform(txtPerformanceCapacity5.Text) + " Run1, " + queryfieldform(txtPerformanceCapacity6.Text)
                        + " Run2, " + queryfieldform(txtPerformanceCapacity7.Text) + " Run3, " + queryfieldform(txtPerformanceCapacity8.Text)
                        + " Run4, " + queryfieldform(txtPerformanceCapacityBrevn.Text) + " BreakEven union all ";
                query += "select 'Tickets Sold' rows, '0.00' field," + queryfieldform(lblTicketsoldweek1.Text) + " Week1, " + queryfieldform(lblTicketsoldweek2.Text)
                        + " Week2, " + queryfieldform(lblTicketsoldweek3.Text) + " Week3, " + queryfieldform(lblTicketsoldweek4.Text)
                        + " Week4, " + queryfieldform(lblTicketsoldRun1.Text) + " Run1, " + queryfieldform(lblTicketsoldRun2.Text)
                        + " Run2, " + queryfieldform(lblTicketsoldRun3.Text) + " Run3, " + queryfieldform(lblTicketsoldRun4.Text)
                        + " Run4, " + queryfieldform(lblTicketsoldBreakeven.Text) + " BreakEven union all ";
                query += "select 'Sub Load - in' rows, " + queryfieldform(txtSubloadin.Text) + " field," + queryfieldform(lblSubloadin.Text) + " Week1, " + queryfieldform(lblSubloadin1.Text)
                       + " Week2, " + queryfieldform(lblSubloadin2.Text) + " Week3, " + queryfieldform(lblSubloadin3.Text)
                       + " Week4, " + queryfieldform(lblSubloadin4.Text) + " Run1, " + queryfieldform(lblSubloadin5.Text)
                       + " Run2, " + queryfieldform(lblSubloadin6.Text) + " Run3, " + queryfieldform(lblSubloadin7.Text)
                       + " Run4, " + queryfieldform(lblSubloadinBreak.Text) + " BreakEven union all ";
                //query += "select 'No. of Weeks' rows, '0.00' field," + queryfieldform(lblNoofweeks.Text) + " Week1, " + queryfieldform(lblNoofweeks1.Text)
                //       + " Week2, " + queryfieldform(lblNoofweeks2.Text) + " Week3, " + queryfieldform(lblNoofweeks3.Text)
                //       + " Week4, " + queryfieldform(lblNoofweeks4.Text) + " Run1, " + queryfieldform(lblNoofweeks5.Text)
                //       + " Run2, " + queryfieldform(lblNoofweeks6.Text) + " Run3, " + queryfieldform(lblNoofweeks7.Text)
                //       + " Run4, " + queryfieldform(lblNoofweeks8.Text) + " BreakEven union all ";
                query += "select 'Box Office Gross' rows, '0.00' field," + queryfieldform(lblBoxofcgrosssale.Text) + " Week1, " + queryfieldform(lblBoxofcgrosssale1.Text)
                       + " Week2, " + queryfieldform(lblBoxofcgrosssale2.Text) + " Week3, " + queryfieldform(lblBoxofcgrosssale3.Text)
                       + " Week4, " + queryfieldform(lblBoxofcgrosssale4.Text) + " Run1, " + queryfieldform(lblBoxofcgrosssale5.Text)
                       + " Run2, " + queryfieldform(lblBoxofcgrosssale6.Text) + " Run3, " + queryfieldform(lblBoxofcgrosssale7.Text)
                       + " Run4, " + queryfieldform(lblBoxofcgrosssalebreakeven.Text) + " BreakEven union all ";
                query += "select 'Less Discounts' rows, " + queryfieldform(txtLessDiscounts.Text) + " field," + queryfieldform(lblLessdiscountWeek1.Text) + " Week1, " + queryfieldform(lblLessdiscountWeek2.Text)
                       + " Week2, " + queryfieldform(lblLessdiscountWeek3.Text) + " Week3, " + queryfieldform(lblLessdiscountWeek4.Text)
                       + " Week4, " + queryfieldform(lblLessdiscountWeek5.Text) + " Run1, " + queryfieldform(lblLessdiscountWeek6.Text)
                       + " Run2, " + queryfieldform(lblLessdiscountWeek7.Text) + " Run3, " + queryfieldform(lblLessdiscountWeek8.Text)
                       + " Run4, " + queryfieldform(lblLessdiscountBreakeven.Text) + " BreakEven union all ";
                query += "select 'Adjusted Gross' rows, '0.00' field," + queryfieldform(lblAdjustedgrossWeekly1.Text) + " Week1, " + queryfieldform(lblAdjustedgrossWeekly2.Text)
                       + " Week2, " + queryfieldform(lblAdjustedgrossWeekly3.Text) + " Week3, " + queryfieldform(lblAdjustedgrossWeekly4.Text)
                       + " Week4, " + queryfieldform(lblAdjustedgrossWeekly5.Text) + " Run1, " + queryfieldform(lblAdjustedgrossWeekly6.Text)
                       + " Run2, " + queryfieldform(lblAdjustedgrossWeekly7.Text) + " Run3, " + queryfieldform(lblAdjustedgrossWeekly8.Text)
                       + " Run4, " + queryfieldform(lblAdjustedgrossBreakeven.Text) + " BreakEven union all ";
                query += "select 'Tax' rows, " + queryfieldform(txtTax.Text) + " field," + queryfieldform(lblTax1.Text) + " Week1, " + queryfieldform(lblTax2.Text)
                       + " Week2, " + queryfieldform(lblTax3.Text) + " Week3, " + queryfieldform(lblTax4.Text)
                       + " Week4, " + queryfieldform(lblTax5.Text) + " Run1, " + queryfieldform(lblTax6.Text)
                       + " Run2, " + queryfieldform(lblTax7.Text) + " Run3, " + queryfieldform(lblTax8.Text)
                       + " Run4, " + queryfieldform(lblTax8.Text) + " BreakEven union all ";
                query += "select 'Restoration' rows, " + queryfieldform(txtRestoration.Text) + " field,"
                                    + queryfieldform(lblRestoration1.Text)
                       + " Week1, " + queryfieldform(lblRestoration2.Text)
                       + " Week2, " + queryfieldform(lblRestoration3.Text)
                       + " Week3, " + queryfieldform(lblRestoration4.Text)
                       + " Week4, " + queryfieldform(lblRestoration5.Text)
                       + " Run1, " + queryfieldform(lblRestoration6.Text)
                       + " Run2, " + queryfieldform(lblRestoration7.Text)
                       + " Run3, " + queryfieldform(lblRestoration8.Text)
                       + " Run4, " + queryfieldform(lblRestorationBreakeven.Text) + " BreakEven union all ";
                query += "select 'Subscription Charge' rows, " + queryfieldform(txtSubscriptioncharge.Text) + " field,"
                                  + queryfieldform(lblSubscriptioncharge1.Text)
                     + " Week1, " + queryfieldform(lblSubscriptioncharge2.Text)
                     + " Week2, " + queryfieldform(lblSubscriptioncharge3.Text)
                     + " Week3, " + queryfieldform(lblSubscriptioncharge4.Text)
                     + " Week4, " + queryfieldform(lblSubscriptioncharge5.Text)
                     + " Run1, " + queryfieldform(lblSubscriptioncharge6.Text)
                     + " Run2, " + queryfieldform(lblSubscriptioncharge7.Text)
                     + " Run3, " + queryfieldform(lblSubscriptioncharge8.Text)
                     + " Run4, " + queryfieldform(lblSubscriptionchargeBreakeven.Text) + " BreakEven union all ";
                query += "select 'Credit Card & Other Commissions' rows, " + queryfieldform(txtCCothercommissions.Text) + " field,"
                                  + queryfieldform(lblCCothercommssns1.Text)
                     + " Week1, " + queryfieldform(lblCCothercommssns2.Text)
                     + " Week2, " + queryfieldform(lblCCothercommssns3.Text)
                     + " Week3, " + queryfieldform(lblCCothercommssns4.Text)
                     + " Week4, " + queryfieldform(lblCCothercommssns5.Text)
                     + " Run1, " + queryfieldform(lblCCothercommssns6.Text)
                     + " Run2, " + queryfieldform(lblCCothercommssns7.Text)
                     + " Run3, " + queryfieldform(lblCCothercommssns8.Text)
                     + " Run4, " + queryfieldform(lblCCothercommssnsBreakeven.Text) + " BreakEven union all ";
                query += "select 'Net Adjusted B. O. Receipts' rows, '0.00' field,"
                                  + queryfieldform(lblNetAR1.Text)
                     + " Week1, " + queryfieldform(lblNetAR2.Text)
                     + " Week2, " + queryfieldform(lblNetAR3.Text)
                     + " Week3, " + queryfieldform(lblNetAR4.Text)
                     + " Week4, " + queryfieldform(lblNetAR5.Text)
                     + " Run1, " + queryfieldform(lblNetAR6.Text)
                     + " Run2, " + queryfieldform(lblNetAR7.Text)
                     + " Run3, " + queryfieldform(lblNetAR8.Text)
                     + " Run4, " + queryfieldform(lblNetARBEvn.Text) + " BreakEven union all ";
                query += "select 'Guarantee' rows, " + queryfieldform(txtGuarantee.Text) + " field,"
                                  + queryfieldform(lblGuarantee1.Text)
                     + " Week1, " + queryfieldform(lblGuarantee2.Text)
                     + " Week2, " + queryfieldform(lblGuarantee3.Text)
                     + " Week3, " + queryfieldform(lblGuarantee4.Text)
                     + " Week4, " + queryfieldform(lblGuarantee5.Text)
                     + " Run1, " + queryfieldform(lblGuarantee6.Text)
                     + " Run2, " + queryfieldform(lblGuarantee7.Text)
                     + " Run3, " + queryfieldform(lblGuarantee8.Text)
                     + " Run4, " + queryfieldform(lblGuaranteeBEvn.Text) + " BreakEven union all ";
                query += "select 'Royalty' rows, " + queryfieldform(txtRoyalty.Text) + " field,"
                                 + queryfieldform(lblRoyalty1.Text)
                    + " Week1, " + queryfieldform(lblRoyalty2.Text)
                    + " Week2, " + queryfieldform(lblRoyalty3.Text)
                    + " Week3, " + queryfieldform(lblRoyalty4.Text)
                    + " Week4, " + queryfieldform(lblRoyalty5.Text)
                    + " Run1, " + queryfieldform(lblRoyalty6.Text)
                    + " Run2, " + queryfieldform(lblRoyalty7.Text)
                    + " Run3, " + queryfieldform(lblRoyalty8.Text)
                    + " Run4, " + queryfieldform(lblRoyaltyBEvn.Text) + " BreakEven union all ";
                query += "select 'Fixed Costs' rows, " + queryfieldform(txtFixedCosts.Text) + " field,"
                                 + queryfieldform(lblFixedcosts1.Text)
                    + " Week1, " + queryfieldform(lblFixedcosts2.Text)
                    + " Week2, " + queryfieldform(lblFixedcosts3.Text)
                    + " Week3, " + queryfieldform(lblFixedcosts4.Text)
                    + " Week4, " + queryfieldform(lblFixedcosts5.Text)
                    + " Run1, " + queryfieldform(lblFixedcosts6.Text)
                    + " Run2, " + queryfieldform(lblFixedcosts7.Text)
                    + " Run3, " + queryfieldform(lblFixedcosts8.Text)
                    + " Run4, " + queryfieldform(lblFixedcostsBEvn.Text) + " BreakEven union all ";
                query += "select 'Additional House Equipment' rows, " + queryfieldform(txtAdHouseEquipment.Text) + " field,"
                                + queryfieldform(lblAdHouseEquipment1.Text)
                   + " Week1, " + queryfieldform(lblAdHouseEquipment2.Text)
                   + " Week2, " + queryfieldform(lblAdHouseEquipment3.Text)
                   + " Week3, " + queryfieldform(lblAdHouseEquipment4.Text)
                   + " Week4, " + queryfieldform(lblAdHouseEquipment5.Text)
                   + " Run1, " + queryfieldform(lblAdHouseEquipment6.Text)
                   + " Run2, " + queryfieldform(lblAdHouseEquipment7.Text)
                   + " Run3, " + queryfieldform(lblAdHouseEquipment8.Text)
                   + " Run4, " + queryfieldform(lblAdHouseEquipmentBEvn.Text) + " BreakEven union all ";
                query += "select 'Advertising' rows, " + queryfieldform(txtAdvertising.Text) + " field,"
                                + queryfieldform(lblAdvertising1.Text)
                   + " Week1, " + queryfieldform(lblAdvertising2.Text)
                   + " Week2, " + queryfieldform(lblAdvertising3.Text)
                   + " Week3, " + queryfieldform(lblAdvertising4.Text)
                   + " Week4, " + queryfieldform(lblAdvertising5.Text)
                   + " Run1, " + queryfieldform(lblAdvertising6.Text)
                   + " Run2, " + queryfieldform(lblAdvertising7.Text)
                   + " Run3, " + queryfieldform(lblAdvertising8.Text)
                   + " Run4, " + queryfieldform(lblAdvertisingBEvn.Text) + " BreakEven union all ";
                query += "select 'Catering' rows, " + queryfieldform(txtCatering.Text) + " field,"
                                          + queryfieldform(lblCatering1.Text)
                             + " Week1, " + queryfieldform(lblCatering2.Text)
                             + " Week2, " + queryfieldform(lblCatering3.Text)
                             + " Week3, " + queryfieldform(lblCatering4.Text)
                             + " Week4, " + queryfieldform(lblCatering5.Text)
                             + " Run1, " + queryfieldform(lblCatering6.Text)
                             + " Run2, " + queryfieldform(lblCatering7.Text)
                             + " Run3, " + queryfieldform(lblCatering8.Text)
                             + " Run4, " + queryfieldform(lblCateringBEvn.Text) + " BreakEven union all ";
                query += "select 'Insurance' rows, " + queryfieldform(txtInsurance.Text) + " field,"
                                          + queryfieldform(lblInsurance1.Text)
                             + " Week1, " + queryfieldform(lblInsurance2.Text)
                             + " Week2, " + queryfieldform(lblInsurance3.Text)
                             + " Week3, " + queryfieldform(lblInsurance4.Text)
                             + " Week4, " + queryfieldform(lblInsurance5.Text)
                             + " Run1, " + queryfieldform(lblInsurance6.Text)
                             + " Run2, " + queryfieldform(lblInsurance7.Text)
                             + " Run3, " + queryfieldform(lblInsurance8.Text)
                             + " Run4, " + queryfieldform(lblInsuranceBEvn.Text) + " BreakEven union all ";
                query += "select 'Fire Watch' rows, " + queryfieldform(txtFireWatch.Text) + " field,"
                                         + queryfieldform(lblFirewatch1.Text)
                            + " Week1, " + queryfieldform(lblFirewatch2.Text)
                            + " Week2, " + queryfieldform(lblFirewatch3.Text)
                            + " Week3, " + queryfieldform(lblFirewatch4.Text)
                            + " Week4, " + queryfieldform(lblFirewatch5.Text)
                            + " Run1, " + queryfieldform(lblFirewatch6.Text)
                            + " Run2, " + queryfieldform(lblFirewatch7.Text)
                            + " Run3, " + queryfieldform(lblFirewatch8.Text)
                            + " Run4, " + queryfieldform(lblFirewatchBEvn.Text) + " BreakEven union all ";
                query += "select 'Musicians' rows, " + queryfieldform(txtMusicians.Text) + " field,"
                                         + queryfieldform(lblMusicians1.Text)
                            + " Week1, " + queryfieldform(lblMusicians2.Text)
                            + " Week2, " + queryfieldform(lblMusicians3.Text)
                            + " Week3, " + queryfieldform(lblMusicians4.Text)
                            + " Week4, " + queryfieldform(lblMusicians5.Text)
                            + " Run1, " + queryfieldform(lblMusicians6.Text)
                            + " Run2, " + queryfieldform(lblMusicians7.Text)
                            + " Run3, " + queryfieldform(lblMusicians8.Text)
                            + " Run4, " + queryfieldform(lblMusiciansBEvn.Text) + " BreakEven union all ";
                query += "select 'Rent' rows, " + queryfieldform(txtRent.Text) + " field,"
                                       + queryfieldform(lblRent1.Text)
                          + " Week1, " + queryfieldform(lblRent2.Text)
                          + " Week2, " + queryfieldform(lblRent3.Text)
                          + " Week3, " + queryfieldform(lblRent4.Text)
                          + " Week4, " + queryfieldform(lblRent5.Text)
                          + " Run1, " + queryfieldform(lblRent6.Text)
                          + " Run2, " + queryfieldform(lblRent7.Text)
                          + " Run3, " + queryfieldform(lblRent8.Text)
                          + " Run4, " + queryfieldform(lblRentBEvn.Text) + " BreakEven union all ";
                query += "select 'Stagehands - Load In/Out' rows, " + queryfieldform(txtStagehandsloadin.Text) + " field,"
                                       + queryfieldform(lblStagehandslodin1.Text)
                          + " Week1, " + queryfieldform(lblStagehandslodin2.Text)
                          + " Week2, " + queryfieldform(lblStagehandslodin3.Text)
                          + " Week3, " + queryfieldform(lblStagehandslodin4.Text)
                          + " Week4, " + queryfieldform(lblStagehandslodin5.Text)
                          + " Run1, " + queryfieldform(lblStagehandslodin6.Text)
                          + " Run2, " + queryfieldform(lblStagehandslodin7.Text)
                          + " Run3, " + queryfieldform(lblStagehandslodin8.Text)
                          + " Run4, " + queryfieldform(lblStagehandslodinBEvn.Text) + " BreakEven union all ";
                query += "select 'Stagehands - Running' rows, " + queryfieldform(txtStagehandsrunning.Text) + " field,"
                                                   + queryfieldform(lblStagehandsrunning1.Text)
                                      + " Week1, " + queryfieldform(lblStagehandsrunning2.Text)
                                      + " Week2, " + queryfieldform(lblStagehandsrunning3.Text)
                                      + " Week3, " + queryfieldform(lblStagehandsrunning4.Text)
                                      + " Week4, " + queryfieldform(lblStagehandsrunning5.Text)
                                      + " Run1, " + queryfieldform(lblStagehandsrunning6.Text)
                                      + " Run2, " + queryfieldform(lblStagehandsrunning7.Text)
                                      + " Run3, " + queryfieldform(lblStagehandsrunning8.Text)
                                      + " Run4, " + queryfieldform(lblStagehandsrunningBEvn.Text) + " BreakEven union all ";
                query += "select 'Ticket Printing - Running' rows, " + queryfieldform(txtTicketprinting.Text) + " field,"
                                                   + queryfieldform(lblTicketPrinting1.Text)
                                      + " Week1, " + queryfieldform(lblTicketPrinting2.Text)
                                      + " Week2, " + queryfieldform(lblTicketPrinting3.Text)
                                      + " Week3, " + queryfieldform(lblTicketPrinting4.Text)
                                      + " Week4, " + queryfieldform(lblTicketPrinting5.Text)
                                      + " Run1, " + queryfieldform(lblTicketPrinting6.Text)
                                      + " Run2, " + queryfieldform(lblTicketPrinting7.Text)
                                      + " Run3, " + queryfieldform(lblTicketPrinting8.Text)
                                      + " Run4, " + queryfieldform(lblTicketPrintingBEvn.Text) + " BreakEven union all ";
                query += "select 'Wardrobe/Hair - Load In/Out' rows, " + queryfieldform(txtWardrobehairloadin.Text) + " field,"
                                                   + queryfieldform(lblWardrobehairloadin1.Text)
                                      + " Week1, " + queryfieldform(lblWardrobehairloadin2.Text)
                                      + " Week2, " + queryfieldform(lblWardrobehairloadin3.Text)
                                      + " Week3, " + queryfieldform(lblWardrobehairloadin4.Text)
                                      + " Week4, " + queryfieldform(lblWardrobehairloadin5.Text)
                                      + " Run1, " + queryfieldform(lblWardrobehairloadin6.Text)
                                      + " Run2, " + queryfieldform(lblWardrobehairloadin7.Text)
                                      + " Run3, " + queryfieldform(lblWardrobehairloadin8.Text)
                                      + " Run4, " + queryfieldform(lblWardrobehairloadinBEvn.Text) + " BreakEven union all ";
                query += "select 'Wardrobe/Hair - Running' rows, " + queryfieldform(txtWardrobehairrunning.Text) + " field,"
                                                   + queryfieldform(lblWardrobehairrunning1.Text)
                                      + " Week1, " + queryfieldform(lblWardrobehairrunning2.Text)
                                      + " Week2, " + queryfieldform(lblWardrobehairrunning3.Text)
                                      + " Week3, " + queryfieldform(lblWardrobehairrunning4.Text)
                                      + " Week4, " + queryfieldform(lblWardrobehairrunning5.Text)
                                      + " Run1, " + queryfieldform(lblWardrobehairrunning6.Text)
                                      + " Run2, " + queryfieldform(lblWardrobehairrunning7.Text)
                                      + " Run3, " + queryfieldform(lblWardrobehairrunning8.Text)
                                      + " Run4, " + queryfieldform(lblWardrobehairrunningBEvn.Text) + " BreakEven union all ";
                query += "select 'Presenter Profit' rows, " + queryfieldform(txtPF.Text) + " field,"
                                                   + queryfieldform(lblPF1.Text)
                                      + " Week1, " + queryfieldform(lblPF2.Text)
                                      + " Week2, " + queryfieldform(lblPF3.Text)
                                      + " Week3, " + queryfieldform(lblPF4.Text)
                                      + " Week4, " + queryfieldform(lblPF5.Text)
                                      + " Run1, " + queryfieldform(lblPF6.Text)
                                      + " Run2, " + queryfieldform(lblPF7.Text)
                                      + " Run3, " + queryfieldform(lblPF8.Text)
                                      + " Run4, " + queryfieldform(lblPFBEvn.Text) + " BreakEven union all ";
                query += "select 'Other' rows, " + queryfieldform(txtOther.Text) + " field,"
                                                   + queryfieldform(lblOther1.Text)
                                      + " Week1, " + queryfieldform(lblOther2.Text)
                                      + " Week2, " + queryfieldform(lblOther3.Text)
                                      + " Week3, " + queryfieldform(lblOther4.Text)
                                      + " Week4, " + queryfieldform(lblOther5.Text)
                                      + " Run1, " + queryfieldform(lblOther6.Text)
                                      + " Run2, " + queryfieldform(lblOther7.Text)
                                      + " Run3, " + queryfieldform(lblOther8.Text)
                                      + " Run4, " + queryfieldform(lblOtherBEvn.Text) + " BreakEven union all ";
                query += "select 'TOTAL LOCAL EXPENSE' rows, '0.00' field,"
                                                   + queryfieldform(lblTotalLExp1.Text)
                                      + " Week1, " + queryfieldform(lblTotalLExp2.Text)
                                      + " Week2, " + queryfieldform(lblTotalLExp3.Text)
                                      + " Week3, " + queryfieldform(lblTotalLExp4.Text)
                                      + " Week4, " + queryfieldform(lblTotalLExp5.Text)
                                      + " Run1, " + queryfieldform(lblTotalLExp6.Text)
                                      + " Run2, " + queryfieldform(lblTotalLExp7.Text)
                                      + " Run3, " + queryfieldform(lblTotalLExp8.Text)
                                      + " Run4, " + queryfieldform(lblTotalLExpBEvn.Text) + " BreakEven union all ";
                query += "select 'FORMULA CHECK' rows, '0.00' field,"
                                                  + queryfieldform(lblTotalLExp1.Text)
                                     + " Week1, " + queryfieldform(lblTotalLExp2.Text)
                                     + " Week2, " + queryfieldform(lblTotalLExp3.Text)
                                     + " Week3, " + queryfieldform(lblTotalLExp4.Text)
                                     + " Week4, " + queryfieldform(lblTotalLExp5.Text)
                                     + " Run1, " + queryfieldform(lblTotalLExp6.Text)
                                     + " Run2, " + queryfieldform(lblTotalLExp7.Text)
                                     + " Run3, " + queryfieldform(lblTotalLExp8.Text)
                                     + " Run4, " + queryfieldform(lblTotalLExpBEvn.Text) + " BreakEven union all ";
                query += "select 'Money Remaining' rows, '0.00' field,"
                                                 + queryfieldform(lblMoneyRemaining1.Text)
                                    + " Week1, " + queryfieldform(lblMoneyRemaining2.Text)
                                    + " Week2, " + queryfieldform(lblMoneyRemaining3.Text)
                                    + " Week3, " + queryfieldform(lblMoneyRemaining4.Text)
                                    + " Week4, " + queryfieldform(lblMoneyRemaining5.Text)
                                    + " Run1, " + queryfieldform(lblMoneyRemaining6.Text)
                                    + " Run2, " + queryfieldform(lblMoneyRemaining7.Text)
                                    + " Run3, " + queryfieldform(lblMoneyRemaining8.Text)
                                    + " Run4, " + queryfieldform(lblMoneyRemainingBEvn.Text) + " BreakEven union all ";
                query += "select 'Next Monies - To Producer' rows, " + queryfieldform(txtNMTP.Text) + " field,"
                                                + queryfieldform(lblNMTP1.Text)
                                   + " Week1, " + queryfieldform(lblNMTP2.Text)
                                   + " Week2, " + queryfieldform(lblNMTP3.Text)
                                   + " Week3, " + queryfieldform(lblNMTP4.Text)
                                   + " Week4, " + queryfieldform(lblNMTP5.Text)
                                   + " Run1, " + queryfieldform(lblNMTP6.Text)
                                   + " Run2, " + queryfieldform(lblNMTP7.Text)
                                   + " Run3, " + queryfieldform(lblNMTP8.Text)
                                   + " Run4, " + queryfieldform(lblNMTPBEvn.Text) + " BreakEven union all ";
                query += "select 'Next Monies - To Presenter' rows, " + queryfieldform(txtNMTPTR.Text) + " field,"
                                                + queryfieldform(lblNMTPTR1.Text)
                                   + " Week1, " + queryfieldform(lblNMTPTR2.Text)
                                   + " Week2, " + queryfieldform(lblNMTPTR3.Text)
                                   + " Week3, " + queryfieldform(lblNMTPTR4.Text)
                                   + " Week4, " + queryfieldform(lblNMTPTR5.Text)
                                   + " Run1, " + queryfieldform(lblNMTPTR6.Text)
                                   + " Run2, " + queryfieldform(lblNMTPTR7.Text)
                                   + " Run3, " + queryfieldform(lblNMTPTR8.Text)
                                   + " Run4, " + queryfieldform(lblNMTPTRBEvn.Text) + " BreakEven union all ";
                query += "select 'Total Engagement Profit' rows, " + queryfieldform(txtNMTPTR.Text) + " field,"
                                               + queryfieldform(lblTEP1.Text)
                                  + " Week1, " + queryfieldform(lblTEP2.Text)
                                  + " Week2, " + queryfieldform(lblTEP3.Text)
                                  + " Week3, " + queryfieldform(lblTEP4.Text)
                                  + " Week4, " + queryfieldform(lblTEP5.Text)
                                  + " Run1, " + queryfieldform(lblTEP6.Text)
                                  + " Run2, " + queryfieldform(lblTEP7.Text)
                                  + " Run3, " + queryfieldform(lblTEP8.Text)
                                  + " Run4, " + queryfieldform(lblTEPBEvn.Text) + " BreakEven union all ";
                query += "select 'Presenter Share' rows, " + queryfieldform(txtPShare.Text) + " field,"
                                              + queryfieldform(lblPShare1.Text)
                                 + " Week1, " + queryfieldform(lblPShare2.Text)
                                 + " Week2, " + queryfieldform(lblPShare3.Text)
                                 + " Week3, " + queryfieldform(lblPShare4.Text)
                                 + " Week4, " + queryfieldform(lblPShare5.Text)
                                 + " Run1, " + queryfieldform(lblPShare6.Text)
                                 + " Run2, " + queryfieldform(lblPShare7.Text)
                                 + " Run3, " + queryfieldform(lblPShare8.Text)
                                 + " Run4, " + queryfieldform(lblPShareBEvn.Text) + " BreakEven union all ";
                query += "select 'Producer Share of split' rows, " + queryfieldform(txtPShareofsplit.Text) + " field,"
                                              + queryfieldform(lblPSharesplit1.Text)
                                 + " Week1, " + queryfieldform(lblPSharesplit2.Text)
                                 + " Week2, " + queryfieldform(lblPSharesplit3.Text)
                                 + " Week3, " + queryfieldform(lblPSharesplit4.Text)
                                 + " Week4, " + queryfieldform(lblPSharesplit5.Text)
                                 + " Run1, " + queryfieldform(lblPSharesplit6.Text)
                                 + " Run2, " + queryfieldform(lblPSharesplit7.Text)
                                 + " Run3, " + queryfieldform(lblPSharesplit8.Text)
                                 + " Run4, " + queryfieldform(lblPSharesplitBEvn.Text) + " BreakEven union all ";
                query += "select 'Middle Monies' rows, '0.00' field,"
                                             + queryfieldform(lblMiddlemonies1.Text)
                                + " Week1, " + queryfieldform(lblMiddlemonies2.Text)
                                + " Week2, " + queryfieldform(lblMiddlemonies3.Text)
                                + " Week3, " + queryfieldform(lblMiddlemonies4.Text)
                                + " Week4, " + queryfieldform(lblMiddlemonies5.Text)
                                + " Run1, " + queryfieldform(lblMiddlemonies6.Text)
                                + " Run2, " + queryfieldform(lblMiddlemonies7.Text)
                                + " Run3, " + queryfieldform(lblMiddlemonies8.Text)
                                + " Run4, " + queryfieldform(lblMiddlemoniesBEvn.Text) + " BreakEven union all ";
                query += "select 'Royalty' rows, '0.00' field,"
                                            + queryfieldform(lblRoyal1.Text)
                               + " Week1, " + queryfieldform(lblRoyal2.Text)
                               + " Week2, " + queryfieldform(lblRoyal3.Text)
                               + " Week3, " + queryfieldform(lblRoyal4.Text)
                               + " Week4, " + queryfieldform(lblRoyal5.Text)
                               + " Run1, " + queryfieldform(lblRoyal6.Text)
                               + " Run2, " + queryfieldform(lblRoyal7.Text)
                               + " Run3, " + queryfieldform(lblRoyal8.Text)
                               + " Run4, " + queryfieldform(lblRoyalBEv.Text) + " BreakEven union all ";
                query += "select 'Guarantee' rows, '0.00' field,"
                                            + queryfieldform(lblGuarante1.Text)
                               + " Week1, " + queryfieldform(lblGuarante2.Text)
                               + " Week2, " + queryfieldform(lblGuarante3.Text)
                               + " Week3, " + queryfieldform(lblGuarante4.Text)
                               + " Week4, " + queryfieldform(lblGuarante5.Text)
                               + " Run1, " + queryfieldform(lblGuarante6.Text)
                               + " Run2, " + queryfieldform(lblGuarante7.Text)
                               + " Run3, " + queryfieldform(lblGuarante8.Text)
                               + " Run4, " + queryfieldform(lblGuaranteBEvn.Text) + " BreakEven union all ";
                query += "select 'TOTAL TO PRODUCER' rows, '0.00' field,"
                                            + queryfieldform(lblTotalPR1.Text)
                               + " Week1, " + queryfieldform(lblTotalPR2.Text)
                               + " Week2, " + queryfieldform(lblTotalPR3.Text)
                               + " Week3, " + queryfieldform(lblTotalPR4.Text)
                               + " Week4, " + queryfieldform(lblTotalPR5.Text)
                               + " Run1, " + queryfieldform(lblTotalPR6.Text)
                               + " Run2, " + queryfieldform(lblTotalPR7.Text)
                               + " Run3, " + queryfieldform(lblTotalPR8.Text)
                               + " Run4, " + queryfieldform(lblTotalPRBEVn.Text) + " BreakEven union all ";
                query += "select 'Less Taxes Witheld at Source' rows, " + queryfieldform(txtLessTWS.Text) + " field,"
                                            + queryfieldform(lbllestaxwithsouce1.Text)
                               + " Week1, " + queryfieldform(lbllestaxwithsouce2.Text)
                               + " Week2, " + queryfieldform(lbllestaxwithsouce3.Text)
                               + " Week3, " + queryfieldform(lbllestaxwithsouce4.Text)
                               + " Week4, " + queryfieldform(lbllestaxwithsouce5.Text)
                               + " Run1, " + queryfieldform(lbllestaxwithsouce6.Text)
                               + " Run2, " + queryfieldform(lbllestaxwithsouce7.Text)
                               + " Run3, " + queryfieldform(lbllestaxwithsouce8.Text)
                               + " Run4, " + queryfieldform(lbllestaxwithsouceBEvn.Text) + " BreakEven union all ";
                query += "select 'Less Taxes Witheld at Source' rows, " + queryfieldform(txtLessTWamt.Text) + " field,"
                                            + queryfieldform(lblLessTWamt1.Text)
                               + " Week1, " + queryfieldform(lblLessTWamt2.Text)
                               + " Week2, " + queryfieldform(lblLessTWamt3.Text)
                               + " Week3, " + queryfieldform(lblLessTWamt4.Text)
                               + " Week4, " + queryfieldform(lblLessTWamt5.Text)
                               + " Run1, " + queryfieldform(lblLessTWamt6.Text)
                               + " Run2, " + queryfieldform(lblLessTWamt7.Text)
                               + " Run3, " + queryfieldform(lblLessTWamt8.Text)
                               + " Run4, " + queryfieldform(lblLessTWamtBEvn.Text) + " BreakEven union all ";
                query += "select 'Net Income to Producer' rows, " + queryfieldform(txtLessTWamt.Text) + " field,"
                                            + queryfieldform(lblNetincomePr1.Text)
                               + " Week1, " + queryfieldform(lblNetincomePr2.Text)
                               + " Week2, " + queryfieldform(lblNetincomePr3.Text)
                               + " Week3, " + queryfieldform(lblNetincomePr4.Text)
                               + " Week4, " + queryfieldform(lblNetincomePr5.Text)
                               + " Run1, " + queryfieldform(lblNetincomePr6.Text)
                               + " Run2, " + queryfieldform(lblNetincomePr7.Text)
                               + " Run3, " + queryfieldform(lblNetincomePr8.Text)
                               + " Run4, " + queryfieldform(lblNetincomePrBEvn.Text) + " BreakEven union all ";
                query += "select 'Weekly Operating Expenses' rows, " + queryfieldform(txtWOE.Text) + " field,"
                                            + queryfieldform(lblWOE1.Text)
                               + " Week1, " + queryfieldform(lblWOE2.Text)
                               + " Week2, " + queryfieldform(lblWOE3.Text)
                               + " Week3, " + queryfieldform(lblWOE4.Text)
                               + " Week4, " + queryfieldform(lblWOE5.Text)
                               + " Run1, " + queryfieldform(lblWOE6.Text)
                               + " Run2, " + queryfieldform(lblWOE7.Text)
                               + " Run3, " + queryfieldform(lblWOE8.Text)
                               + " Run4, " + queryfieldform(lblWOEBEvn.Text) + " BreakEven union all ";
                query += "select 'Variable Royalties' rows, " + queryfieldform(txtVR.Text) + " field,"
                                            + queryfieldform(lblVR1.Text)
                               + " Week1, " + queryfieldform(lblVR2.Text)
                               + " Week2, " + queryfieldform(lblVR3.Text)
                               + " Week3, " + queryfieldform(lblVR4.Text)
                               + " Week4, " + queryfieldform(lblVR5.Text)
                               + " Run1, " + queryfieldform(lblVR6.Text)
                               + " Run2, " + queryfieldform(lblVR7.Text)
                               + " Run3, " + queryfieldform(lblVR8.Text)
                               + " Run4, " + queryfieldform(lblVRBEvn.Text) + " BreakEven union all ";
                query += "select 'Total Show Profit' rows, '0.00' field,"
                                           + queryfieldform(txtTotalSHOW1.Text)
                              + " Week1, " + queryfieldform(txtTotalSHOW2.Text)
                              + " Week2, " + queryfieldform(txtTotalSHOW3.Text)
                              + " Week3, " + queryfieldform(txtTotalSHOW4.Text)
                              + " Week4, " + queryfieldform(txtTotalSHOW5.Text)
                              + " Run1, " + queryfieldform(txtTotalSHOW6.Text)
                              + " Run2, " + queryfieldform(txtTotalSHOW7.Text)
                              + " Run3, " + queryfieldform(txtTotalSHOW8.Text)
                              + " Run4, " + queryfieldform(txtTotalSHOWBEvn.Text) + " BreakEven ";

                DataTable dtgetBEdata = new DataTable();
                dtgetBEdata = objrpt.GetBEReportData(query);
                if (dtgetBEdata.Rows.Count > 0)
                {
                    rptBEvn.ShowCredentialPrompts = false;
                    rptBEvn.ProcessingMode = ProcessingMode.Remote;
                    String ReportServer = System.Configuration.ConfigurationManager.AppSettings["TargetServerURL"].ToString();
                    String TargetFolder = System.Configuration.ConfigurationManager.AppSettings["TargetFolder"].ToString();
                    // String TargetFolder = "/NTOS_SSRS";
                    rptBEvn.ServerReport.ReportServerUrl = new Uri(ReportServer);
                    objcf.DisableUnwantedExportFormat(rptBEvn, "Excel");
                    rptBEvn.ShowParameterPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    rptBEvn.Height = 400;
                    //DataSourceCredentials[] d = new DataSourceCredentials[1];
                    rptBEvn.ServerReport.ReportPath = TargetFolder + "/NTOS_BreakEven";
                    //rptBEvn.ServerReport.ReportPath = TargetFolder + "/NTOS_BreakEv";
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "query";
                    reportParameterCollection[0].Values.Add(Convert.ToString(query));
                    rptBEvn.ServerReport.SetParameters(reportParameterCollection);
                    rptBEvn.ServerReport.Refresh();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnClearall_Click(object sender, EventArgs e)
        {
            try
            {
                chkGuarantee.Checked = false;
                chkRoyalty.Checked = false;
                chkFixedCosts.Checked = false;
                chkAddlHouseEquipment.Checked = false;
                chkAdvertising.Checked = false;
                chkCatering.Checked = false;
                chkInsurance.Checked = false;
                chkFireWatch.Checked = false;
                chkMusicians.Checked = false;
                chkRent.Checked = false;
                chkStagehandsLoadInOut.Checked = false;
                chkStagehandsRunning.Checked = false;
                chkTicketPrinting.Checked = false;
                chkWardrobeHairLoadInOut.Checked = false;
                chkWardrobeHairRunning.Checked = false;
                chkPresenterProfit.Checked = false;
                chkOther.Checked = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "calculate();", true);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        protected void btnSelectall_Click(object sender, EventArgs e)
        {
            try
            {
                chkGuarantee.Checked = true;
                chkRoyalty.Checked = true;
                chkFixedCosts.Checked = true;
                chkAddlHouseEquipment.Checked = true;
                chkAdvertising.Checked = true;
                chkCatering.Checked = true;
                chkInsurance.Checked = true;
                chkFireWatch.Checked = true;
                chkMusicians.Checked = true;
                chkRent.Checked = true;
                chkStagehandsLoadInOut.Checked = true;
                chkStagehandsRunning.Checked = true;
                chkTicketPrinting.Checked = true;
                chkWardrobeHairLoadInOut.Checked = true;
                chkWardrobeHairRunning.Checked = true;
                chkPresenterProfit.Checked = true;
                chkOther.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "calculate();", true);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnPrintReport_Click(object sender, EventArgs e)
        {
            try
            {
                BEval();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void SetReadonly()
        {
            try
            {
                txtPerformanceCapacityBrevn.Attributes.Add("ReadOnly", "ReadOnly");
                lblHousecapacity.Attributes.Add("ReadOnly", "ReadOnly");
                lblHousecapacity1.Attributes.Add("ReadOnly", "ReadOnly");
                lblHousecapacity2.Attributes.Add("ReadOnly", "ReadOnly");
                lblHousecapacity3.Attributes.Add("ReadOnly", "ReadOnly");
                lblHousecapacity4.Attributes.Add("ReadOnly", "ReadOnly");
                lblHousecapacity5.Attributes.Add("ReadOnly", "ReadOnly");
                lblHousecapacity6.Attributes.Add("ReadOnly", "ReadOnly");
                lblHousecapacity7.Attributes.Add("ReadOnly", "ReadOnly");
                lblHousecapacity8.Attributes.Add("ReadOnly", "ReadOnly");



                lblTicketsoldweek1.Attributes.Add("ReadOnly", "ReadOnly");
                lblTicketsoldweek2.Attributes.Add("ReadOnly", "ReadOnly");
                lblTicketsoldweek3.Attributes.Add("ReadOnly", "ReadOnly");
                lblTicketsoldweek4.Attributes.Add("ReadOnly", "ReadOnly");
                lblTicketsoldRun1.Attributes.Add("ReadOnly", "ReadOnly");
                lblTicketsoldRun2.Attributes.Add("ReadOnly", "ReadOnly");
                lblTicketsoldRun3.Attributes.Add("ReadOnly", "ReadOnly");
                lblTicketsoldRun4.Attributes.Add("ReadOnly", "ReadOnly");
                lblTicketsoldBreakeven.Attributes.Add("ReadOnly", "ReadOnly");
                lblSubloadin.Attributes.Add("ReadOnly", "ReadOnly");
                lblSubloadin1.Attributes.Add("ReadOnly", "ReadOnly");
                lblSubloadin2.Attributes.Add("ReadOnly", "ReadOnly");
                lblSubloadin3.Attributes.Add("ReadOnly", "ReadOnly");
                lblSubloadin4.Attributes.Add("ReadOnly", "ReadOnly");
                lblSubloadin5.Attributes.Add("ReadOnly", "ReadOnly");
                lblSubloadin6.Attributes.Add("ReadOnly", "ReadOnly");
                lblSubloadin7.Attributes.Add("ReadOnly", "ReadOnly");
                lblSubloadinBreak.Attributes.Add("ReadOnly", "ReadOnly");

                lblBoxofcgrosssale.Attributes.Add("ReadOnly", "ReadOnly");
                lblBoxofcgrosssale1.Attributes.Add("ReadOnly", "ReadOnly");
                lblBoxofcgrosssale2.Attributes.Add("ReadOnly", "ReadOnly"); lblBoxofcgrosssale3.Attributes.Add("ReadOnly", "ReadOnly");
                lblBoxofcgrosssale4.Attributes.Add("ReadOnly", "ReadOnly"); lblBoxofcgrosssale5.Attributes.Add("ReadOnly", "ReadOnly");
                lblBoxofcgrosssale6.Attributes.Add("ReadOnly", "ReadOnly"); lblBoxofcgrosssale7.Attributes.Add("ReadOnly", "ReadOnly");
                lblBoxofcgrosssalebreakeven.Attributes.Add("ReadOnly", "ReadOnly");

                lblLessdiscountWeek1.Attributes.Add("ReadOnly", "ReadOnly");
                lblLessdiscountWeek2.Attributes.Add("ReadOnly", "ReadOnly");
                lblLessdiscountWeek3.Attributes.Add("ReadOnly", "ReadOnly"); lblLessdiscountWeek4.Attributes.Add("ReadOnly", "ReadOnly");
                lblLessdiscountWeek5.Attributes.Add("ReadOnly", "ReadOnly"); lblLessdiscountWeek6.Attributes.Add("ReadOnly", "ReadOnly");
                lblLessdiscountWeek7.Attributes.Add("ReadOnly", "ReadOnly"); lblLessdiscountWeek8.Attributes.Add("ReadOnly", "ReadOnly");
                lblLessdiscountBreakeven.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdjustedgrossWeekly1.Attributes.Add("ReadOnly", "ReadOnly"); lblAdjustedgrossWeekly2.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdjustedgrossWeekly3.Attributes.Add("ReadOnly", "ReadOnly"); lblAdjustedgrossWeekly4.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdjustedgrossWeekly5.Attributes.Add("ReadOnly", "ReadOnly"); lblAdjustedgrossWeekly6.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdjustedgrossWeekly7.Attributes.Add("ReadOnly", "ReadOnly"); lblAdjustedgrossWeekly8.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdjustedgrossBreakeven.Attributes.Add("ReadOnly", "ReadOnly");
                // txtTax.Attributes.Add("ReadOnly", "ReadOnly");
                lblTax1.Attributes.Add("ReadOnly", "ReadOnly");
                lblTax2.Attributes.Add("ReadOnly", "ReadOnly");
                lblTax3.Attributes.Add("ReadOnly", "ReadOnly");
                lblTax4.Attributes.Add("ReadOnly", "ReadOnly");
                lblTax5.Attributes.Add("ReadOnly", "ReadOnly");
                lblTax6.Attributes.Add("ReadOnly", "ReadOnly");
                lblTax7.Attributes.Add("ReadOnly", "ReadOnly");
                lblTax8.Attributes.Add("ReadOnly", "ReadOnly");
                lblTax8.Attributes.Add("ReadOnly", "ReadOnly");
                //txtRestoration.Attributes.Add("ReadOnly", "ReadOnly");
                lblRestoration1.Attributes.Add("ReadOnly", "ReadOnly");
                lblRestoration2.Attributes.Add("ReadOnly", "ReadOnly");
                lblRestoration3.Attributes.Add("ReadOnly", "ReadOnly");
                lblRestoration4.Attributes.Add("ReadOnly", "ReadOnly");
                lblRestoration5.Attributes.Add("ReadOnly", "ReadOnly");
                lblRestoration6.Attributes.Add("ReadOnly", "ReadOnly");
                lblRestoration7.Attributes.Add("ReadOnly", "ReadOnly");
                lblRestoration8.Attributes.Add("ReadOnly", "ReadOnly");
                lblRestorationBreakeven.Attributes.Add("ReadOnly", "ReadOnly");
                //txtSubscriptioncharge.Attributes.Add("ReadOnly", "ReadOnly");
                lblSubscriptioncharge1.Attributes.Add("ReadOnly", "ReadOnly");
                lblSubscriptioncharge2.Attributes.Add("ReadOnly", "ReadOnly");
                lblSubscriptioncharge3.Attributes.Add("ReadOnly", "ReadOnly");
                lblSubscriptioncharge4.Attributes.Add("ReadOnly", "ReadOnly");
                lblSubscriptioncharge5.Attributes.Add("ReadOnly", "ReadOnly");
                lblSubscriptioncharge6.Attributes.Add("ReadOnly", "ReadOnly");
                lblSubscriptioncharge7.Attributes.Add("ReadOnly", "ReadOnly");
                lblSubscriptioncharge8.Attributes.Add("ReadOnly", "ReadOnly");
                lblSubscriptionchargeBreakeven.Attributes.Add("ReadOnly", "ReadOnly");
                // txtCCothercommissions.Attributes.Add("ReadOnly", "ReadOnly");
                lblCCothercommssns1.Attributes.Add("ReadOnly", "ReadOnly");
                lblCCothercommssns2.Attributes.Add("ReadOnly", "ReadOnly");
                lblCCothercommssns3.Attributes.Add("ReadOnly", "ReadOnly");
                lblCCothercommssns4.Attributes.Add("ReadOnly", "ReadOnly");
                lblCCothercommssns5.Attributes.Add("ReadOnly", "ReadOnly");
                lblCCothercommssns6.Attributes.Add("ReadOnly", "ReadOnly");
                lblCCothercommssns7.Attributes.Add("ReadOnly", "ReadOnly");
                lblCCothercommssns8.Attributes.Add("ReadOnly", "ReadOnly");
                lblCCothercommssnsBreakeven.Attributes.Add("ReadOnly", "ReadOnly");

                lblNetAR1.Attributes.Add("ReadOnly", "ReadOnly");
                lblNetAR2.Attributes.Add("ReadOnly", "ReadOnly");
                lblNetAR3.Attributes.Add("ReadOnly", "ReadOnly");
                lblNetAR4.Attributes.Add("ReadOnly", "ReadOnly");
                lblNetAR5.Attributes.Add("ReadOnly", "ReadOnly");
                lblNetAR6.Attributes.Add("ReadOnly", "ReadOnly");
                lblNetAR7.Attributes.Add("ReadOnly", "ReadOnly");
                lblNetAR8.Attributes.Add("ReadOnly", "ReadOnly");
                lblNetARBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                // txtGuarantee.Attributes.Add("ReadOnly", "ReadOnly");
                lblGuarantee1.Attributes.Add("ReadOnly", "ReadOnly");
                lblGuarantee2.Attributes.Add("ReadOnly", "ReadOnly");
                lblGuarantee3.Attributes.Add("ReadOnly", "ReadOnly");
                lblGuarantee4.Attributes.Add("ReadOnly", "ReadOnly");
                lblGuarantee5.Attributes.Add("ReadOnly", "ReadOnly");
                lblGuarantee6.Attributes.Add("ReadOnly", "ReadOnly");
                lblGuarantee7.Attributes.Add("ReadOnly", "ReadOnly");
                lblGuarantee8.Attributes.Add("ReadOnly", "ReadOnly");
                lblGuaranteeBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                //txtRoyalty.Attributes.Add("ReadOnly", "ReadOnly");
                lblRoyalty1.Attributes.Add("ReadOnly", "ReadOnly");
                lblRoyalty2.Attributes.Add("ReadOnly", "ReadOnly");
                lblRoyalty3.Attributes.Add("ReadOnly", "ReadOnly");
                lblRoyalty4.Attributes.Add("ReadOnly", "ReadOnly");
                lblRoyalty5.Attributes.Add("ReadOnly", "ReadOnly");
                lblRoyalty6.Attributes.Add("ReadOnly", "ReadOnly");
                lblRoyalty7.Attributes.Add("ReadOnly", "ReadOnly");
                lblRoyalty8.Attributes.Add("ReadOnly", "ReadOnly");
                lblRoyaltyBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                // txtFixedCosts.Attributes.Add("ReadOnly", "ReadOnly");
                lblFixedcosts1.Attributes.Add("ReadOnly", "ReadOnly");
                lblFixedcosts2.Attributes.Add("ReadOnly", "ReadOnly");
                lblFixedcosts3.Attributes.Add("ReadOnly", "ReadOnly");
                lblFixedcosts4.Attributes.Add("ReadOnly", "ReadOnly");
                lblFixedcosts5.Attributes.Add("ReadOnly", "ReadOnly");
                lblFixedcosts6.Attributes.Add("ReadOnly", "ReadOnly");
                lblFixedcosts7.Attributes.Add("ReadOnly", "ReadOnly");
                lblFixedcosts8.Attributes.Add("ReadOnly", "ReadOnly");
                lblFixedcostsBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                // txtAdHouseEquipment.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdHouseEquipment1.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdHouseEquipment2.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdHouseEquipment3.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdHouseEquipment4.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdHouseEquipment5.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdHouseEquipment6.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdHouseEquipment7.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdHouseEquipment8.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdHouseEquipmentBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                //txtAdvertising.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdvertising1.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdvertising2.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdvertising3.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdvertising4.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdvertising5.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdvertising6.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdvertising7.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdvertising8.Attributes.Add("ReadOnly", "ReadOnly");
                lblAdvertisingBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                // txtCatering.Attributes.Add("ReadOnly", "ReadOnly");
                lblCatering1.Attributes.Add("ReadOnly", "ReadOnly");
                lblCatering2.Attributes.Add("ReadOnly", "ReadOnly");
                lblCatering3.Attributes.Add("ReadOnly", "ReadOnly");
                lblCatering4.Attributes.Add("ReadOnly", "ReadOnly");
                lblCatering5.Attributes.Add("ReadOnly", "ReadOnly");
                lblCatering6.Attributes.Add("ReadOnly", "ReadOnly");
                lblCatering7.Attributes.Add("ReadOnly", "ReadOnly");
                lblCatering8.Attributes.Add("ReadOnly", "ReadOnly");
                lblCateringBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                // txtInsurance.Attributes.Add("ReadOnly", "ReadOnly");
                lblInsurance1.Attributes.Add("ReadOnly", "ReadOnly");
                lblInsurance2.Attributes.Add("ReadOnly", "ReadOnly");
                lblInsurance3.Attributes.Add("ReadOnly", "ReadOnly");
                lblInsurance4.Attributes.Add("ReadOnly", "ReadOnly");
                lblInsurance5.Attributes.Add("ReadOnly", "ReadOnly");
                lblInsurance6.Attributes.Add("ReadOnly", "ReadOnly");
                lblInsurance7.Attributes.Add("ReadOnly", "ReadOnly");
                lblInsurance8.Attributes.Add("ReadOnly", "ReadOnly");
                lblInsuranceBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                //txtFireWatch.Attributes.Add("ReadOnly", "ReadOnly");
                lblFirewatch1.Attributes.Add("ReadOnly", "ReadOnly");
                lblFirewatch2.Attributes.Add("ReadOnly", "ReadOnly");
                lblFirewatch3.Attributes.Add("ReadOnly", "ReadOnly");
                lblFirewatch4.Attributes.Add("ReadOnly", "ReadOnly");
                lblFirewatch5.Attributes.Add("ReadOnly", "ReadOnly");
                lblFirewatch6.Attributes.Add("ReadOnly", "ReadOnly");
                lblFirewatch7.Attributes.Add("ReadOnly", "ReadOnly");
                lblFirewatch8.Attributes.Add("ReadOnly", "ReadOnly");
                lblFirewatchBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                // txtMusicians.Attributes.Add("ReadOnly", "ReadOnly");
                lblMusicians1.Attributes.Add("ReadOnly", "ReadOnly");
                lblMusicians2.Attributes.Add("ReadOnly", "ReadOnly");
                lblMusicians3.Attributes.Add("ReadOnly", "ReadOnly");
                lblMusicians4.Attributes.Add("ReadOnly", "ReadOnly");
                lblMusicians5.Attributes.Add("ReadOnly", "ReadOnly");
                lblMusicians6.Attributes.Add("ReadOnly", "ReadOnly");
                lblMusicians7.Attributes.Add("ReadOnly", "ReadOnly");
                lblMusicians8.Attributes.Add("ReadOnly", "ReadOnly");
                lblMusiciansBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                //txtRent.Attributes.Add("ReadOnly", "ReadOnly");
                lblRent1.Attributes.Add("ReadOnly", "ReadOnly");
                lblRent2.Attributes.Add("ReadOnly", "ReadOnly");
                lblRent3.Attributes.Add("ReadOnly", "ReadOnly");
                lblRent4.Attributes.Add("ReadOnly", "ReadOnly");
                lblRent5.Attributes.Add("ReadOnly", "ReadOnly");
                lblRent6.Attributes.Add("ReadOnly", "ReadOnly");
                lblRent7.Attributes.Add("ReadOnly", "ReadOnly");
                lblRent8.Attributes.Add("ReadOnly", "ReadOnly");
                lblRentBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                // txtStagehandsloadin.Attributes.Add("ReadOnly", "ReadOnly");
                lblStagehandslodin1.Attributes.Add("ReadOnly", "ReadOnly");
                lblStagehandslodin2.Attributes.Add("ReadOnly", "ReadOnly");
                lblStagehandslodin3.Attributes.Add("ReadOnly", "ReadOnly");
                lblStagehandslodin4.Attributes.Add("ReadOnly", "ReadOnly");
                lblStagehandslodin5.Attributes.Add("ReadOnly", "ReadOnly");
                lblStagehandslodin6.Attributes.Add("ReadOnly", "ReadOnly");
                lblStagehandslodin7.Attributes.Add("ReadOnly", "ReadOnly");
                lblStagehandslodin8.Attributes.Add("ReadOnly", "ReadOnly");
                lblStagehandslodinBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                // txtStagehandsrunning.Attributes.Add("ReadOnly", "ReadOnly");
                lblStagehandsrunning1.Attributes.Add("ReadOnly", "ReadOnly");
                lblStagehandsrunning2.Attributes.Add("ReadOnly", "ReadOnly");
                lblStagehandsrunning3.Attributes.Add("ReadOnly", "ReadOnly");
                lblStagehandsrunning4.Attributes.Add("ReadOnly", "ReadOnly");
                lblStagehandsrunning5.Attributes.Add("ReadOnly", "ReadOnly");
                lblStagehandsrunning6.Attributes.Add("ReadOnly", "ReadOnly");
                lblStagehandsrunning7.Attributes.Add("ReadOnly", "ReadOnly");
                lblStagehandsrunning8.Attributes.Add("ReadOnly", "ReadOnly");
                lblStagehandsrunningBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                //txtTicketprinting.Attributes.Add("ReadOnly", "ReadOnly");
                lblTicketPrinting1.Attributes.Add("ReadOnly", "ReadOnly");
                lblTicketPrinting2.Attributes.Add("ReadOnly", "ReadOnly");
                lblTicketPrinting3.Attributes.Add("ReadOnly", "ReadOnly");
                lblTicketPrinting4.Attributes.Add("ReadOnly", "ReadOnly");
                lblTicketPrinting5.Attributes.Add("ReadOnly", "ReadOnly");
                lblTicketPrinting6.Attributes.Add("ReadOnly", "ReadOnly");
                lblTicketPrinting7.Attributes.Add("ReadOnly", "ReadOnly");
                lblTicketPrinting8.Attributes.Add("ReadOnly", "ReadOnly");
                lblTicketPrintingBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                // txtWardrobehairloadin.Attributes.Add("ReadOnly", "ReadOnly");
                lblWardrobehairloadin1.Attributes.Add("ReadOnly", "ReadOnly");
                lblWardrobehairloadin2.Attributes.Add("ReadOnly", "ReadOnly");
                lblWardrobehairloadin3.Attributes.Add("ReadOnly", "ReadOnly");
                lblWardrobehairloadin4.Attributes.Add("ReadOnly", "ReadOnly");
                lblWardrobehairloadin5.Attributes.Add("ReadOnly", "ReadOnly");
                lblWardrobehairloadin6.Attributes.Add("ReadOnly", "ReadOnly");
                lblWardrobehairloadin7.Attributes.Add("ReadOnly", "ReadOnly");
                lblWardrobehairloadin8.Attributes.Add("ReadOnly", "ReadOnly");
                lblWardrobehairloadinBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                //txtWardrobehairrunning.Attributes.Add("ReadOnly", "ReadOnly");
                lblWardrobehairrunning1.Attributes.Add("ReadOnly", "ReadOnly");
                lblWardrobehairrunning2.Attributes.Add("ReadOnly", "ReadOnly");
                lblWardrobehairrunning3.Attributes.Add("ReadOnly", "ReadOnly");
                lblWardrobehairrunning4.Attributes.Add("ReadOnly", "ReadOnly");
                lblWardrobehairrunning5.Attributes.Add("ReadOnly", "ReadOnly");
                lblWardrobehairrunning6.Attributes.Add("ReadOnly", "ReadOnly");
                lblWardrobehairrunning7.Attributes.Add("ReadOnly", "ReadOnly");
                lblWardrobehairrunning8.Attributes.Add("ReadOnly", "ReadOnly");
                lblWardrobehairrunningBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                // txtPF.Attributes.Add("ReadOnly", "ReadOnly");
                lblPF1.Attributes.Add("ReadOnly", "ReadOnly");
                lblPF2.Attributes.Add("ReadOnly", "ReadOnly");
                lblPF3.Attributes.Add("ReadOnly", "ReadOnly");
                lblPF4.Attributes.Add("ReadOnly", "ReadOnly");
                lblPF5.Attributes.Add("ReadOnly", "ReadOnly");
                lblPF6.Attributes.Add("ReadOnly", "ReadOnly");
                lblPF7.Attributes.Add("ReadOnly", "ReadOnly");
                lblPF8.Attributes.Add("ReadOnly", "ReadOnly");
                lblPFBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                //txtOther.Attributes.Add("ReadOnly", "ReadOnly");
                lblOther1.Attributes.Add("ReadOnly", "ReadOnly");
                lblOther2.Attributes.Add("ReadOnly", "ReadOnly");
                lblOther3.Attributes.Add("ReadOnly", "ReadOnly");
                lblOther4.Attributes.Add("ReadOnly", "ReadOnly");
                lblOther5.Attributes.Add("ReadOnly", "ReadOnly");
                lblOther6.Attributes.Add("ReadOnly", "ReadOnly");
                lblOther7.Attributes.Add("ReadOnly", "ReadOnly");
                lblOther8.Attributes.Add("ReadOnly", "ReadOnly");
                lblOtherBEvn.Attributes.Add("ReadOnly", "ReadOnly");

                lblTotalLExp1.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalLExp2.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalLExp3.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalLExp4.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalLExp5.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalLExp6.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalLExp7.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalLExp8.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalLExpBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalLExp1.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalLExp2.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalLExp3.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalLExp4.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalLExp5.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalLExp6.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalLExp7.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalLExp8.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalLExpBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                lblMoneyRemaining1.Attributes.Add("ReadOnly", "ReadOnly");
                lblMoneyRemaining2.Attributes.Add("ReadOnly", "ReadOnly");
                lblMoneyRemaining3.Attributes.Add("ReadOnly", "ReadOnly");
                lblMoneyRemaining4.Attributes.Add("ReadOnly", "ReadOnly");
                lblMoneyRemaining5.Attributes.Add("ReadOnly", "ReadOnly");
                lblMoneyRemaining6.Attributes.Add("ReadOnly", "ReadOnly");
                lblMoneyRemaining7.Attributes.Add("ReadOnly", "ReadOnly");
                lblMoneyRemaining8.Attributes.Add("ReadOnly", "ReadOnly");
                lblMoneyRemainingBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                //txtNMTP.Attributes.Add("ReadOnly", "ReadOnly");
                lblNMTP1.Attributes.Add("ReadOnly", "ReadOnly");
                lblNMTP2.Attributes.Add("ReadOnly", "ReadOnly");
                lblNMTP3.Attributes.Add("ReadOnly", "ReadOnly");
                lblNMTP4.Attributes.Add("ReadOnly", "ReadOnly");
                lblNMTP5.Attributes.Add("ReadOnly", "ReadOnly");
                lblNMTP6.Attributes.Add("ReadOnly", "ReadOnly");
                lblNMTP7.Attributes.Add("ReadOnly", "ReadOnly");
                lblNMTP8.Attributes.Add("ReadOnly", "ReadOnly");
                lblNMTPBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                // txtNMTPTR.Attributes.Add("ReadOnly", "ReadOnly");
                lblNMTPTR1.Attributes.Add("ReadOnly", "ReadOnly");
                lblNMTPTR2.Attributes.Add("ReadOnly", "ReadOnly");
                lblNMTPTR3.Attributes.Add("ReadOnly", "ReadOnly");
                lblNMTPTR4.Attributes.Add("ReadOnly", "ReadOnly");
                lblNMTPTR5.Attributes.Add("ReadOnly", "ReadOnly");
                lblNMTPTR6.Attributes.Add("ReadOnly", "ReadOnly");
                lblNMTPTR7.Attributes.Add("ReadOnly", "ReadOnly");
                lblNMTPTR8.Attributes.Add("ReadOnly", "ReadOnly");
                lblNMTPTRBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                //txtNMTPTR.Attributes.Add("ReadOnly", "ReadOnly");
                lblTEP1.Attributes.Add("ReadOnly", "ReadOnly");
                lblTEP2.Attributes.Add("ReadOnly", "ReadOnly");
                lblTEP3.Attributes.Add("ReadOnly", "ReadOnly");
                lblTEP4.Attributes.Add("ReadOnly", "ReadOnly");
                lblTEP5.Attributes.Add("ReadOnly", "ReadOnly");
                lblTEP6.Attributes.Add("ReadOnly", "ReadOnly");
                lblTEP7.Attributes.Add("ReadOnly", "ReadOnly");
                lblTEP8.Attributes.Add("ReadOnly", "ReadOnly");
                lblTEPBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                // txtPShare.Attributes.Add("ReadOnly", "ReadOnly");
                lblPShare1.Attributes.Add("ReadOnly", "ReadOnly");
                lblPShare2.Attributes.Add("ReadOnly", "ReadOnly");
                lblPShare3.Attributes.Add("ReadOnly", "ReadOnly");
                lblPShare4.Attributes.Add("ReadOnly", "ReadOnly");
                lblPShare5.Attributes.Add("ReadOnly", "ReadOnly");
                lblPShare6.Attributes.Add("ReadOnly", "ReadOnly");
                lblPShare7.Attributes.Add("ReadOnly", "ReadOnly");
                lblPShare8.Attributes.Add("ReadOnly", "ReadOnly");
                lblPShareBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                // txtPShareofsplit.Attributes.Add("ReadOnly", "ReadOnly");
                lblPSharesplit1.Attributes.Add("ReadOnly", "ReadOnly");
                lblPSharesplit2.Attributes.Add("ReadOnly", "ReadOnly");
                lblPSharesplit3.Attributes.Add("ReadOnly", "ReadOnly");
                lblPSharesplit4.Attributes.Add("ReadOnly", "ReadOnly");
                lblPSharesplit5.Attributes.Add("ReadOnly", "ReadOnly");
                lblPSharesplit6.Attributes.Add("ReadOnly", "ReadOnly");
                lblPSharesplit7.Attributes.Add("ReadOnly", "ReadOnly");
                lblPSharesplit8.Attributes.Add("ReadOnly", "ReadOnly");
                lblPSharesplitBEvn.Attributes.Add("ReadOnly", "ReadOnly");

                lblMiddlemonies1.Attributes.Add("ReadOnly", "ReadOnly");
                lblMiddlemonies2.Attributes.Add("ReadOnly", "ReadOnly");
                lblMiddlemonies3.Attributes.Add("ReadOnly", "ReadOnly");
                lblMiddlemonies4.Attributes.Add("ReadOnly", "ReadOnly");
                lblMiddlemonies5.Attributes.Add("ReadOnly", "ReadOnly");
                lblMiddlemonies6.Attributes.Add("ReadOnly", "ReadOnly");
                lblMiddlemonies7.Attributes.Add("ReadOnly", "ReadOnly");
                lblMiddlemonies8.Attributes.Add("ReadOnly", "ReadOnly");
                lblMiddlemoniesBEvn.Attributes.Add("ReadOnly", "ReadOnly");

                lblRoyal1.Attributes.Add("ReadOnly", "ReadOnly");
                lblRoyal2.Attributes.Add("ReadOnly", "ReadOnly");
                lblRoyal3.Attributes.Add("ReadOnly", "ReadOnly");
                lblRoyal4.Attributes.Add("ReadOnly", "ReadOnly");
                lblRoyal5.Attributes.Add("ReadOnly", "ReadOnly");
                lblRoyal6.Attributes.Add("ReadOnly", "ReadOnly");
                lblRoyal7.Attributes.Add("ReadOnly", "ReadOnly");
                lblRoyal8.Attributes.Add("ReadOnly", "ReadOnly");
                lblRoyalBEv.Attributes.Add("ReadOnly", "ReadOnly");

                lblGuarante1.Attributes.Add("ReadOnly", "ReadOnly");
                lblGuarante2.Attributes.Add("ReadOnly", "ReadOnly");
                lblGuarante3.Attributes.Add("ReadOnly", "ReadOnly");
                lblGuarante4.Attributes.Add("ReadOnly", "ReadOnly");
                lblGuarante5.Attributes.Add("ReadOnly", "ReadOnly");
                lblGuarante6.Attributes.Add("ReadOnly", "ReadOnly");
                lblGuarante7.Attributes.Add("ReadOnly", "ReadOnly");
                lblGuarante8.Attributes.Add("ReadOnly", "ReadOnly");
                lblGuaranteBEvn.Attributes.Add("ReadOnly", "ReadOnly");

                lblTotalPR1.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalPR2.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalPR3.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalPR4.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalPR5.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalPR6.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalPR7.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalPR8.Attributes.Add("ReadOnly", "ReadOnly");
                lblTotalPRBEVn.Attributes.Add("ReadOnly", "ReadOnly");
                //txtLessTWS.Attributes.Add("ReadOnly", "ReadOnly");
                lbllestaxwithsouce1.Attributes.Add("ReadOnly", "ReadOnly");
                lbllestaxwithsouce2.Attributes.Add("ReadOnly", "ReadOnly");
                lbllestaxwithsouce3.Attributes.Add("ReadOnly", "ReadOnly");
                lbllestaxwithsouce4.Attributes.Add("ReadOnly", "ReadOnly");
                lbllestaxwithsouce5.Attributes.Add("ReadOnly", "ReadOnly");
                lbllestaxwithsouce6.Attributes.Add("ReadOnly", "ReadOnly");
                lbllestaxwithsouce7.Attributes.Add("ReadOnly", "ReadOnly");
                lbllestaxwithsouce8.Attributes.Add("ReadOnly", "ReadOnly");
                lbllestaxwithsouceBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                //txtLessTWamt.Attributes.Add("ReadOnly", "ReadOnly");
                lblLessTWamt1.Attributes.Add("ReadOnly", "ReadOnly");
                lblLessTWamt2.Attributes.Add("ReadOnly", "ReadOnly");
                lblLessTWamt3.Attributes.Add("ReadOnly", "ReadOnly");
                lblLessTWamt4.Attributes.Add("ReadOnly", "ReadOnly");
                lblLessTWamt5.Attributes.Add("ReadOnly", "ReadOnly");
                lblLessTWamt6.Attributes.Add("ReadOnly", "ReadOnly");
                lblLessTWamt7.Attributes.Add("ReadOnly", "ReadOnly");
                lblLessTWamt8.Attributes.Add("ReadOnly", "ReadOnly");
                lblLessTWamtBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                // txtLessTWamt.Attributes.Add("ReadOnly", "ReadOnly");
                lblNetincomePr1.Attributes.Add("ReadOnly", "ReadOnly");
                lblNetincomePr2.Attributes.Add("ReadOnly", "ReadOnly");
                lblNetincomePr3.Attributes.Add("ReadOnly", "ReadOnly");
                lblNetincomePr4.Attributes.Add("ReadOnly", "ReadOnly");
                lblNetincomePr5.Attributes.Add("ReadOnly", "ReadOnly");
                lblNetincomePr6.Attributes.Add("ReadOnly", "ReadOnly");
                lblNetincomePr7.Attributes.Add("ReadOnly", "ReadOnly");
                lblNetincomePr8.Attributes.Add("ReadOnly", "ReadOnly");
                lblNetincomePrBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                //txtWOE.Attributes.Add("ReadOnly", "ReadOnly");
                lblWOE1.Attributes.Add("ReadOnly", "ReadOnly");
                lblWOE2.Attributes.Add("ReadOnly", "ReadOnly");
                lblWOE3.Attributes.Add("ReadOnly", "ReadOnly");
                lblWOE4.Attributes.Add("ReadOnly", "ReadOnly");
                lblWOE5.Attributes.Add("ReadOnly", "ReadOnly");
                lblWOE6.Attributes.Add("ReadOnly", "ReadOnly");
                lblWOE7.Attributes.Add("ReadOnly", "ReadOnly");
                lblWOE8.Attributes.Add("ReadOnly", "ReadOnly");
                lblWOEBEvn.Attributes.Add("ReadOnly", "ReadOnly");
                // txtVR.Attributes.Add("ReadOnly", "ReadOnly");
                lblVR1.Attributes.Add("ReadOnly", "ReadOnly");
                lblVR2.Attributes.Add("ReadOnly", "ReadOnly");
                lblVR3.Attributes.Add("ReadOnly", "ReadOnly");
                lblVR4.Attributes.Add("ReadOnly", "ReadOnly");
                lblVR5.Attributes.Add("ReadOnly", "ReadOnly");
                lblVR6.Attributes.Add("ReadOnly", "ReadOnly");
                lblVR7.Attributes.Add("ReadOnly", "ReadOnly");
                lblVR8.Attributes.Add("ReadOnly", "ReadOnly");
                lblVRBEvn.Attributes.Add("ReadOnly", "ReadOnly");

                txtTotalSHOW1.Attributes.Add("ReadOnly", "ReadOnly");
                txtTotalSHOW2.Attributes.Add("ReadOnly", "ReadOnly");
                txtTotalSHOW3.Attributes.Add("ReadOnly", "ReadOnly");
                txtTotalSHOW4.Attributes.Add("ReadOnly", "ReadOnly");
                txtTotalSHOW5.Attributes.Add("ReadOnly", "ReadOnly");
                txtTotalSHOW6.Attributes.Add("ReadOnly", "ReadOnly");
                txtTotalSHOW7.Attributes.Add("ReadOnly", "ReadOnly");
                txtTotalSHOW8.Attributes.Add("ReadOnly", "ReadOnly");
                txtTotalSHOWBEvn.Attributes.Add("ReadOnly", "ReadOnly");

                //                txtLessDiscounts
                //txtTax
                //txtRestoration
                //txtSubscriptioncharge
                //txtCCothercommissions
                //txtGuarantee
                //txtRoyalty
                //txtFixedCosts
                //txtAdHouseEquipment
                //txtAdvertising
                //txtCatering
                //txtInsurance
                //txtFireWatch
                //txtMusicians
                //txtRent
                //txtStagehandsloadin
                //txtStagehandsrunning
                //txtTicketprinting
                //txtWardrobehairloadin
                //txtWardrobehairrunning
                //txtPF
                //txtOther
                //txtNMTP
                //txtNMTPTR
                //txtPShare
                //txtPShareofsplit
                //txtLessTWS
                //txtLessTWamt
                //txtWOE
                //txtVR


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void clear(string type)
        {
            lblEngtEndDate.Text = string.Empty;            
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
    }
}