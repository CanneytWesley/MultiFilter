using Filter.Filter_Results;
using MultiFilter.Core.Filters;
using MultiFilter.Core.Filters.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Filter.Filter_Calculator
{
    public interface IFilterExecutor { 
    
    }

    public class FilterExecutor<T> : IFilterExecutor
    {
        private List<T> AllItems;
        private List<T> Items;

        private int HowManyTimesFiltered;

        public List<ICalculation<T>> FilterCalculations { get; set; }

        public List<T> Result
        {
            get
            {
                if (HowManyTimesFiltered == 0) return AllItems;
                else return Items;
            }
        }

        public Edit Edit { get; private set; }

        public FilterExecutor()
        {
            AllItems = new List<T>();
            Items = new List<T>();
            FilterCalculations = new List<ICalculation<T>>();
        }

        private void Setup(string filterTitle, Type filterTrigger, Func<T, double> Property, FilterOption val)
            => FilterCalculations.Add(new DoubleCalculation<T>(filterTitle, filterTrigger, Property, val));
        
        private void Setup(string filterTitle, Type filterTrigger, Func<T, int> Property, FilterOption val)
            => FilterCalculations.Add(new IntCalculation<T>(filterTitle, filterTrigger, Property, val));
        
        private void Setup(string filterTitle, Type filterTrigger, Func<T, string> Property, FilterOption val)
            => FilterCalculations.Add(new StringCalculation<T>(filterTitle, filterTrigger, Property, val));
        
        private void Setup(string filterTitle, Type filterTrigger, Func<T, DateTime> Property, FilterOption val)
            => FilterCalculations.Add(new DateTimeCalculation<T>(filterTitle, filterTrigger, Property, val));
        
        private void Setup(string filterTitle, Type filterTrigger, Func<T, DateTime?> Property, FilterOption val)
            => FilterCalculations.Add(new NullableDateTimeCalculation<T>(filterTitle, filterTrigger, Property, val));

        private void Add(IEnumerable<T> items)
        {
            if (Edit == Edit.Or)
            {
                foreach (var item in items)
                {
                    if (!Items.Contains(item))
                        Items.Add(item);
                }
            }
            else if (Edit == Edit.And)
            {
                if (HowManyTimesFiltered == 0)
                    items.ToList().ForEach(p => Items.Add(p));
                else
                {
                    var result = Items.Intersect(items).ToList();
                    Items.Clear();
                    result.ToList().ForEach(p => Items.Add(p));
                }
            }

            HowManyTimesFiltered++;
        }

        private void ResetFilter()
        {
            HowManyTimesFiltered = 0;
            Items.Clear();
        }


        public void SetData(List<T> Items)
        {
            AllItems = Items;
        }

        public void Filter(Edit edit, List<IResult> results)
        {
            ResetFilter();

            Edit = edit;

            foreach (var filterResult in results)
            {
                var type = typeof(object);
                if (filterResult.Model.Model != null)
                    type = filterResult?.Model?.Model.GetType();
                else
                    type = filterResult.Filter.GetType().GetGenericArguments()[1];

                var filter = FilterCalculations.FirstOrDefault(p => p.FilterTrigger == type && p.FilterTitle == filterResult.Filter.Title);
                
                var result = filter.FilterResult(AllItems, filterResult);

                Add(result);
                
            }
        }

        public void Setup(List<IFilter> filters)
        {
            foreach (var filter in filters)
            {
                var actualFilterType = filter.GetType();

                if (filter.GetType().IsGenericType)
                {
                    var genericFilterType = filter.GetType().GetGenericTypeDefinition();

                    if (genericFilterType == typeof(MultipleChoiceFilter<,>) ||
                        genericFilterType == typeof(LogicalFilter<,>))
                    {
                        dynamic castedFilter = Convert.ChangeType(filter, actualFilterType);
                        Type actualDataType = castedFilter.Data.GetType();
                        dynamic castedFilterInstelling = Convert.ChangeType(castedFilter.Data, actualDataType);

                        Type type = actualFilterType.GetGenericArguments()[1];

                        if (actualDataType.IsNotPublic)
                            throw new Exception($"{actualDataType} must be public to use in the filter");

                        if (genericFilterType == typeof(LogicalFilter<,>) && 
                            type != typeof(double) && type != typeof(int) && type != typeof(string) && type != typeof(DateTime) && type != typeof(DateTime?))
                            throw new Exception($"Your logical filter has a non existing type '{type}' that you can use in this filter");

                        Setup(castedFilterInstelling.Title, type, castedFilterInstelling.PropertyFromDataset, castedFilterInstelling.FilterOptions);
                    }
                }
            }
        }
    }
}