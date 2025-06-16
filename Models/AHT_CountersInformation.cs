namespace AHTAPI.Models
{
    public class AHT_CountersInformation
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Ip { get; set; }
        public string? Location { get; set; }
        public DateTime? Time { get; set; }
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public string? Flight { get; set; }
        public string? AutoImg { get; set; }
        public string? SetImg { get; set; }
        public DateTime? TimeMcdt { get; set; }
        public string? Status { get; set; }
        public string? Counters { get; set; }
        public string? Mode { get; set; }
        public string? Auto { get; set; }
        public string? ConnectionId { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
