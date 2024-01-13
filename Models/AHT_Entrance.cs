namespace AHTAPI.Models
{
    public class AHT_Entrance
    {
        public string? LineCode { set; get; }
        public List<CodeShare>? Code { set; get; }
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
