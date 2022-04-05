using MultiFilter.Core.Filters.Model;

namespace Filter.Filter_Results
{
    public class BooleanResult : Result 
    {
        public BooleanResult(IFilter filter, string onderdeel, Icon icon) : base(filter, (IResult result) => { }, icon)
        => Model = new MultipleChoiceModel(onderdeel.Replace(" ",""));
    }

}