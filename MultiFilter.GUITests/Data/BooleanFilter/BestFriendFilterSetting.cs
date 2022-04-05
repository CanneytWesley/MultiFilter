using Filter.Filter_Calculator;
using Filter.Filter_Settings;
using MultiFilter.Core.Filters.Model;
using MultiFilter.GUITests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MultiFilter.GUITests.Data.BooleanFilter
{
    public class BestFriendFilterSetting : IBooleanSettings<Friend>
    {
        public Func<Friend, bool> PropertyFromDataset { get; set; } = p => p.IsBestFriend;
        public string Title { get; set; } = "Best friend";
        public string Shortcut { get; set; } = "Bff";
        public FilterOption FilterOptions { get; set; } = FilterOption.Exact;
        public Icon Icon { get; set; } = new Icon(Brushes.AntiqueWhite.ToString(), Icons.Bericht);
    }
}
