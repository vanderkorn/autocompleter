using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Vdk.AutoCompleter.Common;
using Vdk.AutoCompleter.Core.Services;
using Vdk.AutoCompleter.Wcf.Core;

namespace Vdk.AutoCompleter.Wcf.ServerModule
{
    public class WcfServerApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new AutoCompleteWcfService(c.Resolve<IAutoCompleteService<string>>())).As<IAutoCompleteWcfService>();
            builder.RegisterType<WcfServerApplicationServer>()
                .As<IApplicationServer>()
                .InstancePerDependency();
        }
    }
}
