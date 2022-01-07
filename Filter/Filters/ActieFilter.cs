using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filter.Filters
{
    public class ActieFilter : BaseFilter, IFilter
    {
        public IActieFilterInstellingen Instelling { get; }

        public ActieFilter(IActieFilterInstellingen Instelling)
        {
            this.Instelling = Instelling;
            Titel = Instelling.Titel;
            Icon = Instelling.Icon;
            ShortCut = Instelling.Shortcut;
        }

        public async Task<List<IResult>> Filteren(string uitvoeren)
        {
            if (!TestShortCut(uitvoeren))
                return new List<IResult>();

            var data = await Instelling.GetData();

            var result = data.Where(p => TestShortCut(uitvoeren) && p.ActionName.IndexOf(VerwijderShortCut(uitvoeren), StringComparison.OrdinalIgnoreCase) != -1).ToList();
            return result.Select(p => (IResult)new ActionResult(this, p.ActionName, (IResult result) => { p.Action?.Invoke(); }, Icon)).ToList();
        }
    }
}