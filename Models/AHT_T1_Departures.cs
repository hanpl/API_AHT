namespace AHTAPI.Models
{
    public class AHT_T1_Departures /*Id,ScheduledDate, LineCode, FlightNo, Route, City, SOBT, EOBT, Mcdt, Status, CkiRow*/
    {
        public string Id { get; set; }
        public string? ScheduledDate { get; set; }
        public string? LineCode { get; set; }
        public string? FlightNo { get; set; }
        public string? Route { get; set; }
        public string? City { get; set; }
        public string? SOBT { get; set; }
        public string? EOBT { get; set; }
        public string? Mcdt { get; set; }
        public string? Status { get; set; }
        public string? CkiRow { get; set; }
        public string? DGATE { get;set; }
    }
}
