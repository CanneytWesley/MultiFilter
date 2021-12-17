using System.Text.RegularExpressions;

namespace Filter.Filters
{
    public abstract class BaseFilter
    {
        public string Titel { get; set; }
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
                if (ShortCut.ToUpper() == uitvoeren.Substring(0, 1).ToUpper())
                    return true;
                else
                    return false;
            }
            else return true;
        }

        public string VerwijderShortCut(string uitvoeren)
        {
            if (HasShortCut(uitvoeren))
                return uitvoeren.Substring(2);
            else return uitvoeren;
        }

        private bool HasShortCut(string uitvoeren)
        {
            if (uitvoeren == null) return false;
            if (uitvoeren.Length < 2) return false;

            var gevonden = Regex.Matches(uitvoeren.Substring(0, 1), @"[a-zA-Z]").Count;
            var spatie = uitvoeren.Substring(1, 1) == " ";

            return gevonden == 1 && spatie;
        }

        public BaseFilter()
        {
            Icon = new Icon();
        }
    }
}