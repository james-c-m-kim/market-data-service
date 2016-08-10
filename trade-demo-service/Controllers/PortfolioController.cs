using System;
using Infusion.Trading.MarketData.CoreServices.Routes;
using Infusion.Trading.MarketData.Models;
using Microsoft.AspNet.Mvc;

namespace trade_demo_service.Controllers
{
    [Route("portfolio")]
    public class PortfolioController : Controller
    {
        private readonly MdsRepository repository;

        public PortfolioController(MdsRepository repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            this.repository = repository;
        }

        [HttpGet]
        public Portfolio Get([FromQuery] string userName)
        {
            return repository.GetPortfolio(userName);
        }
    }
}