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

        public List<IBerekening<T>> FilterBerekeningen { get; set; }

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
            FilterBerekeningen = new List<IBerekening<T>>();
        }

        private void Instellen(string filterTitel, Type filterTrigger, Func<T, double> Property, FilterOptie val)
            => FilterBerekeningen.Add(new DoubleBerekening<T>(filterTitel, filterTrigger, Property, val));

        private void Instellen(string filterTitel, Type filterTrigger, Func<T, int> Property, FilterOptie val)
            => FilterBerekeningen.Add(new IntBerekening<T>(filterTitel, filterTrigger, Property, val));

        private void Instellen(string filterTitel, Type filterTrigger, Func<T, string> Property, FilterOptie val)
            => FilterBerekeningen.Add(new StringBerekening<T>(filterTitel, filterTrigger, Property, val));

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

        private void AndereFilteren(List<IResult> resultaten)
        {
            foreach (var filterresultaat in resultaten)
            {
                var type = filterresultaat.Filter.GetType().GetGenericArguments()[1];
                var filter = FilterBerekeningen.FirstOrDefault(p => p.FilterTrigger == type && p.FilterTitel == filterresultaat.Filter.Titel);

                if (filterresultaat.Filter is ILogischeFilter)
                {
                    if (filter is DoubleBerekening<T> dbi)
                    {
                        var result = dbi.FilterResult(AlleItems, filterresultaat);

                        Add(result);
                    }
                    else if (filter is IntBerekening<T> ibi)
                    {
                        var result = ibi.FilterResult(AlleItems, filterresultaat);

                        Add(result);
                    }
                }
            }
        }

        private void ModelFilteren(List<IResult> resultaten)
        {
            foreach (var filterresultaat in resultaten)
            {
                var type = filterresultaat?.Model?.Model.GetType();

                var filter = FilterBerekeningen.FirstOrDefault(p => p.FilterTrigger == type && p.FilterTitel == filterresultaat.Filter.Titel);

                if (filter is StringBerekening<T> sfi)
                {
                    List<T> result = sfi.FilterResult(AlleItems, filterresultaat);

                    Add(result);
                }
            }
        }

        public void SetData(List<T> Items)
        {
            AlleItems = Items;
        }

        public void Filteren(Soort soort, List<IResult> resultaten)
        {
            ResetFilter();

            Soort = soort;

            var ModelFilters = resultaten.Where(p => p.Model.Model != null).ToList();
            var AndereFilters = resultaten.Where(p => p.Model.Model == null).ToList();
            ModelFilteren(ModelFilters);
            AndereFilteren(AndereFilters);
        }

        public void Instellen(List<IFilter> filters)
        {
            foreach (var filter in filters)
            {
                var actualFilterType = filter.GetType();

                if (filter.GetType().IsGenericType)
                {
                    var genericFilterType = filter.GetType().GetGenericTypeDefinition();

                    if (genericFilterType == typeof(KeuzeFilter<,>) ||
                        genericFilterType == typeof(LogischeFilter<,>))
                    {
                        dynamic castedFilter = Convert.ChangeType(filter, actualFilterType);
                        Type actualDataType = castedFilter.Data.GetType();
                        dynamic castedFilterInstelling = Convert.ChangeType(castedFilter.Data, actualDataType);

                        Type type = actualFilterType.GetGenericArguments()[1];
                        Instellen(castedFilterInstelling.Titel, type, castedFilterInstelling.PropertyUitDataGrid, castedFilterInstelling.FilterOpties);
                    }
                }
            }
        }
    }
}