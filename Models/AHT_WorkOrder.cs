namespace AHTAPI.Models
{
    public class AHT_WorkOrder
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string SystemName { get; set; }
        public string? Location { get; set; }
        public string? Status { get; set; }
        public string? ErrorDevice { get; set; }
        public string? Error { get; set; }
        public string? TimeOrder { get; set; }
        public string? TimeStart { get; set; }
        public string? TimeEnd { get; set; }
        public string? Description { get; set; }
        public string? Complate { get; set; }
    }
}


