using Filter.Filter_Results;
using Filter.Filter_Settings;
using Filter.Filters.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filter.Filters
{
    public class ActionFilter : BaseFilter, IFilter
    {
        public IActionFilterSettings Setting { get; }

        public ActionFilter(IActionFilterSettings setting)
        {
            this.Setting = setting;
            Title = setting.Title;
            Icon = setting.Icon;
            ShortCut = setting.Shortcut;
        }

        public async Task<List<IResult>> Filter(string uitvoeren)
        {
            if (!TestShortCut(uitvoeren))
                return new List<IResult>();

            var data = await Setting.GetData();

            var result = data.Where(p => TestShortCut(uitvoeren) && p.ActionName.IndexOf(RemoveShortCut(uitvoeren), StringComparison.OrdinalIgnoreCase) != -1).ToList();
            return result.Select(p => (IResult)new ActionResult(this, p.ActionName, (IResult result) => { p.Action?.Invoke(); }, Icon)).ToList();
        }
    }
}