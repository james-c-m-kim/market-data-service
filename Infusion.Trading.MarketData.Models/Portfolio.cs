using System.Collections.Generic;

namespace Infusion.Trading.MarketData.Models
{
    public class Portfolio
    {
        public IList<HoldingInfo> CurrentHoldings { get; set; }
        public IList<OrderInfo> PendingOrders { get; set; }
        public IList<OrderInfo> PastOrders { get; set; }
    }
}