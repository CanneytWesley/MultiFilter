using Filter.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filter
{
    public interface IActionFilterSettings
    {

        public string Title { get; set; }

        public string Shortcut { get; set; }

        public FilterOption FilterOptions { get; set; }

        public Task<List<FilterAction>> GetData();

        public Icon Icon { get; set; }


    }
}