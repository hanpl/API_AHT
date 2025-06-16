namespace AHTAPI.Models
{
    public class AHT_Stands
    {
        public string Id { get; set; }
        public string? StandName { get; set; }
        public string? ArrOrDep { get; set; }
        public string? Flight { get; set; }
        public DateTime? Schedule { get; set; }
        public string? Status { get; set; }
        public DateTime? StandStart { get; set; }
        public DateTime? StandEnd { get; set; }
        public DateTime? Mcdt { get; set; }
        public DateTime? Mcat { get; set; }
        public string? DevicesBle { get; set; }
        public string? Note { get; set; }
        public DateTime? TimeUpdate { get; set; }

    }
}
