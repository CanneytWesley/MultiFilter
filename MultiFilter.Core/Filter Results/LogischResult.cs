using MultiFilter.Core.Filters.Model;
using System;

namespace Filter.Filter_Results
{
    public class LogischResult : Result 
    {
        public LogischResult(IFilter filter, string onderdeel, Icon icon) : base(filter, (IResult result) => { }, icon)
        => Model = new MultipleChoiceModel(onderdeel.Replace(" ",""));
    }

}