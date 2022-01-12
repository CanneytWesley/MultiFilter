

namespace Filter.Filters.Model
{
    public class Icon
    {

        public Icon(string iconColor, string iconPath)
        {
            IconPath = iconPath;
            IconColor = iconColor;
        }

        public string IconPath { get; set; }

        public string IconColor { get; set; }

        public Icon()
        {

        }
    }

}