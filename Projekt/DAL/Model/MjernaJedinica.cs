using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class MjernaJedinica
    {
        public int IDMjerneJedinice { get; set; }
        public string Naziv { get; set; }
        public int TezinaUGramima { get; set; }

        public override string ToString() => $"{IDMjerneJedinice} {Naziv}";
        public override bool Equals(object obj) => (obj is MjernaJedinica) ? (obj as MjernaJedinica).IDMjerneJedinice.Equals(IDMjerneJedinice) : false;
        public override int GetHashCode() => IDMjerneJedinice.GetHashCode();
    }
}
