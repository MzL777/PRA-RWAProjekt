using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class TipDijabetesa
    {
        public int IDTipDijabetesa { get; set; }
        public string Naziv { get; set; }

        public override string ToString() => $"{IDTipDijabetesa} {Naziv}";
        public override bool Equals(object obj) => (obj is TipDijabetesa) ? (obj as TipDijabetesa).IDTipDijabetesa.Equals(IDTipDijabetesa) : false;
        public override int GetHashCode() => IDTipDijabetesa.GetHashCode();
    }
}
