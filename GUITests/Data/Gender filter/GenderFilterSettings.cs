using Filter.Filter_Calculator;
using Filter.Filter_Settings;
using Filter.Filters.Model;
using GUITests.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GUITests.Data.Gender_filter
{
    public class GenderFilterSettings : IMultipleChoiceSettings<Friend, Gender>
    {
        public Func<Gender, string> PropertyToFilterWith { get; set; } = p => p.ToString();
        public Func<Friend, string> PropertyFromDataset { get; set; } = p => p.Sex.ToString();
        public string Title { get; set; }
        = "Gender";
        public string Shortcut { get; set; }
        = "G";
        public FilterOption FilterOptions { get; set; }
        public Icon Icon { get; set; }
        = new Icon(Brushes.AntiqueWhite.ToString(), Icons.Bericht);

        public Task<List<Gender>> GetData()
        {
            return Task.FromResult(
                new List<Gender>() {
                    Gender.Female,
                    Gender.Men,
                    Gender.Other,
                });
        }
    }
}
