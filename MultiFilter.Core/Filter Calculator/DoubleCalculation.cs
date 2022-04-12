using Filter.Filter_Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Filter.Filter_Calculator
{
    public class DoubleCalculation<T> : ICalculation<T>
    {
        public Func<T, double> Property { get; set; }
        public Type FilterTrigger { get; set; }
        public string FilterTitle { get; set; }

        public DoubleCalculation(string filterTitel, Type filterTrigger, Func<T, double> property)
        {
            Property = property;
            FilterTrigger = filterTrigger;
            FilterTitle = filterTitel;
        }

        public List<T> FilterResult(List<T> allItems, IResult filterResult)
        {
            var information = filterResult.Model.Item.ToUpper();
            if (information.StartsWith(filterResult.Filter.ShortCut.ToUpper()))
                information = information.Substring(filterResult.Filter.ShortCut.Length + 1);

            LogicalCalculator logic = new LogicalCalculator();
            logic.Calculate(information);

            bool firstRun = true;
            LogicalOperator AndOrOperator = LogicalOperator.And;
            List<T> result = new List<T>();

            for (int i = 0; i < logic.Logic.Count - 1; i += 2)
            {
                var op = logic.Logic[i].Operator;
                var value = Convert.ToDouble( logic.Logic[i + 1].value);


                if (firstRun)
                {
                    result = Filter(allItems, Property, op, value);
                    firstRun = false;
                }
                else if (AndOrOperator == LogicalOperator.And)
                {
                    result = Filter(result, Property, op, value);
                }
                else if (AndOrOperator == LogicalOperator.Or)
                {
                    var tempResult = Filter(allItems, Property, op, value);

                    foreach (var item in tempResult)
                        if (!result.Contains(item)) result.Add(item);
                }

                if (i < logic.Logic.Count - 2)
                {
                    AndOrOperator = logic.Logic[i + 2].Operator;
                    i += 1;
                }
            }

            return result;
        }

        private List<T> Filter(List<T> allItems, Func<T, double> property, LogicalOperator op, double value)
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