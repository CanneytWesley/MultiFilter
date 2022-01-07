using System;

namespace Filter.Filters
{
    public class KeuzeModelResult : Result 
    {
        public KeuzeModelResult(IFilter filter, string onderdeel, object model, Action<IResult> actie, Icon icon) : base(filter, actie, icon)
        => Model = new KeuzeModel(onderdeel, model);
    }

}