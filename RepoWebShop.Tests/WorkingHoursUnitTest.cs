using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;

namespace RepoWebShop.Tests
{
    [TestClass]
    public class WorkingHoursUnitTest
    {        
        public WorkingHoursUnitTest()
        {
        }

        [TestMethod]
        public void CalculatePickupDate()
        {
            var _openHours = new List<OpenHours>()
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
            var _processingHours = new List<ProcessingHours>()
            {
                new ProcessingHours() { DayId = 2, StartingAt = new TimeSpan(8, 0, 0), Duration = new TimeSpan(4, 0, 0) },
                new ProcessingHours() { DayId = 2, StartingAt = new TimeSpan(16, 0, 0), Duration = new TimeSpan(4, 0, 0) },
                new ProcessingHours() { DayId = 3, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) },
                new ProcessingHours() { DayId = 4, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) },
                new ProcessingHours() { DayId = 5, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) },
                new ProcessingHours() { DayId = 6, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) },
                new ProcessingHours() { DayId = 0, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(4, 0, 0) }
            };

            int estimationHs;
            DateTime orderAccreditted;
            DateTime finishingDate;
            DateTime orderPickup;

            estimationHs = 8;
            orderAccreditted = new DateTime(2017, 9, 3, 16, 0, 0); // Domingo 3 de septiembre a las 4pm
            finishingDate = WorkingHours.GetOrderReady(orderAccreditted, estimationHs, _processingHours, null, null, true);
            Assert.IsTrue(finishingDate.Year == 2017);
            Assert.IsTrue(finishingDate.Month == 9);
            Assert.IsTrue(finishingDate.Day == 5);
            Assert.IsTrue(finishingDate.Hour == 19);
            Assert.IsTrue(finishingDate.Minute == 45);
            orderPickup = WorkingHours.GetPickUpDate(orderAccreditted, estimationHs, _processingHours, _openHours, null, null);
            Assert.IsTrue(orderPickup.Year == 2017);
            Assert.IsTrue(orderPickup.Month == 9);
            Assert.IsTrue(orderPickup.Day == 5);
            Assert.IsTrue(orderPickup.Hour == 19);
            Assert.IsTrue(orderPickup.Minute == 45);

            estimationHs = 6;
            orderAccreditted = new DateTime(2017, 9, 5, 19, 50, 0); // Martes 5 de septiembre 2017
            finishingDate = WorkingHours.GetOrderReady(orderAccreditted, estimationHs, _processingHours, null, null, true);
            Assert.IsTrue(finishingDate.Year == 2017);
            Assert.IsTrue(finishingDate.Month == 9);
            Assert.IsTrue(finishingDate.Day == 6);
            Assert.IsTrue(finishingDate.Hour == 14);
            Assert.IsTrue(finishingDate.Minute == 15);
            orderPickup = WorkingHours.GetPickUpDate(orderAccreditted, estimationHs, _processingHours, _openHours, null, null);
            Assert.IsTrue(orderPickup.Year == 2017);
            Assert.IsTrue(orderPickup.Month == 9);
            Assert.IsTrue(orderPickup.Day == 6);
            Assert.IsTrue(orderPickup.Hour == 16);
            Assert.IsTrue(orderPickup.Minute == 0);

            estimationHs = 58;
            orderAccreditted = new DateTime(2017, 9, 5, 19, 50, 0); // Martes 5 de septiembre 2017
            finishingDate = WorkingHours.GetOrderReady(orderAccreditted, estimationHs, _processingHours, null, null, true);
            Assert.IsTrue(finishingDate.Year == 2017);
            Assert.IsTrue(finishingDate.Month == 9);
            Assert.IsTrue(finishingDate.Day == 12);
            Assert.IsTrue(finishingDate.Hour == 19);
            Assert.IsTrue(finishingDate.Minute == 45);
            orderPickup = WorkingHours.GetPickUpDate(orderAccreditted, estimationHs, _processingHours, _openHours, null, null);
            Assert.IsTrue(orderPickup.Year == 2017);
            Assert.IsTrue(orderPickup.Month == 9);
            Assert.IsTrue(orderPickup.Day == 12);
            Assert.IsTrue(orderPickup.Hour == 19);
            Assert.IsTrue(orderPickup.Minute == 45);
        }

        [TestMethod]
        public void CalculatePickupDateWithHolidays()
        {
            var _openHours = new List<OpenHours>() { };
            var _processingHours = new List<ProcessingHours>()
            {
                new ProcessingHours() { DayId = 6, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) },
                new ProcessingHours() { DayId = 0, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(4, 0, 0) }
            };

            int estimationHs;
            DateTime orderAccreditted;
            DateTime orderPickup;

            estimationHs = 10;
            orderAccreditted = new DateTime(2017, 10, 21, 16, 0, 0); // Sabado 4pm

            List<PublicHoliday> _publicHolidays = new List<PublicHoliday>()
            {
                new PublicHoliday() {
                    Date = new DateTime(2017, 10, 23), //Lunes
                    OpenHours = new OpenHours()
                    {
                        StartingAt = new TimeSpan(8, 0, 0),
                        Duration = new TimeSpan(4, 0, 0)
                    },
                    ProcessingHours = new ProcessingHours()
                    {
                        StartingAt = new TimeSpan(6, 0, 0),
                        Duration = new TimeSpan(4, 0, 0)
                    }
                }
            };

            orderPickup = WorkingHours.GetPickUpDate(orderAccreditted, estimationHs, _processingHours, _openHours, _publicHolidays, null);
            Assert.IsTrue(orderPickup.Day == 23);
            Assert.IsTrue(orderPickup.TimeOfDay.Hours == 8);
        }


        [TestMethod]
        public void CalculatePickupDateWithVacations()
        {
            var _openHours = new List<OpenHours>()
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
            var _processingHours = new List<ProcessingHours>()
            {
                new ProcessingHours() { DayId = 2, StartingAt = new TimeSpan(8, 0, 0), Duration = new TimeSpan(4, 0, 0) },
                new ProcessingHours() { DayId = 2, StartingAt = new TimeSpan(16, 0, 0), Duration = new TimeSpan(4, 0, 0) },
                new ProcessingHours() { DayId = 3, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) },
                new ProcessingHours() { DayId = 4, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) },
                new ProcessingHours() { DayId = 5, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) },
                new ProcessingHours() { DayId = 6, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(11, 30, 0) },
                new ProcessingHours() { DayId = 0, StartingAt = new TimeSpan(8, 30, 0), Duration = new TimeSpan(4, 0, 0) }
            };

            int estimationHs;
            DateTime orderAccreditted;
            DateTime orderPickup;

            List<Vacation> _vacations = new List<Vacation>()
            {
                new Vacation() {
                    StartDate = new DateTime(2018, 1, 10), //mierc
                    EndDate = new DateTime(2018, 1, 22) //lunes
                }
            };

            estimationHs = 8;
            orderAccreditted = new DateTime(2018, 1, 9, 16, 0, 0); // Domingo 3 de septiembre a las 4pm
            orderPickup = WorkingHours.GetPickUpDate(orderAccreditted, estimationHs, _processingHours, _openHours, null, _vacations);
            Assert.IsTrue(orderPickup.Year == 2018);
            Assert.IsTrue(orderPickup.Month == 1);
            Assert.IsTrue(orderPickup.Day == 23);
            Assert.IsTrue(orderPickup.Hour == 11);
            Assert.IsTrue(orderPickup.Minute == 45);

        }
    }
}
