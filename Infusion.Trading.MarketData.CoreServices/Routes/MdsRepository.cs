using System;
using System.Collections.Generic;
using System.Linq;
using Infusion.Trading.MarketData.CoreServices.Contracts;
using Infusion.Trading.MarketData.Models;
using NodaTime;

namespace Infusion.Trading.MarketData.CoreServices.Routes
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
            var start = new LocalDate(request.Start.Year, request.Start.Month, request.Start.Day);
            var end = new LocalDate(request.End.Year, request.End.Month, request.End.Day);
            var period = Period.Between(start, end, PeriodUnits.Months);

            if (period.Months > 12)
            {
                // divide up into 12 month increments
                var numYrs = period.Months/12;
                if (period.Months%12 > 0)
                    numYrs++ ;

                var rangeOfDates = Enumerable.Range(start.Year, (int) numYrs + 1).Select(yr =>
                {
                    // if it's the start yr, use start month/day else use jan - dec
                    var startMonth = yr == start.Year ? start.Month : 1;
                    var startDay = yr == start.Year ? start.Day : 1;
                    var endMonth = yr == end.Year ? end.Month : 12;
                    var endDay = yr == end.Year ? end.Day : 31;

                    var rangeStart = new DateTime(yr, startMonth, startDay);
                    var rangeEnd = new DateTime(yr, endMonth, endDay);
                    return new {Start = rangeStart, End = rangeEnd};
                }).ToList();
                
                var result = rangeOfDates.AsParallel().SelectMany(parm => quoteProvider.GetQuoteHistory(request.Symbol, parm.Start, parm.End)).ToList();

                return result;
            }
            else
            {
                return quoteProvider.GetQuoteHistory(request.Symbol, request.Start, request.End);
            }
        } 

        public Quote GetBySymbol(MdsQuoteRequest request)
        {
            return quoteProvider.GetQuotes(request.Symbol).FirstOrDefault();
        }

        public string SetSubscriptionBySymbol(MdsGenericRequest request)
        {
            publishingService.SubscribeTicker(request.Symbol);
            return $"{request.Symbol} has been subscribed.";
        }

        public IList<LeaderboardItem> GetLeaderboardItems(int maxCount)
        {
            return new List<LeaderboardItem>
            {
                new LeaderboardItem
                {
                    UserFriendlyName = "Bill Gates",
                    PortfolioValue = int.MaxValue
                },
                new LeaderboardItem
                {
                    UserFriendlyName = "Steve Zuckberg",
                    PortfolioValue = 10000000000
                },
                new LeaderboardItem
                {
                    UserFriendlyName = "Steven Spielberg",
                    PortfolioValue = 100000000
                },
                new LeaderboardItem
                {
                    UserFriendlyName = "Jimmy Bob",
                    PortfolioValue = 40000000
                },
                new LeaderboardItem
                {
                    UserFriendlyName = "Billy Bob",
                    PortfolioValue = 20000000
                },

            };
        }

        public IList<Quote> GetTrendingStocks(int maxCount)
        {
            var quotes = quoteProvider.GetQuotes("MSFT", "GOOG", "GS", "MS", "YHOO");
            return quotes.ToList();
        }

        public Portfolio GetPortfolio(string userName)
        {
            return new Portfolio
            {
                CurrentHoldings = new List<HoldingInfo>
                {
                    new HoldingInfo
                    {
                        Symbol = "MSFT",
                        CurrentPrice = 100m,
                        Quantity = 4000
                    },
                    new HoldingInfo
                    {
                        Symbol = "GOOG",
                        CurrentPrice = 350m,
                        Quantity = 1000
                    },
                    new HoldingInfo
                    {
                        Symbol = "CS",
                        CurrentPrice = 14m,
                        Quantity = 5000
                    }
                },
                PastOrders = new List<OrderInfo>
                {
                    new OrderInfo
                    {
                        Symbol = "YHOO",
                        Quantity = -500,
                        TransactionPrice = 35m,
                        TransactionDate = DateTime.Now.AddDays(-15)
                    },
                    new OrderInfo
                    {
                        Symbol = "YHOO",
                        Quantity = -1500,
                        TransactionPrice = 33m,
                        TransactionDate = DateTime.Now.AddDays(-10)
                    },
                    new OrderInfo
                    {
                        Symbol = "YHOO",
                        Quantity = -2500,
                        TransactionPrice = 30m,
                        TransactionDate = DateTime.Now.AddDays(-9)
                    },
                    new OrderInfo
                    {
                        Symbol = "YHOO",
                        Quantity = -2500,
                        TransactionPrice = 25m,
                        TransactionDate = DateTime.Now.AddDays(-4)
                    },
                },
                PendingOrders = new List<OrderInfo>
                {
                    new OrderInfo
                    {
                        Symbol = "CS",
                        Quantity = -5000,
                        TransactionPrice = 12m,
                        TransactionDate = DateTime.Now.AddHours(-2)
                    },
                }
            };
        }
    }
}
