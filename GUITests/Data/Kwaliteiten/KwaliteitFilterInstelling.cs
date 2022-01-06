using Filter;
using Filter.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GUITests.Data.Kwaliteiten
{

    public class KwaliteitFilterInstelling : IKeuzeFilterInstellingen<Lot, DBKwaliteit>
    {
        public Func<DBKwaliteit, string> PropertyOmMeeTeFilteren { get; set; }
        public Func<Lot, string> PropertyUitDataGrid { get; set; }
        public string Titel { get; set; }
        public string Shortcut { get; set; }
        public FilterOptie FilterOpties { get; set; }
        public Icon Icon { get; set; }

        public KwaliteitFilterInstelling()
        {
            PropertyOmMeeTeFilteren = p => p.Naam;
            PropertyUitDataGrid = p => p.Kwaliteit;
            Titel = "Kwaliteiten";
            Shortcut = "K";
            FilterOpties = FilterOptie.IndexOf;
            Icon = new Icon(Brushes.Purple.ToString(), Icons.Mislukt);
        }

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
