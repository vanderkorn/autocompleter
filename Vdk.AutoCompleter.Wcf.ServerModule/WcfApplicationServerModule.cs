// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WcfApplicationServerModule.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the WcfServerApplicationModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Wcf.ServerModule
{
    using Autofac;
    using Vdk.AutoCompleter.Common;
    using Vdk.AutoCompleter.Core.Services;
    using Vdk.AutoCompleter.Wcf.Core;

    /// <summary>
    /// The WCF server AUTOFAC module.
    /// </summary>
    public class WcfApplicationServerModule : Module
    {
        /// <summary>
        /// The load module.
        /// </summary>
        /// <param name="builder">
        /// The container builder.
        /// </param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new AutoCompleteWcfService(c.Resolve<IAutoCompleteService<string>>())).As<IAutoCompleteWcfService>();
            builder.RegisterType<WcfApplicationServer>()
                .As<IApplicationServer>()
                .InstancePerDependency();
        }
    }
}
