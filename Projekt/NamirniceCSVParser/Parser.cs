using DAL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamirniceCSVParser
{
    public static class Run
    {
        public static void Main(string[] args)
        {
            try
            {
                var namirnice = Parser.ParseCSVFile("..\\..\\PopisNamirnica.csv");

                namirnice.ToList().ForEach(x => DAL.DAL.DodajNamirnicu(x));

                var dohvaceneNamirnice = DAL.DAL.DohvatiNamirnice();

                dohvaceneNamirnice.ToList().ForEach(Console.WriteLine);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public static class Parser
    {
        public static IEnumerable<Namirnica> ParseCSVFile(string path)
        {
            var linije = File.ReadAllLines(path);

            var tipoviNamirnica = DAL.DAL.DohvatiTipoveNamirnica();

            foreach (var linija in linije)
            {
                if (linija.Equals(linije[0])) continue;

                var data = linija.Split(';');
                yield return new Namirnica()
                {
                    Naziv = data[0],
                    EnergetskaVrijednostKJPoGramu = int.Parse(data[1]) / 100.0f,
                    EnergetskaVrijednostKcalPoGramu = int.Parse(data[2]) / 100.0f,
                    TipNamirniceID = tipoviNamirnica.FirstOrDefault(x => x.Naziv == data[3]).IDTipNamirnice
                };
            }
        }
    }
}
