using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filter.Filter_Berekenaar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter.Filter_Berekenaar.Tests
{
    [TestClass()]
    public class LogischBerekenenTests
    {
        [TestMethod()]
        public void BerekenLogicaTest_GroterDan15000()
        {
            LogischBerekenen model = new LogischBerekenen();

            model.BerekenLogica(">15000");

            Assert.AreEqual(2, model.Logica.Count);
            Assert.AreEqual(LogischeOperator.GroterDan, model.Logica[0].Operator);
            Assert.AreEqual(0, model.Logica[0].Waarde);
            Assert.AreEqual(LogischeOperator.Waarde, model.Logica[1].Operator);
            Assert.AreEqual(15000d, model.Logica[1].Waarde);
            Assert.AreEqual(true, model.IsSuccessVol);
        }
        [TestMethod()]
        public void BerekenLogicaTest_GroterDan15000_2()
        {
            LogischBerekenen model = new LogischBerekenen();

            model.BerekenLogica("> 15000 ");

            Assert.AreEqual(2, model.Logica.Count);
            Assert.AreEqual(LogischeOperator.GroterDan, model.Logica[0].Operator);
            Assert.AreEqual(0, model.Logica[0].Waarde);
            Assert.AreEqual(LogischeOperator.Waarde, model.Logica[1].Operator);
            Assert.AreEqual(15000d, model.Logica[1].Waarde);
        }
        [TestMethod()]
        public void BerekenLogicaTest_GroterDan15000_3()
        {
            LogischBerekenen model = new LogischBerekenen();

            model.BerekenLogica(">      15000 ");

            Assert.AreEqual(2, model.Logica.Count);
            Assert.AreEqual(LogischeOperator.GroterDan, model.Logica[0].Operator);
            Assert.AreEqual(0, model.Logica[0].Waarde);
            Assert.AreEqual(LogischeOperator.Waarde, model.Logica[1].Operator);
            Assert.AreEqual(15000d, model.Logica[1].Waarde);
            Assert.AreEqual(true, model.IsSuccessVol);
        }
        [TestMethod()]
        public void BerekenLogicaTest_KleinerDan15000()
        {
            LogischBerekenen model = new LogischBerekenen();

            model.BerekenLogica("<15000");

            Assert.AreEqual(2, model.Logica.Count);
            Assert.AreEqual(LogischeOperator.KleinerDan, model.Logica[0].Operator);
            Assert.AreEqual(0, model.Logica[0].Waarde);
            Assert.AreEqual(LogischeOperator.Waarde, model.Logica[1].Operator);
            Assert.AreEqual(15000d, model.Logica[1].Waarde);
            Assert.AreEqual(true, model.IsSuccessVol);
        }
        [TestMethod()]
        public void BerekenLogicaTest_GroterDanOfGelijkAan15000()
        {
            LogischBerekenen model = new LogischBerekenen();

            model.BerekenLogica(">=15000");

            Assert.AreEqual(2, model.Logica.Count);
            Assert.AreEqual(LogischeOperator.GroterOfGelijkAan, model.Logica[0].Operator);
            Assert.AreEqual(0, model.Logica[0].Waarde);
            Assert.AreEqual(LogischeOperator.Waarde, model.Logica[1].Operator);
            Assert.AreEqual(15000d, model.Logica[1].Waarde);
            Assert.AreEqual(true, model.IsSuccessVol);
        }
        [TestMethod()]
        public void BerekenLogicaTest_KleinerDanOfGelijkAan15000()
        {
            LogischBerekenen model = new LogischBerekenen();

            model.BerekenLogica("<=15000");

            Assert.AreEqual(2, model.Logica.Count);
            Assert.AreEqual(LogischeOperator.KleinerOfGelijkAan, model.Logica[0].Operator);
            Assert.AreEqual(0, model.Logica[0].Waarde);
            Assert.AreEqual(LogischeOperator.Waarde, model.Logica[1].Operator);
            Assert.AreEqual(15000, model.Logica[1].Waarde);
            Assert.AreEqual(true, model.IsSuccessVol);
        }
        [TestMethod()]
        public void BerekenLogicaTest_GelijkAan15000()
        {
            LogischBerekenen model = new LogischBerekenen();

            model.BerekenLogica("=15000");

            Assert.AreEqual(2, model.Logica.Count);
            Assert.AreEqual(LogischeOperator.GelijkAan, model.Logica[0].Operator);
            Assert.AreEqual(0, model.Logica[0].Waarde);
            Assert.AreEqual(LogischeOperator.Waarde, model.Logica[1].Operator);
            Assert.AreEqual(15000d, model.Logica[1].Waarde);
            Assert.AreEqual(true, model.IsSuccessVol);
        }
        [TestMethod()]
        public void BerekenLogicaTest_NietGelijkAan15000()
        {
            LogischBerekenen model = new LogischBerekenen();

            model.BerekenLogica("!=15000");

            Assert.AreEqual(2, model.Logica.Count);
            Assert.AreEqual(LogischeOperator.NietGelijkAan, model.Logica[0].Operator);
            Assert.AreEqual(0, model.Logica[0].Waarde);
            Assert.AreEqual(LogischeOperator.Waarde, model.Logica[1].Operator);
            Assert.AreEqual(15000d, model.Logica[1].Waarde);
            Assert.AreEqual(true, model.IsSuccessVol);
        }
        [TestMethod()]
        public void BerekenLogicaTest_NietGelijkAan15000_2()
        {
            LogischBerekenen model = new LogischBerekenen();

            model.BerekenLogica("<>15000");

            Assert.AreEqual(2, model.Logica.Count);
            Assert.AreEqual(LogischeOperator.NietGelijkAan, model.Logica[0].Operator);
            Assert.AreEqual(0, model.Logica[0].Waarde);
            Assert.AreEqual(LogischeOperator.Waarde, model.Logica[1].Operator);
            Assert.AreEqual(15000d, model.Logica[1].Waarde);
            Assert.AreEqual(true, model.IsSuccessVol);
        }
        [TestMethod()]
        public void BerekenLogicaTest_HeleLijn()
        {
            LogischBerekenen model = new LogischBerekenen();

            model.BerekenLogica(">1500&<3000");

            Assert.AreEqual(LogischeOperator.GroterDan, model.Logica[0].Operator);
            Assert.AreEqual(1500d, model.Logica[1].Waarde);
            Assert.AreEqual(LogischeOperator.En, model.Logica[2].Operator);
            Assert.AreEqual(LogischeOperator.KleinerDan, model.Logica[3].Operator);
            Assert.AreEqual(3000d, model.Logica[4].Waarde);
            Assert.AreEqual(5, model.Logica.Count);
            Assert.AreEqual(true, model.IsSuccessVol);
        }

        [TestMethod()]
        public void TestLogischeOpbouwing_StartenMetEn_UnSuccessfull()
        {
            LogischBerekenen model = new LogischBerekenen();

            model.BerekenLogica("En>1500&<3000");

            Assert.AreEqual(false, model.IsSuccessVol);
        }

        [TestMethod()]
        public void TestLogischeOpbouwing_EindigenMetEn_UnSuccessfull()
        {
            LogischBerekenen model = new LogischBerekenen();

            model.BerekenLogica(">1500&<3000En");

            Assert.AreEqual(false, model.IsSuccessVol);
        }
        [TestMethod()]
        public void TestLogischeOpbouwing_2operators_UnSuccessfull()
        {
            LogischBerekenen model = new LogischBerekenen();

            model.BerekenLogica(">>1500&<3000En");

            Assert.AreEqual(false, model.IsSuccessVol);
        }
    }
}