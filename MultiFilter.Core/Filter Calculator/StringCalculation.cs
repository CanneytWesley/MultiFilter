using Filter.Filter_Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Filter.Filter_Calculator
{


    public class StringCalculation<T> : ICalculation<T>
    {
        public Func<T, string> Property { get; set; }
        public Type FilterTrigger { get; set; }
        public string FilterTitle { get; set; }

        public StringCalculation(string filterTitle, Type filterTrigger, Func<T, string> property)
        {
            Property = property;
            FilterTrigger = filterTrigger;
            FilterTitle = filterTitle;
        }
        public StringCalculation(string filterTitle, Func<T, string> property)
        {
            Property = property;
            FilterTitle = filterTitle;
        }

        public List<T> FilterResult(List<T> allItems, string filterValue)
        {
            if (filterValue.StartsWith("*") && filterValue.EndsWith("*"))
            {
                return allItems.Where(p => Property(p) != null && Property(p)
                               .IndexOf(filterValue.Substring(1, filterValue.Length - 2), StringComparison.OrdinalIgnoreCase) != -1)
                               .ToList();
            }
            else if (filterValue.StartsWith("*"))
            {
                string cutoff = filterValue.Substring(1, filterValue.Length - 1);
                return allItems.Where(p => Property(p) != null && Property(p)
               .EndsWith(cutoff, StringComparison.OrdinalIgnoreCase))
               .ToList();
            }
            else if (filterValue.EndsWith("*"))
            {
                string cutoff = filterValue.Substring(0, filterValue.Length - 1);
                return allItems.Where(p => Property(p) != null && Property(p)
               .StartsWith(cutoff, StringComparison.OrdinalIgnoreCase))
               .ToList();
            }
            else
            {
                return allItems.Where(p => Property(p) != null && Property(p)
                               .Trim()
                               .Equals(filterValue, StringComparison.OrdinalIgnoreCase))
                               .ToList();
            }
        }

        public List<T> FilterResult(List<T> allItems, IResult filterResult)
        {
            return FilterResult(allItems, filterResult.Model.Item);
        }
    }
}