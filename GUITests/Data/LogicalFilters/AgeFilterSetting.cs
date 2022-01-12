using Filter;
using Filter.Filters;
using System;
using System.Windows.Media;

namespace GUITests.Data.LogischeFilters
{
    public class AgeFilterSetting : ILogicalFilterSettings<Friend, int>
    {
        public Func<Friend, int> PropertyFromDataset { get; set; }
        public string Title { get; set; }
        public string Shortcut { get; set; }
        public FilterOption FilterOptions { get; set; }
        public Icon Icon { get; set; }

        public AgeFilterSetting()
        {
            Title = "Age";
            Shortcut = "AG";
            Icon = new Icon(Brushes.Green.ToString(), Icons.Alertbericht);
            PropertyFromDataset = p => p.Age;
        }
    }
}
