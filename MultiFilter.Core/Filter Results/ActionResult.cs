using MultiFilter.Core.Filters.Model;
using System;

namespace Filter.Filter_Results
{
    public class ActionResult : Result 
    {
        public ActionResult(IFilter filter, string onderdeel, Action<IResult> actie, Icon icon) : base(filter, actie, icon)
        => Model = new MultipleChoiceModel(onderdeel);
    }

}