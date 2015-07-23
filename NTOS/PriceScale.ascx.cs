using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data;
using PriceScaleDataLayer;
using System.Web.UI.HtmlControls;
namespace NTOS
{
    public partial class PriceScale : System.Web.UI.UserControl
    {
        PriceScaleData psd = new PriceScaleData();
        //public event EventHandler ButtonClickDemo;

        protected void Page_Load(object sender, EventArgs e)
        {

           // ScriptManager.RegisterStartupScript(this, this.GetType(), "", "changemodifystatus();", true);
            // this.lnkbtnDelete.OnClientClick = "return checkSelRows_" + this.ID + "();";         
        }
        protected override bool OnBubbleEvent(object source, EventArgs args)
        {
            //handled
            return true;

            //uncomment line below to bubble up (unhandled)
            //return base.OnBubbleEvent(source, args);
        }
        #region MyRegion
        protected void calc_TextChanged(object sender, EventArgs e)
        {
            System.Web.UI.Control txt = null;
            if (sender.GetType().Name.ToLower() == "dropdownlist")
                txt = (DropDownList)sender;
            if (sender.GetType().Name.ToLower() == "textbox")
                txt = (TextBox)sender;
            string focusid = "";
            switch (txt.ID)
            {
                case "txt_ps1_seats":
                    focusid = "txt_ps1_ticketprice";
                    break;
                case "txt_ps1_ticketprice":
                    focusid = "txt_ps1_seatingdetail";
                    break;
                case "txt_ps1_seatingdetail":
                    focusid = "drp_ps1_sunit";
                    break;
                case "drp_ps1_sunit":
                    focusid = "drp_ps1_sunit";
                    break;
                case "txt_ps1_sdiscount":
                    focusid = "drp_ps1_gunit";
                    break;
                case "drp_ps1_gunit":
                    focusid = "drp_ps1_gunit";
                    break;
                case "txt_ps1_gdiscount":
                    focusid = "txt_ps1_seats";
                    break;
            }
            GridViewRow gr;
            gr = (GridViewRow)txt.Parent.Parent;
            if (txt.ID == "txt_ps1_gdiscount" && (gr.RowIndex + 1) != gdv_ps1.Rows.Count)
                gr = (GridViewRow)gdv_ps1.Rows[gr.RowIndex + 1];
            System.Web.UI.Control txtfocus = (System.Web.UI.Control)gr.FindControl(focusid);
            string cntrid = ((System.Web.UI.Control)(sender)).ID;
            decimal psxtotal = 0;
            psxtotal = 0;
            decimal ps1totalout = RecalculateGrid(gdv_ps1, psxtotal);
            lbl_ps1total.Text = ps1totalout.ToString("C2", new CultureInfo("en-US"));
            lbl_ps1subtotal.Text = (Convert.ToInt16(lbl_ps1shows.Text) * ps1totalout).ToString("C2", new CultureInfo("en-US"));
            if (sender.GetType().Name.ToLower() == "textbox")
            {
                if (txt.ID != "txt_ps1_ticketprice" || (txt as TextBox).Text.Length != 1)
                    txtfocus.Focus();
                if (focusid == "txt_ps1_seatingdetail" && (txtfocus as TextBox).Text.Trim() == "")
                { (txtfocus as TextBox).Text = " "; }
            }
            else
            { txtfocus.Focus(); }
        }
        public void calc_textChanged(object sender)
        {
            System.Web.UI.Control txt = null;

            if (sender.GetType().Name.ToLower() == "dropdownlist")
                txt = (DropDownList)sender;
            if (sender.GetType().Name.ToLower() == "textbox")
                txt = (TextBox)sender;
            string focusid = "";
            switch (txt.ID)
            {
                case "txt_ps1_seats":
                    focusid = "txt_ps1_ticketprice";
                    break;
                case "txt_ps1_ticketprice":
                    focusid = "txt_ps1_seatingdetail";
                    break;
                case "txt_ps1_seatingdetail":
                    focusid = "drp_ps1_sunit";
                    break;
                case "drp_ps1_sunit":
                    focusid = "drp_ps1_sunit";
                    break;
                case "txt_ps1_sdiscount":
                    focusid = "drp_ps1_gunit";
                    break;
                case "drp_ps1_gunit":
                    focusid = "drp_ps1_gunit";
                    break;
                case "txt_ps1_gdiscount":
                    focusid = "txt_ps1_seats";
                    break;
            }
            GridViewRow gr;
            gr = (GridViewRow)txt.Parent.Parent;
            if (txt.ID == "txt_ps1_gdiscount" && (gr.RowIndex + 1) != gdv_ps1.Rows.Count)
                gr = (GridViewRow)gdv_ps1.Rows[gr.RowIndex + 1];
            System.Web.UI.Control txtfocus = (System.Web.UI.Control)gr.FindControl(focusid);
            if (string.IsNullOrEmpty(hdnfocusindex.Value))
            {
                hdnfocusindex.Value = "0";
                hdnfocusid.Value = "0";
            }
            System.Web.UI.Control txtfocus1 = (System.Web.UI.Control)gdv_ps1.Rows[Convert.ToInt32(hdnfocusindex.Value)].FindControl(hdnfocusid.Value);
            int rowcount = gdv_ps1.Rows.Count;
            //string cntrid = ((System.Web.UI.Control)(sender)).ID;
            decimal psxtotal = 0;
            psxtotal = 0;
            decimal ps1totalout = RecalculateGrid(gdv_ps1, psxtotal);
            lbl_ps1total.Text = ps1totalout.ToString("C2", new CultureInfo("en-US"));
            lbl_ps1subtotal.Text = (Convert.ToInt16(lbl_ps1shows.Text) * ps1totalout).ToString("C2", new CultureInfo("en-US"));
            //if (sender.GetType().Name.ToLower() == "textbox")
            //{
            //    if (txt.ID != "txt_ps1_ticketprice" || (txt as TextBox).Text.Length != 1)
            //        txtfocus.Focus();
            //    if (focusid == "txt_ps1_seatingdetail" && (txtfocus as TextBox).Text.Trim() == "")
            //    { (txtfocus as TextBox).Text = " "; }
            //}
            //else
            //{ txtfocus.Focus(); }
            if (txtfocus1 != null)
                txtfocus1.Focus();
        }
        protected void txt_ps1_seats_TextChanged(object sender, EventArgs e)
        {
            calc_textChanged(sender);
            //TextBox t = (TextBox)sender;
            //GridViewRow gr = (GridViewRow)t.Parent.Parent;
            //string fid = hdnfocusid.Value;
            //TextBox txt_ps1_ticketprice = (TextBox)gr.FindControl("txt_ps1_ticketprice");
            // txt_ps1_ticketprice.Focus();

        }

        protected void txt_ps1_ticketprice_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (!string.IsNullOrEmpty(t.Text.TrimStart('$')))
                calc_textChanged(sender);

        }

        protected void drp_ps1_sunit_SelectedIndexChanged(object sender, EventArgs e)
        {
            calc_textChanged(sender);
            //DropDownList t = (DropDownList)sender;
            // t.Focus();
        }

        protected void txt_ps1_sdiscount_TextChanged(object sender, EventArgs e)
        {
            calc_textChanged(sender);
            //TextBox t = (TextBox)sender;
            //GridViewRow gr = (GridViewRow)t.Parent.Parent;
            //DropDownList drp_ps1_gunit = (DropDownList)gr.FindControl("drp_ps1_gunit");
            // drp_ps1_gunit.Focus();
        }

        protected void txt_ps1_gdiscount_TextChanged(object sender, EventArgs e)
        {
            calc_textChanged(sender);
            //TextBox t = (TextBox)sender;
            //GridViewRow gr = (GridViewRow)t.Parent.Parent;
            //TextBox txt_ps1_seats = (TextBox)gr.FindControl("txt_ps1_seats");
            // txt_ps1_seats.Focus();
        }

        protected void drp_ps1_gunit_SelectedIndexChanged(object sender, EventArgs e)
        {
            calc_textChanged(sender);
            // DropDownList t = (DropDownList)sender;
            // t.Focus();
        }
        #endregion



        public decimal RecalculateGrid(GridView gdv, decimal psxtotal)
        {
            GridView gdv1 = new GridView();
            gdv1 = gdv;
            int seattotal = 0;
            foreach (GridViewRow gvr in gdv.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    TextBox seats = gvr.FindControl("txt_ps1_seats") as TextBox;
                    TextBox price = gvr.FindControl("txt_ps1_ticketprice") as TextBox;
                    Label saleprice = gvr.FindControl("lbl_ps1_saleamount") as Label;
                    DropDownList sunit = gvr.FindControl("drp_ps1_sunit") as DropDownList;
                    TextBox sdiscount = gvr.FindControl("txt_ps1_sdiscount") as TextBox;
                    Label sprice = gvr.FindControl("lbl_ps1_sprice") as Label;
                    DropDownList gunit = gvr.FindControl("drp_ps1_gunit") as DropDownList;
                    TextBox gdiscount = gvr.FindControl("txt_ps1_gdiscount") as TextBox;
                    Label gprice = gvr.FindControl("lbl_ps1_gprice") as Label;
                    seattotal += (string.IsNullOrEmpty(seats.Text) ? 0 : Convert.ToInt32(seats.Text));
                    if (seats.Text.Trim() != "" && Regex.Replace(Convert.ToString(price.Text.AutoformatDecimal()), @"\$|\,", "").Trim() != "")
                    {
                        saleprice.Text = (Convert.ToInt32(seats.Text) * Convert.ToDecimal(Regex.Replace(Convert.ToString(price.Text.AutoformatDecimal()), @"\$|\,", ""))).ToString("C2", new CultureInfo("en-US"));
                        psxtotal = psxtotal + Convert.ToDecimal(Regex.Replace(Convert.ToString(saleprice.Text.AutoformatDecimal()), @"\$|\,", ""));
                    }
                    else
                    {
                        saleprice.Text = "$.00";
                    }
                    if (Regex.Replace(Convert.ToString(price.Text.AutoformatDecimal()), @"\$|\,", "").Trim() != "" && Regex.Replace(Convert.ToString(sdiscount.Text.AutoformatDecimal()), @"\$|\,", "").Trim() != "")
                    {
                        if (sunit.Text == "$")
                        {
                            sprice.Text = (Convert.ToDecimal(Regex.Replace(Convert.ToString(price.Text.AutoformatDecimal()), @"\$|\,", "")) - Convert.ToDecimal(Regex.Replace(Convert.ToString(sdiscount.Text.AutoformatDecimal()), @"\$|\,", ""))).ToString("C2", new CultureInfo("en-US"));
                        }
                        else
                        {
                            sprice.Text = ((1 - (Convert.ToDecimal(Regex.Replace(Convert.ToString(sdiscount.Text.AutoformatDecimal()), @"\$|\,", "")) / 100)) * Convert.ToDecimal(Regex.Replace(Convert.ToString(price.Text.AutoformatDecimal()), @"\$|\,", ""))).ToString("C2", new CultureInfo("en-US"));
                        }
                    }
                    else
                    {
                        sprice.Text = "$.00";
                    }
                    if (Regex.Replace(Convert.ToString(price.Text.AutoformatDecimal()), @"\$|\,", "").Trim() != "" && Regex.Replace(Convert.ToString(gdiscount.Text.AutoformatDecimal()), @"\$|\,", "").Trim() != "")
                    {
                        if (gunit.Text == "$")
                        {
                           // gprice.Text = ((1 - (Convert.ToDecimal(Regex.Replace(Convert.ToString(gdiscount.Text.AutoformatDecimal()), @"\$|\,", "")) / 100)) * Convert.ToDecimal(Regex.Replace(Convert.ToString(price.Text.AutoformatDecimal()), @"\$|\,", ""))).ToString("C2", new CultureInfo("en-US"));
                           gprice.Text = (Convert.ToDecimal(Regex.Replace(Convert.ToString(price.Text.AutoformatDecimal()), @"\$|\,", "")) - Convert.ToDecimal(Regex.Replace(gdiscount.Text, @"\$|\,", ""))).ToString("C2", new CultureInfo("en-US"));
                        }
                        else
                        {
                            gprice.Text = ((1 - (Convert.ToDecimal(Regex.Replace(Convert.ToString(gdiscount.Text.AutoformatDecimal()), @"\$|\,", "")) / 100)) * Convert.ToDecimal(Regex.Replace(Convert.ToString(price.Text.AutoformatDecimal()), @"\$|\,", ""))).ToString("C2", new CultureInfo("en-US"));
                        }
                    }
                    else
                    {
                        gprice.Text = "$.00";
                    }
                }

            }
            lbl_seattotal.Text = seattotal.ToString();
            return psxtotal;
        }
        public void cal()
        {
            lbl_ps1total.Text = "$0.00";
            lbl_ps1shows.Text = "0";
            lbl_ps1subtotal.Text = "0";
        }
        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }
        public void AddNewRow()
        {
            if (gdv_ps1.Rows.Count < 10)
            {
                if (gdv_ps1.Rows[0].Visible == false)
                {
                    gdv_ps1.Rows[0].Visible = true;
                    Label lbl_ps1_pricelevel = (Label)gdv_ps1.Rows[0].FindControl("lbl_ps1_pricelevel");
                    lbl_ps1_pricelevel.Text = "A";
                    gdv_ps1.Rows[0].Cells[1].Focus();
                }
                else
                    AddGridRow();
            }
        }
        public void AddGridRow()
        {

            int engagementid = Convert.ToInt32(Request.QueryString["engmtid"]);
            Int32 reccount = 0;
            string ps_scale = hdnps_scale.Value;
            DataTable dt = psd.GetPriceScales(engagementid, reccount, ps_scale);
            DataTable dt1 = new DataTable();
            //if (dt.Select("PS_Scale='" + ps_scale + "'", "").Length > 0)
            //    dt1 = dt.Select("PS_Scale='" + ps_scale + "'", "").CopyToDataTable();
            //else
            dt1 = dt.Clone();

            filldata(dt1, "a");

            DataTable pdaytime = psd.GetScheduleDayTime(engagementid);
            DataTable pfcount = dt.DefaultView.ToTable(true, "ps_scale");
            gdv_ps1.DataSource = dt1;
            gdv_ps1.DataBind();
            gdv_ps1.Rows[gdv_ps1.Rows.Count - 1].Cells[1].Focus();
        }
        public void filldata(DataTable dt, string flg)
        {
            string[] pslvl = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            char[] chDlr = { '$', ',', ' ', '(', ')' };
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].AllowDBNull = true;
            }
            DBNull nl = DBNull.Value;
            DataRow dr;
            foreach (GridViewRow gr in gdv_ps1.Rows)
            {
                CheckBox chkdelete = (CheckBox)gr.FindControl("chkdelete");
                Label lbl_ps1_pricelevel = (Label)gr.FindControl("lbl_ps1_pricelevel");

                if (chkdelete.Checked == false && string.IsNullOrEmpty(lbl_ps1_pricelevel.Text) == false)
                {
                    dr = dt.NewRow();


                    TextBox txt_ps1_seats = (TextBox)gr.FindControl("txt_ps1_seats");
                    TextBox txt_ps1_ticketprice = (TextBox)gr.FindControl("txt_ps1_ticketprice");
                    Label lbl_ps1_saleamount = (Label)gr.FindControl("lbl_ps1_saleamount");
                    TextBox txt_ps1_seatingdetail = (TextBox)gr.FindControl("txt_ps1_seatingdetail");
                    DropDownList drp_ps1_sunit = (DropDownList)gr.FindControl("drp_ps1_sunit");
                    TextBox txt_ps1_sdiscount = (TextBox)gr.FindControl("txt_ps1_sdiscount");
                    Label lbl_ps1_sprice = (Label)gr.FindControl("lbl_ps1_sprice");
                    DropDownList drp_ps1_gunit = (DropDownList)gr.FindControl("drp_ps1_gunit");
                    TextBox txt_ps1_gdiscount = (TextBox)gr.FindControl("txt_ps1_gdiscount");
                    Label lbl_ps1_gprice = (Label)gr.FindControl("lbl_ps1_gprice");
                    dr["ps_price_level"] = lbl_ps1_pricelevel.Text;
                    if (txt_ps1_seats.Text.Trim(chDlr) == "")
                        dr["PS_SEATS_SINGLE"] = DBNull.Value;
                    else
                        dr["PS_SEATS_SINGLE"] = txt_ps1_seats.Text.Trim(chDlr);
                    if (Convert.ToString(txt_ps1_ticketprice.Text.AutoformatDecimal()) == "")
                        dr["PS_T_PRICE_SINGLE"] = DBNull.Value;
                    else
                        dr["PS_T_PRICE_SINGLE"] =Convert.ToString(txt_ps1_ticketprice.Text.AutoformatDecimal());
                    if (lbl_ps1_saleamount.Text.Trim(chDlr) == "")
                        dr["PS_SALE_AMOUNT"] = DBNull.Value;
                    else
                        dr["PS_SALE_AMOUNT"] = lbl_ps1_saleamount.Text.Trim(chDlr);

                    dr["PS_SEAT_DETAIL_SINGLE"] = txt_ps1_seatingdetail.Text.Trim(chDlr);
                    dr["PS_DISCOUNT_UNIT_SUB"] = drp_ps1_sunit.SelectedItem.Value;
                    if (txt_ps1_sdiscount.Text.Trim(chDlr) == "")
                        dr["PS_DISCOUNT_SUB"] = DBNull.Value;
                    else
                        dr["PS_DISCOUNT_SUB"] = txt_ps1_sdiscount.Text.Trim(chDlr);
                    if (lbl_ps1_sprice.Text.Trim(chDlr) == "")
                        dr["ps_t_price_sub"] = DBNull.Value;
                    else
                        dr["ps_t_price_sub"] = lbl_ps1_sprice.Text.Trim(chDlr);
                    dr["ps_discount_unit_grp"] = drp_ps1_gunit.SelectedItem.Value;
                    if (txt_ps1_gdiscount.Text.Trim(chDlr) == "")
                        dr["ps_discount_grp"] = DBNull.Value;
                    else
                        dr["ps_discount_grp"] = txt_ps1_gdiscount.Text.Trim(chDlr);
                    if (lbl_ps1_gprice.Text.Trim(chDlr) == "")
                        dr["ps_t_price_grp"] = DBNull.Value;
                    else
                        dr["ps_t_price_grp"] = lbl_ps1_gprice.Text.Trim(chDlr);
                    dt.Rows.Add(dr);
                }
            }
            if (flg == "a")
            {
                dr = dt.NewRow();
                dr["ps_price_level"] = pslvl[gdv_ps1.Rows.Count];
                dt.Rows.Add(dr);
            }
            if (dt.Compute("sum(ps_seats_single)", "") != DBNull.Value)
            {
                lbl_seattotal.Text = dt.Compute("sum(ps_seats_single)", "").ToString();
                lbl_ps1total.Text = Convert.ToDecimal(dt.Compute("sum(ps_sale_amount)", null)).ToString("C2", new CultureInfo("en-US"));
                //lbl_ps1shows.Text = dt.DefaultView.ToTable(true, "scheduledaylist").Rows[0][0].ToString().Split(',').Length.ToString();
                lbl_ps1subtotal.Text = (Convert.ToInt16(lbl_ps1shows.Text) * Convert.ToDecimal(Regex.Replace(lbl_ps1total.Text, @"\$|\,", ""))).ToString("C2", new CultureInfo("en-US"));
            }
            else
            {
                lbl_seattotal.Text = "0";
                lbl_ps1shows.Text = "0";
                lbl_ps1subtotal.Text = "$0.00";
                lbl_ps1total.Text = "$0.00";
            }
        }
        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            int engagementid = Convert.ToInt32(Request.QueryString["engmtid"]);
            int delflg = 0;
            foreach (GridViewRow gr in gdv_ps1.Rows)
            {
                CheckBox chkdelete = (CheckBox)gr.FindControl("chkdelete");
                if (chkdelete.Checked == true)
                {
                    delflg = 1;
                }
            }

            Int32 reccount = 0;
            string ps_scale = hdnps_scale.Value;
            DataTable dt = psd.GetPriceScales(engagementid, reccount, ps_scale);
            DataTable dt1 = new DataTable();
            //if (dt.Select("PS_Scale='" + ps_scale + "'", "").Length > 0)
            //    dt1 = dt.Select("PS_Scale='" + ps_scale + "'", "").CopyToDataTable();
            //else
            dt1 = dt.Clone();
            if (delflg != 0)
            {
                filldata(dt1, "d");

                if (dt1.Rows.Count > 0)
                {
                    gdv_ps1.DataSource = dt1;
                    gdv_ps1.DataBind();
                }
                else
                {
                    AddEmptyRow(gdv_ps1, dt1);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Select record!');", true);
                // AddEmptyRow(gdv_ps1, dt1);
            }

            hdndelcount.Value = "0";
        }
        public void AddEmptyRow(GridView gv, DataTable dt1)
        {
            foreach (DataColumn cl in dt1.Columns)
            {
                cl.AllowDBNull = true;
            }
            dt1.Rows.Add(dt1.NewRow());
            gv.DataSource = dt1;
            gv.DataBind();
            int columncount = gv.Rows[0].Cells.Count;
            gv.Rows[0].Visible = false;
            //gv.Rows[0].Cells.Clear();
            //gv.Rows[0].Cells.Add(new TableCell());
            //gv.Rows[0].Cells[0].ColumnSpan = columncount;
            //gv.Rows[0].Cells[0].Text = "-----";

        }

        protected void btnpricescalehide_Click(object sender, EventArgs e)
        {
            //ButtonClickDemo(sender, e);
            EngagementPriceScales obj = new EngagementPriceScales();
            obj.FindControl("hdn_engagementid");
            string[] pslist = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" };
            ContentPlaceHolder contentid = (ContentPlaceHolder)this.Page.Master.FindControl("MainContent");
            HtmlGenericControl div = (HtmlGenericControl)contentid.FindControl("div_ps" + (Array.IndexOf(pslist, hdnps_scale.Value) + 1));
            div.Style.Add("display", "none");
            int engagementid = Convert.ToInt32(Request.QueryString["engmtid"]);
            psd.PriceScaleDelete(hdnps_scale.Value, "", engagementid);
            Int32 reccount = 0;
            string ps_scale = hdnps_scale.Value;
            DataTable dt = psd.GetPriceScales(engagementid, reccount, ps_scale);
            DataTable dt1 = new DataTable();
            dt1 = dt.Clone();
            if (dt1.Rows.Count > 0)
            {
                gdv_ps1.DataSource = dt1;
                gdv_ps1.DataBind();
            }
            else
            {
                AddEmptyRow(gdv_ps1, dt1);
            }
            hdndelcount.Value = "0";

        }

        protected void gdv_ps1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    CheckBox chkdelete = (CheckBox)e.Row.FindControl("chkdelete");
            //    chkdelete.Attributes.Add("onclick", "AddCount_" + this.ID + "(" + chkdelete.ClientID + ");");
            //}
        }

        protected void gdv_ps1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }


    }

}