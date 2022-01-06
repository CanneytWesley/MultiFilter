using Filter;
using Filter.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GUITests.Data.LogischeFilters
{
    public class BreedteFilterInstelling : ILogischeFilterInstellingen<Lot, double>
    {
        public Func<Lot, double> PropertyUitDataGrid { get; set; }
        public string Titel { get; set; }
        public string Shortcut { get; set; }
        public FilterOptie FilterOpties { get; set; }
        public Icon Icon { get; set; }

        public BreedteFilterInstelling()
        {
            Titel = "Gereserveerde afmetingen";
            Shortcut = "GA";
            Icon = new Icon(Brushes.Red.ToString(), Icons.Alertbericht);
            FilterOpties = FilterOptie.Exact;
            PropertyUitDataGrid = p => p.Breedte;
        }
    }
}
