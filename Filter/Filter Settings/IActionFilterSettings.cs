using Filter.Filter_Calculator;
using Filter.Filters;
using Filter.Filters.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filter.Filter_Settings
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