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
        public void ApplyDiscountRepeatedDiscount()
        {
            var date = new DateTime(2018, 4, 7);

            var discount = new Discount
            {
                ValidFrom = new DateTime(2016, 11, 5),
                DurationDays = 1,
                InstancesLeft = null,
                Loop = 7,
                Percentage = 15,
                Phrase = "SABADO15REPO",
                Roof = 200,
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
                Loop = 7,
                Percentage = 15,
                Phrase = "VIERNES15REPO",
                Roof = 200,
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
                Loop = 7,
                Percentage = 15,
                Phrase = "VIERNES15REPO",
                Roof = 30,
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
                Loop = 7,
                Percentage = 15,
                Phrase = "VIERNES15REPO",
                Roof = 30,
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
                Loop = 7,
                Percentage = 15,
                Phrase = "VIERNES15REPO",
                Roof = 30,
                IsActive = true
            };
            string error = string.Empty;
            var result = Discount.ApplyDiscount(date, 500, discount, out error);

            Assert.AreEqual(result, -30m);
        }
    }
}
