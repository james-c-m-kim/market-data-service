namespace Infusion.Trading.MarketData.Models.Serialization
{
    public class QuoteResponse
    {
        public QueryDetail query { get; set; }
    }

    public class HistoricalQuoteReponse
    {
        public HistoricalQueryDetail query { get; set; }
    }
}