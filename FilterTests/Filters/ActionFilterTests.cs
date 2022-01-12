using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filter.Filters;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter.Filters.Tests
{
    [TestClass()]
    public partial class ActionFilterTests
    {

        [TestMethod()]
        public async Task FilterenTest_ZonderShortcut()
        {


            ActionFilter af = new ActionFilter(new FilterInstellingTestClass());
            af.Title = "Leverancier starten";
            af.ShortCut = "A";

            var result = await af.Filter("l");

            Assert.AreEqual(1, result.Count);
        }
        [TestMethod()]
        public async Task FilterenTest_MetShortcut()
        {
            ActionFilter af = new ActionFilter(new FilterInstellingTestClass());
            af.Title = "Leverancier starten";
            af.ShortCut = "A";

            var result = await af.Filter("A l");

            Assert.AreEqual(1, result.Count);
        }
        [TestMethod()]
        public async Task FilterenTest_NietToepasselijk()
        {
            ActionFilter af = new ActionFilter(new FilterInstellingTestClass());
            af.Title = "Leverancier starten";
            af.ShortCut = "A";

            var result = await af.Filter("qsfd");

            Assert.AreEqual(0, result.Count);
        }
    }
}