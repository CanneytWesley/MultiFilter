using Filter.Filter_Calculator;
using MultiFilter.Core.Filters.Model;
using System;

namespace Filter.Filter_Settings
{
    /// <summary>
    /// Een filter instelling dient om een filter volledig te kunnen instellen
    /// </summary>
    /// <typeparam name="T">Het model die je gegevenset is</typeparam>
    public interface IBooleanSettings<T>
    {
        Func<T, bool> PropertyFromDataset { get; set; }

        public string Title { get; set; }

        public string Shortcut { get; set; }

        public FilterOption FilterOptions { get; set; }

        public Icon Icon { get; set; }
    }    
}