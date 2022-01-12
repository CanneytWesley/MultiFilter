﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public async Task FilterTest_WithoutShortcut()
        {


            ActionFilter af = new ActionFilter(new FilterInstellingTestClass());
            af.Title = "Leverancier starten";
            af.ShortCut = "A";

            var result = await af.Filter("l");

            Assert.AreEqual(1, result.Count);
        }
        [TestMethod()]
        public async Task FilterTest_WithShortcut()
        {
            ActionFilter af = new ActionFilter(new FilterInstellingTestClass());
            af.Title = "Leverancier starten";
            af.ShortCut = "A";

            var result = await af.Filter("A l");

            Assert.AreEqual(1, result.Count);
        }
        [TestMethod()]
        public async Task FilterTest_NonExistingFilter()
        {
            ActionFilter af = new ActionFilter(new FilterInstellingTestClass());
            af.Title = "Leverancier starten";
            af.ShortCut = "A";

            var result = await af.Filter("qsfd");

            Assert.AreEqual(0, result.Count);
        }
    }
}