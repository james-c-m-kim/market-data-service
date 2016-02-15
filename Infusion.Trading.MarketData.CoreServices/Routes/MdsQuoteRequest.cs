using System;
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

    [Route("/history/{Id}/{Start}/{End}", "GET", Summary = @"Fetch quote history details for a symbol.")]
    public class MdsHistoricalQuoteRequest : IReturn<IList<HistoricalQuote>>
    {
        public string Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public MdsHistoricalQuoteRequest(string id)
        {
            this.Id = id;
        }
    }
}