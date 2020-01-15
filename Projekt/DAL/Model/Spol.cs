using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Spol
    {
        public int IDSpol { get; set; }
        public string Naziv { get; set; }

        public override string ToString() => $"{IDSpol} {Naziv}";
        public override bool Equals(object obj) => (obj is Spol) ? (obj as Spol).IDSpol.Equals(IDSpol) : false;
        public override int GetHashCode() => IDSpol.GetHashCode();
    }
}
