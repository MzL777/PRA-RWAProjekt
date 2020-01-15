using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PublicSite.ViewModels
{
    public class RegisterViewModel
    {
        public int IDKorisnik { get; set; }

        [Required(ErrorMessage = "Niste upisali ime!")]
        [StringLength(127, ErrorMessage = "Maksimalna duljina imena je 127 znakova!")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Niste upisali prezime!")]
        [StringLength(127, ErrorMessage = "Maksimalna duljina prezimena je 127 znakova!")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Niste upisali E-mail!")]
        [StringLength(127, ErrorMessage = "Maksimalna duljina E-mail adrese je 127 znakova!")]
        [EmailAddress(ErrorMessage = "Neispravna E-mail adresa!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Niste upisali lozinku!")]
        [StringLength(100, ErrorMessage = "Maksimalna duljina lozinke je 100 znakova!")]
        public string Lozinka { get; set; }

        [Display(Name = "Potvrdi lozinku")]
        [Required(ErrorMessage = "Niste upisali lozinku!")]
        [StringLength(100, ErrorMessage = "Maksimalna duljina lozinke je 100 znakova!")]
        [Compare(nameof(Lozinka), ErrorMessage = "Lozinke se ne podudaraju, pokušajte ponovno!")]
        public string PotvrdiLozinku { get; set; }

        [Display(Name = "Visina (cm)")]
        [Required(ErrorMessage = "Niste upisali visinu!")]
        public float Visina { get; set; }

        [Display(Name = "Težina (kg)")]
        [Required(ErrorMessage = "Niste upisali težinu!")]
        public float Tezina { get; set; }

        [Display(Name = "Datum rođenja")]
        [DataType(dataType: DataType.Date, ErrorMessage = "Polje datum rođenja mora biti datum!")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Niste upisali datum rođenja!")]
        public DateTime DatumRodjenja { get; set; }

        [Display(Name = "Spol")]
        [Required(ErrorMessage = "Niste odabrali spol!")]
        public int SpolID { get; set; }

        [Display(Name = "Tip dijabetesa")]
        [Required(ErrorMessage = "Niste odabrali tip dijabetesa!")]
        public int TipDijabetesaID { get; set; }

        [Display(Name = "Razina aktivnosti")]
        [Required(ErrorMessage = "Niste odabrali razinu fizičke aktivnosti!")]
        public int RazinaFizickeAktivnostiID { get; set; }

        public Korisnik GetModel() => new Korisnik()
        {
            IDKorisnik = this.IDKorisnik,
            Ime = this.Ime,
            Prezime = this.Prezime,
            Email = this.Email,
            Lozinka = this.Lozinka,
            Visina = this.Visina,
            Tezina = this.Tezina,
            DatumRodjenja = this.DatumRodjenja,
            SpolID = this.SpolID,
            RazinaFizickeAktivnostiID = this.RazinaFizickeAktivnostiID,
            TipDijabetesaID = this.TipDijabetesaID
        };

        public static RegisterViewModel FromModel(Korisnik korisnik) => new RegisterViewModel
        {
            IDKorisnik = korisnik.IDKorisnik,
            Ime = korisnik.Ime,
            Prezime = korisnik.Prezime,
            Email = korisnik.Email,
            Lozinka = korisnik.Lozinka,
            Visina = korisnik.Visina,
            Tezina = korisnik.Tezina,
            DatumRodjenja = korisnik.DatumRodjenja,
            SpolID = korisnik.SpolID,
            RazinaFizickeAktivnostiID = korisnik.RazinaFizickeAktivnostiID,
            TipDijabetesaID = korisnik.TipDijabetesaID
        };
    }
}