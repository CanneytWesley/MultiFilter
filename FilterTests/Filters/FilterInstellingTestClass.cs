using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filter.Filters.Tests
{
    public partial class ActionFilterTests
    {
        public class FilterInstellingTestClass : IActieFilterInstellingen
        {
            public string Titel { get; set; }
            public string Shortcut { get; set; }
            public FilterOptie FilterOpties { get; set; }
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