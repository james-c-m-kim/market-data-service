using Infusion.Trading.MarketData.CoreServices.Routes;
using ServiceStack;

namespace Infusion.Trading.MarketData.CoreServices.Services.ServiceStack
{
    //[Authenticate]
    public class MdsSubService : Service
    {
        public MdsRepository Repository { get; set; }

        public object Get(MdsQuoteRequest request)
        {
            return Repository.GetBySymbol(request);
        }

        public object Get(MdsHistoricalQuoteRequest request)
        {
            return Repository.GetHistoryBySymbol(request);
        }

        public object Get(MdsGenericRequest request)
        {
            return Repository.SetSubscriptionBySymbol(request);
        }

        public object Get(MdsLeaderboardRequest request)
        {
            return Repository.GetLeaderboardItems(request.MaxCount);
        }

        public object Get(MdsTrendingRequest request)
        {
            return Repository.GetTrendingStocks(request.MaxCount);
        }

        public object Get(MdsPortfolioRequest request)
        {
            var userName = "blah";
            return Repository.GetPortfolio(userName);
        }
    }
}