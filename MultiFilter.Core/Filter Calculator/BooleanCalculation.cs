using Filter.Filter_Results;
using MultiFilter.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Filter.Filter_Calculator
{
    public class BooleanCalculation<T> : ICalculation<T>
    {

        public Func<T, bool> Property { get; set; }
        public FilterOption FilterOption { get; set; }
        public Type FilterTrigger { get; set; }
        public string FilterTitle { get; set; }

        public BooleanCalculation(string filterTitle, Type filterTrigger, Func<T, bool> property, FilterOption filterOptie)
        {
            Property = property;
            FilterOption = filterOptie;
            FilterTrigger = filterTrigger;
            FilterTitle = filterTitle;
        }

        public List<T> FilterResult(List<T> allitems, IResult filterResult)
        {
            var answer = filterResult.Model.Item.ToUpper();

            bool con = BooleanFilter<T>.Translations.TryGetValue(answer.ToLower(), out bool result);

            if (!con) return allitems;

            return allitems.Where(p => Property(p) == result).ToList();

        }
    }
}