using Filter;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GUITests
{
    public class ProductenData : IData<DBProduct>
    {
        public Func<DBProduct, string> Property { get; set; }
        = p => p.Naam;

        public Task<List<DBProduct>> GetData()
        {
            return Task.FromResult(new List<DBProduct>() 
            { 
                new DBProduct("PL10"),
                new DBProduct("PL20"),
                new DBProduct("PL30"),
                new DBProduct("HEA400"),
                new DBProduct("PL40") 
            });
        }
    }
}
