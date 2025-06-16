namespace AHTAPI.Models
{
    public class AHT_Entrance
    {
        public string? LineCode { set; get; }
        public List<CodeShare>? Code { set; get; }
        public string? Schedule { get; set; }
        public string? Flight { get; set; }
        public string? Mcdt { get; set; }
        public string? RowFrom { get; set; }
        public string? RowTo { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            AHT_Entrance other = (AHT_Entrance)obj;
            return LineCode == other.LineCode;
        }

        public override int GetHashCode()
        {
            return LineCode.GetHashCode();
        }
    }
}
