using Filter.Filter_Calculator;
using Filter.Filter_Settings;
using MultiFilter.Core.Filters.Model;
using MultiFilter.GUITests.Models;
using System;
using System.Windows.Media;

namespace MultiFilter.GUITests.Data.LogicalFilters
{
    public class DateOfDeathSetting : ILogicalFilterSettings<Friend, DateTime?>
    {
        public Func<Friend, DateTime?> PropertyFromDataset { get; set; }
        = p => p.DateOfDeath;
        public string Title { get; set; }
        = "Date Of Death";
        public string Shortcut { get; set; }
        = "De";
        public Icon Icon { get; set; }
        = new Icon(Brushes.BlueViolet.ToString(), Icons.Freeze);

        public DateOfDeathSetting()
        {

        }
    }
}