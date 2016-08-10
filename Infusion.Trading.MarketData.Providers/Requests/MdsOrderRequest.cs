using Infusion.Trading.MarketData.Models;
using ServiceStack;

namespace Infusion.Trading.MarketData.CoreServices.Routes
{
    [AddHeader(ContentType = MimeTypes.Json)]
    [Route("/order", "POST", Summary = @"Add a symbol to the websocket subscription channel.")]
    public class MdsOrderRequest : IReturn<OrderInfo>
    {
        [ApiMember(Description = "The stock symbol, example: MSFT")]
        public string Symbol { get; set; }

        [ApiMember(Description = "Negative for sell, positive for buy")]
        public int Quantity { get; set; }
    }
}