using Filter;
using Filter.Filters;
using GUITests.Data;
using System;
using System.Windows.Media;

namespace GUITests
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