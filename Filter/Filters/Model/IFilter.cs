using Filter.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Drawing;

namespace Filter
{
    /// <summary>
    /// There are multiple possibilities of implementations:
    /// MultipleChoiceFilter => You will get a list of data in the filter. You can pick one to filter.
    /// ActionFilter => You can add values to filter and bind an action to it. Example: for reporting, or opening a window
    /// LogicalFilter => With this filter you can add logic to it. Example: All ages above 60
    /// </summary>
    public interface IFilter
    {
        public Task<List<IResult>> Filter(string text);

        public string Title { get; }
        string ShortCut { get; set; }

        Icon Icon { get; set; }
    }
}
