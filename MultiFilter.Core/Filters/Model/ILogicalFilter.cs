using Filter.Filter_Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiFilter.Core.Filters.Model
{
    public interface ILogicalFilter : IFilter
    {
        public Task<List<IResult>> FilterLogical(string text);
    }
}
