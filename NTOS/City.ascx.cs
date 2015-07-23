using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MasterDataLayer;
using CommonFunction;
namespace NTOS
{
    public partial class City : System.Web.UI.UserControl
    {
        MasterData objmsd = new MasterData();
        CommonFun objcf = new CommonFun();
        DataTable dt;
        protected string uniqueKey;
        protected void page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdncityid.Value = "0";
                filldropdown();
                if (this.Page.Title.Contains("Venue") == true)
                {
                    rfv_ddlcity.Enabled = true;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.uniqueKey = Guid.NewGuid().ToString("N");
            this.aceresidcity.OnClientItemSelected = "setcityID_" + this.ID + "";
            this.aceZipcode.OnClientItemSelected = "setcityID_" + this.ID + "";
            if (!Page.IsPostBack)
            {

            }
        }
        public void filldropdown()
        {
            fillcity();
            fillstate();
            fillcountry();
        }
        public void fillcity()
        {
            dt = new DataTable();
            dt = objmsd.GetCityStates();
            objcf.FillDropDownList(ddlcity, dt, "City_Name", "City_id");

        }
        protected void fillstate()
        {
            MasterData mdl = new MasterData();
            DataTable dt = new DataTable();
            dt = mdl.Getstate();
            ddlstate.DataSource = dt;
            ddlstate.DataValueField = "state_id";
            ddlstate.DataTextField = "state_name";
            ddlstate.DataBind();
            ListItem li = new ListItem("-Select-", "0");
            ddlstate.Items.Insert(0, li);

        }
        protected void fillcountry()
        {
            MasterData mdl = new MasterData();
            DataTable dt = new DataTable();
            dt = mdl.Getcountry();
            ddlcountry.DataSource = dt;
            ddlcountry.DataValueField = "country_id";
            ddlcountry.DataTextField = "country_name";
            ddlcountry.DataBind();
            ListItem li = new ListItem("-Select-", "0");
            ddlcountry.Items.Insert(0, li);

        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getcityname(string prefixText)
        {

            MasterData edl = new MasterData();
            List<string> CountryNames = edl.searchcity(prefixText, "Y");
            return CountryNames;

        }
        public static List<string> GetZipcode(string prefixText)
        {

            MasterData edl = new MasterData();
            List<string> Zipcodelist = edl.serachZipcode(prefixText);
            return Zipcodelist;

        }
        public void fillcitydetails(Int32 cityid, string streetname, string parent_zip)
        {
            txtaddress.Text = streetname.ToString();
            if (cityid != 0)
            {
                hdncityid.Value = cityid.ToString();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                MasterData mdl = new MasterData();
                dt = mdl.Getcitydetails(Convert.ToInt32(hdncityid.Value));
                if (dt.Rows.Count > 0)
                {
                    txtaddress.Text = streetname.ToString();
                    ddlcity.SelectedIndex = ddlcity.Items.IndexOf(ddlcity.Items.FindByValue(dt.Rows[0]["City_ID"].ToString()));
                    lblcountry.Text = dt.Rows[0]["COUNTRY_NAME"].ToString();
                    lblstate.Text = dt.Rows[0]["STATE_NAME"].ToString();
                    string[] zip = dt.Rows[0]["Zip"].ToString().Split(',');
                    dt1.Columns.Add("ZipCode", typeof(string));
                    dt1.Columns.Add("Zipid", typeof(Int32));
                    for (int i = 0; i < zip.Length; i++)
                    {
                        dt1.Rows.Add(zip[i],i);
                    }
                    objcf.FillDropDownList(ddlzip, dt1, "ZipCode", "Zipid");
                    if (parent_zip != "0")
                        ddlzip.SelectedIndex = ddlzip.Items.IndexOf(ddlzip.Items.FindByText(parent_zip));
                }
            }
            else
            {
                clearall();
            }
        }

        public void clearall()
        {
            ddlcountry.ClearSelection();
            ddlstate.ClearSelection();
            txtzipcode.Text = string.Empty;
            txtcity.Text = string.Empty;
            ddlcity.ClearSelection();
            lblstate.Text = string.Empty;
            lblcountry.Text = string.Empty;

        }
        protected void txtcity_TextChanged(object sender, EventArgs e)
        {
            MasterData edl = new MasterData();
            List<string> CityNames = edl.searchcity(txtcity.Text.Trim(), "Y");
            if (CityNames.Count == 0)
            {
                hdncityid.Value = "0";
                ddlcountry.ClearSelection();
                ddlstate.ClearSelection();
            }
            else if (!string.IsNullOrEmpty(hdncityid.Value))
            {
                fillcitydetails(Convert.ToInt32(hdncityid.Value), txtaddress.Text.Trim(), "0");
            }
            setstate();
            ddlstate.Focus();
            SetValidation();
        }
        public void setstate()
        {
            if (ddlcountry.SelectedItem.Text == "USA" || ddlcountry.SelectedIndex == 0)
            {
                txtstate.Visible = false;
                ddlstate.Visible = true;
            }
            else
            {
                ddlstate.ClearSelection();
                txtstate.Visible = true;
                ddlstate.Visible = false;
            }
        }
        protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
        {

            setstate();
            SetValidation();
        }

        public Nullable<Int32> InsertCitydetails()
        {
            Int32 newcityid = Convert.ToInt32(hdncityid.Value);
            if (!string.IsNullOrEmpty(txtcity.Text.Trim()))
            {
                string cityname, statename, zipcode, countryname;
                int countryid, stateid, cityid;
                cityid = Convert.ToInt16(hdncityid.Value);
                cityname = txtcity.Text.Trim();
                statename = txtstate.Text.Trim();
                zipcode = txtzipcode.Text.Trim();
                countryname = ddlcountry.SelectedItem.Text.Trim();
                stateid = Convert.ToInt32(ddlstate.SelectedItem.Value);
                countryid = Convert.ToInt32(ddlcountry.SelectedItem.Value);
                if (cityid == 0 || (statename.Trim() != hdnstate.Value.Trim() || countryname.Trim() != hdncountry.Value.Trim() || zipcode.Trim() != hdnzipcode.Value.Trim()))
                {
                    MasterData objmst = new MasterData();
                    newcityid = objmst.CityDetails_Insert(cityname, stateid, statename, countryid, zipcode, null);
                    hdncityid.Value = newcityid.ToString();
                }
            }
           // fillstate();
           // fillcitydetails(newcityid, txtaddress.Text, "0");
            if (newcityid == 0) { return null; } else { return newcityid; }
        }

        protected void txtzipcode_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdncityid.Value) && hdncityid.Value != "0")
            {
                fillcitydetails(Convert.ToInt32(hdncityid.Value), txtaddress.Text.Trim(), "0");
            }
            else
            {
                ddlcountry.ClearSelection();
                ddlstate.ClearSelection();
            }
            SetValidation();
        }
        public void SetValidation()
        {
            if (this.Page.Title.Contains("Venue") == false)
            {
                bool flg;
                flg = (!string.IsNullOrEmpty(txtaddress.Text.Trim())) ? true : false;
                rfv_ddlcity.Enabled = flg;
            }
        }
        protected void txtaddress_TextChanged(object sender, EventArgs e)
        {
            SetValidation();

        }

        protected void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdncityid.Value = ddlcity.SelectedItem.Value;
            if (ddlcity.SelectedIndex > 0)
            {
                if (this.Page.Title.Contains("Venue") == false)
                    rfvAddress.Enabled = true;
                fillcitydetails(Convert.ToInt32(ddlcity.SelectedItem.Value), txtaddress.Text, "0");
            }
            else
            {
                rfvAddress.Enabled = false;
                lblcountry.Text = string.Empty;
                lblstate.Text = string.Empty;
                lblzipcode.Text = string.Empty;
            }
            ddlcity.Focus();
        }
        public string Get_child_zipcode()
        {
            string zip = "";
            zip = (ddlzip.SelectedIndex > 0) ? ddlzip.SelectedItem.Text : zip;
            return zip;

        }
    }
}