using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSite
{
    public partial class SiteMaster : MasterPage
    {
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["Username"] = null;
            Session["Password"] = null;
            Session["Site"] = null;

            Response.Redirect("Default.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Session["Username"] = null;
            Session["Password"] = null;
            Session["Site"] = null;

            Response.Redirect("Login.aspx");
        }

        protected override void OnError(EventArgs e)
        {
            var error = Server.UrlEncode(Server.GetLastError().Message);
            Response.Redirect($"Error.aspx?error={error}");
        }

        protected void btnLanguage_Click(object sender, EventArgs e)
        {
            var nc = (sender as LinkButton).Text.Substring(0, 2).ToLower();
            Session["Language"] = nc;
            Response.Redirect(Request.Url.OriginalString);
        }
    }
}