using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filter.Filter_Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filter.Filter_Calculator;

namespace FilterTests.Filter_Calculator
{
    [TestClass()]
    public class LogicalCalculationTests
    {
        [TestMethod()]
        public void CalculateLogicTest_GreaterThan15000()
        {
            LogicalCalculator model = new LogicalCalculator();

            model.Calculate(">15000");

            Assert.AreEqual(2, model.Logic.Count);
            Assert.AreEqual(LogicalOperator.GreaterThan, model.Logic[0].Operator);
            Assert.AreEqual(0, model.Logic[0].value);
            Assert.AreEqual(LogicalOperator.Value, model.Logic[1].Operator);
            Assert.AreEqual(15000, model.Logic[1].value);
            Assert.AreEqual(true, model.IsSuccess);
        }
        [TestMethod()]
        public void CalculateLogicTest_GreaterThan15000_2()
        {
            LogicalCalculator model = new LogicalCalculator();

            model.Calculate("> 15000 ");

            Assert.AreEqual(2, model.Logic.Count);
            Assert.AreEqual(LogicalOperator.GreaterThan, model.Logic[0].Operator);
            Assert.AreEqual(0, model.Logic[0].value);
            Assert.AreEqual(LogicalOperator.Value, model.Logic[1].Operator);
            Assert.AreEqual(15000, model.Logic[1].value);
        }
        [TestMethod()]
        public void CalculateLogicTest_GreaterThan15000_3()
        {
            LogicalCalculator model = new LogicalCalculator();

            model.Calculate(">      15000 ");

            Assert.AreEqual(2, model.Logic.Count);
            Assert.AreEqual(LogicalOperator.GreaterThan, model.Logic[0].Operator);
            Assert.AreEqual(0, model.Logic[0].value);
            Assert.AreEqual(LogicalOperator.Value, model.Logic[1].Operator);
            Assert.AreEqual(15000, model.Logic[1].value);
            Assert.AreEqual(true, model.IsSuccess);
        }
        [TestMethod()]
        public void CalculateLogicTest_SmallerThan15000()
        {
            LogicalCalculator model = new LogicalCalculator();

            model.Calculate("<15000");

            Assert.AreEqual(2, model.Logic.Count);
            Assert.AreEqual(LogicalOperator.SmallerThan, model.Logic[0].Operator);
            Assert.AreEqual(0, model.Logic[0].value);
            Assert.AreEqual(LogicalOperator.Value, model.Logic[1].Operator);
            Assert.AreEqual(15000, model.Logic[1].value);
            Assert.AreEqual(true, model.IsSuccess);
        }
        [TestMethod()]
        public void CalculateLogicTest_GreaterThanOfEqualTo15000()
        {
            LogicalCalculator model = new LogicalCalculator();

            model.Calculate(">=15000");

            Assert.AreEqual(2, model.Logic.Count);
            Assert.AreEqual(LogicalOperator.GreaterOrEqualThan, model.Logic[0].Operator);
            Assert.AreEqual(0, model.Logic[0].value);
            Assert.AreEqual(LogicalOperator.Value, model.Logic[1].Operator);
            Assert.AreEqual(15000, model.Logic[1].value);
            Assert.AreEqual(true, model.IsSuccess);
        }
        [TestMethod()]
        public void CalculateLogicTest_SmallerThanOfEqualTo15000()
        {
            LogicalCalculator model = new LogicalCalculator();

            model.Calculate("<=15000");

            Assert.AreEqual(2, model.Logic.Count);
            Assert.AreEqual(LogicalOperator.SmallerOrEqualThan, model.Logic[0].Operator);
            Assert.AreEqual(0, model.Logic[0].value);
            Assert.AreEqual(LogicalOperator.Value, model.Logic[1].Operator);
            Assert.AreEqual(15000, model.Logic[1].value);
            Assert.AreEqual(true, model.IsSuccess);
        }
        [TestMethod()]
        public void CalculateLogicTest_EqualTo15000()
        {
            LogicalCalculator model = new LogicalCalculator();

            model.Calculate("=15000");

            Assert.AreEqual(2, model.Logic.Count);
            Assert.AreEqual(LogicalOperator.Equal, model.Logic[0].Operator);
            Assert.AreEqual(0, model.Logic[0].value);
            Assert.AreEqual(LogicalOperator.Value, model.Logic[1].Operator);
            Assert.AreEqual(15000, model.Logic[1].value);
            Assert.AreEqual(true, model.IsSuccess);
        }
        [TestMethod()]
        public void CalculateLogicTest_EqualTo15000_WithoutEqual()
        {
            LogicalCalculator model = new LogicalCalculator();

            model.Calculate("15000");

            Assert.AreEqual(2, model.Logic.Count);
            Assert.AreEqual(LogicalOperator.Equal, model.Logic[0].Operator);
            Assert.AreEqual(0, model.Logic[0].value);
            Assert.AreEqual(LogicalOperator.Value, model.Logic[1].Operator);
            Assert.AreEqual(15000, model.Logic[1].value);
            Assert.AreEqual(true, model.IsSuccess);
        }

        [TestMethod()]
        public void CalculateLogicTest_NotEqualTo15000()
        {
            LogicalCalculator model = new LogicalCalculator();

            model.Calculate("!=15000");

            Assert.AreEqual(2, model.Logic.Count);
            Assert.AreEqual(LogicalOperator.NotEqual, model.Logic[0].Operator);
            Assert.AreEqual(0, model.Logic[0].value);
            Assert.AreEqual(LogicalOperator.Value, model.Logic[1].Operator);
            Assert.AreEqual(15000, model.Logic[1].value);
            Assert.AreEqual(true, model.IsSuccess);
        }
        [TestMethod()]
        public void CalculateLogicTest_NotEqualTo15000_2()
        {
            LogicalCalculator model = new LogicalCalculator();

            model.Calculate("<>15000");

            Assert.AreEqual(2, model.Logic.Count);
            Assert.AreEqual(LogicalOperator.NotEqual, model.Logic[0].Operator);
            Assert.AreEqual(0, model.Logic[0].value);
            Assert.AreEqual(LogicalOperator.Value, model.Logic[1].Operator);
            Assert.AreEqual(15000, model.Logic[1].value);
            Assert.AreEqual(true, model.IsSuccess);
        }
        [TestMethod()]
        public void CalculateLogicTest_WholeLine()
        {
            LogicalCalculator model = new LogicalCalculator();

            model.Calculate(">1500&<3000");

            Assert.AreEqual(LogicalOperator.GreaterThan, model.Logic[0].Operator);
            Assert.AreEqual(1500, model.Logic[1].value);
            Assert.AreEqual(LogicalOperator.And, model.Logic[2].Operator);
            Assert.AreEqual(LogicalOperator.SmallerThan, model.Logic[3].Operator);
            Assert.AreEqual(3000, model.Logic[4].value);
            Assert.AreEqual(5, model.Logic.Count);
            Assert.AreEqual(true, model.IsSuccess);
        }

        [TestMethod()]
        public void TestLogicalBuild_StartWithAnd_UnSuccessfull()
        {
            LogicalCalculator model = new LogicalCalculator();

            model.Calculate("En>1500&<3000");

            Assert.AreEqual(false, model.IsSuccess);
        }

        [TestMethod()]
        public void TestLogicalBuild_StopWithEn_UnSuccessfull()
        {
            LogicalCalculator model = new LogicalCalculator();

            model.Calculate(">1500&<3000En");

            Assert.AreEqual(false, model.IsSuccess);
        }
        [TestMethod()]
        public void TestLogicalBuild_2operators_UnSuccessfull()
        {
            LogicalCalculator model = new LogicalCalculator();

            model.Calculate(">>1500&<3000En");

            Assert.AreEqual(false, model.IsSuccess);
        }
    }
}