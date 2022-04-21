using Filter.Filter_Calculator;
using Filter.Filter_Results;
using Filter.Filter_Settings;
using MultiFilter.Core.Filters.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiFilter.Core.Filters
{
    public class BooleanFilter<T> : BaseFilter, IBooleanFilter
    {
        public static Dictionary<string, bool> Translations = new Dictionary<string, bool>(){
            { @"ja",true },
            { @"nee",false },
            { @"yes",true },
            { @"no",false },
            { @"1",true },
            { @"0",false },
            { @"true",true },
            { @"false",false },
        };
        public BooleanFilter(IBooleanSettings<T> data)
        {
            Title = data.Title;
            ShortCut = data.Shortcut;
            Icon = data.Icon;
            Data = data;
        }

        public IBooleanSettings<T> Data { get; }

        public Task<List<IResult>> Filter(string uitvoeren)
        {
            if (!HasThisShortCut(uitvoeren))
                return Task.FromResult(new List<IResult>());

            var answer = RemoveShortCut(uitvoeren).ToLower();

            var succes = Translations.TryGetValue(answer.ToLower(), out bool result);

            if (!succes)
                return Task.FromResult(new List<IResult>() { });

            
            return Task.FromResult(new List<IResult>() { new BooleanResult(this, answer, Icon) });

        }


    }
}