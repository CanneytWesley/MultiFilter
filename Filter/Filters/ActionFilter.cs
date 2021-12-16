using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Filter.Filters
{
    public abstract class BaseFilter
    {
        public string Titel { get; set; }
        public string ShortCut { get; set; } = "A";
        public Icon Icon { get; set; }
        public bool TestShortCut(string uitvoeren, string shortCut)
        {
            if (uitvoeren?.Length > 0)
            { 
                
            }

            return false;
        }

        public string VerwijderShortCut(string uitvoeren)
        {



            return null;
        }
    }

    public class ActionFilter : BaseFilter, IFilter
    {
        public Action Action { get; set; }

        public Task<List<IResult>> Filteren(string uitvoeren)
        {
            var result = Titel.IndexOf(VerwijderShortCut(uitvoeren), StringComparison.OrdinalIgnoreCase) != -1 && TestShortCut(uitvoeren,ShortCut);

            if (result)
                return Task.FromResult( new List<IResult>() { new Result(this, Titel, (IResult result) => { Action.Invoke(); },Icon) });
            else
                return Task.FromResult(new List<IResult>() );
        }

    }
}