using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Jelovnik
    {
        public int IDJelovnik { get; set; }
        public int KorisnikID { get; set; }
        public DateTime Datum { get; set; }
        public IList<Obrok> Obroci { get; set; }
    }
}
