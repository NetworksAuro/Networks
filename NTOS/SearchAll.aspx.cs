using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PresenterDataLayer;
using MasterDataLayer;
using System.Data;
using CommonFunction;
using VenueDataLayer;
using ShowDataLayer;
using NTOS.DataLayer;
using PersonalDataLayer;
using System.Text.RegularExpressions;

namespace NTOS
{
    public partial class SearchAll : System.Web.UI.Page, ISearchAll
    {
        MasterData objmsd = new MasterData();
        CommonFun objcf = new CommonFun();
        DataTable dt;
        protected string uniqueKey;
        public string mode = "";
        public string userid = "";
        public string type = "";
        public string hid ="";
        protected void page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 filldropdown();
                 fillCompanyManagers();
                 fillemployeetype();
                 filltitle();
                
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string finlstring = "";
            this.uniqueKey = Guid.NewGuid().ToString("N");
            //this.aceresidcity.OnClientItemSelected = "setcityID_" + this.ID + "";
           // this.aceZipcode.OnClientItemSelected = "setcityID_" + this.ID + "";
            this.Master.FindControl("imgbtnsave").Visible = false;
            this.Master.FindControl("lisearchlist").Visible = false;
            this.Master.FindControl("btnsearch1").Visible = true;
            userid = Convert.ToString(Session["userid"]);
            type = Convert.ToString(Request.QueryString["type"]);
            string call = Convert.ToString(Request.QueryString["call"]);
            if (string.IsNullOrEmpty(call) == false)
            {
                if (call == "Show")
                    GetSearchShowData(false);
                else if (call == "Presenter")
                    GetSearchPresentationData(false);
                else if (call == "Venue")
                    GetSearchVenueData(false);

            }

                if (Convert.ToString(Request.QueryString["title"]) == "Presenter")
                {
                    pnlpresenter.Visible = true;
                    Session["Page"] = "Presentation";
                    mode = "Presentation";
                    DataTable dt = objmsd.GetHistoryForAll(userid, 1);
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
                else if (Convert.ToString(Request.QueryString["title"]) == "Venue")
                {
                    pnlpresenter.Visible = true;
                    Session["Page"] = "Venue";
                    mode = "Venue";
                    DataTable dt = objmsd.GetHistoryForAll(userid, 2);
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
                else if (Convert.ToString(Request.QueryString["title"]) == "Show")
                {
                    pnlShow.Visible = true;
                    Session["Page"] = "Show";
                    mode = "Show";
                    DataTable dt = objmsd.GetHistoryForAll(userid, 3);
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
                else if (Convert.ToString(Request.QueryString["title"]) == "Personnel")
                {
                    pnlContact.Visible = true;
                    Session["Page"] = "Personnel";
                    mode="Personnel";
                     DataTable dt = objmsd.GetHistoryForAll(userid, 10);
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

                if (Page.IsPostBack)
                {
                    ddckComanyManager.Visible = true;
                    lblCompanymanager.Visible = false;
                }
                else
                {

                    if (Convert.ToString(Request.QueryString["title"]) == "Presenter")
                    {
                        VenuePresenter vp = objmsd.GetSearchAllHistory(1, userid);
                        if (vp != null)
                        {
                            if (type != "1")
                                LoadVenuePresenterData(1, vp);
                        }
                    }
                    else if (Convert.ToString(Request.QueryString["title"]) == "Venue")
                    {
                        VenuePresenter vp = objmsd.GetSearchAllHistory(2, userid);
                        if (vp != null)
                        {
                            LoadVenuePresenterData(2, vp);
                        }
                    }
                    else if (Convert.ToString(Request.QueryString["title"]) == "Show")
                    {
                        ShowDetail shw = objmsd.GetSearchShowHistory(3, userid);
                        if (shw != null)
                        {
                            LoadShowHistory(shw);
                        }
                    }
                
                }


          
        
        }

        public void filldropdown()
        {
            fillcity();
           
        }
        public void fillcity()
        {
            dt = new DataTable();
            dt = objmsd.GetCityStates();
            //objcf.FillDropDownList(ddlcity, dt, "City_Name", "City_id");

        //    ddchkCountry.DataSource = dt;
        //    ddchkCountry.DataTextField = "City_Name";
        //    ddchkCountry.DataValueField = "City_id";
        //    ddchkCountry.DataBind();
       }


        public void fillCompanyManagers()
        {
            objmsd = new MasterData();
            dt = objmsd.GetUserList("compmanager");
            ddckComanyManager.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                ddckComanyManager.DataTextField = "users_name";
                ddckComanyManager.DataValueField = "users_id";
                ddckComanyManager.DataBind();
            }
            else { lblCompanymanager.Text = "No Manager exists";
            lblCompanymanager.Visible = true;
            ddckComanyManager.Visible = false;
            }

        }

        //public void fillzipcodedetails(List<string> cityIDs)
        //{
        //    DataTable dt = new DataTable();
        //    DataTable dt1 = new DataTable();
        //    MasterData mdl = new MasterData();
        //    dt1.Columns.Add("ZipCode", typeof(string));
        //    dt1.Columns.Add("Zipid", typeof(Int32));
        //    foreach(string st in cityIDs)
        //    {
        //        if (Convert.ToInt32(st) != 0)
        //        {
        //            dt = mdl.Getcitydetails(Convert.ToInt32(st));
        //            if (dt.Rows.Count > 0)
        //            {
        //                string[] zip = dt.Rows[0]["Zip"].ToString().Split(',');
                        
        //                for (int  j= 0; j < zip.Length; j++)
        //                {
        //                    dt1.Rows.Add(zip[j], j);
        //                }
        //            }
        //        }
        //    }
        //    drpdwnckbxZipCode.DataSource = dt1;
        //    drpdwnckbxZipCode.DataTextField = "ZipCode";
        //    drpdwnckbxZipCode.DataValueField = "Zipid";
        //    drpdwnckbxZipCode.DataBind();

        //}







        public void GetSearchPresentationData(bool isMaster)
        {
            int outhistid = 0;
            int histid = 0;
            string[] citySelected;
            InsertHistoryPresenterVenue(1);
            Session["recindexAll"] = "1";
            DataTable dt = new DataTable();
            string presenter = "", country = "", lastname = "", firstname = "", city = "", zipcode = "", state="";
            presenter = (string.IsNullOrEmpty(txtpresentername.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txtpresentername.Text.Trim());
            country = (string.IsNullOrEmpty(txtCountry.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txtCountry.Text);
            lastname = (string.IsNullOrEmpty(txtbxLastName.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txtbxLastName.Text.Trim());
            firstname = (string.IsNullOrEmpty(txtbxFirstname.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txtbxFirstname.Text.Trim());
            zipcode = (string.IsNullOrEmpty(txtPVZipCode.Text.Trim()) == true) ? string.Empty : txtPVZipCode.Text.Trim();
            city = (string.IsNullOrEmpty(txtPVCityState.Text.Trim()) == true) ? string.Empty :objcf.getCityAfterSplit( txtPVCityState.Text.Trim());
            state = (string.IsNullOrEmpty(txtstatePV.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txtstatePV.Text.Trim());

            int typ = Convert.ToInt16(type);
            if (typ == 2 || typ == 3 || typ == 4)
            {
                if (Session["HistoryidAll"] != null)
                    histid = Convert.ToInt32(Session["HistoryidAll"]);

            }
            if (isMaster == true)
                typ = 4;
            dt = objmsd.GetSearchDataPresenter(presenter, city,state, zipcode, country, firstname, lastname,typ,userid,histid,out outhistid);
           

            if (dt.Rows.Count > 0)
            {
                Session["HistoryidAll"] = outhistid;
                //this.Master.FindControl("navpage").Visible = true;
                this.Master.FindControl("btnlist").Visible = true;
                this.Master.FindControl("navpage").Visible = true;
                //  this.Master.FindControl("btnsearch1").Visible = false;
                this.Master.FindControl("lisearchlist").Visible = true;

                if (this.MasterPageFile == "/Site.Master")
                {
                    this.Master.FindControl("btnsearch1").Visible = true;
                }
                else { this.Master.FindControl("btnsearch1").Visible = true; }
                

               
                    Session["searchAll"] = dt;
                
                Response.Redirect("~/Presenter.aspx?presenterid=" + dt.Rows[0]["KeyID"].ToString() + "&search=y&Title=Presenter");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Records Found');", true);
            }
        }

        public void GetSearchVenueData(bool isMaster)
        {
            int outhistid = 0;
            int histid = 0;
            string[] citySelected;
            InsertHistoryPresenterVenue(2);
            Session["recindexAll"] = "1";
            DataTable dt = new DataTable();
            string venue = "", country="", lastname="", firstname="", city = "", zipcode = "", capacity = "",state="";
            venue = (string.IsNullOrEmpty(txtvenuname.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txtvenuname.Text.Trim());
            country = (string.IsNullOrEmpty(txtCountry.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txtCountry.Text);
            lastname = (string.IsNullOrEmpty(txtbxLastName.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txtbxLastName.Text.Trim());
            firstname = (string.IsNullOrEmpty(txtbxFirstname.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txtbxFirstname.Text.Trim());
            capacity = (string.IsNullOrEmpty(txtCapacity1.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForSql(txtCapacity1.Text.Trim());
            zipcode = (string.IsNullOrEmpty(txtPVZipCode.Text.Trim()) == true) ? string.Empty : txtPVZipCode.Text.Trim();
            city = (string.IsNullOrEmpty(txtPVCityState.Text.Trim()) == true) ? string.Empty: objcf.getCityAfterSplit( txtPVCityState.Text.Trim());
            state = (string.IsNullOrEmpty(txtstatePV.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txtstatePV.Text.Trim());

            int typ = Convert.ToInt16(type);
            if (typ == 2 || typ == 3 || typ==4)
            {
                if (Session["HistoryidAll"] != null)
                    histid = Convert.ToInt32(Session["HistoryidAll"]);

            }
            if (isMaster == true)
                typ = 4;
            
            dt = objmsd.GetSearchDataVenue(venue, city,state, zipcode, country, firstname, lastname, capacity,typ,userid,histid,out outhistid);
          
            if (dt.Rows.Count > 0)
            {
                Session["HistoryidAll"] = outhistid;
                //this.Master.FindControl("navpage").Visible = true;
                this.Master.FindControl("btnlist").Visible = true;
                this.Master.FindControl("navpage").Visible = true;
                //  this.Master.FindControl("btnsearch1").Visible = false;
                this.Master.FindControl("lisearchlist").Visible = true;

                if (this.MasterPageFile == "/Site.Master")
                {
                    this.Master.FindControl("btnsearch1").Visible = true;
                }
                else { this.Master.FindControl("btnsearch1").Visible = true; }
              

                Session["searchAll"] = dt;
                Response.Redirect("~/Venue.aspx?venueid=" + dt.Rows[0]["KeyID"].ToString() + "&search=y");

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Records Found');", true);
            }
        }


        public void GetSearchPersonalData(bool isMaster)
        {
            int outhistid = 0;
            int histid = 0;
         
           // InsertHistoryPresenterVenue(2);
            Session["recindexAll"] = "1";
            DataTable dt = new DataTable();
            string fname = "", lname = "", mname = "", doh = "",comp="",tdate ="",empstatus="", city = "",emptype="", zipcode = "",show="",ms="",dob="",country="",Title="";
            fname = (string.IsNullOrEmpty(txtbxCFirstName.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txtbxCFirstName.Text.Trim());
            mname = (string.IsNullOrEmpty(txtbxCMiddleName.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txtbxCMiddleName.Text);
            lname = (string.IsNullOrEmpty(txtlastname.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txtlastname.Text.Trim());
            comp = (string.IsNullOrEmpty(txtcompany.Text.Trim()) == true) ? string.Empty : txtcompany.Text.Trim();
            show = (string.IsNullOrEmpty(txtCShowName.Text.Trim()) == true) ? string.Empty : txtCShowName.Text.Trim();
            comp = (string.IsNullOrEmpty(txtcompany.Text.Trim()) == true) ? string.Empty : txtcompany.Text.Trim();
            doh = (string.IsNullOrEmpty(txtdateifhire.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForSql(txtdateifhire.Text.Trim());
            tdate = (string.IsNullOrEmpty(txttermanitationdate.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForSql(txttermanitationdate.Text.Trim());
            ms = (string.IsNullOrEmpty(txtmaterialstatus.Text.Trim()) == true) ? string.Empty : txtmaterialstatus.Text.Trim();
            dob = (string.IsNullOrEmpty(txtdob.Text.Trim()) == true) ? string.Empty :objcf.SplitStringForSql( txtdob.Text.Trim());
            city = (string.IsNullOrEmpty(txtCity.Text.Trim()) == true) ? string.Empty : objcf.getCityAfterSplit( txtCity.Text.Trim());
            zipcode = (string.IsNullOrEmpty(txtZipCode.Text.Trim()) == true) ? string.Empty : txtZipCode.Text.Trim();
            country = (string.IsNullOrEmpty(txtCCountry.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txtCCountry.Text.Trim());
            if(ddltitle.SelectedIndex == 0)
                Title  ="";
            else
            Title =objcf.SplitStringForSql( ddltitle.SelectedItem.Value);
            if (ddlemployeestatus.SelectedIndex == 0)
                empstatus = "";
            else
            empstatus = ddlemployeestatus.SelectedItem.Text;
            if (ddlemployeetype.SelectedIndex == 0)
                emptype = "";
            else
            emptype = ddlemployeetype.SelectedItem.Text;

              
            int typ = Convert.ToInt16(type);
        
            if (typ == 2 || typ == 3 || typ == 4)
            {
                if (Session["HistoryidPerson"] != null)
                    histid = Convert.ToInt32(Session["HistoryidPerson"]);

            }
            if (isMaster == true)
                typ = 4;

            dt = objmsd.GetSearchPersonalData(fname, lname, mname, Title, emptype, empstatus, ms, show, doh, tdate, zipcode, dob, comp, city, typ, country, userid, histid, out outhistid);

            if (dt.Rows.Count > 0)
            {
                Session["HistoryidPerson"] = outhistid;
                //this.Master.FindControl("navpage").Visible = true;
                this.Master.FindControl("btnlist").Visible = true;
                this.Master.FindControl("navpage").Visible = true;
                //  this.Master.FindControl("btnsearch1").Visible = false;
                this.Master.FindControl("lisearchlist").Visible = true;

                if (this.MasterPageFile == "/Site.Master")
                {
                    this.Master.FindControl("btnsearch1").Visible = true;
                }
                else { this.Master.FindControl("btnsearch1").Visible = true; }


                Session["searchAll"] = dt;
                Response.Redirect("~/Personal.aspx?personalid=" + dt.Rows[0]["KeyID"].ToString() + "&search=y");

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Records Found');", true);
            }
        }





        private void InsertHistoryPresenterVenue(int type)
        {
            string city = "", zipcode = "";
            VenuePresenter vp = new VenuePresenter();
            int t = type ;
            if (type == 2)
            {
                vp.capacity = txtCapacity1.Text.Trim().ToString();
                vp.VenuePresenterName = txtvenuname.Text.Trim().ToString();
            }
            else{vp.VenuePresenterName= txtpresentername.Text.Trim().ToString();}
            vp.Country= txtCountry.Text.Trim().ToString();
            vp.FirstName= txtbxFirstname.Text.Trim().ToString();
            vp.LastName=txtbxLastName.Text.Trim().ToString();
            
            
            vp.ZipCode = zipcode;
            objmsd.SearchAllHistoryUpdate(t,0,userid,vp);

            }
        

            private void LoadVenuePresenterData(int type ,VenuePresenter vp)
            {
                if (type == 1)
                {
                   txtpresentername.Text = vp.VenuePresenterName; 

                }
                else
                {
                    txtvenuname.Text = vp.VenuePresenterName;
                    txtCapacity1.Text = vp.capacity;

                }
            txtCountry.Text= vp.Country;
            txtbxFirstname.Text= vp.FirstName;
            txtbxLastName.Text = vp.LastName;
           
            

             
            }

            private void InsertShowHistory()
            {
                string companyManager = "";
                ShowDetail shw = new ShowDetail();
                foreach (System.Web.UI.WebControls.ListItem item in ddckComanyManager.Items)
                {
                    if (item.Selected)
                    {
                        if (companyManager != "")
                        {
                            companyManager = "," + item.Text;
                        }
                        else
                        { companyManager = item.Text; }

                    }
                }
                shw.Show = txtshowname.Text.Trim().ToString();
                shw.ShowBegindate = txtshowbegindate.Text.Trim().ToString();
                shw.VariableRoylaties = txtvariableroyalities.Text.Trim().ToString();
                shw.WeeklyOperatingExpense = txtwklyopexp.Text.Trim().ToString();
                shw.CompanyManager = companyManager;
                shw.CorporateName = txtcorpname.Text.Trim().ToString();
                objmsd.SearchShowHistoryUpdate(userid, shw);

                  
            }
            private void LoadShowHistory(ShowDetail shw)
            {
                txtshowname.Text = shw.Show;
                txtshowbegindate.Text = shw.ShowBegindate;
                txtoverheadnut.Text = shw.OverheadNut;
                txtvariableroyalities.Text = shw.VariableRoylaties;
                txtwklyopexp.Text = shw.WeeklyOperatingExpense;
                txtcorpname.Text = shw.CorporateName;
                string[] CM = shw.CompanyManager.Split(',');
                for (int i = 0; i < CM.Count(); i++)
                {
                    foreach (ListItem item in ddckComanyManager.Items)
                    {
                        if (CM[i] == item.Text)
                            item.Selected = true;
                    }
                }
                  

            }

            public void GetSearchShowData(bool isMaster)
            {
                int outhistid = 0;
                int histid = 0;
                

            InsertShowHistory();
            Session["recindexAll"] = "1";
            DataTable dt = new DataTable();
            string Show = "", Showbegindate="",corporateN="" ,CM="",ON="",VR="",WOE="";
            Show = (string.IsNullOrEmpty(txtshowname.Text.Trim()) == true) ? string.Empty : objcf.SplitStringForComma(txtshowname.Text.Trim());
            corporateN = (string.IsNullOrEmpty(txtcorpname.Text.Trim()) == true) ? string.Empty : txtcorpname.Text;

            if (txtshowbegindate.Text != "")
            {
              Showbegindate= objcf.SplitStringForSql(objcf.SplitForDates(txtshowbegindate.Text));
            }
                

           if (txtoverheadnut.Text != "")
            {
                 ON = objcf.SplitStringForSql(txtoverheadnut.Text);
            }
           
            if (txtvariableroyalities.Text != "")
            {

                VR = objcf.SplitStringForSql(txtvariableroyalities.Text);

            }
            

            if (txtwklyopexp.Text != "")
            {
                WOE = objcf.SplitStringForSql(txtwklyopexp.Text);

            }
           

            foreach (System.Web.UI.WebControls.ListItem item in ddckComanyManager.Items)
            {
                if (item.Selected)
                {
                    if (CM != "")
                    {
                        CM += ",'" + item.Text+"'";
                    }
                    else
                    { CM = "'"+item.Text+"'"; }

                }
            }

      
            int typ = Convert.ToInt16(type);

            if (typ == 2 || typ == 3 )
            {
                if (Session["HistoryidAll"] != null)
                    histid = Convert.ToInt32(Session["HistoryidAll"]);

            }
            if (isMaster == true)
                typ = 4;
             dt = objmsd.GetSearchDataShow(Show, Showbegindate, corporateN, CM, ON, VR, WOE,typ,userid,histid,out outhistid);
            


            if (dt.Rows.Count > 0)
            {
                Session["HistoryidAll"] = outhistid;
                //this.Master.FindControl("navpage").Visible = true;
                this.Master.FindControl("btnlist").Visible = true;
                this.Master.FindControl("navpage").Visible = true;
                //  this.Master.FindControl("btnsearch1").Visible = false;
                this.Master.FindControl("lisearchlist").Visible = true;

                if (this.MasterPageFile == "/Site.Master")
                {
                    this.Master.FindControl("btnsearch1").Visible = true;
                }
                else { this.Master.FindControl("btnsearch1").Visible = true; }
                //if (type == "3")
                //{
                //    DataTable dt1 = new DataTable();
                //    dt1 = (DataTable)Session["searchAll"];
                //    dt.Merge(dt1, false, MissingSchemaAction.Add);
                //}
                Session["searchAll"] = dt;
                Response.Redirect("~/Show.aspx?showid=" + dt.Rows[0]["KeyID"].ToString() + "&search=y");

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Records Found');", true);
            }
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
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetZipcode(string prefixText)
        {

            MasterData edl = new MasterData();
            List<string> Zipcodelist = edl.serachZipcode(prefixText);
            return Zipcodelist;

        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getcityname(string prefixText)
        {

            MasterData edl = new MasterData();
            List<string> CountryNames = edl.searchcity(prefixText, "Y");
            return CountryNames;

        }



        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getcountryname(string prefixText)
        {

            MasterData edl = new MasterData();
            List<string> CountryNames = edl.searchcountry(prefixText, "Y");
            return CountryNames;

        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getpersonalname(string prefixText)
         {
            PresenterData objpres = new PresenterData();
            List<string> personal = objpres.searchpersonal("", prefixText, "F", "Y");
            return personal;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getlastname(string prefixText)
        {
            PresenterData objpres = new PresenterData();
            List<string> personal = objpres.searchpersonal( prefixText,"", "L", "Y");
            return personal;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getcompanyname(string prefixText)
        {
            PersonalData objper = new PersonalData();
            List<string> company = objper.GetCompanyName(prefixText);
            return company;
        }
      

        protected void fillemployeetype()
        {
            MasterData objmsd = new MasterData();
            DataTable dt = new DataTable();
            dt = objmsd.GetLookupList("employeetype");
            objcf.FillDropDownList(ddlemployeetype, dt, "lkup_desc", "lkup_id");
            dt = objmsd.GetLookupList("employeestatus");
            objcf.FillDropDownList(ddlemployeestatus, dt, "lkup_desc", "lkup_id");
            string def_status = "";
            if (dt.Select("lkup_desc='active'").Length > 0)
            {
                def_status = dt.Select("lkup_desc='active'").CopyToDataTable().Rows[0]["lkup_desc"].ToString();
            }
            ddlemployeestatus.SelectedIndex = 0;

        }
        protected void filltitle()
        {
            MasterData objmsd = new MasterData();
            DataTable dt = new DataTable();
            dt = objmsd.Gettitle();
            ddltitle.DataSource = dt;
            ddltitle.DataTextField = "title_name";
            ddltitle.DataValueField = "title_id";
            ddltitle.DataBind();
            ListItem li = new ListItem("-Select-", "0");
            ddltitle.Items.Insert(0, li);

        }





        protected void ddckComanyManager_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnLastSearch_Click(object sender, EventArgs e)
        {
                         if( mode == "Presentation")
                         {

                             Session["HistoryidAll"]= hid;
                             type="2";
                             GetSearchPresentationData(false);
                         }
                         else if (mode == "Venue")
                         {
                             Session["HistoryidAll"] = hid;
                             type = "2";
                             GetSearchVenueData(false);
 
                         }
                         else if (mode == "Show")
                         {
                             Session["HistoryidAll"] = hid;
                             type = "2";
                             GetSearchShowData(false);
                         }
                         else if (mode == "Personnel")
                         {
                             Session["HistoryidPerson"] = hid;
                             type = "2";
                             GetSearchPersonalData(false);
                         }
                        
        }

        protected void txtPVCityState_TextChanged(object sender, EventArgs e)
        {
            string str = txtPVCityState.Text;
            if(str.Contains('/'))
            {
            string[] str1 = str.Split('/');
            txtPVCityState.Text = str1[0];
            txtstatePV.Text = str1[1];
            }

                 


        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetstATEname(string prefixText)
        {

            MasterData edl = new MasterData();
            List<string> CountryNames = edl.SearchState(prefixText);
            return CountryNames;

        }

        
    }
}