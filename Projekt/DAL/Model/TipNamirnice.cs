using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class TipNamirnice
    {
        public int IDTipNamirnice { get; set; }
        public string Naziv { get; set; }

        public override string ToString() => $"{IDTipNamirnice} {Naziv}";
        public override bool Equals(object obj) => (obj is TipNamirnice) ? (obj as TipNamirnice).IDTipNamirnice.Equals(IDTipNamirnice) : false;
        public override int GetHashCode() => IDTipNamirnice.GetHashCode();
    }
}
