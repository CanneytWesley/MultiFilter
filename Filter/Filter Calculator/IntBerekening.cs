using Filter.Filter_Berekenaar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Filter.Filters
{
    public class IntBerekening<T> : ICalculation<T>
    {
        public Func<T, int> Property { get; set; }
        public FilterOption FilterOption { get; set; }
        public Type FilterTrigger { get; set; }
        public string FilterTitle { get; set; }

        public IntBerekening(string filterTitel, Type filterTrigger, Func<T, int> property, FilterOption filterOptie)
        {
            Property = property;
            FilterOption = filterOptie;
            FilterTrigger = filterTrigger;
            FilterTitle = filterTitel;
        }

        public List<T> FilterResult(List<T> alleItems, IResult filterresultaat)
        {
            var informatie = filterresultaat.Model.Onderdeel.ToUpper();
            if (informatie.StartsWith(filterresultaat.Filter.ShortCut.ToUpper()))
                informatie = informatie.Substring(filterresultaat.Filter.ShortCut.Length + 1);

            LogicalCalculator logisch = new LogicalCalculator();
            logisch.Calculate(informatie);

            bool eersteRun = true;
            LogicalOperator OfEnOperator = LogicalOperator.And;
            List<T> result = new List<T>();

            for (int i = 0; i < logisch.Logic.Count - 1; i += 2)
            {
                var op = logisch.Logic[i].Operator;
                var waarde = Convert.ToInt32( logisch.Logic[i + 1].value);


                if (eersteRun)
                {
                    result = Filter(alleItems, Property, op, waarde);
                    eersteRun = false;
                }
                else if (OfEnOperator == LogicalOperator.And)
                {
                    result = Filter(result, Property, op, waarde);
                }
                else if (OfEnOperator == LogicalOperator.Or)
                {
                    var tussenresultaat = Filter(alleItems, Property, op, waarde);

                    foreach (var item in tussenresultaat)
                        if (!result.Contains(item)) result.Add(item);
                }

                if (i < logisch.Logic.Count - 2)
                {
                    OfEnOperator = logisch.Logic[i + 2].Operator;
                    i += 1;
                }
            }

            return result;
        }

        private List<T> Filter(List<T> alleItems, Func<T, int> property, LogicalOperator op, int waarde)
        {
            switch (op)
            {
                case LogicalOperator.SmallerThan:
                    return alleItems.Where(p => property(p) < waarde).ToList();
                case LogicalOperator.GreaterThan:
                    return alleItems.Where(p => property(p) > waarde).ToList();
                case LogicalOperator.SmallerOrEqualThan:
                    return alleItems.Where(p => property(p) <= waarde).ToList();
                case LogicalOperator.GreaterOrEqualThan:
                    return alleItems.Where(p => property(p) >= waarde).ToList();
                case LogicalOperator.Equal:
                    return alleItems.Where(p => property(p) == waarde).ToList();
                case LogicalOperator.NotEqual:
                    return alleItems.Where(p => property(p) != waarde).ToList();
                default:
                    break;
            }

            return new List<T>();

        }
    }
}