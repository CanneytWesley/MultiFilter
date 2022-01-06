using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Filter.Filters
{
    public interface IFilterUitvoerenEvent
    {
        event FilterEventHandler FilterUitvoeren;
    }

    public delegate void FilterEventHandler(IResult resultaat);

    public class KeuzeFilter<T,F> : BaseFilter, IInitialiseren, IFilter, IFilterUitvoerenEvent
    {
        public List<IModel<F>> AlleOnderdelen { get; private set; }
        public IKeuzeFilterInstellingen<T,F> Data { get; }

        public bool IsGeinitialiseerd { get; private set; }
        public event FilterEventHandler FilterUitvoeren;


        public async Task<List<IResult>> Filteren(string uitvoeren)
        {
            return await Task.Run(() => {

                if (!TestShortCut(uitvoeren))
                    return new List<IResult>();

                var result = AlleOnderdelen.Where(p => TestShortCut(uitvoeren) && p.Naam.IndexOf(VerwijderShortCut(uitvoeren), StringComparison.OrdinalIgnoreCase) != -1).ToList();
                return result.Select(p => (IResult)new Result(this, p.Naam,p.Model, (IResult result) => { FilterUitvoeren?.Invoke(result); },Icon)).ToList();
            });
        }

        public async Task Initialiseren()
        {
            if (IsGeinitialiseerd) return;

            AlleOnderdelen = new List<IModel<F>>();

            var result = await Data.GetData();

            result.ForEach(p => AlleOnderdelen.Add(new FilterModel<F>(p,Data.PropertyOmMeeTeFilteren.Invoke(p))));
        }

        public KeuzeFilter(IKeuzeFilterInstellingen<T,F> data)
        {
            Data = data;
            Icon = data.Icon;
            Titel = data.Titel;
            ShortCut = data.Shortcut;
        }


    }
}