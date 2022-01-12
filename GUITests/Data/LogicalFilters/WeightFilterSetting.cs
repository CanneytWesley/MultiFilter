using Filter;
using Filter.Filter_Calculator;
using Filter.Filter_Settings;
using Filter.Filters;
using Filter.Filters.Model;
using GUITests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GUITests.Data.LogicalFilters
{

    public class WeightFilterSetting : ILogicalFilterSettings<Friend, double>
    {
        public Func<Friend, double> PropertyFromDataset { get; set; }
        public string Title { get; set; }
        public string Shortcut { get; set; }
        public FilterOption FilterOptions { get; set; }
        public Icon Icon { get; set; }

        public WeightFilterSetting()
        {
            Title = "Weight";
            Shortcut = "W";
            Icon = new Icon(Brushes.Red.ToString(), Icons.Alertbericht);
            FilterOptions = FilterOption.Exact;
            PropertyFromDataset = p => p.Weight;
        }
    }
}
