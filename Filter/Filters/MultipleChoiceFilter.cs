﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Filter.Filters
{
    public interface IFilterExecuteEvent
    {
        event FilterEventHandler ExecuteFilter;
    }

    public delegate void FilterEventHandler(IResult resultaat);

    public class MultipleChoiceFilter<T,F> : BaseFilter, IInitialise, IFilter, IFilterExecuteEvent
    {
        public List<IModel<F>> AllItems { get; private set; }
        public IMultipleChoiceSettings<T,F> Data { get; }

        public bool IsInitialised { get; private set; }
        public event FilterEventHandler ExecuteFilter;


        public async Task<List<IResult>> Filter(string uitvoeren)
        {
            return await Task.Run(() => {

                if (!TestShortCut(uitvoeren))
                    return new List<IResult>();

                var result = AllItems.Where(p => TestShortCut(uitvoeren) && p.Name.IndexOf(VerwijderShortCut(uitvoeren), StringComparison.OrdinalIgnoreCase) != -1).ToList();
                return result.Select(p => (IResult)new KeuzeModelResult(this, p.Name,p.Model, (IResult result) => { ExecuteFilter?.Invoke(result); },Icon)).ToList();
            });
        }

        public async Task Initialise()
        {
            if (IsInitialised) return;

            AllItems = new List<IModel<F>>();

            var result = await Data.GetData();

            result.ForEach(p => AllItems.Add(new FilterModel<F>(p,Data.PropertyToFilterWith.Invoke(p))));
        }

        public MultipleChoiceFilter(IMultipleChoiceSettings<T,F> data)
        {
            Data = data;
            Icon = data.Icon;
            Title = data.Title;
            ShortCut = data.Shortcut;
        }


    }
}