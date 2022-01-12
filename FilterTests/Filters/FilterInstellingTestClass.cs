using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filter.Filters.Tests
{
    public partial class ActionFilterTests
    {
        public class FilterInstellingTestClass : IActionFilterSettings
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
                        new FilterAction("Leverancier starten", () => { })
                    });
                
            }
        }
    }
}