using Filter.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filter
{
    /// <summary>
    /// Een filter instelling dient om een filter volledig te kunnen instellen
    /// </summary>
    /// <typeparam name="T">Het model die je gegevenset is</typeparam>
    /// <typeparam name="F">Het model waar je mee wil filteren (bvb een lijst van producten)</typeparam>
    public interface IKeuzeFilterInstellingen<T, F>
    {
        Func<F, string> PropertyOmMeeTeFilteren { get; set; }
        Func<T, string> PropertyUitDataGrid { get; set; }

        public string Titel { get; set; }

        public string Shortcut { get; set; }

        public FilterOptie FilterOpties { get; set; }

        public Task<List<F>> GetData();

        public Icon Icon { get; set; }
    }    


    public interface IActieFilterInstellingen
    {

        public string Titel { get; set; }

        public string Shortcut { get; set; }

        public FilterOptie FilterOpties { get; set; }

        public Task<List<FilterAction>> GetData();

        public Icon Icon { get; set; }


    }
}