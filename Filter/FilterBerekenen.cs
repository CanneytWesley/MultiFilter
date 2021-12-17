using System.Collections.Generic;
using System.Linq;

namespace Filter.Filters
{
    public class FilterBerekenen<T>
    {
        private List<T> AlleItems;
        private List<T> Items;

        private int AantalKeerGefilterd;

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
            Soort = soort;
        }

        public void Add(IEnumerable<T> items)
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
    }
}