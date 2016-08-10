using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infusion.Trading.MarketData.CoreServices.Routes;
using Infusion.Trading.MarketData.Models;
using Microsoft.AspNet.Mvc;

namespace trade_demo_service.Controllers
{
    [Route("quotes")]
    public class QuotesController : Controller
    {
        private readonly MdsRepository repository;

        public QuotesController(MdsRepository repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            this.repository = repository;
        }

        // GET quotes/5
        [HttpGet("{id}")]
        public Quote Get(string id)
        {
            var quoteRequest = new MdsQuoteRequest(id);
            var result = repository.GetBySymbol(quoteRequest);
            return result;
        }

        [HttpGet("history")]
        public IActionResult GetHistory([FromQuery]string id, [FromQuery]DateTime start, [FromQuery]DateTime end)
        {
            var historyRequest = new MdsHistoricalQuoteRequest(id);

            if (start > DateTime.MinValue) historyRequest.Start = start;
            if (end > DateTime.MinValue) historyRequest.End = end;

            var result = repository.GetHistoryBySymbol(historyRequest);
            return Json(result);
        }

        // POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
