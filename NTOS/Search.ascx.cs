using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MasterDataLayer;
namespace NTOS
{
    public partial class Search1 : System.Web.UI.UserControl
    {

        DataTable dt = new DataTable();
        MasterData objmst = new MasterData();
        public string Code;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Convert.ToString(Session["pagetitle"]) == Page.Title)
                {
                    DataTable dt1 = (DataTable)Session["Sdt"];
                    gvrep.DataSource = dt1;
                    gvrep.DataBind();
                    lblresults.Text = string.Empty;
                }
                Session["pagetitle"] = Page.Title;

                if (Convert.ToString(Session["pagetitle"]).Contains("Engagement") == true)
                {
                    divsearch.Attributes.Add("style", "display:block");

                }
                else
                {
                    divsearch.Attributes.Add("style", "display:none");
                }
                drp_showsearch.DataSource = objmst.Getshows("0");
                drp_showsearch.DataTextField = "show_name";
                drp_showsearch.DataValueField = "show_id";
                drp_showsearch.DataBind();
                drp_showsearch.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });

                drp_presentersearch.DataSource = objmst.GetPresenters(null);
                drp_presentersearch.DataTextField = "presenter_name";
                drp_presentersearch.DataValueField = "presenter_id";
                drp_presentersearch.DataBind();
                drp_presentersearch.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });


                drp_venuesearch.DataSource = objmst.GetVenues(null);
                drp_venuesearch.DataTextField = "venue_name";
                drp_venuesearch.DataValueField = "venue_id";
                drp_venuesearch.DataBind();
                drp_venuesearch.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });

          

                drp_metrosearch.DataSource = objmst.GetMetroCityStates(null);
                drp_metrosearch.DataTextField = "metro_state";
                drp_metrosearch.DataValueField = "city_id";
                drp_metrosearch.DataBind();
                drp_metrosearch.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
            }

        }
        public void LoadSearchData()
        {
            lblsearchhead.Text = "Search Results:";
            try
            {
                searchdata getsearch = Page as searchdata;
                dt = getsearch.getdata();


                if (dt.Rows.Count == 0)
                {
                    //dt.Rows.Add(dt.NewRow());
                    //gvrep.DataSource = dt;
                    //gvrep.DataBind();
                    //int columncount = gvrep.Rows[0].Cells.Count;
                    //gvrep.Rows[0].Cells.Clear();
                    //gvrep.Rows[0].Cells.Add(new TableCell());
                    //gvrep.Rows[0].Cells[0].ColumnSpan = columncount;
                    lblresults.Text = "No Records Found";
                    gvrep.Visible = false;
                }
                else
                {
                    lblresults.Text = string.Empty;
                    DataTable dt1 = (DataTable)Session["Sdt"];
                    gvrep.DataSource = dt1;
                    gvrep.DataBind();
                    gvrep.Visible = true;
                    gvrep.HeaderRow.Cells[0].Text = Convert.ToString(Session["pagetitle"]).Contains("Engagement") == true ? "Engagement" : Convert.ToString(Session["pagetitle"]).Contains("Personnel") == true ? "First name" : Convert.ToString(Session["pagetitle"]);
                }

            }
            catch (Exception ex)
            {
            }


            //gvrep.Columns[1].Visible = false;
        }
        public void LoadSearchDataE(DataTable dt)
        {

            //gvrep.Columns[1].Visible = false;
            lblsearchhead.Text = "Search Results:";
            try
            {
                if (dt.Rows.Count == 0)
                {
                    lblresults.Text = "No Records Found";
                    gvrep.Visible = false;
                   
                }
                else
                {
                    divsearch.Attributes.Add("style", "display:block");
                    lblresults.Text = string.Empty;
                    gvrep.Visible = true;
                 
                    DataTable dt1 = (DataTable)Session["Sdt"];
                    gvrep.DataSource = dt1;

                    gvrep.DataBind();
                    gvrep.HeaderRow.Cells[0].Text = Convert.ToString(Session["pagetitle"]).Contains("Engagement") ==true ? "Engagement" : Convert.ToString(Session["pagetitle"]);  
                }
            }

            catch (Exception ex)
            {
            }

        }
        public interface searchdata
        {
            DataTable getdata();
        }

        protected void gvrep_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
        }


        protected void btnsearcheng_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //dt = objmst.GetSearchDataNew("Engagement", drp_showsearch.SelectedValue, txtcreatedatesearch.Text , drp_presentersearch.SelectedValue, drp_venuesearch.SelectedValue, drp_metrosearch.SelectedValue);
            //gvrep.DataSource = dt;
            //gvrep.DataBind();

            (this.Page.Master as EngagementMaster).loadsearcheng(drp_showsearch.SelectedValue, Convert.ToString(txtcreatedatesearch.Text) == string.Empty ? "0" : Convert.ToString(txtcreatedatesearch.Text), drp_presentersearch.SelectedValue, drp_venuesearch.SelectedValue, drp_metrosearch.SelectedValue);

        }



    }

}