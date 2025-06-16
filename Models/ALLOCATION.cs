using static System.Net.Mime.MediaTypeNames;

namespace AHTAPI.Models
{
    public class ALLOCATION
    {
		public int No {  get; set; }
		public string AC_TYPE { get; set; }
		public string ARR_FLIGHT { get; set; }
		public DateTime? ETA { get; set; }
		public DateTime? STA { get; set; }
		public string FLIGHT_FROM { get; set; }
		public string ARR_STAND { get;set; }
		public string DEP_FLIGHT { get; set; }
        public DateTime? STD { get; set; }
        public DateTime? ETD { get; set; }
		public string FLIGHT_TO { get; set; }
		public string DEP_STAND { get;set; }
        public DateTime? TIMESET { get; set; }
        public DateTime? TIMEUPDATE { get; set; }
        public string STATUS { get;	set;}
    }
}
