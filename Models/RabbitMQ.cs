namespace AHTAPI.Models
{
    public class RabbitMQ
    {
        public string FlightNo { get; set; }
        public string? FlightDate { get; set; }
        public string? Route { get; set; }
        public List<FlightData> FlightData { get; set; }
    }
}
