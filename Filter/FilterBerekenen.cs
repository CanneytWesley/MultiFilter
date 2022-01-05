using System;
using System.Collections.Generic;
using System.Linq;

namespace Filter.Filters
{
    [Flags]
    public enum FilterOptie
    { 
        /// <summary>
        /// Als indexof voldoende is voor de filter dan deze flag meegeven
        /// </summary>
        IndexOf,
        /// <summary>
        /// Bij string zal het exacte woord moeten overeenkomen
        /// </summary>
        Exact,
        /// <summary>
        /// Standaard wordt geen rekening gehouden met hoofdletters of kleine letters, indien je dit toch wil moet je deze flag aanzetten
        /// </summary>
        OrdinalCase,
    }

    public interface IFilterInstellingen<T>
    {

        public FilterOptie FilterOptie { get; set; }

        public Type FilterTrigger { get; set; }

        public string FilterTitel { get; set; }

    }

    public class StringFilterInstelling<T> : IFilterInstellingen<T>
    {
        public Func<T, string> Property { get; set; }

        public FilterOptie FilterOptie { get; set; }
        public Type FilterTrigger { get; set; }
        public string FilterTitel { get; set; }

        public StringFilterInstelling(string filterTitel, Type filterTrigger, Func<T, string> property, FilterOptie filterOptie)
        {
            Property = property;
            FilterOptie = filterOptie;
            FilterTrigger = filterTrigger;
            FilterTitel = filterTitel;
        }
    }
    public class DoubleFilterInstelling<T> : IFilterInstellingen<T>
    {
        public Func<T, double> Property { get; set; }
        public FilterOptie FilterOptie { get; set; }
        public Type FilterTrigger { get; set; }
        public string FilterTitel { get; set; }

        public DoubleFilterInstelling(string filterTitel, Type filterTrigger, Func<T, double> property, FilterOptie filterOptie)
        {
            Property = property;
            FilterOptie = filterOptie;
            FilterTrigger = filterTrigger;
            FilterTitel = filterTitel;
        }
    }

    public class FilterBerekenen<T>
    {
        private List<T> AlleItems;
        private List<T> Items;

        private int AantalKeerGefilterd;

        public List<IFilterInstellingen<T>> FilterInstellingen { get; set; }

        public List<T> Resultaat { get {
            if (AantalKeerGefilterd == 0) return AlleItems;
            else return Items;
            } 
        }
        public Soort Soort { get; }

        public FilterBerekenen(List<T> alleItems, Soort soort)
        {
            AlleItems = alleItems;
            Items = new List<T>();
            FilterInstellingen = new List<IFilterInstellingen<T>>();
            Soort = soort;
        }

        public void Instellen(string filterTitel, Type filterTrigger, Func<T, double> Property, FilterOptie val)
            => FilterInstellingen.Add(new DoubleFilterInstelling<T>(filterTitel, filterTrigger, Property, val));
        public void Instellen(string filterTitel, Type filterTrigger, Func<T, string> Property, FilterOptie val)
            => FilterInstellingen.Add(new StringFilterInstelling<T>(filterTitel, filterTrigger, Property, val));
        


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

        public void Filteren(List<IResult> resultaten)
        {
            var ModelFilters = resultaten.Where(p => p.Model.Model != null).ToList();
            var AndereFilters = resultaten.Where(p => p.Model.Model == null).ToList();
            ModelFilteren(ModelFilters);
            AndereFilteren(AndereFilters);
        }

        private void AndereFilteren(List<IResult> resultaten)
        { 
        
        }
        private void ModelFilteren(List<IResult> resultaten)
        {
            foreach (var filterresultaat in resultaten)
            {
                var type = filterresultaat?.Model?.Model.GetType();

                var filter = FilterInstellingen.FirstOrDefault(p => p.FilterTrigger == type && p.FilterTitel == filterresultaat.Filter.Titel);

                if (filter is StringFilterInstelling<T> sfi)
                {
                    StringComparison sc = StringComparison.OrdinalIgnoreCase;
                    if (sfi.FilterOptie.HasFlag(FilterOptie.OrdinalCase))
                        sc = StringComparison.Ordinal;

                    if (sfi.FilterOptie.HasFlag(FilterOptie.IndexOf))
                    {
                        var result = AlleItems.Where(p => sfi.Property(p).IndexOf(filterresultaat.Model.Onderdeel, sc) != -1).ToList();
                        Add(result);
                    }
                    else if (sfi.FilterOptie.HasFlag(FilterOptie.Exact))
                    {
                        var result = AlleItems.Where(p => sfi.Property(p) == filterresultaat.Model.Onderdeel).ToList();
                        Add(result);

                    }

                }
                else if (filter is DoubleFilterInstelling<T> dfi)
                {

                }

            }
        }
    }
}