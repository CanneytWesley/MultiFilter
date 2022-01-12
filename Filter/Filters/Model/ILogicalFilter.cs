using Filter.Filter_Results;
using Filter.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filter.Filters.Model
{
    public interface ILogicalFilter : IFilter
    {
        public Task<List<IResult>> FilterLogical(string text);
    }
}
