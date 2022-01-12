using System;

namespace Filter
{
    public class FilterModel<T> : IModel<T>
    {
        public string Name { get; set; }
        public T Model { get; }

        public FilterModel(T model, string name)
        {
            Model = model;
            Name = name;
        }
    }
}