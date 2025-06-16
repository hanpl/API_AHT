namespace AHTAPI.Models
{
    public class AHT_SortingInfor
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string? Ip { get; set; }
        public int? TimeStart { get; set; }
        public int? TimeEnd { get; set; } 
        public int? NumberLine { get; set; }
        public int? CMobi { get; set; }
        public string? ConnectionId { get;set; }
    }
}
