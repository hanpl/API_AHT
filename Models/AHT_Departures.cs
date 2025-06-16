namespace AHTAPI.Models
{
    public class AHT_Departures
    {
        public string Id { get; set; }
        public string ScheduledDate { get; set; }
        public string Schedule { get; set; }
        public string Estimated { get; set; }
        public string Actual { get; set; }
        public string LineCode { get; set; }
        public string Flight { get; set; }
        public string City { get; set; }
        public string Gate { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public string RowFrom { get; set; }
        public string RowTo { get; set; }
        public string CheckInCounters { get; set; }
        public string CounterStart { get; set; }
        public string CounterEnd { get; set; }
        public string GateStart { get; set; }
        public string GateEnd { get; set; }
        public string Mcdt { get; set; }
        public string CodeShare { get; set; } 
    }
}
