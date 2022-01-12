using Filter.Filter_Calculator;
using Filter.Filter_Settings;
using Filter.Filters.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilterTests.Filters
{
        public class FilterSettingsTestClass : IActionFilterSettings
        {
            public string Title { get; set; }
            public string Shortcut { get; set; } = "C";
            public FilterOption FilterOptions { get; set; }
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