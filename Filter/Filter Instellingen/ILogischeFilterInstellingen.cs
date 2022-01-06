using Filter.Filters;
using System;

namespace Filter
{
    public interface ILogischeFilterInstellingen<T, F>
    {
        Func<T, F> PropertyUitDataGrid { get; set; }

        public string Titel { get; set; }

        public string Shortcut { get; set; }

        public FilterOptie FilterOpties { get; set; }

        public Icon Icon { get; set; }
    }
}