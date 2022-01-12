using Filter;
using Filter.Filters;
using GUITests.Data;
using System;
using System.Windows.Media;

namespace GUITests
{
    public class DateOfBirthSetting : ILogischeFilterInstellingen<Friend, DateTime>
    {
        public Func<Friend, DateTime> PropertyUitDataGrid { get; set; }
        = p => p.DateOfBirth;
        public string Titel { get; set; }
        = "Date Of Birth";
        public string Shortcut { get; set; }
        = "D";
        public FilterOptie FilterOpties { get; set; }
        = FilterOptie.Exact;
        public Icon Icon { get; set; }
        = new Icon(Brushes.BlueViolet.ToString(), Icons.Freeze);

        public DateOfBirthSetting()
        {

        }
    }
}