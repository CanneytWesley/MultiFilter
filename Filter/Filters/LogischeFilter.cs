using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filter.Filters
{
    public class LogischeFilter : BaseFilter, IFilter
    {

        public LogischeFilter(string titel, string shortcut)
        {
            Titel = titel;
            ShortCut = shortcut;
        }
        public Task<List<IResult>> Filteren(string uitvoeren)
        {
            return Task.FromResult(new List<IResult>() { });
        }

        public Task<List<IResult>> LogischFilteren(string uitvoeren)
        {
            return Task.FromResult(new List<IResult>() { new Result(this, VerwijderShortCut(uitvoeren), (IResult result) => { }, Icon) });
        }


    }
}