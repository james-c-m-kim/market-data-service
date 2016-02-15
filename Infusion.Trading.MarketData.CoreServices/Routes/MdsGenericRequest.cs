using ServiceStack;

namespace Infusion.Trading.MarketData.CoreServices.Services
{
    [AddHeader(ContentType = MimeTypes.Json)]
    [Route("/subscribe/{Symbol}", "GET", Summary = @"Add a symbol to the websocket subscription channel.")]
    public class MdsGenericRequest : IReturn<string>
    {
        public string Symbol { get; set; }

        public MdsGenericRequest(string symbol)
        {
            this.Symbol = symbol;
        }
    }
}