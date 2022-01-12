using System;

namespace Filter.Filters
{
    public class MultipleChoiceModelResult : Result 
    {
        public MultipleChoiceModelResult(IFilter filter, string item, object model, Action<IResult> actie, Icon icon) : base(filter, actie, icon)
        => Model = new MultipleChoiceModel(item, model);
    }

}