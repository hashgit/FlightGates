using System;
using System.Linq;
using System.Web.Http;
using FlightGates.Services;

namespace FlightGates.Controllers.Api
{
    [RoutePrefix("api/Gates")]
    public class GatesController : ApiController
    {
        private readonly IScheduleService _scheduleService;

        public GatesController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        [Route("")]
        // GET: Gates
        public IHttpActionResult Get()
        {
            var models = _scheduleService.GetAllGates().Select(gate => new { gate.Id });
            return Ok(models);
        }

        [HttpGet]
        [Route("{id}")]
        // GET: Gates
        public IHttpActionResult Get(int id, DateTime? date = null)
        {
            var gate = _scheduleService.GetGate(id);
            if (gate == null) return NotFound();

            if (date == null)
                date = DateTime.Now.Date;

            var model = new { gate.Id, Flights = gate.Flights.Where(f => f.ArrivalDateTime.Date == date.Value.Date)
                .Select(f => new { f.Id, f.ArrivalDateTime, f.DepartureDateTime }) };
            return Ok(model);
        }
    }
}