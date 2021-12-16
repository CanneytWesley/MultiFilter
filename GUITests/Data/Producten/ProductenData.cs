using Filter;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GUITests
{
    public class ProductenData : IData<DBProduct>
    {
        public Task<List<IModel<DBProduct>>> GetData()
        {
            return Task.FromResult(new List<IModel<DBProduct>>() 
            { 
                new ProductenModel(new DBProduct("PL10")),
                new ProductenModel(new DBProduct("PL20")),
                new ProductenModel(new DBProduct("PL30")),
                new ProductenModel(new DBProduct("HEA400") ),
                new ProductenModel(new DBProduct("PL40") )
            });
        }
    }
}
