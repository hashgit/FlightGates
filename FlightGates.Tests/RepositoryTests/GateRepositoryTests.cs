using FlightGates.DataStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlightGates.Tests.RepositoryTests
{
    [TestClass]
    public class GateRepositoryTests
    {
        [TestMethod]
        public void CanGetGates()
        {
            var gateRepository = new GatesRepository(new DataContext());
            var gates = gateRepository.GetAll();

            Assert.IsNotNull(gates);
            Assert.AreEqual(2, gates.Count);

            Assert.AreEqual(1, gates[0].Id);
            Assert.AreEqual(2, gates[1].Id);
        }

        [TestMethod]
        public void CanGetSingleGate()
        {
            var gateRepository = new GatesRepository(new DataContext());
            var gate = gateRepository.GetById(1);

            Assert.IsNotNull(gate);
        }

        [TestMethod]
        public void CanGetGateWithNoFlights()
        {
            var gateRepository = new GatesRepository(new DataContext());
            var gate = gateRepository.GetById(2);

            Assert.IsNotNull(gate);
            Assert.IsNotNull(gate.Flights);
            Assert.AreEqual(0, gate.Flights.Count);
        }

        [TestMethod]
        public void CanQueryNonExistingGate()
        {
            var gateRepository = new GatesRepository(new DataContext());
            var gate = gateRepository.GetById(10);

            Assert.IsNull(gate);
        }
    }
}
