using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Infusion.Trading.CoreService
{
    public partial class BaseService : ServiceBase
    {
        public BaseService()
        {
            InitializeComponent();
            //nfusionBootstrapper.Instance.Container.Resolve<MarketDataService>().Start();
            for (;;) { }

        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
