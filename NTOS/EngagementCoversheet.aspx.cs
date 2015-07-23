using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoverSheetDataLayer;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;

namespace NTOS
{
    public partial class EngagementCoversheet : System.Web.UI.Page, MasterPageSaveInterface
    {
        Label lbl_msg;
        DataTable dt = new DataTable();
        CoverSheetData objcvr = new CoverSheetData();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "$('.dollor').autoNumeric({ aSign: '$', vMax: '999999999999999.99' });", true);
                hdn_dcc_total.Value = "0";
                hdn_ocr_total.Value = "0";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int engagementid = Convert.ToInt32(Request.QueryString["engmtid"]);
            string schcount = Request.QueryString["schcount"];
            this.Master.FindControl("ImageButton1").Visible = false;
            (this.Master as EngagementMaster).SetActiveTab("licover");
           (this.Master as EngagementMaster).hidesummary();
            //((ImageButton)this.Master.FindControl("imgbtncs")).ImageUrl = "~/Images/tabb-cs.png";
            hdn_engagementid.Value = engagementid.ToString();
            hdn_schedulecount.Value = schcount;
            ValidationSummary mastersummary = (ValidationSummary)this.Master.FindControl("val_summarymaster");
            mastersummary.Enabled = false;
            lbl_msg = (Label)this.Master.FindControl("lbl_message");
            lbl_msg.Text = "";
            if (!Page.IsPostBack)
            {
                if (engagementid > 0)
                {
                    lbl_cs.Visible = false;
                    div_cs.Visible = true;
                    CreateEmail_List();
                    LoadCoverSheetDetails();
                    if (string.IsNullOrEmpty(Request.QueryString["status"])==false && Request.QueryString["status"]=="1")
                    {
                        lbl_msg.Text = "Record submitted successfully!";
                        lbl_msg.ForeColor = System.Drawing.Color.Green;
                    }

                }
            }
        }

        public void LoadCoverSheetDetails()
        {
            Int32 cvr_engt_id = Convert.ToInt32(hdn_engagementid.Value);
            dt = new DataTable();
            dt = objcvr.GetCoverSheetDetails(cvr_engt_id, "D");
            if (dt.Rows.Count > 0)
            {
                chksetcvrpage.Checked = (dt.Rows[0]["CVR_S_COVER_FLAG"].ToString() == "Y") ? true : false;
                chkguarantee.Checked = (dt.Rows[0]["CVR_GRNTY_FLAG"].ToString() == "Y") ? true : false;
                chkroyalty.Checked = (dt.Rows[0]["CVR_ROYALTY_FLAG"].ToString() == "Y") ? true : false;
                chkoverage.Checked = (dt.Rows[0]["CVR_OVRG_FLAG"].ToString() == "Y") ? true : false;
                chksetsummary.Checked = (dt.Rows[0]["CVR_S_SUMMARY_FLAG"].ToString() == "Y") ? true : false;
                chkvenuesettlement.Checked = (dt.Rows[0]["CVR_VENUE_SETT_FLAG"].ToString() == "Y") ? true : false;
                chkboxoffice.Checked = (dt.Rows[0]["CVR_BO_SHEET_FLAG"].ToString() == "Y") ? true : false;
                chkboxoffsettlement.Checked = (dt.Rows[0]["CVR_BO_STATEMENTS_FLAG"].ToString() == "Y") ? true : false;
                chklabourbills.Checked = (dt.Rows[0]["CVR_LBR_BILLS_FLAG"].ToString() == "Y") ? true : false;
                chkmusicians.Checked = (dt.Rows[0]["CVR_MUSICIAN_BILLS_FLAG"].ToString() == "Y") ? true : false;
                chklocaldocexp.Checked = (dt.Rows[0]["CVR_LOCAL_EXP_INVOICE_FLAG"].ToString() == "Y") ? true : false;
                chkadvertising.Checked = (dt.Rows[0]["CVR_AD_FLAG"].ToString() == "Y") ? true : false;
                chkcontractcopy.Checked = (dt.Rows[0]["CVR_CONTACT_FLAG"].ToString() == "Y") ? true : false;
                txtsetcvrpagenotes.Text = dt.Rows[0]["CVR_S_COVER_NOTES"].ToString();
                txtguaranteenotes.Text = dt.Rows[0]["CVR_GRNTY_NOTES"].ToString();
                txtroyaltynotes.Text = dt.Rows[0]["CVR_ROYALTY_NOTES"].ToString();
                txtoveragenotes.Text = dt.Rows[0]["CVR_OVRG_NOTES"].ToString();
                txtsettlementsumnotes.Text = dt.Rows[0]["CVR_S_SUMMARY_NOTES"].ToString();
                txtvenuesettlementnotes.Text = dt.Rows[0]["CVR_VENUE_SETT_NOTES"].ToString();
                txtboxofficenotes.Text = dt.Rows[0]["CVR_BO_SHEET_NOTES"].ToString();
                txtboxoffsettlementnotes.Text = dt.Rows[0]["CVR_BO_STATEMENTS_NOTES"].ToString();
                txtlabourbillsnotes.Text = dt.Rows[0]["CVR_LBR_BILLS_NOTES"].ToString();
                txtmusiciansnotes.Text = dt.Rows[0]["CVR_MUSICIAN_BILLS_NOTES"].ToString();
                txtlocaldocexpnotes.Text = dt.Rows[0]["CVR_LOCAL_EXP_INVOICE_NOTES"].ToString();
                txtadvertisingnotes.Text = dt.Rows[0]["CVR_AD_NOTES"].ToString();
                txtcontractcopynotes.Text = dt.Rows[0]["CVR_CONTACT_NOTES"].ToString();
                txtemailidlist.Text = dt.Rows[0]["EmailList"].ToString();
                hdnemaillist.Value = dt.Rows[0]["EmailList"].ToString();
            }
            LoadCharges();
            LoadReceivables();
       //     tr_dcc_footer.Visible = tr_ocr_footer.Visible = false;

        }
        public void CreateEmail_List()
        {
            dt = new DataTable();
            dt.Columns.Add("SlNo", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            DataRow dr;
            for (int i = 0; i < 50; i++)
            {
                dr = dt.NewRow();
                dr["SlNo"] = i + 1;
                dt.Rows.Add(dr);
            }
            repdistribution.DataSource = dt;
            repdistribution.DataBind();
        }
        public void SaveData()
        {
            if (IsEmailvalid() == false && txtemailidlist.Text.Trim() != "")
            {

                return;
            }
            EngagementMaster EngmtMaster = (EngagementMaster)Page.Master;
            if (hdn_engagementid.Value != "0" && hdn_engagementid.Value != "")
            {
                //if (hdn_schedulecount.Value != "0" && hdn_schedulecount.Value != "")
                //{
                SaveDocuments();
                SaveDirectCompanyCharges();
                SaveOSCompanyReceivables();
                SaveEmailList();
                lbl_msg.Text = "Record submitted successfully!";
                lbl_msg.ForeColor = System.Drawing.Color.Green;
                if (string.IsNullOrEmpty(Request.QueryString["status"]))
                {
                    Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri + "&status=1");
                }
                else
                {
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }

                    //}
                //else
                //{
                //    lbl_msg.Text = "Please create Engagement Schedule first";
                //    lbl_msg.ForeColor = System.Drawing.Color.Red;
                //}
            }
        }
        public void SaveDocuments()
        {
            Int32 cvr_engt_id = Convert.ToInt32(hdn_engagementid.Value);
            string cvr_cover_flag, cvr_grnty_flag, cvr_royalty_flag, cvr_ovrg_flag, cvr_s_summary_flag, cvr_venue_sett_flag,
            cvr_bo_sheet_flag, cvr_bo_statements_flag, cvr_lbr_bills_flag, cvr_musician_bills_flag, cvr_local_exp_invoice_flag, cvr_ad_flag,
            cvr_contact_flag, cvr_cover_notes, cvr_grnty_notes, cvr_royalty_notes, cvr_ovrg_notes, cvr_s_summary_notes, cvr_venue_sett_notes,
           cvr_bo_sheet_notes, cvr_bo_statements_notes, cvr_lbr_bills_notes, cvr_musician_bills_notes, cvr_local_exp_invoice_notes, cvr_ad_notes, cvr_contact_notes;
            cvr_cover_flag = (chksetcvrpage.Checked == true) ? "Y" : "N";
            cvr_grnty_flag = (chkguarantee.Checked == true) ? "Y" : "N";
            cvr_royalty_flag = (chkroyalty.Checked == true) ? "Y" : "N";
            cvr_ovrg_flag = (chkoverage.Checked == true) ? "Y" : "N";
            cvr_s_summary_flag = (chksetsummary.Checked == true) ? "Y" : "N";
            cvr_venue_sett_flag = (chkvenuesettlement.Checked == true) ? "Y" : "N";
            cvr_bo_sheet_flag = (chkboxoffice.Checked == true) ? "Y" : "N";
            cvr_bo_statements_flag = (chkboxoffsettlement.Checked == true) ? "Y" : "N";
            cvr_lbr_bills_flag = (chklabourbills.Checked == true) ? "Y" : "N";
            cvr_musician_bills_flag = (chkmusicians.Checked == true) ? "Y" : "N";
            cvr_local_exp_invoice_flag = (chklocaldocexp.Checked == true) ? "Y" : "N";
            cvr_ad_flag = (chkadvertising.Checked == true) ? "Y" : "N";
            cvr_contact_flag = (chkcontractcopy.Checked == true) ? "Y" : "N";
            cvr_cover_notes = txtsetcvrpagenotes.Text.Trim();
            cvr_grnty_notes = txtguaranteenotes.Text.Trim();
            cvr_royalty_notes = txtroyaltynotes.Text.Trim();
            cvr_ovrg_notes = txtoveragenotes.Text.Trim();
            cvr_s_summary_notes = txtsettlementsumnotes.Text.Trim();
            cvr_venue_sett_notes = txtvenuesettlementnotes.Text.Trim();
            cvr_bo_sheet_notes = txtboxofficenotes.Text.Trim();
            cvr_bo_statements_notes = txtboxoffsettlementnotes.Text.Trim();
            cvr_lbr_bills_notes = txtlabourbillsnotes.Text.Trim();
            cvr_musician_bills_notes = txtmusiciansnotes.Text.Trim();
            cvr_local_exp_invoice_notes = txtlocaldocexpnotes.Text.Trim();
            cvr_ad_notes = txtadvertisingnotes.Text.Trim();
            cvr_contact_notes = txtcontractcopynotes.Text.Trim();
            Int32 cvrsheetid = objcvr.Cvr_DocumentInsert(cvr_engt_id, cvr_cover_flag, cvr_grnty_flag, cvr_royalty_flag, cvr_ovrg_flag, cvr_s_summary_flag, cvr_venue_sett_flag,
             cvr_bo_sheet_flag, cvr_bo_statements_flag, cvr_lbr_bills_flag, cvr_musician_bills_flag, cvr_local_exp_invoice_flag, cvr_ad_flag,
             cvr_contact_flag, cvr_cover_notes, cvr_grnty_notes, cvr_royalty_notes, cvr_ovrg_notes, cvr_s_summary_notes, cvr_venue_sett_notes,
            cvr_bo_sheet_notes, cvr_bo_statements_notes, cvr_lbr_bills_notes, cvr_musician_bills_notes, cvr_local_exp_invoice_notes, cvr_ad_notes, cvr_contact_notes);
        }
        public void SaveOSCompanyReceivables()
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            char[] chDlr = { '$', ',', ' ' };
            Int32 cvr_engt_id = Convert.ToInt32(hdn_engagementid.Value);
            string cvr_cvabls_desc, cvr_cvabls_notes;
            Nullable<decimal> cvr_cvabls_charge = null;
            cvr_cvabls_desc = textInfo.ToTitleCase(txt_ocr_desc.Text.Trim());
            cvr_cvabls_charge = (txt_ocr_charges.Text != "") ? Convert.ToDecimal(txt_ocr_charges.Text.Trim(chDlr)) : cvr_cvabls_charge;
            cvr_cvabls_notes = txt_ocr_notes.Text;
            string msg = "";
            if (cvr_cvabls_desc.Trim() != "")
                msg = objcvr.Cvr_ReceivablesInsert(cvr_engt_id, cvr_cvabls_desc, cvr_cvabls_charge, cvr_cvabls_notes);
            lbl_msg.Text = msg;
            lbl_msg.ForeColor = System.Drawing.Color.Orange;
        }
        public void SaveDirectCompanyCharges()
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            char[] chDlr = { '$', ',', ' ' };
            Int32 cvr_engt_id = Convert.ToInt32(hdn_engagementid.Value);
            string cvr_chgs_desc, cvr_chgs_check, cvr_chgs_notes;
            Nullable<decimal> cvr_chgs_amt = null;
            cvr_chgs_desc = textInfo.ToTitleCase(txt_dcc_desc.Text.Trim());
            cvr_chgs_check = txt_dcc_check.Text;
            cvr_chgs_notes = txt_dcc_notes.Text;
            cvr_chgs_amt = (txt_dcc_charges.Text != "") ? Convert.ToDecimal(txt_dcc_charges.Text.Trim(chDlr)) : cvr_chgs_amt;
            string msg = "";
            if (cvr_chgs_desc.Trim() != "")
                msg = objcvr.Cvr_ChargesInsert(cvr_engt_id, cvr_chgs_desc, cvr_chgs_amt, cvr_chgs_check, cvr_chgs_notes);
            lbl_msg.Text = msg;
            lbl_msg.ForeColor = System.Drawing.Color.Orange;
        }
        public void SaveEmailList()
        {
            if (hdnemaillist.Value != txtemailidlist.Text)
            {
                Int32 cvr_engt_id = Convert.ToInt32(hdn_engagementid.Value);
                string emailid = "";
                string[] emaillist = txtemailidlist.Text.Split(',');
                for (int i = 0; i < emaillist.Length; i++)
                {
                    emailid = emaillist[i];
                    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = regex.Match(emailid);
                    objcvr.Cvr_EmailInsert(cvr_engt_id, emailid, i);
                }
            }
        }
        public bool IsEmailvalid()
        {
            string emailid = "";
            string[] emaillist = txtemailidlist.Text.Split(',');
            for (int i = 0; i < emaillist.Length; i++)
            {
                emailid = emaillist[i];
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(emailid);
                if (match.Success == false)
                {
                    lbl_msg.Text = "Invalid email id: " + emailid + "";
                    lbl_msg.ForeColor = System.Drawing.Color.Orange;
                    return false;
                }
            }
            return true;
        }
        public void DeleteData(string flag)
        {
            EngagementMaster EngmtMaster = (EngagementMaster)Page.Master;
            int delflag = EngmtMaster.DeleteEngagement(flag);
            lbl_msg.Text = "Engagement Deleted successfully";
            lbl_msg.ForeColor = System.Drawing.Color.Green;
        }
        public void LoadCharges()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            Int32 cvr_engt_id = Convert.ToInt32(hdn_engagementid.Value);
            dt = new DataTable();
            dt = objcvr.GetCoverSheetDetails(cvr_engt_id, "C");
            rep_dir_com_chg.DataSource = dt;
            rep_dir_com_chg.DataBind();
            decimal totamt = 0;
            if (dt.Rows.Count > 0 && dt.Compute("Sum(cvr_chgs_amt)", "").ToString() != "")
            {
                chkSelectAll.Visible = true;
                totamt = Convert.ToDecimal(dt.Compute("Sum(cvr_chgs_amt)", ""));
                hdn_dcc_total.Value = totamt.ToString();
                lbl_dcc_total.Text = totamt.ToString("C");
            }
        }
        public void LoadReceivables()
        {
            Int32 cvr_engt_id = Convert.ToInt32(hdn_engagementid.Value);
            dt = new DataTable();
            dt = objcvr.GetCoverSheetDetails(cvr_engt_id, "R");
            rep_os_com_receivables.DataSource = dt;
            rep_os_com_receivables.DataBind();
            if (dt.Rows.Count > 0 && dt.Compute("Sum(cvr_cvabls_charge)", "").ToString() != "")
            {
                CheckBox1.Visible = true;
                decimal totamt = Convert.ToDecimal(dt.Compute("Sum(cvr_cvabls_charge)", ""));
                hdn_ocr_total.Value = totamt.ToString();
                lbl_ocr_total.Text = totamt.ToString("C");
            }
        }
        protected void lnkbtn_dccAdd_Click(object sender, EventArgs e)
        {
            if (tr_dcc_footer.Visible == true)
            {
                SaveDirectCompanyCharges();
                if (lbl_msg.Text == "")
                    LoadCharges();
            }
            else
            {
                tr_dcc_footer.Visible = true;
            } if (lbl_msg.Text == "")
            {
                txt_dcc_charges.Text = string.Empty;
                txt_dcc_desc.Text = string.Empty;
                txt_dcc_check.Text = string.Empty;
                txt_dcc_notes.Text = string.Empty;
            }
        }

        protected void lnkbtn_dccDelete_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem gr in rep_dir_com_chg.Items)
            {
                CheckBox chkdelete = (CheckBox)gr.FindControl("chk_dcc_delete");
                HiddenField hdnchargesid = (HiddenField)gr.FindControl("hdnchargesid");
                if (chkdelete.Checked == true && hdnchargesid.Value != "0")
                {
                    objcvr.Cvr_SheetDelete(Convert.ToInt32(hdnchargesid.Value), "C");
                }
            }
            LoadCharges();
            tr_dcc_footer.Visible = false;
        }

        protected void lnkbtn_ocradd_Click(object sender, EventArgs e)
        {
            if (tr_ocr_footer.Visible == true)
            {
                SaveOSCompanyReceivables();
                if (lbl_msg.Text == "")
                    LoadReceivables();
            }
            else
            {
                tr_ocr_footer.Visible = true;
            }
            if (lbl_msg.Text == "")
            {
                txt_ocr_desc.Text = string.Empty;
                txt_ocr_charges.Text = string.Empty;
                txt_ocr_notes.Text = string.Empty;
            }
        }

        protected void lnkbtn_ocrdelete_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem gr in rep_os_com_receivables.Items)
            {
                CheckBox chkdelete = (CheckBox)gr.FindControl("chk_ocr_delete");
                HiddenField hdnchargesid = (HiddenField)gr.FindControl("hdnreceivablesid");
                if (chkdelete.Checked == true && hdnchargesid.Value != "0")
                {
                    objcvr.Cvr_SheetDelete(Convert.ToInt32(hdnchargesid.Value), "R");
                }
            }
            LoadReceivables();
            tr_ocr_footer.Visible = false;
        }
        public void Reset()
        {
            ClearAll();
        }
        public void ClearAll()
        {
            chksetcvrpage.Checked = false;
            chkguarantee.Checked = false;
            chkroyalty.Checked = false;
            chkoverage.Checked = false;
            chksetsummary.Checked = false;
            chkvenuesettlement.Checked = false;
            chkboxoffice.Checked = false;
            chkboxoffsettlement.Checked = false;
            chklabourbills.Checked = false;
            chkmusicians.Checked = false;
            chklocaldocexp.Checked = false;
            chkadvertising.Checked = false;
            chkcontractcopy.Checked = false;
            txtsetcvrpagenotes.Text = string.Empty;
            txtguaranteenotes.Text = string.Empty;
            txtroyaltynotes.Text = string.Empty;
            txtoveragenotes.Text = string.Empty;
            txtsettlementsumnotes.Text = string.Empty;
            txtvenuesettlementnotes.Text = string.Empty;
            txtboxofficenotes.Text = string.Empty;
            txtboxoffsettlementnotes.Text = string.Empty;
            txtlabourbillsnotes.Text = string.Empty;
            txtmusiciansnotes.Text = string.Empty;
            txtlocaldocexpnotes.Text = string.Empty;
            txtadvertisingnotes.Text = string.Empty;
            txtcontractcopynotes.Text = string.Empty;
            txtemailidlist.Text = string.Empty;
            hdnemaillist.Value = "0";
            txt_dcc_charges.Text = string.Empty;
            txt_dcc_desc.Text = string.Empty;
            txt_dcc_check.Text = string.Empty;
            txt_dcc_notes.Text = string.Empty;
            txt_ocr_desc.Text = string.Empty;
            txt_ocr_charges.Text = string.Empty;
            txt_ocr_notes.Text = string.Empty;
        }

        protected void rep_dir_com_chg_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "edit")
            {
                e.Item.FindControl("tr_dcc_edit").Visible = true;
                e.Item.FindControl("tr_dcc_crow").Visible = false;
            }
            if (e.CommandName.ToLower() == "cancel")
            {
                LoadCharges();
            }
            if (e.CommandName.ToLower() == "update")
            {
                UpdateDirectCompanyCharges(e.Item);
                LoadCharges();
            }
            tr_dcc_footer.Visible = false;
        }
        public void UpdateDirectCompanyCharges(RepeaterItem ri)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            TextBox txt_dcc_descE = (TextBox)ri.FindControl("txt_dcc_descE");
            TextBox txt_dcc_checkE = (TextBox)ri.FindControl("txt_dcc_checkE");
            TextBox txt_dcc_notesE = (TextBox)ri.FindControl("txt_dcc_notesE");
            TextBox txt_dcc_chargesE = (TextBox)ri.FindControl("txt_dcc_chargesE");
            HiddenField hdnchargesid = (HiddenField)ri.FindControl("hdnchargesid");
            char[] chDlr = { '$', ',', ' ' };
            Int32 cvr_engt_id = Convert.ToInt32(hdn_engagementid.Value);
            string cvr_chgs_desc, cvr_chgs_check, cvr_chgs_notes;
            Nullable<decimal> cvr_chgs_amt = null;
            cvr_chgs_desc = textInfo.ToTitleCase(txt_dcc_descE.Text.Trim());
            cvr_chgs_check = txt_dcc_checkE.Text;
            cvr_chgs_notes = txt_dcc_notesE.Text;
            cvr_chgs_amt = (txt_dcc_chargesE.Text != "") ? Convert.ToDecimal(txt_dcc_chargesE.Text.Trim(chDlr)) : cvr_chgs_amt;
            string msg = "";
            if (cvr_chgs_desc.Trim() != "")
                msg = objcvr.Cvr_ChargesUpdate(Convert.ToInt32(hdnchargesid.Value), cvr_engt_id, cvr_chgs_desc, cvr_chgs_amt, cvr_chgs_check, cvr_chgs_notes);
            lbl_msg.Text = msg;
            lbl_msg.ForeColor = System.Drawing.Color.Orange;
        }

        protected void rep_os_com_receivables_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "edit")
            {
                e.Item.FindControl("tr_ocr_edit").Visible = true;
                e.Item.FindControl("tr_ocr_crow").Visible = false;
            }
            if (e.CommandName.ToLower() == "cancel")
            {
                LoadReceivables();
            }
            if (e.CommandName.ToLower() == "update")
            {
                UpdateOSCompanyReceivables(e.Item);
                LoadReceivables();
            }
            tr_ocr_footer.Visible = false;
        }
        public void UpdateOSCompanyReceivables(RepeaterItem ri)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            TextBox txt_ocr_descE = (TextBox)ri.FindControl("txt_ocr_descE");
            TextBox txt_ocr_chargesE = (TextBox)ri.FindControl("txt_ocr_chargesE");
            TextBox txt_ocr_notesE = (TextBox)ri.FindControl("txt_ocr_notesE");
            HiddenField hdnreceivablesid = (HiddenField)ri.FindControl("hdnreceivablesid");
            char[] chDlr = { '$', ',', ' ' };
            Int32 cvr_engt_id = Convert.ToInt32(hdn_engagementid.Value);
            string cvr_cvabls_desc, cvr_cvabls_notes;
            Nullable<decimal> cvr_cvabls_charge = null;
            cvr_cvabls_desc = textInfo.ToTitleCase(txt_ocr_descE.Text.Trim());
            cvr_cvabls_charge = (txt_ocr_chargesE.Text != "") ? Convert.ToDecimal(txt_ocr_chargesE.Text.Trim(chDlr)) : cvr_cvabls_charge;
            cvr_cvabls_notes = txt_ocr_notesE.Text;
            string msg = "";
            if (cvr_cvabls_desc.Trim() != "")
                msg = objcvr.Cvr_ReceivablesUpdate(Convert.ToInt32(hdnreceivablesid.Value), cvr_engt_id, cvr_cvabls_desc, cvr_cvabls_charge, cvr_cvabls_notes);
            lbl_msg.Text = msg;
            lbl_msg.ForeColor = System.Drawing.Color.Orange;
        }
    }
}