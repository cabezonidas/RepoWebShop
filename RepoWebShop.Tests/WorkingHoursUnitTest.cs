using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepoWebShop.Tests
{
    [TestClass]
    public class WorkingHoursUnitTest
    {        
        public WorkingHoursUnitTest()
        {
        }
        //            var openSlots = WorkingHours.GetOpenSlots(pickUpTime, openHours, holidays, vacations);
        //var pickUpOptions = WorkingHours.GetCompatibleOpenSlots(openSlots, discount, LocalTime());
        //WorkingHours.ContainsDateFrame(openSlots, new KeyValuePair<DateTime, TimeSpan>(result.From, result.To));
        [TestMethod]
        public void ContainsDateFrame()
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

            var today = new DateTime(2018, 5, 17, 12, 11, 0); //Jueves Mediodia
            var pickUpTime = new DateTime(2018, 5, 17, 19, 45, 0); //

            /************* Get Pick Up Option *******************/
            var openSlots = WorkingHours.GetOpenSlots(pickUpTime, _openHours, new List<PublicHoliday>(), new List<Vacation>());
            var compatibleHours = WorkingHours.GetCompatibleOpenSlots(openSlots, null, today);
            /************* Get Pick Up Option *******************/

            var result = WorkingHours.ContainsDateFrame(compatibleHours, new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 17, 11, 15, 0), new TimeSpan(1, 15, 0)));
            Assert.IsFalse(result);
        }



        [TestMethod]
        public void CompatibleSlotsContainsLaterDateFrame()
        {
            var compatibleOpenSlots = new List<KeyValuePair<DateTime, TimeSpan>>()
            {
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 4, 29, 11, 0, 0), new TimeSpan(2, 0, 0)), //Domingo
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 4, 29, 9, 0, 0), new TimeSpan(4, 0, 0)), //Domingo
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 1, 8, 30, 0), new TimeSpan(4, 0, 0)), //Martes
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 1, 16, 0, 0), new TimeSpan(4, 0, 0)), //Martes
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 2, 8, 30, 0), new TimeSpan(4, 0, 0)), //Miercoles
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 2, 16, 0, 0), new TimeSpan(4, 0, 0)) //Miercoles
            };
            var selectedtimeframe = new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 4, 29, 9, 0, 0), new TimeSpan(4, 0, 0)); //Domingo, pero mas temprano
            var result = WorkingHours.ContainsDateFrame(compatibleOpenSlots, selectedtimeframe);

            Assert.IsTrue(result);
         }

        [TestMethod]
        public void CompatibleSlotsContainsDateFrame()
        {
            var compatibleOpenSlots = new List<KeyValuePair<DateTime, TimeSpan>>()
            {
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 4, 29, 9, 0, 0), new TimeSpan(4, 0, 0)), //Domingo
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 1, 8, 30, 0), new TimeSpan(4, 0, 0)), //Martes
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 1, 16, 0, 0), new TimeSpan(4, 0, 0)), //Martes
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 2, 8, 30, 0), new TimeSpan(4, 0, 0)), //Miercoles
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 2, 16, 0, 0), new TimeSpan(4, 0, 0)) //Miercoles
            };
            var selectedtimeframe = new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 4, 29, 9, 0, 0), new TimeSpan(4, 0, 0)); //Domingo, pero mas temprano
            var result = WorkingHours.ContainsDateFrame(compatibleOpenSlots, selectedtimeframe);

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void CompatibleSlotsDoesntContainsDateFrame()
        {
            var compatibleOpenSlots = new List<KeyValuePair<DateTime, TimeSpan>>()
            {
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 4, 29, 9, 0, 0), new TimeSpan(4, 0, 0)), //Domingo
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 1, 8, 30, 0), new TimeSpan(4, 0, 0)), //Martes
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 1, 16, 0, 0), new TimeSpan(4, 0, 0)), //Martes
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 2, 8, 30, 0), new TimeSpan(4, 0, 0)), //Miercoles
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 2, 16, 0, 0), new TimeSpan(4, 0, 0)) //Miercoles
            };
            var selectedtimeframe = new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 4, 29, 13, 1, 0), new TimeSpan(1, 0, 0)); //Domingo, pero fuera de horario de trabajo
            var result = WorkingHours.ContainsDateFrame(compatibleOpenSlots, selectedtimeframe);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetOpenSlots()
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

            List<PublicHoliday> _publicHolidays = new List<PublicHoliday>()
            {
                new PublicHoliday() {
                    Date = new DateTime(2018, 5, 1), //Martes
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

            List<Vacation> _vacations = new List<Vacation>()
            {
                new Vacation() {
                    StartDate = new DateTime(2018, 5, 3), //mierc
                    EndDate = new DateTime(2018, 5, 4) //lunes
                }
            };

            var today = new DateTime(2018, 4, 28, 18, 45, 0);

            var result = WorkingHours.GetOpenSlots(today, _openHours, _publicHolidays, _vacations);

            var asserts = result.ToArray();

            Assert.IsTrue(asserts[0].Key == new DateTime(2018, 4, 28, 18, 45, 0));
            Assert.IsTrue(asserts[0].Value == new TimeSpan(1, 15, 0));

            Assert.IsTrue(asserts[1].Key == new DateTime(2018, 4, 29, 8, 30, 0));
            Assert.IsTrue(asserts[1].Value == new TimeSpan(4, 30, 0));

            Assert.IsTrue(asserts[2].Key == new DateTime(2018, 5, 1, 8, 0, 0));
            Assert.IsTrue(asserts[2].Value == new TimeSpan(4, 0, 0));

            Assert.IsTrue(asserts[3].Key == new DateTime(2018, 5, 2, 8, 30, 0));
            Assert.IsTrue(asserts[3].Value == new TimeSpan(4, 0, 0));

            Assert.IsTrue(asserts[4].Key == new DateTime(2018, 5, 2, 16, 0, 0));
            Assert.IsTrue(asserts[4].Value == new TimeSpan(4, 0, 0));
            
            Assert.IsTrue(asserts[5].Key == new DateTime(2018, 5, 5, 8, 30, 0));
            Assert.IsTrue(asserts[5].Value == new TimeSpan(11, 30, 0));
        }

        [TestMethod]
        public void CalculateCompatibleDaysDiscountSameDayButFirstDeliverableDate()
        {
            var openSlots = new List<KeyValuePair<DateTime, TimeSpan>>()
            {
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 4, 29, 9, 0, 0), new TimeSpan(4, 0, 0)), //Domingo
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 1, 8, 30, 0), new TimeSpan(4, 0, 0)), //Martes
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 1, 16, 0, 0), new TimeSpan(4, 0, 0)), //Martes
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 2, 8, 30, 0), new TimeSpan(4, 0, 0)), //Miercoles
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 2, 16, 0, 0), new TimeSpan(4, 0, 0)) //Miercoles
            };
            var discount = new Discount
            {
                ValidFrom = new DateTime(2016, 11, 5), //Sabado
                DurationDays = 1,
                InstancesLeft = null,
                Loop = 7,
                Percentage = 15,
                Phrase = "SABADO15REPO",
                Roof = 200,
                IsActive = true
            };
            var today = new DateTime(2018, 4, 28); //Sabado

            var result = WorkingHours.GetCompatibleOpenSlots(openSlots, discount, today);

            Assert.AreEqual(result.Count(), 1);
            Assert.AreEqual(result.First(), new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 4, 29, 9, 0, 0), new TimeSpan(4, 0, 0))); //El desc es del sabado, y se usa un sabado, pero la primera hora disponible de entrega es domingo,new DateTime(2018, 4, 29, 9, 0, 0), new TimeSpan(4, 0, 0)
        }

        [TestMethod]
        public void CalculateCompatibleDays()
        {
            var openSlots = new List<KeyValuePair<DateTime, TimeSpan>>()
            {
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 4, 28, 18, 45, 0), new TimeSpan(1, 15, 0)), //Sabado
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 4, 29, 9, 0, 0), new TimeSpan(4, 0, 0)), //Domingo
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 1, 8, 30, 0), new TimeSpan(4, 0, 0)), //Martes
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 1, 16, 0, 0), new TimeSpan(4, 0, 0)), //Martes
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 2, 8, 30, 0), new TimeSpan(4, 0, 0)), //Miercoles
                new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 5, 2, 16, 0, 0), new TimeSpan(4, 0, 0)) //Miercoles
            };
            var discount = new Discount
            {
                ValidFrom = new DateTime(2016, 11, 5), //Sabado
                DurationDays = 1,
                InstancesLeft = null,
                Loop = 7,
                Percentage = 15,
                Phrase = "SABADO15REPO",
                Roof = 200,
                IsActive = true
            };
            var today = new DateTime(2018, 4, 28); //Sabado

            var result = WorkingHours.GetCompatibleOpenSlots(openSlots, discount, today);

            Assert.AreEqual(result.Count(), 1);
            Assert.AreEqual(result.First(), new KeyValuePair<DateTime, TimeSpan>(new DateTime(2018, 4, 28, 18, 45, 0), new TimeSpan(1, 15, 0)));
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
