using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class KombinacijaObroka
    {
        public int IDKombinacijeObroka { get; set; }
        public DateTime VrijediOd { get; set; }
        public DateTime VrijediDo { get; set; }
        public List<ObrokUKombinaciji> Obroci { get; set; }
        public int BrojObroka
        {
            get => Obroci != null ? Obroci.Count() : 0;
        }

        public override bool Equals(object obj) => (obj is KombinacijaObroka) ? (obj as KombinacijaObroka).IDKombinacijeObroka.Equals(IDKombinacijeObroka) : false;
        public override int GetHashCode() => IDKombinacijeObroka.GetHashCode();
    }
}
