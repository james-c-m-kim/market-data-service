using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Infusion.Trading.MarketData.CoreServices.Unity;
using Infusion.Trading.MarketData.Models.Util;
using Microsoft.Practices.Unity;

namespace Infusion.Trading.MarketData.CoreServices.Services.ServiceStack
{
    public class SStackService
    {
        public void Start()
        {
            var hostname = Dns.GetHostName();
            var uriList = Dns.GetHostAddresses(hostname);

            if (uriList.All(u => u.AddressFamily != AddressFamily.InterNetwork)) return;

            var ipAddress = uriList.First(u => u.AddressFamily == AddressFamily.InterNetwork);
            //var startUrl = $"http://{ipAddress.ToString()}:{MarketDataSettings.RestServicePort}/";
            var startUrl = $"http://localhost:{MarketDataSettings.RestServicePort}/";
            var appHost = InfusionBootstrapper.Instance.Container.Resolve<AppHost>();
            appHost.Init();
            appHost.Start(startUrl);
            Console.WriteLine($"REST services started on: {startUrl}");
        }
    }
}
