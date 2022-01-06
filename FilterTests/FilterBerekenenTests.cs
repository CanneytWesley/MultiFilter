using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filter.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUITests.Data;
using GUITests.Data.Certificaat;

namespace Filter.Filters.Tests
{
    [TestClass()]
    public class FilterBerekenenTests
    {
        [TestMethod()]
        public void FilterenTest_GewoneFilter()
        {
            var filter = new KeuzeFilter<Lot,DBCertificaat>(new Testdatalot() { Titel = "Certificaat", Shortcut="C" });
            var f = new FilterBerekenen<Lot>(SeedLoten.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new Result(filter,"2.2",new DBCertificaat("2.2"),null,new Icon()) });

            Assert.AreEqual(6, f.Resultaat.Count);
        }
        [TestMethod()]
        public void FilterenTest_GewoneFilter_FilterTitelVerschilt()
        {
            var filter = new KeuzeFilter<Lot,DBCertificaat>(new Testdatalot() { Titel = "Certificaat", Shortcut="C" });
            var filter2 = new KeuzeFilter<Lot,DBCertificaat>(new Testdatalot() { Titel = "Certificaat2", Shortcut="C" });
            var f = new FilterBerekenen<Lot>(SeedLoten.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new Result(filter2,"2.2",new DBCertificaat("2.2"),null,new Icon()) });

            Assert.AreEqual(8, f.Resultaat.Count);
        }
    }

    public class Testdatalot : IKeuzeFilterInstellingen<Lot, DBCertificaat>
    {
        public Func<DBCertificaat, string> PropertyOmMeeTeFilteren { get; set; }
        public Func<Lot, string> PropertyUitDataGrid { get; set; } = p => p.Certificaat;
        public string Titel { get; set; } 
        public string Shortcut { get; set; }
        public FilterOptie FilterOpties { get; set; }
        public Icon Icon { get; set; }

        public Task<List<DBCertificaat>> GetData()
        {
            return Task.FromResult(new List<DBCertificaat>() {
                new DBCertificaat( "Bla"),
                new DBCertificaat("Oef") ,
                new DBCertificaat("aaa") ,
                new DBCertificaat("wes") ,
                new DBCertificaat("ttt") ,
            });
        }
    }
}