using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DairyDataLayer;
using System.Data;
using System.Text;
using System.IO;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using MasterDataLayer;
using System.Web.Services;
using System.Collections;

namespace NTOS
{
    public partial class Dairy : System.Web.UI.Page
    {
        DairyData objData = new DairyData();
        static DairyData objData1 = new DairyData();
        DataSet ds = new DataSet();

        //declare global variables
        int pageNum;
        protected string headvalues;
        protected string rowvalues;
        protected DataTable dtfls;
        protected int colcount;
        protected string q;
        protected string emptyspace = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try

                {
                    pageNum = 0;
                    (this.Master as Site1).HideNewbutton();
           
                    hfdiaryhide.Value = "";
                    if (System.DateTime.Now.Month < 7)
                    {
                        txtDatepicker.Text = "07/01/" + System.DateTime.Now.AddYears(-1).Year.ToString();
                    }
                    else
                    {
                        txtDatepicker.Text = "07/01/" + System.DateTime.Now.Year.ToString();
                    }
                    Label lbl_msg;
                    lbl_msg = (Label)this.Master.FindControl("lbl_headersite1");
                   // lbl_msg.Text = "Diary";
                    hdfdate.Value = txtDatepicker.Text;
                    fillrepeater(Convert.ToDateTime(txtDatepicker.Text), "");
                    Seasonfill();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('tab', 250, 900 , 60 ,true); </script>", false);
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>scroll(); </script>", false);
                }
                catch (Exception ex)
                {
                    divmsg.InnerHtml = "Contact system administrator!";
                }
                ((ImageButton)this.Master.FindControl("imgbtnsave")).Visible = false;
                ((ImageButton)this.Master.FindControl("imgbtnreset")).Visible = false;
                //txtDatepicker.Attributes.Add("ReadOnly", "ReadOnly");
            }

        }
        public void Seasonfill()
        {
            ds = new DataSet();
            objData = new DairyData();
            ds = objData.GetSeasonDates();
            ddlSeason.DataSource = ds;
            ddlSeason.DataTextField = "Dates";
            ddlSeason.DataValueField = "value";
            ddlSeason.DataBind();
            ddlSeason.Items.Insert(0, "--Select--");
            int cfinyear = (System.DateTime.Now.Month < 7) ? System.DateTime.Now.Year - 1 : System.DateTime.Now.Year;
            ddlSeason.SelectedIndex = ddlSeason.Items.IndexOf(ddlSeason.Items.FindByValue(cfinyear.ToString()));


        }
        public void fillrepeater(Nullable<DateTime> date, string type)
        {
            ds = new DataSet();
            objData = new DairyData();
            ds = objData.GetDiary(date, type);
            string title = "";
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                divmsg.InnerHtml = string.Empty;
                divrep.Visible = true;
                title = Convert.ToString(ds.Tables[2].Rows[0][0]);
               
                // ltH.Text = title;



                    DataTable dtbody = ds.Tables[1].Copy();
                     
                  
                    DataRow dr = dtbody.NewRow();
                    dr["rep"] = title;
                    dtbody.Rows.InsertAt(dr, 0);
                    Session["PagedData"] = dtbody; 

                PagedDataSource pagedData = new PagedDataSource();
                pagedData.DataSource = dtbody.DefaultView;
                pagedData.AllowPaging = true;
                pagedData.PageSize = 15;
                pagedData.CurrentPageIndex = pageNum;


                
                     StringBuilder html = new StringBuilder();
 
        //Table start.
                   html.Append("<table id ='tab' cellpadding='0'>");


                StringBuilder sb1 = new StringBuilder();
                for (int i = 0; i <  dtbody.Rows.Count; i++)
                {
                    html.Append(dtbody.Rows[i][0].ToString());
                }
                //Table end.
                html.Append("</table>");

                //Append the HTML string to Placeholder.
                PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
              //  rep.DataSource = pagedData;
               // rep.DataBind();
               // ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key1", "<script>BindTable('"+sb1+"'); </script>", false);
               
               // lblCurrentPage.Text = "Page " + Convert.ToString(pagedData.CurrentPageIndex) + " of " + Convert.ToString(pagedData.PageCount);

                //if (pageNum == 0)
                //{
                //    btnPrev.Visible = false;
                //}
                //else
                //{ btnPrev.Visible = true; }
                //if (pageNum >= Math.Floor((decimal)ds.Tables[1].Rows.Count / 15))
                //{
                //    btnNext.Visible = false;
                //}
                //else { btnNext.Visible = true; }
              
                //bindliteral(ds);
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('tab', 250, 900 , 60 ,true); </script>", false);

            }
            else
            {
                divmsg.InnerHtml = "No Records Found!";
                divrep.Visible = false;
            }


        }
        //public void bindliteral(DataSet ds)
        //{
        //    StringBuilder body = new StringBuilder();
        //    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
        //    {
        //        rep.DataSource = ds.Tables[1];
        //        rep.DataBind();

        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "scroll();", true);
        //    }
        //}
        
        protected void btn_Click(object sender, EventArgs e)
        {
            hfdiaryhide.Value = "0";
           // ddlSeason.SelectedIndex = 0;
            chkbxFiveyears.Checked = false;
            hdfdate.Value = txtDatepicker.Text;
            DateTime datval = Convert.ToDateTime(hdfdate.Value);
            int cfinyear = (datval.Month < 7) ? datval.Year - 1 : datval.Year;
            ddlSeason.SelectedIndex = ddlSeason.Items.IndexOf(ddlSeason.Items.FindByValue(cfinyear.ToString()));
            fillrepeater(datval, "");
        }
        protected void ddlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfdiaryhide.Value = "0";
            chkbxFiveyears.Checked = false;
            if (ddlSeason.SelectedIndex == 0)
            {
                return;
            }
            txtDatepicker.Text = "07/01/" + ddlSeason.SelectedItem.Value;
            hdfdate.Value = "07/01/" + ddlSeason.SelectedItem.Value;
            fillrepeater(Convert.ToDateTime(txtDatepicker.Text), "");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "scroll();", true);

        }
        //protected void btnFiveyears_Click(object sender, EventArgs e)
        //{
        //    //if (chkbxFiveyears.Checked == true)
        //    //{
        //    //    ddlSeason.SelectedIndex = 0;
        //        txtDatepicker.Text = "07/01/" + Convert.ToString(System.DateTime.Now.Year - 4);
        //        hdfdate.Value = "07/01/" + System.DateTime.Now.Year.ToString();
        //        fillrepeater(Convert.ToDateTime(hdfdate.Value), "F");
        //    //}
        //    //else
        //    //{
        //    //    ddlSeason.SelectedIndex = 0;
        //    //    txtDatepicker.Text = "07/01/" + System.DateTime.Now.Year.ToString();
        //    //    hdfdate.Value = "07/01/" + System.DateTime.Now.Year.ToString();
        //    //    fillrepeater(Convert.ToDateTime(txtDatepicker.Text), "");
        //    //}
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "scroll();", true);
        //}
        protected void chkbxFiveyears_CheckedChanged(object sender, EventArgs e)
        {
            hfdiaryhide.Value = "0";
            if (chkbxFiveyears.Checked == true)
            {
                ddlSeason.SelectedIndex = 0;
                txtDatepicker.Text = "07/01/" + Convert.ToString(System.DateTime.Now.Year - 4);
                hdfdate.Value = "07/01/" + System.DateTime.Now.Year.ToString();
                fillrepeater(Convert.ToDateTime(hdfdate.Value), "F");
            }
            else
            {
                ddlSeason.SelectedIndex = 0;
                txtDatepicker.Text = "07/01/" + System.DateTime.Now.Year.ToString();
                hdfdate.Value = "07/01/" + System.DateTime.Now.Year.ToString();
                fillrepeater(Convert.ToDateTime(txtDatepicker.Text), "");
            }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "scroll();", true);
        }

        protected void lnkbtnlastyears_Click(object sender, EventArgs e)
        {

            ddlSeason.SelectedIndex = 0;
            txtDatepicker.Text = "07/01/" + Convert.ToString(Convert.ToDateTime(hdfdate.Value).Year - 4); txtDatepicker.Text = "07/01/" + Convert.ToString(Convert.ToDateTime(hdfdate.Value).Year - 4);
            hdfdate.Value = (System.DateTime.Now.Month < 7) ? "07/01/" + Convert.ToString(System.DateTime.Now.Year - 1) : "07/01/" + System.DateTime.Now.Year.ToString();
            hdfdate.Value = txtDatepicker.Text;
            DateTime datval = Convert.ToDateTime(hdfdate.Value);
            int cfinyear = (datval.Month < 7) ? datval.Year - 1 : datval.Year;
            ddlSeason.SelectedIndex = ddlSeason.Items.IndexOf(ddlSeason.Items.FindByValue(cfinyear.ToString()));
            fillrepeater(Convert.ToDateTime(hdfdate.Value), "F");

        }

        protected void rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int index = e.Item.ItemIndex ;
                
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            //send to next page

            string pno ="0";
            if(ViewState["Pageno"]!= null)
            pno = ViewState["Pageno"] as string;
            pageNum =Convert.ToInt16(pno)+ 1;
            ViewState["Pageno"] = Convert.ToString(pageNum);
            fillrepeater(Convert.ToDateTime(txtDatepicker.Text), "");
           // Response.Redirect("?Page=" + Convert.ToString(pageNum + 1));
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            //send to previous page
            string pno = "0";
            if (ViewState["Pageno"] != null)
                pno = ViewState["Pageno"] as string;
            pageNum = Convert.ToInt16(pno) - 1;
            ViewState["Pageno"] = Convert.ToString(pageNum);
            fillrepeater(Convert.ToDateTime(txtDatepicker.Text), "");
           // Response.Redirect("?Page=" + Convert.ToString(pageNum - 1));
        }




        [WebMethod (EnableSession=true)]
       public static string BindDataAsync(int pageNumber)
        {
            StringBuilder sb = new StringBuilder();

            // Pass in page number and page size as your database call should
            // only return the first (x) records in this case it's page 1, max records 100

            DataTable dt = HttpContext.Current.Session["PagedData"] as DataTable;
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    int TotalPages = dt.Rows.Count / 15;
                   
                     if (pageNumber > 0 && pageNumber <= TotalPages)
                    {
                        int startAt = 15 * (pageNumber - 1) + 1;


                        for (int i = startAt; i < startAt + 15; i++)
                        {
                            sb.Append(dt.Rows[i][0].ToString());
                        }
                    }
                }
            }

               
           
            
            return sb.ToString();
        }
    }

}