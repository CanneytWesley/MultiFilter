using Filter;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GUITests
{
    public class LeveranciersData : IData<DBLeverancier>
    {
        public Func<DBLeverancier, string> Property { get; set; }
        = p => p.Naam;

        public Task<List<DBLeverancier>> GetData()
        {
            return Task.FromResult(new List<DBLeverancier>() 
            { 
                new DBLeverancier("Bakker"),
                new DBLeverancier("Boekhouder"),
                new DBLeverancier("Slager"),
                new DBLeverancier("Groentewinkel") 
            });
        }
    }
}
