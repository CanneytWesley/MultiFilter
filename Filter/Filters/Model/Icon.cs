

namespace Filter.Filters
{
    public class Icon
    {

        public Icon(string iconKleur, string iconPad)
        {
            IconPad = iconPad;
            IconKleur = iconKleur;
        }

        public string IconPad { get; set; }

        public string IconKleur { get; set; }

        public Icon()
        {

        }
    }

}