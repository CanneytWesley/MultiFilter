using Filter;

namespace GUITests
{
    public class LeverancierModel : IModel<DBLeverancier>
    {
        public DBLeverancier Model { get; }
        public string Naam { get; set; }

        public LeverancierModel(DBLeverancier leverancier)
        {
            Model = leverancier;
            Naam = leverancier.Naam;
        }
    }
}
