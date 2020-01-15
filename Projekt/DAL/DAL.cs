using DAL.Model;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace DAL
{
    public class DAL
    {
        public static string serverTempFilePath = $"{Path.GetTempPath()}\\Detalji.csv";
        public static string cs = "Server=.\\SQLEXPRESS;Database=PRARWA_Projekt;Uid=sa;Pwd=SQL";

        private static IList<Spol> spolovi = null;
        private static IList<Korisnik> korisnici = null;
        private static IList<Namirnica> namirnice = null;
        private static IList<NazivObroka> naziviObroka = null;
        private static IList<TipNamirnice> tipoviNamirnica = null;
        private static IList<KombinacijaObroka> kombinacije = null;
        private static IList<MjernaJedinica> mjerneJedinice = null;
        private static IList<TipDijabetesa> tipoviDijabetesa = null;
        private static IList<RazinaFizickeAktivnosti> razineFizickeAktivnosti = null;


        // Administrator.
        public static bool AutenticirajAdministratora(string KorisnickoIme, string Zaporka)
        {
            try
            {
                DataSet ds = SqlHelper.ExecuteDataset(cs, "LoginAdministrator", KorisnickoIme, Zaporka);

                switch ((int)ds.Tables[0].Rows[0]["Izlaz"])
                {
                    case 1:
                        // Uspjeh!
                        return true;
                    case 2:
                        // Korisničko ime ne postoji!
                        return false;
                    case 3:
                        // Lozinka nije ispravna!
                        return false;
                    default:
                        // Ovo se nije trebalo dogoditi!
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool JeLiAdminAutenticiran(HttpSessionState Session, HttpServerUtility Server)
        {
            if (Session["Username"] != null && Session["Password"] != null)
            {
                var username = Server.UrlDecode(Session["Username"].ToString());
                var password = Server.UrlDecode(Session["Password"].ToString());

                if (AutenticirajAdministratora(username, password))
                {
                    return true;
                }
            }
            return false;
        }


        // Korisnik.
        public static bool RegistrirajKorisnika(Korisnik k)
        {
            int value = (int)SqlHelper.ExecuteScalar(cs, "RegisterUser", k.Ime, k.Prezime, k.Email, k.Lozinka, k.Visina, k.Tezina, k.DatumRodjenja, k.SpolID, k.TipDijabetesaID, k.RazinaFizickeAktivnostiID);

            if (value == 1)
            {
                korisnici = null;
                return true;
            }
            return false;
        }

        public static bool AutenticirajKorisnika(string Email, string Lozinka)
        {
            try
            {
                DataSet ds = SqlHelper.ExecuteDataset(cs, "LoginUser", Email, Lozinka);

                switch ((int)ds.Tables[0].Rows[0]["Izlaz"])
                {
                    case 1:
                        // Uspjeh!
                        return true;
                    case 2:
                        // Korisničko ime ne postoji!
                        return false;
                    case 3:
                        // Lozinka nije ispravna!
                        return false;
                    default:
                        // Ovo se nije trebalo dogoditi!
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool JeLiKorisnikAutenticiran(HttpSessionStateBase session, HttpServerUtilityBase server)
        {
            if (session["Username"] != null && session["Password"] != null)
            {
                var username = server.UrlDecode(session["Username"].ToString());
                var password = server.UrlDecode(session["Password"].ToString());

                if (AutenticirajKorisnika(username, password))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool UrediKorisnika(Korisnik k)
        {
            int value = (int)SqlHelper.ExecuteScalar(cs, "UrediKorisnika", k.Ime, k.Prezime, k.Email, k.Lozinka, k.Visina, k.Tezina, k.DatumRodjenja, k.SpolID, k.TipDijabetesaID, k.RazinaFizickeAktivnostiID);

            if (value == 1)
            {
                korisnici = null;
                return true;
            }
            return false;

        }

        public static Korisnik DohvatiKorisnika(string Email, string Lozinka)
        {
            DataSet ds = SqlHelper.ExecuteDataset(cs, "DohvatiKorisnika", Email, Lozinka);

            DataRow row = ds.Tables[0].Rows[0];

            if (row[0] == null)
            {
                return null;
            }

            try
            {
                return new Korisnik
                {
                    IDKorisnik = (int)row["IDKorisnik"],
                    Ime = row["Ime"].ToString(),
                    Prezime = row["Prezime"].ToString(),
                    Email = row["Email"].ToString(),
                    Visina = float.Parse(row["Visina"].ToString()),
                    Tezina = float.Parse(row["Tezina"].ToString()),
                    DatumRodjenja = DateTime.Parse(row["DatumRodjenja"].ToString()),
                    SpolID = (int)row["SpolID"],
                    RazinaFizickeAktivnostiID = (int)row["RazinaFizickeAktivnostiID"],
                    TipDijabetesaID = (int)row["TipDijabetesaID"]
                };
            }
            catch
            {
                return null;
            }
        }

        public static IEnumerable<Korisnik> DohvatiSveKorisnike()
        {
            if (korisnici != null && korisnici.Count > 0)
            {
                return korisnici;
            }

            DataSet ds = SqlHelper.ExecuteDataset(cs, "DohvatiSveKorisnike");

            korisnici = new List<Korisnik>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                korisnici.Add(new Korisnik()
                {
                    IDKorisnik = (int)row["IDKorisnik"],
                    Ime = row["Ime"].ToString(),
                    Prezime = row["Prezime"].ToString(),
                    Email = row["Email"].ToString(),
                    Visina = float.Parse(row["Visina"].ToString()),
                    Tezina = float.Parse(row["Tezina"].ToString()),
                    DatumRodjenja = DateTime.Parse(row["DatumRodjenja"].ToString()),
                    SpolID = (int)row["SpolID"],
                    RazinaFizickeAktivnostiID = (int)row["RazinaFizickeAktivnostiID"],
                    TipDijabetesaID = (int)row["TipDijabetesaID"]
                });
            }
            return korisnici;
        }

        public static bool IspisiSveKorisnikeUDatoteku(string path, string delim)
        {
            try
            {
                DataSet ds = SqlHelper.ExecuteDataset(cs, "DohvatiSveKorisnikeDetalji");

                var data = new List<string>();
                data.Add($"IDKorisnik{delim}Ime{delim}Prezime{delim}Email{delim}Visina{delim}Tezina{delim}DatumRodjenja{delim}Spol{delim}RazinaFizickeAktivnosti{delim}TipDijabetesa");

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    data.Add($"{row["IDKorisnik"]}{delim}{row["Ime"]}{delim}{row["Prezime"]}{delim}{row["Email"]}{delim}{row["Visina"]}{delim}{row["Tezina"]}{delim}{(row["DatumRodjenja"] as DateTime?).Value.ToShortDateString()}{delim}{row["Spol"]}{delim}{row["RazinaFizickeAktivnosti"]}{delim}{row["TipDijabetesa"]}");
                }

                File.WriteAllLines(path, data, Encoding.UTF8);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static double IzracunajDnevnuPotrebuEnergije(Korisnik korisnik)
        {
            var godine = DateTime.Now.Year - korisnik.DatumRodjenja.Year;
            var f_spol = korisnik.SpolID == 1 ? 5 : -161;
            var f_aktivnost = korisnik.RazinaFizickeAktivnostiID == 1 ? 1.2 : korisnik.RazinaFizickeAktivnostiID == 2 ? 1.375 : 1.5;
            var f_tip_dijabetesa = korisnik.TipDijabetesaID == 1 ? 0.99 : 0.98;

            double rezultat = ((10 * korisnik.Tezina) + (6.25 * korisnik.Visina) - (5 * (godine)) + (f_spol)) *
                f_aktivnost * f_tip_dijabetesa;

            return rezultat;
        }

        // Mjerne jedinice.
        public static int DodajMjernuJedinicu(MjernaJedinica mj)
        {
            var result = SqlHelper.ExecuteNonQuery(cs, "DodajMjernuJedinicu", mj.Naziv);
            mjerneJedinice = null;
            return result;
        }

        public static int UrediMjernuJedinicu(MjernaJedinica mj)
        {
            var result = SqlHelper.ExecuteNonQuery(cs, "UrediMjernuJedinicu", mj.IDMjerneJedinice, mj.Naziv);
            mjerneJedinice = null;
            return result;
        }

        public static int UkloniMjernuJedinicu(int mjernaJedinicaID)
        {
            var result = SqlHelper.ExecuteNonQuery(cs, "UkloniMjernuJedinicu", mjernaJedinicaID);
            mjerneJedinice = null;
            return result;
        }

        public static IEnumerable<MjernaJedinica> DohvatiMjerneJedinice()
        {
            if (mjerneJedinice != null && mjerneJedinice.Count > 0)
            {
                return mjerneJedinice;
            }

            DataSet ds = SqlHelper.ExecuteDataset(cs, "DohvatiMjerneJedinice");

            mjerneJedinice = new List<MjernaJedinica>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                mjerneJedinice.Add(new MjernaJedinica()
                {
                    IDMjerneJedinice = (int)row["IDMjernaJedinica"],
                    Naziv = row["Naziv"].ToString()
                });
            }
            return mjerneJedinice;
        }


        // Mjerne jedinice za namirnicu.
        public static IEnumerable<MjernaJedinica> DohvatiMjerneJediniceZaNamirnicu(int IDNamirnica)
        {
            DataSet ds = SqlHelper.ExecuteDataset(cs, "DohvatiMjerneJediniceZaNamirnicu", IDNamirnica);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                yield return new MjernaJedinica()
                {
                    IDMjerneJedinice = (int)row["IDMjernaJedinica"],
                    Naziv = row["Naziv"].ToString(),
                    TezinaUGramima = (int)row["TezinaUGramima"]
                };
            }
        }

        public static int UkloniMjernuJedinicuZaNamirnicu(int namirnicaID, int mjernaJedinicaID)
        {
            return SqlHelper.ExecuteNonQuery(cs, "UkloniMjernuJedinicuZaNamirnicu", namirnicaID, mjernaJedinicaID);
        }

        public static int DodajMjernuJedinicuZaNamirnicu(int namirnicaID, MjernaJedinica m)
        {
            return SqlHelper.ExecuteNonQuery(cs, "DodajMjernuJedinicuZaNamirnicu",
                namirnicaID,
                m.IDMjerneJedinice,
                m.TezinaUGramima);
        }

        public static int UrediMjernuJedinicuZaNamirnicu(int namirnicaID, MjernaJedinica m)
        {
            return SqlHelper.ExecuteNonQuery(cs, "UrediMjernuJedinicuZaNamirnicu", namirnicaID, m.IDMjerneJedinice, m.TezinaUGramima);
        }


        // Namirnice.
        public static int DodajNamirnicu(Namirnica n)
        {
            var result = SqlHelper.ExecuteNonQuery(cs, "DodajNamirnicu",
                n.Naziv,
                n.TipNamirniceID,
                n.EnergetskaVrijednostKcalPoGramu,
                n.EnergetskaVrijednostKJPoGramu);
            namirnice = null;
            return result;
        }

        public static int UrediNamirnicu(Namirnica n)
        {
            var result = SqlHelper.ExecuteNonQuery(cs, "UrediNamirnicu",
                n.IDNamirnice,
                n.Naziv,
                n.EnergetskaVrijednostKcalPoGramu,
                n.EnergetskaVrijednostKJPoGramu,
                n.TipNamirniceID);
            namirnice = null;
            return result;
        }

        public static int UkloniNamirnicu(int namirnicaID)
        {
            var result = SqlHelper.ExecuteNonQuery(cs, "UkloniNamirnicu", namirnicaID);
            namirnice = null;
            return result;
        }

        public static IEnumerable<Namirnica> DohvatiNamirnice()
        {
            if (namirnice != null && namirnice.Count > 0)
            {
                return namirnice;
            }

            DataSet ds = SqlHelper.ExecuteDataset(cs, "DohvatiNamirnice");

            namirnice = new List<Namirnica>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                namirnice.Add(new Namirnica()
                {
                    IDNamirnice = (int)row["IDNamirnica"],
                    Naziv = row["Naziv"].ToString(),
                    TipNamirniceID = (int)row["TipNamirniceID"],
                    EnergetskaVrijednostKcalPoGramu = float.Parse(row["EnergetskaVrijednostKcalPoGramu"].ToString()),
                    EnergetskaVrijednostKJPoGramu = float.Parse(row["EnergetskaVrijednostKJPoGramu"].ToString()),
                    MjerneJedinice = DohvatiMjerneJediniceZaNamirnicu((int)row["IDNamirnica"])
                });
            }
            return namirnice;
        }


        // Nazivi obroka.
        public static int DodajNazivObroka(NazivObroka n)
        {
            var result = SqlHelper.ExecuteNonQuery(cs, "DodajNazivObroka", n.Naziv);
            naziviObroka = null;
            return result;
        }

        public static int UrediNazivObroka(NazivObroka n)
        {
            var result = SqlHelper.ExecuteNonQuery(cs, "UrediNazivObroka", n.IDNazivObroka, n.Naziv);
            naziviObroka = null;
            return result;
        }

        public static int UkloniNazivObroka(int nazivObrokaID)
        {
            var result = SqlHelper.ExecuteNonQuery(cs, "UkloniNazivObroka", nazivObrokaID);
            naziviObroka = null;
            return result;
        }

        public static IEnumerable<NazivObroka> DohvatiNaziveObroka()
        {
            if (naziviObroka != null && naziviObroka.Count > 0)
            {
                return naziviObroka;
            }

            DataSet ds = SqlHelper.ExecuteDataset(cs, "DohvatiNaziveObroka");

            naziviObroka = new List<NazivObroka>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                naziviObroka.Add(new NazivObroka()
                {
                    IDNazivObroka = (int)row["IDNazivObroka"],
                    Naziv = row["Naziv"].ToString()
                });
            }
            return naziviObroka;
        }



        // Kombinacije.
        public static void DodajKombinaciju(KombinacijaObroka k)
        {
            var IDKombinacija = Convert.ToInt32(SqlHelper.ExecuteScalar(cs, "DodajKombinaciju", k.VrijediOd));

            foreach (var o in k.Obroci)
            {
                SqlHelper.ExecuteNonQuery(cs, "DodajObrokKombinaciji", IDKombinacija, o.NazivObroka.IDNazivObroka, o.UdioBjelancevina, o.UdioMasti, o.UdioUgljikohidrata, o.DnevniUdio);
            }
        }

        public static int UkloniKombinaciju(KombinacijaObroka k)
        {
            return SqlHelper.ExecuteNonQuery(cs, "UkloniKombinaciju", k.IDKombinacijeObroka, k.VrijediDo);
        }

        public static KombinacijaObroka DohvatiKombinaciju(int IDKombinacija)
        {
            DataSet ds = SqlHelper.ExecuteDataset(cs, "DohvatiKombinaciju", IDKombinacija);

            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                var table0row = ds.Tables[0].Rows[0];

                var isValid = DateTime.TryParse(table0row["VrijediDo"].ToString(), out DateTime vrijediDo);

                var k = new KombinacijaObroka
                {
                    IDKombinacijeObroka = (int)table0row["IDKombinacijaObroka"],
                    VrijediOd = DateTime.Parse(table0row["VrijediOd"].ToString()),
                    VrijediDo = isValid ? vrijediDo : DateTime.MaxValue
                };

                k.Obroci = new List<ObrokUKombinaciji>();

                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    k.Obroci.Add(new ObrokUKombinaciji
                    {
                        IDObrokUKombinaciji = (int)row["IDObrokUKombinaciji"],
                        NazivObroka = DohvatiNaziveObroka().First(x => x.IDNazivObroka == (int)row["NazivObrokaID"]),
                        UdioBjelancevina = (int)row["UdioBjelancevina"],
                        UdioMasti = (int)row["UdioMasti"],
                        UdioUgljikohidrata = (int)row["UdioUgljikohidrata"],
                        DnevniUdio = (int)row["DnevniUdio"]
                    });
                }
                return k;
            }

            return null;
        }

        public static KombinacijaObroka DohvatiKombinacijuZaBrojObroka(int brojObroka)
        {
            DataSet ds = SqlHelper.ExecuteDataset(cs, "DohvatiKombinacijuZaBrojObroka", brojObroka);

            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                var table0row = ds.Tables[0].Rows[0];

                var isValid = DateTime.TryParse(table0row["VrijediDo"].ToString(), out DateTime vrijediDo);

                var k = new KombinacijaObroka
                {
                    IDKombinacijeObroka = (int)table0row["IDKombinacijaObroka"],
                    VrijediOd = DateTime.Parse(table0row["VrijediOd"].ToString()),
                    VrijediDo = isValid ? vrijediDo : DateTime.MaxValue
                };

                k.Obroci = new List<ObrokUKombinaciji>();

                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    k.Obroci.Add(new ObrokUKombinaciji
                    {
                        IDObrokUKombinaciji = (int)row["IDObrokUKombinaciji"],
                        NazivObroka = DohvatiNaziveObroka().First(x => x.IDNazivObroka == (int)row["NazivObrokaID"]),
                        UdioBjelancevina = (int)row["UdioBjelancevina"],
                        UdioMasti = (int)row["UdioMasti"],
                        UdioUgljikohidrata = (int)row["UdioUgljikohidrata"],
                        DnevniUdio = (int)row["DnevniUdio"]
                    });
                }

                return k;
            }

            return null;
        }

        public static IEnumerable<KombinacijaObroka> DohvatiKombinacije()
        {
            if (kombinacije != null && kombinacije.Count > 0)
            {
                return kombinacije;
            }

            DataSet ds = SqlHelper.ExecuteDataset(cs, "DohvatiKombinacije");

            kombinacije = new List<KombinacijaObroka>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var isValid = DateTime.TryParse(row["VrijediDo"].ToString(), out DateTime vrijediDo);

                kombinacije.Add(new KombinacijaObroka()
                {
                    IDKombinacijeObroka = (int)row["IDKombinacijaObroka"],
                    VrijediOd = DateTime.Parse(row["VrijediOd"].ToString()),
                    VrijediDo = isValid ? vrijediDo : DateTime.MaxValue,
                    Obroci = new List<ObrokUKombinaciji>()
                });
            }

            foreach (DataRow row in ds.Tables[1].Rows)
            {
                var obrokUKombinaciji = new ObrokUKombinaciji
                {
                    IDObrokUKombinaciji = (int)row["KombinacijaObrokaID"],
                    NazivObroka = DohvatiNaziveObroka().First(x => x.IDNazivObroka == (int)row["NazivObrokaID"]),
                    DnevniUdio = (int)row["DnevniUdio"],
                    UdioBjelancevina = (int)row["UdioBjelancevina"],
                    UdioMasti = (int)row["UdioMasti"],
                    UdioUgljikohidrata = (int)row["UdioUgljikohidrata"]
                };

                kombinacije.First(x => x.IDKombinacijeObroka == (int)row["KombinacijaObrokaID"]).Obroci.Add(obrokUKombinaciji);
            }

            return kombinacije;
        }


        // Tipovi namirnica.
        public static IEnumerable<TipNamirnice> DohvatiTipoveNamirnica()
        {
            if (tipoviNamirnica != null && tipoviNamirnica.Count > 0)
            {
                return tipoviNamirnica;
            }

            DataSet ds = SqlHelper.ExecuteDataset(cs, "DohvatiTipoveNamirnica");

            tipoviNamirnica = new List<TipNamirnice>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                tipoviNamirnica.Add(new TipNamirnice()
                {
                    IDTipNamirnice = (int)row["IDTipNamirnice"],
                    Naziv = row["Naziv"].ToString()
                });
            }
            return tipoviNamirnica;
        }


        // Spolovi.
        public static IEnumerable<Spol> DohvatiSpolove()
        {
            if (spolovi != null && spolovi.Count > 0)
            {
                return spolovi;
            }

            DataSet ds = SqlHelper.ExecuteDataset(cs, "DohvatiSpolove");

            spolovi = new List<Spol>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                spolovi.Add(new Spol()
                {
                    IDSpol = (int)row["IDSpol"],
                    Naziv = row["Naziv"].ToString()
                });
            }
            return spolovi;
        }


        // Tipovi dijabetesa.
        public static IEnumerable<TipDijabetesa> DohvatiTipoveDijabetesa()
        {
            if (tipoviDijabetesa != null && tipoviDijabetesa.Count > 0)
            {
                return tipoviDijabetesa;
            }

            DataSet ds = SqlHelper.ExecuteDataset(cs, "DohvatiTipoveDijabetesa");

            tipoviDijabetesa = new List<TipDijabetesa>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                tipoviDijabetesa.Add(new TipDijabetesa()
                {
                    IDTipDijabetesa = (int)row["IDTipDijabetesa"],
                    Naziv = row["Naziv"].ToString()
                });
            }
            return tipoviDijabetesa;
        }


        // Tipovi dijabetesa.
        public static IEnumerable<RazinaFizickeAktivnosti> DohvatiRazineFizickeAktivnosti()
        {
            if (razineFizickeAktivnosti != null && razineFizickeAktivnosti.Count > 0)
            {
                return razineFizickeAktivnosti;
            }

            DataSet ds = SqlHelper.ExecuteDataset(cs, "DohvatiRazineFizickeAktivnosti");

            razineFizickeAktivnosti = new List<RazinaFizickeAktivnosti>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                razineFizickeAktivnosti.Add(new RazinaFizickeAktivnosti()
                {
                    IDRazinaFizickeAktivnosti = (int)row["IDRazinaFizickeAktivnosti"],
                    Naziv = row["Naziv"].ToString()
                });
            }
            return razineFizickeAktivnosti;
        }


        // Jelovnik.
        public static bool DodajJelovnik(Jelovnik jelovnik, int korisnikID, DateTime datum)
        {
            try
            {
                int jelovnikID = int.Parse(SqlHelper.ExecuteScalar(cs, "DodajJelovnik", korisnikID, datum.Date).ToString());

                foreach (var obrok in jelovnik.Obroci)
                {
                    int obrokID = int.Parse(SqlHelper.ExecuteScalar(cs, "DodajObrokJelovniku", jelovnikID, obrok.NazivObroka.IDNazivObroka).ToString());

                    foreach (var namirnica in obrok.Namirnice)
                    {
                        SqlHelper.ExecuteScalar(cs, "DodajNamirnicuObroku", obrokID, namirnica.Namirnica.IDNamirnice, namirnica.Kolicina);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Jelovnik DohvatiJelovnikKorisnikaZaDatum(int korisnikID, DateTime datum)
        {
            DataSet ds = SqlHelper.ExecuteDataset(cs, "DohvatiJelovnikKorisnikaZaDatum", korisnikID, datum);

            Jelovnik jelovnik = null;
            if (ds != null && ds.Tables.Count == 3 && ds.Tables[0].Rows.Count != 0 && ds.Tables[1].Rows.Count != 0 && ds.Tables[2].Rows.Count != 0)
            {
                jelovnik = new Jelovnik();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    jelovnik.IDJelovnik = (int)row["IDJelovnik"];
                    jelovnik.KorisnikID = (int)row["KorisnikID"];
                    jelovnik.Datum = DateTime.Parse(row["Datum"].ToString());
                }
                jelovnik.Obroci = new List<Obrok>();

                var jId = 1;
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    jelovnik.Obroci.Add(new Obrok
                    {
                        IDUJelovniku = jId++,
                        IDObrok = (int)row["IDObrok"],
                        NazivObroka = DohvatiNaziveObroka().First(x => x.IDNazivObroka == (int)row["NazivObrokaID"]),
                        Namirnice = new List<NamirnicaUObroku>()
                    });
                }

                foreach (DataRow row in ds.Tables[2].Rows)
                {
                    var obrokID = (int)row["ObrokID"];
                    jelovnik.Obroci.First(x => x.IDObrok == obrokID).Namirnice.Add(new NamirnicaUObroku
                    {
                        Kolicina = float.Parse(row["Kolicina"].ToString()),
                        Namirnica = DohvatiNamirnice().First(x => x.IDNamirnice == (int)row["NamirnicaID"])
                    });
                }
            }
            return jelovnik;
        }
    }
}
