using System;
using System.Linq;
using System.Web.Http;
using FlightGates.Services;

namespace FlightGates.Controllers.Api
{
    [RoutePrefix("api/Gates")]
    public class GatesController : ApiController
    {
        private readonly IGateService _gateService;

        public GatesController(IGateService gateService)
        {
            _gateService = gateService;
        }

        [HttpGet]
        [Route("")]
        // GET: Gates
        public IHttpActionResult Get()
        {
            var models = _gateService.GetAll().Select(gate => new { gate.Id });
            return Ok(models);
        }

        [HttpGet]
        [Route("{id}")]
        // GET: Gates
        public IHttpActionResult Get(int id, DateTime? date = null)
        {
            var gate = _gateService.Get(id);
            if (gate == null) return NotFound();

            if (date == null)
                date = DateTime.Now.Date;

            var model = new { gate.Id, Flights = gate.Flights.Where(f => f.ArrivalDateTime.Date == date.Value.Date)
                .Select(f => new { f.Id, f.ArrivalDateTime, f.DepartureDateTime }) };
            return Ok(model);
        }
    }
}