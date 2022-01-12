using System;

namespace Filter.Filters
{
    [Flags]
    public enum FilterOption
    { 
        IndexOf,
        Exact,
        OrdinalCase,
    }
}