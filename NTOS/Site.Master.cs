using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using CommonFunction;
using System.IO;
using System.Drawing;
namespace NTOS
{
    public partial class Site1 : System.Web.UI.MasterPage
    {

        public static bool isAscend = false;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";
        public static bool showImage = false;
        public static bool isSort = false;
        CommonFunction.CommonFun objcf = new CommonFunction.CommonFun();
        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }


        private SortDirection GridViewSortDirection
        {

            get
            {

                if (ViewState["sortDirection"] == null)

                    ViewState["sortDirection"] = SortDirection.Ascending;


                return (SortDirection)ViewState["sortDirection"];

            }

            set { ViewState["sortDirection"] = value; }

        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Context.Session["username"] == null)
            {
                Response.Redirect("~/Account/Login.aspx?url=" + HttpContext.Current.Request.Url.AbsolutePath.Replace("http://", "") + "");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            

            if (Session["searchAll"] != null)
            {


                //if (Session["isNewAll"] != null)
                //{
                //    string isn = Convert.ToString(Session["isNewAll"]);
                //    if (isn == "1")
                //        navpage.Visible = false;
                //    else
                //        navpage.Visible = true;
                //}



                if (this.Page.Title == "SearchAll")
                {

                    btnsearch1.Visible = true;
                    btnNegativeSearch.Visible = true;
                    btnNew.Visible = true;
                }

                if (!IsPostBack) //check if the webpage is loaded for the first time.
                {
                    ViewState["PreviousPage"] =
                    Request.UrlReferrer;//Saves the Previous page url in ViewState
                }

                if (string.IsNullOrEmpty(Request.QueryString["search"]) == false)
                {
                    navpage.Visible = !string.IsNullOrEmpty(Request.QueryString["search"]);
                    // btnlist.Visible = !string.IsNullOrEmpty(Request.QueryString["searchAll"]);
                    btnsearch1.Visible = !string.IsNullOrEmpty(Request.QueryString["search"]);
                    //  lisearchlist.Visible = false;
                    lblrecindex.Text = Convert.ToString(Session["recindexAll"]);
                    DataTable dtsession = new DataTable();
                    dtsession = (DataTable)Session["searchAll"];
                    if (dtsession.Rows.Count > 0)
                    {

                        if (drpdwnSearch.Items.Count == 0)
                        {
                            drpdwnSearch.Items.Add(new ListItem("--Search--", "0"));
                            drpdwnSearch.Items.Add(new ListItem("New", "1"));
                            drpdwnSearch.Items.Add(new ListItem("Constrain", "2"));
                            drpdwnSearch.Items.Add(new ListItem("Extended", "3"));
                            drpdwnSearch.DataBind();
                            drpdwnSearch.Visible = true;
                            ImageButton1.Visible = false;
                            btnsearch1.Visible = false;
                            btnNegativeSearch.Visible = false;
                            //   Session["isNewAll"] = null;

                        }


                        lblrectot.Text = "of " + dtsession.Rows.Count.ToString();
                        lisearchlist.Visible = true;

                    }
                }




            }
            if (!Page.IsPostBack)
            {
                //    ddlSearch.Items.Add(new ListItem("Select", "0", true));
                //    ddlSearch.Items.Add(new ListItem("New", "1"));
                //    ddlSearch.DataBind();
                lblrecindex.Text = Convert.ToString(Session["recindexAll"]);
            }

        }



        public void checkcontrols()
        {
            objcf.HideControls(MainContent, this.Page);

        }

        protected void btninsert_Click(object sender, EventArgs e)
        {
            try
            {
                MasterPageSaveInterface insertinterface = Page as MasterPageSaveInterface;
                if (insertinterface != null)
                {
                    insertinterface.SaveData();
                    hdn_modify_status.Value = "0";
                }
            }
            catch (Exception ex)
            {
                Label lbl = (Label)MainContent.FindControl("lblerrmsg");
                //lbl.Text = "Error: Data did not submit. Please contact system administrator!";
                lbl.Text = "Error: " + ex.Message.ToString();
                lbl.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                MasterPageSaveInterface updateinterface = Page as MasterPageSaveInterface;
                if (updateinterface != null)
                {
                    updateinterface.DeleteData("n");
                }
            }
            catch (Exception)
            {
                Label lbl = (Label)MainContent.FindControl("lblerrmsg");
                lbl.Text = "Error: Data did not submit. Please contact system administrator!";
                lbl.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btnundelete_Click(object sender, EventArgs e)
        {
            try
            {
                MasterPageSaveInterface updateinterface = Page as MasterPageSaveInterface;
                if (updateinterface != null)
                {
                    updateinterface.DeleteData("y");
                }
            }
            catch (Exception)
            {
                Label lbl = (Label)MainContent.FindControl("lblerrmsg");
                lbl.Text = "Error: Data did not submit. Please contact system administrator!";
                lbl.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void imgbtndiary_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Diary.aspx", false);
        }
        public void hide()
        {
            imgbtndelete.Visible = false;
            imgbtnsave.Visible = false;
        }
        public void show_control(string activeflag, Panel pnl)
        {

            if (activeflag.ToLower() == "y")
            {
                imgbtndelete.Visible = true;
                imgbtnundelete.Visible = false;
                pnl.Enabled = true;
                imgbtnsave.Visible = true;
            }
            else
            {
                pnl.Enabled = false;
                imgbtnsave.Visible = false;
                imgbtndelete.Visible = false;
                imgbtnundelete.Visible = true;
            }
        }
        public void show_control(string activeflag, Panel pnl, string beastflg)
        {

            if (activeflag.ToLower() == "y")
            {
                imgbtndelete.Visible = true;
                imgbtnundelete.Visible = false;
                pnl.Enabled = true;
                imgbtnsave.Visible = true;
            }
            else
            {
                pnl.Enabled = false;
                imgbtnsave.Visible = false;
                imgbtndelete.Visible = false;
                imgbtnundelete.Visible = true;
            }
            divbeast.Visible = (beastflg.ToLower() == "y") ? true : false;
        }
        public void HideNewbutton()
        {
            li_search.Attributes.Add("style", "visibility:hidden");
            btnNew.Visible = true;
            ImageButton1.Visible = false;
            //  li_new .Attributes.Add("style", "visibility:hidden");
        }
        public void SetfNewbutton(string strpage)
        {
            //a_new_btn.Target = "_self";
            //if (strpage == "Deal")
            //{ 
            //    a_new_btn.HRef="/Deal.aspx";

            //}
            //else if (strpage == "Contact")
            //{
            //    a_new_btn.HRef = "/Personal.aspx";
            //}
            //else if (strpage == "Presenter")
            //{
            //    a_new_btn.HRef = "/Presenter.aspx";
            //}
            //else if (strpage == "Show")
            //{
            //    a_new_btn.HRef = "/Show.aspx";
            //}
            //else if (strpage == "Venue")
            //{
            //    a_new_btn.HRef = "/Venue.aspx";
            //}
            //else
            //{


            //}

        }

        public void ClearModifyStatus()
        {
            hdn_modify_status.Value = "0";
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            string a = hdn_modify_status.Value;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // ucsearch.LoadSearchData();
            //btnsearch.Focus();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

            var title = this.Page.Title;
            //var a_tag = document.getElementById("A2");
            Response.Redirect("SearchAll.aspx?title=" + title + "&type=1", false);

        }
        protected void btnSearch1_Click(object sender, EventArgs e)
        {
            if (this.Page.Title != "SearchAll")
            {
                Response.Redirect("SearchAll.aspx", false);
            }
            ISearchAll insertinterface = Page as ISearchAll;
            if (insertinterface != null)
            {
                string url = HttpContext.Current.Request.Url.AbsoluteUri;

                if (url.Contains("Presenter") == true)
                    insertinterface.GetSearchPresentationData(false);
                if (url.Contains("Venue") == true)
                    insertinterface.GetSearchVenueData(false);
                if (url.Contains("Show") == true)
                    insertinterface.GetSearchShowData(false);
                if (url.Contains("Personnel") == true)
                    insertinterface.GetSearchPersonalData(false);

            }



        }

        #region Search
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            MoveRecord("f");

        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            MoveRecord("p");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            MoveRecord("n");
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            MoveRecord("l");
        }
        public void MoveRecord(string type)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["searchAll"];
            Int32 idx = 0, dtcount = dt.Rows.Count;
            string keyid = "";
            switch (type)
            {
                case "f":
                    {
                        idx = 0;
                        keyid = dt.Rows[idx]["keyid"].ToString();
                        idx++;
                        break;
                    }
                case "p":
                    {
                        idx = Convert.ToInt32(lblrecindex.Text) - 1;
                        if (idx == 0)
                            break;
                        keyid = dt.Rows[idx - 1]["keyid"].ToString(); break;
                    }
                case "n":
                    {
                        idx = Convert.ToInt32(lblrecindex.Text) + 1;
                        if (idx > dtcount)
                            break;
                        keyid = dt.Rows[idx - 1]["keyid"].ToString(); break;
                    }
                case "l": { idx = dtcount; keyid = dt.Rows[dtcount - 1]["keyid"].ToString(); break; }
            }

            if (keyid != "")
            {
                string url = "";
                if (Convert.ToString(Session["Page"]) == "Presentation")
                {
                    // url = "" + "?presenterid=" + keyid + "&search=y";
                    url = "~/Presenter.aspx?presenterid=" + keyid + "&search=y";
                }
                if (Convert.ToString(Session["Page"]) == "Venue")
                {
                    //  url = HttpContext.Current.Request.Url.AbsolutePath + "?venueid=" + keyid + "&search=y";
                    url = "~/Venue.aspx?venueid=" + keyid + "&search=y";
                }
                if (Convert.ToString(Session["Page"]) == "Show")
                {
                    // url = HttpContext.Current.Request.Url.AbsolutePath + "?showid=" + keyid + "&search=y";
                    url = "~/Show.aspx?showid=" + keyid + "&search=y";
                }
                if (Convert.ToString(Session["Page"]) == "Personnel")
                {
                    // url = HttpContext.Current.Request.Url.AbsolutePath + "?personalid=" + keyid + "&search=y";
                    url = "~/Personal.aspx?personalid=" + keyid + "&search=y";
                }

                Session["recindexAll"] = idx.ToString();
                Response.Redirect(url);
            }
        }
        #endregion
        protected void btnlist_Click(object sender, EventArgs e)
        {

            //Response.Redirect("Search.aspx?mode=list", false);


            DataTable dt = new DataTable();
            dt = (DataTable)Session["searchAll"];
            // ucsearch.LoadSearchDataE(dt);
            gvrep.DataSource = dt;
            gvrep.DataBind();
            gvrep.HeaderRow.Cells[0].Text = Convert.ToString(this.Page.Title).Contains("Presenter") == true ? "Presenter" : Convert.ToString(this.Page.Title).Contains("Personnel") == true ? "First name" : Convert.ToString(this.Page.Title);
            Panel1.Attributes.Add("class", "popup");
            modpop.Show();


        }
        protected void gvrep_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Text.ToString().Length > 10)
                    {
                        e.Row.Cells[i].Text = e.Row.Cells[i].Text.ToString().Substring(0, 10) + "...";
                    }
                }
            }
        }

        protected void drpdwnSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = drpdwnSearch.SelectedItem.Value;
            if (Convert.ToInt16(type) > 0)
            {

                if (type == "1")
                {
                    // Session["search"] = null;
                    navpage.Visible = false;
                    //    Session["isNewAll"] = "1";
                }
                var title = this.Page.Title;
                //var a_tag = document.getElementById("A2");
                Response.Redirect("SearchAll.aspx?title=" + title + "&type=" + type, false);

            }
        }

        protected void btnNegativeSearch_Click(object sender, ImageClickEventArgs e)
        {

           
            ISearchAll insertinterface = Page as ISearchAll;
            if (insertinterface != null)
            {
                string url = HttpContext.Current.Request.Url.AbsoluteUri;

                if (url.Contains("Presenter") == true)
                    insertinterface.GetSearchPresentationData(true);
                if (url.Contains("Venue") == true)
                    insertinterface.GetSearchVenueData(true);
                if (url.Contains("Show") == true)
                    insertinterface.GetSearchShowData(true);
                if (url.Contains("Personnel") == true)
                    insertinterface.GetSearchPersonalData(true);
            }


        }

        protected void btnNew_Click(object sender, EventArgs e)
        {


        }


        protected void btnredirect_Click(object sender, EventArgs e)
        {
            navpage.Visible = false;
            string title = this.Page.Title; ;
            switch (title)
            {
                case "Show":
                    {
                        Response.Redirect("Show.aspx", true);
                        break;
                    }
                case "Presenter":
                    {
                        Response.Redirect("Presenter.aspx", true);
                        break;
                    }
                case "Venue":
                    {
                        Response.Redirect("Venue.aspx", true);
                        break;
                    }
                case "Personnel":
                    {

                        Response.Redirect("Personal.aspx", true);
                        break;
                    }

            }

        }


      

        protected void gvrep_Sorting(object sender, GridViewSortEventArgs e)
        {

            string sortingDirection = string.Empty;
            if (direction == SortDirection.Ascending)
            {
                direction = SortDirection.Descending;
                sortingDirection = "Desc";

            }
            else
            {
                direction = SortDirection.Ascending;
                sortingDirection = "Asc";

            }
            DataTable dt = new DataTable();
            dt = (DataTable)Session["searchAll"];
            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;
            gvrep.DataSource = sortedView;
            gvrep.DataBind();
            modpop.Show();

              }

        protected void imgbtnexit_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["PreviousPage"] != null)	//Check if the ViewState 
            //contains Previous page URL
            {
                Response.Redirect(ViewState["PreviousPage"].ToString());//Redirect to 
                //Previous page by retrieving the PreviousPage Url from ViewState.
            }
        }





        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages

                DataTable dt = new DataTable();
                dt = (DataTable)Session["search"];
                System.Web.UI.WebControls.GridView dg = new GridView();
                dg.DataSource = dt;
                dg.DataBind();
                dg.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in dg.HeaderRow.Cells)
                {
                    cell.BackColor = gvrep.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in dg.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = dg.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = dg.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                dg.RenderControl(hw);
                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        protected void gvrep_RowCreated(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void btnSettelementreport_Click(object sender, EventArgs e)
        {

            Response.Redirect("Reports\\SettelementReport.aspx");
        }
        //protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //string type = ddlSearch.SelectedItem.Value;
        //    if (Convert.ToInt16(type) > 0)
        //    {

        //        if (this.Page.Title != "SearchAll")
        //        {
        //            Response.Redirect("SearchAll.aspx", false);
        //        }
        //        ISearchAll insertinterface = Page as ISearchAll;
        //        if (insertinterface != null)
        //        {
        //            string url = HttpContext.Current.Request.Url.AbsoluteUri;

        //            if (url.Contains("Presenter") == true)
        //                insertinterface.GetSearchPresentationData(type);
        //            if (url.Contains("Venue") == true)
        //                insertinterface.GetSearchVenueData(type);
        //            if (url.Contains("Show") == true)
        //                insertinterface.GetSearchShowData(type);
        //        }
        //    }

        //}


       

    }

 
}
