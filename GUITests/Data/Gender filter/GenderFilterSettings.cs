using Filter;
using Filter.Filters;
using GUITests.Data.Certificaat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GUITests.Data.Gender_filter
{
    public class GenderFilterSettings : IKeuzeFilterInstellingen<Friend, Gender>
    {
        public Func<Gender, string> PropertyOmMeeTeFilteren { get; set; } = p => p.ToString();
        public Func<Friend, string> PropertyUitDataGrid { get; set; } = p => p.Sex.ToString();
        public string Titel { get; set; }
        = "Gender";
        public string Shortcut { get; set; }
        = "G";
        public FilterOptie FilterOpties { get; set; }
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
