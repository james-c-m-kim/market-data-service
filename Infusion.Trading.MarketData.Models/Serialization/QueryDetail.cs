namespace Infusion.Trading.MarketData.Models.Serialization
{
    public class QueryDetail
    {
        public string count { get; set; }
        public string created { get; set; }
        public string lang { get; set; }
        public QuoteListWrapper results { get; set; }
    }

    public class HistoricalQueryDetail
    {
        public string count { get; set; }
        public string created { get; set; }
        public string lang { get; set; }
        public HistoricalQuoteListWrapper results { get; set; }
    }
}