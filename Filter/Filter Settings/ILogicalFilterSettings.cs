using Filter.Filters;
using System;

namespace Filter
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