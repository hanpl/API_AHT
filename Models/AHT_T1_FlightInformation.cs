using System.Threading;

namespace AHTAPI.Models
{
    public class AHT_T1_FlightInformation
    {
        public string Id { get; set; }
        public string? Adi { get; set; }
        public string? LineCode { get; set; }
        public string? Number { get; set; }
        public string? FlightNo { get; set; }
        public string? ScheduledDate { get; set; }
        public string? Route { get; set; }
        public string? Terminal { get; set; }
        public string? City { get; set; }
        public string? CityName { get; set; }
        public string? Aircraft { get; set; }
        public string? PaxCount { get; set; }
        public string? SIBT { get; set; }
        public string? EIBT { get; set; }
        public string? AIBT { get; set; }
        public string? Belt { get; set; }
        public string? SOBT { get; set; }
        public string? EOBT { get; set ; }
        public string? AOBT { get ; set; }
        public string? DGATE { get; set; }
        public string? Status { get; set; }
        public string? Remark { get; set; }
        public string? CkiRow { get ; set; }
        public string? CkiOPN { get ; set; }
        public string? CheckInCounters { get; set; }
        public DateTime GateStart { get; set; }
        public DateTime GateEnd { get; set; }
        public DateTime CounterStart { get; set; }
        public DateTime CounterEnd { get; set; }
        public DateTime ClaimStart { get; set; }
        public DateTime ClaimEnd { get; set; }
        public DateTime TimeSet { get; set; }
        public DateTime TimeUpdate { get; set; }
        public string? ValueOLD { get; set; } 
        public string? InputSource { get; set;}
    }
}
