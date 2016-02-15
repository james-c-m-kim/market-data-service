using ServiceStack;

namespace Infusion.Trading.MarketData.CoreServices.Services
{
    [AddHeader(ContentType = MimeTypes.Json)]
    [Route("/subscribe/{Id}", "GET", Summary = @"Add a symbol to the websocket subscription channel.")]
    public class MdsGenericRequest : IReturn<string>
    {
        public string Id { get; set; }

        public MdsGenericRequest(string id)
        {
            this.Id = id;
        }
    }
}