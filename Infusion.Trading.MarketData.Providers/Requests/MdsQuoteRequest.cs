using System.Collections.Generic;
using Infusion.Trading.MarketData.Models;
using ServiceStack;

namespace Infusion.Trading.MarketData.CoreServices.Routes
{
    [AddHeader(ContentType = MimeTypes.Json)]
    [Route("/quote/{Symbol}", "GET", Summary = @"Fetch quote details for a symbol.")]
    public class MdsQuoteRequest : IReturn<List<Quote>>
    {
        public string Symbol { get; set; }

        public MdsQuoteRequest(string symbol)
        {
            this.Symbol = symbol;
        }
    }

    // return null if user is not AUTH'd
    // https://www.yammer.com/api/v1/users/current.json

    // the auth url which should bring user back to us when done
    // https://www.yammer.com/oauth2/authorize?client_id=rwKNTVw2idIza5XShMiQw&redirect_uri={redirect_url}
}