namespace Filter
{
    public class FilterModel<T> : IModel<T>
    {
        public string Naam { get; set; }
        public T Model { get; }

        public FilterModel(T model, string naam)
        {
            Model = model;
            Naam = naam;
        }
    }
}