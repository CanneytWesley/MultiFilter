using Filter.Filter_Calculator;
using MultiFilter.Core.Filters.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filter.Filter_Settings
{
    public interface IActionFilterSettings
    {

        public string Title { get; set; }

        public string Shortcut { get; set; }


        public Task<List<FilterAction>> GetData();

        public Icon Icon { get; set; }


    }
}