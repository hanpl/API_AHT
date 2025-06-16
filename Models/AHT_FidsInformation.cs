namespace AHTAPI.Models
{
    public class AHT_FidsInformation
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string? Ip {  get; set; }
        public int RollOn { get; set; }
        public int RollOff { get; set; }
        public int PageSize { get; set; }
        public int MaxPages { get; set; }
        public int PageInterval { get; set; }
        public int ReloadInterval { get; set; }
        public string? Mobilities { get; set; }
        public string? ConnectionId { get; set; }
    }
}
