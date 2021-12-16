namespace GUITests
{
    public class DBLeverancier
    {
        public long ID { get; set; }

        public string Naam { get; set; }

        public string Adres { get; set; }

        public DBLeverancier(string naam)
        {
            Naam = naam;
        }
    }
}
