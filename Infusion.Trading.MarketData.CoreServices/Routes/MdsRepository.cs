using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Infusion.Trading.MarketData.CoreServices.Contracts;
using Infusion.Trading.MarketData.Models;
using NodaTime;

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
            //if((request.End - request.Start).
            var start = new LocalDate(request.Start.Year, request.Start.Month, request.Start.Day);
            var end = new LocalDate(request.End.Year, request.End.Month, request.End.Day);
            var period = Period.Between(start, end, PeriodUnits.Months);

            if (period.Months > 12)
            {
                // divide up into 12 month increments
                var numYrs = period.Months/12;
                if (period.Months%12 > 0)
                    numYrs++;

                var result = Enumerable.Range(start.Year, (int) numYrs + start.Year).Select(yr =>
                {
                    // if it's the start yr, use start month/day else use jan - dec
                    var startMonth = yr == start.Year ? start.Month : 1;
                    var startDay = yr == start.Year ? start.Day : 1;
                    var endMonth = yr == end.Year ? end.Month : 12;
                    var endDay = yr == end.Year ? end.Day : 31;

                    var rangeStart = new DateTime(yr, startMonth, startDay);
                    var rangeEnd = new DateTime(yr, endMonth, endDay);
                    return new {Start = rangeStart, End = rangeEnd};
                }).AsParallel().SelectMany(parm => quoteProvider.GetQuoteHistory(request.Id, parm.Start, parm.End));

                return result.ToList();
            }
            else
            {
                return quoteProvider.GetQuoteHistory(request.Id, request.Start, request.End);
            }
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