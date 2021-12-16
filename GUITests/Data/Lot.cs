﻿using System;
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
        public string Kwaliteit { get; internal set; }
    }

    public static class SeedLoten
    {
        public static List<Lot> GetSeed()
        {
            return new List<Lot>() { 
                new Lot() { Kwaliteit = "A", Afmetingen = "PL10 2500x12000", Leverancier = "Bakker" },
                new Lot() { Kwaliteit = "B", Afmetingen = "PL10 3000x12000", Leverancier = "Boekhouder" },
                new Lot() { Kwaliteit = "C", Afmetingen = "PL10 2500x13000", Leverancier = "Boekhouder" },
                new Lot() { Kwaliteit = "D", Afmetingen = "PL10 2000x10000", Leverancier = "Slager" },
                new Lot() { Kwaliteit = "E", Afmetingen = "PL20 2500x12000", Leverancier = "Slager" },
                new Lot() { Kwaliteit = "E", Afmetingen = "PL20 2500x12000", Leverancier = "Slager" },
                new Lot() { Kwaliteit = "D", Afmetingen = "PL20 2500x12000", Leverancier = "Groentewinkel" },
                new Lot() { Kwaliteit = "A", Afmetingen = "PL20 2500x12000", Leverancier = "Groentewinkel" },
            };
        }
    }
}
