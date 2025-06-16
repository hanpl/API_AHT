namespace AHTAPI.Models
{
    public class AlertStateZalo
    {
        public int MessageId { get; set; }
        public string? MessageText { get; set; }
        public DateTime SentTime { get; set; }
        public bool Confirmed { get; set; }
        public int RetryCount { get; set; }

    }
}
