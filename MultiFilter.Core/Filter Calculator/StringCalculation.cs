using Filter.Filter_Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Filter.Filter_Calculator
{
    public class StringCalculation<T> : ICalculation<T>
    {
        public Func<T, string> Property { get; set; }

        public FilterOption FilterOption { get; set; }
        public Type FilterTrigger { get; set; }
        public string FilterTitle { get; set; }

        public StringCalculation(string filterTitle, Type filterTrigger, Func<T, string> property, FilterOption filterOptie)
        {
            Property = property;
            FilterOption = filterOptie;
            FilterTrigger = filterTrigger;
            FilterTitle = filterTitle;
        }

        public List<T> FilterResult(List<T> allItems, IResult filterResult)
        {
            StringComparison sc = StringComparison.OrdinalIgnoreCase;
            if (FilterOption.HasFlag(FilterOption.OrdinalCase))
                sc = StringComparison.Ordinal;

            if (FilterOption.HasFlag(FilterOption.IndexOf))
            {
                var result = allItems.Where(p => Property(p) != null && Property(p).IndexOf(filterResult.Model.Item, sc) != -1).ToList();
                return result;
            }
            else if (FilterOption.HasFlag(FilterOption.Exact))
            {
                var result = allItems.Where(p => Property(p) != null && Property(p) == filterResult.Model.Item).ToList();
                return result;

            }

            return new List<T>();
        }
    }
}