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

        public string Product { get; set; }
        public string Kwaliteit { get; internal set; }
        public string Certificaat { get; internal set; }

        public double Breedte { get; set; }

        public double Lengte { get; set; }
    }

    public static class SeedLoten
    {
        public static List<Lot> GetSeed()
        {
            return new List<Lot>() { 
                new Lot() {Certificaat = "2.2", Kwaliteit = "A",Product="PL10",  Breedte=2500, Lengte = 12000, Leverancier = "Bakker" },
                new Lot() {Certificaat = "2.2", Kwaliteit = "B",Product="PL10",  Breedte=3000, Lengte = 12000, Leverancier = "Boekhouder" },
                new Lot() {Certificaat = "2.2", Kwaliteit = "C",Product="PL10",  Breedte=2500, Lengte = 13000, Leverancier = "Boekhouder" },
                new Lot() {Certificaat = "2.2", Kwaliteit = "D",Product="PL10",  Breedte=2000, Lengte = 10000, Leverancier = "Slager" },
                new Lot() {Certificaat = "2.2", Kwaliteit = "E",Product="PL20",  Breedte=2500, Lengte = 12000, Leverancier = "Slager" },
                new Lot() {Certificaat = "2.2", Kwaliteit = "E",Product="PL20",  Breedte=2500, Lengte = 12000, Leverancier = "Slager" },
                new Lot() {Certificaat = "3.2", Kwaliteit = "D",Product="PL20",  Breedte=2500, Lengte = 12000, Leverancier = "Groentewinkel" },
                new Lot() {Certificaat = "3.2", Kwaliteit = "A",Product="PL20",  Breedte=2500, Lengte = 12000, Leverancier = "Groentewinkel" },
            };
        }
    }
}
