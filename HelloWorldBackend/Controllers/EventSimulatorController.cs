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

        public IEnumerable<> GetEventDates([FromQuery] DateTime lastPleadingDate, [FromQuery] DateTime preTrialDate)
        {
            return View();
        }
    }
}
