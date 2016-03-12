using System.Web.ModelBinding;
using Infusion.Trading.MarketData.CoreServices.Routes;
using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.Html;
using ServiceStack.Text;

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

        public object Post(MdsOrderRequest request)
        {
            if (string.IsNullOrEmpty(request.Symbol))
            {
                var rawBody = base.Request.GetRawBody();
                request = JsonConvert.DeserializeObject<MdsOrderRequest>(rawBody);
            }

            return Repository.PostOrder(request);
        }
    }
}