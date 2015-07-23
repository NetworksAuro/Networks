using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShowDataLayer;
using System.Configuration;
using EngagementDataLayer;
using MasterDataLayer;
using System.Data;
using ReportDataLayer;
using Microsoft.Reporting.WebForms;
namespace NTOS.Reports
{
    public partial class RouteReport : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        MasterData objshow = new MasterData();
        ReportData objrpt = new ReportData();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblmsg.Text = string.Empty;
            if (!IsPostBack)
            {
                string scr = "javascript:return comparedate('" + txtAsofDate.ClientID + "','" + txtTodate.ClientID + "','As of date should be less than To date!');";
                txtAsofDate.Attributes.Add("onblur", scr);
                txtTodate.Attributes.Add("onblur", scr);
                loadShow();
                loadCities();
                txtCity.Attributes.Add("ReadOnly", "ReadOnly");
                txtcolumn.Attributes.Add("ReadOnly", "ReadOnly");
                bindcolumns();
                ((ImageButton)this.Master.FindControl("imgbtnsave")).Visible = false;
                ((HiddenField)this.Page.Master.FindControl("hdnreqlist")).Value = (ddlShow.ClientID + "," + txtAsofDate.ClientID + "," + txtTodate.ClientID);
            }
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
        protected void Page_Init(object sender, EventArgs e)
        {

        }
        public void loadShow()
        {
            try
            {
                dt = objshow.Getshows("Y");
                ddlShow.DataSource = dt;
                ddlShow.DataTextField = "show_name";
                ddlShow.DataValueField = "show_id";
                ddlShow.DataBind();
                ddlShow.Items.Insert(0, "-Select-");
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

                dt = objshow.LoadCities();
                chkCity.DataSource = dt;
                chkCity.DataTextField = "City_Name";
                chkCity.DataValueField = "City_id";
                chkCity.DataBind();
                chkCity.Items.Insert(0, "All");
                //string text = "";
                //string delimeter = "";
                foreach (ListItem item in chkCity.Items)
                {
                    item.Selected = true;
                    //text = text + delimeter + item.Text.ToString();
                    //delimeter = ",";
                }
                //chkCity.SelectedIndex = 0;
                txtCity.Text ="All";
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
        protected void btnExtract_Click(object sender, EventArgs e)
        {
            try
            {
                rptviewer.Visible = false;
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                string city = "";
                string Delemiter = "",columns="";
                foreach (ListItem lst in chkCity.Items)
                {
                    if (lst.Selected == true)
                    {
                        if (lst.Text.ToUpper().Trim() == "All".ToUpper().Trim())
                        {
                            city = "0";
                            break;
                        }
                        else
                        {
                            city += Delemiter + lst.Value;
                        }
                        Delemiter = ",";
                    }
                }
                dt = objrpt.GetRouteReportdetails(Convert.ToInt32(ddlShow.SelectedItem.Value), Convert.ToDateTime(txtAsofDate.Text.ToString()), Convert.ToDateTime(txtTodate.Text.ToString()), city);
                if (dt.Select("city<>''", "").Length > 0)
                {
                    ds.Tables.Add(dt);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        columns = getcollist(chklstcolumn);
                        rptviewer.Visible = true;
                        //rptviewer.dis
                        rptviewer.ShowCredentialPrompts = false;
                        rptviewer.ProcessingMode = ProcessingMode.Remote;
                        String ReportServer = System.Configuration.ConfigurationManager.AppSettings["TargetServerURL"].ToString();
                        String TargetFolder = System.Configuration.ConfigurationManager.AppSettings["TargetFolder"].ToString();
                        rptviewer.ServerReport.ReportServerUrl = new Uri(ReportServer);
                        rptviewer.ShowParameterPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[5];
                        //DataSourceCredentials[] d = new DataSourceCredentials[1];
                        rptviewer.ServerReport.ReportPath = TargetFolder + "/NTOS_Route";
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Show";
                        reportParameterCollection[0].Values.Add(Convert.ToString(ddlShow.SelectedItem.Value));
                        reportParameterCollection[1] = new ReportParameter();
                        reportParameterCollection[1].Name = "fromdate";
                        reportParameterCollection[1].Values.Add(Convert.ToString(txtAsofDate.Text.ToString()));
                        reportParameterCollection[2] = new ReportParameter();
                        reportParameterCollection[2].Name = "todate";
                        reportParameterCollection[2].Values.Add(Convert.ToString(txtTodate.Text.ToString()));
                        reportParameterCollection[3] = new ReportParameter();
                        reportParameterCollection[3].Name = "city";
                        reportParameterCollection[3].Values.Add(Convert.ToString(city));
                        reportParameterCollection[4] = new ReportParameter();
                        reportParameterCollection[4].Name = "Columns";
                        reportParameterCollection[4].Values.Add(Convert.ToString(columns));
                        rptviewer.ServerReport.SetParameters(reportParameterCollection);
                        rptviewer.ServerReport.Refresh();
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "hideWord()", true);





                      
                    }
                }
                else
                {
                    lblmsg.Text = "No Records found!";
                    
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        protected void chkCity_SelectedIndexChanged(object sender, EventArgs e)
        {

            string city = "";
            string Delemiter = "";
            foreach (ListItem lst in chkCity.Items)
            {
                if (lst.Selected == true)
                {
                    if (lst.Text.ToUpper().Trim() == "All".ToUpper().Trim())
                    {
                        city = "All";
                        break;
                    }
                    else
                    {
                        city += Delemiter + lst.Text;
                    }
                    Delemiter = ",";
                }
            }
            txtCity.Text = city;

        }
    }
}