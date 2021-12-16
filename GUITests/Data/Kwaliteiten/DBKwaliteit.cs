using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUITests.Data.Kwaliteiten
{
    public class DBKwaliteit
    {
        public string Naam { get; set; }

        public DBKwaliteit(string naam)
        {
            Naam = naam;
        }
    }
}
