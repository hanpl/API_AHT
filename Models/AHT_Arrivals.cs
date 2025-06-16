namespace AHTAPI.Models
{
    public class AHT_Arrivals
    {
        public string Id { get; set; }
        public string? ScheduledDate { get; set; }
        public string? Schedule { get; set; }
        public string? Estimated { get; set; }
        public string? Actual { get; set; }
        public string? LineCode { get; set; }
        public string? Flight { get; set; }
        public string? City { get; set; }
        public string? Belt { get; set; }
        public string? Remark { get; set; }
    }
}
