using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filter.Filters
{
    public class LogischeFilter<T,F> : BaseFilter, ILogischeFilter
    {
        public ILogischeFilterInstellingen<T,F> Data { get; set; }

        public LogischeFilter(ILogischeFilterInstellingen<T,F> data)
        {
            Titel = data.Titel;
            ShortCut = data.Shortcut;
            Icon = data.Icon;
            Data = data;
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