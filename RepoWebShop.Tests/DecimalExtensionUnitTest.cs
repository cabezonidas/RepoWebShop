using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RepoWebShop.Extensions;
using System.Collections.Generic;
using System.Text;

namespace RepoWebShop.Tests
{
    [TestClass]
    public class DecimalExtensionUnitTest
    {
        public DecimalExtensionUnitTest()
        {
        }

        [TestMethod]
        public void ApplyPercentage()
        {
            Assert.AreEqual(10m.ApplyPercentage(10), 11m);
            Assert.AreEqual(20m.ApplyPercentage(15), 23m);
        }

        [TestMethod]
        public void RoundTo5()
        {
            Assert.AreEqual(10m.RoundTo(5), 10m);
            Assert.AreEqual(11m.RoundTo(5), 10m);
            Assert.AreEqual(12m.RoundTo(5), 10m);
            Assert.AreEqual(13m.RoundTo(5), 15m);
            Assert.AreEqual(14m.RoundTo(5), 15m);
            Assert.AreEqual(15m.RoundTo(5), 15m);
            Assert.AreEqual(16m.RoundTo(5), 15m);
            Assert.AreEqual(17m.RoundTo(5), 15m);
            Assert.AreEqual(18m.RoundTo(5), 20m);
            Assert.AreEqual(19m.RoundTo(5), 20m);
        }

        [TestMethod]
        public void RoundTo7()
        {
            Assert.AreEqual(10m.RoundTo(7), 7m);
            Assert.AreEqual(11m.RoundTo(7), 14m);
            Assert.AreEqual(12m.RoundTo(7), 14m);
            Assert.AreEqual(13m.RoundTo(7), 14m);
            Assert.AreEqual(14m.RoundTo(7), 14m);
            Assert.AreEqual(15m.RoundTo(7), 14m);
            Assert.AreEqual(16m.RoundTo(7), 14m);
            Assert.AreEqual(17m.RoundTo(7), 14m);
            Assert.AreEqual(18m.RoundTo(7), 21m);
            Assert.AreEqual(19m.RoundTo(7), 21m);
        }
    }
}
