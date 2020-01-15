using PublicSite.Models;
using PublicSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublicSite.Controllers
{
    public class UserController : Controller
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

        [HttpGet]
        public ActionResult Register()
        {
            // Ako je korisnik već prijavljen redirect na Home/Index.
            if (DAL.DAL.JeLiKorisnikAutenticiran(Session, Server))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Spolovi = DAL.DAL.DohvatiSpolove();
            ViewBag.RazineFizickeAktivnosti = DAL.DAL.DohvatiRazineFizickeAktivnosti();
            ViewBag.TipoviDijabetesa = DAL.DAL.DohvatiTipoveDijabetesa();
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel korisnik)
        {
            try
            {
                if (DAL.DAL.RegistrirajKorisnika(korisnik.GetModel()))
                {
                    return View("Info", new CustomMessage("Uspjeh", "Korisnik uspješno registriran!"));
                }
                else
                {
                    return View("Info", new CustomMessage("Pogreška", "Neuspješna registracija! Korisnik s tom email adresom već postoji!"));
                }
            }
            catch (Exception e)
            {
                return View("Info", new CustomMessage("Pogreška", $"Neuspješna registracija!{Environment.NewLine}{e.Message}"));
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            // Ako je korisnik već prijavljen redirect na Home/Index.
            if (DAL.DAL.JeLiKorisnikAutenticiran(Session, Server))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginUser model)
        {
            Session["Username"] = model.Email;
            Session["Password"] = model.Lozinka;

            if (DAL.DAL.JeLiKorisnikAutenticiran(Session, Server))
            {
                if (Session["Site"] != null)
                {
                    return Redirect(Session["Site"].ToString());
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View("Info", new CustomMessage("Pogreška", "Korisnik nije prijavljen!"));
            }
        }

        public ActionResult Logout()
        {
            Session["Username"] = null;
            Session["Password"] = null;

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Info(CustomMessage model)
        {
            return View(model);
        }

        [HttpGet]
        public ActionResult MyProfile()
        {
            Session["Site"] = HttpContext.Request.Url.AbsolutePath;

            // Ako korisnik nije prijavljen redirect na Login stranicu.
            if (!DAL.DAL.JeLiKorisnikAutenticiran(Session, Server))
            {
                return RedirectToAction("Login", "User");
            }

            return View(model: RegisterViewModel.FromModel(DAL.DAL.DohvatiKorisnika(Session["Username"].ToString(), Session["Password"].ToString())));
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            Session["Site"] = HttpContext.Request.Url.AbsolutePath;

            // Ako korisnik nije prijavljen redirect na Login stranicu.
            if (!DAL.DAL.JeLiKorisnikAutenticiran(Session, Server))
            {
                return RedirectToAction("Login", "User");
            }

            ViewBag.Spolovi = DAL.DAL.DohvatiSpolove();
            ViewBag.RazineFizickeAktivnosti = DAL.DAL.DohvatiRazineFizickeAktivnosti();
            ViewBag.TipoviDijabetesa = DAL.DAL.DohvatiTipoveDijabetesa();
            return View(model: RegisterViewModel.FromModel(DAL.DAL.DohvatiKorisnika(Session["Username"].ToString(), Session["Password"].ToString())));
        }

        [HttpPost]
        public ActionResult EditProfile(RegisterViewModel model)
        {
            try
            {
                if (DAL.DAL.UrediKorisnika(model.GetModel()))
                {
                    return View("Info", new CustomMessage("Uspjeh", "Osobni podaci uspješno ažurirani!"));
                }
                return View("Info", new CustomMessage("Pogreška", "Ažuriranje podataka nije uspjelo!"));
            }
            catch (Exception e)
            {
                return View("Info", new CustomMessage("Pogreška", $"Ažuriranje podataka nije uspjelo!{Environment.NewLine}{e.Message}"));
            }

        }


        [ChildActionOnly]
        public ActionResult GetSpol(int id)
        {
            return Content(DAL.DAL.DohvatiSpolove().First(x => x.IDSpol == id).Naziv);
        }

        [ChildActionOnly]
        public ActionResult GetRazinuFizickeAktivnosti(int id)
        {
            return Content(DAL.DAL.DohvatiRazineFizickeAktivnosti().First(x => x.IDRazinaFizickeAktivnosti == id).Naziv);
        }

        [ChildActionOnly]
        public ActionResult GetTipDijabetesa(int id)
        {
            return Content(DAL.DAL.DohvatiTipoveDijabetesa().First(x => x.IDTipDijabetesa == id).Naziv);
        }
    }
}