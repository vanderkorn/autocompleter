using Autofac;
using Vdk.AutoCompleter.Common;


namespace Vdk.AutoCompleter.Wcf.ClientModule
{
    public class WcfClientApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WcfClientApplication>()
                .As<IApplicationClient<string>>()
                .InstancePerDependency();
        }
    }
}
