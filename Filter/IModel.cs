namespace Filter
{
    public interface IModel<T>
    { 
        public T Model { get; }

        public string Naam { get; set; }
    }
}