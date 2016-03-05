using Funq;
using Infusion.Trading.MarketData.CoreServices.Routes;
using Infusion.Trading.MarketData.CoreServices.Unity;
using Microsoft.Practices.Unity;
using ServiceStack;
using ServiceStack.Api.Swagger;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Configuration;
using ServiceStack.Text;

namespace Infusion.Trading.MarketData.CoreServices.Services.ServiceStack
{
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

                    //new YammerAuthProvider(appSettings)
                    //{
                    //    RedirectUrl = "http://192.168.1.154:2223",
                    //    CallbackUrl = "http://192.168.1.154:2223/auth/yammer",
                    //    ClientId = "rwKNTVw2idIza5XShMiQw",
                    //    ClientSecret = "9e9X1kpJx96mA44nsBY6flCfsnyN7fgE7s9bmQVo",
                    //}
                }));
            //Plugins.Add(new RegistrationFeature());

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
}