using System;

namespace Filter
{
    public interface IModel<T>
    { 
        public T Model { get; }

        public string Name { get; set; }
    }
}