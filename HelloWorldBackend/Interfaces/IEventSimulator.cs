using HelloWorldBackend.Models.EventSimulator;

namespace HelloWorldBackend.Interfaces
{
    public interface IEventSimulatorService
    {
        IEnumerable<SimulatedEvent> GetSimulatedEvents(DateTime lastPleadingDate, DateTime preTrialDate);
    }
}
