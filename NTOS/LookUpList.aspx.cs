using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
namespace NTOS
{
    public partial class LookUpList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        

            if (!IsPostBack)
            {
                (this.Master as Site1).HideNewbutton();
                Label lbl_msg;
                lbl_msg = (Label)this.Master.FindControl("lbl_headersite");
                lbl_msg.Text = "Lookup List";
                (this.Master as Site1).hide();
            }
        }

     
        protected void select()
        {
        }

       
    }
}