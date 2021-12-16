using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filter
{
    public interface IData<T>
    {
        Func<T, string> Property { get; set; }

        public Task<List<T>> GetData();
    }
}