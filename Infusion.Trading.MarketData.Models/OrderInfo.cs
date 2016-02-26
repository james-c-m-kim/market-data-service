using System;

namespace Infusion.Trading.MarketData.Models
{
    public class OrderInfo
    {
        public string Symbol { get; set; }
        public decimal TransactionPrice { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Quantity { get; set; }
    }
}