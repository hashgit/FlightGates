using System;
using System.Collections.Generic;
using System.Linq;
using FlightGates.DataStore;
using FlightGates.DataStore.Entities;

namespace FlightGates.Services
{
    public interface IGateService
    {
        IEnumerable<Gate> GetAll();
        Gate Get(int id);
        Flight GetFlight(int id);
        bool UpdateFlight(int id, int gateId, DateTime arrivalDateTime, DateTime? departureDateTime);
        bool CancelFlight(int id);
        bool ScheduleFlight(int gateId, DateTime arrivalDateTime, DateTime? departureDateTime);
    }

    public class GateService : IGateService
    {
        private readonly IGatesRepository _repository;
        private readonly IFlightsRepository _flightsRepository;

        public GateService(IGatesRepository repository, IFlightsRepository flightsRepository)
        {
            _repository = repository;
            _flightsRepository = flightsRepository;
        }

        public IEnumerable<Gate> GetAll()
        {
            return _repository.GetAll();
        }

        public Gate Get(int id)
        {
            return _repository.GetById(id);
        }

        public Flight GetFlight(int id)
        {
            return _flightsRepository.GetById(id);
        }

        public bool UpdateFlight(int id, int gateId, DateTime arrivalDateTime, DateTime? departureDateTime)
        {
            var flight = _flightsRepository.GetById(id);
            if (flight == null) return false;

            var gate = _repository.GetById(gateId);
            if (gate == null) return false;

            if (departureDateTime == null)
                departureDateTime = arrivalDateTime.AddMinutes(30);

            if (!GateHasSlotAvailable(gate, flight, arrivalDateTime, departureDateTime.Value))
                return false;

            var existingGate = flight.Gate;
            flight.ArrivalDateTime = arrivalDateTime;
            flight.DepartureDateTime = departureDateTime.Value;
            flight.Gate = gate;

            existingGate.Flights.Remove(flight);
            gate.Flights.Add(flight);

            return true;
        }

        public bool CancelFlight(int id)
        {
            var flight = _flightsRepository.GetById(id);
            if (flight == null) return false;

            var gate = flight.Gate;
            _flightsRepository.Delete(flight);
            gate.Flights.Remove(flight);

            return true;
        }

        public bool ScheduleFlight(int gateId, DateTime arrivalDateTime, DateTime? departureDateTime)
        {
            if (departureDateTime == null)
                departureDateTime = arrivalDateTime.AddMinutes(30);

            var flight = new Flight { ArrivalDateTime = arrivalDateTime, DepartureDateTime = departureDateTime.Value };
            var gate = _repository.GetById(gateId);
            if (gate == null) return false;

            if (!GateHasSlotAvailable(gate, flight, arrivalDateTime, departureDateTime.Value))
                return false;

            _flightsRepository.Add(flight);
            flight.Gate = gate;
            gate.Flights.Add(flight);
            return true;
        }

        private static bool GateHasSlotAvailable(Gate gate, Flight flight, DateTime arrivalDateTime, DateTime departureDateTime)
        {
            var existingflight = gate.Flights.Where(f => f.ArrivalDateTime.Date == DateTime.Now.Date)
                .OrderBy(f => f.ArrivalDateTime)
                .FirstOrDefault(f => (f.ArrivalDateTime <= arrivalDateTime && f.DepartureDateTime >= arrivalDateTime)
                                    || (f.ArrivalDateTime <= departureDateTime && f.DepartureDateTime >= departureDateTime)
                                    || (arrivalDateTime <= f.ArrivalDateTime && departureDateTime >= f.ArrivalDateTime)
                                    || (arrivalDateTime <= f.DepartureDateTime && departureDateTime >= f.DepartureDateTime));

            return existingflight == null || existingflight == flight;
        }
    }
}
