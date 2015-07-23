using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using EngagementDataLayer;
using ReportDataLayer;
using Microsoft.Reporting.WebForms;
using System.Reflection;
namespace NTOS.Reports
{
    public partial class MarketHistoryReport : System.Web.UI.Page
    {
        RenderingExtension Extension1;
        FieldInfo info1;
        ServerReport server1;


        EngagementData objengt = new EngagementData();
        CommonFunction.CommonFun objcf = new CommonFunction.CommonFun();
        DataTable dt = new DataTable();
        ReportData objrpt = new ReportData();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                bindcolumns();
                bindshows();
                ((ImageButton)this.Master.FindControl("imgbtnsave")).Visible = false;
                ((HiddenField)this.Master.FindControl("hdnreqlist")).Value = (txtfromdate.ClientID + "," + txtToDate.ClientID);
            }
        }
        public void DisableUnwantedExportFormat(ReportViewer ReportViewerID, string strFormatName)
        {
            FieldInfo info;
            foreach (RenderingExtension extension in ReportViewerID.ServerReport.ListRenderingExtensions())
            {
                if (extension.Name.ToLower() == strFormatName.ToLower())
                {
                    info = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
                    info.SetValue(extension, false);
                }
            }
        }
        public void bindshows()
        {
            dt = objengt.GetEngagementShows("Y");
            FillCheckBoxList(chklstshow, dt, "Show_name", "Engt_Show_id");
            foreach (ListItem li in chklstshow.Items)
            {
                li.Selected = true;
            }
            txtshow.Text = "All";
            LoadParameters();
        }
        public void bindcolumns()
        {
            dt = new DataTable();
            dt.Columns.Add("Text", typeof(string));
            dt.Columns.Add("Value", typeof(string));
            DataRow dr;
            for (int i = 1; i < chklstcolumn.Items.Count; i++)
            {
                dr = dt.NewRow();
                dr["Text"] = chklstcolumn.Items[i].Text;
                dr["Value"] = chklstcolumn.Items[i].Value;
                dt.Rows.Add(dr);
            }
            dt.DefaultView.Sort = "Text Asc";
            FillCheckBoxList(chklstcolumn, dt, "Text", "Value");

        }
        public void FillCheckBoxList(CheckBoxList chk, DataTable mydt, string textField, string valueFeild)
        {
            chk.DataSource = mydt;
            chk.DataValueField = valueFeild;
            chk.DataTextField = textField;
            chk.DataBind();
            chk.Items.Insert(0, new ListItem { Value = "0", Text = "All", Selected = false });
        }

        protected void chklstshow_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadParameters();
        }
        public void LoadParameters()
        {
            DataSet ds = new DataSet();
            string showidlist = "0";
            if (chklstshow.Items[0].Selected == false)
            {
                for (int i = 0; i < chklstshow.Items.Count; i++)
                {
                    if (chklstshow.Items[i].Selected == true)
                    {
                        showidlist += ("," + chklstshow.Items[i].Value);
                    }
                }
            }
            ds = objrpt.GetMarketHistoryRpt_Parameter(showidlist.TrimStart(',').TrimEnd(','));
            FillCheckBoxList(chklistvenue, ds.Tables[0], "Venue_Name", "Venue_ID");
            FillCheckBoxList(chklstpresenter, ds.Tables[1], "Presenter_Name", "Presenter_ID");
            FillCheckBoxList(chklstmetro, ds.Tables[2], "City_Name", "City_ID");
            txtvenue.Text = "All";
            txtpresenter.Text = "All";
            txtmetro.Text = "All";
        }
        protected void btnExtract_Click(object sender, EventArgs e)
        {
            ShowReport();
        }
        public string getcollist(CheckBoxList chklist)
        {
            string idlist = "";
            if (chklist.Items[0].Selected == true)
            {
                idlist = "0";
            }
            else
            {
                for (int i = 1; i < chklist.Items.Count; i++)
                {
                    if (chklist.Items[i].Selected == false)
                    {
                        idlist += "#" + chklist.Items[i].Value;
                    }
                }
            }
            return idlist.TrimEnd('#');
        }
        public string getidlist(CheckBoxList chklist)
        {
            string idlist = "0";
            if (chklist.Items.Count > 0)
            {
                if (chklist.Items[0].Selected == true)
                {
                    idlist = "0";
                }
                else
                {
                    for (int i = 1; i < chklist.Items.Count; i++)
                    {
                        if (chklist.Items[i].Selected == true)
                        {
                            idlist += "," + chklist.Items[i].Value;
                        }
                    }
                }
            }
            return idlist.TrimEnd(',').TrimStart(',');
        }

        public void ShowReport()
        {
            string showidlist = "", venueidlist = "", presenteridlist = "", cityidlist = "", colnames = "";
            showidlist = getidlist(chklstshow);
            venueidlist = getidlist(chklistvenue);
            presenteridlist = getidlist(chklstpresenter);
            cityidlist = getidlist(chklstmetro);
            colnames = getcollist(chklstcolumn);
            rptviewer.Visible = true;
            //rptviewer.dis
            rptviewer.ShowCredentialPrompts = false;
            rptviewer.ProcessingMode = ProcessingMode.Remote;
            String ReportServer = System.Configuration.ConfigurationManager.AppSettings["TargetServerURL"].ToString();

            String TargetFolder = System.Configuration.ConfigurationManager.AppSettings["TargetFolder"].ToString();
            rptviewer.ServerReport.ReportServerUrl = new Uri(ReportServer);
            rptviewer.ShowParameterPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[7];
            //DataSourceCredentials[] d = new DataSourceCredentials[1];
            rptviewer.ServerReport.ReportPath = TargetFolder + "/NTOS_MarketHistory";
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "ENGT_SHOW_ID";
            reportParameterCollection[0].Values.Add(Convert.ToString(showidlist));
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "ENGT_VENUE_ID";
            reportParameterCollection[1].Values.Add(Convert.ToString(venueidlist));
            reportParameterCollection[2] = new ReportParameter();
            reportParameterCollection[2].Name = "ENGT_PRESENTER_ID";
            reportParameterCollection[2].Values.Add(Convert.ToString(presenteridlist));
            reportParameterCollection[3] = new ReportParameter();
            reportParameterCollection[3].Name = "ENGT_CITY_ID";
            reportParameterCollection[3].Values.Add(Convert.ToString(cityidlist));
            reportParameterCollection[4] = new ReportParameter();
            reportParameterCollection[4].Name = "FROMDATE";
            reportParameterCollection[4].Values.Add(Convert.ToString(txtfromdate.Text));
            reportParameterCollection[5] = new ReportParameter();
            reportParameterCollection[5].Name = "TODATE";
            reportParameterCollection[5].Values.Add(Convert.ToString(txtToDate.Text));
            reportParameterCollection[6] = new ReportParameter();
            reportParameterCollection[6].Name = "ColumnName";
            reportParameterCollection[6].Values.Add(Convert.ToString(colnames));
            rptviewer.ServerReport.SetParameters(reportParameterCollection);
            rptviewer.ServerReport.Refresh();
        }

        //protected void rptviewer_PreRender(object sender, EventArgs e)
        //{
        //    String ReportServer = System.Configuration.ConfigurationManager.AppSettings["TargetServerURL"].ToString();
        //    String TargetFolder = System.Configuration.ConfigurationManager.AppSettings["TargetFolder"].ToString();
        //    rptviewer.ServerReport.ReportServerUrl = new Uri(ReportServer);
        //    DisableUnwantedExportFormat(rptviewer, "PDF");
        //}



        //#region hideexten

        //public void hide()
        //{
        //    server1 = rptviewer.ServerReport;

        //     server1.ListRenderingExtensions();
        //     //server1.ListRenderingExtensions()[1].Visible = false;
        //}

        //#endregion
    }

}