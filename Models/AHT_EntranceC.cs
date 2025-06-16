namespace AHTAPI.Models
{
    public class AHT_EntranceC
    {
        public string? LineCode { set; get; }
        public List<CodeShare>? Code { set; get; }
        public string? Schedule { get; set; }
        public string? Estimated { get; set; }
        public string? Flight { get; set; }
        public string? Mcdt { get; set; }
        public string? RowFrom { get; set; }
        public string? RowTo { get; set; }
    }
}
