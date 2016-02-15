using System;
using System.Collections.Generic;
using System.Linq;
using Infusion.Trading.MarketData.CoreServices.Contracts;
using Infusion.Trading.MarketData.Models;

namespace Infusion.Trading.MarketData.CoreServices.Services
{
    public class MdsRepository
    {
        private readonly IQuoteProvider quoteProvider;
        private readonly IZmqService publishingService;
        private List<Quote> quotes = new List<Quote>();

        public MdsRepository(IQuoteProvider quoteProvider, IZmqService publishingService)
        {
            if (quoteProvider == null) throw new ArgumentNullException(nameof(quoteProvider));
            if (publishingService == null) throw new ArgumentNullException(nameof(publishingService));

            this.quoteProvider = quoteProvider;
            this.publishingService = publishingService;
        }


        public IList<HistoricalQuote> GetHistoryBySymbol(MdsHistoricalQuoteRequest request)
        {
            return quoteProvider.GetQuoteHistory(request.Id, request.Start, request.End);
        } 

        public Quote GetBySymbol(MdsQuoteRequest request)
        {
            return quoteProvider.GetQuotes(request.Id).FirstOrDefault();
        }

        public string SubscribeBySymbol(MdsGenericRequest request)
        {
            publishingService.SubscribeTicker(request.Id);
            return $"{request.Id} has been subscribed.";
        }
    }
}