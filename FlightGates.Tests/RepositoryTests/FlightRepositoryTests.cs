using System;
using System.Linq;
using FlightGates.DataStore;
using FlightGates.DataStore.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlightGates.Tests.RepositoryTests
{
    [TestClass]
    public class FlightRepositoryTests
    {
        [TestMethod]
        public void CanGetSingleFlight()
        {
            var flightRepository = new FlightsRepository(new DataContext());
            var flight = flightRepository.GetById(1);

            Assert.IsNotNull(flight);
        }

        [TestMethod]
        public void CanQueryNonExistingFlight()
        {
            var flightRepository = new FlightsRepository(new DataContext());
            var flight = flightRepository.GetById(50);

            Assert.IsNull(flight);
        }

        [TestMethod]
        public void CanAddNewFlight()
        {
            var flightRepository = new FlightsRepository(new DataContext());
            var flights = flightRepository.GetAll();

            Assert.IsNotNull(flights);
            Assert.AreEqual(20, flights.Count);

            // not adding it to a gate because we are only testing the flight repo
            var flight = new Flight {ArrivalDateTime = DateTime.Now, DepartureDateTime = DateTime.Now.AddHours(1)};
            flightRepository.Add(flight);

            Assert.AreEqual(21, flight.Id);
        }

        [TestMethod]
        public void CanDeleteFlight()
        {
            var flightRepository = new FlightsRepository(new DataContext());
            var flights = flightRepository.GetAll();

            Assert.IsNotNull(flights);
            Assert.AreEqual(20, flights.Count);

            flightRepository.Delete(flights.Single(f => f.Id == 1));
            flights = flightRepository.GetAll();
            Assert.IsNotNull(flights);
            Assert.AreEqual(19, flights.Count);
            Assert.AreEqual(2, flights.First().Id);
        }
    }
}
