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
            FilterBerekenen<Lot> f = new FilterBerekenen<Lot>(SeedLoten.GetSeed(),Soort.En);

            f.Instellen("Certificaat",typeof(DBCertificaat), p => p.Certificaat, FilterOptie.IndexOf);

            var filter = new KeuzeFilter<DBCertificaat>(null, "Certificaat", "C");

            f.Filteren(new List<IResult>() { new Result(filter,"2.2",new DBCertificaat("2.2"),null,new Icon()) });

            Assert.AreEqual(6, f.Resultaat.Count);
        }
        [TestMethod()]
        public void FilterenTest_GewoneFilter_FilterTitelVerschilt()
        {
            FilterBerekenen<Lot> f = new FilterBerekenen<Lot>(SeedLoten.GetSeed(),Soort.En);

            f.Instellen("Certificaat2",typeof(DBCertificaat), p => p.Certificaat, FilterOptie.IndexOf);

            var filter = new KeuzeFilter<DBCertificaat>(null, "Certificaat", "C");

            f.Filteren(new List<IResult>() { new Result(filter,"2.2",new DBCertificaat("2.2"),null,new Icon()) });

            Assert.AreEqual(8, f.Resultaat.Count);
        }
    }
}