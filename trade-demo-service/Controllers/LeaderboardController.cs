using System;
using Infusion.Trading.MarketData.CoreServices.Routes;
using Microsoft.AspNet.Mvc;

namespace trade_demo_service.Controllers
{
    [Route("leaderboard")]
    public class LeaderboardController : Controller
    {
        private readonly MdsRepository repository;
        private readonly int defaultMaxCount = 5;

        public LeaderboardController(MdsRepository repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]int? maxCount)
        {
            return Json(repository.GetLeaderboardItems(maxCount ?? defaultMaxCount));
        }
    }
}