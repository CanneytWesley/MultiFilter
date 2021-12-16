using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUITests.Data.Certificaat
{
    public class DBCertificaat
    {
        public DBCertificaat(string naam)
        {
            this.Naam = naam;
        }

        public string Naam { get; set; }
    }
}
