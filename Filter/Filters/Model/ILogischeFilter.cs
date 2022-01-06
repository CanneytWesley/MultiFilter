using Filter.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filter
{
    public interface ILogischeFilter : IFilter
    {
        public Task<List<IResult>> LogischFilteren(string uitvoeren);
    }
}
