namespace Infusion.Trading.MarketData.Models
{
    public class HoldingInfo
    {
        public string Symbol { get; set; }
        public int Quantity { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}