// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WcfApplicationClientModule.cs" company="Ivan Kornilov">
//   Ivan Kornilov
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the WcfClientApplicationModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Wcf.ClientModule
{
    using Autofac;
    using Vdk.AutoCompleter.Common;

    /// <summary>
    /// The WCF client AUTOFAC module.
    /// </summary>
    public class WcfApplicationClientModule : Module
    {
        /// <summary>
        /// The load module.
        /// </summary>
        /// <param name="builder">
        /// The container builder.
        /// </param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WcfApplicationClient>()
                .As<IApplicationClient<string>>()
                .InstancePerDependency();
        }
    }
}
