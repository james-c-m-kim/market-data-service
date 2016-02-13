using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Funq;
using Infusion.Trading.MarketData.CoreServices.Unity;
using Infusion.Trading.MarketData.Models.Util;
using ServiceStack;
using ServiceStack.Api.Swagger;


namespace Infusion.Trading.MarketData.CoreServices.Services
{
    public class SStackService
    {
        public void Start()
        {
            var hostname = Dns.GetHostName();
            var uriList = Dns.GetHostAddresses(hostname);

            if (uriList.All(u => u.AddressFamily != AddressFamily.InterNetwork)) return;

            var ipAddress = uriList.First(u => u.AddressFamily == AddressFamily.InterNetwork);
            var startUrl = $"http://{ipAddress.ToString()}:{MarketDataSettings.RestServicePort}/";
            var appHost = InfusionBootstrapper.Instance.Container.Resolve<AppHost>();
            appHost.Init();
            appHost.Start(startUrl);
            Console.WriteLine($"REST services started on: {startUrl}");
        }
    }

    public class AppHost : AppHostHttpListenerBase
    {
        public AppHost() : base("MdsService", typeof (MdsQuoteRequest).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            container.Register(InfusionBootstrapper.Instance.Container.Resolve<MdsRepository>());
            Plugins.Add(new SwaggerFeature());
            SetConfig(new HostConfig
            {
                EnableFeatures = Feature.All.Remove(Feature.Metadata),
                DefaultRedirectPath = "/swagger-ui"
            });
        }
    }

    public class MdsSubService : Service
    {
        public MdsRepository Repository { get; set; }

        public object Get(MdsQuoteRequest request)
        {
            return Repository.GetBySymbol(request);
        }

        public object Get(MdsGenericRequest request)
        {
            return Repository.SubscribeBySymbol(request);
        }
    }
}