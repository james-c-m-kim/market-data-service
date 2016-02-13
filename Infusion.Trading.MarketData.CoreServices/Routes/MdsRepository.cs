using System;
using System.Collections.Generic;
using System.Linq;
using Infusion.Trading.MarketData.CoreServices.Contracts;
using Infusion.Trading.MarketData.Models;

namespace Infusion.Trading.MarketData.CoreServices.Services
{
    public class MdsRepository
    {
        private readonly IQuoteService quoteService;
        private readonly IZmqService publishingService;
        private List<Quote> quotes = new List<Quote>();

        public MdsRepository(IQuoteService quoteService, IZmqService publishingService)
        {
            if (quoteService == null) throw new ArgumentNullException(nameof(quoteService));
            if (publishingService == null) throw new ArgumentNullException(nameof(publishingService));

            this.quoteService = quoteService;
            this.publishingService = publishingService;
        }

        public Quote GetBySymbol(MdsQuoteRequest request)
        {
            return quoteService.GetQuotes(request.Id).FirstOrDefault();
        }

        public string SubscribeBySymbol(MdsGenericRequest request)
        {
            publishingService.SubscribeTicker(request.Id);
            return $"{request.Id} has been subscribed.";
        }
    }
}