using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using MasterDataLayer;
namespace NTOS
{
    public partial class ExcelImport : System.Web.UI.Page
    {
        public static string serverpath = System.Web.HttpContext.Current.Server.MapPath(".");
        string ValidationExcelTemplate = serverpath + @"\Reports\ExcelTemplates\Validation Report.xlsx";
        string ValidationExcelPath = serverpath + @"\Reports\ExcelImport\Validation Report.xlsx";
        string ValidationInputPath = serverpath + @"\Reports\ExcelImport\Sheets\Validation_input.xlsx";
        //public static string ExcelFolderPath = serverpath + @"\Reports\ExcelImport\Sheets\\";
        public static string ExcelFolderPath = @"C:\testing\Excel Sheets\Sheets\\";
        string Saveaspath = @"D:\Siva\NTOS\Data Conversion\saveas\";
        CommonFunction.CommonFun objcf = new CommonFunction.CommonFun();
        MasterData objmst = new MasterData();
        int valstatus = 0, AllNew = 0, WriteError = 0;
        string dwn_path = "http:/localhost:54006/";
        Microsoft.Office.Interop.Excel.Application ExApp = new Microsoft.Office.Interop.Excel.Application();
        Excel.Workbook ExWorkbook = null;
        Excel.Sheets ExSheets = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            lnkvalexcel.Visible = false;
            lnkexcelclms.Visible = false;
            lblErrmsg.Text = string.Empty;
            if (!Page.IsPostBack)
            {
                String ExcelPath = System.Configuration.ConfigurationManager.AppSettings["ExcelPath"].ToString();
                lnkvalexcel.HRef = @"/Reports/ExcelImport/Validation Report.xlsx";
                lnkexcelclms.HRef = @"/Reports/ExcelImport/Validation Report.xlsx";
                hdnvalidation_status.Value = "0";
                Kill_Excel_Process();
                //ReadFolderFiles();
            }
        }
        public void Excelvalidation(string path, int sno, Excel.Sheets val_sheetlist)
        {
            #region Initial
            Excel.Worksheet valsheet = (Excel.Worksheet)val_sheetlist[1];
            FileInfo finfo = new FileInfo(path);
            Microsoft.Office.Interop.Excel.Application xlApp = null;
            Excel.Workbook xlWorkbook = null;
            Excel.Sheets xlSheets = null;
            Excel.Worksheet xlNewSheet = null;
            Excel.Worksheet xlsummarysheet = null;
            Excel.Worksheet xlscvsheet = null;
            string ErrMsg = "", hidelist = "";
            int i = 1;
            try
            {
                valsheet.Cells[sno, 1] = finfo.Name;
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                    return;
                xlWorkbook = xlApp.Workbooks.Open(path, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

                int boindex = 0, sumindex = 0, cvrindex = 0;
                for (int l = 1; l <= xlWorkbook.Sheets.Count; l++)
                {
                    if (xlWorkbook.Sheets[l].Name.ToString().Trim().Replace(" ", "").ToLower() == "boxoffice")
                    {
                        boindex = l;
                    }
                    if (xlWorkbook.Sheets[l].Name.ToString().Trim().Replace(" ", "").ToLower() == "coversheet")
                    {
                        cvrindex = l;
                    }
                    if (xlWorkbook.Sheets[l].Name.ToString().Trim().Replace(" ", "").ToLower() == "summary")
                    {
                        sumindex = l;
                    }
                }
                xlSheets = xlWorkbook.Sheets as Excel.Sheets;
                if (boindex != 0)
                {
                    xlNewSheet = (Excel.Worksheet)xlSheets[boindex];
                    xlNewSheet.Unprotect("7135");
                }
                if (boindex != 0)
                {
                    xlsummarysheet = (Excel.Worksheet)xlSheets[sumindex];
                    xlsummarysheet.Unprotect("7135");
                }
            #endregion


                if (1 == valstatus)
                {
                    hidelist = "2,3";
                    lnkvalexcel.Visible = true;
                    #region Validate process
                    if (xlNewSheet.ProtectContents)
                    {
                        ErrMsg = "File is protcted";
                    }
                    else if (boindex == 0 && sumindex == 0 && cvrindex == 0)
                    {
                        ErrMsg = "Sheet name not found!";
                    }
                    //if (!xlNewSheet.ProtectContents && (boindex != 0 && sumindex != 0 && cvrindex != 0))
                    if ((boindex != 0 && sumindex != 0 && cvrindex != 0))
                    {
                        string sch_day = "", sch_date = "", sch_time = "", tot_att = "", gross_sales = "", tot_exp = "", gross_rec = "",
                                                Discount = "", SummaryMsg = "";
                        string royalty = "", gurantee = "", var_exp_act = "", share1 = "", share2 = "", share3 = "", iferr = "";
                        if (WriteError == 0)
                        {
                            #region Write Error
                            royalty = xlsummarysheet.Cells[31, 2].Text.Trim();
                            gurantee = xlsummarysheet.Cells[34, 2].Text.Trim();
                            var_exp_act = xlsummarysheet.Cells[54, 2].Text.Trim();
                            share1 = xlsummarysheet.Cells[90, 2].Text.Trim();
                            share2 = xlsummarysheet.Cells[93, 2].Text.Trim();
                            share3 = xlsummarysheet.Cells[96, 2].Text.Trim();
                            if (royalty.AutoformatDecimal() == 0)
                                SummaryMsg += "Royalty Zero,";
                            if (gurantee.AutoformatDecimal() == 0)
                                SummaryMsg += "Guarantee Zero,";
                            if (var_exp_act.AutoformatDecimal() == 0)
                                SummaryMsg += "Expenses Zero,";
                            //if (share1.AutoformatDecimal() + share2.AutoformatDecimal() + share3.AutoformatDecimal() != 100)
                            //    SummaryMsg += "Total share Not equal to 100,";

                            iferr = "";
                            for (i = 3; i < 13; i++)
                            {
                                ErrMsg = ""; sch_day = ""; sch_date = ""; sch_time = ""; tot_att = ""; gross_sales = ""; tot_exp = "0"; gross_rec = "";

                                sch_date = (xlNewSheet.Cells[1, i].Value != null) ? Convert.ToString(xlNewSheet.Cells[1, i].Value).Trim() : sch_date;
                                sch_day = Convert.ToString(xlNewSheet.Cells[2, i].Text).ToString().Trim();
                                sch_time = Convert.ToString(xlNewSheet.Cells[3, i].Text).ToString().Trim();
                                tot_att = Convert.ToString(xlNewSheet.Cells[9, i].Value).ToString().Trim();
                                gross_sales = Convert.ToString(xlNewSheet.Cells[13, i].Value).ToString().Trim();
                                //tot_exp = Convert.ToString(xlNewSheet.Cells[26, i].Value).ToString().Trim();
                                gross_rec = (xlNewSheet.Cells[31, i].Value != null) ? Convert.ToString(xlNewSheet.Cells[31, i].Value.ToString()).Trim() : gross_rec;

                                sch_date = (sch_date.ToLower().Contains("perf") == true) ? "0" : "1";
                                sch_day = (sch_day.Contains("Day") == true) ? "0" : "1";
                                sch_time = (sch_time.Contains("time") == true) ? "0" : "1";
                                tot_att = (tot_att.AutoformatInt() == 0) ? "0" : "1";
                                gross_sales = (gross_sales.AutoformatDecimal() == 0) ? "0" : "1";
                                //tot_exp = (tot_exp.AutoformatDecimal() == 0) ? "0" : "1";
                                gross_rec = (gross_rec.AutoformatDecimal() == 0) ? "0" : "1";

                                string ticket = "", price = "", ps_tkt_price = "";
                                for (int j = 100; j <= 110; j++)
                                {
                                    ticket = xlNewSheet.Cells[j, 1].Text.Trim();
                                    price = xlNewSheet.Cells[j, i].Text.Trim();
                                    price = (price.AutoformatDecimal() == null) ? "0" : price.AutoformatDecimal().ToString();
                                    if ((ticket.Contains("#") == true && price.AutoformatDecimal() > 0) || (ticket != "" && ticket.Contains("#") == false && price.AutoformatDecimal() == 0))
                                    {
                                        ps_tkt_price = "0";
                                    }
                                }

                                int totcnt = Convert.ToInt32(sch_date.AutoformatInt() + sch_day.AutoformatInt() + sch_time.AutoformatInt() + tot_att.AutoformatInt() +
                                    gross_sales.AutoformatInt() + tot_exp.AutoformatInt() + gross_rec.AutoformatInt());
                                if (totcnt < 6 && totcnt != 0)
                                {
                                    if (sch_date.AutoformatInt() == 0)
                                    {
                                        //ErrMsg += "Schedule date error,";
                                    }
                                    if (sch_day.AutoformatInt() == 0)
                                    {
                                        ErrMsg += "Schedule day error,";
                                    }
                                    if (tot_att.AutoformatInt() == 0)
                                    {
                                        ErrMsg += "Total attendance error,";
                                    }
                                    if (gross_sales.AutoformatInt() == 0)
                                    {
                                        ErrMsg += "Gross sales error,";
                                    }
                                    //if (tot_exp.AutoformatInt() == 0)
                                    //{
                                    //    ErrMsg += "Total expenses error,";
                                    //}
                                    if (gross_rec.AutoformatInt() == 0)
                                    {
                                        ErrMsg += "Gross receipts error,";
                                    }
                                }
                                if (Discount.AutoformatInt() == 0 && i == 3)
                                {
                                    ErrMsg += "Discount extra columns,";
                                }
                                if (ps_tkt_price.AutoformatInt() == 0 && totcnt == 7)
                                {
                                    ErrMsg += "Ticket price mismatching,";
                                }
                                valsheet.Cells[sno, i] = ErrMsg;
                                if (string.IsNullOrEmpty(ErrMsg) == false && string.IsNullOrEmpty(iferr) == true)
                                    iferr = ErrMsg;
                            }
                            valsheet.Cells[sno, 13] = SummaryMsg;
                            #endregion
                        }

                        xlscvsheet = (Excel.Worksheet)xlSheets[cvrindex];
                        string recid = "";

                        #region GetMySQLRecID
                        string shname = "", city = "", venue = "", presenter = "";
                        DateTime opdate, endate;
                        shname = xlscvsheet.Cells[1, 1].Text;
                        city = xlscvsheet.Cells[3, 2].Text;
                        venue = xlscvsheet.Cells[4, 2].Text;
                        presenter = xlscvsheet.Cells[5, 2].Text;

                        if (!string.IsNullOrEmpty(xlscvsheet.Cells[8, 1].Text))
                        {
                            opdate = Convert.ToDateTime(xlscvsheet.Cells[8, 1].Text);
                            endate = Convert.ToDateTime(xlscvsheet.Cells[8, 2].Text);
                            valsheet.Cells[sno, 14] = shname;
                            valsheet.Cells[sno, 15] = city;
                            valsheet.Cells[sno, 16] = opdate;
                            valsheet.Cells[sno, 17] = endate;
                            if (AllNew != 0)
                            {
                                MasterDataLayer.MasterData objmst = new MasterDataLayer.MasterData();
                                DataTable dt = new DataTable();
                                dt = objmst.GetMysql_Recordid(shname, city, opdate, endate);
                                if (dt.Rows.Count > 0)
                                {
                                    recid = dt.Rows[0]["RecordID"].ToString();
                                }
                            }
                        }
                        else
                        {
                            recid = "Opening date not found!";
                        }
                        valsheet.Cells[sno, 2] = recid;
                        #endregion

                        if (!string.IsNullOrEmpty(ErrMsg) || !string.IsNullOrEmpty(SummaryMsg))
                        {
                            hdnvalidation_status.Value = "1";
                        }
                        else if (recid.ToLower().Contains("new") == false && string.IsNullOrEmpty(iferr) == true)
                        {
                            //xlWorkbook.SaveAs(Saveaspath + finfo.Name);
                            valsheet.Cells[sno, 25] = "Pass";
                        }
                        else { valsheet.Cells[sno, 25] = "Fail"; }
                    }
                    else
                    {
                        valsheet.Cells[sno, i + 1] = ErrMsg;
                    }
                    #endregion
                }
                else
                {
                    lnkexcelclms.Visible = true;
                    hidelist = "1";
                    write_excel_columns(finfo, xlNewSheet, xlsummarysheet, sno, val_sheetlist);
                }
                // objcf.HideDBData_XLSheets(val_sheetlist, "1");
                // xlWorkbook.Save();
                xlWorkbook.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                GC.Collect();
            }
            catch (Exception ex)
            {
                valsheet.Cells[sno, i + 1] = ex.Message + " File Name: " + finfo.Name;
                xlWorkbook.Save();
                xlWorkbook.Close(Type.Missing, Type.Missing, Type.Missing);
                xlApp.Quit();
                GC.Collect();
                // throw new Exception(ex.Message + " File Name: " + finfo.Name);
            }
            finally
            {
                // objcf.HideDBData_XLSheets(val_sheetlist, hidelist);
                xlApp = null;
            }
        }
        public void Kill_Excel_Process()
        {
            foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
            {
                if (proc.MainWindowTitle.ToString() == "")
                {
                    //  proc.Kill();
                }
            }
        }
        private void Read(string path, int sno, string engt_id, string ps_wkno)
        {

            Microsoft.Office.Interop.Excel.Application xlApp = null;
            Excel.Workbook xlWorkbook = null;
            Excel.Sheets xlSheets = null;
            Excel.Worksheet xlNewSheet = null;
            Excel.Worksheet xlscvsheet = null;
            try
            {
                #region initial
                xlApp = new Microsoft.Office.Interop.Excel.Application();

                if (xlApp == null)
                    return;
                xlWorkbook = xlApp.Workbooks.Open(path, 0, false, 5, "", "",
                        false, Excel.XlPlatform.xlWindows, "",
                        true, false, 0, true, false, false);
                int boindex = 0, sumindex = 0, cvrindex = 0;
                for (int l = 1; l <= xlWorkbook.Sheets.Count; l++)
                {
                    if (xlWorkbook.Sheets[l].Name.ToString().Trim().Replace(" ", "").ToLower() == "boxoffice")
                    {
                        boindex = l;
                    }
                    if (xlWorkbook.Sheets[l].Name.ToString().Trim().Replace(" ", "").ToLower() == "coversheet")
                    {
                        cvrindex = l;
                    }
                    if (xlWorkbook.Sheets[l].Name.ToString().Trim().Replace(" ", "").ToLower() == "summary")
                    {
                        sumindex = l;
                    }
                }
                xlSheets = xlWorkbook.Sheets as Excel.Sheets;
                xlNewSheet = (Excel.Worksheet)xlSheets[boindex];
                xlNewSheet.Unprotect("7135");
                Excel.Worksheet excelsheet3 = null;
                excelsheet3 = (Excel.Worksheet)xlSheets[sumindex];
                DataTable dt = new DataTable("table1");
                DataTable dt1 = new DataTable("table2");
                DataTable dtcvr = new DataTable("tablecvr");
                DataTable dtchr = new DataTable("tablecvrchg");
                string filename = xlNewSheet.Name;
                #endregion

                #region getrecordid
                xlscvsheet = (Excel.Worksheet)xlSheets[cvrindex];
                string shname = "", city = "", recid = engt_id;
                DateTime opdate, endate;
                shname = xlscvsheet.Cells[1, 1].Text;
                city = xlscvsheet.Cells[3, 2].Text;
                opdate = Convert.ToDateTime(xlscvsheet.Cells[8, 1].Text);
                endate = Convert.ToDateTime(xlscvsheet.Cells[8, 2].Text);
                xlNewSheet.Cells[sno, 14] = shname;
                xlNewSheet.Cells[sno, 15] = city;
                xlNewSheet.Cells[sno, 16] = opdate;
                xlNewSheet.Cells[sno, 17] = endate;

                MasterDataLayer.MasterData objmst = new MasterDataLayer.MasterData();
                //DataTable dt_id = new DataTable();
                //dt_id = objmst.GetMysql_Recordid(shname, city, opdate, endate);
                //if (dt_id.Rows.Count > 0)
                //{
                //    recid = dt_id.Rows[0]["RecordID"].ToString();
                //}
                //else { recid = "7604"; }
                #endregion

                #region engt dt creation
                dt.Columns.Add("Sno");
                dt.Columns.Add("Recordid");
                dt.Columns.Add("Show Name");
                dt.Columns.Add("City Name");
                dt.Columns.Add("Engt_Date");
                dt.Columns.Add("Deal_Tax_Ptg");
                dt.Columns.Add("Deal_Tax2_Ptg");
                dt.Columns.Add("deal_sub_sales_comm");
                dt.Columns.Add("deal_ph_sales_comm");
                dt.Columns.Add("deal_web_sales_comm");
                dt.Columns.Add("deal_cc_sales_comm");
                dt.Columns.Add("deal_remote_sales_comm");
                dt.Columns.Add("deal_single_tix_comm");
                dt.Columns.Add("deal_grp_sales_comm1");
                dt.Columns.Add("deal_grp_sales_comm2");
                dt.Columns.Add("deal_misc_othr_amt_1");
                dt.Columns.Add("deal_misc_othr_amt_2");
                dt.Columns.Add("deal_royalty_income");
                dt.Columns.Add("deal_incm_wthd_tax_act_amt");
                dt.Columns.Add("deal_guarantee_income");
                dt.Columns.Add("deal_cmpny_mid_monies_ptg");
                dt.Columns.Add("deal_producer_share_split_ptg");
                dt.Columns.Add("deal_star_royalty_ptg");
                dt.Columns.Add("deal_presenter_share_split_Ptg");
                dt.Columns.Add("exp_d_ad_gross_bgt");
                dt.Columns.Add("exp_d_ad_gross_act");
                dt.Columns.Add("exp_d_stghand_loadin_bgt");
                dt.Columns.Add("exp_d_stghand_loadin_act");
                dt.Columns.Add("exp_d_stghand_loadout_bgt");
                dt.Columns.Add("exp_d_stghand_loadout_act");
                dt.Columns.Add("exp_d_stghand_running_bgt");
                dt.Columns.Add("exp_d_stghand_running_act");
                dt.Columns.Add("exp_d_wardrobe_loadin_bgt");
                dt.Columns.Add("exp_d_wardrobe_loadin_act");
                dt.Columns.Add("exp_d_wardrobe_loadout_bgt");
                dt.Columns.Add("exp_d_wardrobe_loadout_act");
                dt.Columns.Add("exp_d_wardrobe_running_bgt");
                dt.Columns.Add("exp_d_wardrobe_running_act");
                dt.Columns.Add("exp_d_labor_catering_bgt");
                dt.Columns.Add("exp_d_labor_catering_act");
                dt.Columns.Add("exp_d_musician_bgt");
                dt.Columns.Add("exp_d_musician_act");
                dt.Columns.Add("exp_d_insurance_per_unit");
                dt.Columns.Add("exp_d_insurance_bgt");
                dt.Columns.Add("exp_d_insurance_act");
                dt.Columns.Add("exp_d_ticket_print_per_unit");
                dt.Columns.Add("exp_d_ticket_print_bgt");
                dt.Columns.Add("exp_d_ticket_print_act");
                dt.Columns.Add("exp_d_other_1_desc");
                dt.Columns.Add("exp_d_other_1_bgt");
                dt.Columns.Add("exp_d_other_1_act");
                dt.Columns.Add("exp_l_ada_expense_bgt");
                dt.Columns.Add("exp_l_ada_expense_act");
                dt.Columns.Add("exp_l_bo_bgt");
                dt.Columns.Add("exp_l_bo_act");
                dt.Columns.Add("exp_l_catering_bgt");
                dt.Columns.Add("exp_l_catering_act");
                dt.Columns.Add("exp_l_equip_rental_bgt");
                dt.Columns.Add("exp_l_equip_rental_act");
                dt.Columns.Add("exp_l_grp_sales_bgt");
                dt.Columns.Add("exp_l_grp_sales_act");
                dt.Columns.Add("exp_l_house_staff_bgt");
                dt.Columns.Add("exp_l_house_staff_act");
                dt.Columns.Add("exp_l_league_fee_bgt");
                dt.Columns.Add("exp_l_league_fee_act");
                dt.Columns.Add("exp_l_license_bgt");
                dt.Columns.Add("exp_l_license_act");
                dt.Columns.Add("exp_l_limo_bgt");
                dt.Columns.Add("exp_l_limo_act");
                dt.Columns.Add("exp_l_orchestra_sh_remove_bgt");
                dt.Columns.Add("exp_l_orchestra_sh_remove_act");
                dt.Columns.Add("exp_l_presenter_profit_bgt");
                dt.Columns.Add("exp_l_presenter_profit_act");
                dt.Columns.Add("exp_l_police_bgt");
                dt.Columns.Add("exp_l_police_act");
                dt.Columns.Add("exp_l_program_bgt");
                dt.Columns.Add("exp_l_program_act");
                dt.Columns.Add("exp_l_rent_btg");
                dt.Columns.Add("exp_l_rent_act");
                dt.Columns.Add("exp_l_sound_bgt");
                dt.Columns.Add("exp_l_sound_act");
                dt.Columns.Add("exp_l_ticket_print_bgt");
                dt.Columns.Add("exp_l_ticket_print_act");
                dt.Columns.Add("exp_l_phone_bgt");
                dt.Columns.Add("exp_l_phone_act");
                dt.Columns.Add("exp_l_dryice_bgt");
                dt.Columns.Add("exp_l_dryice_act");
                dt.Columns.Add("MISCELLANEOUS_bgt");
                dt.Columns.Add("MISCELLANEOUS_act");
                dt.Columns.Add("exp_l_other1_desc");
                dt.Columns.Add("exp_l_other1_bgt");
                dt.Columns.Add("exp_l_other1_act");
                dt.Columns.Add("exp_l_local_fixed_bgt");
                dt.Columns.Add("exp_l_local_fixed_act");
                dt.Columns.Add("deal_facility_fee_amt");
                dt.Columns.Add("engt_exchange_rate");
                dt.Columns.Add("engt_subscription_amt");
                dt.Columns.Add("deal_incm_wthd_tax_act_unit");
                dt.Columns.Add("ps_schedule_weeks");
                dt.Columns.Add("deal_misc_othr_unit_1");
                dt.Columns.Add("deal_misc_othr_unit_2");
                dt.Columns.Add("deal_presenter_mid_monies_ptg");
                dt.Columns.Add("mny_remaining_mid_mny");
                #endregion

                #region engt val assign
                string Show_Name = xlNewSheet.Cells[1, 1].text;
                string City_Name = xlNewSheet.Cells[2, 1].text;
                string Engt_Date = xlNewSheet.Cells[4, 1].text;
                string deal_tax_ptg = xlNewSheet.Cells[14, 2].text;
                string deal_tax2_ptg = xlNewSheet.Cells[15, 2].text;
                string deal_sub_sales_comm = xlNewSheet.Cells[16, 2].text;
                string deal_ph_sales_comm = xlNewSheet.Cells[17, 2].text;
                string deal_web_sales_comm = xlNewSheet.Cells[18, 2].text;
                string deal_cc_sales_comm = xlNewSheet.Cells[19, 2].text;
                string deal_remote_sales_comm = xlNewSheet.Cells[20, 2].text;
                string deal_single_tix_comm = xlNewSheet.Cells[21, 2].text;
                string deal_grp_sales_comm1 = xlNewSheet.Cells[22, 2].text;
                string deal_grp_sales_comm2 = xlNewSheet.Cells[23, 2].text;
                string deal_misc_othr_amt_1 = xlNewSheet.Cells[24, 2].text;
                string deal_misc_othr_amt_2 = "", deal_facility_fee_amt = "";
                string fforother = xlNewSheet.Cells[25, 2].text;
                //if (fforother.ToLower() == "facility" == true)
                deal_misc_othr_amt_2 = xlNewSheet.Cells[25, 2].text;
                //else
                deal_facility_fee_amt = "0";// xlNewSheet.Cells[25, 2].text;
                string deal_misc_othr_unit_1 = (deal_misc_othr_amt_1.Contains("%") == true) ? "%" : "$";
                string deal_misc_othr_unit_2 = (fforother.Contains("%") == true) ? "%" : "$";
                string deal_royalty_income = excelsheet3.Cells[31, 2].text;
                string deal_incm_wthd_tax_act_amt = excelsheet3.Cells[32, 2].text;
                string deal_incm_wthd_tax_act_unit = (deal_incm_wthd_tax_act_amt.Contains("%") == true) ? "%" : "$";
                string deal_guarantee_income = excelsheet3.Cells[34, 2].text;
                string deal_cmpny_mid_monies_ptg = excelsheet3.Cells[83, 2].text;
                string deal_producer_share_split_ptg = excelsheet3.Cells[90, 2].text;
                string deal_star_royalty_ptg = excelsheet3.Cells[93, 2].text;
                string deal_presenter_share_split_Ptg = excelsheet3.Cells[96, 2].text;
                string exp_d_ad_gross_bgt = excelsheet3.Cells[42, 3].text;
                string exp_d_ad_gross_act = excelsheet3.Cells[42, 4].text;
                string exp_d_stghand_loadin_bgt = excelsheet3.Cells[43, 3].text;
                string exp_d_stghand_loadin_act = excelsheet3.Cells[43, 4].text;
                string exp_d_stghand_loadout_bgt = excelsheet3.Cells[44, 3].text;
                string exp_d_stghand_loadout_act = excelsheet3.Cells[44, 4].text;
                string exp_d_stghand_running_bgt = excelsheet3.Cells[45, 3].text;
                string exp_d_stghand_running_act = excelsheet3.Cells[45, 4].text;
                string exp_d_wardrobe_loadin_bgt = excelsheet3.Cells[46, 3].text;
                string exp_d_wardrobe_loadin_act = excelsheet3.Cells[46, 4].text;
                string exp_d_wardrobe_loadout_bgt = excelsheet3.Cells[47, 3].text;
                string exp_d_wardrobe_loadout_act = excelsheet3.Cells[47, 4].text;
                string exp_d_wardrobe_running_bgt = excelsheet3.Cells[48, 3].text;
                string exp_d_wardrobe_running_act = excelsheet3.Cells[48, 4].text;
                string exp_d_labor_catering_bgt = excelsheet3.Cells[49, 3].text;
                string exp_d_labor_catering_act = excelsheet3.Cells[49, 4].text;
                string exp_d_musician_bgt = excelsheet3.Cells[50, 3].text;
                string exp_d_musician_act = excelsheet3.Cells[50, 4].text;
                string exp_d_insurance_per_unit = excelsheet3.Cells[51, 2].text;
                string exp_d_insurance_bgt = excelsheet3.Cells[51, 3].text;
                string exp_d_insurance_act = excelsheet3.Cells[51, 4].text;
                string exp_d_ticket_print_per_unit = excelsheet3.Cells[52, 2].text;
                string exp_d_ticket_print_bgt = excelsheet3.Cells[52, 3].text;
                string exp_d_ticket_print_act = excelsheet3.Cells[52, 4].text;
                string exp_d_other_1_desc = excelsheet3.Cells[53, 2].text;
                string exp_d_other_1_bgt = excelsheet3.Cells[53, 3].text;
                string exp_d_other_1_act = excelsheet3.Cells[53, 4].text;
                string exp_l_ada_expense_bgt = excelsheet3.Cells[56, 3].text;
                string exp_l_ada_expense_act = excelsheet3.Cells[56, 4].text;
                string exp_l_bo_bgt = excelsheet3.Cells[57, 3].text;
                string exp_l_bo_act = excelsheet3.Cells[57, 4].text;
                string exp_l_catering_bgt = excelsheet3.Cells[58, 3].text;
                string exp_l_catering_act = excelsheet3.Cells[58, 4].text;
                string exp_l_equip_rental_bgt = excelsheet3.Cells[59, 3].text;
                string exp_l_equip_rental_act = excelsheet3.Cells[59, 4].text;
                string exp_l_grp_sales_bgt = excelsheet3.Cells[60, 3].text;
                string exp_l_grp_sales_act = excelsheet3.Cells[60, 4].text;
                string exp_l_house_staff_bgt = excelsheet3.Cells[61, 3].text;
                string exp_l_house_staff_act = excelsheet3.Cells[61, 4].text;
                string exp_l_league_fee_bgt = excelsheet3.Cells[62, 3].text;
                string exp_l_league_fee_act = excelsheet3.Cells[62, 4].text;
                string exp_l_license_bgt = excelsheet3.Cells[63, 3].text;
                string exp_l_license_act = excelsheet3.Cells[63, 4].text;
                string exp_l_limo_bgt = excelsheet3.Cells[64, 3].text;
                string exp_l_limo_act = excelsheet3.Cells[64, 4].text;
                string exp_l_orchestra_sh_remove_bgt = excelsheet3.Cells[65, 3].text;
                string exp_l_orchestra_sh_remove_act = excelsheet3.Cells[65, 4].text;
                string exp_l_presenter_profit_bgt = excelsheet3.Cells[66, 3].text;
                string exp_l_presenter_profit_act = excelsheet3.Cells[66, 4].text;
                string exp_l_police_bgt = excelsheet3.Cells[67, 3].text;
                string exp_l_police_act = excelsheet3.Cells[67, 4].text;
                string exp_l_program_bgt = excelsheet3.Cells[68, 3].text;
                string exp_l_program_act = excelsheet3.Cells[68, 4].text;
                string exp_l_rent_btg = excelsheet3.Cells[69, 3].text;
                string exp_l_rent_act = excelsheet3.Cells[69, 4].text;
                string exp_l_sound_bgt = excelsheet3.Cells[70, 3].text;
                string exp_l_sound_act = excelsheet3.Cells[70, 4].text;
                string exp_l_ticket_print_bgt = excelsheet3.Cells[71, 3].text;
                string exp_l_ticket_print_act = excelsheet3.Cells[71, 4].text;
                string exp_l_phone_bgt = excelsheet3.Cells[72, 3].text;
                string exp_l_phone_act = excelsheet3.Cells[72, 4].text;
                string exp_l_dryice_bgt = excelsheet3.Cells[73, 3].text;
                string exp_l_dryice_act = excelsheet3.Cells[73, 4].text;
                string misc1 = excelsheet3.Cells[74, 3].text;
                string misc2 = excelsheet3.Cells[74, 4].text;
                string exp_l_other1_desc = excelsheet3.Cells[75, 2].text;
                string exp_l_other1_bgt = excelsheet3.Cells[75, 3].text;
                string exp_l_other1_act = excelsheet3.Cells[75, 4].text;
                string exp_l_local_fixed_bgt = excelsheet3.Cells[76, 3].text;
                string exp_l_local_fixed_act = excelsheet3.Cells[76, 4].text;
                //                string deal_facility_fee_amt = xlNewSheet.Cells[25, 2].text;
                string engt_exchange_rate = excelsheet3.Cells[3, 7].text;
                string engt_subscription_amt = (string.IsNullOrEmpty(xlNewSheet.Cells[32, 15].text) == true) ? xlNewSheet.Cells[32, 14].text : xlNewSheet.Cells[32, 15].text;
                string deal_presenter_mid_monies_ptg = excelsheet3.Cells[83, 4].text;
                string mny_remaining_mid_mny = excelsheet3.Cells[81, 5].text;
                #endregion

                #region engt row add
                dt.Rows.Add(sno, recid,
                            Show_Name,
                            City_Name,
                            Engt_Date,
                            deal_tax_ptg.AutoformatDecimal(),
                            deal_tax2_ptg.AutoformatDecimal(),
                            deal_sub_sales_comm.AutoformatDecimal(),
                            deal_ph_sales_comm.AutoformatDecimal(),
                            deal_web_sales_comm.AutoformatDecimal(),
                            deal_cc_sales_comm.AutoformatDecimal(),
                            deal_remote_sales_comm.AutoformatDecimal(),
                            deal_single_tix_comm.AutoformatDecimal(),
                            deal_grp_sales_comm1.AutoformatDecimal(),
                            deal_grp_sales_comm2.AutoformatDecimal(),
                            deal_misc_othr_amt_1.AutoformatDecimal(),
                            deal_misc_othr_amt_2.AutoformatDecimal(),
                            deal_royalty_income.AutoformatDecimal(),
                            deal_incm_wthd_tax_act_amt.AutoformatDecimal(),
                            deal_guarantee_income.AutoformatDecimal(),
                            deal_cmpny_mid_monies_ptg.AutoformatDecimal(),
                            deal_producer_share_split_ptg.AutoformatDecimal(),
                            deal_star_royalty_ptg.AutoformatDecimal(),
                            deal_presenter_share_split_Ptg.AutoformatDecimal(),
                            exp_d_ad_gross_bgt.AutoformatDecimal(),
                            exp_d_ad_gross_act.AutoformatDecimal(),
                            exp_d_stghand_loadin_bgt.AutoformatDecimal(),
                            exp_d_stghand_loadin_act.AutoformatDecimal(),
                            exp_d_stghand_loadout_bgt.AutoformatDecimal(),
                            exp_d_stghand_loadout_act.AutoformatDecimal(),
                            exp_d_stghand_running_bgt.AutoformatDecimal(),
                            exp_d_stghand_running_act.AutoformatDecimal(),
                            exp_d_wardrobe_loadin_bgt.AutoformatDecimal(),
                            exp_d_wardrobe_loadin_act.AutoformatDecimal(),
                            exp_d_wardrobe_loadout_bgt.AutoformatDecimal(),
                            exp_d_wardrobe_loadout_act.AutoformatDecimal(),
                            exp_d_wardrobe_running_bgt.AutoformatDecimal(),
                            exp_d_wardrobe_running_act.AutoformatDecimal(),
                            exp_d_labor_catering_bgt.AutoformatDecimal(),
                            exp_d_labor_catering_act.AutoformatDecimal(),
                            exp_d_musician_bgt.AutoformatDecimal(),
                            exp_d_musician_act.AutoformatDecimal(),
                            exp_d_insurance_per_unit.AutoformatDecimal(),
                            exp_d_insurance_bgt.AutoformatDecimal(),
                            exp_d_insurance_act.AutoformatDecimal(),
                            exp_d_ticket_print_per_unit.AutoformatDecimal(),
                            exp_d_ticket_print_bgt.AutoformatDecimal(),
                            exp_d_ticket_print_act.AutoformatDecimal(),
                            exp_d_other_1_desc.AutoformatDecimal(),
                            exp_d_other_1_bgt.AutoformatDecimal(),
                            exp_d_other_1_act.AutoformatDecimal(),
                            exp_l_ada_expense_bgt.AutoformatDecimal(),
                            exp_l_ada_expense_act.AutoformatDecimal(),
                            exp_l_bo_bgt.AutoformatDecimal(),
                            exp_l_bo_act.AutoformatDecimal(),
                            exp_l_catering_bgt.AutoformatDecimal(),
                            exp_l_catering_act.AutoformatDecimal(),
                            exp_l_equip_rental_bgt.AutoformatDecimal(),
                            exp_l_equip_rental_act.AutoformatDecimal(),
                            exp_l_grp_sales_bgt.AutoformatDecimal(),
                            exp_l_grp_sales_act.AutoformatDecimal(),
                            exp_l_house_staff_bgt.AutoformatDecimal(),
                            exp_l_house_staff_act.AutoformatDecimal(),
                            exp_l_league_fee_bgt.AutoformatDecimal(),
                            exp_l_league_fee_act.AutoformatDecimal(),
                            exp_l_license_bgt.AutoformatDecimal(),
                            exp_l_license_act.AutoformatDecimal(),
                            exp_l_limo_bgt.AutoformatDecimal(),
                            exp_l_limo_act.AutoformatDecimal(),
                            exp_l_orchestra_sh_remove_bgt.AutoformatDecimal(),
                            exp_l_orchestra_sh_remove_act.AutoformatDecimal(),
                            exp_l_presenter_profit_bgt.AutoformatDecimal(),
                            exp_l_presenter_profit_act.AutoformatDecimal(),
                            exp_l_police_bgt.AutoformatDecimal(),
                            exp_l_police_act.AutoformatDecimal(),
                            exp_l_program_bgt.AutoformatDecimal(),
                            exp_l_program_act.AutoformatDecimal(),
                            exp_l_rent_btg.AutoformatDecimal(),
                            exp_l_rent_act.AutoformatDecimal(),
                            exp_l_sound_bgt.AutoformatDecimal(),
                            exp_l_sound_act.AutoformatDecimal(),
                            exp_l_ticket_print_bgt.AutoformatDecimal(),
                            exp_l_ticket_print_act.AutoformatDecimal(),
                            exp_l_phone_bgt.AutoformatDecimal(),
                            exp_l_phone_act.AutoformatDecimal(),
                            exp_l_dryice_bgt.AutoformatDecimal(),
                            exp_l_dryice_act.AutoformatDecimal(),
                            misc1.AutoformatDecimal(),
                            misc2.AutoformatDecimal(),
                            exp_l_other1_desc.AutoformatDecimal(),
                            exp_l_other1_bgt.AutoformatDecimal(),
                            exp_l_other1_act.AutoformatDecimal(),
                            exp_l_local_fixed_bgt.AutoformatDecimal(),
                            exp_l_local_fixed_act.AutoformatDecimal(),
                            deal_facility_fee_amt.AutoformatDecimal(),
                            engt_exchange_rate.AutoformatDecimal(),
                            engt_subscription_amt.AutoformatDecimal(), deal_incm_wthd_tax_act_unit, ps_wkno, deal_misc_othr_unit_1, deal_misc_othr_unit_2,
                            deal_presenter_mid_monies_ptg.AutoformatDecimal(),
                            mny_remaining_mid_mny.AutoformatDecimal()
                            );
                #endregion

                #region schedule dt creation
                dt1.Columns.Add("Sno");
                dt1.Columns.Add("Recordid");
                dt1.Columns.Add("Schedule_Type");
                dt1.Columns.Add("schedule_date");
                dt1.Columns.Add("schedule_st_time");
                dt1.Columns.Add("bo_drop_count");
                dt1.Columns.Add("bo_paid_attendance");
                dt1.Columns.Add("bo_comps");
                dt1.Columns.Add("bo_gross_sales");
                dt1.Columns.Add("bo_sub_gross_rcpt");
                dt1.Columns.Add("bo_ph_gross_rcpt");
                dt1.Columns.Add("bo_web_gross_rcpt");
                dt1.Columns.Add("bo_cc_gross_rcpt");
                dt1.Columns.Add("bo_outlet_gross_rcpt");
                dt1.Columns.Add("bo_single_tix_gross_rcpt");
                dt1.Columns.Add("bo_small_group_gross_rcpt");
                dt1.Columns.Add("bo_large_group_gross_rcpt");
                dt1.Columns.Add("bo_other_per_gross_rcpt");
                dt1.Columns.Add("bo_other_usd_gross_rcpt");
                dt1.Columns.Add("bo_sub_t_sold");
                dt1.Columns.Add("bo_ph_t_sold");
                dt1.Columns.Add("bo_web_t_sold");
                dt1.Columns.Add("bo_cc_t_sold");
                dt1.Columns.Add("bo_outlet_t_sold");
                dt1.Columns.Add("bo_single_tix_t_sold");
                dt1.Columns.Add("bo_small_group_t_sold");
                dt1.Columns.Add("dsct_sub1_per");
                dt1.Columns.Add("dsct_sub1_tickets");
                dt1.Columns.Add("dsct_sub2_per");
                dt1.Columns.Add("dsct_sub2_tickets");
                dt1.Columns.Add("dsct_sub3_per");
                dt1.Columns.Add("dsct_sub3_tickets");
                dt1.Columns.Add("dsct_sub4_per");
                dt1.Columns.Add("dsct_sub4_tickets");
                dt1.Columns.Add("dsct_sub5_per");
                dt1.Columns.Add("dsct_sub5_tickets");
                dt1.Columns.Add("dsct_sub6_per");
                dt1.Columns.Add("dsct_sub6_tickets");
                dt1.Columns.Add("dsct_sml_grp_per");
                dt1.Columns.Add("dsct_sml_grp_tickets");
                dt1.Columns.Add("dsct_lrg_grp_per");
                dt1.Columns.Add("dsct_lrg_grp_tickets");
                dt1.Columns.Add("dsct_misc1_per");
                dt1.Columns.Add("dsct_misc1_tickets");
                dt1.Columns.Add("dsct_misc2_per");
                dt1.Columns.Add("dsct_misc2_tickets");
                dt1.Columns.Add("dsct_misc3_per");
                dt1.Columns.Add("dsct_misc3_tickets");
                dt1.Columns.Add("dsct_misc4_per");
                dt1.Columns.Add("dsct_misc4_tickets");
                dt1.Columns.Add("dsct_demand_price");
                dt1.Columns.Add("Scale1 ticket");
                dt1.Columns.Add("Scale2 ticket");
                dt1.Columns.Add("Scale3 ticket");
                dt1.Columns.Add("Scale4 ticket");
                dt1.Columns.Add("Scale5 ticket");
                dt1.Columns.Add("Scale6 ticket");
                dt1.Columns.Add("Scale7 ticket");
                dt1.Columns.Add("Scale8 ticket");
                dt1.Columns.Add("Scale9 ticket");
                dt1.Columns.Add("Scale10 ticket");
                dt1.Columns.Add("Scale11 ticket");
                dt1.Columns.Add("Scale1 price");
                dt1.Columns.Add("Scale2 price");
                dt1.Columns.Add("Scale3 price");
                dt1.Columns.Add("Scale4 price");
                dt1.Columns.Add("Scale5 price");
                dt1.Columns.Add("Scale6 price");
                dt1.Columns.Add("Scale7 price");
                dt1.Columns.Add("Scale8 price");
                dt1.Columns.Add("Scale9 price");
                dt1.Columns.Add("Scale10 price");
                dt1.Columns.Add("Scale11 price");
                dt1.Columns.Add("dsct_misc5_per");
                dt1.Columns.Add("dsct_misc5_tickets");
                #endregion

                #region insert schedule using for loop
                string stype = "", perday = "", perday_p = "";
                bool perflag;
                DateTime opdateinc = opdate, Tdate = opdate;
                for (int i = 3; i <= 12; i++)
                {

                    stype = Convert.ToString(xlNewSheet.Cells[1, i].value);
                    perflag = stype.ToLower().Contains("perf");


                    perday = Convert.ToString(xlNewSheet.Cells[2, i].value).ToLower();
                    perday = (perday.Length > 3) ? perday.Substring(0, 3) : perday;
                    opdateinc = (perday_p == perday || i == 3) ? Tdate : opdateinc.AddDays(1);
                    perday_p = perday;
                    stype = (perday != "day" && perflag == true) ? Convert.ToString(opdateinc) : stype;
                    perflag = stype.ToLower().Contains("perf");
                    if (perflag == false)
                    {
                        //dt1.Rows.Add("Performance " + (i - 2).ToString(),
                        //       stype,
                        //        xlNewSheet.Cells[3, i].text,
                        Tdate = Convert.ToDateTime(stype);
                        string bo_drop_count = xlNewSheet.Cells[6, i].text;
                        string bo_paid_attendance = xlNewSheet.Cells[7, i].text;
                        string bo_comps = xlNewSheet.Cells[8, i].text;
                        string bo_gross_sales = xlNewSheet.Cells[13, i].text;
                        string bo_sub_gross_rcpt = xlNewSheet.Cells[32, i].text;
                        string bo_ph_gross_rcpt = xlNewSheet.Cells[33, i].text;
                        string bo_web_gross_rcpt = xlNewSheet.Cells[34, i].text;
                        string bo_cc_gross_rcpt = xlNewSheet.Cells[35, i].text;
                        string bo_outlet_gross_rcpt = xlNewSheet.Cells[36, i].text;
                        string bo_single_tix_gross_rcpt = xlNewSheet.Cells[37, i].text;
                        string bo_small_group_gross_rcpt = xlNewSheet.Cells[38, i].text;
                        string bo_large_group_gross_rcpt = xlNewSheet.Cells[39, i].text;
                        string bo_other_per_gross_rcpt = xlNewSheet.Cells[40, i].text;
                        string bo_other_usd_gross_rcpt = xlNewSheet.Cells[41, i].text;
                        string bo_sub_t_sold = xlNewSheet.Cells[45, i].text;
                        string bo_ph_t_sold = xlNewSheet.Cells[46, i].text;
                        string bo_web_t_sold = xlNewSheet.Cells[47, i].text;
                        string bo_cc_t_sold = xlNewSheet.Cells[48, i].text;
                        string bo_outlet_t_sold = xlNewSheet.Cells[49, i].text;
                        string bo_single_tix_t_sold = xlNewSheet.Cells[50, i].text;
                        string bo_small_group_t_sold = xlNewSheet.Cells[51, i].text;
                        string dsct_sub1_per = xlNewSheet.Cells[58, i].text;
                        string dsct_sub1_tickets = xlNewSheet.Cells[59, i].text;
                        string dsct_sub2_per = xlNewSheet.Cells[60, i].text;
                        string dsct_sub2_tickets = xlNewSheet.Cells[61, i].text;
                        string dsct_sub3_per = xlNewSheet.Cells[62, i].text;
                        string dsct_sub3_tickets = xlNewSheet.Cells[63, i].text;
                        string dsct_sub4_per = xlNewSheet.Cells[64, i].text;
                        string dsct_sub4_tickets = xlNewSheet.Cells[65, i].text;
                        string dsct_sub5_per = xlNewSheet.Cells[66, i].text;
                        string dsct_sub5_tickets = xlNewSheet.Cells[67, i].text;
                        string dsct_sub6_per = xlNewSheet.Cells[68, i].text;
                        string dsct_sub6_tickets = xlNewSheet.Cells[69, i].text;
                        string dsct_sml_grp_per = xlNewSheet.Cells[72, i].text;
                        string dsct_sml_grp_tickets = xlNewSheet.Cells[73, i].text;
                        string dsct_lrg_grp_per = xlNewSheet.Cells[76, i].text;
                        string dsct_lrg_grp_tickets = xlNewSheet.Cells[77, i].text;
                        string dsct_misc1_per = xlNewSheet.Cells[80, i].text;
                        string dsct_misc1_tickets = xlNewSheet.Cells[81, i].text;
                        string dsct_misc2_per = xlNewSheet.Cells[82, i].text;
                        string dsct_misc2_tickets = xlNewSheet.Cells[83, i].text;
                        string dsct_misc3_per = xlNewSheet.Cells[84, i].text;
                        string dsct_misc3_tickets = xlNewSheet.Cells[85, i].text;
                        string dsct_misc4_per = xlNewSheet.Cells[86, i].text;
                        string dsct_misc4_tickets = xlNewSheet.Cells[87, i].text;
                        string dsct_misc5_per = xlNewSheet.Cells[88, i].text;
                        string dsct_misc5_tickets = xlNewSheet.Cells[89, i].text;
                        string dsct_demand_price = xlNewSheet.Cells[97, i].text;
                        string Scale1ticket = xlNewSheet.Cells[100, 1].text;
                        string Scale2ticket = xlNewSheet.Cells[101, 1].text;
                        string Scale3ticket = xlNewSheet.Cells[102, 1].text;
                        string Scale4ticket = xlNewSheet.Cells[103, 1].text;
                        string Scale5ticket = xlNewSheet.Cells[104, 1].text;
                        string Scale6ticket = xlNewSheet.Cells[105, 1].text;
                        string Scale7ticket = xlNewSheet.Cells[106, 1].text;
                        string Scale8ticket = xlNewSheet.Cells[107, 1].text;
                        string Scale9ticket = xlNewSheet.Cells[108, 1].text;
                        string Scale10ticket = xlNewSheet.Cells[109, 1].text;
                        string Scale11ticket = xlNewSheet.Cells[110, 1].text;
                        string Scale1price = xlNewSheet.Cells[100, i].text;
                        string Scale2price = xlNewSheet.Cells[101, i].text;
                        string Scale3price = xlNewSheet.Cells[102, i].text;
                        string Scale4price = xlNewSheet.Cells[103, i].text;
                        string Scale5price = xlNewSheet.Cells[104, i].text;
                        string Scale6price = xlNewSheet.Cells[105, i].text;
                        string Scale7price = xlNewSheet.Cells[106, i].text;
                        string Scale8price = xlNewSheet.Cells[107, i].text;
                        string Scale9price = xlNewSheet.Cells[108, i].text;
                        string Scale10price = xlNewSheet.Cells[109, i].text;
                        string Scale11price = xlNewSheet.Cells[110, i].text;



                        #region test
                        Nullable<decimal> d;
                        Nullable<Int32> dd;
                        dd = bo_drop_count.AutoformatInt();
                        dd = bo_paid_attendance.AutoformatInt();
                        d = bo_comps.AutoformatInt();
                        d = bo_gross_sales.AutoformatDecimal();
                        d = bo_sub_gross_rcpt.AutoformatDecimal();
                        d = bo_ph_gross_rcpt.AutoformatDecimal();
                        d = bo_web_gross_rcpt.AutoformatDecimal();
                        d = bo_cc_gross_rcpt.AutoformatDecimal();
                        d = bo_outlet_gross_rcpt.AutoformatDecimal();
                        d = bo_single_tix_gross_rcpt.AutoformatDecimal();
                        d = bo_small_group_gross_rcpt.AutoformatDecimal();
                        d = bo_large_group_gross_rcpt.AutoformatDecimal();
                        d = bo_other_per_gross_rcpt.AutoformatDecimal();
                        d = bo_other_usd_gross_rcpt.AutoformatDecimal();
                        d = bo_sub_t_sold.AutoformatInt();
                        d = bo_ph_t_sold.AutoformatInt();
                        d = bo_web_t_sold.AutoformatInt();
                        d = bo_cc_t_sold.AutoformatInt();
                        d = bo_outlet_t_sold.AutoformatInt();
                        d = bo_single_tix_t_sold.AutoformatInt();
                        d = bo_small_group_t_sold.AutoformatInt();
                        d = dsct_sub1_per.AutoformatDecimal();
                        d = dsct_sub1_tickets.AutoformatInt();
                        d = dsct_sub2_per.AutoformatDecimal();
                        d = dsct_sub2_tickets.AutoformatInt();
                        d = dsct_sub3_per.AutoformatDecimal();
                        d = dsct_sub3_tickets.AutoformatInt();
                        d = dsct_sub4_per.AutoformatDecimal();
                        d = dsct_sub4_tickets.AutoformatInt();
                        d = dsct_sub5_per.AutoformatDecimal();
                        d = dsct_sub5_tickets.AutoformatInt();
                        d = dsct_sub6_per.AutoformatDecimal();
                        d = dsct_sub6_tickets.AutoformatInt();
                        d = dsct_sml_grp_per.AutoformatDecimal();
                        d = dsct_sml_grp_tickets.AutoformatInt();
                        d = dsct_lrg_grp_per.AutoformatDecimal();
                        d = dsct_lrg_grp_tickets.AutoformatInt();
                        d = dsct_misc1_per.AutoformatDecimal();
                        d = dsct_misc1_tickets.AutoformatInt();
                        d = dsct_misc2_per.AutoformatDecimal();
                        d = dsct_misc2_tickets.AutoformatInt();
                        d = dsct_misc3_per.AutoformatDecimal();
                        d = dsct_misc3_tickets.AutoformatInt();
                        d = dsct_misc4_per.AutoformatDecimal();
                        d = dsct_misc4_tickets.AutoformatInt();
                        d = dsct_demand_price.AutoformatDecimal();
                        d = Scale1price.AutoformatDecimal();
                        d = Scale2price.AutoformatDecimal();
                        d = Scale3price.AutoformatDecimal();
                        d = Scale4price.AutoformatDecimal();
                        d = Scale5price.AutoformatDecimal();
                        d = Scale6price.AutoformatDecimal();
                        d = Scale7price.AutoformatDecimal();
                        d = Scale8price.AutoformatDecimal();
                        d = Scale9price.AutoformatDecimal();
                        d = Scale10price.AutoformatDecimal();
                        d = Scale11price.AutoformatDecimal();
                        d = dsct_misc5_per.AutoformatDecimal();
                        d = dsct_misc5_tickets.AutoformatDecimal();
                        #endregion


                        dt1.Rows.Add(sno, recid, "Performance " + (i - 2).ToString(),
                              stype,
                               xlNewSheet.Cells[3, i].text,
                              bo_drop_count.AutoformatInt(),
                              bo_paid_attendance.AutoformatInt(),
                              bo_comps.AutoformatInt(),
                              bo_gross_sales.AutoformatDecimal(),
                              bo_sub_gross_rcpt.AutoformatDecimal(),
                              bo_ph_gross_rcpt.AutoformatDecimal(),
                              bo_web_gross_rcpt.AutoformatDecimal(),
                              bo_cc_gross_rcpt.AutoformatDecimal(),
                              bo_outlet_gross_rcpt.AutoformatDecimal(),
                              bo_single_tix_gross_rcpt.AutoformatDecimal(),
                              bo_small_group_gross_rcpt.AutoformatDecimal(),
                              bo_large_group_gross_rcpt.AutoformatDecimal(),
                              bo_other_per_gross_rcpt.AutoformatDecimal(),
                              bo_other_usd_gross_rcpt.AutoformatDecimal(),
                              bo_sub_t_sold.AutoformatInt(),
                              bo_ph_t_sold.AutoformatInt(),
                              bo_web_t_sold.AutoformatInt(),
                              bo_cc_t_sold.AutoformatInt(),
                              bo_outlet_t_sold.AutoformatInt(),
                              bo_single_tix_t_sold.AutoformatInt(),
                              bo_small_group_t_sold.AutoformatInt(),
                              dsct_sub1_per.AutoformatDecimal(),
                              dsct_sub1_tickets.AutoformatInt(),
                              dsct_sub2_per.AutoformatDecimal(),
                              dsct_sub2_tickets.AutoformatInt(),
                              dsct_sub3_per.AutoformatDecimal(),
                              dsct_sub3_tickets.AutoformatInt(),
                              dsct_sub4_per.AutoformatDecimal(),
                              dsct_sub4_tickets.AutoformatInt(),
                              dsct_sub5_per.AutoformatDecimal(),
                              dsct_sub5_tickets.AutoformatInt(),
                              dsct_sub6_per.AutoformatDecimal(),
                              dsct_sub6_tickets.AutoformatInt(),
                              dsct_sml_grp_per.AutoformatDecimal(),
                              dsct_sml_grp_tickets.AutoformatInt(),
                              dsct_lrg_grp_per.AutoformatDecimal(),
                              dsct_lrg_grp_tickets.AutoformatInt(),
                              dsct_misc1_per.AutoformatDecimal(),
                              dsct_misc1_tickets.AutoformatInt(),
                              dsct_misc2_per.AutoformatDecimal(),
                              dsct_misc2_tickets.AutoformatInt(),
                              dsct_misc3_per.AutoformatDecimal(),
                              dsct_misc3_tickets.AutoformatInt(),
                              dsct_misc4_per.AutoformatDecimal(),
                              dsct_misc4_tickets.AutoformatInt(),
                              dsct_demand_price.AutoformatDecimal(),
                               ticketseats(Scale1ticket),
                               ticketseats(Scale2ticket),
                               ticketseats(Scale3ticket),
                               ticketseats(Scale4ticket),
                               ticketseats(Scale5ticket),
                               ticketseats(Scale6ticket),
                               ticketseats(Scale7ticket),
                               ticketseats(Scale8ticket),
                               ticketseats(Scale9ticket),
                               ticketseats(Scale10ticket),
                               ticketseats(Scale11ticket),
                              Scale1price.AutoformatDecimal(),
                              Scale2price.AutoformatDecimal(),
                              Scale3price.AutoformatDecimal(),
                              Scale4price.AutoformatDecimal(),
                              Scale5price.AutoformatDecimal(),
                              Scale6price.AutoformatDecimal(),
                              Scale7price.AutoformatDecimal(),
                              Scale8price.AutoformatDecimal(),
                              Scale9price.AutoformatDecimal(),
                              Scale10price.AutoformatDecimal(),
                              Scale11price.AutoformatDecimal(),
                              dsct_misc5_per.AutoformatDecimal(),
                              dsct_misc5_tickets.AutoformatDecimal()
                  );

                    }
                }
                #endregion


                #region Coversheet dt creation

                #region cvrdoc
                dtcvr.Columns.Add("cvr_docs_id");
                dtcvr.Columns.Add("cvr_engt_id");
                dtcvr.Columns.Add("cvr_s_cover_flag");
                dtcvr.Columns.Add("cvr_grnty_flag");
                dtcvr.Columns.Add("cvr_royalty_flag");
                dtcvr.Columns.Add("cvr_ovrg_flag");
                dtcvr.Columns.Add("cvr_s_summary_flag");
                dtcvr.Columns.Add("cvr_venue_sett_flag");
                dtcvr.Columns.Add("cvr_bo_sheet_flag");
                dtcvr.Columns.Add("cvr_bo_statements_flag");
                dtcvr.Columns.Add("cvr_lbr_bills_flag");
                dtcvr.Columns.Add("cvr_musician_bills_flag");
                dtcvr.Columns.Add("cvr_local_exp_invoice_flag");
                dtcvr.Columns.Add("cvr_ad_flag");
                dtcvr.Columns.Add("cvr_contact_flag");
                dtcvr.Columns.Add("cvr_s_cover_notes");
                dtcvr.Columns.Add("cvr_grnty_notes");
                dtcvr.Columns.Add("cvr_royalty_notes");
                dtcvr.Columns.Add("cvr_ovrg_notes");
                dtcvr.Columns.Add("cvr_s_summary_notes");
                dtcvr.Columns.Add("cvr_venue_sett_notes");
                dtcvr.Columns.Add("cvr_bo_sheet_notes");
                dtcvr.Columns.Add("cvr_bo_statements_notes");
                dtcvr.Columns.Add("cvr_lbr_bills_notes");
                dtcvr.Columns.Add("cvr_musician_bills_notes");
                dtcvr.Columns.Add("cvr_local_exp_invoice_notes");
                dtcvr.Columns.Add("cvr_ad_notes");
                dtcvr.Columns.Add("cvr_contact_notes");
                dtcvr.Columns.Add("email_list");
                #endregion

                #region cvrchages and receivables
                dtchr.Columns.Add("cvr_chgs_id");
                dtchr.Columns.Add("cvr_engt_id");
                dtchr.Columns.Add("cvr_chgs_desc");
                dtchr.Columns.Add("cvr_chgs_amt");
                dtchr.Columns.Add("cvr_chgs_check");
                dtchr.Columns.Add("cvr_chgs_notes");
                dtchr.Columns.Add("cvr_type");
                #endregion

                #endregion

                #region Assign value for coversheet

                #region cvrdoc_assign

                string cvr_s_cover_flag = "", cvr_grnty_flag = "", cvr_royalty_flag = "", cvr_ovrg_flag = "", cvr_s_summary_flag = "", cvr_venue_sett_flag = "", cvr_bo_sheet_flag = "",
                cvr_bo_statements_flag = "", cvr_lbr_bills_flag = "", cvr_musician_bills_flag = "", cvr_local_exp_invoice_flag = "", cvr_ad_flag = "", cvr_contact_flag = "",
                cvr_s_cover_notes = "", cvr_grnty_notes = "", cvr_royalty_notes = "", cvr_ovrg_notes = "", cvr_s_summary_notes = "", cvr_venue_sett_notes = "",
                cvr_bo_sheet_notes = "", cvr_bo_statements_notes = "", cvr_lbr_bills_notes = "", cvr_musician_bills_notes = "", cvr_local_exp_invoice_notes = "",
               cvr_ad_notes = "", cvr_contact_notes = "", email_list = "";
                cvr_s_cover_flag = get_cvr_doc_flg(xlscvsheet.Cells[29, 4].text);
                cvr_grnty_flag = get_cvr_doc_flg(xlscvsheet.Cells[30, 4].text);
                cvr_royalty_flag = get_cvr_doc_flg(xlscvsheet.Cells[31, 4].text);
                cvr_ovrg_flag = get_cvr_doc_flg(xlscvsheet.Cells[32, 4].text);
                cvr_s_summary_flag = get_cvr_doc_flg(xlscvsheet.Cells[33, 4].text);
                cvr_venue_sett_flag = get_cvr_doc_flg(xlscvsheet.Cells[34, 4].text);
                cvr_bo_sheet_flag = get_cvr_doc_flg(xlscvsheet.Cells[35, 4].text);
                cvr_bo_statements_flag = get_cvr_doc_flg(xlscvsheet.Cells[36, 4].text);
                cvr_lbr_bills_flag = get_cvr_doc_flg(xlscvsheet.Cells[37, 4].text);
                cvr_musician_bills_flag = get_cvr_doc_flg(xlscvsheet.Cells[38, 4].text);
                cvr_local_exp_invoice_flag = get_cvr_doc_flg(xlscvsheet.Cells[39, 4].text);
                cvr_ad_flag = get_cvr_doc_flg(xlscvsheet.Cells[40, 4].text);
                cvr_contact_flag = get_cvr_doc_flg(xlscvsheet.Cells[41, 4].text);
                cvr_s_cover_notes = xlscvsheet.Cells[29, 5].text;
                cvr_grnty_notes = xlscvsheet.Cells[30, 5].text;
                cvr_royalty_notes = xlscvsheet.Cells[31, 5].text;
                cvr_ovrg_notes = xlscvsheet.Cells[32, 5].text;
                cvr_s_summary_notes = xlscvsheet.Cells[33, 5].text;
                cvr_venue_sett_notes = xlscvsheet.Cells[34, 5].text;
                cvr_bo_sheet_notes = xlscvsheet.Cells[35, 5].text;
                cvr_bo_statements_notes = xlscvsheet.Cells[36, 5].text;
                cvr_lbr_bills_notes = xlscvsheet.Cells[37, 5].text;
                cvr_musician_bills_notes = xlscvsheet.Cells[38, 5].text;
                cvr_local_exp_invoice_notes = xlscvsheet.Cells[39, 5].text;
                cvr_ad_notes = xlscvsheet.Cells[40, 5].text;
                cvr_contact_notes = xlscvsheet.Cells[41, 5].text;
                email_list = xlscvsheet.Cells[66, 1].text;
                dtcvr.Rows.Add(sno, recid, cvr_s_cover_flag, cvr_grnty_flag, cvr_royalty_flag, cvr_ovrg_flag, cvr_s_summary_flag, cvr_venue_sett_flag, cvr_bo_sheet_flag,
                cvr_bo_statements_flag, cvr_lbr_bills_flag, cvr_musician_bills_flag, cvr_local_exp_invoice_flag, cvr_ad_flag, cvr_contact_flag, cvr_s_cover_notes,
                cvr_grnty_notes, cvr_royalty_notes, cvr_ovrg_notes, cvr_s_summary_notes, cvr_venue_sett_notes, cvr_bo_sheet_notes, cvr_bo_statements_notes, cvr_lbr_bills_notes,
                cvr_musician_bills_notes, cvr_local_exp_invoice_notes, cvr_ad_notes, cvr_contact_notes, email_list);

                #endregion

                #region cvrcharges and receivables assign

                string cvr_chgs_desc, cvr_chgs_amt, cvr_chgs_check, cvr_chgs_notes, cvr_type = "c";
                for (int c = 44; c <= 52; c++)
                {
                    cvr_chgs_desc = xlscvsheet.Cells[c, 1].text;
                    cvr_chgs_amt = xlscvsheet.Cells[c, 4].text;
                    cvr_chgs_check = xlscvsheet.Cells[c, 5].text;
                    cvr_chgs_notes = xlscvsheet.Cells[c, 6].text;
                    if (string.IsNullOrEmpty(cvr_chgs_desc) == false)
                        dtchr.Rows.Add(sno, recid, cvr_chgs_desc, cvr_chgs_amt.AutoformatDecimal(), cvr_chgs_check, cvr_chgs_notes, cvr_type);
                }
                cvr_type = "r";
                for (int c = 56; c <= 62; c++)
                {
                    cvr_chgs_desc = xlscvsheet.Cells[c, 1].text;
                    cvr_chgs_amt = xlscvsheet.Cells[c, 4].text;
                    cvr_chgs_check = "";
                    cvr_chgs_notes = xlscvsheet.Cells[c, 5].text;
                    if (string.IsNullOrEmpty(cvr_chgs_desc) == false)
                        dtchr.Rows.Add(sno, recid, cvr_chgs_desc, cvr_chgs_amt.AutoformatDecimal(), cvr_chgs_check, cvr_chgs_notes, cvr_type);
                }
                #endregion

                #endregion


                #region write to db
                MasterDataLayer.MasterData md = new MasterDataLayer.MasterData();
                md.sqlbcopy(dt, "temp_engt");
                md.sqlbcopy(dt1, "temp_schedule");
                md.sqlbcopy(dtcvr, "temp_cvr_documents");
                md.sqlbcopy(dtchr, "temp_cvr_charges");


                string result;
                using (StringWriter sw = new StringWriter())
                {
                    dt.WriteXml(sw);
                    result = sw.ToString();
                }
                #endregion


            }
            catch (Exception ex)
            {
                Int32 lineno = new System.Diagnostics.StackTrace(ex, true).GetFrame(1).GetFileLineNumber();
                throw new Exception(ex.Message + " Line No.:<<" + lineno.ToString() + ">> " + path);
            }
            finally
            {
                xlWorkbook.Save();
                xlWorkbook.Close(Type.Missing, Type.Missing, Type.Missing);
                xlApp.Quit();
                Kill_Excel_Process();
                xlApp = null;
            }
        }
        public string get_cvr_doc_flg(string flgstatus)
        {
            flgstatus = flgstatus.ToLower();
            if (flgstatus.Contains("n/a"))
                flgstatus = "n";
            else if (flgstatus.Contains("x"))
                flgstatus = "y";
            else
                flgstatus = "n";
            return flgstatus;
        }
        public string ticketseats(string v)
        {
            string value = "";
            if (v == string.Empty || v.Contains("#") == true)
            {
                value = "";
            }
            else
            {
                value = Convert.ToString(v.Split('-').GetValue(1)).Replace(" seats", "").Replace(" of", "").Trim();
            }
            return value;
        }

        protected void btnvalidate_Click(object sender, EventArgs e)
        {
            valstatus = 1;
            hdnvalidation_status.Value = "0";
            lblfilename.Text = "Processing...";
            try
            {
                validate("folder");
            }
            catch (Exception ex)
            {
                lblErrmsg.Text = ex.Message;
            }



            lblfilename.Text = "Completed...";
        }
        public void write_to_db()
        {
            int sno = 2;
            Kill_Excel_Process();
            string valexcelpath = ValidationInputPath;
            Microsoft.Office.Interop.Excel.Application xlApp1 = null;
            Excel.Workbook xlWorkbook1 = null;
            Excel.Sheets val_sheets = null;
            Excel.Worksheet valsheet = null;
            Excel.Worksheet xlClmWritingsheet = null;
            xlApp1 = new Microsoft.Office.Interop.Excel.Application();
            xlWorkbook1 = xlApp1.Workbooks.Open(valexcelpath, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            val_sheets = xlWorkbook1.Sheets as Excel.Sheets;
            valsheet = (Excel.Worksheet)val_sheets[1];
            xlClmWritingsheet = (Excel.Worksheet)val_sheets[2];
            string file, _recoid, _psWeek, _cityid = "0";
            try
            {
                for (int e = 2; e < 5000; e++)
                {
                    try
                    {
                        file = valsheet.Cells[e, 1].Text;
                        _recoid = valsheet.Cells[e, 2].Text;
                        _psWeek = valsheet.Cells[e, 23].Text;
                        if (_recoid.ToLower() == "new")
                        {
                            _recoid = createnewengt(e, valsheet);
                            _cityid = _recoid.ToString().Split(',').GetValue(1).ToString();
                            _recoid = _recoid.ToString().Split(',').GetValue(0).ToString();
                            valsheet.Cells[e, 2] = _recoid;
                            valsheet.Cells[e, 20] = _cityid;
                        }
                        else
                        {
                            _cityid = valsheet.Cells[e, 20].Text;
                        }
                        if (string.IsNullOrEmpty(file) == true)
                            break;
                        file = ExcelFolderPath + file;
                        if (_cityid != "0" && _recoid != "0")
                        {
                            Read(file, sno, _recoid, _psWeek);
                            valsheet.Cells[e, 25] = "Pass";
                        }
                    }
                    catch (Exception ex1)
                    {
                        valsheet.Cells[e, 25] = "Fail: " + ex1.Message;
                    }
                    sno++;
                }
            }
            finally
            {
                xlWorkbook1.Save();
                xlWorkbook1.Close(Type.Missing, Type.Missing, Type.Missing);
                xlApp1.Quit();
                GC.Collect();
                Kill_Excel_Process();
            }
        }
        public void copy_validation_template()
        {
            if (File.Exists(ValidationExcelPath) == true)
            {
                File.Delete(ValidationExcelPath);
            }
            File.Copy(ValidationExcelTemplate, ValidationExcelPath);
        }
        public void validate(string from)
        {
            if (File.Exists(ValidationExcelPath) == true)
            {
                File.Delete(ValidationExcelPath);
            }
            File.Copy(ValidationExcelTemplate, ValidationExcelPath);

            int sno = 2;
            Kill_Excel_Process();
            string valexcelpath = ValidationExcelPath;
            Microsoft.Office.Interop.Excel.Application xlApp1 = null;
            Excel.Workbook xlWorkbook1 = null;
            Excel.Sheets val_sheets = null;
            Excel.Worksheet valsheet = null;
            Excel.Worksheet xlClmWritingsheet = null;
            xlApp1 = new Microsoft.Office.Interop.Excel.Application();
            xlWorkbook1 = xlApp1.Workbooks.Open(valexcelpath, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            val_sheets = xlWorkbook1.Sheets as Excel.Sheets;
            valsheet = (Excel.Worksheet)val_sheets[1];
            xlClmWritingsheet = (Excel.Worksheet)val_sheets[2];
            try
            {

                if (from == "folder")
                {
                    foreach (string f in Directory.GetFiles(ExcelFolderPath))
                    {
                        if (f.ToLower().Contains("xls"))
                        {
                            Excelvalidation(f, sno, val_sheets);
                            // Read(f, sno);
                            sno++;
                        }
                    }
                }
                if (from == "excel")
                {
                    string file, _recoid;
                    for (int e = 2; e < 5000; e++)
                    {
                        file = ExcelFolderPath + valsheet.Cells[e, 1].Text;
                        _recoid = valsheet.Cells[e, 2].Text;
                        if (_recoid.ToLower() == "new")
                        {
                            createnewengt(e, valsheet);
                        }
                        if (string.IsNullOrEmpty(file) == true)
                            break;
                        Excelvalidation(file, sno, val_sheets);
                        sno++;
                    }
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " S.No." + sno);
            }
            finally
            {
                xlWorkbook1.Save();
                xlWorkbook1.Close(Type.Missing, Type.Missing, Type.Missing);
                xlApp1.Quit();
                GC.Collect();
                Kill_Excel_Process();
            }
        }
        public void write_excel_columns(FileInfo finfo, Excel.Worksheet bosheet, Excel.Worksheet sumsheet, int sno, Excel.Sheets WritingSheet)
        {
            Excel.Worksheet writing_bo = (Excel.Worksheet)WritingSheet[2];
            Excel.Worksheet writing_sum = (Excel.Worksheet)WritingSheet[3];
            int col = sno - 1;
            writing_bo.Cells[1, col] = finfo.Name;
            writing_sum.Cells[1, col] = finfo.Name;
            if (bosheet != null && sumsheet != null)
            {
                for (int i = 2; i < 130; i++)
                {
                    writing_bo.Cells[i, col] = bosheet.Cells[i - 1, 1].Text;
                    writing_sum.Cells[i, col] = sumsheet.Cells[i - 1, 1].Text;
                }
            }
        }

        protected void btnMoveps_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt_ps = new DataTable();
            DataTable dt_tmp = new DataTable();
            string[] ps = new string[10] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            dt_ps.Columns.Add("ps_scale", typeof(string));
            dt_ps.Columns.Add("ps_price_level", typeof(string));
            dt_ps.Columns.Add("ps_engt_id", typeof(string));
            dt_ps.Columns.Add("ps_seats_single", typeof(string));
            dt_ps.Columns.Add("ps_t_price_single", typeof(string));
            dt_ps.Columns.Add("ps_scheduletype", typeof(string));
            MasterDataLayer.MasterData objmst = new MasterDataLayer.MasterData();
            ds = objmst.GetTempData();
            dt = ds.Tables[0].DefaultView.ToTable(true, "TotPrice", "recordID");
            string engtid = "", totprice = "", temp_engt_id = "";
            dt1 = dt_ps.Clone();
            for (int i = 0, n = 0; i < dt.Rows.Count; i++, n++)
            {
                if (temp_engt_id != dt.Rows[i]["recordid"].ToString())
                {
                    n = 0;
                }
                temp_engt_id = dt.Rows[i]["recordid"].ToString();
                dt1 = dt_ps.Clone();
                engtid = dt.Rows[i]["recordID"].ToString();
                totprice = dt.Rows[i]["TotPrice"].ToString();
                dt_tmp = ds.Tables[0].Select("recordid=" + engtid + " and TotPrice=" + totprice).CopyToDataTable();


                for (int j = 0; j < dt_tmp.Rows.Count; j++)
                {
                    for (int k = 0; k < 10 && j == 0; k++)
                    {
                        DataRow dr = dt1.NewRow();
                        dr["ps_seats_single"] = dt_tmp.Rows[0]["SCALE" + (k + 1).ToString() + "TICKET"].ToString();
                        dr["ps_t_price_single"] = dt_tmp.Rows[0]["Scale" + (k + 1).ToString() + "price"].ToString();
                        dr["ps_engt_id"] = engtid;
                        dr["ps_scale"] = "A";
                        dr["ps_price_level"] = ps[k];
                        dr["ps_scheduletype"] = dt_tmp.Rows[0]["schedule_type"].ToString();
                        if (string.IsNullOrEmpty(dt_tmp.Rows[0]["SCALE" + (k + 1).ToString() + "TICKET"].ToString()) == true)
                        {
                            break;
                        }
                        dt1.Rows.Add(dr);
                    }
                    dt1.Columns["ps_scheduletype"].DefaultValue = "A";
                    dt1.Select("").ToList<DataRow>().ForEach(r => r["ps_scheduletype"] = dt_tmp.Rows[j]["schedule_type"].ToString());
                    dt1.Select("").ToList<DataRow>().ForEach(r => r["ps_scale"] = ps[n].ToString());
                    dt_ps.Merge(dt1);
                }
            }
            objmst.sqlbcopy(dt_ps, "temp_pricescale");
        }

        protected void btnvalfromexcel_Click(object sender, EventArgs e)
        {
            //validate("excel");
            write_to_db();
        }
        public string createnewengt(int rowid, Excel.Worksheet _valsheet)
        {
            string showid, cityid, venueid, presenterid, statename = "";
            DateTime engtdate;
            showid = _valsheet.Cells[rowid, 18].Text;
            cityid = _valsheet.Cells[rowid, 15].Text;
            if (cityid.Split(',').Length > 1)
            {
                statename = cityid.Split(',').GetValue(1).ToString();
            }
            cityid = cityid.Split(',').GetValue(0).ToString();
            venueid = _valsheet.Cells[rowid, 19].Text;
            presenterid = _valsheet.Cells[rowid, 22].Text;
            engtdate = Convert.ToDateTime(_valsheet.Cells[rowid, 21].Text);
            MasterData objmst = new MasterData();
            String newengtid = objmst.EngtInsert_fromexcel(showid, cityid, venueid, presenterid, engtdate, statename);
            return newengtid.ToString();
        }

        protected void btnTemptoMain_Click(object sender, EventArgs e)
        {
            MasterData objmst = new MasterData();
            try
            {
                objmst.Insert_Temp_To_MainTable();
            }
            catch (Exception ex)
            {
                lblErrmsg.Text = ex.Message;
            }

        }
        protected void btnwritecolumns_Click(object sender, EventArgs e)
        {
            valstatus = 2;
            validate("folder");

        }
        protected void btnupload_Click(object sender, EventArgs e)
        {
            HttpFileCollection hfc = Request.Files;
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];
                if (hpf.ContentLength > 0)
                {
                    hpf.SaveAs(ExcelFolderPath + Path.GetFileName(hpf.FileName));
                }
            }
        }

        protected void btncleanfolder_Click(object sender, EventArgs e)
        {
            foreach (string f in Directory.GetFiles(ExcelFolderPath))
            {
                File.Delete(f);
            }
        }
        public void ReadFolderFiles()
        {
            copy_validation_template();
            int cvrindex = 0, rwindex = 2, flg = 1;
            string foldername = "", _showname = "", _venuename = "", _cityname = "", _presentername = "", _opdate = "", _cldate = "", _statename = "";
            Excel.Workbook valbook = ExApp.Workbooks.Open(ValidationExcelPath, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            Excel.Worksheet ws_Allclms = valbook.Sheets[4];
            Excel.Worksheet ws_Validation = valbook.Sheets[1];
            DataTable dt;
            FileInfo fi;
            try
            {
                foreach (string fld in Directory.GetDirectories(ExcelFolderPath))
                {
                    foldername = new DirectoryInfo(fld).Name;
                    foreach (string f_path in Directory.GetFiles(fld))
                    {
                        fi = new FileInfo(f_path);
                        cvrindex = 0;
                        ExWorkbook = ExApp.Workbooks.Open(f_path, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                        ExSheets = (Excel.Sheets)ExWorkbook.Sheets;
                        for (int l = 1; l <= ExWorkbook.Sheets.Count; l++)
                        {
                            if (ExWorkbook.Sheets[l].Name.ToString().Trim().Replace(" ", "").ToLower() == "coversheet")
                            {
                                cvrindex = l;
                            }
                        }
                        Excel.Worksheet cvrsheet = ExSheets[cvrindex];
                        _showname = cvrsheet.Cells[1, 1].Text;
                        _cityname = cvrsheet.Cells[3, 2].Text;
                        if (_cityname.Split(',').Length > 1)
                        {
                            _statename = _cityname.Split(',').GetValue(1).ToString();
                        }
                        _cityname = _cityname.Split(',').GetValue(0).ToString();
                        _venuename = cvrsheet.Cells[4, 2].Text;
                        _presentername = cvrsheet.Cells[5, 2].Text;
                        _opdate = cvrsheet.Cells[8, 1].Text;
                        _cldate = cvrsheet.Cells[8, 2].Text;
                        if (flg == 1)
                        {
                            ws_Validation.Cells[rwindex, 1] = fi.Name;
                            ws_Validation.Cells[rwindex, 14] = _showname;
                            ws_Validation.Cells[rwindex, 15] = _cityname;
                            ws_Validation.Cells[rwindex, 16] = _opdate;
                            ws_Validation.Cells[rwindex, 17] = _cldate;
                            ws_Validation.Cells[rwindex, 26] = _venuename;
                            ws_Validation.Cells[rwindex, 27] = _presentername;
                            dt = objmst.spGetExcelKeyFieldsID(_showname, _cityname, _venuename, _presentername, _statename);
                            if (dt.Rows.Count > 0)
                            {
                                ws_Validation.Cells[rwindex, 18] = dt.Rows[0]["showname"].ToString();
                                ws_Validation.Cells[rwindex, 20] = dt.Rows[0]["cityname"].ToString();
                                ws_Validation.Cells[rwindex, 19] = dt.Rows[0]["venuename"].ToString();
                                ws_Validation.Cells[rwindex, 22] = dt.Rows[0]["presentername"].ToString();
                            }
                        }
                        else if (flg == 4)
                        {
                            #region Write Allcolumns
                            ws_Allclms.Cells[rwindex, 1] = new FileInfo(f_path).Name;
                            ws_Allclms.Cells[rwindex, 2] = _showname;
                            ws_Allclms.Cells[rwindex, 3] = _cityname;
                            ws_Allclms.Cells[rwindex, 4] = _venuename;
                            ws_Allclms.Cells[rwindex, 5] = _presentername;
                            ws_Allclms.Cells[rwindex, 6] = foldername;
                            #endregion
                        }
                        ExWorkbook.Close(false, Type.Missing, Type.Missing);
                        rwindex++;
                    }

                }
            }
            catch (Exception ex)
            {
                lblErrmsg.Text = ex.Message;
            }
            finally
            {
                valbook.Save();
                valbook.Close(Type.Missing, Type.Missing, Type.Missing);
                ExApp.Workbooks.Close();
                ExApp.Quit();
                Kill_Excel_Process();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ExApp);
                ExApp = null;
                if (flg == 1)
                {
                    lnkvalexcel.Visible = true;
                }
            }
        }

        protected void btnkillprocess_Click(object sender, EventArgs e)
        {
            foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
            {
                if (proc.MainWindowTitle.ToString() == "")
                {
                    proc.Kill();
                }
            }
        }
    }


}