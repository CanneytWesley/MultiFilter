using System;

namespace Filter.Filters
{
    public interface IBerekening<T>
    {

        public FilterOptie FilterOptie { get; set; }

        public Type FilterTrigger { get; set; }

        public string FilterTitel { get; set; }

    }
}