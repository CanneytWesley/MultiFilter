using System;
using System.Collections.Generic;
using System.Linq;

namespace Filter.Filters
{
    public class FilterBerekenen<T>
    {
        private List<T> AlleItems;
        private List<T> Items;

        private int AantalKeerGefilterd;

        public List<ICalculation<T>> FilterBerekeningen { get; set; }

        public List<T> Resultaat
        {
            get
            {
                if (AantalKeerGefilterd == 0) return AlleItems;
                else return Items;
            }
        }

        public Soort Soort { get; private set; }

        public FilterBerekenen()
        {
            AlleItems = new List<T>();
            Items = new List<T>();
            FilterBerekeningen = new List<ICalculation<T>>();
        }

        private void Instellen(string filterTitel, Type filterTrigger, Func<T, double> Property, FilterOption val)
            => FilterBerekeningen.Add(new DoubleBerekening<T>(filterTitel, filterTrigger, Property, val));
        
        private void Instellen(string filterTitel, Type filterTrigger, Func<T, int> Property, FilterOption val)
            => FilterBerekeningen.Add(new IntBerekening<T>(filterTitel, filterTrigger, Property, val));
        
        private void Instellen(string filterTitel, Type filterTrigger, Func<T, string> Property, FilterOption val)
            => FilterBerekeningen.Add(new StringBerekening<T>(filterTitel, filterTrigger, Property, val));
        
        private void Instellen(string filterTitel, Type filterTrigger, Func<T, DateTime> Property, FilterOption val)
            => FilterBerekeningen.Add(new DateTimeCalculation<T>(filterTitel, filterTrigger, Property, val));

        private void Add(IEnumerable<T> items)
        {
            if (Soort == Soort.Of)
            {
                foreach (var item in items)
                {
                    if (!Items.Contains(item))
                        Items.Add(item);
                }
            }
            else if (Soort == Soort.En)
            {
                IEnumerable<T> lijst = new List<T>();

                if (AantalKeerGefilterd == 0)
                    items.ToList().ForEach(p => Items.Add(p));
                else
                {
                    var result = Items.Intersect(items).ToList();
                    Items.Clear();
                    result.ToList().ForEach(p => Items.Add(p));
                }
            }

            AantalKeerGefilterd++;
        }

        private void ResetFilter()
        {
            AantalKeerGefilterd = 0;
            Items.Clear();
        }


        public void SetData(List<T> Items)
        {
            AlleItems = Items;
        }

        public void Filteren(Soort soort, List<IResult> resultaten)
        {
            ResetFilter();

            Soort = soort;

            foreach (var filterresultaat in resultaten)
            {
                var type = typeof(object);
                if (filterresultaat.Model.Model != null)
                    type = filterresultaat?.Model?.Model.GetType();
                else
                    type = filterresultaat.Filter.GetType().GetGenericArguments()[1];

                var filter = FilterBerekeningen.FirstOrDefault(p => p.FilterTrigger == type && p.FilterTitle == filterresultaat.Filter.Title);
                
                var result = filter.FilterResult(AlleItems, filterresultaat);

                Add(result);
                
            }
        }

        public void Instellen(List<IFilter> filters)
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
                            type != typeof(double) && type != typeof(int) && type != typeof(string) && type != typeof(DateTime))
                            throw new Exception($"Your logical filter has a non existing type '{type}' that you can use in this filter");

                        Instellen(castedFilterInstelling.Title, type, castedFilterInstelling.PropertyFromDataset, castedFilterInstelling.FilterOptions);
                    }
                }
            }
        }
    }
}