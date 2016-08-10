using Infusion.Trading.MarketData.Models;
using ServiceStack;

namespace Infusion.Trading.MarketData.CoreServices.Routes
{
    [AddHeader(ContentType = MimeTypes.Json)]
    [Route("/portfolio", "GET", Summary = @"Fetch the current user's portfolio details.")]
    public class MdsPortfolioRequest : IReturn<Portfolio>
    {
    }
}