using System.Collections.Generic;
using System.Linq;
using FlightGates.DataStore.Entities;

namespace FlightGates.DataStore
{
    public interface IFlightsRepository
    {
        Flight GetById(int id);
        void Delete(Flight flight);
        void Add(Flight flight);
        IList<Flight> GetAll();
    }

    public class FlightsRepository : IFlightsRepository
    {
        private readonly IDataContext _dataContext;

        public FlightsRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Flight GetById(int id)
        {
            return _dataContext.Flights.FirstOrDefault(f => f.Id == id);
        }

        public void Delete(Flight flight)
        {
            _dataContext.Flights.Remove(flight);
        }

        public void Add(Flight flight)
        {
            flight.Id = ++_dataContext.FlightCount;
            _dataContext.Flights.Add(flight);
        }

        public IList<Flight> GetAll()
        {
            return _dataContext.Flights;
        }
    }
}