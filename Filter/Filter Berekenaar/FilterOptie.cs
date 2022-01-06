using System;

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
}