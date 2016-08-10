using System;
using Infusion.Trading.MarketData.CoreServices.Routes;
using Infusion.Trading.MarketData.Models;
using Microsoft.AspNet.Mvc;

namespace trade_demo_service.Controllers
{
    [Route("order")]
    public class OrderController : Controller
    {
        private readonly MdsRepository repository;
        public OrderController(MdsRepository repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            this.repository = repository;
        }

        [HttpPost]
        public OrderInfo Post([FromBody]MdsOrderRequest request)
        {
            return repository.PostOrder(request);
        }
    }
}