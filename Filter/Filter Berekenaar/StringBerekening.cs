using System;
using System.Collections.Generic;
using System.Linq;

namespace Filter.Filters
{
    public class StringBerekening<T> : IBerekening<T>
    {
        public Func<T, string> Property { get; set; }

        public FilterOptie FilterOptie { get; set; }
        public Type FilterTrigger { get; set; }
        public string FilterTitel { get; set; }

        public StringBerekening(string filterTitel, Type filterTrigger, Func<T, string> property, FilterOptie filterOptie)
        {
            Property = property;
            FilterOptie = filterOptie;
            FilterTrigger = filterTrigger;
            FilterTitel = filterTitel;
        }

        public List<T> FilterResult(List<T> alleItems, IResult filterresultaat)
        {
            StringComparison sc = StringComparison.OrdinalIgnoreCase;
            if (FilterOptie.HasFlag(FilterOptie.OrdinalCase))
                sc = StringComparison.Ordinal;

            if (FilterOptie.HasFlag(FilterOptie.IndexOf))
            {
                var result = alleItems.Where(p => Property(p).IndexOf(filterresultaat.Model.Onderdeel, sc) != -1).ToList();
                return result;
            }
            else if (FilterOptie.HasFlag(FilterOptie.Exact))
            {
                var result = alleItems.Where(p => Property(p) == filterresultaat.Model.Onderdeel).ToList();
                return result;

            }

            return new List<T>();
        }
    }
}