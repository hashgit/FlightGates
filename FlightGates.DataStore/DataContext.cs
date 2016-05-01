using System;
using System.Collections.Generic;
using FlightGates.DataStore.Entities;

namespace FlightGates.DataStore
{
    public interface IDataContext
    {
        IList<Gate> Gates { get; set; }
        IList<Flight> Flights { get; set; }

        int FlightCount { get; set; }
        int GateCount { get; set; }
    }

    public class DataContext : IDataContext
    {
        public DataContext()
        {
            Gates = new List<Gate> { new Gate { Id = ++GateCount, Flights = new List<Flight>() }, new Gate { Id = ++GateCount, Flights = new List<Flight>() } };
            Flights = new List<Flight>
                {
                    new Flight { Gate = Gates[0], Id = ++FlightCount, ArrivalDateTime = new DateTime(2016, 05, 01, 02, 30, 00), DepartureDateTime = new DateTime(2016, 05, 01, 03, 00, 00)},
                    new Flight { Gate = Gates[0], Id = ++FlightCount, ArrivalDateTime = new DateTime(2016, 05, 01, 03, 00, 00), DepartureDateTime = new DateTime(2016, 05, 01, 03, 30, 00)}
                };

            Gates[0].Flights.Add(Flights[0]);
            Gates[0].Flights.Add(Flights[1]);
        }

        public IList<Gate> Gates { get; set; }
        public IList<Flight> Flights { get; set; }
        public int FlightCount { get; set; }
        public int GateCount { get; set; }
    }
}