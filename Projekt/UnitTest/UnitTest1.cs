using System;
using DAL.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestIzracunKalorijskeVrijednosti()
        {
            var korisnik = new Korisnik
            {
                RazinaFizickeAktivnostiID = 1, // Niska razina fizičke aktivnosti.
                SpolID = 1, // Muški spol.
                Tezina = 93,
                Visina = 183,
                TipDijabetesaID = 1, // Tip 1 dijabetesa.
                DatumRodjenja = DateTime.Now.AddYears(-29).AddDays(-1)  // Starost je 29 godina.
            };

            double dnevnaPotrebaEnergije = DAL.DAL.IzracunajDnevnuPotrebuEnergije(korisnik);

            Assert.AreEqual(Math.Round(dnevnaPotrebaEnergije, 2), 2297.30);
        }

        [TestMethod]
        public void TestIzracunKalorijskeVrijednosti2()
        {
            var korisnik = new Korisnik
            {
                RazinaFizickeAktivnostiID = 3, // Visoka razina fizičke aktivnosti.
                SpolID = 2, // Ženski spol.
                Tezina = 93,
                Visina = 183,
                TipDijabetesaID = 2, // Tip 2 dijabetesa.
                DatumRodjenja = DateTime.Now.AddYears(-24).AddDays(-1)  // Starost je 24 godine.
            };

            double dnevnaPotrebaEnergije = DAL.DAL.IzracunajDnevnuPotrebuEnergije(korisnik);

            Assert.AreEqual(Math.Round(dnevnaPotrebaEnergije, 2), 2635.34);
        }
    }
}
