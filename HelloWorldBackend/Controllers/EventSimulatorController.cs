using HelloWorldBackend.Models.EventSimulator;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldBackend.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class EventSimulatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IEnumerable<SimulatedEvent> GetEventDates([FromQuery] DateTime lastPleadingDate, [FromQuery] DateTime preTrialDate)
        {
            var simulatedEventsResponse = new List<SimulatedEvent>();

            return simulatedEventsResponse;
        }
    }
}
