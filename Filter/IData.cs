using System.Collections.Generic;
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
}