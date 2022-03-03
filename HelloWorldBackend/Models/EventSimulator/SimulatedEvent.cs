namespace HelloWorldBackend.Models.EventSimulator
{
    public class SimulatedEvent
    {
        public SimulatedEvent()
        {
            Description = String.Empty;
            Notes = new List<string>();
        }
        public Enums.EventType EventType { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string DateString { get { return Date.ToString("dd/MM/yyyy"); } }
        public List<string> Notes { get; set; }
    }
}
