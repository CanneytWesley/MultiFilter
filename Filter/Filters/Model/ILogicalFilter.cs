using Filter.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filter
{
    public interface ILogicalFilter : IFilter
    {
        public Task<List<IResult>> FilterLogical(string text);
    }
}
