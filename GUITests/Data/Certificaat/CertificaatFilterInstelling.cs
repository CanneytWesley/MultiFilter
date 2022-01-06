using Filter;
using Filter.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GUITests.Data.Certificaat
{
    public class CertificaatFilterInstelling : IKeuzeFilterInstellingen<Lot, DBCertificaat>
    {
        public string Titel { get; set; }
        public string Shortcut { get; set; }
        public FilterOptie FilterOpties { get; set; }
        public Func<DBCertificaat, string> PropertyOmMeeTeFilteren { get; set; }
        public Func<Lot, string> PropertyUitDataGrid { get; set; }
        public Icon Icon { get; set; }

        public CertificaatFilterInstelling()
        {
            Titel = "Certificaten";
            Shortcut = "C";
            FilterOpties = FilterOptie.Exact;
            PropertyOmMeeTeFilteren = p => p.Naam;
            PropertyUitDataGrid = p => p.Certificaat;
            Icon = new Icon(Brushes.Pink.ToString(), Icons.Bericht);
        }

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
