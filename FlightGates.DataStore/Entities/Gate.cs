using System.Collections.Generic;

namespace FlightGates.DataStore.Entities
{
    public class Gate
    {
        public int Id { get; set; }
        public IList<Flight> Flights { get; set; } 
    }
}
