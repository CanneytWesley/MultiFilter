using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filter.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUITests.Data;

namespace Filter.Filters.Tests
{
    [TestClass()]
    public class KeuzeFilterTests
    {
        [TestMethod()]
        public async Task FilterenTest()
        {
            var kf = new KeuzeFilter<string,string>(new Testdata());
            await kf.Initialiseren();

            var result = await kf.Filteren("bl");

            Assert.AreEqual(1, result.Count);
        }
        [TestMethod()]
        public async Task FilterenTest_2()
        {
            var kf = new KeuzeFilter<string,string>(new Testdata());
            await kf.Initialiseren();

            var result = await kf.Filteren("A bl");

            Assert.AreEqual(0, result.Count);
        }
        [TestMethod()]
        public async Task FilterenTest_3()
        {
            var kf = new KeuzeFilter<string,string>(new Testdata());
            await kf.Initialiseren();

            var result = await kf.Filteren("P bl");

            Assert.AreEqual(0, result.Count);
        }
        [TestMethod()]
        public async Task FilterenTest_4()
        {
            var kf = new KeuzeFilter<string,string>(new Testdata());
            await kf.Initialiseren();

            var result = await kf.Filteren("P a");

            Assert.AreEqual(0, result.Count);
        }
    }

    public class Testdata : IKeuzeFilterInstellingen<string, string>
    {
        public Func<string, string> Property { get; set; }
        public Func<string, string> PropertyOmMeeTeFilteren { get; set; }
        = p => p;
        public Func<string, string> PropertyUitDataGrid { get; set; }
        public string Titel { get; set; }
        public string Shortcut { get; set; }
        = "C";
        public FilterOptie FilterOpties { get; set; }
        = FilterOptie.IndexOf;
        public Icon Icon { get; set; }

        public Task<List<string>> GetData()
        {
            return Task.FromResult(new List<string>() { 
                "Bla",
                "Oef" ,
                "aaa" ,
                "wes" ,
                "ttt" ,
            });
        }

    }
}