using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Filter.Filters
{
    public class ActionFilter : IFilter
    {
        public Action Action { get; set; }

        public string Titel { get; set; }
        public string ShortCut { get; set; } = "A";

        public Task<List<IResult>> Filteren(string uitvoeren)
        {
            var result = Titel.IndexOf(uitvoeren, StringComparison.OrdinalIgnoreCase) != -1;

            if (result)
                return Task.FromResult( new List<IResult>() { new Result(this, Titel, (IResult result) => { Action.Invoke(); }) });
            else
                return Task.FromResult(new List<IResult>() );
        }
    }
}