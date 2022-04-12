using Filter;
using Filter.Filter_Calculator;
using Filter.Filter_Settings;
using MultiFilter.Core.Filters.Model;
using MultiFilter.GUITests;
using MultiFilter.GUITests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MultiFilter.GUITests.Data.LogicalFilters
{

    public class WeightFilterSetting : ILogicalFilterSettings<Friend, double>
    {
        public Func<Friend, double> PropertyFromDataset { get; set; }
        public string Title { get; set; }
        public string Shortcut { get; set; }
        public Icon Icon { get; set; }

        public WeightFilterSetting()
        {
            Title = "Weight";
            Shortcut = "W";
            Icon = new Icon(Brushes.Red.ToString(), Icons.Alertbericht);
            PropertyFromDataset = p => p.Weight;
        }
    }
}
