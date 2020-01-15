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
    public partial class BasePage : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            string culture = Session["Language"]?.ToString();

            if (string.IsNullOrEmpty(culture))
            {
                culture = "Auto";
            }

            if (culture != "Auto")
            {
                var cultureInfo = new CultureInfo(culture);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }

            base.InitializeCulture();
        }
    }
}