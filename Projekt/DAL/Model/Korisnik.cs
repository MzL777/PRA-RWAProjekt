using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Korisnik
    {
        public int IDKorisnik { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public float Visina { get; set; }
        public float Tezina { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public int SpolID { get; set; }
        public int TipDijabetesaID { get; set; }
        public int RazinaFizickeAktivnostiID { get; set; }

        public override string ToString() => $"{Ime} {Prezime}, {Email}";
        public override bool Equals(object obj) => obj is Korisnik ? (obj as Korisnik).Email.Equals(this.Email) : false;
        public override int GetHashCode() => this.Email.GetHashCode();
    }
}
