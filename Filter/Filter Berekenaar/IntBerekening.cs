using System;

namespace Filter.Filters
{
    public class IntBerekening<T> : IBerekening<T>
    {
        public Func<T, int> Property { get; set; }
        public FilterOptie FilterOptie { get; set; }
        public Type FilterTrigger { get; set; }
        public string FilterTitel { get; set; }

        public IntBerekening(string filterTitel, Type filterTrigger, Func<T, int> property, FilterOptie filterOptie)
        {
            Property = property;
            FilterOptie = filterOptie;
            FilterTrigger = filterTrigger;
            FilterTitel = filterTitel;
        }
    }
}