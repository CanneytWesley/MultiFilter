using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filter.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter.Filters.Tests
{
    [TestClass()]
    public class KeuzeFilterTests
    {
        [TestMethod()]
        public async Task FilterenTest()
        {
            KeuzeFilter<string> kf = new KeuzeFilter<string>(new Testdata(), "Producten", "P");
            await kf.Initialiseren();

            var result = await kf.Filteren("bl");

            Assert.AreEqual(1, result.Count);
        }
        [TestMethod()]
        public async Task FilterenTest_2()
        {
            KeuzeFilter<string> kf = new KeuzeFilter<string>(new Testdata(), "Producten", "P");
            await kf.Initialiseren();

            var result = await kf.Filteren("A bl");

            Assert.AreEqual(0, result.Count);
        }
        [TestMethod()]
        public async Task FilterenTest_3()
        {
            KeuzeFilter<string> kf = new KeuzeFilter<string>(new Testdata(), "Producten", "P");
            await kf.Initialiseren();

            var result = await kf.Filteren("P bl");

            Assert.AreEqual(1, result.Count);
        }
        [TestMethod()]
        public async Task FilterenTest_4()
        {
            KeuzeFilter<string> kf = new KeuzeFilter<string>(new Testdata(), "Producten", "P");
            await kf.Initialiseren();

            var result = await kf.Filteren("P a");

            Assert.AreEqual(2, result.Count);
        }
    }

    public class Testdata : IData<string>
    {
        public Func<string, string> Property { get; set; }
        = p => p;

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