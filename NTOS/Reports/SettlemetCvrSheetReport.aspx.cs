using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShowDataLayer;
using MasterDataLayer;
using ReportDataLayer;
using Microsoft.Reporting.WebForms;
namespace NTOS.Reports
{
    public partial class SettlemetCvrSheetReport : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        MasterData objshow = new MasterData();
        ReportData objrpt = new ReportData();
        CommonFunction.CommonFun objcf = new CommonFunction.CommonFun();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadShow();
                ((ImageButton)this.Master.FindControl("imgbtnsave")).Visible = false;
                ((HiddenField)this.Master.FindControl("hdnreqlist")).Value = (ddlShow.ClientID + "," + ddlCity.ClientID + "," + ddlCreateddate.ClientID);
            }
        }
        public void loadShow()
        {
            try
            {
                dt = objrpt.GetEngtReportParameters("S", null, null, null);
                objcf.FillDropDownList(ddlShow, dt, "show_name", "show_id");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void loadCities()
        {
            try
            {
                clear("c");
                dt = objrpt.GetEngtReportParameters("C", Convert.ToInt32(ddlShow.SelectedItem.Value), null, null);
                objcf.FillDropDownList(ddlCity, dt, "City_Name", "City_id");
                if (dt.Rows.Count == 1)
                {
                    ddlCity.SelectedIndex = 1;
                    loadVenues();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void loadVenues()
        {
            objshow = new MasterData();
            DataTable dt = new DataTable();
            dt = objrpt.GetEngtReportParameters("V", Convert.ToInt32(ddlShow.SelectedItem.Value), Convert.ToInt32(ddlCity.SelectedItem.Value), null);
            objcf.FillDropDownList(ddlVenue, dt, "VENUE_NAME", "VENUE_ID");
            if (dt.Rows.Count == 1)
            {
                ddlVenue.SelectedIndex = 1;
            }
            loadCreateddate();
        }
        public void loadCreateddate()
        {

            objshow = new MasterData();
            objrpt = new ReportData();
            DataTable dt = new DataTable();
            Nullable<int> venueid = null;
            venueid = (ddlVenue.SelectedIndex > 0) ? Convert.ToInt32(ddlVenue.SelectedItem.Value) : venueid;
            dt = objrpt.GetEngtReportParameters("D", Convert.ToInt32(ddlShow.SelectedItem.Value), Convert.ToInt32(ddlCity.SelectedItem.Value), venueid);
            objcf.FillDropDownList(ddlCreateddate, dt, "ENGTSTARTDATE", "ENGAGEMENTID");
            if (dt.Rows.Count == 1)
            {
                ddlCreateddate.SelectedIndex = 1;
                LoadEnddate();
            }
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear("c");
            loadVenues();

        }

        protected void ddlVenue_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCreateddate();
        }
        public void clear(string type)
        {
            lblEngtEndDate.Text = string.Empty;
            switch (type)
            {
                case "s":
                    {
                        Clearddl(ddlCreateddate);
                        Clearddl(ddlVenue);
                        break;
                    }
                case "c":
                    {
                        Clearddl(ddlCreateddate);
                        Clearddl(ddlVenue);
                        break;
                    }
            }
        }
        protected void ddlShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear("s");
            loadCities();
        }
        public void Clearddl(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
        }

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            ShowReport();
        }
        public void ShowReport()
        {
            rptviewer.Visible = true;
            //rptviewer.dis
            rptviewer.ShowCredentialPrompts = false;
            rptviewer.ProcessingMode = ProcessingMode.Remote;
            String ReportServer = System.Configuration.ConfigurationManager.AppSettings["TargetServerURL"].ToString();

            String TargetFolder = System.Configuration.ConfigurationManager.AppSettings["TargetFolder"].ToString();
            rptviewer.ServerReport.ReportServerUrl = new Uri(ReportServer);
            rptviewer.ShowParameterPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[1];
            //DataSourceCredentials[] d = new DataSourceCredentials[1];
            rptviewer.ServerReport.ReportPath = TargetFolder + "/NTOS_SettlementCvrSheet";
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "ENGTID";
            reportParameterCollection[0].Values.Add(Convert.ToString(ddlCreateddate.SelectedItem.Value));
            rptviewer.ServerReport.SetParameters(reportParameterCollection);
            rptviewer.ServerReport.Refresh();
        }
        public void LoadEnddate()
        {
            lblEngtEndDate.Text = string.Empty;
            objshow = new MasterData();
            objrpt = new ReportData();
            DataTable dt = new DataTable();
            Nullable<int> venueid = null;
            venueid = (ddlVenue.SelectedIndex > 0) ? Convert.ToInt32(ddlVenue.SelectedItem.Value) : venueid;
            dt = objrpt.GetEngtReportParameters("E", Convert.ToInt32(ddlCreateddate.SelectedItem.Value), venueid, venueid);
            if (dt.Rows.Count > 0)
            {
                lblEngtEndDate.Text = dt.Rows[0]["ENGTENDDATE"].ToString();
            }
        }

        protected void ddlCreateddate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEnddate();

        }
    }
}