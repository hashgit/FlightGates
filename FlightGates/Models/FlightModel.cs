using System;

namespace FlightGates.Models
{
    public class FlightModel
    {
        public int GateId { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public DateTime? DepartureDateTime { get; set; }
    }
}