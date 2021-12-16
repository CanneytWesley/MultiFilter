using Filter.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Filter
{
    public interface IFilter
    {
        public Task<List<IResult>> Filteren(string uitvoeren);

        public string Titel { get; }
        string ShortCut { get; set; }
    }
}
