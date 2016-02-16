using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OpenId.Provider;
using Microsoft.Practices.Unity;
using Funq;
using Infusion.Trading.MarketData.CoreServices.Unity;
using Infusion.Trading.MarketData.Models.Util;
using ServiceStack;
using ServiceStack.Api.Swagger;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Configuration;
using ServiceStack.Text;


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
            var appSettings = new AppSettings();
            container.Register(InfusionBootstrapper.Instance.Container.Resolve<MdsRepository>());
            JsConfig.DateHandler = DateHandler.ISO8601;
            Plugins.Add(new SwaggerFeature());
            Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                new IAuthProvider[]
                {
                    new YammerAuthProvider(appSettings)
                    {
                        RedirectUrl = "http://192.168.1.154:2223",
                        CallbackUrl = "http://192.168.1.154:2223/auth/yammer",
                        ClientId = "rwKNTVw2idIza5XShMiQw",
                        ClientSecret = "9e9X1kpJx96mA44nsBY6flCfsnyN7fgE7s9bmQVo",
                    }
                }));
            Plugins.Add(new RegistrationFeature());

            container.Register<ICacheClient>(new MemoryCacheClient());
            var userRep = new InMemoryAuthRepository();
            container.Register<IUserAuthRepository>(userRep);
            SetConfig(new HostConfig
            {
               
                DefaultContentType = MimeTypes.Json,
                DefaultRedirectPath = "/swagger-ui"
            });
        }
    }

    [Authenticate]
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
            return Repository.SubscribeBySymbol(request);
        }
    }
}
