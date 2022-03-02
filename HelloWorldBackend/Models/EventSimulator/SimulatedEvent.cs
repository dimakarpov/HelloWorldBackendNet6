namespace HelloWorldBackend.Models.EventSimulator
{
    public class SimulatedEvent
    {
        public Enums.EventType EventType { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
