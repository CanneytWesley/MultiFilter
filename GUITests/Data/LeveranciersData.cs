using Filter;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GUITests
{
    public class LeveranciersData : IData<DBLeverancier>
    {
        public Task<List<IModel<DBLeverancier>>> GetData()
        {
            return Task.FromResult(new List<IModel<DBLeverancier>>() 
            { 
                new LeverancierModel(new DBLeverancier("Bakker")),
                new LeverancierModel(new DBLeverancier("Boekhouder")),
                new LeverancierModel(new DBLeverancier("Slager")),
                new LeverancierModel(new DBLeverancier("Groentewinkel") )
            });
        }
    }
}
