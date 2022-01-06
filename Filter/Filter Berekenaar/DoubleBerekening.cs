using System;

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
    }
}