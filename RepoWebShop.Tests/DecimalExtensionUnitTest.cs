using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RepoWebShop.Extensions;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

namespace RepoWebShop.Tests
{
    [TestClass]
    public class DecimalExtensionUnitTest
    {
        public DecimalExtensionUnitTest()
        {
        }

        [TestMethod]
        public void Diatricts()
        {
			var text = "Hólá, Hono lulú, !¡ - é í";
			var result = text.RemoveAccents();

			Assert.AreEqual(result, "Hola, Hono lulu, !¡ - e i");
		}

        [TestMethod]
        public void ApplyPercentage()
        {
            Assert.AreEqual(10m.ApplyPercentage(10), 11m);
            Assert.AreEqual(20m.ApplyPercentage(15), 23m);
        }

        [TestMethod]
        public void Pagination()
		{
			var test0 = ((uint)2530).Paginate(0);
			var test1 = ((uint)2530).Paginate(1);
			var test2 = ((uint)2530).Paginate(100);
			var test3 = ((uint)2530).Paginate(1000);
			var test4 = ((uint)2530).Paginate(10000);

			Assert.AreEqual(test0.Count(), 2530);
			Assert.AreEqual(test1.Count(), 2530);
			Assert.AreEqual(test2.Count(), 26);
			Assert.AreEqual(test3.Count(), 3);
			Assert.AreEqual(test4.Count(), 1);

			Assert.AreEqual(test0.Select(x => (int)x.Value).Sum(), 2530);
			Assert.AreEqual(test1.Select(x => (int)x.Value).Sum(), 2530);
			Assert.AreEqual(test2.Select(x => (int)x.Value).Sum(), 2530);
			Assert.AreEqual(test3.Select(x => (int)x.Value).Sum(), 2530);
			Assert.AreEqual(test4.Select(x => (int)x.Value).Sum(), 2530);
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
