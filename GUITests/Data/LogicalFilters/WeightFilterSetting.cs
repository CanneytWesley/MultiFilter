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

    public class WeightFilterSetting : ILogischeFilterInstellingen<Friend, double>
    {
        public Func<Friend, double> PropertyUitDataGrid { get; set; }
        public string Titel { get; set; }
        public string Shortcut { get; set; }
        public FilterOptie FilterOpties { get; set; }
        public Icon Icon { get; set; }

        public WeightFilterSetting()
        {
            Titel = "Weight";
            Shortcut = "W";
            Icon = new Icon(Brushes.Red.ToString(), Icons.Alertbericht);
            FilterOpties = FilterOptie.Exact;
            PropertyUitDataGrid = p => p.Weight;
        }
    }
}
