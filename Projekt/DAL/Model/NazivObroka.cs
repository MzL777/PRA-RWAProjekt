using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class NazivObroka
    {
        public int IDNazivObroka { get; set; }
        public string Naziv { get; set; }

        public override string ToString() => $"{IDNazivObroka} {Naziv}";
        public override bool Equals(object obj) => (obj is NazivObroka) ? (obj as NazivObroka).IDNazivObroka.Equals(IDNazivObroka) : false;
        public override int GetHashCode() => IDNazivObroka.GetHashCode();
    }
}
