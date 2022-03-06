using Filter.Filter_Results;
using System;
using System.Collections.Generic;

namespace Filter.Filter_Calculator
{
    public interface ICalculation<T>
    {

        public FilterOption FilterOption { get; set; }

        public Type FilterTrigger { get; set; }

        public string FilterTitle { get; set; }

        List<T> FilterResult(List<T> alleItems, IResult filterresultaat);
    }

    public interface INumberBerekening<T> : ICalculation<T>
    { 
    
    }
}