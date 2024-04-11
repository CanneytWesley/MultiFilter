using Filter.Filter_Settings;
using MultiFilter.Core.Filters.Model;
using MultiFilter.GUITests.Models;
using System;
using System.Windows.Media;

namespace MultiFilter.GUITests.Data.BooleanFilter
{
    public class NonAutonaticFilter : IBooleanSettings<Friend>
    {
        public Func<Friend, bool> PropertyFromDataset { get; set; }
        public string Title { get; set; } = "Does not filter automatically";
        public string Shortcut { get; set; } = "f";
        public Icon Icon { get; set; } = new Icon(Brushes.AntiqueWhite.ToString(), Icons.Bericht);
        public string InformationText { get; set; }
    }
}
