using Autofac;
using Vdk.AutoCompleter.Common;

namespace Vdk.AutoCompleter.Thrift.ClientModule
{
    public class ThriftClientApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ThriftClientApplication>()
                .As<IApplicationClient<string>>()
                .InstancePerDependency();
        }
    }
}