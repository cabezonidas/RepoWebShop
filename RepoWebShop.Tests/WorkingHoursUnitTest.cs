using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using RepoWebShop.Helpers;

namespace RepoWebShop.Tests
{
    [TestClass]
    public class WorkingHoursUnitTest
    {
        private readonly List<OpenHours> _openHours;
        private readonly List<ProcessingHours> _processingHours;
        
        public WorkingHoursUnitTest()
        {
            _openHours = new List<OpenHours>()
            {
                new OpenHours() { DayId = 2, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(4, 0, 0) },
                new OpenHours() { DayId = 2, StartingAt = new TimeSpan(16, 00, 0), Duration = new TimeSpan(4, 0, 0) },
                new OpenHours() { DayId = 3, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(4, 0, 0) },
                new OpenHours() { DayId = 3, StartingAt = new TimeSpan(16, 00, 0), Duration = new TimeSpan(4, 0, 0) },
                new OpenHours() { DayId = 4, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(4, 0, 0) },
                new OpenHours() { DayId = 4, StartingAt = new TimeSpan(16, 00, 0), Duration = new TimeSpan(4, 0, 0) },
                new OpenHours() { DayId = 5, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(4, 0, 0) },
                new OpenHours() { DayId = 5, StartingAt = new TimeSpan(16, 00, 0), Duration = new TimeSpan(4, 0, 0) },
                new OpenHours() { DayId = 6, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) },
                new OpenHours() { DayId = 0, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(4, 30, 0) }
            };
            _processingHours = new List<ProcessingHours>()
            {
                new ProcessingHours() { DayId = 2, StartingAt = new TimeSpan(8, 0, 0), Duration = new TimeSpan(4, 0, 0) },
                new ProcessingHours() { DayId = 2, StartingAt = new TimeSpan(16, 0, 0), Duration = new TimeSpan(4, 0, 0) },
                new ProcessingHours() { DayId = 3, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) },
                new ProcessingHours() { DayId = 4, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) },
                new ProcessingHours() { DayId = 5, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) },
                new ProcessingHours() { DayId = 6, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) },
                new ProcessingHours() { DayId = 0, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(4, 0, 0) }
            };
        }

        [TestMethod]
        public void CalculatePickupDate()
        {
            int estimationHs;
            DateTime orderAccreditted;
            DateTime finishingDate;
            DateTime orderPickup;

            estimationHs = 8;
            orderAccreditted = new DateTime(2017, 9, 3, 16, 0, 0); // Domingo 3 de septiembre a las 4pm
            finishingDate = WorkingHours.GetOrderReady(orderAccreditted, estimationHs, _processingHours);
            Assert.IsTrue(finishingDate.Year == 2017);
            Assert.IsTrue(finishingDate.Month == 9);
            Assert.IsTrue(finishingDate.Day == 5);
            Assert.IsTrue(finishingDate.Hour == 19);
            Assert.IsTrue(finishingDate.Minute == 59);
            orderPickup = WorkingHours.GetPickUpDate(orderAccreditted, estimationHs, _processingHours, _openHours);
            Assert.IsTrue(orderPickup.Year == 2017);
            Assert.IsTrue(orderPickup.Month == 9);
            Assert.IsTrue(orderPickup.Day == 5);
            Assert.IsTrue(orderPickup.Hour == 19);
            Assert.IsTrue(orderPickup.Minute == 59);

            estimationHs = 6;
            orderAccreditted = new DateTime(2017, 9, 5, 19, 50, 0); // Martes 5 de septiembre 2017
            finishingDate = WorkingHours.GetOrderReady(orderAccreditted, estimationHs, _processingHours);
            Assert.IsTrue(finishingDate.Year == 2017);
            Assert.IsTrue(finishingDate.Month == 9);
            Assert.IsTrue(finishingDate.Day == 6);
            Assert.IsTrue(finishingDate.Hour == 14);
            Assert.IsTrue(finishingDate.Minute == 19);
            orderPickup = WorkingHours.GetPickUpDate(orderAccreditted, estimationHs, _processingHours, _openHours);
            Assert.IsTrue(orderPickup.Year == 2017);
            Assert.IsTrue(orderPickup.Month == 9);
            Assert.IsTrue(orderPickup.Day == 6);
            Assert.IsTrue(orderPickup.Hour == 16);
            Assert.IsTrue(orderPickup.Minute == 0);

            estimationHs = 58;
            orderAccreditted = new DateTime(2017, 9, 5, 19, 50, 0); // Martes 5 de septiembre 2017
            finishingDate = WorkingHours.GetOrderReady(orderAccreditted, estimationHs, _processingHours);
            Assert.IsTrue(finishingDate.Year == 2017);
            Assert.IsTrue(finishingDate.Month == 9);
            Assert.IsTrue(finishingDate.Day == 12);
            Assert.IsTrue(finishingDate.Hour == 19);
            Assert.IsTrue(finishingDate.Minute == 49);
            orderPickup = WorkingHours.GetPickUpDate(orderAccreditted, estimationHs, _processingHours, _openHours);
            Assert.IsTrue(orderPickup.Year == 2017);
            Assert.IsTrue(orderPickup.Month == 9);
            Assert.IsTrue(orderPickup.Day == 12);
            Assert.IsTrue(orderPickup.Hour == 19);
            Assert.IsTrue(orderPickup.Minute == 49);
        }
    }
}
