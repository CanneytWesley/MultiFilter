using System;

namespace Filter.Filters
{
    public class StringBerekening<T> : IBerekening<T>
    {
        public Func<T, string> Property { get; set; }

        public FilterOptie FilterOptie { get; set; }
        public Type FilterTrigger { get; set; }
        public string FilterTitel { get; set; }

        public StringBerekening(string filterTitel, Type filterTrigger, Func<T, string> property, FilterOptie filterOptie)
        {
            Property = property;
            FilterOptie = filterOptie;
            FilterTrigger = filterTrigger;
            FilterTitel = filterTitel;
        }
    }
}