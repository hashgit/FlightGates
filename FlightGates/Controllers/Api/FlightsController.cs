using System.Web.Http;
using FlightGates.Models;
using FlightGates.Services;

namespace FlightGates.Controllers.Api
{
    [RoutePrefix("api/Flights")]
    public class FlightsController : ApiController
    {
        private readonly IGateService _gateService;

        public FlightsController(IGateService gateService)
        {
            _gateService = gateService;
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var flight = _gateService.GetFlight(id);
            if (flight == null) return NotFound();

            var model = new {flight.Id, flight.ArrivalDateTime, flight.DepartureDateTime, GateId = flight.Gate.Id};
            return Ok(model);
        }

        [HttpPost]
        [Route("{id}")]
        public IHttpActionResult Post(int id, FlightModel model)
        {
            var result = _gateService.UpdateFlight(id, model.GateId, model.ArrivalDateTime, model.DepartureDateTime);
            return Ok(result);
        }

        [HttpPut]
        public IHttpActionResult Put(FlightModel model)
        {
            var result = _gateService.ScheduleFlight(model.GateId, model.ArrivalDateTime, model.DepartureDateTime);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            var result = _gateService.CancelFlight(id);
            return Ok(result);
        }
    }
}
