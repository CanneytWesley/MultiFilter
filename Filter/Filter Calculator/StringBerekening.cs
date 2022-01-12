using System;
using System.Collections.Generic;
using System.Linq;

namespace Filter.Filters
{
    public class StringBerekening<T> : ICalculation<T>
    {
        public Func<T, string> Property { get; set; }

        public FilterOption FilterOption { get; set; }
        public Type FilterTrigger { get; set; }
        public string FilterTitle { get; set; }

        public StringBerekening(string filterTitel, Type filterTrigger, Func<T, string> property, FilterOption filterOptie)
        {
            Property = property;
            FilterOption = filterOptie;
            FilterTrigger = filterTrigger;
            FilterTitle = filterTitel;
        }

        public List<T> FilterResult(List<T> alleItems, IResult filterresultaat)
        {
            StringComparison sc = StringComparison.OrdinalIgnoreCase;
            if (FilterOption.HasFlag(FilterOption.OrdinalCase))
                sc = StringComparison.Ordinal;

            if (FilterOption.HasFlag(FilterOption.IndexOf))
            {
                var result = alleItems.Where(p => Property(p).IndexOf(filterresultaat.Model.Onderdeel, sc) != -1).ToList();
                return result;
            }
            else if (FilterOption.HasFlag(FilterOption.Exact))
            {
                var result = alleItems.Where(p => Property(p) == filterresultaat.Model.Onderdeel).ToList();
                return result;

            }

            return new List<T>();
        }
    }
}