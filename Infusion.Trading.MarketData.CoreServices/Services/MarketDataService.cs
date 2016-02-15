using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infusion.Trading.MarketData.CoreServices.Contracts;

namespace Infusion.Trading.MarketData.CoreServices.Services
{
    public class MarketDataService
    {
        private readonly FleckService fleckService;
        //private readonly NancyService nancyService;
        private readonly SStackService svcStackService;

        public MarketDataService(FleckService fleckService, 
            //NancyService nancyService, 
            SStackService svcStackService)
        {
            if (fleckService == null) throw new ArgumentNullException(nameof(fleckService));
            //if (nancyService == null) throw new ArgumentNullException(nameof(nancyService));
            if (svcStackService == null) throw new ArgumentNullException(nameof(svcStackService));

            this.fleckService = fleckService;
            //this.nancyService = nancyService;
            this.svcStackService = svcStackService;
        }

        public void Start()
        {
            fleckService.Start();
            svcStackService.Start();
            //nancyService.Start();
        }
    }
}
