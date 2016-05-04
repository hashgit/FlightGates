using System;
using System.Collections.Generic;
using FlightGates.DataStore;
using FlightGates.DataStore.Entities;
using FlightGates.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FlightGates.Tests.ServicesTest
{
    [TestClass]
    public class ScheduleServiceTest
    {
        [TestMethod]
        public void CanCancelFlights()
        {
            var gateRepositoryMoq = new Mock<IGatesRepository>();
            var flightsRepositoryMoq = new Mock<IFlightsRepository>();

            var gate = new Gate { Flights = new List<Flight>() };
            var flight = new Flight { Gate = gate };
            gate.Flights.Add(flight);

            flightsRepositoryMoq.Setup(f => f.GetById(1)).Returns(flight);

            var service = new ScheduleService(gateRepositoryMoq.Object, flightsRepositoryMoq.Object);
            service.CancelFlight(1);

            flightsRepositoryMoq.Verify(f => f.GetById(1));
            flightsRepositoryMoq.Verify(f => f.Delete(flight));
        }

        [TestMethod]
        public void CanScheduleFlight()
        {
            var gateRepositoryMoq = new Mock<IGatesRepository>();
            var flightsRepositoryMoq = new Mock<IFlightsRepository>();

            var gate = new Gate { Flights = new List<Flight>() };
            gate.Flights.Add(new Flight { ArrivalDateTime = DateTime.Today.AddHours(1), DepartureDateTime = DateTime.Today.AddHours(1).AddMinutes(29) });
            gate.Flights.Add(new Flight { ArrivalDateTime = DateTime.Today.AddHours(1).AddMinutes(30), DepartureDateTime = DateTime.Today.AddHours(1).AddMinutes(59) });

            gateRepositoryMoq.Setup(g => g.GetById(1)).Returns(gate);
            
            var service = new ScheduleService(gateRepositoryMoq.Object, flightsRepositoryMoq.Object);

            var result = service.ScheduleFlight(1, DateTime.Today.AddHours(2), DateTime.Today.AddHours(2).AddMinutes(30) );
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanNotScheduleFlightIfArrivalTimeOverlaps()
        {
            var gateRepositoryMoq = new Mock<IGatesRepository>();
            var flightsRepositoryMoq = new Mock<IFlightsRepository>();

            var gate = new Gate { Flights = new List<Flight>() };
            gate.Flights.Add(new Flight { ArrivalDateTime = DateTime.Today.AddHours(1), DepartureDateTime = DateTime.Today.AddHours(1).AddMinutes(29) });
            gate.Flights.Add(new Flight { ArrivalDateTime = DateTime.Today.AddHours(1).AddMinutes(30), DepartureDateTime = DateTime.Today.AddHours(1).AddMinutes(59) });

            gateRepositoryMoq.Setup(g => g.GetById(1)).Returns(gate);

            var service = new ScheduleService(gateRepositoryMoq.Object, flightsRepositoryMoq.Object);

            var result = service.ScheduleFlight(1, DateTime.Today.AddHours(1).AddMinutes(59), DateTime.Today.AddHours(2).AddMinutes(30));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CanNotScheduleFlightIfDepartureTimeOverlaps()
        {
            var gateRepositoryMoq = new Mock<IGatesRepository>();
            var flightsRepositoryMoq = new Mock<IFlightsRepository>();

            var gate = new Gate { Flights = new List<Flight>() };
            gate.Flights.Add(new Flight { ArrivalDateTime = DateTime.Today.AddHours(1), DepartureDateTime = DateTime.Today.AddHours(1).AddMinutes(29) });
            gate.Flights.Add(new Flight { ArrivalDateTime = DateTime.Today.AddHours(1).AddMinutes(30), DepartureDateTime = DateTime.Today.AddHours(1).AddMinutes(59) });

            gateRepositoryMoq.Setup(g => g.GetById(1)).Returns(gate);

            var service = new ScheduleService(gateRepositoryMoq.Object, flightsRepositoryMoq.Object);

            var result = service.ScheduleFlight(1, DateTime.Today.AddMinutes(45), DateTime.Today.AddHours(1).AddMinutes(15));
            Assert.IsFalse(result);
        }

    }
}
