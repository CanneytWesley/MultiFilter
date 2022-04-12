using Filter.Filter_Calculator;
using Filter.Filter_Settings;
using MultiFilter.Core.Filters.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiFilter.CoreTests.Filters
{
    public class FilterSettingsTestClass : IActionFilterSettings
    {
        public string Title { get; set; }
        public string Shortcut { get; set; } = "C";
        public Icon Icon { get; set; }

        public Task<List<FilterAction>> GetData()
        {
            return Task.FromResult(
                new List<FilterAction>()
                {
                        new FilterAction("Leverancier start", () => { })
                });

        }
    }

}