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
        public void FilterenTest_LogischeFilterTest_LengteGroterDan15000_Resultaat0()
        {
            var filter = new LogischeFilter<Lot,double>(new LogischeFilterInstelling());
            var f = new FilterBerekenen<Lot>(SeedLoten.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new Result(filter,">15000",null, (IResult result) => { },new Icon()) });

            Assert.AreEqual(0, f.Resultaat.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogischeFilterTest_LengteGroterDan14000_Resultaat1()
        {
            var filter = new LogischeFilter<Lot,double>(new LogischeFilterInstelling());
            var f = new FilterBerekenen<Lot>(SeedLoten.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new Result(filter,">14000",null, (IResult result) => { },new Icon()) });

            Assert.AreEqual(1, f.Resultaat.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogischeFilterTest_LengteGroterOfGelijkAan15000_Resultaat1()
        {
            var filter = new LogischeFilter<Lot,double>(new LogischeFilterInstelling());
            var f = new FilterBerekenen<Lot>(SeedLoten.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new Result(filter,">=15000",null, (IResult result) => { },new Icon()) });

            Assert.AreEqual(1, f.Resultaat.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogischeFilterTest_LengteGroterOfGelijkAan0_Resultaat8()
        {
            var filter = new LogischeFilter<Lot,double>(new LogischeFilterInstelling());
            var f = new FilterBerekenen<Lot>(SeedLoten.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new Result(filter,">=0",null, (IResult result) => { },new Icon()) });

            Assert.AreEqual(8, f.Resultaat.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogischeFilterTest_LengteGroterOfGelijkAan0EnKleinerDan11000_1_Resultaat1()
        {
            var filter = new LogischeFilter<Lot,double>(new LogischeFilterInstelling());
            var f = new FilterBerekenen<Lot>(SeedLoten.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new Result(filter,">=0&<11000",null, (IResult result) => { },new Icon()) });

            Assert.AreEqual(1, f.Resultaat.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogischeFilterTest_LengteGroterOfGelijkAan0EnKleinerDan11000_2_Resultaat1()
        {
            var filter = new LogischeFilter<Lot,double>(new LogischeFilterInstelling());
            var f = new FilterBerekenen<Lot>(SeedLoten.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new Result(filter,">=0en<11000",null, (IResult result) => { },new Icon()) });

            Assert.AreEqual(1, f.Resultaat.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogischeFilterTest_LengteGroterOfGelijkAan0OfKleinerDan11000_1_Resultaat8()
        {
            var filter = new LogischeFilter<Lot,double>(new LogischeFilterInstelling());
            var f = new FilterBerekenen<Lot>(SeedLoten.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new Result(filter,">=0|<11000",null, (IResult result) => { },new Icon()) });

            Assert.AreEqual(8, f.Resultaat.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogischeFilterTest_LengteGroterOfGelijkAan0OfKleinerDan11000_2_Resultaat8()
        {
            var filter = new LogischeFilter<Lot,double>(new LogischeFilterInstelling());
            var f = new FilterBerekenen<Lot>(SeedLoten.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new Result(filter,">=0of<11000",null, (IResult result) => { },new Icon()) });

            Assert.AreEqual(8, f.Resultaat.Count);
        }
    }

    public class LogischeFilterInstelling : ILogischeFilterInstellingen<Lot, double>
    {
        public Func<Lot, double> PropertyUitDataGrid { get; set; }
        = p => p.Lengte;
        public string Titel { get; set; }
        = "Lengte filter";
        public string Shortcut { get; set; }
        = "B";
        public FilterOptie FilterOpties { get; set; }
        = FilterOptie.Exact;
        public Icon Icon { get; set; }
        = new Icon();
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