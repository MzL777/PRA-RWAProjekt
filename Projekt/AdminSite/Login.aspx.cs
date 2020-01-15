using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSite
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Authenticate(txtUsername.Text, txtPass.Text);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Authenticate(txtUsername.Text, txtPass.Text);
        }

        private void Authenticate(string username, string password)
        {
            if (DAL.DAL.JeLiAdminAutenticiran(Session, Server))
            {
                if (Session["Site"] != null)
                {
                    Response.Redirect(Session["Site"].ToString());
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
                return;
            }
            if (DAL.DAL.AutenticirajAdministratora(username, password))
            {
                Session["Username"] = Server.UrlEncode(username);
                Session["Password"] = Server.UrlEncode(password);
            }
        }

        protected override void OnError(EventArgs e)
        {
            var error = Server.UrlEncode(Server.GetLastError().Message);
            Response.Redirect($"Error.aspx?error={error}");
        }
    }
}