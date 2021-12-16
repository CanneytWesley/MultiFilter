using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Filter.Filters
{

    public class ActionFilter : BaseFilter, IFilter
    {
        public Action Action { get; set; }

        public Task<List<IResult>> Filteren(string uitvoeren)
        {
            var result = Titel.IndexOf(VerwijderShortCut(uitvoeren), StringComparison.OrdinalIgnoreCase) != -1 && TestShortCut(uitvoeren);

            if (result)
                return Task.FromResult( new List<IResult>() { new Result(this, Titel, (IResult result) => { Action.Invoke(); },Icon) });
            else
                return Task.FromResult(new List<IResult>() );
        }

    }
}