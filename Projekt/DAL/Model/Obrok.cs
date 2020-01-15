using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Obrok
    {
        public int IDObrok { get; set; }
        public int IDUJelovniku { get; set; }
        public NazivObroka NazivObroka { get; set; }
        public IList<NamirnicaUObroku> Namirnice { get; set; }

        public bool Zadrzi { get; set; }

        public override bool Equals(object obj) => (obj is Obrok) ? (obj as Obrok).IDObrok.Equals(IDObrok) : false;
        public override int GetHashCode() => IDObrok.GetHashCode();
    }
}
