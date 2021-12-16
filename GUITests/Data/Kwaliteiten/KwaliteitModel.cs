using Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUITests.Data.Kwaliteiten
{
    public class KwaliteitModel : IModel<DBKwaliteit>
    {
        public DBKwaliteit Model { get; }
        public string Naam { get; set; }

        public KwaliteitModel(DBKwaliteit kwal)
        {
            Model = kwal;
            Naam = kwal.Naam;
        }
    }
}
