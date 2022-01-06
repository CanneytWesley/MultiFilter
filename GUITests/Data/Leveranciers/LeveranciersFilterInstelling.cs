using Filter;
using Filter.Filters;
using GUITests.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GUITests
{
    public class LeveranciersFilterInstelling : IKeuzeFilterInstellingen<Lot, DBLeverancier>
    {
        public Func<DBLeverancier, string> PropertyOmMeeTeFilteren { get; set; }
        public Func<Lot, string> PropertyUitDataGrid { get; set; }
        public string Titel { get; set; }
        public string Shortcut { get; set; }
        public FilterOptie FilterOpties { get; set; }
        public Icon Icon { get; set; }

        public LeveranciersFilterInstelling()
        {
            PropertyOmMeeTeFilteren =  p => p.Naam;
            PropertyUitDataGrid = p => p.Leverancier;
            Titel = "Leveranciers";
            Shortcut = "L";
            FilterOpties = FilterOptie.IndexOf;
            Icon = new Icon(Brushes.Green.ToString(), Icons.Gelukt);
        }

        public Task<List<DBLeverancier>> GetData()
        {
            return Task.FromResult(new List<DBLeverancier>() 
            { 
                new DBLeverancier("Bakker"),
                new DBLeverancier("Boekhouder"),
                new DBLeverancier("Slager"),
                new DBLeverancier("Groentewinkel") 
            });
        }
    }
}
