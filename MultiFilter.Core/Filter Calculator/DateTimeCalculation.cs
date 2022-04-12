using Filter.Filter_Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Filter.Filter_Calculator
{
    public class NullableDateTimeCalculation<T> : ICalculation<T>
    {
        public Func<T, DateTime?> Property { get; set; }
        public Type FilterTrigger { get; set; }
        public string FilterTitle { get; set; }

        public NullableDateTimeCalculation(string filterTitle, Type filterTrigger, Func<T, DateTime?> property)
        {
            Property = property;
            FilterTrigger = filterTrigger;
            FilterTitle = filterTitle;
        }

        public List<T> FilterResult(List<T> allItems, IResult filterResult)
        {
            var informatie = filterResult.Model.Item.ToUpper();
            if (informatie.StartsWith(filterResult.Filter.ShortCut.ToUpper()))
                informatie = informatie.Substring(filterResult.Filter.ShortCut.Length);

            var logicalCalc = new LogicalCalculator();
            logicalCalc.Calculate(informatie);

            bool firstRun = true;
            LogicalOperator OrAndOperator = LogicalOperator.And;
            List<T> result = new List<T>();

            for (int i = 0; i < logicalCalc.Logic.Count - 1; i += 2)
            {
                var op = logicalCalc.Logic[i].Operator;
                var value = new DateTime(Convert.ToInt64(logicalCalc.Logic[i + 1].value));


                if (firstRun)
                {
                    result = Filter(allItems, Property, op, value);
                    firstRun = false;
                }
                else if (OrAndOperator == LogicalOperator.And)
                {
                    result = Filter(result, Property, op, value);
                }
                else if (OrAndOperator == LogicalOperator.Or)
                {
                    var tempresult = Filter(allItems, Property, op, value);

                    foreach (var item in tempresult)
                        if (!result.Contains(item)) result.Add(item);
                }

                if (i < logicalCalc.Logic.Count - 2)
                {
                    OrAndOperator = logicalCalc.Logic[i + 2].Operator;
                    i += 1;
                }
            }

            return result;
        }

        private List<T> Filter(List<T> allItems, Func<T, DateTime?> property, LogicalOperator op, DateTime value)
        {
            switch (op)
            {
                case LogicalOperator.SmallerThan:
                    return allItems.Where(p => property(p) < value).ToList();
                case LogicalOperator.GreaterThan:
                    return allItems.Where(p => property(p) > value).ToList();
                case LogicalOperator.SmallerOrEqualThan:
                    return allItems.Where(p => property(p) <= value).ToList();
                case LogicalOperator.GreaterOrEqualThan:
                    return allItems.Where(p => property(p) >= value).ToList();
                case LogicalOperator.Equal:
                    return allItems.Where(p => property(p) == value).ToList();
                case LogicalOperator.NotEqual:
                    return allItems.Where(p => property(p) != value).ToList();
                default:
                    break;
            }

            return new List<T>();

        }
    }

    public class DateTimeCalculation<T> : ICalculation<T>
    {
        public Func<T, DateTime> Property { get; set; }
        public Type FilterTrigger { get; set; }
        public string FilterTitle { get; set; }

        public DateTimeCalculation(string filterTitle, Type filterTrigger, Func<T, DateTime> property)
        {
            Property = property;
            FilterTrigger = filterTrigger;
            FilterTitle = filterTitle;
        }

        public List<T> FilterResult(List<T> allItems, IResult filterResult)
        {
            var informatie = filterResult.Model.Item.ToUpper();
            if (informatie.StartsWith(filterResult.Filter.ShortCut.ToUpper()))
                informatie = informatie.Substring(filterResult.Filter.ShortCut.Length + 1);

            var logicalCalc = new LogicalCalculator();
            logicalCalc.Calculate(informatie);

            bool firstRun = true;
            LogicalOperator OrAndOperator = LogicalOperator.And;
            List<T> result = new List<T>();

            for (int i = 0; i < logicalCalc.Logic.Count - 1; i += 2)
            {
                var op = logicalCalc.Logic[i].Operator;
                var value = new DateTime(Convert.ToInt64(logicalCalc.Logic[i + 1].value));


                if (firstRun)
                {
                    result = Filter(allItems, Property, op, value);
                    firstRun = false;
                }
                else if (OrAndOperator == LogicalOperator.And)
                {
                    result = Filter(result, Property, op, value);
                }
                else if (OrAndOperator == LogicalOperator.Or)
                {
                    var tempresult = Filter(allItems, Property, op, value);

                    foreach (var item in tempresult)
                        if (!result.Contains(item)) result.Add(item);
                }

                if (i < logicalCalc.Logic.Count - 2)
                {
                    OrAndOperator = logicalCalc.Logic[i + 2].Operator;
                    i += 1;
                }
            }

            return result;
        }

        private List<T> Filter(List<T> allItems, Func<T, DateTime> property, LogicalOperator op, DateTime value)
        {
            switch (op)
            {
                case LogicalOperator.SmallerThan:
                    return allItems.Where(p => property(p) < value).ToList();
                case LogicalOperator.GreaterThan:
                    return allItems.Where(p => property(p) > value).ToList();
                case LogicalOperator.SmallerOrEqualThan:
                    return allItems.Where(p => property(p) <= value).ToList();
                case LogicalOperator.GreaterOrEqualThan:
                    return allItems.Where(p => property(p) >= value).ToList();
                case LogicalOperator.Equal:
                    return allItems.Where(p => property(p) == value).ToList();
                case LogicalOperator.NotEqual:
                    return allItems.Where(p => property(p) != value).ToList();
                default:
                    break;
            }

            return new List<T>();

        }
    }
}