using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class NamirnicaUObroku
    {
        public Namirnica Namirnica { get; set; }
        public double Kolicina { get; set; }

        public bool Zadrzi { get; set; } = true;
    }
}
