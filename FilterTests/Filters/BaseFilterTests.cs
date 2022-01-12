using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filter.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterTests.Filters
{
    [TestClass()]
    public class BaseFilterTests
    {
        [TestMethod()]
        public void DeleteShortCutTest()
        {
            ActionFilter action = new ActionFilter(new FilterSettingsTestClass());
            action.ShortCut = "B";

            var result = action.RemoveShortCut("B boe");

            Assert.AreEqual("boe", result);
        }

        [TestMethod()]
        public void DeleteShortCutTest2()
        {
            ActionFilter action = new ActionFilter(new FilterSettingsTestClass());
            action.ShortCut = "B";

            var result = action.RemoveShortCut("B");

            Assert.AreEqual("B", result);
        }

        [TestMethod()]
        public void DeleteShortCutTest3()
        {
            ActionFilter action = new ActionFilter(new FilterSettingsTestClass());
            action.ShortCut = "B";

            var result = action.RemoveShortCut("Babd");

            Assert.AreEqual("Babd", result);
        }

        [TestMethod()]
        public void TestShortCutTest_NoShortCut()
        {
            ActionFilter action = new ActionFilter(new FilterSettingsTestClass());
            action.ShortCut = "B";

            var result = action.TestShortCut("Babd");

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void TestShortCutTest_WrongShortCut()
        {
            ActionFilter action = new ActionFilter(new FilterSettingsTestClass());
            action.SetShortcuts(new List<string>() { "A", "B" });
            action.ShortCut = "B";


            var result = action.TestShortCut("A Babd");

            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void TestShortCutTest_CorrectShortCut()
        {
            ActionFilter action = new ActionFilter(new FilterSettingsTestClass());
            action.ShortCut = "A";

            var result = action.TestShortCut("A Babd");

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void HasThisShortCutTest_false()
        {
            ActionFilter af = new ActionFilter(new FilterSettingsTestClass());

            var result = af.HasThisShortCut("");

            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void HasThisShortCutTest_true()
        {
            ActionFilter af = new ActionFilter(new FilterSettingsTestClass());

            var result = af.HasThisShortCut("c bla");

            Assert.AreEqual(true, result);
        }
    }
}