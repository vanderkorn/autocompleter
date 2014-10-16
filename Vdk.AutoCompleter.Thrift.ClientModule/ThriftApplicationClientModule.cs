// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThriftApplicationClientModule.cs" company="Ivan Kornilov">
//   Ivan Kornilov
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the ThriftClientApplicationModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Thrift.ClientModule
{
    using Autofac;
    using Vdk.AutoCompleter.Common;

    /// <summary>
    /// The THRIFT client AUTOFAC module.
    /// </summary>
    public class ThriftApplicationClientModule : Module
    {
        /// <summary>
        /// The load module.
        /// </summary>
        /// <param name="builder">
        /// The container builder.
        /// </param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ThriftApplicationClient>()
                .As<IApplicationClient<string>>()
                .InstancePerDependency();
        }
    }
}