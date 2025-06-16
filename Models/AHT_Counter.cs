namespace AHTAPI.Models
{
    public class AHT_Counter
    {
        public string Name { get; set; }
        public List<AHT_Departures>? Flights { set; get; }
    }
}
