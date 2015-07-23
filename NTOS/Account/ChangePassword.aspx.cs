using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTOS.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
       // UserDataAccess uda = new UserDataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ChangeUserPassword_ChangingPassword(object sender, LoginCancelEventArgs e)
        {
            //int up = uda.ResetPassword(Convert.ToInt32(Session["loginuserid"]), ChangeUserPassword.ConfirmNewPassword);
            Response.Redirect("~/Diary.aspx");
        }
    }
}