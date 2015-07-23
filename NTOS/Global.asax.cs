using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using NTOS;

namespace NTOS
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();

            ScriptResourceDefinition jQuery = new ScriptResourceDefinition();
            jQuery.Path = "~/scripts/jquery-1.7.1.min.js";
            jQuery.DebugPath = "~/scripts/jquery-1.7.1.js";
            jQuery.CdnPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.7.1.min.js";
            jQuery.CdnDebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.7.1.js";

            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", jQuery);
            System.Data.SqlClient.SqlDependency.Start(DBConnectionLayer.DBConnection.GetDBConnection());
        }

        void Application_End(object sender, EventArgs e)
        {

            System.Data.SqlClient.SqlDependency.Stop(DBConnectionLayer.DBConnection.GetDBConnection());
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
    }
}
