using System;
using System.Collections.Generic;
using Infusion.Trading.MarketData.Models;
using ServiceStack;

namespace Infusion.Trading.MarketData.CoreServices.Routes
{
    [AddHeader(ContentType = MimeTypes.Json)]
    [Route("/history/{Symbol}/{Start}/{End}", "GET", Summary = @"Fetch quote history details for a symbol.")]
    public class MdsHistoricalQuoteRequest : IReturn<IList<HistoricalQuote>>
    {
        [ApiMember(IsRequired = true)]
        public string Symbol { get; set; }
        [ApiMember(Description = "(YYYY-MM-DD)", IsRequired = true)]
        public DateTime Start { get; set; }
        [ApiMember(Description = "(YYYY-MM-DD)", IsRequired = true)]
        public DateTime End { get; set; }

        public MdsHistoricalQuoteRequest(string symbol)
        {
            this.Symbol = symbol;
            Start = DateTime.Today.AddDays(-1);
            End = DateTime.Today;
        }
    }
}