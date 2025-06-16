using System.Text.Json.Serialization;

namespace AHTAPI.Models
{
    public class WaitingTimeFlight
    {
        public List<CheckinArea> CheckinArea { get; set; }
        public List<OtherArea> OtherArea { get; set; }
    }

    public class CheckinArea
    {
        public string AreaName { get; set; }
        public List<Flights> Flights { get; set; }
    }

    public class Flights
    {
        public string Name { get; set; }
        public string Airline { get; set; }
        public string Number { get; set; }
        public List<int> FlightCounters { get; set; }
        public List<Data> Data { get; set; }
        public string DepartureTime { get; set; }
        public string CounterStart { get; set; }
        public string CounterEnd { get; set; }
        public string ScheduleDate { get; set; }        
    }

    public class Data
    { 
        public int InPeople { get; set; }
        public double WaitingTime { get; set; }
        public string LaneIdOfFlight { get; set; }
        public double AverageProcessTime { get; set; }
        public List<string> LaneCounters { get; set; }

    }

    public class OtherArea
    {
        public string AreaName { get; set; }
        [JsonPropertyName("data")]
        public List<Datas> Datas { get; set; }
    }

    public class Datas
    {
        public int InPeople { get; set; }
        public string LaneId { get; set; }
        public double WaitingTime { get; set; }
        public double AverageProcessTime { get;set; }
        
    }
}
