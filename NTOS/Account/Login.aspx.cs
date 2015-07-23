using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices.AccountManagement;
using MasterDataLayer;
using System.Data;
namespace NTOS.Account
{
    public partial class Login : Page
    {
        MasterData mda = new MasterData();
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
        {
            dt = new DataTable();
            try
            {
                bool valid = false;
                Int32 userid = 0;
                using (PrincipalContext context = new PrincipalContext(ContextType.Machine))
                {
                    valid = context.ValidateCredentials(LoginUser.UserName, LoginUser.Password);
                    //object urole = mda.GetRole(LoginUser.UserName);
                    dt = mda.GetRole(LoginUser.UserName);
                    object urole = null;
                    if (dt.Rows.Count > 0)
                    {
                        urole = dt.Rows[0]["USERS_ROLE"];
                        userid = Convert.ToInt32(dt.Rows[0]["users_id"]);
                    }
                    else
                    {
                        LoginUser.FailureText = "You are not permitted to access this application";
                    }
                    if (valid && (Convert.ToString(urole) == "superadmin" || Convert.ToString(urole) == "admin" || Convert.ToString(urole) == "reader" || Convert.ToString(urole) == "compmanager" || Convert.ToString(urole) == "officestaff"))
                    {
                        e.Authenticated = true;
                        UserPrincipal up = null;
                        up = UserPrincipal.FindByIdentity(context, LoginUser.UserName);
                        Session["username"] = up.Name;
                        Session["userrole"] = urole;
                        Session["userid"] = userid;
                        Session["Historyid"] = "";
                        Session["search"] = null;
                        Session["isNew"] = null;
                        Session["isNewAll"] = null;
                        Session["searchAll"] = null;

                    }
                }
                if (Request.QueryString["url"] != null && Request.QueryString["url"].ToString() != "")
                {
                    string url = Request.QueryString["url"].ToString();
                    Response.Redirect("~" + url, true);
                }
            }
            catch (Exception ex)
            {
                LoginUser.FailureText = ex.Message.ToString();
            }

        }
    }
}