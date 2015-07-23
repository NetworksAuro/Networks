using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MasterDataLayer;
using MetroDataLayer;
using CommonFunction;
using System.Globalization;
using System.Threading;
namespace NTOS
{
    public partial class Metro : System.Web.UI.Page, MasterPageSaveInterface
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            divmsg.InnerHtml = "";
            lblerrmsg.Text = "";
            if (!Page.IsPostBack)
            {
                createtemptable();
                BindDropdown();
                hdnmetrocityid.Value = "0";
                hdnnearbycityid.Value = "0";
                if (!string.IsNullOrEmpty(Request.QueryString["mertorcityid"]))
                {
                 //   lnknew.Visible = true;
                 //   lblhead.Text = "Modify Metro";
                    hdnmetrocityid.Value = Request.QueryString["mertorcityid"];
                    loadmetrodetails();
                }
                if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                {
                    divmsg.InnerHtml = "Record submitted successfully!";
                }
                ((HiddenField)this.Master.FindControl("hdnreqlist")).Value = (txtmetrocityname.ClientID + "," + ddlcountry.ClientID + "," + ddlstate.ClientID);
            }

        }
        public void BindDropdown()
        {
            BindNearyByMetro();
            filltimezone();
            fillcountry();
            fillstate();
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
        public static List<string> GetStateName(string prefixText)
        {
            MasterData edl = new MasterData();
            List<string> StateName = edl.SearchState(prefixText);
            return StateName;

        }
        public void createtemptable()
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn();
            dc.ColumnName = "tempid";
            dc.DataType = typeof(int);
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            dt.Columns.Add(dc);
            dt.Columns.Add("cityid", typeof(Int32));
            dt.Columns.Add("cityname", typeof(string));
            dt.Columns.Add("statename", typeof(string));
            dt.Columns.Add("CountryName", typeof(string));
            dt.Columns.Add("Zip", typeof(string));
            dt.Columns.Add("metro_id", typeof(int)).DefaultValue = 0;
            ViewState["temptable"] = dt;
        }
        protected void fillstate()
        {
            MasterData mdl = new MasterData();
            DataTable dt = new DataTable();
            dt = mdl.Getstate();
            CommonFunction.CommonFun objcf = new CommonFunction.CommonFun();
            objcf.FillDropDownList(ddlstate, dt, "state_name", "state_id");
            //DropDownList ddlstatenc = Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("ddlstatenc") as DropDownList;
            //objcf.FillDropDownList(ddlstatenc, dt, "state_name", "state_id");
        }
        public void fillcountry()
        {
            MasterData objmst = new MasterData();
            DataTable dt = objmst.Getcountry();
            CommonFun objcf = new CommonFun();
            objcf.FillDropDownList(ddlcountry, dt, "Country_Name", "Country_ID");
            //DropDownList ddlcountrync = Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("ddlcountrync") as DropDownList;
            //objcf.FillDropDownList(ddlcountrync, dt, "Country_Name", "Country_ID");
        }
        public void filltimezone()
        {
            MasterData objmst = new MasterData();
            DataTable dt = objmst.GetTimezone("Y");
            ddltimezone.DataSource = dt;
            ddltimezone.DataValueField = "TIMEZONE_ID";
            ddltimezone.DataTextField = "TIMEZONE_DESC";
            ddltimezone.DataBind();
            ddltimezone.Items.Insert(0, new ListItem { Value = "0", Text = "--Select--", Selected = true });
        }
        public void SaveTempnearbymetro()
        {
            if (hdnmetrocityid.Value == hdnnearbycityid.Value && hdnStateID.Value == hdnstateidnc.Value && hdncountry.Value == hdncountryidnc.Value)
            {
                lblerrmsg.Text = "Metro city and near by metro city should not be same!";
                return;
            }
            DropDownList ddlcountrync = Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("ddlcountrync") as DropDownList;
            DropDownList ddlcitynearby = Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("ddlcitynearby") as DropDownList;
            DropDownList ddlstatenc = (DropDownList)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("ddlstatenc");
            TextBox txtcitynearby = (TextBox)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("txtcitynearby");
            Label lblnearbystate = (Label)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("lblnearbystate");
            Label lblzipcodenc = (Label)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("lblzipcodenc");
            if ((hdnnearbycityid.Value.Trim() == "" && hdnnearbycityid.Value.Trim() == "0" && txtcitynearby.Text != string.Empty) || (ddlcountrync.SelectedIndex > 0 || ddlstatenc.SelectedIndex > 0))
            {
                SaveNewNearbyCity();
            }

            if (hdnnearbycityid.Value.Trim() != "" && hdnnearbycityid.Value.Trim() != "0")
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["temptable"];
                DataRow dr;
                dr = dt.NewRow();
                dr["cityid"] = hdnnearbycityid.Value;
                dr["cityname"] = ddlcitynearby.SelectedItem.Text;
                dr["statename"] = lblnearbystate.Text;
                dr["CountryName"] = lblnearbystate.Text;
                dr["Zip"] = lblzipcodenc.Text;
                dt.Rows.Add(dr);
                ViewState["temptable"] = dt;
                BindNearyByMetro();
            }

        }
        public void BindNearyByMetro()
        {
            DataTable dt = (DataTable)ViewState["temptable"];
            Repnearbymetro.DataSource = dt;
            Repnearbymetro.DataBind();

        }
        public void SaveNewNearbyCity()
        {
            TextBox txtcitynearby = (TextBox)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("txtcitynearby");
            TextBox txtstatenc = (TextBox)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("txtstatenc");
            DropDownList ddlstatenc = (DropDownList)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("ddlstatenc");
            DropDownList ddlcountrync = (DropDownList)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("ddlcountrync");
            TextBox txtZipCodenc = (TextBox)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("txtZipCodenc");
            MasterData objmst = new MasterData();
            string cityname, statename, zipcode;
            cityname = txtcitynearby.Text.Trim();
            statename = (ddlcountrync.SelectedItem.Text.ToLower() != "usa") ? txtstatenc.Text : ddlstatenc.SelectedItem.Text;
            zipcode = txtZipCodenc.Text;
            int countryid = 0, stateid;
            Nullable<int> timezoneid = null;
            countryid = Convert.ToInt32(ddlcountrync.SelectedItem.Value);
            stateid = Convert.ToInt32(ddlstatenc.SelectedItem.Value);
            int newcityid = objmst.CityDetails_Insert(cityname, stateid, statename, countryid, zipcode, timezoneid);
            if (newcityid < 0)
            {
                lblerrmsg.Text = "Error: Data did not submit. Please contact system administrator!";
                lblerrmsg.ForeColor = System.Drawing.Color.Red;
            }
            hdnnearbycityid.Value = newcityid.ToString();
        }
        public void SaveNewCity()
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            MasterData objmst = new MasterData();
            string cityname, statename, zipcode;
            cityname = textInfo.ToTitleCase(txtmetrocityname.Text.Trim());
            statename = txtstate.Text;
            zipcode = "";
            int countryid = 0, stateid;
            countryid = Convert.ToInt32(ddlcountry.SelectedItem.Value);
            Nullable<int> timezoneid = null;
            timezoneid = (ddltimezone.SelectedIndex > 0) ? Convert.ToInt32(ddltimezone.SelectedItem.Value) : timezoneid;
            stateid = Convert.ToInt32(ddlstate.SelectedItem.Value);
            int newcityid = objmst.CityDetails_Insert(cityname, stateid, statename, countryid, zipcode, timezoneid);
            hdnmetrocityid.Value = newcityid.ToString();
        }
        public void SaveData()
        {
            char[] chDlr = { '$', ',', ' ', '%' };
            if (hdnmetrocityid.Value == hdnnearbycityid.Value && hdnStateID.Value == hdnstateidnc.Value && hdncountry.Value == hdncountryidnc.Value)
            {
                lblerrmsg.Text = "Metro city and near by metro city should not be same!";
                return;
            }
            MasterData edl = new MasterData();
            List<string> CountryNames = edl.searchcity(txtmetrocityname.Text, "Y");
            if (CountryNames.Count != 0 && hdnmetrocityid.Value == "0")
            {
                return;
            }
            if (CountryNames.Count == 0 || (hdncountry.Value != ddlcountry.SelectedItem.Value || hdnStateID.Value != ddlstate.SelectedItem.Value))
            {
                SaveNewCity();
            }
            Panel pnlfooter = Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("pnlfooter") as Panel;
            if (pnlfooter.Visible == true)
            {
                DropDownList ddlcitynearby = Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("ddlcitynearby") as DropDownList;
                if (ddlcitynearby.SelectedIndex > 0) { SaveTempnearbymetro(); }
            }
            MetroData objmet = new MetroData();
            Int32 Metrocityid = 0, Nearbycityid = 0, countryid = 0, stateid = 0;
            Nullable<decimal> Metrotax = null;
            string statename = "";
            DataTable dt = (DataTable)ViewState["temptable"];
            Metrocityid = Convert.ToInt32(hdnmetrocityid.Value);
            countryid = Convert.ToInt32(ddlcountry.SelectedItem.Value);
            stateid = Convert.ToInt32(ddlstate.SelectedItem.Value);
            statename = txtstate.Text.Trim();
            Nullable<int> timezoneid = null;
            timezoneid = (ddltimezone.SelectedIndex > 0) ? Convert.ToInt32(ddltimezone.SelectedItem.Value) : timezoneid;
            Metrotax = (txttax.Text == "") ? Metrotax : Convert.ToDecimal(txttax.Text.Trim(chDlr));
            string type = "D";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Nearbycityid = Convert.ToInt32(dt.Rows[i]["cityid"]);
                    if (dt.Rows[i]["metro_id"].ToString() == "0")
                    {
                        objmet.Metro_Insert(Metrocityid, Nearbycityid, Metrotax, type, countryid, stateid, statename, timezoneid);
                        type = "I";
                    }
                }
            }
            else
            {
                objmet.Metro_Insert(Metrocityid, Nearbycityid, Metrotax, type, countryid, stateid, statename, timezoneid);
            }
            if (string.IsNullOrEmpty(Request.QueryString["mertorcityid"]))
            {
                if (Convert.ToString(ViewState["status"]) == "")
                    Response.Redirect("~/metro.aspx?mertorcityid=" + Metrocityid.ToString() + "&type=I");
            }
            else
            {
                divmsg.InnerHtml = "Record updated successfully!";
            }

        }
        public void DeleteData(string flag)
        {
            flag = (flag.ToLower() == "y") ? flag : "d";
            MetroData objmetro = new MetroData();
            Int32 metrocityid = Convert.ToInt32(hdnmetrocityid.Value);
            string msg = objmetro.Metro_Delete(metrocityid, flag);
            if (msg == "")
            {
                divmsg.InnerHtml = (flag.ToLower() == "d") ? "Record deleted successfully!" : "Record undeleted successfully!";
                (this.Master as Site1).show_control(flag, pnlmetro);
            }
            else
            {
                lblerrmsg.Text = msg;
            }


        }
        protected void txtcitynearby_TextChanged(object sender, EventArgs e)
        {
            TextBox txtcitynearby = (TextBox)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("txtcitynearby");
            TextBox txtstatenc = (TextBox)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("txtstatenc");
            TextBox txtZipCodenc = (TextBox)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("txtZipCodenc");
            Label lblnearbystate = (Label)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("lblnearbystate");
            Label lblcountrync = (Label)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("lblcountrync");
            Label lblzipcodenc = (Label)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("lblzipcodenc");
            DropDownList ddlstatenc = (DropDownList)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("ddlstatenc");
            DropDownList ddlcountrync = (DropDownList)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("ddlcountrync");
            MasterData edl = new MasterData();
            List<string> CountryNames = edl.searchcity(txtcitynearby.Text.Trim(), "Y");
            bool flg = false;
            if (CountryNames.Count == 0)
            {
                flg = true;
                hdnnearbycityid.Value = "0";
                hdncountryidnc.Value = "0";
                hdnstateidnc.Value = "0";
            }
            else
            {
                MasterData mdl = new MasterData();
                DataTable dt1 = mdl.Getcitydetails(Convert.ToInt32(hdnnearbycityid.Value));
                lblnearbystate.Text = dt1.Rows[0]["STATE_NAME"].ToString();
                lblcountrync.Text = dt1.Rows[0]["Country_NAME"].ToString();
                txtcitynearby.Text = dt1.Rows[0]["City_name"].ToString();
                lblzipcodenc.Text = dt1.Rows[0]["Zip"].ToString();
                hdncountryidnc.Value = dt1.Rows[0]["Country_ID"].ToString();
                hdnstateidnc.Value = dt1.Rows[0]["State_ID"].ToString();
                txtstatenc.Visible = flg;
                ddlstatenc.ClearSelection();
                ddlcountrync.ClearSelection();
            }
            ddlcountrync.Visible = flg;
            ddlstatenc.Visible = flg;
            lblnearbystate.Visible = !flg;
            lblcountrync.Visible = !flg;
            lblzipcodenc.Visible = !flg;
            txtZipCodenc.Visible = flg;

        }
        protected void txtmetrocityname_TextChanged(object sender, EventArgs e)
        {
            createtemptable();
            MasterData objmst = new MasterData();
            List<string> CountryNames = objmst.searchcity(txtmetrocityname.Text.Split('/').GetValue(0).ToString().Trim(), "Y");
            if (CountryNames.Count == 0)
            {
                hdnmetrocityid.Value = "0";
            }
            if (hdnmetrocityid.Value != "0" && hdnmetrocityid.Value != "")
            {
                loadmetrodetails();

            }
            else
            {
                clearall();
            }
            showhidefooter(false);
            ddltimezone.Focus();
        }
        public void loadmetrodetails()
        {
            try
            {
                MasterData objmst = new MasterData();
                MetroData mdl = new MetroData();
                DataSet ds = mdl.GetMetroDetails(Convert.ToInt32(hdnmetrocityid.Value));
                DataTable dt1 = ds.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    txtmetrocityname.Text = dt1.Rows[0]["City_name"].ToString();
                    ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByValue(dt1.Rows[0]["STATE_ID"].ToString()));
                    ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(dt1.Rows[0]["Country_ID"].ToString()));
                    ddltimezone.SelectedIndex = ddltimezone.Items.IndexOf(ddltimezone.Items.FindByValue(dt1.Rows[0]["TIMEZONE_ID"].ToString()));
                    txttax.Text = dt1.Rows[0]["METRO_TAX"].ToString();
                    txtzipcode.Text = dt1.Rows[0]["Zip"].ToString().Trim();
                    txtstate.Visible = false;
                    ddlstate.Visible = true;
                    hdncountry.Value = ddlcountry.SelectedItem.Value;
                    hdnStateID.Value = ddlstate.SelectedItem.Value;
                    string active_flag = dt1.Rows[0]["metro_flag"].ToString();
                    if (active_flag.ToLower() == "y")
                    {
                        ImageButton imgbtndelete = (ImageButton)this.Master.FindControl("imgbtndelete");
                        imgbtndelete.Visible = true;
                    }
                    if (active_flag.ToLower() == "d")
                    {
                        (this.Master as Site1).show_control("n", pnlmetro);
                    }
                }
                DataTable dt = new DataTable();
                dt = ds.Tables[1];
                DataColumn dc = new DataColumn();
                dc.ColumnName = "tempid";
                dc.DataType = typeof(int);
                dc.AutoIncrement = true;
                dc.AutoIncrementSeed = 1;
                dt.Columns.Add(dc);
                dt.Columns["metro_id"].DefaultValue = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["tempid"] = i + 1;
                }

                ViewState["temptable"] = dt;
                dt = new DataTable();
                dt = ds.Tables[2];
                Repvenue.DataSource = dt;
                Repvenue.DataBind();
                BindNearyByMetro();
                if (!string.IsNullOrEmpty(Request.QueryString["mertorcityid"]))
                {
                    showhidefooter(false);
                }
            }
            catch (Exception ex)
            {
                lblerrmsg.Text = "Error: " + ex.Message.ToString();
                lblerrmsg.ForeColor = System.Drawing.Color.Red;
            }

        }
        public void showhidefooter(bool flg)
        {
            Panel pnlfooter = Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("pnlfooter") as Panel;
            pnlfooter.Visible = flg;
        }
        public void clearall()
        {
            txtstate.Text = string.Empty;
            ddlcountry.ClearSelection();
            ddltimezone.ClearSelection();
            ddlstate.ClearSelection();
            txtzipcode.Text = string.Empty;
            hdncountry.Value = "0";
            hdnStateID.Value = "0";
        }
        protected void lnknewvenue_Click(object sender, EventArgs e)
        {
            lnknewvenue.CommandArgument = "1";
            ViewState["status"] = "movetovenue";
            SaveData();
        }
        public void setstate()
        {
            if (ddlcountry.SelectedItem.Text == "USA" || ddlcountry.SelectedIndex == 0)
            {
                txtstate.Visible = false;
                ddlstate.Visible = true;
                rfv_txtstate.Enabled = false;
            }
            else
            {
                txtstate.Visible = true;
                ddlstate.Visible = false;
                ddlstate.ClearSelection();
                rfv_txtstate.Enabled = true;
            }
        }

        protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtstate.Text = string.Empty;
            setstate();
        }

        protected void ddlcountrync_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlcountrync = (DropDownList)sender;
            DropDownList ddlstatenc = (DropDownList)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("ddlstatenc");
            TextBox txtstatenc = (TextBox)Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("txtstatenc");
            bool flg = (ddlcountrync.SelectedItem.Text == "USA") ? true : false;
            ddlstatenc.Visible = flg;
            if (!flg)
            {
                ddlstatenc.ClearSelection();
            }
            txtstatenc.Visible = !flg;

        }

        protected void Repnearbymetro_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                MasterData mdl = new MasterData();
                DataTable dt = new DataTable();
                CommonFunction.CommonFun objcf = new CommonFunction.CommonFun();
                DropDownList ddlcitynearby = e.Item.FindControl("ddlcitynearby") as DropDownList;

                dt = mdl.GetCityStates();
                objcf.FillDropDownList(ddlcitynearby, dt, "City_Name", "City_id");
                dt = mdl.Getstate();
                DropDownList ddlstatenc = e.Item.FindControl("ddlstatenc") as DropDownList;
                objcf.FillDropDownList(ddlstatenc, dt, "state_name", "state_id");
                MasterData objmst = new MasterData();
                dt = new DataTable();
                dt = objmst.Getcountry();
                DropDownList ddlcountrync = e.Item.FindControl("ddlcountrync") as DropDownList;
                objcf.FillDropDownList(ddlcountrync, dt, "Country_Name", "Country_ID");
            }
        }

        protected void lnkbtnAddmetro_Click(object sender, EventArgs e)
        {
            DropDownList ddlcitynearby = Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("ddlcitynearby") as DropDownList;
            if (ddlcitynearby.SelectedIndex > 0) { SaveTempnearbymetro(); }
            showhidefooter(true);
        }

        protected void lnkbtnDeletemetro_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["temptable"];
            foreach (RepeaterItem gr in Repnearbymetro.Items)
            {
                CheckBox chkdelete = (CheckBox)gr.FindControl("chkdelete");
                HiddenField hdntempid = (HiddenField)gr.FindControl("hdntempid");
                HiddenField hdnmetroid = (HiddenField)gr.FindControl("hdnmetroid");
                if (chkdelete.Checked == true)
                {
                    DataRow[] dr;
                    dr = dt.Select("tempid='" + hdntempid.Value + "'", "");
                    dt.Rows.Remove(dr[0]); ;
                    if (hdnmetroid.Value != "0")
                    {
                        MetroData objmet = new MetroData();
                        objmet.NearbyMetro_Delete(Convert.ToInt32(hdnmetroid.Value));
                    }
                }
            }
            BindNearyByMetro();
            showhidefooter(false);
        }
        public bool cityexist(Int32 nearbycityid)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["temptable"];
            if (dt.Select("cityid='" + nearbycityid + "'", "").Length > 0)
                return false;
            else
                return true;
        }
        protected void ddlcitynearby_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlcitynearby = Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("ddlcitynearby") as DropDownList;

            Label lblnearbystate = Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("lblnearbystate") as Label;
            Label lblcountrync = Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("lblcountrync") as Label;
            Label lblzipcodenc = Repnearbymetro.Controls[Repnearbymetro.Controls.Count - 1].FindControl("lblzipcodenc") as Label;
            DataTable dt = new DataTable();
            if (ddlcitynearby.SelectedItem.Value == hdnmetrocityid.Value)
            {
                lblerrmsg.Text = "Metro city and near by metro city should not be same!";
                ddlcitynearby.ClearSelection();
                return;
            }
            if (ddlcitynearby.SelectedIndex > 0)
            {
                if (cityexist(Convert.ToInt32(ddlcitynearby.SelectedItem.Value)) == false)
                {
                    lblerrmsg.Text = "Near by city already exists!";
                    ddlcitynearby.ClearSelection();
                    return;
                }
                hdnnearbycityid.Value = ddlcitynearby.SelectedItem.Value;
                MasterData mdl = new MasterData();
                dt = mdl.Getcitydetails(Convert.ToInt32(ddlcitynearby.SelectedItem.Value));
                lblcountrync.Text = dt.Rows[0]["COUNTRY_NAME"].ToString();
                lblnearbystate.Text = dt.Rows[0]["STATE_NAME"].ToString();
                lblzipcodenc.Text = dt.Rows[0]["Zip"].ToString();
            }
            else
            {
                lblcountrync.Text = string.Empty;
                lblnearbystate.Text = string.Empty;
                lblzipcodenc.Text = string.Empty;
            }
            ddlcitynearby.Focus();
        }

        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlstate.SelectedIndex > 0)
            {
                MasterData objmst = new MasterData();
                DataTable dt = new DataTable();
                dt = objmst.Getcountry_frmstate(Convert.ToInt32(ddlstate.SelectedItem.Value));
                ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByValue(dt.Rows[0]["Country_ID"].ToString()));
            }
            else
            {
                ddlcountry.ClearSelection();
            }
        }
        public void Reset() { }


    }
}