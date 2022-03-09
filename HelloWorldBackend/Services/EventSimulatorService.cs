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

            ApplyWeekendDelays(simulatedEventsResponse);
            ApplyRecessDelays(simulatedEventsResponse);
            ApplyHolidayDelays(simulatedEventsResponse);

            return simulatedEventsResponse;
        }

        private void ApplyWeekendDelays(IEnumerable<SimulatedEvent> simulatedEventsResponse)
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

        private void ApplyRecessDelays(IEnumerable<SimulatedEvent> simulatedEventsResponse)
        {
            var recessList = new List<Recess>();

            recessList.Add(new Recess() { Id = 1, Description = "פגרת פסח 2022", Start = new DateTime(2022, 4, 15), End = new DateTime(2022, 4, 22) });
            recessList.Add(new Recess() { Id = 2, Description = "פגרת פסח 2023", Start = new DateTime(2023, 4, 5), End = new DateTime(2023, 4, 12) });
            recessList.Add(new Recess() { Id = 3, Description = "פגרת פסח 2024", Start = new DateTime(2024, 4, 22), End = new DateTime(2024, 4, 29) });
            recessList.Add(new Recess() { Id = 4, Description = "פגרת פסח 2025", Start = new DateTime(2025, 4, 12), End = new DateTime(2025, 4, 19) });
            recessList.Add(new Recess() { Id = 5, Description = "פגרת פסח 2026", Start = new DateTime(2026, 4, 1), End = new DateTime(2026, 4, 8) });
            recessList.Add(new Recess() { Id = 6, Description = "פגרת פסח 2027", Start = new DateTime(2027, 4, 21), End = new DateTime(2027, 4, 28) });
            recessList.Add(new Recess() { Id = 7, Description = "פגרת פסח 2028", Start = new DateTime(2028, 4, 10), End = new DateTime(2028, 4, 17) });
            recessList.Add(new Recess() { Id = 8, Description = "פגרת פסח 2029", Start = new DateTime(2029, 3, 30), End = new DateTime(2029, 4, 6) });
            recessList.Add(new Recess() { Id = 9, Description = "פגרת פסח 2030", Start = new DateTime(2030, 4, 17), End = new DateTime(2030, 4, 24) });
            recessList.Add(new Recess() { Id = 10, Description = "פגרת פסח 2031", Start = new DateTime(2031, 4, 7), End = new DateTime(2031, 4, 14) });

            recessList.Add(new Recess() { Id = 11, Description = "פגרת סוכות 2022", Start = new DateTime(2022, 10, 9), End = new DateTime(2022, 10, 17) });
            recessList.Add(new Recess() { Id = 12, Description = "פגרת סוכות 2023", Start = new DateTime(2023, 9, 29), End = new DateTime(2023, 10, 7) });
            recessList.Add(new Recess() { Id = 13, Description = "פגרת סוכות 2024", Start = new DateTime(2024, 10, 16), End = new DateTime(2024, 10, 24) });
            recessList.Add(new Recess() { Id = 14, Description = "פגרת סוכות 2025", Start = new DateTime(2025, 10, 6), End = new DateTime(2025, 10, 14) });
            recessList.Add(new Recess() { Id = 15, Description = "פגרת סוכות 2026", Start = new DateTime(2026, 9, 25), End = new DateTime(2026, 10, 3) });
            recessList.Add(new Recess() { Id = 16, Description = "פגרת סוכות 2027", Start = new DateTime(2027, 10, 15), End = new DateTime(2027, 10, 23) });
            recessList.Add(new Recess() { Id = 17, Description = "פגרת סוכות 2028", Start = new DateTime(2028, 10, 4), End = new DateTime(2028, 10, 12) });
            recessList.Add(new Recess() { Id = 18, Description = "פגרת סוכות 2029", Start = new DateTime(2029, 9, 23), End = new DateTime(2029, 10, 1) });
            recessList.Add(new Recess() { Id = 19, Description = "פגרת סוכות 2030", Start = new DateTime(2030, 10, 11), End = new DateTime(2030, 10, 19) });
            recessList.Add(new Recess() { Id = 20, Description = "פגרת סוכות 2031", Start = new DateTime(2031, 10, 1), End = new DateTime(2031, 10, 9) });

            recessList.Add(new Recess() { Id = 21, Description = "פגרת קיץ 2022", Start = new DateTime(2022, 7, 21), End = new DateTime(2022, 9, 5) });
            recessList.Add(new Recess() { Id = 22, Description = "פגרת קיץ 2023", Start = new DateTime(2023, 7, 21), End = new DateTime(2023, 9, 5) });
            recessList.Add(new Recess() { Id = 23, Description = "פגרת קיץ 2024", Start = new DateTime(2024, 7, 21), End = new DateTime(2024, 9, 5) });
            recessList.Add(new Recess() { Id = 24, Description = "פגרת קיץ 2025", Start = new DateTime(2025, 7, 21), End = new DateTime(2025, 9, 5) });
            recessList.Add(new Recess() { Id = 25, Description = "פגרת קיץ 2026", Start = new DateTime(2026, 7, 21), End = new DateTime(2026, 9, 5) });
            recessList.Add(new Recess() { Id = 26, Description = "פגרת קיץ 2027", Start = new DateTime(2027, 7, 21), End = new DateTime(2027, 9, 5) });
            recessList.Add(new Recess() { Id = 27, Description = "פגרת קיץ 2028", Start = new DateTime(2028, 7, 21), End = new DateTime(2028, 9, 5) });
            recessList.Add(new Recess() { Id = 28, Description = "פגרת קיץ 2029", Start = new DateTime(2029, 7, 21), End = new DateTime(2029, 9, 5) });
            recessList.Add(new Recess() { Id = 29, Description = "פגרת קיץ 2030", Start = new DateTime(2030, 7, 21), End = new DateTime(2030, 9, 5) });
            recessList.Add(new Recess() { Id = 30, Description = "פגרת קיץ 2031", Start = new DateTime(2031, 7, 21), End = new DateTime(2031, 9, 5) });

            foreach (var item in simulatedEventsResponse)
            {
                if (recessList.Where(a => a.Start <= item.Date && a.End >= item.Date).Any())
                {
                    var recess = recessList.Where(a => a.Start <= item.Date && a.End >= item.Date).FirstOrDefault();
                    item.Date = recess.End.AddDays((item.Date - recess.Start).TotalDays);
                    item.Notes.Add($"המועד נדחה בעקבות {recess.Description}");
                }
            }
        }

        private void ApplyHolidayDelays(IEnumerable<SimulatedEvent> simulatedEventsResponse)
        {

        }
    }
}
