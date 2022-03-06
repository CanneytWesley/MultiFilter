using System;

namespace MultiFilter.Core.Filters.Model
{
    public interface IModel<T>
    {
        public T Model { get; }

        public string Name { get; set; }
    }
}