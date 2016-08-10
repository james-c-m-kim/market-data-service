using System;
using Infusion.Trading.MarketData.CoreServices.Routes;
using Microsoft.AspNet.Mvc;

namespace trade_demo_service.Controllers
{
    [Route("trending")]
    public class TrendingStocksController : Controller
    {
        private readonly MdsRepository repository;
        private readonly int defaultMaxCount = 5;

        public TrendingStocksController(MdsRepository repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int? maxCount)
        {
            return Json(repository.GetTrendingStocks(maxCount ?? defaultMaxCount));
        }
    }
}