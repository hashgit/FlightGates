using System;
using System.Collections.Generic;
using System.Linq;
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
                    new Flight { Gate = Gates[0], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(1), DepartureDateTime = DateTime.Today.AddHours(1).AddMinutes(30)},
                    new Flight { Gate = Gates[0], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(2), DepartureDateTime = DateTime.Today.AddHours(2).AddMinutes(30)},
                    new Flight { Gate = Gates[0], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(3), DepartureDateTime = DateTime.Today.AddHours(3).AddMinutes(30)},
                    new Flight { Gate = Gates[0], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(4), DepartureDateTime = DateTime.Today.AddHours(4).AddMinutes(30)},
                    new Flight { Gate = Gates[0], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(5), DepartureDateTime = DateTime.Today.AddHours(5).AddMinutes(30)},
                    new Flight { Gate = Gates[0], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(6), DepartureDateTime = DateTime.Today.AddHours(6).AddMinutes(30)},
                    new Flight { Gate = Gates[0], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(7), DepartureDateTime = DateTime.Today.AddHours(7).AddMinutes(30)},
                    new Flight { Gate = Gates[0], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(8), DepartureDateTime = DateTime.Today.AddHours(8).AddMinutes(30)},
                    new Flight { Gate = Gates[0], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(9), DepartureDateTime = DateTime.Today.AddHours(9).AddMinutes(30)},
                    new Flight { Gate = Gates[0], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(10), DepartureDateTime = DateTime.Today.AddHours(10).AddMinutes(30)},

                    // gate 1

                    new Flight { Gate = Gates[1], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(1), DepartureDateTime = DateTime.Today.AddHours(1).AddMinutes(30)},
                    new Flight { Gate = Gates[1], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(2), DepartureDateTime = DateTime.Today.AddHours(2).AddMinutes(30)},
                    new Flight { Gate = Gates[1], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(3), DepartureDateTime = DateTime.Today.AddHours(3).AddMinutes(30)},
                    new Flight { Gate = Gates[1], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(4), DepartureDateTime = DateTime.Today.AddHours(4).AddMinutes(30)},
                    new Flight { Gate = Gates[1], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(5), DepartureDateTime = DateTime.Today.AddHours(5).AddMinutes(30)},
                    new Flight { Gate = Gates[1], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(6), DepartureDateTime = DateTime.Today.AddHours(6).AddMinutes(30)},
                    new Flight { Gate = Gates[1], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(7), DepartureDateTime = DateTime.Today.AddHours(7).AddMinutes(30)},
                    new Flight { Gate = Gates[1], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(8), DepartureDateTime = DateTime.Today.AddHours(8).AddMinutes(30)},
                    new Flight { Gate = Gates[1], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(9), DepartureDateTime = DateTime.Today.AddHours(9).AddMinutes(30)},
                    new Flight { Gate = Gates[1], Id = ++FlightCount, ArrivalDateTime = DateTime.Today.AddHours(10), DepartureDateTime = DateTime.Today.AddHours(10).AddMinutes(30)},
            };

            foreach (var flight in Flights.Where(f => f.Gate == Gates[0]))
            {
                Gates[0].Flights.Add(flight);
            }

            foreach (var flight in Flights.Where(f => f.Gate == Gates[1]))
            {
                Gates[1].Flights.Add(flight);
            }
        }

        public IList<Gate> Gates { get; set; }
        public IList<Flight> Flights { get; set; }
        public int FlightCount { get; set; }
        public int GateCount { get; set; }
    }
}