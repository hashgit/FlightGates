using System.Web.Http;
using FlightGates.Models;
using FlightGates.Services;

namespace FlightGates.Controllers.Api
{
    [RoutePrefix("api/Flights")]
    public class FlightsController : ApiController
    {
        private readonly IScheduleService _scheduleService;

        public FlightsController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var flight = _scheduleService.GetFlight(id);
            if (flight == null) return NotFound();

            var model = new {flight.Id, flight.ArrivalDateTime, flight.DepartureDateTime, GateId = flight.Gate.Id};
            return Ok(model);
        }

        [HttpPost]
        [Route("{id}")]
        public IHttpActionResult Post(int id, FlightModel model)
        {
            var result = _scheduleService.UpdateFlight(id, model.GateId, model.ArrivalDateTime, model.DepartureDateTime);
            return Ok(result);
        }

        [HttpPut]
        public IHttpActionResult Put(FlightModel model)
        {
            var result = _scheduleService.ScheduleFlight(model.GateId, model.ArrivalDateTime, model.DepartureDateTime);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            var result = _scheduleService.CancelFlight(id);
            return Ok(result);
        }
    }
}
