using HelloWorldBackend.Interfaces;
using HelloWorldBackend.Models.EventSimulator;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace HelloWorldBackend.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class EventSimulatorController : Controller
    {
        private readonly IEventSimulatorService _eventSimulatorService;
        public EventSimulatorController(IEventSimulatorService eventSimulatorService)
        {
            _eventSimulatorService = eventSimulatorService;
        }

        [HttpGet(Name = "GetEventDates")]
        public IEnumerable<SimulatedEvent> GetEventDates(DateTime lastPleadingDate, DateTime preTrialDate)
        {
            var simulatedEventsResponse = _eventSimulatorService.GetSimulatedEvents(lastPleadingDate, preTrialDate);

            return simulatedEventsResponse;
        }
    }
}
