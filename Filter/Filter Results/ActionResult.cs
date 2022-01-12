using System;

namespace Filter.Filters
{
    public class ActionResult : Result 
    {
        public ActionResult(IFilter filter, string onderdeel, Action<IResult> actie, Icon icon) : base(filter, actie, icon)
        => Model = new MultipleChoiceModel(onderdeel);
    }

}