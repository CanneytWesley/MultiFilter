using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Filter.Filters
{
    public delegate void FilterEventHandler(IResult resultaat);
    public class KeuzeFilter<T> : BaseFilter, IInitialiseren, IFilter
    {
        public event FilterEventHandler FilterUitvoeren;

        public List<IModel<T>> AlleOnderdelen { get; private set; }
        public IData<T> Data { get; }

        public bool IsGeinitialiseerd { get; private set; }


        public async Task<List<IResult>> Filteren(string uitvoeren)
        {
            return await Task.Run(() => {

                if (TestShortCut(uitvoeren, ShortCut))
                    return new List<IResult>();

                var result = AlleOnderdelen.Where(p => p.Naam.IndexOf(VerwijderShortCut(uitvoeren), StringComparison.OrdinalIgnoreCase) != -1).ToList();
                return result.Select(p => (IResult)new Result(this, p.Naam,p.Model, (IResult result) => { FilterUitvoeren?.Invoke(result); },Icon)).ToList();
            });
        }

        public async Task Initialiseren()
        {
            if (IsGeinitialiseerd) return;

            AlleOnderdelen = new List<IModel<T>>();

            var result = await Data.GetData();
            result.ForEach(p => AlleOnderdelen.Add(p));
        }

        public KeuzeFilter(IData<T> data, string titel, string shortcut)
        {
            Data = data;
            Titel = titel;
            ShortCut = shortcut;
        }
    }
}