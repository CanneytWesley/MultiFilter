using Filter;

namespace GUITests
{
    public class ProductenModel : IModel<DBProduct>
    {
        public DBProduct Model { get; }
        public string Naam { get; set; }

        public ProductenModel(DBProduct product)
        {
            Model = product;
            Naam = product.Naam;
        }
    }
}
