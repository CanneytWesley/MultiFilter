using Filter.Filter_Results;
using Filter.Filter_Settings;
using Filter.Filters.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filter.Filters
{
    public class LogicalFilter<T,F> : BaseFilter, ILogicalFilter
    {
        public ILogicalFilterSettings<T,F> Data { get; set; }

        public LogicalFilter(ILogicalFilterSettings<T,F> data)
        {
            Title = data.Title;
            ShortCut = data.Shortcut;
            Icon = data.Icon;
            Data = data;
        }
        public Task<List<IResult>> Filter(string uitvoeren)
        {
            return Task.FromResult(new List<IResult>() { });
        }

        public Task<List<IResult>> FilterLogical(string uitvoeren)
        {
            return Task.FromResult(new List<IResult>() { new LogischResult(this, RemoveShortCut(uitvoeren),  Icon) });
        }


    }
}