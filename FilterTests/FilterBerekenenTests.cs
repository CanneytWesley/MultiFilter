using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filter.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUITests.Data;
using GUITests.Data.Certificaat;
using GUITests;

namespace Filter.Filters.Tests
{
    [TestClass()]
    public class FilterBerekenenTests
    {
        [TestMethod()]
        public void FilterenTest_GewoneFilter()
        {
            var filter = new KeuzeFilter<Friend,Company>(new Testdatacompany() { Titel = "Company", Shortcut="C" });
            var f = new FilterBerekenen<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new KeuzeModelResult(filter, "Luminus", new Company("Luminus"),null,new Icon()) });

            Assert.AreEqual(1, f.Resultaat.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogischeFilterTest_LengteGroterDan15000_Resultaat0()
        {
            var filter = new LogischeFilter<Friend,double>(new WeightFilterInstelling());
            var f = new FilterBerekenen<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new LogischResult(filter,">15000", new Icon()) });

            Assert.AreEqual(0, f.Resultaat.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogischeFilterTest_LengteGroterDan14000_Resultaat1()
        {
            var filter = new LogischeFilter<Friend,double>(new WeightFilterInstelling());
            var f = new FilterBerekenen<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new LogischResult(filter,">100", new Icon()) });

            Assert.AreEqual(4, f.Resultaat.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogischeFilterTest_LengteGroterOfGelijkAan15000_Resultaat1()
        {
            var filter = new LogischeFilter<Friend,double>(new WeightFilterInstelling());
            var f = new FilterBerekenen<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new LogischResult(filter,">=145",new Icon()) });

            Assert.AreEqual(1, f.Resultaat.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogischeFilterTest_LengteGroterOfGelijkAan0_Resultaat8()
        {
            var filter = new LogischeFilter<Friend,double>(new WeightFilterInstelling());
            var f = new FilterBerekenen<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new LogischResult(filter,">=0", new Icon()) });

            Assert.AreEqual(8, f.Resultaat.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogischeFilterTest_LengteGroterOfGelijkAan0EnKleinerDan11000_1_Resultaat1()
        {
            var filter = new LogischeFilter<Friend,double>(new WeightFilterInstelling());
            var f = new FilterBerekenen<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new LogischResult(filter,">=0&<100", new Icon()) });

            Assert.AreEqual(4, f.Resultaat.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogischeFilterTest_LengteGroterOfGelijkAan0EnKleinerDan11000_2_Resultaat1()
        {
            var filter = new LogischeFilter<Friend,double>(new WeightFilterInstelling());
            var f = new FilterBerekenen<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new LogischResult(filter,">=0en<100", new Icon()) });

            Assert.AreEqual(4, f.Resultaat.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogischeFilterTest_LengteGroterOfGelijkAan0OfKleinerDan11000_1_Resultaat8()
        {
            var filter = new LogischeFilter<Friend,double>(new WeightFilterInstelling());
            var f = new FilterBerekenen<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new LogischResult(filter,">=0|<11000", new Icon()) });

            Assert.AreEqual(8, f.Resultaat.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogischeFilterTest_LengteGroterOfGelijkAan0OfKleinerDan11000_2_Resultaat8()
        {
            var filter = new LogischeFilter<Friend,double>(new WeightFilterInstelling());
            var f = new FilterBerekenen<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new LogischResult(filter,">=0of<11000", new Icon()) });

            Assert.AreEqual(8, f.Resultaat.Count);
        }

        [TestMethod()]
        public void FilterenTest_LogischeFilterTest_LengteGroterOfGelijkAan0OfKleinerDan11000MetShortcut_2_Resultaat8()
        {
            var filter = new LogischeFilter<Friend,double>(new WeightFilterInstelling());
            var f = new FilterBerekenen<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new LogischResult(filter,">=0of<11000",new Icon()) });

            Assert.AreEqual(8, f.Resultaat.Count);
        }

        [TestMethod()]
        public void FilterenTest_LogischeFilterTest_LengteGroterOfGelijkAan0EnKleinerDan13000MetShortcut_2_Resultaat6()
        {
            var filter = new LogischeFilter<Friend,double>(new WeightFilterInstelling());
            var f = new FilterBerekenen<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Instellen(new List<IFilter>() { filter });


            f.Filteren(Soort.En, new List<IResult>() { new LogischResult(filter,">=0en<150",new Icon()) });

            Assert.AreEqual(7, f.Resultaat.Count);
        }
    }

    public class WeightFilterInstelling : ILogischeFilterInstellingen<Friend, double>
    {
        public Func<Friend, double> PropertyUitDataGrid { get; set; }
        = p => p.Weight;
        public string Titel { get; set; }
        = "Lengte filter";
        public string Shortcut { get; set; }
        = "B";
        public FilterOptie FilterOpties { get; set; }
        = FilterOptie.Exact;
        public Icon Icon { get; set; }
        = new Icon();
    }

    public class Testdatacompany : IKeuzeFilterInstellingen<Friend, Company>
    {
        public Func<Company, string> PropertyOmMeeTeFilteren { get; set; } = p => p.Name;
        public Func<Friend, string> PropertyUitDataGrid { get; set; } = p => p.Company;
        public string Titel { get; set; } = "Company";
        public string Shortcut { get; set; } = "C";
        public FilterOptie FilterOpties { get; set; }
        public Icon Icon { get; set; }

        public Task<List<Company>> GetData()
        {
            return Task.FromResult(new List<Company>() {
                new Company( "Luminus"),
                new Company("Oef") ,
                new Company("aaa") ,
                new Company("wes") ,
                new Company("ttt") ,
            });
        }
    }
}