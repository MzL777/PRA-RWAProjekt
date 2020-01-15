using DAL.Model;
using PublicSite.Models;
using PublicSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublicSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

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
        public ActionResult Jelovnik()
        {
            Session["Jelovnik"] = null;
            Session["Site"] = HttpContext.Request.Url.AbsolutePath;

            if (DAL.DAL.JeLiKorisnikAutenticiran(Session, Server))
            {
                ViewBag.kombinacije = DAL.DAL.DohvatiKombinacije().ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        public ActionResult Jelovnik(JelovnikPreGenerateData model)
        {
            Session["MjerneJedinice"] = null;
            Session["Site"] = HttpContext.Request.Url.AbsolutePath;

            if (DAL.DAL.JeLiKorisnikAutenticiran(Session, Server))
            {
                var korisnik = DAL.DAL.DohvatiKorisnika(Session["Username"].ToString(), Session["Password"].ToString());

                Session["SelectedObroka"] = model.BrojObroka;
                Session["Date"] = model.Datum;
                ViewBag.saveDisabled = true;

                // Ako je korisnik kliknuo na gumb "Spremi" spremi jelovnik u bazu.
                if (Session["Save"] != null && (bool)Session["Save"])
                {
                    Session["Save"] = null;
                    if (DAL.DAL.DodajJelovnik(Session["Jelovnik"] as Jelovnik, korisnik.IDKorisnik, model.Datum.Date))
                    {
                        Session["Jelovnik"] = null;
                    }
                    else
                    {
                        throw new Exception("Jelovnik nije moguće spremiti!");
                    }
                }

                // Ako je odabrani datum u prošlosti, onemogući gumb "Generiraj".
                if (model.Datum.Date >= DateTime.Now.Date)
                {
                    ViewBag.genDisabled = false;
                }
                else
                {
                    ViewBag.genDisabled = true;
                    Session["Jelovnik"] = null;
                }

                // Ako se jelovnik prvi put prikazuje za današnji datum.
                if (Session["Jelovnik"] == null)
                {
                    var danasnjiJelovnik = DAL.DAL.DohvatiJelovnikKorisnikaZaDatum(korisnik.IDKorisnik, DateTime.Parse(Session["Date"].ToString()));

                    // Ako u bazi postoji generiran jelovnik za današnji datum, prikaži ga...
                    if (danasnjiJelovnik != null)
                    {
                        model.Jelovnik = danasnjiJelovnik;
                        Session["Jelovnik"] = model.Jelovnik;
                        ViewBag.saveDisabled = true;
                        ViewBag.genDisabled = true;
                        ViewBag.mjerneJedinice = DohvatiMjerneJediniceZaJelovnik(model.Jelovnik);

                    }
                    // ... u suprotnom generiraj novi.
                    else if (Session["Generate"] != null && (bool)Session["Generate"])
                    {
                        model.Jelovnik = GenerirajJelovnik();
                        Session["Jelovnik"] = model.Jelovnik;
                        Session["Generate"] = null;
                        ViewBag.mjerneJedinice = DohvatiMjerneJediniceZaJelovnik(model.Jelovnik);
                        ViewBag.saveDisabled = false;
                    }
                }
                // Ako je kliknuto da se jelovnik ponovno generira za današnji datum.
                else
                {
                    model.Jelovnik = ReGenerirajJelovnik();
                    Session["Jelovnik"] = model.Jelovnik;
                    ViewBag.mjerneJedinice = DohvatiMjerneJediniceZaJelovnik(model.Jelovnik);
                    ViewBag.saveDisabled = false;
                }

                ViewBag.kombinacije = DAL.DAL.DohvatiKombinacije().ToList();

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        private IEnumerable<MjernaJedinica> DohvatiMjerneJediniceZaJelovnik(Jelovnik j)
        {
            // Dohvati mjerne jedinice svim namirnica koje su određene u proslijeđenom jelovniku.
            var mjerneJedinice = new List<MjernaJedinica>();

            foreach (var obrok in j.Obroci)
            {
                foreach (var namirnica in obrok.Namirnice)
                {
                    mjerneJedinice = mjerneJedinice.Union(DAL.DAL.DohvatiMjerneJediniceZaNamirnicu(namirnica.Namirnica.IDNamirnice)).ToList();
                }
            }

            return mjerneJedinice;
        }

        private Jelovnik ReGenerirajJelovnik()
        {
            Session["MjerneJedinice"] = null;

            var random = new Random(DateTime.Now.Millisecond);
            var selectedValue = (int)Session["SelectedObroka"];
            var selectedDate = DateTime.Parse(Session["Date"].ToString());

            var korisnik = DAL.DAL.DohvatiKorisnika(Session["Username"].ToString(), Session["Password"].ToString());
            var energija = DAL.DAL.IzracunajDnevnuPotrebuEnergije(korisnik);

            var kombinacija = DAL.DAL.DohvatiKombinacijuZaBrojObroka(selectedValue);
            var namirnice = DAL.DAL.DohvatiNamirnice();

            var jelovnik = Session["Jelovnik"] as Jelovnik;
            var mjerneJedinice = new List<MjernaJedinica>();

            List<Namirnica> excludeNamirnice = jelovnik.Obroci.SelectMany(x => x.Namirnice.Select(y => y.Namirnica)).ToList();

            foreach (var obrok in jelovnik.Obroci)
            {
                for (int i = 0; i < obrok.Namirnice.Count; i++)
                {
                    var obrokUKombinaciji = kombinacija.Obroci[obrok.IDObrok - 1];
                    var udioObrokaOdDnevnePotrebe = ((obrokUKombinaciji.DnevniUdio / 100.0) * energija) / 100.0;

                    // Zadrži one namirnice koje imaju označen checkbox, ostale ponovno generiraj.
                    var namirnicaUObroku = obrok.Namirnice[i];
                    if (!namirnicaUObroku.Zadrzi)
                    {
                        var tip = namirnicaUObroku.Namirnica.TipNamirniceID;
                        var validNamirnice = namirnice
                            .Where(z => z.TipNamirniceID == tip && z.IDNamirnice != namirnicaUObroku.Namirnica.IDNamirnice)
                            .Except(excludeNamirnice);

                        var rndNamirnica = random.Next(1, validNamirnice.Count());

                        obrok.Namirnice[i] = new NamirnicaUObroku
                        {
                            Namirnica = validNamirnice.ElementAt(rndNamirnica),
                            Zadrzi = true,
                            Kolicina = (udioObrokaOdDnevnePotrebe * obrokUKombinaciji.UdioUgljikohidrata) / validNamirnice.ElementAt(rndNamirnica).EnergetskaVrijednostKcalPoGramu
                        };
                        excludeNamirnice.Add(obrok.Namirnice[i].Namirnica);
                    }
                }
            }

            return jelovnik;
        }

        private Jelovnik GenerirajJelovnik()
        {
            Session["MjerneJedinice"] = null;
            
            var random = new Random(DateTime.Now.Millisecond);
            var selectedValue = (int)Session["SelectedObroka"];
            var selectedDate = DateTime.Parse(Session["Date"].ToString());

            var korisnik = DAL.DAL.DohvatiKorisnika(Session["Username"].ToString(), Session["Password"].ToString());
            var energija = DAL.DAL.IzracunajDnevnuPotrebuEnergije(korisnik);

            var kombinacija = DAL.DAL.DohvatiKombinacijuZaBrojObroka(selectedValue);
            var namirnice = DAL.DAL.DohvatiNamirnice();

            var namirniceBjelancevine = namirnice.Where(x => x.TipNamirniceID == 1).ToList();
            var namirniceUgljikohidrati = namirnice.Where(x => x.TipNamirniceID == 2).ToList();
            var namirniceMasti = namirnice.Where(x => x.TipNamirniceID == 3).ToList();

            var mjerneJedinice = new List<MjernaJedinica>();

            var jelovnik = new Jelovnik
            {
                Datum = selectedDate,
                KorisnikID = korisnik.IDKorisnik,
                Obroci = new List<Obrok>()
            };

            int i = 1;
            foreach (var obrok in kombinacija.Obroci)
            {
                var randBjelancevina = random.Next(1, namirniceBjelancevine.Count());
                var randUgljikohidrati = random.Next(1, namirniceUgljikohidrati.Count());
                var randMasti = random.Next(1, namirniceMasti.Count());

                var udioObrokaOdDnevnePotrebe = ((obrok.DnevniUdio / 100.0) * energija) / 100.0;
                
                var noviObrok = new Obrok
                {
                    IDUJelovniku = i,
                    IDObrok = i++,
                    NazivObroka = obrok.NazivObroka,
                    Zadrzi = true,
                    Namirnice = new List<NamirnicaUObroku>()
                };

                // Dodaj po jednu namirnicu od svakog tipa u obrok.
                noviObrok.Namirnice.Add(new NamirnicaUObroku
                {
                    Namirnica = namirniceBjelancevine.ElementAt(randBjelancevina),
                    Zadrzi = true,
                    Kolicina = (udioObrokaOdDnevnePotrebe * obrok.UdioBjelancevina) / namirniceBjelancevine.ElementAt(randBjelancevina).EnergetskaVrijednostKcalPoGramu
                });


                noviObrok.Namirnice.Add(new NamirnicaUObroku
                {
                    Namirnica = namirniceUgljikohidrati.ElementAt(randUgljikohidrati),
                    Zadrzi = true,
                    Kolicina = (udioObrokaOdDnevnePotrebe * obrok.UdioUgljikohidrata) / namirniceUgljikohidrati.ElementAt(randUgljikohidrati).EnergetskaVrijednostKcalPoGramu
                });


                noviObrok.Namirnice.Add(new NamirnicaUObroku
                {
                    Namirnica = namirniceMasti.ElementAt(randMasti),
                    Zadrzi = true,
                    Kolicina = (udioObrokaOdDnevnePotrebe * obrok.UdioMasti) / namirniceMasti.ElementAt(randMasti).EnergetskaVrijednostKcalPoGramu
                });

                // Ukloni dodane namirnice kako se ne bi ponovile u sljedećim obrocima.
                namirniceBjelancevine.RemoveAt(randBjelancevina);
                namirniceUgljikohidrati.RemoveAt(randUgljikohidrati);
                namirniceMasti.RemoveAt(randMasti);

                jelovnik.Obroci.Add(noviObrok);
            }
            return jelovnik;
        }


        [ChildActionOnly]
        public ActionResult CheckIfDateHasDefinedJelovnik(string date)
        {
            var parsed = DateTime.TryParse(date, out DateTime dateTime);

            if (parsed && DAL.DAL.DohvatiJelovnikKorisnikaZaDatum(DAL.DAL.DohvatiKorisnika(Session["Username"].ToString(), Session["Password"].ToString()).IDKorisnik, dateTime) != null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }

        [ChildActionOnly]
        public ActionResult GetTipNamirnice(int id)
        {
            return Content(DAL.DAL.DohvatiTipoveNamirnica().First(x => x.IDTipNamirnice == id).Naziv);
        }

        [ChildActionOnly]
        public ActionResult GetKolicinaMjerneJediniceZaNamirnicu(NamirnicaUObroku n, MjernaJedinica mj)
        {
            var mjedinica = DAL.DAL.DohvatiMjerneJediniceZaNamirnicu(n.Namirnica.IDNamirnice).FirstOrDefault(x => x.IDMjerneJedinice == mj.IDMjerneJedinice);
            return Content(mjedinica == null ? "-" : ((double)(n.Kolicina / mjedinica.TezinaUGramima)).ToString("0.##"));
        }
    }
}