using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Caching;

namespace PersonalDataLayer
{
    public class PersonalData
    {
        SqlConnection dbconn = new SqlConnection(DBConnectionLayer.DBConnection.GetDBConnection());
        public List<string> searchpersonal(string searchtext, string activeflag)
        {
            DataTable dt = new DataTable();
            List<string> show = new List<string>();
            SqlCommand cmd = new SqlCommand("spPersonalSearch", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PERSON_NAME", searchtext);
            cmd.Parameters.AddWithValue("@ActiveFlag", activeflag);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    show.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                }
                return show;
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
        public Int32 Personaldata_Insert(string fname, string mname, string lname, string company, string employeetype, string materialstatus, string empstatus, string phoneland, string phoncell, string phoneother, string fax, string email, string webpage, string facebook, string twitter, string residstaddress, Nullable<Int32> residcityid, string otherstaddress, Nullable<Int32> othercityid, Nullable<Int16> titleid, Nullable<DateTime> dateifhire, Nullable<DateTime> terminationdate, Nullable<DateTime> dob, string homezip, string otherzip)
        {
            SqlCommand cmd = new SqlCommand("spPersonalInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PERSONAL_TITLE_ID", titleid);
            cmd.Parameters.AddWithValue("@PERSONAL_FNAME", fname);
            cmd.Parameters.AddWithValue("@PERSONAL_LNAME", lname);
            cmd.Parameters.AddWithValue("@PERSONAL_MNAME", mname);
            cmd.Parameters.AddWithValue("@PERSONAL_PHONE_LAND", phoneland);
            cmd.Parameters.AddWithValue("@PERSONAL_PHONE_CELL", phoncell);
            cmd.Parameters.AddWithValue("@PERSONAL_PHONE_OTHER", phoneother);
            cmd.Parameters.AddWithValue("@PERSONAL_FAX", fax);
            cmd.Parameters.AddWithValue("@PERSONAL_EMAIL", email);
            cmd.Parameters.AddWithValue("@PERSONAL_COMPANY", company);
            cmd.Parameters.AddWithValue("@PERSONAL_EMP_TYPE", employeetype);
            cmd.Parameters.AddWithValue("@PERSONAL_HIRE_DATE", dateifhire);
            cmd.Parameters.AddWithValue("@PERSONAL_TERM_DATE", terminationdate);
            cmd.Parameters.AddWithValue("@PERSONAL_EMP_STATUS", empstatus);
            cmd.Parameters.AddWithValue("@PERSONAL_DOB", dob);
            cmd.Parameters.AddWithValue("@PERSONAL_MARRIED_STATUS", materialstatus);
            cmd.Parameters.AddWithValue("@PERSONAL_WEBPAGE", webpage);
            cmd.Parameters.AddWithValue("@PERSONAL_FACEBOOK", facebook);
            cmd.Parameters.AddWithValue("@PERSONAL_TWITTER", twitter);
            cmd.Parameters.AddWithValue("@PERSONAL_HOME_CITY_ID", residcityid);
            cmd.Parameters.AddWithValue("@PERSONAL_OTHER_CITY_ID", othercityid);
            cmd.Parameters.AddWithValue("@PERSONAL_HOME_STREET", residstaddress);
            cmd.Parameters.AddWithValue("@PERSONAL_OTHER_STREET", otherstaddress);
            cmd.Parameters.AddWithValue("@PERSONAL_HOME_ZIP", homezip);
            cmd.Parameters.AddWithValue("@PERSONAL_OTHER_ZIP", otherzip);
            SqlParameter pvNewId = new SqlParameter();
            Int32 newid = 0;
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                pvNewId.ParameterName = "@NewId";
                pvNewId.DbType = DbType.Int32;
                pvNewId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pvNewId);
                int rows;
                rows = cmd.ExecuteNonQuery();
                newid = Convert.ToInt32(cmd.Parameters["@NewId"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }
            return newid;
        }
        public DataTable GetPersonalDetails(string personalid)
        {

            string cachekey = "Personal"+personalid;
            if (HttpContext.Current.Cache[cachekey] != null)
            {
                DataTable cacheds = HttpContext.Current.Cache[cachekey] as DataTable;
                return cacheds;
            }
            else
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("spGetPersonalDetails", dbconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PERSONALID", personalid);
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
        public void PersonalShowAssign_Insert(Int32 personalid, Int32 personalassignshowid, DateTime assigndate, Nullable<DateTime> enddate, string assingflag)
        {
            SqlCommand cmd = new SqlCommand("spPersonalShowAssignInsert", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PERSONAL_ID", personalid);
            cmd.Parameters.AddWithValue("@PER_ASSIGN_SHOW_ID", personalassignshowid);
            cmd.Parameters.AddWithValue("@PER_ASSIGN_ST_DATE", assigndate);
            cmd.Parameters.AddWithValue("@PER_ASSIGN_END_DATE", enddate);
            cmd.Parameters.AddWithValue("@PER_ASSIGN_CURRENT_FLAG", assingflag);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                cmd.ExecuteNonQuery();
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
        public DataTable GetPersonalShowAssignDetails(Int32 personalid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("spGetPersonalShowAssign", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PERSONALID", personalid);
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
        public void Personaldata_Update(Int32 personalid, string fname, string mname, string lname, string company, string employeetype, string materialstatus, string empstatus, string phoneland, string phoncell, string phoneother, string fax, string email, string webpage, string facebook, string twitter, string residstaddress, Nullable<Int32> residcityid, string otherstaddress, Nullable<Int32> othercityid, Nullable<Int16> titleid, Nullable<DateTime> dateifhire, Nullable<DateTime> terminationdate, Nullable<DateTime> dob, string homezip, string otherzip)
        {
            SqlCommand cmd = new SqlCommand("spPersonalUpdate", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PERSONAL_ID", personalid);
            cmd.Parameters.AddWithValue("@PERSONAL_TITLE_ID", titleid);
            cmd.Parameters.AddWithValue("@PERSONAL_FNAME", fname);
            cmd.Parameters.AddWithValue("@PERSONAL_LNAME", lname);
            cmd.Parameters.AddWithValue("@PERSONAL_MNAME", mname);
            cmd.Parameters.AddWithValue("@PERSONAL_PHONE_LAND", phoneland);
            cmd.Parameters.AddWithValue("@PERSONAL_PHONE_CELL", phoncell);
            cmd.Parameters.AddWithValue("@PERSONAL_PHONE_OTHER", phoneother);
            cmd.Parameters.AddWithValue("@PERSONAL_FAX", fax);
            cmd.Parameters.AddWithValue("@PERSONAL_EMAIL", email);
            cmd.Parameters.AddWithValue("@PERSONAL_COMPANY", company);
            cmd.Parameters.AddWithValue("@PERSONAL_EMP_TYPE", employeetype);
            cmd.Parameters.AddWithValue("@PERSONAL_HIRE_DATE", dateifhire);
            cmd.Parameters.AddWithValue("@PERSONAL_TERM_DATE", terminationdate);
            cmd.Parameters.AddWithValue("@PERSONAL_EMP_STATUS", empstatus);
            cmd.Parameters.AddWithValue("@PERSONAL_DOB", dob);
            cmd.Parameters.AddWithValue("@PERSONAL_MARRIED_STATUS", materialstatus);
            cmd.Parameters.AddWithValue("@PERSONAL_WEBPAGE", webpage);
            cmd.Parameters.AddWithValue("@PERSONAL_FACEBOOK", facebook);
            cmd.Parameters.AddWithValue("@PERSONAL_TWITTER", twitter);
            cmd.Parameters.AddWithValue("@PERSONAL_HOME_CITY_ID", residcityid);
            cmd.Parameters.AddWithValue("@PERSONAL_OTHER_CITY_ID", othercityid);
            cmd.Parameters.AddWithValue("@PERSONAL_HOME_STREET", residstaddress);
            cmd.Parameters.AddWithValue("@PERSONAL_OTHER_STREET", otherstaddress);
            cmd.Parameters.AddWithValue("@PERSONAL_HOME_ZIP", homezip);
            cmd.Parameters.AddWithValue("@PERSONAL_OTHER_ZIP", otherzip);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                cmd.ExecuteNonQuery();
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
        public void PersonalShowAssign_Delete(Int32 personalshowid)
        {
            SqlCommand cmd = new SqlCommand("spPersonalShowAssignDelete", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PersonalShowID", personalshowid);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                cmd.ExecuteNonQuery();
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
        public string Personal_Delete(int PersonalID,string delflag)
        {
            SqlCommand cmd = new SqlCommand("spPersonalDelete", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PersonalID", PersonalID);
            cmd.Parameters.AddWithValue("@DelFlag", delflag);
            SqlParameter result = new SqlParameter("@RetMsg", SqlDbType.VarChar, 500);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);
            try
            {
                dbconn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbconn.State == ConnectionState.Open) { dbconn.Close(); }
            }

            return Convert.ToString(cmd.Parameters["@RetMsg"].Value);
        }
        public void PersonalShowAssign_Update(Int32 personalassignid, Int32 personalid, Int32 personalassignshowid, DateTime assigndate, Nullable<DateTime> enddate, string assingflag)
        {
            SqlCommand cmd = new SqlCommand("spPersonalShowAssignUpdate", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PER_ASSIGN_ID", personalassignid);
            cmd.Parameters.AddWithValue("@PERSONAL_ID", personalid);
            cmd.Parameters.AddWithValue("@PER_ASSIGN_SHOW_ID", personalassignshowid);
            cmd.Parameters.AddWithValue("@PER_ASSIGN_ST_DATE", assigndate);
            cmd.Parameters.AddWithValue("@PER_ASSIGN_END_DATE", enddate);
            cmd.Parameters.AddWithValue("@PER_ASSIGN_CURRENT_FLAG", assingflag);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                cmd.ExecuteNonQuery();
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
        public List<string> GetCompanyName(string searchtext)
        {
            DataTable dt = new DataTable();
            List<string> show = new List<string>();
            SqlCommand cmd = new SqlCommand("spGetCompanySearch", dbconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@personal_company", searchtext);
            try
            {
                if (dbconn.State == ConnectionState.Closed) { dbconn.Open(); }
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    show.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dt.Rows[i]["personal_company"].ToString(), dt.Rows[i]["personal_company"].ToString()));
                }
                return show;
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
}