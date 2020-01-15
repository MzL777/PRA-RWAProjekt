using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class ObrokUKombinaciji
    {
        public int IDObrokUKombinaciji { get; set; }
        public NazivObroka NazivObroka { get; set; }
        public int UdioBjelancevina { get; set; }
        public int UdioMasti { get; set; }
        public int UdioUgljikohidrata { get; set; }
        public int DnevniUdio { get; set; }

        public override string ToString() => $"{IDObrokUKombinaciji} {NazivObroka.Naziv} {UdioBjelancevina} {UdioMasti} {UdioUgljikohidrata} {DnevniUdio}";
        public override bool Equals(object obj) => (obj is ObrokUKombinaciji) ? (obj as ObrokUKombinaciji).IDObrokUKombinaciji.Equals(IDObrokUKombinaciji) : false;
        public override int GetHashCode() => IDObrokUKombinaciji.GetHashCode();
    }
}
