// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThriftServerApplicationModule.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the ThriftServerApplicationModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Thrift.ServerModule
{
    using Autofac;
    using Vdk.AutoCompleter.Common;

    /// <summary>
    /// The THRIFT server AUTOFAC module.
    /// </summary>
    public class ThriftServerApplicationModule : Module
    {
        /// <summary>
        /// The load module.
        /// </summary>
        /// <param name="builder">
        /// The container builder.
        /// </param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ThriftServerApplicationServer>()
                .As<IApplicationServer>()
                .InstancePerDependency();
        }
    }
}