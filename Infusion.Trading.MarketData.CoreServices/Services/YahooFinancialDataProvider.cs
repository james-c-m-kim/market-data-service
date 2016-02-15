using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Infusion.Trading.MarketData.CoreServices.Contracts;
using Infusion.Trading.MarketData.Models;
using Infusion.Trading.MarketData.Models.Serialization;
using Newtonsoft.Json;
using ServiceStack;

namespace Infusion.Trading.MarketData.CoreServices.Services
{
    public class YahooFinancialDataProvider : IQuoteProvider
    {
        private static string urlString = "http://query.yahooapis.com/";
        private readonly Uri baseUrl = new Uri(urlString);

        public IList<Quote> GetQuotes(params string[] securityIds)
        {
            // quote details
            // http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20(%22MSFT%22%2C%22GOOG%22%2C%22AAPL%22)&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&format=json

            if (securityIds == null || !securityIds.Any()) return new List<Quote>();

            var securityIdsAsString = string.Join("%2C", securityIds.Select(s => $"\"{s}\""));
            var selectSql = $"select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20({securityIdsAsString})";
            var queryUri = $"v1/public/yql?q={selectSql}&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&format=json";

            var client = new JsonServiceClient(urlString);
            var quoteResponse = client.Get<QuoteResponse>(queryUri);

            var asOf = DateTime.Now;
            var result = quoteResponse.query.results.quote.Select(q =>
            {
                q.AsOf = asOf;
                q.LastTradeDateAsString = q.LastTradeDate?.ToString("MM/dd/yyyy") + " " + q.LastTradeTime;
                return q;
            }).ToList();

            return result;
        }

        public IList<HistoricalQuote> GetQuoteHistory(string securityId, DateTime start, DateTime end)
        {
            // historical - can only get max 1 yr at a time so do this 5 times to get the data (5 threads, join @ end)
            // https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.historicaldata%20where%20symbol%20%3D%20%22GS%22%20and%20startDate%20%3D%20%222015-02-12%22%20and%20endDate%20%3D%20%222016-02-12%22&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=

            var startStr = start.ToString("yyyy-MM-dd");
            var endStr = end.ToString("yyyy-MM-dd");
            var selectSql =
                $"select%20*%20from%20yahoo.finance.historicaldata%20where%20symbol%20%3D%20%22{securityId}%22%20and%20startDate%20%3D%20%22{startStr}%22%20and%20endDate%20%3D%20%22{endStr}%22";
            var queryUri = $"/v1/public/yql?q={selectSql}&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=";

            var client = new JsonServiceClient(urlString);
            var quoteResponse = client.Get<HistoricalQuoteReponse>(queryUri);
            var asOf = DateTime.Now;
            var result = quoteResponse.query.results.quote.Select(q =>
            {
                q.AsOf = asOf;
                return q;
            }).ToList();

            return result;
        }
    }
}
