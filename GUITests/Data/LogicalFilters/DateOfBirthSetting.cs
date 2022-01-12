using Filter;
using Filter.Filter_Calculator;
using Filter.Filter_Settings;
using Filter.Filters;
using Filter.Filters.Model;
using GUITests.Data;
using GUITests.Models;
using System;
using System.Windows.Media;

namespace GUITests.Data.LogicalFilters
{
    public class DateOfBirthSetting : ILogicalFilterSettings<Friend, DateTime>
    {
        public Func<Friend, DateTime> PropertyFromDataset { get; set; }
        = p => p.DateOfBirth;
        public string Title { get; set; }
        = "Date Of Birth";
        public string Shortcut { get; set; }
        = "D";
        public FilterOption FilterOptions { get; set; }
        = FilterOption.Exact;
        public Icon Icon { get; set; }
        = new Icon(Brushes.BlueViolet.ToString(), Icons.Freeze);

        public DateOfBirthSetting()
        {

        }
    }
}