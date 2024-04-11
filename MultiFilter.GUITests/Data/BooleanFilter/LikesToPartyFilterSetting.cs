using Filter.Filter_Settings;
using MultiFilter.Core.Filters.Model;
using MultiFilter.GUITests.Models;
using System;
using System.Windows.Media;

namespace MultiFilter.GUITests.Data.BooleanFilter
{
    public class LikesToPartyFilterSetting : IBooleanSettings<Friend>
    {
        public Func<Friend, bool> PropertyFromDataset { get; set; } = p => p.LikesToParty;
        public string Title { get; set; } = "Likes To Party";
        public string Shortcut { get; set; } = "lt";
        public Icon Icon { get; set; } = new Icon(Brushes.AntiqueWhite.ToString(), Icons.Bericht);
        public string InformationText { get; set; }
    }
}
