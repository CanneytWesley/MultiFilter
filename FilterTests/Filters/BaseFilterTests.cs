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
    public class BaseFilterTests
    {
        [TestMethod()]
        public void VerwijderShortCutTest()
        {
            ActionFilter actie = new ActionFilter();
            actie.ShortCut = "B ";

            var result = actie.VerwijderShortCut("B boe");

            Assert.AreEqual("boe", result);
        }

        [TestMethod()]
        public void VerwijderShortCutTest2()
        {
            ActionFilter actie = new ActionFilter();
            actie.ShortCut = "B ";

            var result = actie.VerwijderShortCut("B");

            Assert.AreEqual("B", result);
        }

        [TestMethod()]
        public void VerwijderShortCutTest3()
        {
            ActionFilter actie = new ActionFilter();
            actie.ShortCut = "B ";

            var result = actie.VerwijderShortCut("Babd");

            Assert.AreEqual("Babd", result);
        }

        [TestMethod()]
        public void TestShortCutTest()
        {
            Assert.Fail();
        }
    }
}