﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Filter.Filter_Calculator;
using Filter.Filter_Results;
using Filter.Filter_Settings;
using MultiFilter.Core.Filters;
using MultiFilter.GUITests.Models;
using MultiFilter.Core.Filters.Model;

namespace MultiFilter.CoreTests
{
    [TestClass()]
    public class FilterBerekenenTests
    {
        [TestMethod()]
        public void BooleanFilterTest1()
        {
            var filter = new BooleanFilter<Friend>(new BestFriendSetting() { Title = "Company", Shortcut = "C" });
            var f = new FilterExecutor<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Setup(new List<IFilter>() { filter });


            f.Filter(Edit.And, new List<IResult>() { new BooleanResult(filter, "True",  new Icon()) });

            Assert.AreEqual(1, f.Result.Count);
        }
        [TestMethod()]
        public void FilterenTest_RegularFilter()
        {
            var filter = new MultipleChoiceFilter<Friend, Company>(new Testdatacompany() { Title = "Company", Shortcut = "C" });
            var f = new FilterExecutor<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Setup(new List<IFilter>() { filter });


            f.Filter(Edit.And, new List<IResult>() { new MultipleChoiceModelResult(filter, "Luminus", new Company("Luminus"), null, new Icon()) });

            Assert.AreEqual(1, f.Result.Count);
        }
        [TestMethod()]
        public void FilterenTest_RegularFilter_OnlyPart_Equal()
        {
            var filter = new MultipleChoiceFilter<Friend, Company>(new Testdatacompany() { Title = "Company", Shortcut = "C" });
            var f = new FilterExecutor<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Setup(new List<IFilter>() { filter });


            f.Filter(Edit.And, new List<IResult>() { new MultipleChoiceModelResult(filter, "Lum", new Company("Luminus"), null, new Icon()) });

            Assert.AreEqual(0, f.Result.Count);
        }
        [TestMethod()]
        public void FilterenTest_RegularFilter_OnlyPart_EndWithStar()
        {
            var filter = new MultipleChoiceFilter<Friend, Company>(new Testdatacompany() { Title = "Company", Shortcut = "C" });
            var f = new FilterExecutor<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Setup(new List<IFilter>() { filter });


            f.Filter(Edit.And, new List<IResult>() { new MultipleChoiceModelResult(filter, "Lum*", new Company("Luminus"), null, new Icon()) });

            Assert.AreEqual(1, f.Result.Count);
        }
        [TestMethod()]
        public void FilterenTest_RegularFilter_OnlyPart_StartsWithStar()
        {
            var filter = new MultipleChoiceFilter<Friend, Company>(new Testdatacompany() { Title = "Company", Shortcut = "C" });
            var f = new FilterExecutor<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Setup(new List<IFilter>() { filter });


            f.Filter(Edit.And, new List<IResult>() { new MultipleChoiceModelResult(filter, "*uminus", new Company("Luminus"), null, new Icon()) });

            Assert.AreEqual(1, f.Result.Count);
        }
        [TestMethod()]
        public void FilterenTest_RegularFilter_OnlyPart_TwoStar()
        {
            var filter = new MultipleChoiceFilter<Friend, Company>(new Testdatacompany() { Title = "Company", Shortcut = "C" });
            var f = new FilterExecutor<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Setup(new List<IFilter>() { filter });


            f.Filter(Edit.And, new List<IResult>() { new MultipleChoiceModelResult(filter, "*um*", new Company("Luminus"), null, new Icon()) });

            Assert.AreEqual(1, f.Result.Count);
        }

        [TestMethod()]
        public void FilterenTest_LogicalFilterTestLengthGreatherThan15000_Result0()
        {
            var filter = new LogicalFilter<Friend, double>(new WeightFilterInstelling());
            var f = new FilterExecutor<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Setup(new List<IFilter>() { filter });


            f.Filter(Edit.And, new List<IResult>() { new LogischResult(filter, ">15000", new Icon()) });

            Assert.AreEqual(0, f.Result.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogicalFilterTest_LengthGreaterThan14000_Result1()
        {
            var filter = new LogicalFilter<Friend, double>(new WeightFilterInstelling());
            var f = new FilterExecutor<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Setup(new List<IFilter>() { filter });


            f.Filter(Edit.And, new List<IResult>() { new LogischResult(filter, ">100", new Icon()) });

            Assert.AreEqual(4, f.Result.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogicalFilterTest_LengthGreaterOrEqualTo15000_Result1()
        {
            var filter = new LogicalFilter<Friend, double>(new WeightFilterInstelling());
            var f = new FilterExecutor<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Setup(new List<IFilter>() { filter });


            f.Filter(Edit.And, new List<IResult>() { new LogischResult(filter, ">=145", new Icon()) });

            Assert.AreEqual(1, f.Result.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogicalFilterTest_LengthGreaterOrEqualTo0_Result8()
        {
            var filter = new LogicalFilter<Friend, double>(new WeightFilterInstelling());
            var f = new FilterExecutor<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Setup(new List<IFilter>() { filter });


            f.Filter(Edit.And, new List<IResult>() { new LogischResult(filter, ">=0", new Icon()) });

            Assert.AreEqual(8, f.Result.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogicalFilterTest_LengthGreaterOrEqualTo0AndSmallerThan11000_1_Result1()
        {
            var filter = new LogicalFilter<Friend, double>(new WeightFilterInstelling());
            var f = new FilterExecutor<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Setup(new List<IFilter>() { filter });


            f.Filter(Edit.And, new List<IResult>() { new LogischResult(filter, ">=0&<100", new Icon()) });

            Assert.AreEqual(4, f.Result.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogicalFilterTest_LengthGreaterOrEqualTo0AndSmallerThan11000_1_Result1_MetSpaties()
        {
            var filter = new LogicalFilter<Friend, double>(new WeightFilterInstelling());
            var f = new FilterExecutor<Friend>();
            f.SetData(SeedFriends.GetSeed());
            f.Setup(new List<IFilter>() { filter });

            f.Filter(Edit.And, new List<IResult>() { new LogischResult(filter, "   >=    0   &   <    100", new Icon()) });

            Assert.AreEqual(4, f.Result.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogicalFilterTest_LengthGreaterThanOrEqualTo0AndSmallerThan11000_2_Result1()
        {
            var filter = new LogicalFilter<Friend, double>(new WeightFilterInstelling());
            var f = new FilterExecutor<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Setup(new List<IFilter>() { filter });


            f.Filter(Edit.And, new List<IResult>() { new LogischResult(filter, ">=0en<100", new Icon()) });

            Assert.AreEqual(4, f.Result.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogicalFilterTest_LengthGreaterThanOrEqualTo0OrSmallerThan11000_1_Result8()
        {
            var filter = new LogicalFilter<Friend, double>(new WeightFilterInstelling());
            var f = new FilterExecutor<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Setup(new List<IFilter>() { filter });


            f.Filter(Edit.And, new List<IResult>() { new LogischResult(filter, ">=0|<11000", new Icon()) });

            Assert.AreEqual(8, f.Result.Count);
        }
        [TestMethod()]
        public void FilterenTest_LogicalFilterTest_LengthGreaterThanOrEqualTo0OrSmallerThan11000_2_Result8()
        {
            var filter = new LogicalFilter<Friend, double>(new WeightFilterInstelling());
            var f = new FilterExecutor<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Setup(new List<IFilter>() { filter });


            f.Filter(Edit.And, new List<IResult>() { new LogischResult(filter, ">=0of<11000", new Icon()) });

            Assert.AreEqual(8, f.Result.Count);
        }

        [TestMethod()]
        public void FilterenTest_LogicalFilterTest_LengthGreaterThanOrEqualTo0OrSmallerThan11000WithShortcut_2_Result8()
        {
            var filter = new LogicalFilter<Friend, double>(new WeightFilterInstelling());
            var f = new FilterExecutor<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Setup(new List<IFilter>() { filter });


            f.Filter(Edit.And, new List<IResult>() { new LogischResult(filter, ">=0of<11000", new Icon()) });

            Assert.AreEqual(8, f.Result.Count);
        }

        [TestMethod()]
        public void FilterenTest_LogicalFilterTest_LengthGreaterThanOrEqual0AndSmallerThan150WithShortcut_2_Result6()
        {
            var filter = new LogicalFilter<Friend, double>(new WeightFilterInstelling());
            var f = new FilterExecutor<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Setup(new List<IFilter>() { filter });


            f.Filter(Edit.And, new List<IResult>() { new LogischResult(filter, ">=0en<150", new Icon()) });

            Assert.AreEqual(7, f.Result.Count);
        }

        [TestMethod()]
        public void FilterenTest_LogicalFilterTest_LengthGreaterThanOrEqual0AndSmallerThan150WithShortcut_MetSpaties()
        {
            var filter = new LogicalFilter<Friend, double>(new WeightFilterInstelling());
            var f = new FilterExecutor<Friend>();
            f.SetData(SeedFriends.GetSeed());

            f.Setup(new List<IFilter>() { filter });


            f.Filter(Edit.And, new List<IResult>() { new LogischResult(filter, ">= 0 en < 150", new Icon()) });

            Assert.AreEqual(7, f.Result.Count);
        }
    }

    public class WeightFilterInstelling : ILogicalFilterSettings<Friend, double>
    {
        public Func<Friend, double> PropertyFromDataset { get; set; }
        = p => p.Weight;
        public string Title { get; set; }
        = "Lengte filter";
        public string Shortcut { get; set; }
        = "B";
        public Icon Icon { get; set; }
        = new Icon();
    }

    public class BestFriendSetting : IBooleanSettings<Friend>
    {
        public Func<Friend, bool> PropertyFromDataset { get; set; } = p => p.IsBestFriend;
        public string Title { get; set; } = "Best friend";
        public string Shortcut { get; set; } = "BF";
        public Icon Icon { get; set; }
    }

    public class Testdatacompany : IMultipleChoiceSettings<Friend, Company>
    {
        public Func<Company, string> PropertyToFilterWith { get; set; } = p => p.Name;
        public Func<Friend, string> PropertyFromDataset { get; set; } = p => p.Company;
        public string Title { get; set; } = "Company";
        public string Shortcut { get; set; } = "C";
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