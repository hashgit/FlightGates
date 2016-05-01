using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightGates.DataStore.Entities
{
    public class Flight
    {
        public int Id { get; set; }
        public Gate Gate { get; set; }

        public DateTime ArrivalDateTime { get; set; }
        public DateTime DepartureDateTime { get; set; }
    }
}
