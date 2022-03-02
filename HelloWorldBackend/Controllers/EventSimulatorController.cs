using HelloWorldBackend.Models.EventSimulator;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace HelloWorldBackend.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class EventSimulatorController : Controller
    {
        [HttpGet(Name = "GetEventDates")]
        public IEnumerable<SimulatedEvent> GetEventDates(DateTime lastPleadingDate, DateTime preTrialDate)
        {
            var simulatedEventsResponse = new List<SimulatedEvent>();

            simulatedEventsResponse.Add(new SimulatedEvent() { EventType = Enums.EventType.PreliminaryHearing, Description = "", Date = lastPleadingDate.AddDays(30) });

            simulatedEventsResponse.Add(new SimulatedEvent() { EventType = Enums.EventType.PreliminaryHearingReportToCourt, Description = "", Date = lastPleadingDate.AddDays(-14) });

            simulatedEventsResponse.Add(new SimulatedEvent() { EventType = Enums.EventType.AffidavitsOfDocuments, Description = "", Date = lastPleadingDate.AddDays(30) });

            simulatedEventsResponse.Add(new SimulatedEvent() { EventType = Enums.EventType.QuestionnairesAndRequestsForSpecificDisclosure, Description = "", Date = lastPleadingDate.AddDays(30) });

            simulatedEventsResponse.Add(new SimulatedEvent() { EventType = Enums.EventType.QuestionnairesAndRequestsForSpecificDisclosurePartiesResponses, Description = "", 
                Date = simulatedEventsResponse.Where(a => a.EventType == Enums.EventType.QuestionnairesAndRequestsForSpecificDisclosure).FirstOrDefault().Date.AddDays(30)});

            simulatedEventsResponse.Add(new SimulatedEvent() { EventType = Enums.EventType.ReviewOfDocuments, Description = "",
                Date = simulatedEventsResponse.Where(a => a.EventType == Enums.EventType.QuestionnairesAndRequestsForSpecificDisclosure).FirstOrDefault().Date.AddDays(30)
            });

            simulatedEventsResponse.Add(new SimulatedEvent() { EventType = Enums.EventType.ListsOfPreTrialMotions, Description = "", Date = preTrialDate.AddDays(-20) });

            simulatedEventsResponse.Add(new SimulatedEvent() { EventType = Enums.EventType.PartiesNoticesRegardingListsOfPreTrialMotions, Description = "",
                Date = simulatedEventsResponse.Where(a => a.EventType == Enums.EventType.ListsOfPreTrialMotions).FirstOrDefault().Date.AddDays(-7)
            });

            simulatedEventsResponse.Add(new SimulatedEvent() { EventType = Enums.EventType.ListsOfPreTrialMotionsPartiesResponses, Description = "",
                Date = simulatedEventsResponse.Where(a => a.EventType == Enums.EventType.ListsOfPreTrialMotions).FirstOrDefault().Date.AddDays(14)
            });

            simulatedEventsResponse.Add(new SimulatedEvent() { EventType = Enums.EventType.WitnessesListPlaintiff, Description = "", Date = preTrialDate.AddDays(-20) });

            simulatedEventsResponse.Add(new SimulatedEvent() { EventType = Enums.EventType.WitnessesListDefendant, Description = "",
                Date = simulatedEventsResponse.Where(a => a.EventType == Enums.EventType.WitnessesListPlaintiff).FirstOrDefault().Date.AddDays(14)
            });

            return simulatedEventsResponse;
        }
    }
}
