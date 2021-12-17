using Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUITests.Data.Kwaliteiten
{

    public class KwaliteitsData : IData<DBKwaliteit>
    {
        public Func<DBKwaliteit, string> Property { get; set; }
        = p => p.Naam;

        public Task<List<DBKwaliteit>> GetData()
        {
            return Task.FromResult(new List<DBKwaliteit>()
            {
                new DBKwaliteit("A"),
                new DBKwaliteit("B"),
                new DBKwaliteit("C"),
                new DBKwaliteit("D" ),
                new DBKwaliteit("E" ),
                new DBKwaliteit("F" ),
                new DBKwaliteit("G" ),
                new DBKwaliteit("H" ),
                new DBKwaliteit("I" ),
                new DBKwaliteit("J" ),
                new DBKwaliteit("K" ),
                new DBKwaliteit("L" ),
                new DBKwaliteit("M" ),
                new DBKwaliteit("N" ),
                new DBKwaliteit("O" ),
                new DBKwaliteit("P" ),
                new DBKwaliteit("Q" ),
                new DBKwaliteit("R" ),
                new DBKwaliteit("S" ),
                new DBKwaliteit("T" ),
                new DBKwaliteit("U" ),
                new DBKwaliteit("V" ),
                new DBKwaliteit("W" ),
                new DBKwaliteit("X" ),
                new DBKwaliteit("Y" ),
                new DBKwaliteit("Z" ),
            });
        }
    }
}
