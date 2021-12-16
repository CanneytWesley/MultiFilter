using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filter
{
    public interface IData<T>
    {
        public Task<List<IModel<T>>> GetData();
    }

    public interface IModel<T>
    { 
        public T Model { get; }

        public string Naam { get; set; }
    }

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