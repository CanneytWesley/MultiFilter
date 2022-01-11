using Filter.Filter_Berekenaar;
using System;
using System.Collections.Generic;

namespace Filter.Filters
{
    public interface IBerekening<T>
    {

        public FilterOptie FilterOptie { get; set; }

        public Type FilterTrigger { get; set; }

        public string FilterTitel { get; set; }

        List<T> FilterResult(List<T> alleItems, IResult filterresultaat);
    }

    public interface INumberBerekening<T> : IBerekening<T>
    { 
    
    }
}