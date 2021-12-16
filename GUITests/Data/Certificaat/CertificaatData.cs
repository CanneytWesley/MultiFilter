using Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUITests.Data.Certificaat
{
    public class CertificaatData : IData<DBCertificaat>
    {
        public Func<DBCertificaat, string> Property { get; set; }
        = p => p.Naam;

        public Task<List<DBCertificaat>> GetData()
        {
            return Task.FromResult(new List<DBCertificaat>()
            {
                new DBCertificaat("2.2"),
                new DBCertificaat("3.2"),
            });
        }
    }
}
