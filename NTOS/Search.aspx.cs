using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MasterDataLayer;
using CommonFunction;
using NTOS.DataLayer;
using ShowDataLayer;
using VenueDataLayer;
using PresenterDataLayer;
using System.Text.RegularExpressions;
namespace NTOS
{
    public partial class Search2 : System.Web.UI.Page, ISearch
    {
        MasterData objmst = new MasterData();
        DataTable dt;
        CommonFun objcf = new CommonFun();
        public string userid = "";
        public string type = "";
        public string mode = "";
        public string hid ="";
        protected void Page_Load(object sender, EventArgs e)
        {

            type = Convert.ToString(Request.QueryString["type"]);
            userid = Convert.ToString(Session["userid"]);
            string call = Convert.ToString(Request.QueryString["call"]);
            if (Convert.ToString(Request.QueryString["title"]) == "Engagement Schedule" || Convert.ToString(Request.QueryString["title"]) == "EngagementSchedule")
                {
                        
                        //Schedule sch = objmst.GetScheduleSearchHistory(1, userid);
                        //if (sch != null)
                        //{
                        //    LoadScheduleSearch(sch);
                        //}
                    mode = "Schedule";
                    DataTable dt = objmst.GetHistoryForAll(userid, 4);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            //string st = objcf.spiltQuery(dt.Rows[0][0].ToString(), "p.presenter_id", out finlstring);
                            txtLastSearch.Text = dt.Rows[0][0].ToString();
                             hid =dt.Rows[0][1].ToString();
                        }
                    }

                    }
                    if (Convert.ToString(Request.QueryString["title"]) == "Engagement Price Scales")
                    {

                        mode = "Price";
                        DataTable dt = objmst.GetHistoryForAll(userid, 6);
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                //string st = objcf.spiltQuery(dt.Rows[0][0].ToString(), "p.presenter_id", out finlstring);
                                txtLastSearch.Text = dt.Rows[0][0].ToString();
                                hid = dt.Rows[0][1].ToString();
                            }
                        }
                        
                        //PriceScaleDetail psc = objmst.GetPriceScaleSearchHistory(2, userid);
                        //if (psc != null)
                        //{ LoadPriceScaleSearch(psc); }
                        ////LoadOperator("p", 2);

                    }
                    if (Convert.ToString(Request.QueryString["title"]) == "Engagement Box Ofice")
                    {
                        mode = "Box";
                        DataTable dt = objmst.GetHistoryForAll(userid, 8);
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                //string st = objcf.spiltQuery(dt.Rows[0][0].ToString(), "p.presenter_id", out finlstring);
                                txtLastSearch.Text = dt.Rows[0][0].ToString();
                                hid = dt.Rows[0][1].ToString();
                            }
                        }

                        //BoxOfficeDetail bd = objmst.GetBoxOfficeSearchHistory(3, userid);
                        //if (bd != null)
                        //{
                        //    LoadBoxOfficeSearch(bd);
                        //}
                    }

                    if (Convert.ToString(Request.QueryString["title"]) == "Engagement Deal")
                    {
                        //DealDetail dd = objmst.GetDealDetailSearchHistory(4, userid);
                        
                        //if (dd != null)
                        //{
                        //    LoadDealSearch(dd);
                        //}
                        mode = "Deal";
                    DataTable dt = objmst.GetHistoryForAll(userid, 5);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            //string st = objcf.spiltQuery(dt.Rows[0][0].ToString(), "p.presenter_id", out finlstring);
                            txtLastSearch.Text = dt.Rows[0][0].ToString();
                             hid =dt.Rows[0][1].ToString();
                        }
                    }

                    }
                    if (Convert.ToString(Request.QueryString["title"]) == "Engagement Discounts")
                    {
                        //Discount d = objmst.GetDiscountSearchHistory(6, userid);

                        //if (d != null)
                        //{
                        //    LoadDiscountSearch(d);
                        //}
                        mode = "Discount";
                        DataTable dt = objmst.GetHistoryForAll(userid, 9);
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                //string st = objcf.spiltQuery(dt.Rows[0][0].ToString(), "p.presenter_id", out finlstring);
                                txtLastSearch.Text = dt.Rows[0][0].ToString();
                                hid = dt.Rows[0][1].ToString();
                            }
                        }



                    }

            if (!IsPostBack)
            {

                this.Master.FindControl("imgbtnsave").Visible = false ;
                this.Master.FindControl("engsummaryDiv").Visible = false;
                this.Master.FindControl("TdSummary").Visible = false;
               
                this.Master.FindControl("lisearchlist").Visible = false;
                this.Master.FindControl("btnsearch1").Visible = true ;


                

               
                //drp_showsearch.DataSource = objmst.Getshows("0");
                //drp_showsearch.DataTextField = "show_name";
                //drp_showsearch.DataValueField = "show_id";
                //drp_showsearch.DataBind();
                //drp_showsearch.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });

                //drp_presentersearch.DataSource = objmst.GetPresenters(null);
                //drp_presentersearch.DataTextField = "presenter_name";
                //drp_presentersearch.DataValueField = "presenter_id";
                //drp_presentersearch.DataBind();
                //drp_presentersearch.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });


                //drp_venuesearch.DataSource = objmst.GetVenues(null);
                //drp_venuesearch.DataTextField = "venue_name";
                //drp_venuesearch.DataValueField = "venue_id";
                //drp_venuesearch.DataBind();
                //drp_venuesearch.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });




                //drp_metrosearch.DataSource = objmst.GetMetroCityStates(null);
                //drp_metrosearch.DataTextField = "metro_state";
                //drp_metrosearch.DataValueField = "city_id";
                //drp_metrosearch.DataBind();
                //drp_metrosearch.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });

                //DataTable dtlookup = objmst.GetLookupList("");
                //if (dtlookup.Rows.Count > 0)
                //{
                //    DataTable dtstatus = dtlookup.Select("lkup_group = 'engagementstatus'").CopyToDataTable();

                //    drp_status.DataSource = dtstatus;
                //    drp_status.DataTextField = "lkup_desc";
                //    drp_status.DataValueField = "lkup_desc";
                //    drp_status.DataBind();
                //}
                //drp_status.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
                if (Convert.ToString(Request.QueryString["engmt_id"]) != "")
                {
                    Session["search_engmt"] = Convert.ToString(Request.QueryString["engmt_id"]);
                }
                else
                {
                    Session["search_engmt"] = string.Empty;
                }


                if (Convert.ToString(Request.QueryString["title"]) == "Engagement Schedule" || Convert.ToString(Request.QueryString["title"]) == "EngagementSchedule")
                {
                    Div_eng_schedule.Visible = true;
                    
                }
                if (Convert.ToString(Request.QueryString["title"]) == "Engagement Deal")
                {
                    divDeal.Visible = true;
                    //LoadOperator("d", 25);

                }
                if (Convert.ToString(Request.QueryString["title"]) == "Engagement Price Scales")
                {
                    DivPricescale.Visible = true;
                   

                }
                if (Convert.ToString(Request.QueryString["title"]) == "Engagement Expenses")
                {
                    DivExpense.Visible = true;
                    //LoadOperator("e", 76);
                }
                if (Convert.ToString(Request.QueryString["title"]) == "Engagement Box Ofice")
                {

                    Divbox.Visible = true;
                }
                if (Convert.ToString(Request.QueryString["title"]) == "Engagement Discounts")
                {
                    DivDiscount.Visible = true;
                    //LoadOperator("di", 2);

                }

                if (Convert.ToString(Request.QueryString["title"]) == "Engagement Coversheet")
                {
                    DivCoversheet.Visible = true;
                }
                //if (this.Page.Title == "Search")
                //{
                //    Response.Redirect("Search.aspx", false);
                //}
               

            }
        }

        public void LoadOperator(string type, int count)
        {
            //ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("MainContent");
            //dt = new DataTable();
            //dt.Columns.Add("opr");
            //dt.Rows.Add("=");
            //dt.Rows.Add("<");
            //dt.Rows.Add("<=");
            //dt.Rows.Add(">");
            //dt.Rows.Add(">=");
            //DropDownList ddlopr;
            //for (int i = 1; i <= count; i++)
            //{
            //    ddlopr = (DropDownList)cph.FindControl("ddl" + type + "opr" + i.ToString());
            //    ddlopr.Width = 40;
            //    objcf.Fillddl_noselect(ddlopr, dt, "opr", "opr");
            //}
        }
        public void GetSearchEngmtDealData(bool isMaster)
        {
            int outhistid = 0;
            int histid = 0;
                InsertHistory(4);
            Session["recindex"] = "1";
            DataTable dt = new DataTable();

            string deal_royalty_income = "", deal_guarantee_income = "", deal_cmpny_mid_monies_ptg = "", deal_presenter_mid_monies_ptg = "", deal_mid_monies_cap = "",
            deal_producer_share_split_ptg = "", deal_presenter_share_split_ptg = "", deal_star_royalty_ptg = "", deal_misc_othr_amt_1 = "",
            deal_misc_othr_amt_2 = "", deal_incm_wthd_tax_bgt_amt = "", deal_incm_wthd_tax_act_amt = "", deal_tax_ptg = "",
            deal_tax_amt_over = "", deal_tax2_ptg = "", deal_sub_sales_comm = "", deal_ph_sales_comm = "", deal_web_sales_comm = "",
            deal_cc_sales_comm = "", deal_remote_sales_comm = "", deal_facility_fee_amt = "", deal_tax_ff_bo_comm = "",
            deal_single_tix_comm = "", deal_grp_sales_comm1 = "", deal_grp_sales_comm2 = "",dealtype="";

            deal_royalty_income = txt_royalty.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_royalty.Text);

            deal_cmpny_mid_monies_ptg = txt_companymonies.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_companymonies.Text);

            deal_tax_ptg = txt_tax.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_tax.Text);
            deal_facility_fee_amt = txt_facilityfee.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_facilityfee.Text);
            deal_guarantee_income = txt_guarantee.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_guarantee.Text);

            deal_presenter_mid_monies_ptg = txt_presentermonies.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_presentermonies.Text);
          
            deal_tax_ff_bo_comm = txt_boxoffice.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_boxoffice.Text);
            deal_cmpny_mid_monies_ptg = txt_middlecap.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_middlecap.Text);
            deal_tax_amt_over = txt_taxover.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_taxover.Text);
            deal_producer_share_split_ptg = txt_producershare.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_producershare.Text);
            deal_incm_wthd_tax_bgt_amt = txt_taxbudget.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_taxbudget.Text);
            deal_sub_sales_comm = txt_subscriptionsale.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_subscriptionsale.Text);

            deal_single_tix_comm = txt_singleticket.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_singleticket.Text);
            deal_misc_othr_amt_1 = txt_miscellaneous1.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_miscellaneous1.Text);
            deal_incm_wthd_tax_act_amt = txt_taxactual.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_taxactual.Text);
            deal_ph_sales_comm = txt_phonecommission.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_phonecommission.Text);

            deal_grp_sales_comm1 = txt_groupsale1.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_groupsale1.Text);
            deal_presenter_share_split_ptg = txt_presentershare2.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_presentershare2.Text);
            deal_web_sales_comm = txt_internetsale.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_internetsale.Text);
            deal_grp_sales_comm2 = txt_groupsale3.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_groupsale3.Text);
            deal_misc_othr_amt_2 = txt_miscellaneous5.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_miscellaneous5.Text);


            deal_cc_sales_comm = txt_cardsale.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_cardsale.Text);
            deal_star_royalty_ptg = txt_starroyalty1.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_starroyalty1.Text);
            deal_remote_sales_comm = txt_remotesale1.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txt_remotesale1.Text);


            int typ = Convert.ToInt16(type);
            if (typ == 2 || typ == 3 || typ == 4)
            {
                if (Session["Historyid"] != null)
                    histid = Convert.ToInt32(Session["Historyid"]);

            }
            if (isMaster == true)
                typ = 4;

              

            dt = objmst.GetSearchDataEngtSchedule(deal_royalty_income, deal_guarantee_income,
                deal_cmpny_mid_monies_ptg, deal_presenter_mid_monies_ptg, deal_mid_monies_cap,
            deal_producer_share_split_ptg, deal_presenter_share_split_ptg, deal_star_royalty_ptg, deal_misc_othr_amt_1,
            deal_misc_othr_amt_2, deal_incm_wthd_tax_bgt_amt, deal_incm_wthd_tax_act_amt, deal_tax_ptg,
            deal_tax_amt_over, deal_tax2_ptg, deal_sub_sales_comm, deal_ph_sales_comm, deal_web_sales_comm,
            deal_cc_sales_comm, deal_remote_sales_comm, deal_facility_fee_amt, deal_tax_ff_bo_comm,
            deal_single_tix_comm, deal_grp_sales_comm1, deal_grp_sales_comm2, dealtype, typ, userid, histid,out outhistid);



            if (dt.Rows.Count > 0)
            {
                Session["Historyid"] = outhistid;
                //this.Master.FindControl("navpage").Visible = true;
                this.Master.FindControl("btnlist").Visible = true;
                this.Master.FindControl("navpage").Visible = true;
                //  this.Master.FindControl("btnsearch1").Visible = false;
                this.Master.FindControl("lisearchlist").Visible = true;

                if (this.MasterPageFile == "/Site.Master")
                {
                    this.Master.FindControl("btnsearch").Visible = true;
                }
                else { this.Master.FindControl("btnsearch1").Visible = true; }

                Session["Search"] = dt;
                Response.Redirect("~/EngagementDeal.aspx?engmtid=" + dt.Rows[0]["KeyID"].ToString() + "&search=y");
            }
            else {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Records Found');", true);
            }
        }

        private void LoadDealData(DealDetail dd)
        {
          
        }

        public void GetSearchEngmtPriceData(bool isMaster)
        {
            int outhistid = 0;
            int histid = 0;
            InsertHistory(2);
            Session["recindex"] = "1";
            DataTable dt = new DataTable();
            string a1="", a2="", a3="", a4="";

           
            if (txtbxSeatFrom.Text != "")
            {
                a1 = objcf.SplitStringForSql(txtbxSeatFrom.Text);
            }
           

            if (txtbxSinglefrom.Text != "")
            {

                a2 = objcf.SplitStringForSql(txtbxSinglefrom.Text);

            }
            

            if (txttbxgroupfrom.Text != "")
            {

                a3 = objcf.SplitStringForSql(txttbxgroupfrom.Text);

            }
          

            if (txtbxsubscriptionfrom.Text != "")
            {

                a4 = objcf.SplitStringForSql(txtbxsubscriptionfrom.Text);

            }

            int typ = Convert.ToInt16(type);
            if (typ == 2 || typ == 3 || typ == 4)
            {
                if (Session["Historyid"] != null)
                    histid = Convert.ToInt32(Session["Historyid"]);

            }
            if (isMaster == true)
                typ = 4;
            dt = objmst.GetSearchDataPriceEngtSchedule(a1, a2, a3, a4,typ,userid,histid,out outhistid );
            
            if (dt.Rows.Count > 0)
            {
                Session["Historyid"] = outhistid;
                //this.Master.FindControl("navpage").Visible = true;
                this.Master.FindControl("btnlist").Visible = true;
                this.Master.FindControl("navpage").Visible = true;
                //  this.Master.FindControl("btnsearch1").Visible = false;
                this.Master.FindControl("lisearchlist").Visible = true;

                if (this.MasterPageFile == "/Site.Master")
                {
                    this.Master.FindControl("btnsearch").Visible = true;
                }
                else { this.Master.FindControl("btnsearch1").Visible = true; }
              
                Session["Search"] = dt;
                Response.Redirect("~/EngagementPriceScales.aspx?engmtid=" + dt.Rows[0]["KeyID"].ToString() + "&search=y");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Records Found');", true);
            }
        

        
        }

        public void GetSearchEngtDiscount(bool isMaster)
        {

            int outhistid = 0;
            int histid = 0;
            InsertHistory(6);
            Session["recindex"] = "1";
            DataTable dt = new DataTable();
            string subper = "", subTkt = "", grpPer = "", grpTkt = "",MisPer="",MisTkt ="";


            if (txtbxDSubscription.Text != "")
            {
                subper = objcf.SplitStringForSql(txtbxDSubscription.Text);
            }


            if (txtxsubTktto.Text != "")
            {

                subTkt = objcf.SplitStringForSql(txtxsubTktto.Text);

            }


            if (txtbxGSubFrom.Text != "")
            {

                grpPer = objcf.SplitStringForSql(txtbxGSubFrom.Text);

            }


            if (txtbxGtktFrom.Text != "")
            {

                grpTkt = objcf.SplitStringForSql(txtbxGtktFrom.Text);

            }
            if (txtbxmis.Text != "")
            {

                MisPer = objcf.SplitStringForSql(txtbxmis.Text);

            }


            if (txtbxmisTfrom.Text != "")
            {

                MisTkt = objcf.SplitStringForSql(txtbxmisTfrom.Text);

            }

            int typ = Convert.ToInt16(type);
            if (typ == 2 || typ == 3 || typ == 4)
            {
                if (Session["Historyid"] != null)
                    histid = Convert.ToInt32(Session["Historyid"]);

            }
            if (isMaster == true)
                typ = 4;
            dt = objmst.GetSearchEngtDiscount(subper, subTkt, grpPer, grpTkt, MisPer, MisTkt, typ, userid, histid,out outhistid);
            
            if (dt.Rows.Count > 0)
            {
                Session["Historyid"] = Convert.ToString(outhistid);
                //this.Master.FindControl("navpage").Visible = true;
                this.Master.FindControl("btnlist").Visible = true;
                this.Master.FindControl("navpage").Visible = true;
                //  this.Master.FindControl("btnsearch1").Visible = false;
                this.Master.FindControl("lisearchlist").Visible = true;

                if (this.MasterPageFile == "/Site.Master")
                {
                    this.Master.FindControl("btnsearch").Visible = true;
                }
                else { this.Master.FindControl("btnsearch1").Visible = true; }
               
                Session["Search"] = dt;
                Response.Redirect("~/EngagementDiscount.aspx?engmtid=" + dt.Rows[0]["KeyID"].ToString() + "&search=y");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Records Found');", true);
            }
        }

        public void GetSearchEngmtBoxOFfice(bool isMaster)
        {
            int outhistid =0;
            int histid = 0;
                InsertHistory(3);
            Session["recindex"] = "1";
            DataTable dt = new DataTable();

            string grpsales = "", drpcount = "", paidA = "", Comp = "", SubTktSale = "", SubGrossRec = "", PhTktSale = "",
                Phgross = "", InternetTktSale = "", InternetGross = "", CCTktSale = "", CCGrossRec = "", RemoteTktSale = "", RemoteGrossRec = "",
                SingleTicketsTktSale = "", SingleTktsGrossRec = "", Group1TktSale = "", Grp1GrossReceipt = "", Gr2TktSale = "", Grp2GrossRecept = "";

           

                grpsales = txtbxgrpSalesfrm.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtbxgrpSalesfrm.Text);
               drpcount = txtbxdrpcountfrom.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtbxdrpcountfrom.Text);
               paidA= txtbxPAfrom.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtbxPAfrom.Text);
               Comp = txtbxCompFrom.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtbxCompFrom.Text);
               SubTktSale = txtbxtsSubFrom.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtbxtsSubFrom.Text);
               PhTktSale = txtbxtsPhoFrom.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtbxtsPhoFrom.Text);

               InternetTktSale = txtbxtsIntFrom.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtbxtsIntFrom.Text);
               CCTktSale = txtbxtsCreFrom.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtbxtsCreFrom.Text);
               RemoteTktSale = txtbxtsRemFrom.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtbxtsRemFrom.Text);
               SingleTicketsTktSale = txtbxtsSinFrom.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtbxtsSinFrom.Text);
               Group1TktSale = txtbxtsGrp1From.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtbxtsGrp1From.Text);
               Gr2TktSale = txtbxtsGrp2From.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtbxtsGrp2From.Text);

               SingleTktsGrossRec = txtbxgrSinFrom.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtbxgrSinFrom.Text);
               Grp1GrossReceipt = txtbxgrGrp1From.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtbxgrGrp1From.Text);
               Grp2GrossRecept = txtbxgrGrp2From.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtbxgrGrp2From.Text);



               int typ = Convert.ToInt16(type);
               if (typ == 2 || typ == 3 || typ == 4)
               {
                   if (Session["Historyid"] != null)
                       histid = Convert.ToInt32(Session["Historyid"]);

                  }
               if (isMaster == true)
                   typ = 4;
            dt = objmst.GetSearchDataBoxOfficeEngtSchedule(grpsales, drpcount, paidA, Comp, SubTktSale, SubGrossRec, PhTktSale, Phgross, InternetTktSale, InternetGross, CCTktSale, CCGrossRec, RemoteTktSale, RemoteGrossRec, SingleTicketsTktSale, SingleTktsGrossRec, Group1TktSale, Grp1GrossReceipt, Gr2TktSale, Grp2GrossRecept, typ, userid, histid,out outhistid);
          

            if (dt.Rows.Count > 0)
            {
                Session["Historyid"] = Convert.ToString(outhistid);
                //this.Master.FindControl("navpage").Visible = true;
                this.Master.FindControl("btnlist").Visible = true;
                this.Master.FindControl("navpage").Visible = true;
                //  this.Master.FindControl("btnsearch1").Visible = false;
                this.Master.FindControl("lisearchlist").Visible = true;

                if (this.MasterPageFile == "/Site.Master")
                {
                    this.Master.FindControl("btnsearch").Visible = true;
                }
                else { this.Master.FindControl("btnsearch1").Visible = true; }
               
                Session["Search"] = dt;
                Response.Redirect("~/EngagementBoxOffice.aspx?engmtid=" + dt.Rows[0]["KeyID"].ToString() + "&search=y");


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Records Found');", true);
            }





        }
                   
        public void GetSearchData(bool isMaster)
        {
            int outhistid=0;
            int histid=0;
            InsertHistory(1);
            Session["recindex"] = "1";
            DataTable dt = new DataTable();
            string a1, a2, a3, a4, a5,a6,a7,a8,a9,a10;
            a1 = (string.IsNullOrEmpty(txt_showsearch.Text.Trim()) ==true) ? string.Empty : objcf.SplitStringForComma(txt_showsearch.Text.Trim());
            a2 = (string.IsNullOrEmpty(txtcreatedatesearch.Text) == true) ? string.Empty : objcf.SplitstringForDates(objcf.SplitStringForSql(objcf.SplitForDates(txtcreatedatesearch.Text)));
            a3 = (string.IsNullOrEmpty(txt_presentersearch.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txt_presentersearch.Text.Trim());
            a4 = (string.IsNullOrEmpty(txt_venuesearch.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txt_venuesearch.Text.Trim());
            a5 = (string.IsNullOrEmpty(txt_metrosearch.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txt_metrosearch.Text.Trim());
            a6 = (string.IsNullOrEmpty(txt_status.Text.Trim()) == true) ? string.Empty : txt_status.Text.Trim();
            a7 = drp_subscription.SelectedValue;
            a8 = drp_repeat.SelectedValue;
            a9 = (string.IsNullOrEmpty(txt_subscription.Text.Trim()) == true) ? string.Empty :objcf.SplitStringForSql( txt_subscription.Text.Trim());
            a10 = (string.IsNullOrEmpty(txtState.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txtState.Text.Trim());
            int typ = Convert.ToInt16(type);
            //if (typ == 4)
            //    typ = 1;  
            if (typ == 2 || typ == 3 || typ == 4)
            {
                if (Session["Historyid"] != null)
                histid = Convert.ToInt32(Session["Historyid"]);
            }
            if (isMaster == true)
                typ = 4;
            dt = objmst.GetSearchDataNew("Engagement", typ, userid, a1, out outhistid,histid, a2, a3, a4, a5,a6,a7,a8,a9,a10);
            
            if (dt.Rows.Count > 0)
            {
                Session["Historyid"] = Convert.ToString(outhistid);
                //this.Master.FindControl("navpage").Visible = true;
                this.Master.FindControl("btnlist").Visible = true;
                this.Master.FindControl("navpage").Visible = true;
              //  this.Master.FindControl("btnsearch1").Visible = false;
                this.Master.FindControl("lisearchlist").Visible = true;

                if (this.MasterPageFile == "/Site.Master")
                {
                    this.Master.FindControl("btnsearch").Visible = true;
                }
                else { this.Master.FindControl("btnsearch1").Visible = true; }
                
                Session["Search"] = dt;
                Response.Redirect("~/EngagementSchedule.aspx?engmtid=" + dt.Rows[0]["KeyID"].ToString() + "&search=y");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Records Found');", true);
            }
        }

        private void LoadScheduleData()
        {
            //txt_showsearch.Text =;
            //txtcreatedatesearch.Text=;

 
        }

       


        public void GetSearchEngmtExpensesData(bool isMaster)
        {
            int outhistid = 0;
            int histid = 0;
            Session["recindex"] = "1";
            DataTable dt = new DataTable();

            string  exp_d_ad_gross_bgt = "", exp_d_ad_gross_act = "", exp_d_labor_catering_bgt = "", exp_d_labor_catering_act = "", exp_d_musician_bgt = "",
                    exp_d_musician_act = "", exp_d_stghand_loadin_bgt = "", exp_d_stghand_loadin_act = "", exp_d_stghand_loadout_bgt = "", exp_d_stghand_loadout_act = "", exp_d_stghand_running_bgt = "",
                    exp_d_stghand_running_act = "", exp_d_wardrobe_loadin_bgt = "", exp_d_wardrobe_loadin_act = "", exp_d_wardrobe_loadout_bgt = "", exp_d_wardrobe_loadout_act = "",
                    exp_d_wardrobe_running_bgt = "", exp_d_wardrobe_running_act = "", exp_d_insurance_per_unit, exp_d_insurance_bgt = "",
                    exp_d_insurance_act = "", exp_d_ticket_print_per_unit = "", exp_d_ticket_print_bgt = "", exp_d_ticket_print_act, exp_d_other_1_bgt = "", exp_d_other_1_act = "",
                    exp_d_other_1_desc = "", exp_d_other_2_bgt = "", exp_d_other_2_act = "", exp_d_other_2_desc = "", exp_l_ada_expense_bgt = "", exp_l_ada_expense_act = "",
                    exp_l_bo_bgt = "", exp_l_bo_act = "", exp_l_catering_bgt = "", exp_l_catering_act = "", exp_l_equip_rental_bgt = "",
                    exp_l_equip_rental_act = "", exp_l_grp_sales_bgt = "", exp_l_grp_sales_act = "", exp_l_house_staff_bgt = "", exp_l_house_staff_act = "",
                    exp_l_league_fee_bgt = "", exp_l_league_fee_act = "", exp_l_license_bgt = "", exp_l_license_act = "", exp_l_limo_bgt = "",
                    exp_l_limo_act = "", exp_l_orchestra_sh_remove_bgt = "", exp_l_orchestra_sh_remove_act = "", exp_l_presenter_profit_bgt = "", exp_l_presenter_profit_act = "", exp_l_police_bgt = "",
                    exp_l_police_act = "", exp_l_program_bgt = "", exp_l_program_act = "", exp_l_rent_btg = "", exp_l_rent_act = "", exp_l_sound_bgt = "",
                    exp_l_sound_act = "", exp_l_ticket_print_bgt = "", exp_l_ticket_print_act = "", exp_l_phone_bgt = "", exp_l_phone_act = "",
                    exp_l_dryice_bgt = "", exp_l_dryice_act = "", exp_l_other1_desc = "", exp_l_other1_bgt = "", exp_l_other1_act = "", exp_l_other2_desc = "", exp_l_other2_bgt = "",
                    exp_l_other2_act = "", exp_l_other3_desc = "", exp_l_other3_bgt = "", exp_l_other3_act = "", exp_l_other4_desc = "", exp_l_other4_bgt = "", exp_l_other4_act = "",
                    exp_l_other5_desc = "", exp_l_other5_bgt = "", exp_l_other5_act = "", exp_l_local_fixed_bgt = "", exp_l_local_fixed_act = "", exp_type = "";




            exp_d_ad_gross_bgt = txtadvt_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtadvt_bud.Text);
            exp_d_ad_gross_act = txtwardrobehairrun_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtwardrobehairrun_bud.Text);
            exp_d_labor_catering_bgt = txtlabourcatering_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtlabourcatering_bud.Text);
            exp_d_labor_catering_act = txtlabourcatering_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtlabourcatering_act.Text);
            exp_d_musician_bgt = txtmusicians_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtmusicians_bud.Text);
            exp_d_musician_act = txtmusicians_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtmusicians_act.Text);
            exp_d_stghand_loadin_bgt = txtstatehandin_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtstatehandin_bud.Text);
            exp_d_stghand_loadin_act = txtstatehandin_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtstatehandin_act.Text);
            exp_d_stghand_loadout_bgt = txtstatehandout_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtstatehandout_bud.Text);
            exp_d_stghand_loadout_act = txtstatehandout_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtstatehandout_act.Text);
            exp_d_stghand_running_bgt = txtstatehandsrun_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtstatehandsrun_bud.Text);
            exp_d_stghand_running_act = txtstatehandsrun_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtstatehandsrun_act.Text);
            exp_d_wardrobe_loadin_bgt = txtwardrobehairin_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtwardrobehairin_bud.Text);
            exp_d_wardrobe_loadin_act = txtwardrobehairin_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtwardrobehairin_act.Text);
            exp_d_wardrobe_loadout_bgt = txtwardrobehairout_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtwardrobehairout_bud.Text);
            exp_d_wardrobe_loadout_act = txtwardrobehairout_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtwardrobehairout_act.Text);
            exp_d_wardrobe_running_bgt = txtwardrobehairrun_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtwardrobehairrun_bud.Text);
            exp_d_wardrobe_running_act = txtwardrobehairrun_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtwardrobehairrun_act.Text);
            exp_d_insurance_per_unit = txtinsurnace_de_unit.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtinsurnace_de_unit.Text);
            exp_d_insurance_bgt = txtinsurance_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtinsurance_bud.Text);

            exp_d_insurance_act = txtinsurance_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtinsurance_act.Text);
            exp_d_ticket_print_per_unit = txtticketprint_de_unit.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtticketprint_de_unit.Text);
            exp_d_ticket_print_bgt = txtticketprint_de_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtticketprint_de_bud.Text);
            exp_d_ticket_print_act = txtticketprint_de_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtticketprint_de_act.Text);
            exp_d_other_1_bgt = txtother1_de_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtother1_de_bud.Text);
            exp_d_other_1_act = txtother1_de_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtother1_de_act.Text);

            exp_d_other_2_bgt = txtother2_de_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtother2_de_bud.Text);
            exp_d_other_2_act = txtother2_de_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtother2_de_act.Text);

            exp_l_ada_expense_bgt = txtadaexp_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtadaexp_bud.Text);
            exp_l_ada_expense_act = txtadaexp_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtadaexp_act.Text);
            exp_l_bo_bgt = txtboxoff_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtboxoff_bud.Text);
            exp_l_bo_act = txtboxoff_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtboxoff_act.Text);
            exp_l_catering_bgt = txtcatering_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtcatering_bud.Text);
            exp_l_catering_act = txtcatering_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtcatering_act.Text);
            exp_l_equip_rental_bgt = txteqiprental_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txteqiprental_bud.Text);
            exp_l_equip_rental_act = txteqiprental_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txteqiprental_act.Text);
            exp_l_grp_sales_bgt = txtgrpsaleexp_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtgrpsaleexp_bud.Text);
            exp_l_grp_sales_act = txtgrpsaleexp_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtgrpsaleexp_act.Text);
            exp_l_house_staff_bgt = txthousestaff_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txthousestaff_bud.Text);
            exp_l_house_staff_act = txthousestaff_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txthousestaff_act.Text);
            exp_l_league_fee_bgt = txtleaguefee_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtleaguefee_bud.Text);
            exp_l_league_fee_act = txtleaguefee_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtleaguefee_act.Text);
            exp_l_license_bgt = txtlicpermits_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtlicpermits_bud.Text);
            exp_l_license_act = txtlicpermits_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtlicpermits_act.Text);
            exp_l_limo_bgt = txtlimosauto_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtlimosauto_bud.Text);
            exp_l_limo_act = txtlimosauto_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtlimosauto_act.Text);
            exp_l_orchestra_sh_remove_bgt = txtorchestrashellrml_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtorchestrashellrml_bud.Text);
            exp_l_orchestra_sh_remove_act = txtorchestrashellrml_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtorchestrashellrml_act.Text);
            exp_l_presenter_profit_bgt = txtpresenterprofit_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtpresenterprofit_bud.Text);
            exp_l_presenter_profit_act = txtpresenterprofit_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtpresenterprofit_act.Text);
            exp_l_police_bgt = txtpol_sec_fire_mar_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtpol_sec_fire_mar_bud.Text);
            exp_l_police_act = txtpol_sec_fire_mar_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtpol_sec_fire_mar_act.Text);
            exp_l_program_bgt = txtprogram_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtprogram_bud.Text);
            exp_l_program_act = txtprogram_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtprogram_act.Text);
            exp_l_rent_btg = txtrent_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtrent_bud.Text);
            exp_l_rent_act = txtrent_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtrent_act.Text);
            exp_l_sound_bgt = txtsoundlignt_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtsoundlignt_bud.Text);
            exp_l_sound_act = txtsoundlignt_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtsoundlignt_act.Text);
            exp_l_ticket_print_bgt = txtticketprint_le_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtticketprint_le_bud.Text);
            exp_l_ticket_print_act = txtticketprint_le_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtticketprint_le_act.Text);
            exp_l_phone_bgt = txttel_internet_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txttel_internet_bud.Text);
            exp_l_phone_act = txttel_internet_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txttel_internet_act.Text);
            exp_l_dryice_bgt = txtdryice_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtdryice_bud.Text);
            exp_l_dryice_act = txtdryice_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtdryice_act.Text);
            // exp_l_other1_desc=txtlicpermits_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtlicpermits_act.Text);
            exp_l_other1_bgt = txtother1_le_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtother1_le_bud.Text);
            exp_l_other1_act = txtother1_le_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtother1_le_act.Text);
            // exp_l_other2_desc=txtlicpermits_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtlicpermits_act.Text);
            exp_l_other2_bgt = txtother2_le_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtother2_le_bud.Text);
            exp_l_other2_act = txtother2_le_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtother2_le_act.Text);
            // exp_l_other3_desc=txtlicpermits_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtlicpermits_act.Text);
            exp_l_other3_bgt = txtother3_le_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtother3_le_bud.Text);
            exp_l_other3_act = txtother3_le_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtother3_le_act.Text);
            // exp_l_other4_desc=txtlicpermits_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtlicpermits_act.Text);
            exp_l_other4_bgt = txtother4_le_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtother4_le_bud.Text);
            exp_l_other4_act = txtother4_le_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtother4_le_act.Text);
            // exp_l_other5_desc=txtlicpermits_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtlicpermits_act.Text);
            exp_l_other5_bgt = txtother5_le_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtother5_le_bud.Text);
            exp_l_other5_act = txtother5_le_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtother5_le_act.Text);
            exp_l_local_fixed_bgt = txtlocalfixed_bud.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtlocalfixed_bud.Text);
            exp_l_local_fixed_act = txtlocalfixed_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtlocalfixed_act.Text);
            //exp_type = txtlicpermits_act.Text == string.Empty ? string.Empty : objcf.SplitStringForSql(txtlicpermits_act.Text);


            int typ = Convert.ToInt16(type);
            //if (typ == 4)
            //    typ = 1;  
            if (typ == 2 || typ == 3 || typ == 4)
            {
                if (Session["Historyid"] != null)
                    histid = Convert.ToInt32(Session["Historyid"]);
            }
            if (isMaster == true)
                typ = 4;



                dt = objmst.GetSearchDataExpenseEngtSchedule(exp_d_ad_gross_bgt ,exp_d_ad_gross_act ,
                    exp_d_labor_catering_bgt ,exp_d_labor_catering_act ,exp_d_musician_bgt ,exp_d_musician_act ,
                    exp_d_stghand_loadin_bgt ,exp_d_stghand_loadin_act ,exp_d_stghand_loadout_bgt ,
                    exp_d_stghand_loadout_act ,exp_d_stghand_running_bgt ,exp_d_stghand_running_act ,
                    exp_d_wardrobe_loadin_bgt ,exp_d_wardrobe_loadin_act ,exp_d_wardrobe_loadout_bgt ,
                    exp_d_wardrobe_loadout_act ,exp_d_wardrobe_running_bgt ,exp_d_wardrobe_running_act ,
                    exp_d_insurance_per_unit ,exp_d_insurance_bgt ,exp_d_insurance_act ,exp_d_ticket_print_per_unit ,
                    exp_d_ticket_print_bgt ,exp_d_ticket_print_act ,exp_d_other_1_bgt ,exp_d_other_1_act ,
                    exp_d_other_2_bgt ,exp_d_other_2_act ,exp_l_ada_expense_bgt ,exp_l_ada_expense_act ,
                    exp_l_bo_bgt ,exp_l_bo_act ,exp_l_catering_bgt ,exp_l_catering_act ,
                    exp_l_equip_rental_bgt ,exp_l_equip_rental_act ,exp_l_grp_sales_bgt ,exp_l_grp_sales_act ,
                    exp_l_house_staff_bgt ,exp_l_house_staff_act ,exp_l_league_fee_bgt ,exp_l_league_fee_act ,
                    exp_l_license_bgt ,exp_l_license_act ,exp_l_limo_bgt ,exp_l_limo_act ,exp_l_orchestra_sh_remove_bgt ,
                    exp_l_orchestra_sh_remove_act ,exp_l_presenter_profit_bgt ,exp_l_presenter_profit_act ,
                    exp_l_police_bgt ,exp_l_police_act ,exp_l_program_bgt ,exp_l_program_act ,exp_l_rent_btg ,
                    exp_l_rent_act ,exp_l_sound_bgt ,exp_l_sound_act ,exp_l_ticket_print_bgt ,exp_l_ticket_print_act ,
                    exp_l_phone_bgt ,exp_l_phone_act ,exp_l_dryice_bgt ,exp_l_dryice_act ,exp_l_other1_desc ,
                    exp_l_other1_bgt ,exp_l_other1_act ,exp_l_other2_desc ,exp_l_other2_bgt ,exp_l_other2_act ,
                    exp_l_other3_desc ,exp_l_other3_bgt ,exp_l_other3_act ,exp_l_other4_desc ,exp_l_other4_bgt ,exp_l_other4_act ,exp_l_other5_desc ,
                    exp_l_other5_bgt ,exp_l_other5_act ,exp_l_local_fixed_bgt ,exp_l_local_fixed_act ,exp_type,typ,userid,histid,out outhistid);

            if (dt.Rows.Count > 0)
            {
                Session["Historyid"] = Convert.ToString(outhistid);
                //this.Master.FindControl("navpage").Visible = true;
                this.Master.FindControl("btnlist").Visible = true;
                this.Master.FindControl("navpage").Visible = true;
                //  this.Master.FindControl("btnsearch1").Visible = false;
                this.Master.FindControl("lisearchlist").Visible = true;

                if (this.MasterPageFile == "/Site.Master")
                {
                    this.Master.FindControl("btnsearch").Visible = true;
                }
                else { this.Master.FindControl("btnsearch1").Visible = true; }
                if (type == "3")
                {
                    DataTable dt1 = new DataTable();
                    dt1 = (DataTable)Session["searchAll"];
                    dt.Merge(dt1, false, MissingSchemaAction.Add);
                }
                Session["Search"] = dt;
                Response.Redirect("~/EngagementExpenses.aspx?engmtid=" + dt.Rows[0]["KeyID"].ToString() + "&search=y");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Records Found');", true);
            }
        }


        private void LoadExpensedata()
        {
            //txtadvt_bud.Text;
        }
        
        private void LoadScheduleSearch(Schedule sch )
        {
            txt_showsearch.Text = sch.Show;
            txt_presentersearch.Text = sch.Presenter;
            txt_metrosearch.Text = sch.City;
            txt_venuesearch.Text = sch.Venue;
            txtcreatedatesearch.Text = sch.SettlementDate;
            txt_status.Text = sch.Status;
        }


        private void LoadExpenseData(ExpenseDetails ed)
        {
                txtadvt_bud.Text=ed.BUDAdvertisingGross;
                txtadvt_act.Text=ed.ACTAdvertisingGross;
                txtadaexp_bud.Text=ed.BUDADAExpense;
                txtadaexp_act.Text=ed.ACTADAExpense;
                txtlabourcatering_bud.Text=ed.BUDLaborCatering;
                txtlabourcatering_act.Text=ed.ACTLaborCatering;
                txtboxoff_bud.Text=ed.BUDBoxOffice;
                txtboxoff_act.Text=ed.ACTBoxOffice;
                txtmusicians_bud.Text=ed.BUDMusicians;
                txtmusicians_act.Text=ed.ACTMusicians;
                txtcatering_bud.Text=ed.BUDCatering;
                txtcatering_act.Text=ed.ACTCatering;
                txteqiprental_bud.Text=ed.BUDEquipmentRental;
                txteqiprental_act.Text=ed.ACTEquipmentRental;
                txtgrpsaleexp_bud.Text=ed.BUDGroupSalesExpenses;
                txtgrpsaleexp_act.Text=ed.ACTGroupSalesExpenses;
                txtstatehandin_bud.Text=ed.BUDshLoadIn;
                txtstatehandin_act.Text=ed.ACTshLoadIn;
                txthousestaff_bud.Text=ed.BUDHouseStaff;
                txthousestaff_act.Text=ed.ACTHouseStaff;
                txtstatehandout_bud.Text=ed.BUDshLoadOut;
                txtstatehandout_act.Text=ed.ACTshLoadOut;
                txtleaguefee_bud.Text=ed.BUDLeagueFees;
                txtleaguefee_act.Text=ed.ACTLeagueFees;
                txtstatehandsrun_bud.Text=ed.BUDshRunning;
                txtstatehandsrun_act.Text=ed.ACTshRunning;
                txtlicpermits_bud.Text=ed.BUDLicensesPermits;
                txtlicpermits_act.Text=ed.ACTLicensesPermits;
                txtlimosauto_bud.Text=ed.BUDLimosAuto;
                txtlimosauto_act.Text=ed.ACTLimosAuto;
                txtorchestrashellrml_bud.Text=ed.BUDOrchestraShellRemoval;
                txtorchestrashellrml_act.Text=ed.ACTOrchestraShellRemoval;

                txtwardrobehairin_bud.Text=ed.BUDwrLoadIn;
                txtwardrobehairin_act.Text=ed.ACTwrLoadIn;
                txtpresenterprofit_bud.Text=ed.ACTPresenterProfit;
                txtpresenterprofit_act.Text=ed.BUDPresenterProfit;
                txtwardrobehairout_bud.Text=ed.BUDwrLoadOut;
                txtwardrobehairout_act.Text=ed.ACTwrLoadOut;
                txtpol_sec_fire_mar_bud.Text=ed.BUDPoliceSecurityFireMarshall;
                txtpol_sec_fire_mar_act.Text=ed.ACTPoliceSecurityFireMarshall;
                txtwardrobehairrun_bud.Text=ed.BUDwrRunning;
                txtwardrobehairrun_act.Text=ed.ACTwrRunning;
                txtprogram_bud.Text=ed.BUDProgram;
                txtprogram_act.Text=ed.ACTProgram;
                txtrent_bud.Text=ed.BUDRent;
                txtrent_act.Text=ed.ACTRent;
                //txtinsurnace_de_unit.text=ed.;
                //txtinsurance_bud.text=ed.budin;
                //txtinsurance_act.text=;
                txtsoundlignt_bud.Text=ed.BUDSoundLights;
                txtsoundlignt_act.Text=ed.ACTSoundLights;
                //txtticketprint_de_unit.text=ed.act;
                //txtticketprint_de_bud.text=;
                //txtticketprint_de_act.text=;
                //txtticketprint_le_bud.text=;
               // txtticketprint_le_act.text=;
                txttel_internet_bud.Text=ed.BUDTelephonesInternet;
                txttel_internet_act.Text=ed.ACTTelephonesInternet;
                txtdryice_bud.Text=ed.BUDDryIceC02;
                txtdryice_act.Text=ed.ACTDryIceC02;
               // txtother1_de_bud.t;
                //txtother1_de_act.text=;
                //txtother2_de_bud.text=;
                //txtother2_de_act.text=;
               // txtother1_le_bud.text=;
                //txtother1_le_act.text=;
                //txtother2_le_bud.text=;
                //t//xtother2_le_act.text=;
                //txtother3_le_bud.text=;
                //txtother3_le_act.text=;
               // txtother4_le_bud.text=;
               // txtother4_le_act.text=;
               // txtother5_le_bud.text=;
                //txtother5_le_act.text=;
                txtlocalfixed_bud.Text=ed.BUDLocalFixed;
                txtlocalfixed_act.Text=ed.ACTLocalFixed;
        }

        private void InsertHistory(int type)
        {
            if (type == 1)
            {
                Schedule sch = new Schedule();
                sch.Show = txt_showsearch.Text.Trim().ToString();;
                sch.SettlementDate = txtcreatedatesearch.Text.Trim().ToString();;
                sch.Presenter = txt_presentersearch.Text.Trim().ToString();;
                sch.Status = txt_status.Text.Trim().ToString();;
                sch.Venue = txt_venuesearch.Text.Trim().ToString();;
                sch.City = txt_metrosearch.Text.Trim().ToString();;

                objmst.SearchHistoryUpdate(userid, sch,null,null,null,null, type);
            }
            else if (type == 2)
            {
                PriceScaleDetail psc = new PriceScaleDetail();
                psc.Seats = txtbxSeatFrom.Text.Trim().ToString();;
                psc.Single = txtbxSinglefrom.Text.Trim().ToString();;
                psc.Group = txttbxgroupfrom.Text.Trim().ToString();;
                psc.Subscription = txtbxsubscriptionfrom.Text.Trim().ToString();;
                objmst.SearchHistoryUpdate(userid, null, psc,null,null,null, type);
            }
            else if (type == 3)
           {
                BoxOfficeDetail bd = new BoxOfficeDetail();
                bd.GroupSales=txtbxgrpSalesfrm.Text.Trim().ToString();

                bd.DropCount = txtbxdrpcountfrom.Text.Trim().ToString();
                bd.PaidAttendance = txtbxPAfrom.Text.Trim().ToString();
                bd.Comp = txtbxCompFrom.Text.Trim().ToString();
                bd.TSSubscription = txtbxtsSubFrom.Text.Trim().ToString();
                bd.GRSubscription = txtbxgrSubFrom.Text.Trim().ToString();
                bd.TSPhone = txtbxtsPhoFrom.Text.Trim().ToString();
                bd.GRPhone=txtbxgrPhoFrom.Text.Trim().ToString();
                bd.TSInternet=txtbxtsIntFrom.Text.Trim().ToString();
                bd.GRInternet=txtbxgrIntFrom.Text.Trim().ToString();
                bd.TSCreditCard=txtbxtsCreFrom.Text.Trim().ToString();
                bd.GRCreditCard=txtbxgrCreFrom.Text.Trim().ToString();
                bd.TSRemoteOutlet=txtbxtsRemFrom.Text.Trim().ToString();
                bd.GRRemoteOutlet=txtbxgrRemFrom.Text.Trim().ToString();
                bd.TSSingleTickets=txtbxtsSinFrom.Text.Trim().ToString();
                bd.GRSingleTickets=txtbxgrSinFrom.Text.Trim().ToString();
                bd.GRGroup1=txtbxgrGrp1From.Text.Trim().ToString();
                bd.GRGroup2=txtbxgrGrp2From.Text.Trim().ToString();
                bd.TSGroup1 = txtbxtsGrp1From.Text.Trim().ToString();
                bd.TSGroup2 = txtbxtsGrp2From.Text.Trim().ToString();
                objmst.SearchHistoryUpdate(userid, null, null, bd, null,null, type);

           }
            else if (type == 4)
            {

                DealDetail dd = new DealDetail();
                  dd.Royalty  =  txt_royalty.Text.Trim().ToString();
                  dd.Company  =txt_companymonies.Text.Trim().ToString();
                  dd.Tax1  =txt_tax.Text.Trim().ToString();
                  dd.TaxFacilityFee  =txt_facilityfee.Text.Trim().ToString();
                  dd.Guarantee  =txt_guarantee.Text.Trim().ToString();
                  dd.Presenter  =txt_presentermonies.Text.Trim().ToString();
                  dd.TaxFacilityFee  =txt_boxoffice.Text.Trim().ToString();
                  dd.Cap  =txt_middlecap.Text.Trim().ToString();
                  dd.TaxAmountOver  =txt_taxover.Text.Trim().ToString();
                  dd.Producer  =txt_producershare.Text.Trim().ToString();
                  dd.Budget  =txt_taxbudget.Text.Trim().ToString();
                  dd.Subscription  =txt_subscriptionsale.Text.Trim().ToString();
                  dd.SingleTickets  =txt_singleticket.Text.Trim().ToString();
                  dd.Other1  =txt_miscellaneous1.Text.Trim().ToString();
                  dd.Actual  =txt_taxactual.Text.Trim().ToString();
                  dd.Phone  =txt_phonecommission.Text.Trim().ToString();
                  dd.Group1  =txt_groupsale1.Text.Trim().ToString();
                  dd.Presenter =txt_presentershare2.Text.Trim().ToString();
                  dd.Internet  =txt_internetsale.Text.Trim().ToString();
                  dd.Group2  =txt_groupsale3.Text.Trim().ToString();
                  dd.Other2  =txt_miscellaneous5.Text.Trim().ToString();
                  dd.CreditCard  =txt_cardsale.Text.Trim().ToString();
                  dd.StarRoyalty  =txt_starroyalty1.Text.Trim().ToString();
                  dd.Remote = txt_remotesale1.Text.Trim().ToString();
                  objmst.SearchHistoryUpdate(userid,null,null,null,dd,null,type);

            }
            else if (type == 5)
            {
             ExpenseDetails ed = new ExpenseDetails();
            ed.BUDAdvertisingGross    =txtadvt_bud.Text.Trim().ToString();
            ed.ACTAdvertisingGross=txtadvt_act.Text.Trim().ToString();
            ed.BUDADAExpense=txtadaexp_bud.Text.Trim().ToString();
            ed.ACTADAExpense=txtadaexp_act.Text.Trim().ToString();
            ed.BUDLaborCatering=txtlabourcatering_bud.Text.Trim().ToString();
            ed.ACTLaborCatering=txtlabourcatering_act.Text.Trim().ToString();
            ed.BUDBoxOffice=txtboxoff_bud.Text.Trim().ToString();
            ed.ACTBoxOffice=txtboxoff_act.Text.Trim().ToString();
            ed.BUDMusicians=txtmusicians_bud.Text.Trim().ToString();
            ed.ACTMusicians=txtmusicians_act.Text.Trim().ToString();
            ed.BUDCatering=txtcatering_bud.Text.Trim().ToString();
            ed.ACTCatering=txtcatering_act.Text.Trim().ToString();
            ed.BUDEquipmentRental=txteqiprental_bud.Text.Trim().ToString();
            ed.ACTEquipmentRental=txteqiprental_act.Text.Trim().ToString();
            ed.BUDGroupSalesExpenses=txtgrpsaleexp_bud.Text.Trim().ToString();
            ed.ACTGroupSalesExpenses=txtgrpsaleexp_act.Text.Trim().ToString();
            ed.BUDshLoadIn=txtstatehandin_bud.Text.Trim().ToString();
            ed.ACTshLoadIn=txtstatehandin_act.Text.Trim().ToString();
            ed.BUDHouseStaff=txthousestaff_bud.Text.Trim().ToString();
            ed.ACTHouseStaff=txthousestaff_act.Text.Trim().ToString();
            ed.BUDshLoadOut=txtstatehandout_bud.Text.Trim().ToString();
            ed.ACTshLoadOut=txtstatehandout_act.Text.Trim().ToString();
            ed.BUDLeagueFees=txtleaguefee_bud.Text.Trim().ToString();
            ed.ACTLeagueFees=txtleaguefee_act.Text.Trim().ToString();
            ed.BUDshRunning=txtstatehandsrun_bud.Text.Trim().ToString();
            ed.ACTshRunning=txtstatehandsrun_act.Text.Trim().ToString();
            ed.BUDLicensesPermits=txtlicpermits_bud.Text.Trim().ToString();
            ed.ACTLicensesPermits=txtlicpermits_act.Text.Trim().ToString();
            ed.BUDLimosAuto=txtlimosauto_bud.Text.Trim().ToString();
            ed.ACTLimosAuto=txtlimosauto_act.Text.Trim().ToString();
            ed.BUDOrchestraShellRemoval=txtorchestrashellrml_bud.Text.Trim().ToString();
            ed.ACTOrchestraShellRemoval=txtorchestrashellrml_act.Text.Trim().ToString();
            ed.BUDwrLoadIn=txtwardrobehairin_bud.Text.Trim().ToString();
            ed.ACTwrLoadOut=txtwardrobehairin_act.Text.Trim().ToString();
            ed.BUDPresenterProfit=txtpresenterprofit_bud.Text.Trim().ToString();
            ed.ACTPresenterProfit=txtpresenterprofit_act.Text.Trim().ToString();
            ed.BUDwrLoadOut=txtwardrobehairout_bud.Text.Trim().ToString();
            ed.ACTwrLoadOut=txtwardrobehairout_act.Text.Trim().ToString();
            ed.BUDPoliceSecurityFireMarshall=txtpol_sec_fire_mar_bud.Text.Trim().ToString();
            ed.ACTPoliceSecurityFireMarshall=txtpol_sec_fire_mar_act.Text.Trim().ToString();
            ed.BUDwrRunning=txtwardrobehairrun_bud.Text.Trim().ToString();
            ed.ACTwrRunning=txtwardrobehairrun_act.Text.Trim().ToString();
            ed.BUDProgram=txtprogram_bud.Text.Trim().ToString();
            ed.ACTProgram=txtprogram_act.Text.Trim().ToString();
            ed.BUDRent=txtrent_bud.Text.Trim().ToString();
            ed.ACTRent=txtrent_act.Text.Trim().ToString();
            //=txtinsurnace_de_unit.Text.Trim().ToString();
            //=txtinsurance_bud.Text.Trim().ToString();
           //=txtinsurance_act.Text.Trim().ToString();
             ed.BUDSoundLights=txtsoundlignt_bud.Text.Trim().ToString();
            ed.ACTSoundLights=txtsoundlignt_act.Text.Trim().ToString();
           // =txtticketprint_de_unit.Text.Trim().ToString();
           // =txtticketprint_de_bud.Text.Trim().ToString();
           // =txtticketprint_de_act.Text.Trim().ToString();
            //=txtticketprint_le_bud.Text.Trim().ToString();
            //=txtticketprint_le_act.Text.Trim().ToString();
            ed.BUDTelephonesInternet=txttel_internet_bud.Text.Trim().ToString();
            ed.ACTTelephonesInternet=txttel_internet_act.Text.Trim().ToString();
            ed.BUDDryIceC02=txtdryice_bud.Text.Trim().ToString();
            ed.ACTDryIceC02=txtdryice_act.Text.Trim().ToString();
            //=txtother1_de_bud.Text.Trim().ToString();
            //=txtother1_de_act.Text.Trim().ToString();
            //=txtother2_de_bud.Text.Trim().ToString();
            //=txtother2_de_act.Text.Trim().ToString();
            //=txtother1_le_bud.Text.Trim().ToString();
            //=txtother1_le_act.Text.Trim().ToString();
            //=txtother2_le_bud.Text.Trim().ToString();
            //=txtother2_le_act.Text.Trim().ToString();
            //=txtother3_le_bud.Text.Trim().ToString();
            //=txtother3_le_act.Text.Trim().ToString();
            //=txtother4_le_bud.Text.Trim().ToString();
            //=txtother4_le_act.Text.Trim().ToString();
            //=txtother5_le_bud.Text.Trim().ToString();
            //=txtother5_le_act.Text.Trim().ToString();
            ed.BUDLocalFixed=txtlocalfixed_bud.Text.Trim().ToString();
            ed.ACTLocalFixed=txtlocalfixed_act.Text.Trim().ToString();
            }
            else if (type == 6)
            {
                Discount d = new Discount();
                d.Sub = txtbxDSubscription.Text.Trim().ToString();
                d.SubTkt = txtxsubTktto.Text.Trim().ToString();
                d.Group = txtbxGSubFrom.Text.Trim().ToString();
                d.Grptkt = txtbxGtktFrom.Text.Trim().ToString();
                d.Misc = txtbxmis.Text.Trim().ToString();
                d.MiscTkt = txtbxmisTfrom.Text.Trim().ToString();
                objmst.SearchHistoryUpdate(userid, null, null, null, null, d, 5);

            }

        }

        private void LoadPriceScaleSearch(PriceScaleDetail psc)
        {
            txtbxSeatFrom.Text = psc.Seats;
            txtbxSinglefrom.Text = psc.Single;
            txttbxgroupfrom.Text = psc.Group;
            txtbxsubscriptionfrom.Text = psc.Subscription;
        }

        private void LoadBoxOfficeSearch(BoxOfficeDetail bd)
        {
            txtbxgrpSalesfrm.Text = bd.GroupSales;
            txtbxdrpcountfrom.Text = bd.DropCount;
            txtbxPAfrom.Text=bd.PaidAttendance;
            txtbxCompFrom.Text=bd.Comp;
            txtbxtsSubFrom.Text=bd.TSSubscription;
            txtbxgrSubFrom.Text=bd.GRSubscription;
            txtbxtsPhoFrom.Text = bd.TSPhone;
            txtbxgrPhoFrom.Text=bd.GRPhone;
            txtbxtsIntFrom.Text=bd.TSInternet;
            txtbxgrIntFrom.Text=bd.GRInternet;
            txtbxtsCreFrom.Text=bd.TSCreditCard;
            txtbxgrCreFrom.Text=bd.GRCreditCard;
            txtbxtsRemFrom.Text=bd.TSRemoteOutlet;
            txtbxgrRemFrom.Text=bd.GRRemoteOutlet;
            txtbxtsSinFrom.Text=bd.TSSingleTickets;
            txtbxgrSinFrom.Text=bd.GRSingleTickets;
            txtbxtsGrp2From.Text =bd.TSGroup2;
            txtbxtsGrp1From.Text =bd.TSGroup1;
            txtbxgrGrp1From.Text=bd.GRGroup1;
            txtbxgrGrp2From.Text = bd.GRGroup2;
           
           
        }
        private void LoadDealSearch(DealDetail dd)
        {
            txt_royalty.Text = dd.Royalty;
            txt_companymonies.Text = dd.Company;
            txt_tax.Text = dd.Tax1;
            txt_facilityfee.Text = dd.OnEachTicket;
            txt_guarantee.Text = dd.Guarantee;
            txt_presentermonies.Text = dd.Presenter;
            txt_boxoffice.Text = dd.TaxFacilityFee;
            txt_middlecap.Text = dd.Cap;
            txt_taxover.Text = dd.TaxAmountOver;
            txt_producershare.Text = dd.Producer;
            txt_taxbudget.Text = dd.Budget;
            txt_subscriptionsale.Text = dd.Subscription;
            txt_singleticket.Text = dd.SingleTickets;
            txt_miscellaneous1.Text = dd.Other1;
            txt_taxactual.Text = dd.Actual;
            txt_phonecommission.Text = dd.Phone;
            txt_groupsale1.Text = dd.Group1;

            txt_internetsale.Text = dd.Internet;
            txt_groupsale3.Text = dd.Group2;
            txt_miscellaneous5.Text = dd.Other2;
            txt_remotesale1.Text = dd.Remote;

        }

        private void LoadDiscountSearch(Discount d)
        {
            txtbxDSubscription.Text = d.Sub;
            txtxsubTktto.Text = d.SubTkt;
            txtbxGSubFrom.Text = d.Group;
            txtbxGtktFrom.Text = d.Grptkt;
            txtbxmis.Text = d.Misc;
            txtbxmisTfrom.Text = d.MiscTkt;

        }

        private void LoadExpenseSearch(ExpenseDetails ed)
        {
            txtadvt_bud.Text = ed.ACTAdvertisingGross;
            txtadvt_act.Text = ed.ACTAdvertisingGross;
            txtadaexp_bud.Text= ed.BUDADAExpense ;
            txtadaexp_act.Text=ed.ACTADAExpense;
            txtlabourcatering_bud.Text= ed.BUDLaborCatering;
            txtlabourcatering_act.Text=ed.ACTLaborCatering;
            txtboxoff_bud.Text= ed.BUDBoxOffice  ;
            txtboxoff_act.Text= ed.ACTBoxOffice;
            txtmusicians_bud.Text=ed.BUDMusicians;
            txtmusicians_act.Text= ed.ACTMusicians;
            txtcatering_bud.Text= ed.BUDCatering;
            txtcatering_act.Text=ed.ACTCatering;
            txteqiprental_bud.Text= ed.BUDEquipmentRental;
            txteqiprental_act.Text= ed.ACTEquipmentRental;
            txtgrpsaleexp_bud.Text =ed.BUDGroupSalesExpenses;
            txtgrpsaleexp_act.Text=ed.ACTGroupSalesExpenses;
            txtstatehandin_bud.Text= ed.BUDshLoadIn ;
            txtstatehandin_act.Text= ed.ACTshLoadIn;
            txthousestaff_bud.Text= ed.BUDHouseStaff;
            txthousestaff_act.Text = ed.ACTHouseStaff;
            //ed.BUDshLoadOut = txtstatehandout_bud.Text.Trim().ToString();
            //ed.ACTshLoadOut = txtstatehandout_act.Text.Trim().ToString();
            //ed.BUDLeagueFees = txtleaguefee_bud.Text.Trim().ToString();
            //ed.ACTLeagueFees = txtleaguefee_act.Text.Trim().ToString();
            //ed.BUDshRunning = txtstatehandsrun_bud.Text.Trim().ToString();
            //ed.ACTshRunning = txtstatehandsrun_act.Text.Trim().ToString();
            //ed.BUDLicensesPermits = txtlicpermits_bud.Text.Trim().ToString();
            //ed.ACTLicensesPermits = txtlicpermits_act.Text.Trim().ToString();
            //ed.BUDLimosAuto = txtlimosauto_bud.Text.Trim().ToString();
            //ed.ACTLimosAuto = txtlimosauto_act.Text.Trim().ToString();
            //ed.BUDOrchestraShellRemoval = txtorchestrashellrml_bud.Text.Trim().ToString();
            //ed.ACTOrchestraShellRemoval = txtorchestrashellrml_act.Text.Trim().ToString();
            //ed.BUDwrLoadIn = txtwardrobehairin_bud.Text.Trim().ToString();
            //ed.ACTwrLoadOut = txtwardrobehairin_act.Text.Trim().ToString();
            //ed.BUDPresenterProfit = txtpresenterprofit_bud.Text.Trim().ToString();
            //ed.ACTPresenterProfit = txtpresenterprofit_act.Text.Trim().ToString();
            //ed.BUDwrLoadOut = txtwardrobehairout_bud.Text.Trim().ToString();
            //ed.ACTwrLoadOut = txtwardrobehairout_act.Text.Trim().ToString();
            //ed.BUDPoliceSecurityFireMarshall = txtpol_sec_fire_mar_bud.Text.Trim().ToString();
            //ed.ACTPoliceSecurityFireMarshall = txtpol_sec_fire_mar_act.Text.Trim().ToString();
            //ed.BUDwrRunning = txtwardrobehairrun_bud.Text.Trim().ToString();
            //ed.ACTwrRunning = txtwardrobehairrun_act.Text.Trim().ToString();
            //ed.BUDProgram = txtprogram_bud.Text.Trim().ToString();
            //ed.ACTProgram = txtprogram_act.Text.Trim().ToString();
            //ed.BUDRent = txtrent_bud.Text.Trim().ToString();
            //ed.ACTRent = txtrent_act.Text.Trim().ToString();
            ////=txtinsurnace_de_unit.Text.Trim().ToString();
            ////=txtinsurance_bud.Text.Trim().ToString();
            ////=txtinsurance_act.Text.Trim().ToString();
            //ed.BUDSoundLights = txtsoundlignt_bud.Text.Trim().ToString();
            //ed.ACTSoundLights = txtsoundlignt_act.Text.Trim().ToString();
            //// =txtticketprint_de_unit.Text.Trim().ToString();
            //// =txtticketprint_de_bud.Text.Trim().ToString();
            //// =txtticketprint_de_act.Text.Trim().ToString();
            ////=txtticketprint_le_bud.Text.Trim().ToString();
            ////=txtticketprint_le_act.Text.Trim().ToString();
            //ed.BUDTelephonesInternet = txttel_internet_bud.Text.Trim().ToString();
            //ed.ACTTelephonesInternet = txttel_internet_act.Text.Trim().ToString();
            //ed.BUDDryIceC02 = txtdryice_bud.Text.Trim().ToString();
            //ed.ACTDryIceC02 = txtdryice_act.Text.Trim().ToString();
            ////=txtother1_de_bud.Text.Trim().ToString();
            ////=txtother1_de_act.Text.Trim().ToString();
            ////=txtother2_de_bud.Text.Trim().ToString();
            ////=txtother2_de_act.Text.Trim().ToString();
            ////=txtother1_le_bud.Text.Trim().ToString();
            ////=txtother1_le_act.Text.Trim().ToString();
            ////=txtother2_le_bud.Text.Trim().ToString();
            ////=txtother2_le_act.Text.Trim().ToString();
            ////=txtother3_le_bud.Text.Trim().ToString();
            ////=txtother3_le_act.Text.Trim().ToString();
            ////=txtother4_le_bud.Text.Trim().ToString();
            ////=txtother4_le_act.Text.Trim().ToString();
            ////=txtother5_le_bud.Text.Trim().ToString();
            ////=txtother5_le_act.Text.Trim().ToString();
            //ed.BUDLocalFixed = txtlocalfixed_bud.Text.Trim().ToString();
            //ed.ACTLocalFixed = txtlocalfixed_act.Text.Trim().ToString();
          
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getcityname(string prefixText)
 {

            MasterData edl = new MasterData();
            List<string> CountryNames = edl.searchcity(prefixText,"Y");
            return CountryNames;

        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetstATEname(string prefixText)
        {

            MasterData edl = new MasterData();
            List<string> CountryNames = edl.SearchState(prefixText);
            return CountryNames;

        }




        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetStatus(string prefixText)
        {

            MasterData edl = new MasterData();
            List<string> Status = edl.SearchEngagementStatus(prefixText);
            return Status;

        }



        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getshownames(string prefixText)
        {
            ShowData edl = new ShowData();
            List<string> shows = edl.searchdata(prefixText, "0");
            return shows;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetVenueName(string prefixText)
        {
            VenueData objven = new VenueData();
            List<string> VenueNames = objven.SearchVenuename(prefixText, "Y");
            return VenueNames;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getpresentername(string prefixText)
        {
            PresenterData objpres = new PresenterData();
            List<string> Presentername = objpres.SearchPresentername(prefixText, "Y");
            return Presentername;

        }

        protected void btnLastSearch_Click(object sender, EventArgs e)
        {
            if (mode == "Schedule")
            {

                Session["Historyid"] = hid;
                type = "2";
                GetSearchData(false);
            }
            else if (mode == "Deal")
            {
                Session["Historyid"] = hid;
                type = "2";
                GetSearchEngmtDealData(false);

            }
            else if (mode == "Price")
            {
                Session["Historyid"] = hid;
                type = "2";
                GetSearchEngmtPriceData(false);
            }
            else if (mode == "Discount")
            {
                Session["Historyid"] = hid;
                type = "2";
                GetSearchEngtDiscount(false);
            }
            else if (mode == "Box")
            {

                Session["Historyid"] = hid;
                type = "2";
                GetSearchEngmtBoxOFfice(false);
 
            }

        }

        protected void drp_subscription_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drp_subscription.SelectedValue == "Y")
            {

                txt_subscription.Visible = true;
            }
            else
            {
               
                txt_subscription.Visible = false;
                txt_subscription.Text = "";
            }
        }

        protected void txt_metrosearch_TextChanged(object sender, EventArgs e)
        {
            string str = txt_metrosearch.Text;
            if(str.Contains('/'))
            {
            string[] str1 = str.Split('/');
            txt_metrosearch.Text = str1[0];
            txtState.Text = str1[1];
            }
        }


    }
}