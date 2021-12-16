﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filter.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter.Filters.Tests
{
    [TestClass()]
    public class ActionFilterTests
    {
        [TestMethod()]
        public async Task FilterenTest_ZonderShortcut()
        {
            ActionFilter af = new ActionFilter();
            af.Titel = "Leverancier starten";
            af.ShortCut = "A";

            var result = await af.Filteren("l");

            Assert.AreEqual(1, result.Count);
        }
        [TestMethod()]
        public async Task FilterenTest_MetShortcut()
        {
            ActionFilter af = new ActionFilter();
            af.Titel = "Leverancier starten";
            af.ShortCut = "A";

            var result = await af.Filteren("A l");

            Assert.AreEqual(1, result.Count);
        }
        [TestMethod()]
        public async Task FilterenTest_NietToepasselijk()
        {
            ActionFilter af = new ActionFilter();
            af.Titel = "Leverancier starten";
            af.ShortCut = "A";

            var result = await af.Filteren("qsfd");

            Assert.AreEqual(0, result.Count);
        }
    }
}