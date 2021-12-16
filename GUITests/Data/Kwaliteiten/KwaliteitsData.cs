using Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUITests.Data.Kwaliteiten
{
    public class KwaliteitsData : IData<DBKwaliteit>
    {
        public Task<List<IModel<DBKwaliteit>>> GetData()
        {
            return Task.FromResult(new List<IModel<DBKwaliteit>>()
            {
                new KwaliteitModel(new DBKwaliteit("A")),
                new KwaliteitModel(new DBKwaliteit("B")),
                new KwaliteitModel(new DBKwaliteit("C")),
                new KwaliteitModel(new DBKwaliteit("D") ),
                new KwaliteitModel(new DBKwaliteit("E") ),
                new KwaliteitModel(new DBKwaliteit("F") ),
                new KwaliteitModel(new DBKwaliteit("G") ),
                new KwaliteitModel(new DBKwaliteit("H") ),
                new KwaliteitModel(new DBKwaliteit("I") ),
                new KwaliteitModel(new DBKwaliteit("J") ),
                new KwaliteitModel(new DBKwaliteit("K") ),
                new KwaliteitModel(new DBKwaliteit("L") ),
                new KwaliteitModel(new DBKwaliteit("M") ),
                new KwaliteitModel(new DBKwaliteit("N") ),
                new KwaliteitModel(new DBKwaliteit("O") ),
                new KwaliteitModel(new DBKwaliteit("P") ),
                new KwaliteitModel(new DBKwaliteit("Q") ),
                new KwaliteitModel(new DBKwaliteit("R") ),
                new KwaliteitModel(new DBKwaliteit("S") ),
                new KwaliteitModel(new DBKwaliteit("T") ),
                new KwaliteitModel(new DBKwaliteit("U") ),
                new KwaliteitModel(new DBKwaliteit("V") ),
                new KwaliteitModel(new DBKwaliteit("W") ),
                new KwaliteitModel(new DBKwaliteit("X") ),
                new KwaliteitModel(new DBKwaliteit("Y") ),
                new KwaliteitModel(new DBKwaliteit("Z") ),
            });
        }
    }
}
