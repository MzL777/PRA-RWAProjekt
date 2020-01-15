using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Namirnica
    {
        public int IDNamirnice { get; set; }
        public int TipNamirniceID { get; set; }
        public string Naziv { get; set; }
        public float EnergetskaVrijednostKcalPoGramu { get; set; }
        public float EnergetskaVrijednostKJPoGramu { get; set; }

        public IEnumerable<MjernaJedinica> MjerneJedinice { get; set; }

        public override string ToString() => $"{IDNamirnice}\t{TipNamirniceID}\t {EnergetskaVrijednostKcalPoGramu}kcal\t{EnergetskaVrijednostKJPoGramu}kJ\t{Naziv}";
        public override bool Equals(object obj) => (obj is Namirnica) ? (obj as Namirnica).IDNamirnice.Equals(IDNamirnice) : false;
        public override int GetHashCode() => IDNamirnice.GetHashCode();
    }
}
