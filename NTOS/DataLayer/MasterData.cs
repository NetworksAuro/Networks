using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using NTOS.DataLayer;
using System.Web.Caching;

namespace MasterDataLayer
{
    public class MasterData
    {
        SqlConnection dbconn = new SqlConnection(DBConnectionLayer.DBConnection.GetDBConnection());


        public DataTable GetMiscNames(int type, string name)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetMiscName", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Type", type);

            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                HttpContext.Current.Session["Sdt"] = dt;
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }

        public List<string> searchcountry(string searchtext, string activeflag)
        {

            DataTable dt = new DataTable();
            List<string> engagements = new List<string>();
            SqlCommand cmd = new SqlCommand("spCountryNameSearch", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Country_NAME", searchtext);
            cmd.Parameters.AddWithValue("@ActiveFlag", activeflag);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                List<string> CountryNames = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CountryNames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                }
                return CountryNames;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }








        public List<string> searchcity(string searchtext, string activeflag)
        {

            DataTable dt = new DataTable();
            List<string> engagements = new List<string>();
            SqlCommand cmd = new SqlCommand("spCityNameSearch", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CITY_NAME", searchtext);
            cmd.Parameters.AddWithValue("@ActiveFlag", activeflag);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                List<string> CountryNames = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CountryNames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                }
                return CountryNames;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }
        public List<string> SearchMetrocity(string searchtext)
        {
            DataTable dt = new DataTable();
            List<string> engagements = new List<string>();
            SqlCommand cmd = new SqlCommand("spMetroCityNameSearch", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CITY_NAME", searchtext);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                List<string> CountryNames = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CountryNames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                }
                return CountryNames;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }
        public List<string> SearchEngagementStatus(string searchtext)
        {
            DataTable dt = new DataTable();
            List<string> engagements = new List<string>();
            SqlCommand cmd = new SqlCommand("spEngagementStatusSearch", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Status", searchtext);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                List<string> StatusNames = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StatusNames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                }
                return StatusNames;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }

        public DataTable GetEVPDetails(int id, int type)
        {
            string cachekey = "EVP"+id+type;
            if (HttpContext.Current.Cache[cachekey] != null)
            {
                DataTable cacheds = HttpContext.Current.Cache[cachekey] as DataTable;
                return cacheds;
            }
            else
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("spGetEVPDetails", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Type", type);
                SqlCacheDependency dependency = new SqlCacheDependency(cmd);
                try
                {
                    if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    HttpContext.Current.Cache.Insert(cachekey, dt, dependency);
                    return dt;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    HttpContext.Current.Session["Sdt"] = dt;
                    if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
                }
            }
        }




        public DataTable GetHistoryForAll(string userid, int screenid)
        {

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("sp_GetHistoryAll", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Userid", userid);
            cmd.Parameters.AddWithValue("@ScreenId", screenid);

            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                HttpContext.Current.Session["Sdt"] = dt;
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

        }



        public List<string> SearchState(string statename)
        {
            DataTable dt = new DataTable();
            List<string> engagements = new List<string>();
            SqlCommand cmd = new SqlCommand("spGetState", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@StateName", statename);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                List<string> CountryNames = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CountryNames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["state_name"].ToString(), dt.Rows[i]["state_id"].ToString()));
                }
                return CountryNames;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }
        public List<string> serachZipcode(string searchtext)
        {
            DataTable dt = new DataTable();
            List<string> engagements = new List<string>();
            SqlCommand cmd = new SqlCommand("spZipCodeSearch", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ZIPCODE", searchtext);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                List<string> CountryNames = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CountryNames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["city_zip"].ToString(), dt.Rows[i]["CITY_ID"].ToString()));
                }
                return CountryNames;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }
        public DataTable Getcountry(string activeflag)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetCountry", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Activeflag", activeflag);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }
        public DataTable Getcountry()
        {


            string cachekey = "country";
            if (HttpContext.Current.Cache[cachekey] != null)
            {
                DataTable cacheds = HttpContext.Current.Cache[cachekey] as DataTable;
                return cacheds;
            }
            else
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("spGetCountry", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCacheDependency dependency = new SqlCacheDependency(cmd);
                try
                {
                    if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    HttpContext.Current.Cache.Insert(cachekey, dt, dependency);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
                }
            }
        }
        public DataTable Getstate()
        {
            string cachekey = "state";
            if (HttpContext.Current.Cache[cachekey] != null)
            {
                DataTable cacheds = HttpContext.Current.Cache[cachekey] as DataTable;
                return cacheds;
            }
            else
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("spGetState", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCacheDependency dependency = new SqlCacheDependency(cmd);
                try
                {
                    if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    HttpContext.Current.Cache.Insert(cachekey, dt, dependency);
                    return dt;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
                }
            }
        }
        public DataTable Getcitydetails(Int32 cityid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetCityDetails", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CITY_ID", cityid);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

        }
        public DataTable Getcitydetails(Int32 cityid, string cityname)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetCityDetails", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CITY_ID", cityid);
            cmd.Parameters.AddWithValue("@CityName", cityname);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

        }
        public DataTable Gettitle()
        {
            string cachekey = "title";
            if (HttpContext.Current.Cache[cachekey] != null)
            {
                DataTable cacheds = HttpContext.Current.Cache[cachekey] as DataTable;
                return cacheds;
            }
            else
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("spGetTitle", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCacheDependency dependency = new SqlCacheDependency(cmd);
                try
                {
                    if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    HttpContext.Current.Cache.Insert(cachekey, dt, dependency);
                    return dt;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
                }
            }

        }
        public DataTable Getshows(string active_flg)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spShownamesearch", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@SHOW_NAME", "");
                cmd.Parameters.AddWithValue("@ActiveFlag", active_flg);
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }
        public DataTable GetTimezone(string activeflag)
        {
            string cachekey = "timezone";
            if (HttpContext.Current.Cache[cachekey] != null)
            {
                DataTable cacheds = HttpContext.Current.Cache[cachekey] as DataTable;
                return cacheds;
            }
            else
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("spGetTimeZone", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TIMEZONE_ACTIVE_FLAG", activeflag);
                SqlCacheDependency dependency = new SqlCacheDependency(cmd);
                try
                {
                    if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    HttpContext.Current.Cache.Insert(cachekey, dt, dependency);
                    return dt;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
                }
            }
        }
        public DataTable GetMetroVenueList(Int32 metrocityid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetMetroVenueList", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CITY_ID", metrocityid);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }
        public void Docx_Insert(string tablename, Int32 tableid, string filename, string filepath)
        {
            SqlCommand cmd = new SqlCommand("spDocxInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@DOCX_TBL_NM", tablename);
            cmd.Parameters.AddWithValue("@DOCX_TBL_ID", tableid);
            cmd.Parameters.AddWithValue("@DOCX_FILE_NM", filename);
            cmd.Parameters.AddWithValue("@DOCX_FILE_PATH", filepath);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }


        public VenuePresenter GetSearchAllHistory(int mode, string userid)
        {
            DataTable dt = new DataTable();
            VenuePresenter vp = null;
            SqlCommand cmd = new SqlCommand("spGetHistoryAll", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Mode", mode);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        vp = new VenuePresenter();
                        vp.VenuePresenterName = Convert.ToString(dt.Rows[0][0]);
                        vp.Country = Convert.ToString(dt.Rows[0][3]);
                        vp.FirstName = Convert.ToString(dt.Rows[0][5]);
                        vp.LastName = Convert.ToString(dt.Rows[0][4]);
                        vp.City = Convert.ToString(dt.Rows[0][1]);
                        vp.ZipCode = Convert.ToString(dt.Rows[0][2]);
                        vp.capacity = Convert.ToString(dt.Rows[0][6]);
                        return vp;
                    }
                }


                return vp;
            }

            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }





        public void SearchAllHistoryUpdate(int type, int mode, string userid, VenuePresenter vp)
        {
            SqlCommand cmd = new SqlCommand("spInsertHistoryAll", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@Mode", mode);
            cmd.Parameters.AddWithValue("@Presenter", vp.VenuePresenterName);
            cmd.Parameters.AddWithValue("@City", vp.City);
            cmd.Parameters.AddWithValue("@ZipCode", vp.ZipCode);
            cmd.Parameters.AddWithValue("@Country", vp.Country);
            cmd.Parameters.AddWithValue("@FirstName", vp.FirstName);
            cmd.Parameters.AddWithValue("@LastName", vp.LastName);
            cmd.Parameters.AddWithValue("@Capacity", vp.capacity);
            cmd.Parameters.AddWithValue("@UserID", userid);

            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }
        }

        public ShowDetail GetSearchShowHistory(int mode, string userid)
        {
            DataTable dt = new DataTable();
            ShowDetail shw = null;
            SqlCommand cmd = new SqlCommand("spGetHistoryAll", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Mode", mode);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        shw = new ShowDetail();
                        shw.CompanyManager = Convert.ToString(dt.Rows[0][3]);
                        shw.CorporateName = Convert.ToString(dt.Rows[0][2]);
                        shw.OverheadNut = Convert.ToString(dt.Rows[0][4]);
                        shw.Show = Convert.ToString(dt.Rows[0][0]);
                        shw.ShowBegindate = Convert.ToString(dt.Rows[0][1]);
                        shw.VariableRoylaties = Convert.ToString(dt.Rows[0][5]);
                        shw.WeeklyOperatingExpense = Convert.ToString(dt.Rows[0][6]);
                        return shw;
                    }
                }


                return shw;
            }

            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }



        public Schedule GetScheduleSearchHistory(int mode, string userid)
        {
            DataTable dt = new DataTable();
            Schedule sch = null;
            SqlCommand cmd = new SqlCommand("spGetSearchHistory", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Mode", mode);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        sch = new Schedule();
                        sch.Show = Convert.ToString(dt.Rows[0][0]);
                        sch.SettlementDate = Convert.ToString(dt.Rows[0][4]);
                        sch.Presenter = Convert.ToString(dt.Rows[0][1]);
                        sch.City = Convert.ToString(dt.Rows[0][3]);
                        sch.Status = Convert.ToString(dt.Rows[0][2]);
                        sch.Venue = Convert.ToString(dt.Rows[0][5]);

                        return sch;
                    }
                }


                return sch;
            }

            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }

        public PriceScaleDetail GetPriceScaleSearchHistory(int mode, string userid)
        {
            DataTable dt = new DataTable();
            PriceScaleDetail psc = null;
            SqlCommand cmd = new SqlCommand("spGetSearchHistory", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Mode", mode);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        psc = new PriceScaleDetail();
                        psc.Seats = Convert.ToString(dt.Rows[0][0]);
                        psc.Single = Convert.ToString(dt.Rows[0][2]);
                        psc.Group = Convert.ToString(dt.Rows[0][1]);
                        psc.Subscription = Convert.ToString(dt.Rows[0][3]);


                        return psc;
                    }
                }


                return psc;
            }

            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }


        public Discount GetDiscountSearchHistory(int mode, string userid)
        {
            DataTable dt = new DataTable();
            Discount d = null;
            SqlCommand cmd = new SqlCommand("spGetSearchHistory", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Mode", mode);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        d = new Discount();
                        d.Sub = Convert.ToString(dt.Rows[0][0]);
                        d.SubTkt = Convert.ToString(dt.Rows[0][1]);
                        d.Group = Convert.ToString(dt.Rows[0][2]);
                        d.Grptkt = Convert.ToString(dt.Rows[0][3]);
                        d.Misc = Convert.ToString(dt.Rows[0][4]);
                        d.MiscTkt= Convert.ToString(dt.Rows[0][5]);

                        return d;
                    }
                }


                return d;
            }

            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }





        public BoxOfficeDetail GetBoxOfficeSearchHistory(int mode, string userid)
        {
            DataTable dt = new DataTable();
            BoxOfficeDetail bd = null;
            SqlCommand cmd = new SqlCommand("spGetSearchHistory", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Mode", mode);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        bd = new BoxOfficeDetail();
                        bd = new BoxOfficeDetail();

                        bd.GroupSales = Convert.ToString(dt.Rows[0][0]);
                        bd.DropCount = Convert.ToString(dt.Rows[0][1]);
                        bd.PaidAttendance = Convert.ToString(dt.Rows[0][2]);
                        bd.Comp = Convert.ToString(dt.Rows[0][3]);
                        bd.TSSubscription = Convert.ToString(dt.Rows[0][4]);
                        bd.TSPhone = Convert.ToString(dt.Rows[0][5]);
                        bd.TSInternet = Convert.ToString(dt.Rows[0][6]);
                        bd.TSCreditCard = Convert.ToString(dt.Rows[0][7]);
                        bd.TSRemoteOutlet = Convert.ToString(dt.Rows[0][8]);
                        bd.TSSingleTickets = Convert.ToString(dt.Rows[0][9]);
                        bd.TSGroup1 = Convert.ToString(dt.Rows[0][10]);
                        bd.TSGroup2 = Convert.ToString(dt.Rows[0][11]);
                        bd.GRSubscription = Convert.ToString(dt.Rows[0][12]);
                        bd.GRPhone = Convert.ToString(dt.Rows[0][13]);
                        bd.GRInternet = Convert.ToString(dt.Rows[0][14]);
                        bd.GRCreditCard = Convert.ToString(dt.Rows[0][15]);
                        bd.GRRemoteOutlet = Convert.ToString(dt.Rows[0][16]);
                        bd.GRSingleTickets = Convert.ToString(dt.Rows[0][17]);
                        bd.GRGroup1 = Convert.ToString(dt.Rows[0][18]);
                        bd.GRGroup2 = Convert.ToString(dt.Rows[0][19]);


                        return bd;
                    }
                }


                return bd;
            }

            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

        }



        public DealDetail GetDealDetailSearchHistory(int mode, string userid)
        {
            DataTable dt = new DataTable();
            DealDetail dd = null;
            SqlCommand cmd = new SqlCommand("spGetSearchHistory", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Mode", mode);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dd = new DealDetail();
                        dd.Royalty = Convert.ToString(dt.Rows[0][0]);
                        dd.Company = Convert.ToString(dt.Rows[0][1]);
                        dd.Guarantee = Convert.ToString(dt.Rows[0][2]);
                        dd.Presenter = Convert.ToString(dt.Rows[0][3]);
                        dd.Cap = Convert.ToString(dt.Rows[0][4]);
                        dd.Tax1 = Convert.ToString(dt.Rows[0][5]);
                        dd.OnEachTicket = Convert.ToString(dt.Rows[0][6]);
                        dd.Tax2 = Convert.ToString(dt.Rows[0][7]);
                        dd.TaxFacilityFee = Convert.ToString(dt.Rows[0][8]);
                        dd.TaxAmountOver = Convert.ToString(dt.Rows[0][9]);
                        dd.Producer = Convert.ToString(dt.Rows[0][10]);
                        dd.Other1 = Convert.ToString(dt.Rows[0][11]);
                        //dd.Presenter = Convert.ToString(dt.Rows[0][12]);
                        dd.Other2 = Convert.ToString(dt.Rows[0][12]);
                        dd.StarRoyalty = Convert.ToString(dt.Rows[0][13]);
                        dd.Budget = Convert.ToString(dt.Rows[0][14]);
                        dd.Actual = Convert.ToString(dt.Rows[0][15]);
                        dd.Subscription = Convert.ToString(dt.Rows[0][16]);
                        dd.Phone = Convert.ToString(dt.Rows[0][17]);
                        dd.Internet = Convert.ToString(dt.Rows[0][18]);
                        dd.CreditCard = Convert.ToString(dt.Rows[0][19]);
                        dd.Remote = Convert.ToString(dt.Rows[0][20]);
                        dd.SingleTickets = Convert.ToString(dt.Rows[0][21]);
                        dd.Group1 = Convert.ToString(dt.Rows[0][22]);
                        dd.Group2 = Convert.ToString(dt.Rows[0][23]);

                        return dd;
                    }
                }


                return dd;
            }

            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }

        public ExpenseDetails GetExpenseDetailSearchHistory(int mode, string userid)
        {
            DataTable dt = new DataTable();
            ExpenseDetails ed = null;
            SqlCommand cmd = new SqlCommand("spGetSearchHistory", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Mode", mode);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ed = new ExpenseDetails();



                        return ed;
                    }
                }


                return ed;
            }

            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }

        public void SearchEngagementExpenseHistoryUpdate(string userid, ExpenseDetails ed)
        {
            SqlCommand cmd = new SqlCommand("sp_GetSearchExpenseDetails", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.Parameters.AddWithValue("@Arg1 ", ed.BUDAdvertisingGross);
            cmd.Parameters.AddWithValue("@Arg2 ", ed.BUDLaborCatering);
            cmd.Parameters.AddWithValue("@Arg3 ", ed.BUDMusicians);
            cmd.Parameters.AddWithValue("@Arg4 ", ed.BUDshLoadIn);
            cmd.Parameters.AddWithValue("@Arg5 ", ed.BUDshLoadOut);
            cmd.Parameters.AddWithValue("@Arg6 ", ed.BUDshRunning);
            cmd.Parameters.AddWithValue("@Arg7 ", ed.BUDwrLoadIn);
            cmd.Parameters.AddWithValue("@Arg8 ", ed.BUDwrLoadOut);
            cmd.Parameters.AddWithValue("@Arg9 ", ed.BUDwrRunning);
            cmd.Parameters.AddWithValue("@Arg10 ", ed.BUDInsuranceOnDropCount);
            cmd.Parameters.AddWithValue("@Arg11 ", ed.BUDTicketPrinting);
            cmd.Parameters.AddWithValue("@Arg12 ", ed.BUDDOCOther1);
            cmd.Parameters.AddWithValue("@Arg13 ", ed.BUDDOCOther2);
            cmd.Parameters.AddWithValue("@Arg14 ", ed.BUD);
            cmd.Parameters.AddWithValue("@Arg15 ", ed.BUDADAExpense);
            cmd.Parameters.AddWithValue("@Arg16 ", ed.BUDBoxOffice);
            cmd.Parameters.AddWithValue("@Arg17 ", ed.BUDCatering);
            cmd.Parameters.AddWithValue("@Arg18 ", ed.BUDEquipmentRental);
            cmd.Parameters.AddWithValue("@Arg19 ", ed.BUDGroupSalesExpenses);
            cmd.Parameters.AddWithValue("@Arg20 ", ed.BUDHouseStaff);
            cmd.Parameters.AddWithValue("@Arg21 ", ed.BUDLeagueFees);
            cmd.Parameters.AddWithValue("@Arg22 ", ed.BUDLicensesPermits);
            cmd.Parameters.AddWithValue("@Arg23 ", ed.BUDLimosAuto);
            cmd.Parameters.AddWithValue("@Arg24 ", ed.BUDOrchestraShellRemoval);
            cmd.Parameters.AddWithValue("@Arg25 ", ed.BUDPresenterProfit);
            cmd.Parameters.AddWithValue("@Arg26 ", ed.BUDPoliceSecurityFireMarshall);
            cmd.Parameters.AddWithValue("@Arg27 ", ed.BUDProgram);
            cmd.Parameters.AddWithValue("@Arg28 ", ed.BUDRent);
            cmd.Parameters.AddWithValue("@Arg29 ", ed.BUDSoundLights);

            cmd.Parameters.AddWithValue("@Arg31 ", ed.BUDTelephonesInternet);
            cmd.Parameters.AddWithValue("@Arg32 ", ed.BUDDryIceC02);
            cmd.Parameters.AddWithValue("@Arg33 ", ed.BUDLOCOther1);
            cmd.Parameters.AddWithValue("@Arg34 ", ed.BUDLOCOther2);
            cmd.Parameters.AddWithValue("@Arg35 ", ed.BUDLOCOther3);
            cmd.Parameters.AddWithValue("@Arg36 ", ed.BUDLOCOther4);
            cmd.Parameters.AddWithValue("@Arg37 ", ed.BUDLOCOther5);
            cmd.Parameters.AddWithValue("@Arg38 ", ed.BUDLocalFixed);
            cmd.Parameters.AddWithValue("@Arg39 ", ed.ACTAdvertisingGross);
            cmd.Parameters.AddWithValue("@Arg40 ", ed.ACTLaborCatering);
            cmd.Parameters.AddWithValue("@Arg41 ", ed.ACTMusicians);
            cmd.Parameters.AddWithValue("@Arg42 ", ed.ACTshLoadIn);
            cmd.Parameters.AddWithValue("@Arg43 ", ed.ACTshLoadOut);
            cmd.Parameters.AddWithValue("@Arg44 ", ed.ACTshRunning);
            cmd.Parameters.AddWithValue("@Arg45 ", ed.ACTwrLoadIn);
            cmd.Parameters.AddWithValue("@Arg46 ", ed.ACTwrLoadOut);
            cmd.Parameters.AddWithValue("@Arg47 ", ed.ACTwrRunning);
            cmd.Parameters.AddWithValue("@Arg48 ", ed.ACTInsuranceOnDropCount);
            cmd.Parameters.AddWithValue("@Arg49 ", ed.ACTTicketPrinting);
            cmd.Parameters.AddWithValue("@Arg50 ", ed.ACTDOCOther1);
            cmd.Parameters.AddWithValue("@Arg51 ", ed.ACTDOCOther2);
            cmd.Parameters.AddWithValue("@Arg52 ", ed.ACT);
            cmd.Parameters.AddWithValue("@Arg53 ", ed.ACTADAExpense);
            cmd.Parameters.AddWithValue("@Arg54 ", ed.ACTBoxOffice);
            cmd.Parameters.AddWithValue("@Arg55 ", ed.ACTCatering);
            cmd.Parameters.AddWithValue("@Arg56 ", ed.ACTEquipmentRental);
            cmd.Parameters.AddWithValue("@Arg57 ", ed.ACTGroupSalesExpenses);
            cmd.Parameters.AddWithValue("@Arg58 ", ed.ACTHouseStaff);
            cmd.Parameters.AddWithValue("@Arg59 ", ed.ACTLeagueFees);
            cmd.Parameters.AddWithValue("@Arg60 ", ed.ACTLicensesPermits);
            cmd.Parameters.AddWithValue("@Arg61 ", ed.ACTLimosAuto);
            cmd.Parameters.AddWithValue("@Arg62 ", ed.ACTOrchestraShellRemoval);
            cmd.Parameters.AddWithValue("@Arg63 ", ed.ACTPresenterProfit);
            cmd.Parameters.AddWithValue("@Arg64 ", ed.ACTPoliceSecurityFireMarshall);
            cmd.Parameters.AddWithValue("@Arg65 ", ed.ACTProgram);
            cmd.Parameters.AddWithValue("@Arg66 ", ed.ACTRent);
            cmd.Parameters.AddWithValue("@Arg67 ", ed.ACTSoundLights);

            cmd.Parameters.AddWithValue("@Arg69 ", ed.ACTTelephonesInternet);
            cmd.Parameters.AddWithValue("@Arg70 ", ed.ACTDryIceC02);
            cmd.Parameters.AddWithValue("@Arg71 ", ed.ACTLOCOther1);
            cmd.Parameters.AddWithValue("@Arg72 ", ed.ACTLOCOther2);
            cmd.Parameters.AddWithValue("@Arg73 ", ed.ACTLOCOther3);
            cmd.Parameters.AddWithValue("@Arg74 ", ed.ACTLOCOther4);
            cmd.Parameters.AddWithValue("@Arg75 ", ed.ACTLOCOther5);
            cmd.Parameters.AddWithValue("@Arg76 ", ed.ACTLocalFixed);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }
        }


        public void SearchHistoryUpdate(string userid, Schedule sch, PriceScaleDetail psc, BoxOfficeDetail bd, DealDetail dd,Discount d, int type)
        {
            SqlCommand cmd = new SqlCommand("spInsertSearchDetails", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            if (type == 1)
            {
                cmd.Parameters.AddWithValue("@Arg1", sch.Show);
                cmd.Parameters.AddWithValue("@Arg2", sch.Presenter);
                cmd.Parameters.AddWithValue("@Arg3", sch.Status);
                cmd.Parameters.AddWithValue("@Arg4", sch.City);
                cmd.Parameters.AddWithValue("@Arg5", sch.SettlementDate);
                cmd.Parameters.AddWithValue("@Arg6", sch.Venue);
                cmd.Parameters.AddWithValue("@Arg7", "");
                cmd.Parameters.AddWithValue("@Arg8", "");
                cmd.Parameters.AddWithValue("@Arg9", "");
                cmd.Parameters.AddWithValue("@Arg10", "");
                cmd.Parameters.AddWithValue("@Arg11", "");
                cmd.Parameters.AddWithValue("@Arg12", "");
                cmd.Parameters.AddWithValue("@Arg13", "");
                cmd.Parameters.AddWithValue("@Arg14", "");
                cmd.Parameters.AddWithValue("@Arg15", "");
                cmd.Parameters.AddWithValue("@Arg16", "");
                cmd.Parameters.AddWithValue("@Arg17", "");
                cmd.Parameters.AddWithValue("@Arg18", "");
                cmd.Parameters.AddWithValue("@Arg19", "");
                cmd.Parameters.AddWithValue("@Arg20", "");
                cmd.Parameters.AddWithValue("@Arg21", "");
                cmd.Parameters.AddWithValue("@Arg22", "");
                cmd.Parameters.AddWithValue("@Arg23", "");
                cmd.Parameters.AddWithValue("@Arg24", "");
                cmd.Parameters.AddWithValue("@Arg25", "");
            }
            else if (type == 2)
            {
                cmd.Parameters.AddWithValue("@Arg1", psc.Seats);
                cmd.Parameters.AddWithValue("@Arg2", psc.Single);
                cmd.Parameters.AddWithValue("@Arg3", psc.Group);
                cmd.Parameters.AddWithValue("@Arg4", psc.Subscription);
                cmd.Parameters.AddWithValue("@Arg5", "");
                cmd.Parameters.AddWithValue("@Arg6", "");
                cmd.Parameters.AddWithValue("@Arg7", "");
                cmd.Parameters.AddWithValue("@Arg8", "");
                cmd.Parameters.AddWithValue("@Arg9", "");
                cmd.Parameters.AddWithValue("@Arg10", "");
                cmd.Parameters.AddWithValue("@Arg11", "");
                cmd.Parameters.AddWithValue("@Arg12", "");
                cmd.Parameters.AddWithValue("@Arg13", "");
                cmd.Parameters.AddWithValue("@Arg14", "");
                cmd.Parameters.AddWithValue("@Arg15", "");
                cmd.Parameters.AddWithValue("@Arg16", "");
                cmd.Parameters.AddWithValue("@Arg17", "");
                cmd.Parameters.AddWithValue("@Arg18", "");
                cmd.Parameters.AddWithValue("@Arg19", "");
                cmd.Parameters.AddWithValue("@Arg20", "");
                cmd.Parameters.AddWithValue("@Arg21", "");
                cmd.Parameters.AddWithValue("@Arg22", "");
                cmd.Parameters.AddWithValue("@Arg23", "");
                cmd.Parameters.AddWithValue("@Arg24", "");
                cmd.Parameters.AddWithValue("@Arg25", "");

            }
            else if (type == 3)
            {
                cmd.Parameters.AddWithValue("@Arg1 ", bd.GroupSales);
                cmd.Parameters.AddWithValue("@Arg2 ", bd.DropCount);
                cmd.Parameters.AddWithValue("@Arg3 ", bd.PaidAttendance);
                cmd.Parameters.AddWithValue("@Arg4 ", bd.Comp);
                cmd.Parameters.AddWithValue("@Arg5 ", bd.TSSubscription);
                cmd.Parameters.AddWithValue("@Arg6 ", bd.TSPhone);
                cmd.Parameters.AddWithValue("@Arg7 ", bd.TSInternet);
                cmd.Parameters.AddWithValue("@Arg8 ", bd.TSCreditCard);
                cmd.Parameters.AddWithValue("@Arg9 ", bd.TSRemoteOutlet);
                cmd.Parameters.AddWithValue("@Arg10 ", bd.TSSingleTickets);
                cmd.Parameters.AddWithValue("@Arg11 ", bd.TSGroup1);
                cmd.Parameters.AddWithValue("@Arg12 ", bd.TSGroup2);
                cmd.Parameters.AddWithValue("@Arg13 ", bd.GRSubscription);
                cmd.Parameters.AddWithValue("@Arg14 ", bd.GRPhone);
                cmd.Parameters.AddWithValue("@Arg15 ", bd.GRInternet);
                cmd.Parameters.AddWithValue("@Arg16 ", bd.GRCreditCard);
                cmd.Parameters.AddWithValue("@Arg17 ", bd.GRRemoteOutlet);
                cmd.Parameters.AddWithValue("@Arg18 ", bd.GRSingleTickets);
                cmd.Parameters.AddWithValue("@Arg19 ", bd.GRGroup1);
                cmd.Parameters.AddWithValue("@Arg20 ", bd.GRGroup2);
            }
            else if (type == 4)
            {
                cmd.Parameters.AddWithValue("@Arg1 ", dd.Royalty);
                cmd.Parameters.AddWithValue("@Arg2 ", dd.Company);
                cmd.Parameters.AddWithValue("@Arg3 ", dd.Guarantee);
                cmd.Parameters.AddWithValue("@Arg4 ", dd.Presenter);
                cmd.Parameters.AddWithValue("@Arg5 ", dd.Cap);
                cmd.Parameters.AddWithValue("@Arg6 ", dd.Tax1);
                cmd.Parameters.AddWithValue("@Arg7 ", dd.OnEachTicket);
                cmd.Parameters.AddWithValue("@Arg8 ", dd.Tax2);
                cmd.Parameters.AddWithValue("@Arg9 ", dd.TaxFacilityFee);
                cmd.Parameters.AddWithValue("@Arg10 ", dd.TaxAmountOver);
                cmd.Parameters.AddWithValue("@Arg11 ", dd.Producer);
                cmd.Parameters.AddWithValue("@Arg12 ", dd.Other1);
                //cmd.Parameters.AddWithValue("@Arg13 ", dd.Presenter);
                cmd.Parameters.AddWithValue("@Arg14 ", dd.Other2);
                cmd.Parameters.AddWithValue("@Arg15 ", dd.StarRoyalty);
                cmd.Parameters.AddWithValue("@Arg16 ", dd.Budget);
                cmd.Parameters.AddWithValue("@Arg17 ", dd.Actual);
                cmd.Parameters.AddWithValue("@Arg18 ", dd.Subscription);
                cmd.Parameters.AddWithValue("@Arg19 ", dd.Phone);
                cmd.Parameters.AddWithValue("@Arg20 ", dd.Internet);
                cmd.Parameters.AddWithValue("@Arg21 ", dd.CreditCard);
                cmd.Parameters.AddWithValue("@Arg22 ", dd.Remote);
                cmd.Parameters.AddWithValue("@Arg23 ", dd.SingleTickets);
                cmd.Parameters.AddWithValue("@Arg24 ", dd.Group1);
                cmd.Parameters.AddWithValue("@Arg25 ", dd.Group2);
            }
            else if (type == 5)
            {

                cmd.Parameters.AddWithValue("@Arg1 ", d.Sub);
                cmd.Parameters.AddWithValue("@Arg2 ", d.SubTkt);
                cmd.Parameters.AddWithValue("@Arg3 ", d.Group);
                cmd.Parameters.AddWithValue("@Arg4 ", d.Grptkt);
                cmd.Parameters.AddWithValue("@Arg5 ", d.Misc);
                cmd.Parameters.AddWithValue("@Arg6 ", d.MiscTkt);
 
            }


            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@UserID", userid);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }
        }

        public void SearchShowHistoryUpdate(string userid, ShowDetail shw)
        {

            SqlCommand cmd = new SqlCommand("spInsertUpdateShowHistory", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.Parameters.AddWithValue("@CompanyM", shw.CompanyManager);
            cmd.Parameters.AddWithValue("@CorporateN", shw.CorporateName);
            cmd.Parameters.AddWithValue("@Show", shw.Show);
            cmd.Parameters.AddWithValue("@Showbegindate", shw.ShowBegindate);
            cmd.Parameters.AddWithValue("@VariableR", shw.VariableRoylaties);
            cmd.Parameters.AddWithValue("@WOE", shw.WeeklyOperatingExpense);
            cmd.Parameters.AddWithValue("@OverheadN", shw.OverheadNut);


            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }


        public void Engmt_update(Int32 engmtid, string pricescalstatus, string expensestatus, string dealdemo, Nullable<decimal> exchange, string contract, string flag)
        {
            SqlCommand cmd = new SqlCommand("spengmtchildupdate", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EngtID", engmtid);
            cmd.Parameters.AddWithValue("@pricescalstatus", pricescalstatus);
            cmd.Parameters.AddWithValue("@expensestatus", expensestatus);
            cmd.Parameters.AddWithValue("@DEAL_DEMO", dealdemo);
            cmd.Parameters.AddWithValue("@DEAL_EXCHANGE", exchange);
            cmd.Parameters.AddWithValue("@DEAL_CONTRACT", contract);
            cmd.Parameters.AddWithValue("@flag", flag);

            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }

        public void Docx_Delete(Int32 docxid)
        {
            SqlCommand cmd = new SqlCommand("spDocxDelete", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@DOCX_ID", docxid);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public void ContactPerson_Delete(Int32 ContactPersonID, string Type)
        {
            SqlCommand cmd = new SqlCommand("spContactPersonDelete", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ContactPersonID", ContactPersonID);
            cmd.Parameters.AddWithValue("@Type", Type);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public DataTable GetDocxDetails(Int32 tableid, string tablename)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetDocxDetails", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@TableID", tableid);
            cmd.Parameters.AddWithValue("@TableName", tablename);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }


        }
        public int CityDetails_Insert(string cityname, int stateid, string statename, int countryid, string zip, Nullable<int> timezoneid)
        {
            SqlCommand cmd = new SqlCommand("spCityDetailsInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CITY_NAME", cityname);
            cmd.Parameters.AddWithValue("@STATE_ID", stateid);
            cmd.Parameters.AddWithValue("@state_name", statename);
            cmd.Parameters.AddWithValue("@COUNTRYID", countryid);
            cmd.Parameters.AddWithValue("@TimeZone", timezoneid);
            cmd.Parameters.AddWithValue("@ZIP", zip);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                SqlParameter pvNewId = new SqlParameter();
                pvNewId.ParameterName = "@NewId";
                pvNewId.DbType = DbType.Int32;
                pvNewId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvNewId);
                int rows;
                rows = cmd.ExecuteNonQuery();
                Int32 newid = 0;
                newid = Convert.ToInt32(cmd.Parameters["@NewId"].Value);
                return newid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public DataTable GetVenues(string active_flag)
        {
            SqlCommand cmd = new SqlCommand("spgetvenuelist", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ActiveFlag", active_flag);
            DataTable dt = new DataTable();
            try
            {
                dbconn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

            return dt;
        }
        public DataTable GetPresenters(string active_flag)
        {
            SqlCommand cmd = new SqlCommand("spGetPresenterList", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ActiveFlag", active_flag);
            DataTable dt = new DataTable();
            try
            {
                dbconn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

            return dt;
        }
        public DataTable GetCityStates()
        {
            string cachekey = "CityStates" ;
            if (HttpContext.Current.Cache[cachekey] != null)
            {
                DataTable cacheds = HttpContext.Current.Cache[cachekey] as DataTable;
                return cacheds;
            }
            else
            {
                SqlCommand cmd = new SqlCommand("spGetMetroList", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCacheDependency dependency = new SqlCacheDependency(cmd);
                DataTable dt = new DataTable();
                try
                {
                    dbconn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    HttpContext.Current.Cache.Insert(cachekey, dt, dependency);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
                }

                return dt;
            }
        }
        public DataTable GetMetroCityStates(string active_flag)
        {
            string cachekey = "MetroCityStates";
            if (HttpContext.Current.Cache[cachekey] != null)
            {
                DataTable cacheds = HttpContext.Current.Cache[cachekey] as DataTable;
                return cacheds;
            }
            else
            {
                SqlCommand cmd = new SqlCommand("spGetMetroList", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActiveFlag", active_flag);
                SqlCacheDependency dependency = new SqlCacheDependency(cmd);
                DataTable dt = new DataTable();
                try
                {
                    dbconn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    HttpContext.Current.Cache.Insert(cachekey, dt, dependency);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
                }

                return dt;
            }
        }
        public DataTable GetLookupList(string lookupgroup)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGETLOOKUPLIST", dbconn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@LKUP_GROUP", lookupgroup);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }
        public DataTable LoadCities()
        {
            string cachekey = "City";
            if (HttpContext.Current.Cache[cachekey] != null)
            {
                DataTable cacheds = HttpContext.Current.Cache[cachekey] as DataTable;
                return cacheds;
            }
            else
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("spCityNameSearch", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCacheDependency dependency = new SqlCacheDependency(cmd);
                try
                {
                    cmd.Parameters.AddWithValue("@CITY_NAME", "");
                    cmd.Parameters.AddWithValue("@ActiveFlag", "Y");
                    if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    HttpContext.Current.Cache.Insert(cachekey, dt, dependency);
                    return dt;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
                }
            }

        }
        #region Master
        public int Country_Insert(string countryName)
        {
            SqlCommand cmd = new SqlCommand("spCountryDetailsInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@COUNTRY_NAME", countryName);
            cmd.Parameters.AddWithValue("@country_active_flag", "");
            cmd.Parameters.AddWithValue("@COUNTRY_ID", 0);
            cmd.Parameters.AddWithValue("@ACTION", 0);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int Country_Update(string countryName, int countryid, string country_active_flag)
        {
            SqlCommand cmd = new SqlCommand("spCountryDetailsInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@COUNTRY_NAME", countryName);
            cmd.Parameters.AddWithValue("@country_active_flag", country_active_flag);
            cmd.Parameters.AddWithValue("@COUNTRY_ID", countryid);
            cmd.Parameters.AddWithValue("@ACTION", 1);

            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int Country_Delete(int countryid)
        {
            SqlCommand cmd = new SqlCommand("spCountryDetailsInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@COUNTRY_NAME", "");
            cmd.Parameters.AddWithValue("@country_active_flag", "");
            cmd.Parameters.AddWithValue("@COUNTRY_ID", countryid);
            cmd.Parameters.AddWithValue("@ACTION", 2);
            //cmd.Parameters.AddWithValue("@OUTPUTMSG", 000);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int Country_Activate(int countryid)
        {
            SqlCommand cmd = new SqlCommand("spCountryDetailsInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@COUNTRY_NAME", "");
            cmd.Parameters.AddWithValue("@country_active_flag", "");
            cmd.Parameters.AddWithValue("@COUNTRY_ID", countryid);
            cmd.Parameters.AddWithValue("@ACTION", 3);
            //cmd.Parameters.AddWithValue("@OUTPUTMSG", 000);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int State_Insert(string StateName, int countryid)
        {
            SqlCommand cmd = new SqlCommand("spStateDetailsInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@STATE_NAME", StateName);
            cmd.Parameters.AddWithValue("@state_active_flag", "");
            cmd.Parameters.AddWithValue("@STATE_ID", 0);
            cmd.Parameters.AddWithValue("@COUNTRY_ID", countryid);
            cmd.Parameters.AddWithValue("@ACTION", 0);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int State_Delete(int stateid)
        {
            SqlCommand cmd = new SqlCommand("spStateDetailsInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@STATE_NAME", "");
            cmd.Parameters.AddWithValue("@state_active_flag", "");
            cmd.Parameters.AddWithValue("@STATE_ID", stateid);
            cmd.Parameters.AddWithValue("@COUNTRY_ID", 0);
            cmd.Parameters.AddWithValue("@ACTION", 2);
            //cmd.Parameters.AddWithValue("@OUTPUTMSG", 000);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int State_Activate(int stateid)
        {
            SqlCommand cmd = new SqlCommand("spStateDetailsInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@STATE_NAME", "");
            cmd.Parameters.AddWithValue("@state_active_flag", "");
            cmd.Parameters.AddWithValue("@STATE_ID", stateid);
            cmd.Parameters.AddWithValue("@COUNTRY_ID", 0);
            cmd.Parameters.AddWithValue("@ACTION", 3);
            //cmd.Parameters.AddWithValue("@OUTPUTMSG", 000);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int State_Update(string StateName, int Stateid, int countryid, string state_active_flag)
        {
            SqlCommand cmd = new SqlCommand("spStateDetailsInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@STATE_NAME", StateName);
            cmd.Parameters.AddWithValue("@state_active_flag", state_active_flag);
            cmd.Parameters.AddWithValue("@STATE_ID", Stateid);
            cmd.Parameters.AddWithValue("@COUNTRY_ID", countryid);
            cmd.Parameters.AddWithValue("@ACTION", 1);

            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int City_Insert(string CityName, int StateID, string zip)
        {
            SqlCommand cmd = new SqlCommand("spCityMasterInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CITY_NAME", CityName);
            cmd.Parameters.AddWithValue("@city_active_flag", "");
            cmd.Parameters.AddWithValue("@ZIP", zip);
            cmd.Parameters.AddWithValue("@CITY_ID", 0);
            cmd.Parameters.AddWithValue("@STATE_ID", StateID);
            cmd.Parameters.AddWithValue("@ACTION", 0);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int City_Update(string CityName, int Cityid, int StateID, string zip, string city_active_flag)
        {
            SqlCommand cmd = new SqlCommand("spCityMasterInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CITY_NAME", CityName);
            cmd.Parameters.AddWithValue("@city_active_flag", city_active_flag);
            cmd.Parameters.AddWithValue("@ZIP", zip);
            cmd.Parameters.AddWithValue("@CITY_ID", Cityid);
            cmd.Parameters.AddWithValue("@STATE_ID", StateID);
            cmd.Parameters.AddWithValue("@ACTION", 1);

            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int City_Delete(int Cityid)
        {
            SqlCommand cmd = new SqlCommand("spCityMasterInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CITY_NAME", "");
            cmd.Parameters.AddWithValue("@city_active_flag", "");
            cmd.Parameters.AddWithValue("@ZIP", "");
            cmd.Parameters.AddWithValue("@CITY_ID", Cityid);
            cmd.Parameters.AddWithValue("@STATE_ID", 0000);
            cmd.Parameters.AddWithValue("@ACTION", 2);
            //cmd.Parameters.AddWithValue("@OUTPUTMSG", 000);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int City_Activate(int Cityid)
        {
            SqlCommand cmd = new SqlCommand("spCityMasterInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CITY_NAME", "");
            cmd.Parameters.AddWithValue("@city_active_flag", "");
            cmd.Parameters.AddWithValue("@ZIP", "");
            cmd.Parameters.AddWithValue("@CITY_ID", Cityid);
            cmd.Parameters.AddWithValue("@STATE_ID", 0000);
            cmd.Parameters.AddWithValue("@ACTION", 3);
            //cmd.Parameters.AddWithValue("@OUTPUTMSG", 000);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int Title_Insert(string Title)
        {
            SqlCommand cmd = new SqlCommand("spTitleMaster", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@TITLE", Title);
            cmd.Parameters.AddWithValue("@title_active_flag", "");
            cmd.Parameters.AddWithValue("@TITLE_ID", 0);
            cmd.Parameters.AddWithValue("@ACTION", 0);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int Title_Update(string Title, int Titleid, string title_active_flag)
        {
            SqlCommand cmd = new SqlCommand("spTitleMaster", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@TITLE", Title);
            cmd.Parameters.AddWithValue("@title_active_flag", title_active_flag);
            cmd.Parameters.AddWithValue("@TITLE_ID", Titleid);
            cmd.Parameters.AddWithValue("@ACTION", 1);

            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int Title_Delete(int Titleid)
        {
            SqlCommand cmd = new SqlCommand("spTitleMaster", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@TITLE", "");
            cmd.Parameters.AddWithValue("@title_active_flag", "");
            cmd.Parameters.AddWithValue("@TITLE_ID", Titleid);
            cmd.Parameters.AddWithValue("@ACTION", 2);
            //cmd.Parameters.AddWithValue("@OUTPUTMSG", 000);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int Title_Activate(int Titleid)
        {
            SqlCommand cmd = new SqlCommand("spTitleMaster", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@TITLE", "");
            cmd.Parameters.AddWithValue("@title_active_flag", "");
            cmd.Parameters.AddWithValue("@TITLE_ID", Titleid);
            cmd.Parameters.AddWithValue("@ACTION", 3);
            //cmd.Parameters.AddWithValue("@OUTPUTMSG", 000);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int Timezone_Insert(string Timezone)
        {
            SqlCommand cmd = new SqlCommand("spTimezonemaster", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@TIMEZONE", Timezone);
            cmd.Parameters.AddWithValue("@timezone_active_flag", "");
            cmd.Parameters.AddWithValue("@TIMEZONE_ID", 0);
            cmd.Parameters.AddWithValue("@ACTION", 0);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int Timezone_Update(string Timezone, int Timezone_id, string Timezone_active_flag)
        {
            SqlCommand cmd = new SqlCommand("spTimezonemaster", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@TIMEZONE", Timezone);
            cmd.Parameters.AddWithValue("@timezone_active_flag", Timezone_active_flag);
            cmd.Parameters.AddWithValue("@TIMEZONE_ID", Timezone_id);
            cmd.Parameters.AddWithValue("@ACTION", 1);

            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int Timezone_Delete(int Timezoneid)
        {
            SqlCommand cmd = new SqlCommand("spTimezonemaster", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@TIMEZONE", "");
            cmd.Parameters.AddWithValue("@timezone_active_flag", "");
            cmd.Parameters.AddWithValue("@TIMEZONE_ID", Timezoneid);
            cmd.Parameters.AddWithValue("@ACTION", 2);
            //cmd.Parameters.AddWithValue("@OUTPUTMSG", 000);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int Timezone_Activate(int Timezoneid)
        {
            SqlCommand cmd = new SqlCommand("spTimezonemaster", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@TIMEZONE", "");
            cmd.Parameters.AddWithValue("@timezone_active_flag", "");
            cmd.Parameters.AddWithValue("@TIMEZONE_ID", Timezoneid);
            cmd.Parameters.AddWithValue("@ACTION", 3);
            //cmd.Parameters.AddWithValue("@OUTPUTMSG", 000);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public DataTable Getlookup()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGETLOOKUPLIST", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

        }
        public int Lookup_Insert(string Lookup, string lookup_group)
        {
            SqlCommand cmd = new SqlCommand("spLookupMaster", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@LOOKUP", Lookup);
            cmd.Parameters.AddWithValue("@lkup_active_flag", "");
            cmd.Parameters.AddWithValue("@LOOKUP_ID", 0);
            cmd.Parameters.AddWithValue("@LOOKUP_GROUP", lookup_group);
            cmd.Parameters.AddWithValue("@ACTION", 0);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int Lookup_Update(string Lookup, int Lookupid, string lookup_group, string lk_active_flag)
        {
            SqlCommand cmd = new SqlCommand("spLookupMaster", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@LOOKUP", Lookup);
            cmd.Parameters.AddWithValue("@lkup_active_flag", lk_active_flag);
            cmd.Parameters.AddWithValue("@LOOKUP_ID", Lookupid);
            cmd.Parameters.AddWithValue("@LOOKUP_GROUP", lookup_group);
            cmd.Parameters.AddWithValue("@ACTION", 1);

            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int Lookup_Delete(int Lookupid, string lookup)
        {
            SqlCommand cmd = new SqlCommand("spLookupMaster", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@LOOKUP", lookup);
            cmd.Parameters.AddWithValue("@lkup_active_flag", "");
            cmd.Parameters.AddWithValue("@LOOKUP_ID", Lookupid);
            cmd.Parameters.AddWithValue("@LOOKUP_GROUP", "");
            cmd.Parameters.AddWithValue("@ACTION", 2);
            //cmd.Parameters.AddWithValue("@OUTPUTMSG", 000);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int Lookup_Activate(int Lookupid, string lookup)
        {
            SqlCommand cmd = new SqlCommand("spLookupMaster", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@LOOKUP", lookup);
            cmd.Parameters.AddWithValue("@lkup_active_flag", "");
            cmd.Parameters.AddWithValue("@LOOKUP_ID", Lookupid);
            cmd.Parameters.AddWithValue("@LOOKUP_GROUP", "");
            cmd.Parameters.AddWithValue("@ACTION", 3);
            //cmd.Parameters.AddWithValue("@OUTPUTMSG", 000);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public DataTable Getstatelist(int countryid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetStatelist", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@COUNTRY_ID", countryid);
            cmd.CommandTimeout = 0;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

        }

        #region user
        public DataTable Users_Get(string userrole)
        {
            SqlCommand cmd = new SqlCommand("SPMANAGEUSERS", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@USERS_LOGIN", "");
            cmd.Parameters.AddWithValue("@USERS_ROLE", userrole);
            cmd.Parameters.AddWithValue("@USERS_ID", 0);
            cmd.Parameters.AddWithValue("@USERS_ACTIVE_FLAG", "");
            cmd.Parameters.AddWithValue("@ACTION", 4);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int User_Delete(int userid)
        {
            SqlCommand cmd = new SqlCommand("SPMANAGEUSERS", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@USERS_LOGIN", "");
            cmd.Parameters.AddWithValue("@USERS_ROLE", "");
            cmd.Parameters.AddWithValue("@USERS_ID", userid);
            cmd.Parameters.AddWithValue("@USERS_ACTIVE_FLAG", "");
            cmd.Parameters.AddWithValue("@ACTION", 2);
            //cmd.Parameters.AddWithValue("@OUTPUTMSG", 000);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int User_Activate(int userid)
        {
            SqlCommand cmd = new SqlCommand("SPMANAGEUSERS", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@USERS_LOGIN", "");
            cmd.Parameters.AddWithValue("@USERS_ROLE", "");
            cmd.Parameters.AddWithValue("@USERS_ID", userid);
            cmd.Parameters.AddWithValue("@USERS_ACTIVE_FLAG", "");
            cmd.Parameters.AddWithValue("@ACTION", 3);
            //cmd.Parameters.AddWithValue("@OUTPUTMSG", 000);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int User_Update(string Username, int userid, string userrole, string active_flag, string domainusername)
        {
            SqlCommand cmd = new SqlCommand("SPMANAGEUSERS", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@USERS_LOGIN", Username);
            cmd.Parameters.AddWithValue("@USERS_ROLE", userrole);
            cmd.Parameters.AddWithValue("@USERS_ID", userid);
            cmd.Parameters.AddWithValue("@USERS_ACTIVE_FLAG", active_flag);
            cmd.Parameters.AddWithValue("@domainusername", domainusername);
            cmd.Parameters.AddWithValue("@ACTION", 1);

            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public int User_Insert(string Username, string Userrole, string domainusername)
        {
            SqlCommand cmd = new SqlCommand("SPMANAGEUSERS", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@USERS_LOGIN", Username);
            cmd.Parameters.AddWithValue("@USERS_ROLE", Userrole);
            cmd.Parameters.AddWithValue("@USERS_ID", 0);
            cmd.Parameters.AddWithValue("@USERS_ACTIVE_FLAG", "");
            cmd.Parameters.AddWithValue("@domainusername", domainusername);
            cmd.Parameters.AddWithValue("@ACTION", 0);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }

                SqlParameter pvOUTPUTMSG = new SqlParameter();
                pvOUTPUTMSG.ParameterName = "@OUTPUTMSG";
                pvOUTPUTMSG.DbType = DbType.Int32;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["@OUTPUTMSG"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        #endregion

        #endregion

        public DataTable GetContactPersonList(string PersonalIDList)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetContactPesonsList", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@PersonalIDList", PersonalIDList);
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }


        }
        public DataTable Getcountry_frmstate(Int32 stateid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetState", dbconn);
            cmd.Parameters.AddWithValue("@StateID", stateid);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

        }

        //public object GetRole(string userlogin)
        //{
        //    SqlCommand cmd = new SqlCommand("spGetLogin", dbconn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@users_login", userlogin);
        //    object userrole = "";
        //    try
        //    {
        //        dbconn.Open();
        //        userrole = cmd.ExecuteScalar();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    finally
        //    {
        //        if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
        //    }

        //    return userrole;
        //}
        public DataTable GetRole(string userlogin)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetLogin", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@users_login", userlogin);

            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }

        public DataTable GetPerformance_Copy(Int32 engtid, string type)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetPerformanceCopy", dbconn);
            cmd.Parameters.AddWithValue("@engmntid", engtid);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

        }
        public DataTable Getshowsidfromdiary(string id)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SPSHOWIDGET", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@diarytitle", id);
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }

        #region Excel process
        public void sqlbcopy(DataTable dt, string tablename)
        {
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                { dbconn.Open(); }
                SqlBulkCopy sqlcopy = new SqlBulkCopy(dbconn);
                sqlcopy.DestinationTableName = tablename;
                sqlcopy.WriteToServer(dt);
                sqlcopy.BulkCopyTimeout = 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                { dbconn.Close(); }

            }
        }
        public DataTable GetMysql_Recordid(string show_name, string city, DateTime opdate, DateTime cldate)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetMySQLRecordID", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@showname", show_name);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@opdate", opdate);
                cmd.Parameters.AddWithValue("@cldate", cldate);
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }
        public DataSet GetTempData()
        {
            DataSet dt = new DataSet();
            SqlCommand cmd = new SqlCommand("GetTempData", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }
        public String EngtInsert_fromexcel(string showid, string cityid, string venueid, string presenterid, DateTime engtdate, string statename)
        {
            SqlCommand cmd = new SqlCommand("spEngtInsert_newid", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@showid", showid);
            cmd.Parameters.AddWithValue("@cityid", cityid);
            cmd.Parameters.AddWithValue("@venueid", venueid);
            cmd.Parameters.AddWithValue("@presenterid", presenterid);
            cmd.Parameters.AddWithValue("@EngtDate", engtdate);
            cmd.Parameters.AddWithValue("@statename", statename);
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                SqlParameter pvOUTPUTMSG = new SqlParameter("@Newid", SqlDbType.VarChar, 500);
                //SqlParameter pvOUTPUTMSG = new SqlParameter();
                //pvOUTPUTMSG.ParameterName = "@Newid";
                //pvOUTPUTMSG.DbType = DbType.String;
                pvOUTPUTMSG.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvOUTPUTMSG);
                cmd.ExecuteNonQuery();
                return Convert.ToString(cmd.Parameters["@Newid"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public void Insert_Temp_To_MainTable()
        {
            SqlCommand cmd = new SqlCommand("SPExcelToTempInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed)
                {
                    dbconn.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }

        }
        public DataTable spGetExcelKeyFieldsID(string showname, string cityname, string venuename, string presentername, string statename)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetExcelKeyFieldID", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@showname", showname);
                cmd.Parameters.AddWithValue("@cityname", cityname);
                cmd.Parameters.AddWithValue("@venuename", venuename);
                cmd.Parameters.AddWithValue("@presentername", presentername);
                cmd.Parameters.AddWithValue("@statename", statename);
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }
        #endregion

        public DataTable GetSearchData(string Type, string ActiveFlag, string BeastFlag, string ArgName, string ArgName1)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetSearchData", dbconn);
            cmd.Parameters.AddWithValue("@Type", Type);
            cmd.Parameters.AddWithValue("@ActiveFlag", ActiveFlag);
            cmd.Parameters.AddWithValue("@BeastFlag", BeastFlag);
            cmd.Parameters.AddWithValue("@ArgName", ArgName);
            cmd.Parameters.AddWithValue("@ArgName1", ArgName1);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }

        public DataTable GetUserList(string userrole)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetUserList", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.Parameters.AddWithValue("@UserRole", userrole);
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }


        }
        public DataTable GetSearchDataNew(string type, int index, string userid, string arg1, out int outhistoryid, int historyid, string arg2 = null, string arg3 = null, string arg4 = null, string arg5 = null, string arg6 = null, string arg7 = null, string arg8 = null, string arg9 = null, string arg10 = null)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetSearchDataNew", dbconn);
            cmd.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@Arg1", arg1);
            cmd.Parameters.AddWithValue("@Arg2", arg2);
            cmd.Parameters.AddWithValue("@Arg3", arg3);
            cmd.Parameters.AddWithValue("@Arg4", arg4);
            cmd.Parameters.AddWithValue("@Arg5", arg5);
            cmd.Parameters.AddWithValue("@Arg6", arg6);
            cmd.Parameters.AddWithValue("@Arg7", arg7);
            cmd.Parameters.AddWithValue("@Arg8", arg8);
            cmd.Parameters.AddWithValue("@Arg9", arg9);
            cmd.Parameters.AddWithValue("@Arg10", arg10);
            cmd.Parameters.AddWithValue("@Index", index);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.Parameters.AddWithValue("@ScreenId", 4);
            cmd.Parameters.AddWithValue("@historyid", historyid);


            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                outhistoryid = Convert.ToInt32(cmd.Parameters["@NewId"].Value);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                HttpContext.Current.Session["Sdt"] = dt;
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }


        public DataTable GetSearchDataEngtSchedule(string deal_royalty_income
 , string deal_guarantee_income, string deal_cmpny_mid_monies_ptg, string deal_presenter_mid_monies_ptg, string deal_mid_monies_cap, string
            deal_producer_share_split_ptg, string deal_presenter_share_split_ptg, string deal_star_royalty_ptg, string deal_misc_othr_amt_1, string
            deal_misc_othr_amt_2, string deal_incm_wthd_tax_bgt_amt, string deal_incm_wthd_tax_act_amt, string deal_tax_ptg, string
            deal_tax_amt_over, string deal_tax2_ptg, string deal_sub_sales_comm, string deal_ph_sales_comm, string deal_web_sales_comm, string
            deal_cc_sales_comm, string deal_remote_sales_comm, string deal_facility_fee_amt, string deal_tax_ff_bo_comm, string
                deal_single_tix_comm, string deal_grp_sales_comm1, string deal_grp_sales_comm2, string dealtype, int type, string userid,int historyid,out int outhistoryid )
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetSearchDealData", dbconn);
             cmd.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@DEAL_ROYALTY_INCOME", deal_royalty_income);
            cmd.Parameters.AddWithValue("@DEAL_GUARANTEE_INCOME", deal_guarantee_income);
            cmd.Parameters.AddWithValue("@DEAL_CMPNY_MID_MONIES_PTG", deal_cmpny_mid_monies_ptg);
            cmd.Parameters.AddWithValue("@DEAL_PRESENTER_MID_MONIES_PTG", deal_presenter_mid_monies_ptg);
            cmd.Parameters.AddWithValue("@DEAL_MID_MONIES_CAP", deal_mid_monies_cap);
            cmd.Parameters.AddWithValue("@DEAL_PRODUCER_SHARE_SPLIT_PTG", deal_producer_share_split_ptg);
            cmd.Parameters.AddWithValue("@DEAL_PRESENTER_SHARE_SPLIT_PTG", deal_presenter_share_split_ptg);
            cmd.Parameters.AddWithValue("@DEAL_STAR_ROYALTY_PTG", deal_star_royalty_ptg);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_1", deal_misc_othr_amt_1);
            cmd.Parameters.AddWithValue("@DEAL_MISC_OTHR_AMT_2", deal_misc_othr_amt_2);
            cmd.Parameters.AddWithValue("@DEAL_INCM_WTHD_TAX_BGT_AMT", deal_incm_wthd_tax_bgt_amt);
            cmd.Parameters.AddWithValue("@DEAL_INCM_WTHD_TAX_ACT_AMT", deal_incm_wthd_tax_act_amt);
            cmd.Parameters.AddWithValue("@DEAL_TAX_PTG", deal_tax_ptg);
            cmd.Parameters.AddWithValue("@DEAL_TAX_AMT_OVER", deal_tax_amt_over);
            cmd.Parameters.AddWithValue("@DEAL_TAX2_PTG", deal_tax2_ptg);
            cmd.Parameters.AddWithValue("@DEAL_SUB_SALES_COMM", deal_sub_sales_comm);
            cmd.Parameters.AddWithValue("@DEAL_PH_SALES_COMM", deal_ph_sales_comm);
            cmd.Parameters.AddWithValue("@DEAL_WEB_SALES_COMM", deal_web_sales_comm);
            cmd.Parameters.AddWithValue("@DEAL_CC_SALES_COMM", deal_cc_sales_comm);
            cmd.Parameters.AddWithValue("@DEAL_REMOTE_SALES_COMM", deal_remote_sales_comm);
            cmd.Parameters.AddWithValue("@DEAL_FACILITY_FEE_AMT", deal_facility_fee_amt);
            cmd.Parameters.AddWithValue("@DEAL_TAX_FF_BO_COMM", deal_tax_ff_bo_comm);
            cmd.Parameters.AddWithValue("@DEAL_SINGLE_TIX_COMM", deal_single_tix_comm);
            cmd.Parameters.AddWithValue("@DEAL_GRP_SALES_COMM1", deal_grp_sales_comm1);
            cmd.Parameters.AddWithValue("@DEAL_GRP_SALES_COMM2", deal_grp_sales_comm2);
            cmd.Parameters.AddWithValue("@DEAL_DEAL_TYPE", dealtype);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.Parameters.AddWithValue("@ScreenId", 5);
             cmd.Parameters.AddWithValue("@historyid", historyid);

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                outhistoryid = Convert.ToInt32(cmd.Parameters["@NewId"].Value);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                HttpContext.Current.Session["Sdt"] = dt;
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }



        public DataTable GetSearchDataExpenseEngtSchedule(string exp_d_ad_gross_bgt, string exp_d_ad_gross_act,
            string exp_d_labor_catering_bgt, string exp_d_labor_catering_act, string exp_d_musician_bgt,
            string exp_d_musician_act, string exp_d_stghand_loadin_bgt, string exp_d_stghand_loadin_act,
            string exp_d_stghand_loadout_bgt, string exp_d_stghand_loadout_act, string exp_d_stghand_running_bgt,
            string exp_d_stghand_running_act, string exp_d_wardrobe_loadin_bgt, string exp_d_wardrobe_loadin_act,
            string exp_d_wardrobe_loadout_bgt, string exp_d_wardrobe_loadout_act, string exp_d_wardrobe_running_bgt,
            string exp_d_wardrobe_running_act, string exp_d_insurance_per_unit, string exp_d_insurance_bgt,
            string exp_d_insurance_act, string exp_d_ticket_print_per_unit, string exp_d_ticket_print_bgt, string exp_d_ticket_print_act, string exp_d_other_1_bgt, string exp_d_other_1_act, string exp_d_other_2_bgt, string exp_d_other_2_act, string exp_l_ada_expense_bgt, string exp_l_ada_expense_act, string exp_l_bo_bgt, string exp_l_bo_act, string exp_l_catering_bgt, string exp_l_catering_act, string exp_l_equip_rental_bgt, string exp_l_equip_rental_act, string exp_l_grp_sales_bgt, string exp_l_grp_sales_act, string exp_l_house_staff_bgt, string exp_l_house_staff_act, string exp_l_league_fee_bgt, string exp_l_league_fee_act, string exp_l_license_bgt, string exp_l_license_act, string exp_l_limo_bgt, string exp_l_limo_act, string exp_l_orchestra_sh_remove_bgt, string exp_l_orchestra_sh_remove_act, string exp_l_presenter_profit_bgt, string exp_l_presenter_profit_act, string exp_l_police_bgt,
            string exp_l_police_act, string exp_l_program_bgt, string exp_l_program_act,
            string exp_l_rent_btg, string exp_l_rent_act, string exp_l_sound_bgt, string exp_l_sound_act,
            string exp_l_ticket_print_bgt, string exp_l_ticket_print_act, string exp_l_phone_bgt,
            string exp_l_phone_act, string exp_l_dryice_bgt, string exp_l_dryice_act,
            string exp_l_other1_desc, string exp_l_other1_bgt, string exp_l_other1_act, string exp_l_other2_desc,
            string exp_l_other2_bgt, string exp_l_other2_act, string exp_l_other3_desc, string exp_l_other3_bgt,
            string exp_l_other3_act, string exp_l_other4_desc, string exp_l_other4_bgt, string exp_l_other4_act,
            string exp_l_other5_desc, string exp_l_other5_bgt, string exp_l_other5_act, string exp_l_local_fixed_bgt,
            string exp_l_local_fixed_act, string exp_type, int type, string userid,int historyid,out int outhistoryid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetSearchExpenseData", dbconn);

            cmd.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.Parameters.AddWithValue("@ScreenId", 7);
            cmd.Parameters.AddWithValue("@historyid", historyid);
            cmd.Parameters.AddWithValue("@Exp_d_ad_gross_bgt ", exp_d_ad_gross_bgt);
            cmd.Parameters.AddWithValue("@Exp_d_ad_gross_act ", exp_d_ad_gross_act);
            cmd.Parameters.AddWithValue("@Exp_d_labor_catering_bgt ", exp_d_labor_catering_bgt);
            cmd.Parameters.AddWithValue("@Exp_d_labor_catering_act ", exp_d_labor_catering_act);
            cmd.Parameters.AddWithValue("@Exp_d_musician_bgt ", exp_d_musician_bgt);
            cmd.Parameters.AddWithValue("@Exp_d_musician_act ", exp_d_musician_act);
            cmd.Parameters.AddWithValue("@Exp_d_stgand_loading_bgt ", exp_d_stghand_loadin_bgt);
            cmd.Parameters.AddWithValue("@Exp_d_stgand_loading_act ", exp_d_stghand_loadin_act);
            cmd.Parameters.AddWithValue("@Exp_d_stghand_loadout_bgt ", exp_d_stghand_loadout_bgt);
            cmd.Parameters.AddWithValue("@Exp_d_stghand_loadout_act ", exp_d_stghand_loadout_act);
            cmd.Parameters.AddWithValue("@Exp_d_stghand_running_bgt ", exp_d_stghand_running_bgt);
            cmd.Parameters.AddWithValue("@Exp_d_stghand_running_act ", exp_d_stghand_running_act);
            cmd.Parameters.AddWithValue("@Exp_d_wardrobe_loadin_bgt ", exp_d_wardrobe_loadin_bgt);
            cmd.Parameters.AddWithValue("@Exp_d_wardrobe_loadin_act ", exp_d_wardrobe_loadin_act);
            cmd.Parameters.AddWithValue("@Exp_d_wardrobe_loadout_bgt ", exp_d_wardrobe_loadout_bgt);
            cmd.Parameters.AddWithValue("@Exp_d_wardrobe_loadout_act ", exp_d_wardrobe_loadout_act);
            cmd.Parameters.AddWithValue("@Exp_d_wardrobe_running_bgt ", exp_d_wardrobe_running_bgt);
            cmd.Parameters.AddWithValue("@Exp_d_wardrobe_running_act ", exp_d_wardrobe_running_act);
            cmd.Parameters.AddWithValue("@Exp_d_insurance_per_unit ", exp_d_insurance_per_unit);
            cmd.Parameters.AddWithValue("@Exp_d_insurance_bgt ", exp_d_insurance_bgt);
            cmd.Parameters.AddWithValue("@Exp_d_insurance_act ", exp_d_insurance_act);
            cmd.Parameters.AddWithValue("@Exp_d_ticket_print_per_unit ", exp_d_ticket_print_per_unit);
            cmd.Parameters.AddWithValue("@Exp_d_ticket_print_bgt ", exp_d_ticket_print_bgt);
            cmd.Parameters.AddWithValue("@Exp_d_ticket_print_act ", exp_d_ticket_print_act);
            cmd.Parameters.AddWithValue("@Exp_d_other_1_bgt ", exp_d_other_1_bgt);
            cmd.Parameters.AddWithValue("@Exp_d_other_1_act ", exp_d_other_1_act);
            cmd.Parameters.AddWithValue("@Exp_d_other_2_bgt ", exp_d_other_2_bgt);
            cmd.Parameters.AddWithValue("@Exp_d_other_2_act ", exp_d_other_2_act);
            cmd.Parameters.AddWithValue("@Exp_l_ada_expense_bgt ", exp_l_ada_expense_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_ada_expense_act ", exp_l_ada_expense_act);
            cmd.Parameters.AddWithValue("@Exp_l_bo_bgt ", exp_l_bo_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_bo_act ", exp_l_bo_act);
            cmd.Parameters.AddWithValue("@Exp_l_catering_bgt ", exp_l_catering_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_catering_act ", exp_l_catering_act);
            cmd.Parameters.AddWithValue("@Exp_l_equip_rental_bgt ", exp_l_equip_rental_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_equip_rental_act ", exp_l_equip_rental_act);
            cmd.Parameters.AddWithValue("@Exp_l_grp_sales_bgt ", exp_l_grp_sales_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_grp_sales_act ", exp_l_grp_sales_act);
            cmd.Parameters.AddWithValue("@Exp_l_house_staff_bgt ", exp_l_house_staff_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_house_staff_act ", exp_l_house_staff_act);
            cmd.Parameters.AddWithValue("@Exp_l_league_fee_bgt ", exp_l_league_fee_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_league_fee_act ", exp_l_league_fee_act);
            cmd.Parameters.AddWithValue("@Exp_l_license_bgt ", exp_l_license_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_license_act ", exp_l_license_act);
            cmd.Parameters.AddWithValue("@Exp_l_limo_bgt ", exp_l_limo_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_limo_act ", exp_l_limo_act);
            cmd.Parameters.AddWithValue("@Exp_l_orchestra_sh_remove_bgt ", exp_l_orchestra_sh_remove_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_orchestra_sh_remove_act ", exp_l_orchestra_sh_remove_act);
            cmd.Parameters.AddWithValue("@Exp_l_presenter_profit_bgt ", exp_l_presenter_profit_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_presenter_profit_act ", exp_l_presenter_profit_act);
            cmd.Parameters.AddWithValue("@Exp_l_police_bgt ", exp_l_police_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_police_act ", exp_l_police_act);
            cmd.Parameters.AddWithValue("@Exp_l_program_bgt ", exp_l_program_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_program_act ", exp_l_program_act);
            cmd.Parameters.AddWithValue("@Exp_l_rent_btg ", exp_l_rent_btg);
            cmd.Parameters.AddWithValue("@Exp_l_rent_act ", exp_l_rent_act);
            cmd.Parameters.AddWithValue("@Exp_l_sound_bgt ", exp_l_sound_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_sound_act ", exp_l_sound_act);
            cmd.Parameters.AddWithValue("@Exp_l_ticket_print_bgt ", exp_l_ticket_print_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_ticket_print_act ", exp_l_ticket_print_act);
            cmd.Parameters.AddWithValue("@Exp_l_phone_bgt ", exp_l_phone_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_phone_act ", exp_l_phone_act);
            cmd.Parameters.AddWithValue("@Exp_l_dryice_bgt ", exp_l_dryice_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_dryice_act ", exp_l_dryice_act);
            cmd.Parameters.AddWithValue("@Exp_l_other1_bgt ", exp_l_other1_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_other1_act ", exp_l_other1_act);
            cmd.Parameters.AddWithValue("@Exp_l_other2_bgt ", exp_l_other2_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_other2_act ", exp_l_other2_act);
            cmd.Parameters.AddWithValue("@Exp_l_other3_bgt ", exp_l_other3_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_other3_act ", exp_l_other3_act);
            cmd.Parameters.AddWithValue("@Exp_l_other4_bgt ", exp_l_other4_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_other4_act ", exp_l_other4_act);
            cmd.Parameters.AddWithValue("@Exp_l_other5_bgt ", exp_l_other5_bgt);
            cmd.Parameters.AddWithValue("@Exp_l_other5_act ", exp_l_other5_act);



            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                outhistoryid = Convert.ToInt32(cmd.Parameters["@NewId"].Value);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                HttpContext.Current.Session["Sdt"] = dt;
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }



        public DataTable GetSearchEngtDiscount(string Sub, string Subtkt, string grp, string grptkt, string misc, string misctkt, int type, string userid, int historyid, out int outhistoryid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("GetSearchDiscountHistory", dbconn);
            cmd.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@Sub", Sub);
            cmd.Parameters.AddWithValue("@Subtkt", Subtkt);
            cmd.Parameters.AddWithValue("@grp", grp);
            cmd.Parameters.AddWithValue("@grptkt", grptkt);
            cmd.Parameters.AddWithValue("@misc", misc);
            cmd.Parameters.AddWithValue("@misctkt", misctkt);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.Parameters.AddWithValue("@ScreenId", 9);
            cmd.Parameters.AddWithValue("@historyid", historyid);


            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                 outhistoryid = Convert.ToInt32(cmd.Parameters["@NewId"].Value);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                HttpContext.Current.Session["Sdt"] = dt;
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }



        public DataTable GetSearchPersonalData(string fname, string lname, string mname, string tid, string emptyp, string empstatus, string ms, string showname, string date, string tdate, string zipcode, string dob, string comp, string city, int type, string country, string userid, int historyid, out int outhistoryid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetSearchPersonalData", dbconn);
            cmd.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@FirstName", fname);
            cmd.Parameters.AddWithValue("@LastName", lname);
            cmd.Parameters.AddWithValue("@MiddleName", mname);
            cmd.Parameters.AddWithValue("@EmployeeType", emptyp);
            cmd.Parameters.AddWithValue("@DateOFHire", date);
            cmd.Parameters.AddWithValue("@Company", comp);
            cmd.Parameters.AddWithValue("@TerminationDate", tdate);
            cmd.Parameters.AddWithValue("@Title", tid);
            cmd.Parameters.AddWithValue("@EmployeStatus", empstatus);
            cmd.Parameters.AddWithValue("@ShowName", showname);
            cmd.Parameters.AddWithValue("@MaritalStatus", ms);
            cmd.Parameters.AddWithValue("@DOB", dob);
            cmd.Parameters.AddWithValue("@ZipCode", zipcode);
            cmd.Parameters.AddWithValue("@City", city);
            cmd.Parameters.AddWithValue("@Country", country);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.Parameters.AddWithValue("@ScreenID", 10);
            cmd.Parameters.AddWithValue("@historyid", historyid);
            cmd.Parameters.AddWithValue("@Type", type);



            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                outhistoryid = Convert.ToInt32(cmd.Parameters["@NewId"].Value);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                HttpContext.Current.Session["Sdt"] = dt;
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }













        public DataTable GetSearchDataPresenter(string presenter, string city, string state, string zipcode, string count, string fname, string lname, int type, string userid, int historyid, out int outhistoryid)
        {

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetSearchPresenterData", dbconn);
            cmd.Parameters.AddWithValue("@Presenter", presenter);
            cmd.Parameters.AddWithValue("@City", city);
            cmd.Parameters.AddWithValue("@ZipCode", zipcode);
            cmd.Parameters.AddWithValue("@Country", count);
            cmd.Parameters.AddWithValue("@FirstName", fname);
            cmd.Parameters.AddWithValue("@LastName", lname);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@State", state);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.Parameters.AddWithValue("@ScreenId", 1);
            cmd.Parameters.AddWithValue("@historyid", historyid);
            cmd.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;


            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                outhistoryid = Convert.ToInt32(cmd.Parameters["@NewId"].Value);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                HttpContext.Current.Session["Sdt"] = dt;
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }


        public DataTable GetSearchDataVenue(string venue, string city, string state, string zipcode, string count, string fname, string lname, string capacity, int type, string userid, int historyid, out int outhistoryid)
        {

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetSearchVenueData", dbconn);
            cmd.Parameters.AddWithValue("@Venue", venue);
            cmd.Parameters.AddWithValue("@City", city);
            cmd.Parameters.AddWithValue("@ZipCode", zipcode);
            cmd.Parameters.AddWithValue("@Country", count);
            cmd.Parameters.AddWithValue("@FirstName", fname);
            cmd.Parameters.AddWithValue("@LastName", lname);
            cmd.Parameters.AddWithValue("@Capacity", capacity);
            cmd.Parameters.AddWithValue("@State", state);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.Parameters.AddWithValue("@ScreenId", 2);
            cmd.Parameters.AddWithValue("@historyid", historyid);
            cmd.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();

                dt.Load(dr);
                outhistoryid = Convert.ToInt32(cmd.Parameters["@NewId"].Value);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                HttpContext.Current.Session["Sdt"] = dt;
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }

        public DataTable GetSearchDataShow(string Show, string Showbegindate, string corporateN, string CM, string ON, string VR, string WOE, int type, string userid, int historyid, out int outhistoryid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetSearchShowData", dbconn);
            cmd.Parameters.AddWithValue("@Show", Show);
            cmd.Parameters.AddWithValue("@Showbegindate", Showbegindate);
            cmd.Parameters.AddWithValue("@CorporateN", corporateN);
            cmd.Parameters.AddWithValue("@CompanyM", CM);
            cmd.Parameters.AddWithValue("@OverheadN", ON);
            cmd.Parameters.AddWithValue("@VariableR", VR);
            cmd.Parameters.AddWithValue("@WOE", WOE);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.Parameters.AddWithValue("@ScreenId", 3);
            cmd.Parameters.AddWithValue("@historyid", historyid);
            cmd.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                outhistoryid = Convert.ToInt32(cmd.Parameters["@NewId"].Value);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                HttpContext.Current.Session["Sdt"] = dt;
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

        }



        public DataTable GetSearchDataPriceEngtSchedule(string p_seat, string p_t_single, string p_t_group, string p_t_sub, int type, string userid, int historyid,out int outhistoryid )
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetSearchPriceScaleData", dbconn);
            cmd.Parameters.AddWithValue("@PriceSeat", p_seat);
            cmd.Parameters.AddWithValue("@PriceTSingle", p_t_single);
            cmd.Parameters.AddWithValue("@PriceTGroup", p_t_group);
            cmd.Parameters.AddWithValue("@PriceTSub", p_t_sub);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.Parameters.AddWithValue("@ScreenId", 6);
            cmd.Parameters.AddWithValue("@historyid", historyid);
            cmd.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                outhistoryid = Convert.ToInt32(cmd.Parameters["@NewId"].Value);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                HttpContext.Current.Session["Sdt"] = dt;
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }



        public DataTable GetSearchDataBoxOfficeEngtSchedule(string grpsales, string drpcount, string paidA, string Comp, string SubTktSale, string SubGrossRec, string PhTktSale, string Phgross, string InternetTktSale, string InternetGross, string CCTktSale, string CCGrossRec, string RemoteTktSale, string RemoteGrossRec, string SingleTicketsTktSale, string SingleTktsGrossRec, string Group1TktSale, string Grp1GrossReceipt, string Gr2TktSale, string Grp2GrossRecept, int type, string userid, int historyid, out int outhistoryid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetSearchBoxOfficeData", dbconn);
            //cmd.Parameters.AddWithValue("@PriceSeat", p_seat);
            //cmd.Parameters.AddWithValue("@PriceTSingle", p_t_single);
            //cmd.Parameters.AddWithValue("@PriceTGroup", p_t_group);
            //cmd.Parameters.AddWithValue("@PriceTSub", p_t_sub);

            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.Parameters.AddWithValue("@Bo_gross_sales ", grpsales);
            cmd.Parameters.AddWithValue("@Bo_drop_count ", drpcount);
            cmd.Parameters.AddWithValue("@Bo_paid_attendance ", paidA);
            cmd.Parameters.AddWithValue("@Bo_comps ", Comp);
            cmd.Parameters.AddWithValue("@Bo_sub_t_sold ", SubTktSale);
            cmd.Parameters.AddWithValue("@Bo_sub_gross_rcpt ", SubGrossRec);
            cmd.Parameters.AddWithValue("@Bo_ph_t_sold ", PhTktSale);
            cmd.Parameters.AddWithValue("@Bo_ph_gross_rcpt ", Phgross);
            cmd.Parameters.AddWithValue("@Bo_web_t_sold ", InternetTktSale);
            cmd.Parameters.AddWithValue("@Bo_web_gross_rcpt ", InternetGross);
            cmd.Parameters.AddWithValue("@Bo_cc_t_sold ", CCTktSale);
            cmd.Parameters.AddWithValue("@Bo_cc_gross_rcpt ", CCGrossRec);
            cmd.Parameters.AddWithValue("@Bo_outlet_t_sold ", RemoteTktSale);
            cmd.Parameters.AddWithValue("@Bo_outlet_gross_rcpt ", RemoteGrossRec);
            cmd.Parameters.AddWithValue("@Bo_single_tix_t_sold ", SingleTicketsTktSale);
            cmd.Parameters.AddWithValue("@Bo_single_tix_gross_rcpt ", SingleTktsGrossRec);
            cmd.Parameters.AddWithValue("@Bo_group1_t_sold ", Group1TktSale);
            cmd.Parameters.AddWithValue("@Bo_group1_gross_rcpt ", Grp1GrossReceipt);
            cmd.Parameters.AddWithValue("@Bo_group2_t_sold ", Gr2TktSale);
            cmd.Parameters.AddWithValue("@Bo_group2_gross_rcpt ", Grp2GrossRecept);
            cmd.Parameters.AddWithValue("@ScreenId", 8);
            cmd.Parameters.AddWithValue("@historyid", historyid);
            cmd.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;


            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                outhistoryid = Convert.ToInt32(cmd.Parameters["@NewId"].Value);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                HttpContext.Current.Session["Sdt"] = dt;
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
        }



        
    }
}

