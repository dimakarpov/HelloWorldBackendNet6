using HelloWorldBackend.Interfaces;
using HelloWorldBackend.Models.EventSimulator;

namespace HelloWorldBackend.Services
{
    public class EventSimulatorService : IEventSimulatorService
    {
        public IEnumerable<SimulatedEvent> GetSimulatedEvents(DateTime lastPleadingDate, DateTime preTrialDate)
        {
            var simulatedEventsResponse = new List<SimulatedEvent>();

            simulatedEventsResponse.Add(new SimulatedEvent() { EventType = Enums.EventType.PreliminaryHearing, Description = "דיון מקדמי", Date = lastPleadingDate.AddDays(30) });

            simulatedEventsResponse.Add(new SimulatedEvent() { EventType = Enums.EventType.PreliminaryHearingReportToCourt, Description = "דיון מקדמי - דיווח לבית המשפט", Date = lastPleadingDate.AddDays(-14) });

            simulatedEventsResponse.Add(new SimulatedEvent() { EventType = Enums.EventType.AffidavitsOfDocuments, Description = "תצהירי גילוי כללי", Date = lastPleadingDate.AddDays(30) });

            simulatedEventsResponse.Add(new SimulatedEvent() { EventType = Enums.EventType.QuestionnairesAndRequestsForSpecificDisclosure, Description = "שאלונים + דרישות לגילוי פרטני", Date = lastPleadingDate.AddDays(30) });

            simulatedEventsResponse.Add(new SimulatedEvent()
            {
                EventType = Enums.EventType.QuestionnairesAndRequestsForSpecificDisclosurePartiesResponses,
                Description = "מענה לשאלונים + דרישות לגילוי פרטני",
                Date = simulatedEventsResponse.Where(a => a.EventType == Enums.EventType.QuestionnairesAndRequestsForSpecificDisclosure).FirstOrDefault().Date.AddDays(30)
            });

            simulatedEventsResponse.Add(new SimulatedEvent()
            {
                EventType = Enums.EventType.ReviewOfDocuments,
                Description = "עיון במסמכים",
                Date = simulatedEventsResponse.Where(a => a.EventType == Enums.EventType.QuestionnairesAndRequestsForSpecificDisclosure).FirstOrDefault().Date.AddDays(30)
            });

            simulatedEventsResponse.Add(new SimulatedEvent() { EventType = Enums.EventType.ListsOfPreTrialMotions, Description = "רשימת בקשות", Date = preTrialDate.AddDays(-20) });

            simulatedEventsResponse.Add(new SimulatedEvent()
            {
                EventType = Enums.EventType.PartiesNoticesRegardingListsOfPreTrialMotions,
                Description = "התראה על רשימת בקשות",
                Date = simulatedEventsResponse.Where(a => a.EventType == Enums.EventType.ListsOfPreTrialMotions).FirstOrDefault().Date.AddDays(-7)
            });

            simulatedEventsResponse.Add(new SimulatedEvent()
            {
                EventType = Enums.EventType.ListsOfPreTrialMotionsPartiesResponses,
                Description = "תשובה לרשימת בקשות",
                Date = simulatedEventsResponse.Where(a => a.EventType == Enums.EventType.ListsOfPreTrialMotions).FirstOrDefault().Date.AddDays(14)
            });

            simulatedEventsResponse.Add(new SimulatedEvent() { EventType = Enums.EventType.WitnessesListPlaintiff, Description = "רשימת עדים מטעם התובע", Date = preTrialDate.AddDays(-20) });

            simulatedEventsResponse.Add(new SimulatedEvent()
            {
                EventType = Enums.EventType.WitnessesListDefendant,
                Description = "רשימת עדים מטעם הנתבע",
                Date = simulatedEventsResponse.Where(a => a.EventType == Enums.EventType.WitnessesListPlaintiff).FirstOrDefault().Date.AddDays(14)
            });

            AdjustWeekendEvents(simulatedEventsResponse);

            return simulatedEventsResponse;
        }

        private void AdjustWeekendEvents(IEnumerable<SimulatedEvent> simulatedEventsResponse)
        {
            foreach (var item in simulatedEventsResponse)
            {
                if (item.Date.DayOfWeek == DayOfWeek.Friday)
                {
                    item.Date = item.Date.AddDays(2);
                    item.Notes.Add("במקור יום שישי, נדחה ביומיים");
                }
                else if (item.Date.DayOfWeek == DayOfWeek.Saturday)
                {
                    item.Date = item.Date.AddDays(1);
                    item.Notes.Add("במקור יום שבת, נדחה ביום");
                }
            }
        }

    }
}
