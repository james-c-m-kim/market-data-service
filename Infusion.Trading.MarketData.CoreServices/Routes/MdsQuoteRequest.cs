using System.Collections.Generic;
using Infusion.Trading.MarketData.Models;
using ServiceStack;

namespace Infusion.Trading.MarketData.CoreServices.Services
{
    [Route("/quote/{Id}", "GET", Summary = @"Fetch quote details for a symbol.")]
    public class MdsQuoteRequest : IReturn<List<Quote>>
    {
        public string Id { get; set; }

        public MdsQuoteRequest(string id)
        {
            this.Id = id;
        }
    }
}