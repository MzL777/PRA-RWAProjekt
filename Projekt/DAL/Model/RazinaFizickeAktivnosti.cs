using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class RazinaFizickeAktivnosti
    {
        public int IDRazinaFizickeAktivnosti { get; set; }
        public string Naziv { get; set; }

        public override string ToString() => $"{IDRazinaFizickeAktivnosti} {Naziv}";
        public override bool Equals(object obj) => (obj is RazinaFizickeAktivnosti) ? (obj as RazinaFizickeAktivnosti).IDRazinaFizickeAktivnosti.Equals(IDRazinaFizickeAktivnosti) : false;
        public override int GetHashCode() => IDRazinaFizickeAktivnosti.GetHashCode();
    }
}
