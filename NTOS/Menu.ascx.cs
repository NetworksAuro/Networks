using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MasterDataLayer;
using EngagementDataLayer;
using System.Data;
using ScheuleDataLayer;
using System.Globalization;
using System.Text.RegularExpressions;
using PersonalDataLayer;
using System.Xml;
using System.IO;

namespace NTOS
{
    public partial class Menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                switch (Convert.ToString(Session["userrole"]))
                {
                    case "reader":
                        {
                            //mnew.Visible = false;
                            //mtemplate.Visible = false;
                            madmin.Visible = false;
                            break;
                        }
                    case "compmanager":
                        {
                            madmin.Visible = false;
                            //liexcel.Visible = liusers.Visible = false;
                            break;
                        }
                    case "officestaff":
                        {
                            //liexcel.Visible = liusers.Visible = false;
                            break;
                        }
                    case "admin":
                        {
                           // liexcel.Visible = false;
                            break;
                        }
                }
               // checkpermission();
                push();
                SetActiveMenu();
            }
        }
        public void SetActiveMenu()
        {
           //  nav-act-main

            if (Page.Request.RawUrl.Contains("Diary"))
            {
                a_diary.Attributes.Add("class", "nav-act-main");
            }
          
            else if (Page.Request.RawUrl.Contains("Venue"))
            {
                h_venue.Attributes.Add("class", "nav-act-main");
            }

             else if (Page.Request.RawUrl.Contains("Engagement") || Page.Request.RawUrl.Contains("engagement") )
            {
                h_engt.Attributes.Add("class", "nav-act-main");
               
            }

             else  if (Page.Request.RawUrl.Contains("Presenter"))
            {
                h_presenter.Attributes.Add("class", "nav-act-main");
            }
            else if (Page.Request.RawUrl.Contains("Show"))
            {
                h_show.Attributes.Add("class", "nav-act-main");
            }
            else if (Page.Request.RawUrl.Contains("Personal"))
            {
                h_conatacts.Attributes.Add("class", "nav-act-main");
            }

            else if (Page.Request.RawUrl.Contains("Report"))
            {
                a_reports.Attributes.Add("class", "nav-act-main");
            }

            else if (Page.Request.RawUrl.Contains("City") || Page.Request.RawUrl.Contains("Country") || Page.Request.RawUrl.Contains("State") || Page.Request.RawUrl.Contains("Title") || Page.Request.RawUrl.Contains("Timezone") || Page.Request.RawUrl.Contains("Deal") || Page.Request.RawUrl.Contains("Users"))
            {
                a_admin.Attributes.Add("class", "nav-act-main");
            }
            else
            {
            }

        }
       
     
        public void checkpermission()
        {
            bool flg = true;
            string urlpath = HttpContext.Current.Request.Url.AbsolutePath.ToLower().Replace("/", "");
            //if (mnew.Visible == false && mnew.InnerHtml.ToLower().Contains(urlpath))
            //{
            //    flg = false;
            //}
            if (mtemplate.Visible == false && mtemplate.InnerHtml.ToLower().Contains(urlpath))
            {
                flg = false;
            }
            if (madmin.Visible == false && madmin.InnerHtml.ToLower().Contains(urlpath))
            {
                flg = false;
            }
            if (flg == false)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

        }
        protected override void OnPreRender(EventArgs e)
        {


        }
        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);
        //    Page.Header.DataBind();
        //}
        public void push()
        {
            DataTable dt = new DataTable();
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            string title = "";
            if (Request.QueryString.Count > 0)
            {
                title = "Modify " + Page.Title;
            }
            else
            {
                title = Page.Title;
            }
            if (Session["history"] == null)
            {
                dt.Columns.Add("Sno");
                dt.Columns.Add("url");
                dt.Columns.Add("title");

            }
            else
            {
                dt = (Session["history"]) as DataTable;
            }
            DataTable dtc = new DataTable();
            if (dt.Select("url='" + url + "'").Length > 0)
            {
                dtc = dt.Select("url='" + url + "'").CopyToDataTable();
                if (dtc.Rows.Count > 0)
                {
                    int ro = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["url"].ToString() == dtc.Rows[0]["url"].ToString())
                        {
                            dt.Rows[ro]["sno"] = DateTime.Now.ToString();
                        }
                        ro++;
                    }
                }
            }
            if (dt.Rows.Count <= 9)
            {

                if (dtc.Rows.Count < 1)
                {
                    dt.Rows.Add(DateTime.Now.ToString(), url, title);
                }
            }
            else
            {
                if (dtc.Rows.Count < 1)
                {
                    dt.Rows.RemoveAt(0);
                    dt.Rows.Add(DateTime.Now.ToString(), url, title);

                }
            }
            Session["history"] = dt;
            dt.DefaultView.Sort = "Sno Desc";
            dt.DefaultView.ToTable();
           
            ddlHistory.DataSource = dt;
            ddlHistory.DataValueField = "url";
            ddlHistory.DataTextField = "title";
            ddlHistory.DataBind();
            ddlHistory.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
        }
        protected void ddlHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlHistory.SelectedIndex > 0)
                    Response.Redirect(ddlHistory.SelectedItem.Value);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}