using Autofac;
using Vdk.AutoCompleter.Common;

namespace Vdk.AutoCompleter.Thrift.ServerModule
{
    public class ThriftServerApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
 
            builder.RegisterType<ThriftServerApplicationServer>()
                .As<IApplicationServer>()
                .InstancePerDependency();
        }
    }
}