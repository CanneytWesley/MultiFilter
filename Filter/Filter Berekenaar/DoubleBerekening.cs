using Filter.Filter_Berekenaar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Filter.Filters
{
    public class DoubleBerekening<T> : IBerekening<T>
    {
        public Func<T, double> Property { get; set; }
        public FilterOptie FilterOptie { get; set; }
        public Type FilterTrigger { get; set; }
        public string FilterTitel { get; set; }

        public DoubleBerekening(string filterTitel, Type filterTrigger, Func<T, double> property, FilterOptie filterOptie)
        {
            Property = property;
            FilterOptie = filterOptie;
            FilterTrigger = filterTrigger;
            FilterTitel = filterTitel;
        }

        public List<T> FilterResult(List<T> alleItems, IResult filterresultaat)
        {
            var informatie = filterresultaat.Model.Onderdeel.ToUpper();
            if (informatie.StartsWith(filterresultaat.Filter.ShortCut.ToUpper()))
                informatie = informatie.Substring(filterresultaat.Filter.ShortCut.Length + 1);

            LogischBerekenen logisch = new LogischBerekenen();
            logisch.BerekenLogica(informatie);

            bool eersteRun = true;
            LogischeOperator OfEnOperator = LogischeOperator.En;
            List<T> result = new List<T>();

            for (int i = 0; i < logisch.Logica.Count - 1; i += 2)
            {
                var op = logisch.Logica[i].Operator;
                var waarde = Convert.ToDouble( logisch.Logica[i + 1].Waarde);


                if (eersteRun)
                {
                    result = Filter(alleItems, Property, op, waarde);
                    eersteRun = false;
                }
                else if (OfEnOperator == LogischeOperator.En)
                {
                    result = Filter(result, Property, op, waarde);
                }
                else if (OfEnOperator == LogischeOperator.Of)
                {
                    var tussenresultaat = Filter(alleItems, Property, op, waarde);

                    foreach (var item in tussenresultaat)
                        if (!result.Contains(item)) result.Add(item);
                }

                if (i < logisch.Logica.Count - 2)
                {
                    OfEnOperator = logisch.Logica[i + 2].Operator;
                    i += 1;
                }
            }

            return result;
        }

        private List<T> Filter(List<T> alleItems, Func<T, double> property, LogischeOperator op, double waarde)
        {
            switch (op)
            {
                case LogischeOperator.KleinerDan:
                    return alleItems.Where(p => property(p) < waarde).ToList();
                case LogischeOperator.GroterDan:
                    return alleItems.Where(p => property(p) > waarde).ToList();
                case LogischeOperator.KleinerOfGelijkAan:
                    return alleItems.Where(p => property(p) <= waarde).ToList();
                case LogischeOperator.GroterOfGelijkAan:
                    return alleItems.Where(p => property(p) >= waarde).ToList();
                case LogischeOperator.GelijkAan:
                    return alleItems.Where(p => property(p) == waarde).ToList();
                case LogischeOperator.NietGelijkAan:
                    return alleItems.Where(p => property(p) != waarde).ToList();
                default:
                    break;
            }

            return new List<T>();

        }
    }
}