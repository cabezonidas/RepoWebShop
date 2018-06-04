using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoWebShop.Repositories;
using RepoWebShop.Interfaces;
using System;
using RepoWebShop.Models;

namespace RepoWebShop.Tests
{
    [TestClass]
    public class DiscountUnitTest
    {
        public DiscountUnitTest()
        {
        }

        [TestMethod]
        public void ApplyDiscountWithBase()
        {
            var date = new DateTime(2018, 4, 7);

            var discount = new Discount
            {
                ValidFrom = new DateTime(2016, 11, 5),
                DurationDays = 1,
                InstancesLeft = null,
                Weekly = true,
                Percentage = 100,
                Phrase = "SABADO15REPO",
                Roof = 150,
                Base = 900,
                IsActive = true
            };
            string error = string.Empty;
            var result1 = Discount.ApplyDiscount(date, 899, discount, out error);
            var result2 = Discount.ApplyDiscount(date, 900, discount, out error);
            var result3 = Discount.ApplyDiscount(date, 1200, discount, out error);
            var result4 = Discount.ApplyDiscount(date, 400, discount, out error);

            Assert.AreEqual(result1, 0m);
            Assert.AreEqual(result2, -150m);
            Assert.AreEqual(result3, -150m);
            Assert.AreEqual(result4, 0m);
        }

        [TestMethod]
        public void ApplyDiscountVoucherBiggerThanValue()
        {
            var date = new DateTime(2018, 4, 7);

            var discount = new Discount
            {
                ValidFrom = new DateTime(2016, 11, 5),
                DurationDays = 1,
                InstancesLeft = null,
                Weekly = true,
                Percentage = 100,
                Phrase = "SABADO15REPO",
                Roof = 150,
                Base = 100,
                IsActive = true
            };
            string error = string.Empty;
            var result1 = Discount.ApplyDiscount(date, 200, discount, out error);
            var result2 = Discount.ApplyDiscount(date, 150, discount, out error);
            var result3 = Discount.ApplyDiscount(date, 100, discount, out error);
            var result4 = Discount.ApplyDiscount(date, 0, discount, out error);

            Assert.AreEqual(result1, -150m);
            Assert.AreEqual(result2, -150m);
            Assert.AreEqual(result3, -100m);
            Assert.AreEqual(result4, 0m);
        }

        [TestMethod]
        public void ApplyDiscountRepeatedDiscount()
        {
            var date = new DateTime(2018, 4, 7);

            var discount = new Discount
            {
                ValidFrom = new DateTime(2016, 11, 5),
                DurationDays = 1,
                InstancesLeft = null,
                Weekly = true,
                Percentage = 15,
                Phrase = "SABADO15REPO",
                Roof = 200,
                Base = 1,
                IsActive = true
            };
            string error = string.Empty;
            var result = Discount.ApplyDiscount(date, 500, discount, out error);

            Assert.AreEqual(result, -500 * 0.15m);
        }

        [TestMethod]
        public void ApplyDiscountRepeatedDiscountWrongDay()
        {
            var date = new DateTime(2018, 4, 7); //Saturday

            var discount = new Discount
            {
                ValidFrom = new DateTime(2016, 11, 4), //Friday
                DurationDays = 1,
                InstancesLeft = null,
                Weekly = true,
                Percentage = 15,
                Phrase = "VIERNES15REPO",
                Roof = 200,
                Base = 1,
                IsActive = true
            };
            string error = string.Empty;
            var result = Discount.ApplyDiscount(date, 500, discount, out error);

            Assert.AreEqual(result, 0m);
        }

        [TestMethod]
        public void ApplyDiscountRepeatedDiscountRoof()
        {
            var date = new DateTime(2018, 4, 7); //Saturday

            var discount = new Discount
            {
                ValidFrom = new DateTime(2016, 11, 4), //Friday
                DurationDays = 2, //2 days
                InstancesLeft = null,
                Weekly = true,
                Percentage = 15,
                Phrase = "VIERNES15REPO",
                Roof = 30,
                Base = 1,
                IsActive = true
            };
            string error = string.Empty;
            var result = Discount.ApplyDiscount(date, 500, discount, out error);

            Assert.AreEqual(result, -30m);
        }

        [TestMethod]
        public void ApplyDiscountRepeatedDiscountNoInstanceLeft()
        {
            var date = new DateTime(2018, 4, 7); //Saturday

            var discount = new Discount
            {
                ValidFrom = new DateTime(2016, 11, 4), //Friday
                DurationDays = 2, //2 days
                InstancesLeft = 0,
                Weekly = true,
                Percentage = 15,
                Phrase = "VIERNES15REPO",
                Roof = 30,
                Base = 1,
                IsActive = true
            };
            string error = string.Empty;
            var result = Discount.ApplyDiscount(date, 500, discount, out error);

            Assert.AreEqual(result, 0m);
        }

        [TestMethod]
        public void ApplyDiscountRepeatedDiscountOneInstanceLeft()
        {
            var date = new DateTime(2018, 4, 7); //Saturday

            var discount = new Discount
            {
                ValidFrom = new DateTime(2016, 11, 4), //Friday
                DurationDays = 2, //2 days
                InstancesLeft = 1,
                Weekly = true,
                Percentage = 15,
                Phrase = "VIERNES15REPO",
                Roof = 30,
                Base = 1,
                IsActive = true
            };
            string error = string.Empty;
            var result = Discount.ApplyDiscount(date, 500, discount, out error);

            Assert.AreEqual(result, -30m);
        }
    }
}
