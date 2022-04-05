using MultiFilter.Core.Filters.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MultiFilter.Core.Filters
{
    public abstract class BaseFilter
    {
        private List<string> ShortCuts;

        public string Title { get; set; }
        public string ShortCut { get; set; } = "A";
        public Icon Icon { get; set; }

        /// <summary>
        /// Indien er een shortcut aanwezig is wordt deze gecontroleerd tegenover de huidige shortcut
        /// Deze is dus enkel false als er een shortcut is die niet overeenkomt met de huidige shortcut
        /// </summary>
        /// <param name="uitvoeren"></param>
        /// <returns></returns>
        public bool TestShortCut(string uitvoeren)
        {
            if (HasShortCut(uitvoeren))
            {
                if (HasThisShortCut(uitvoeren))
                    //Shortcut komt overeen met de shortcut van deze filter
                    return true;
                else
                    //Er is een shortcut maar die komt NIET overeen met de huidige shortcut
                    return false;
            }
            //Er is geen shortcut
            else return true;
        }

        public bool HasThisShortCut(string uitvoeren)
        {
            if (uitvoeren.Length < ShortCut.Length + 1) return false;

            return ShortCut.ToUpper() == uitvoeren.Substring(0, ShortCut.Length).ToUpper() && uitvoeren.Substring(ShortCut.Length, 1) == " ";
        }

        /// <summary>
        /// Verwijder de shortcut van de tekst
        /// </summary>
        /// <param name="uitvoeren"></param>
        /// <returns></returns>
        public string RemoveShortCut(string uitvoeren)
        {
            if (HasShortCut(uitvoeren))
                return uitvoeren.Substring(1 + ShortCut.Length);
            else return uitvoeren;
        }

        /// <summary>
        /// Heeft de tekst een bestaande shortcut?
        /// </summary>
        /// <param name="uitvoeren"></param>
        /// <returns></returns>
        private bool HasShortCut(string uitvoeren)
        {
            if (ShortCuts == null) ShortCuts = new List<string>() { ShortCut };
            return ShortCuts.Any(p => uitvoeren.ToUpper().StartsWith(p.ToUpper() + " "));
        }

        public BaseFilter()
        {
            Icon = new Icon();
        }

        public void SetShortcuts(List<string> shortcuts)
        {
            ShortCuts = shortcuts;
        }
    }
}