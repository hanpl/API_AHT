namespace AHTAPI.Models
{
    public class AHT_FlightInformation
    {
        public string Id { get; set; }
        public string ActionName { get; set; }
        public string Adi { get; set; }
        public string Belt { get; set; }
        public string City { get; set; }
        public string CityName { get; set; }
        public string Domesticintcode { get; set; }
        public string Gate { get; set; }
        public string LineCode { get; set; }
        public string Number { get; set; }
        public string RowFrom { get; set; }
        public string RowTo { get; set; }
        public DateTime? Actual { get; set; }
        public DateTime? Schedule { get; set; }
        public DateTime? Estimated { get; set; }
        public string ScheduledDate { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string Aircraft { get; set; }
        public string PaxCount { get; set; }
        public DateTime? GateStart { get; set; }
        public DateTime? GateEnd { get; set; }
        public DateTime? ClaimEnd { get; set; }
        public DateTime? ClaimStart { get; set; }
        public DateTime? CounterEnd { get; set; }
        public DateTime? CounterStart { get; set; }
        public DateTime? Mcdt { get; set; }
        public DateTime? Mcat { get; set; }
        public string Nopa { get; set; }
        public string Nopc { get; set; }
        public string Totalpax { get; set; }
        public string Transit24 { get; set; }
        public string Potablewater { get; set; }
        public DateTime? AerobridgeFrom { get; set; }
        public DateTime? AerobridgeTo { get; set; }
        public DateTime? Gpu90From { get; set; }
        public DateTime? Gpu90To { get; set; }
        public DateTime? Gpu180From { get; set; }
        public DateTime? Gpu180To { get; set; }
        public DateTime? Acu45From { get; set; }
        public DateTime? Acu45To { get; set; }
        public string Qualifier { get; set; }
        public string Amsflightid { get; set; }
        public string Amslinkedflightid { get; set; }
        public string LineCode2 { get; set; }
        public string Number2 { get; set; }
        public string LineCode3 { get; set; }
        public string Number3 { get; set; }
        public string LineCode4 { get; set; }
        public string Number4 { get; set; }
        public string checkincounters { get; set; }
        public string dailyupdatestand { get; set; }
        public DateTime? ChockOn { get; set; }
        public DateTime? ChockOff { get; set; }
        public DateTime? TimeUpdate { get; set; }
        public DateTime? TimeSet { get; set; }
    }
}
