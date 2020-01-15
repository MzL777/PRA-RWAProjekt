using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublicSite.Controllers
{
    public class AJAXController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            // Redirect na _Error kontroler.
            ViewResult view = new ViewResult();
            view.ViewName = "_Error";
            view.ViewBag.Exception = filterContext.Exception;
            filterContext.Result = view;
            filterContext.ExceptionHandled = true;
        }

        [HttpPost]
        public ActionResult BtnGenerirajClick()
        {
            Session["Generate"] = true;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult BtnSpremiClick()
        {
            Session["Save"] = true;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DateChanged()
        {
            Session["Jelovnik"] = null;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ZadrziChanged(string ident, string value)
        {
            var jelovnik = Session["Jelovnik"] as Jelovnik;

            var split = ident.Split('_');

            // Ako je kliknuto na checkbox namirnice primijeni vrijednost varijable Zadrzi te namirnice.
            if (split.Count() == 2)
            {
                jelovnik.Obroci.First(x => x.IDObrok == int.Parse(split[0])).Namirnice.First(x => x.Namirnica.IDNamirnice == int.Parse(split[1])).Zadrzi = bool.Parse(value);

            }
            // Ako je kliknuto na checkbox obroka primijeni vrijednost varijable Zadrzi za sve namirnice u tom obroku.
            else if (split.Count() == 1)
            {
                jelovnik.Obroci.First(x => x.IDObrok == int.Parse(split[0])).Namirnice.ToList().ForEach(x => x.Zadrzi = bool.Parse(value));
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}