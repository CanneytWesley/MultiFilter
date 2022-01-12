using Filter.Filter_Calculator;
using Filter.Filters;
using Filter.Filters.Model;
using System;

namespace Filter.Filter_Settings
{
    public interface ILogicalFilterSettings<T, F>
    {
        Func<T, F> PropertyFromDataset { get; set; }

        public string Title { get; set; }

        public string Shortcut { get; set; }

        public FilterOption FilterOptions { get; set; }

        public Icon Icon { get; set; }
    }
}