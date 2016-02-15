using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infusion.Trading.MarketData.Models;

namespace Infusion.Trading.MarketData.CoreServices.Contracts
{
    public interface IQuoteProvider
    {
        IList<Quote> GetQuotes(params string[] securityIds);
        IList<HistoricalQuote> GetQuoteHistory(string securityId, DateTime start, DateTime end);
    }
}
