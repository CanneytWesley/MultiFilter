using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUITests.Data
{
    public class Lot
    {
        public string Leverancier { get; set; }

        public string Afmetingen { get; set; }
        public string Product { get; set; }
        public string Kwaliteit { get; internal set; }
        public string Certificaat { get; internal set; }
    }

    public static class SeedLoten
    {
        public static List<Lot> GetSeed()
        {
            return new List<Lot>() { 
                new Lot() {Certificaat = "2.2", Kwaliteit = "A",Product="PL10", Afmetingen = "2500x12000", Leverancier = "Bakker" },
                new Lot() {Certificaat = "2.2", Kwaliteit = "B",Product="PL10", Afmetingen = "3000x12000", Leverancier = "Boekhouder" },
                new Lot() {Certificaat = "2.2", Kwaliteit = "C",Product="PL10", Afmetingen = "2500x13000", Leverancier = "Boekhouder" },
                new Lot() {Certificaat = "2.2", Kwaliteit = "D",Product="PL10", Afmetingen = "2000x10000", Leverancier = "Slager" },
                new Lot() {Certificaat = "2.2", Kwaliteit = "E",Product="PL20", Afmetingen = "2500x12000", Leverancier = "Slager" },
                new Lot() {Certificaat = "2.2", Kwaliteit = "E",Product="PL20", Afmetingen = "2500x12000", Leverancier = "Slager" },
                new Lot() {Certificaat = "3.2", Kwaliteit = "D",Product="PL20", Afmetingen = "2500x12000", Leverancier = "Groentewinkel" },
                new Lot() {Certificaat = "3.2", Kwaliteit = "A",Product="PL20", Afmetingen = "2500x12000", Leverancier = "Groentewinkel" },
            };
        }
    }
}
