namespace Filter.Filters
{
    public class KeuzeModel
    {
        public KeuzeModel(string onderdeel, object model)
        {
            Onderdeel = onderdeel;
            Model = model;
        }
        public KeuzeModel(string onderdeel)
        {
            Onderdeel = onderdeel;
        }

        public object Model { get; private set; }

        public string Onderdeel { get; private set; }
    }

}