using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUITests.Data;
using Filter.Filter_Settings;
using Filter.Filter_Calculator;
using MultiFilter.Core.Filters;
using MultiFilter.Core.Filters.Model;

namespace MultiFilter.CoreTests.Filters
{
    [TestClass()]
    public class KeuzeFilterTests
    {
        [TestMethod()]
        public async Task FilterTest()
        {
            var kf = new MultipleChoiceFilter<string, string>(new Testdata());
            await kf.Initialise();

            var result = await kf.Filter("bl");

            Assert.AreEqual(0, result.Count);
        }
        [TestMethod()]
        public async Task FilterTest_2()
        {
            var kf = new MultipleChoiceFilter<string, string>(new Testdata());
            await kf.Initialise();

            var result = await kf.Filter("A bl");

            Assert.AreEqual(0, result.Count);
        }
        [TestMethod()]
        public async Task FilterTest_3()
        {
            var kf = new MultipleChoiceFilter<string, string>(new Testdata());
            await kf.Initialise();

            var result = await kf.Filter("P bl");

            Assert.AreEqual(0, result.Count);
        }
        [TestMethod()]
        public async Task FilterTest_4()
        {
            var kf = new MultipleChoiceFilter<string, string>(new Testdata());
            await kf.Initialise();

            var result = await kf.Filter("P a");

            Assert.AreEqual(0, result.Count);
        }
    }

    public class Testdata : IMultipleChoiceSettings<string, string>
    {
        public Func<string, string> Property { get; set; }
        public Func<string, string> PropertyToFilterWith { get; set; }
        = p => p;
        public Func<string, string> PropertyFromDataset { get; set; }
        public string Title { get; set; }
        public string Shortcut { get; set; }
        = "C";
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