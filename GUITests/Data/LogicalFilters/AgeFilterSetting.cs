using Filter;
using Filter.Filters;
using System;
using System.Windows.Media;

namespace GUITests.Data.LogischeFilters
{
    public class AgeFilterSetting : ILogischeFilterInstellingen<Friend, int>
    {
        public Func<Friend, int> PropertyUitDataGrid { get; set; }
        public string Titel { get; set; }
        public string Shortcut { get; set; }
        public FilterOptie FilterOpties { get; set; }
        public Icon Icon { get; set; }

        public AgeFilterSetting()
        {
            Titel = "Age";
            Shortcut = "AG";
            Icon = new Icon(Brushes.Green.ToString(), Icons.Alertbericht);
            PropertyUitDataGrid = p => p.Age;
        }
    }
}
