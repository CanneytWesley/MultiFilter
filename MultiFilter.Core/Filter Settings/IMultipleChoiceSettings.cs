using Filter.Filter_Calculator;
using MultiFilter.Core.Filters.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filter.Filter_Settings
{
    /// <summary>
    /// Een filter instelling dient om een filter volledig te kunnen instellen
    /// </summary>
    /// <typeparam name="T">Het model die je gegevenset is</typeparam>
    /// <typeparam name="F">Het model waar je mee wil filteren (bvb een lijst van producten)</typeparam>
    public interface IMultipleChoiceSettings<T, F>
    {
        Func<F, string> PropertyToFilterWith { get; set; }
        Func<T, string> PropertyFromDataset { get; set; }

        public string Title { get; set; }

        public string Shortcut { get; set; }

        public FilterOption FilterOptions { get; set; }

        public Task<List<F>> GetData();

        public Icon Icon { get; set; }
    }    
}