// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoreInitializer.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Регистрация базовых зависимостей
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Common.IOC
{
    using System;
    using Autofac;

    /// <summary>
    /// Registration the basic dependencies
    /// </summary>
    public static class CoreInitializer
    {
        /// <summary>
        /// The initialize dependencies.
        /// </summary>
        /// <param name="registrations">
        /// Container builder
        /// </param>
        public static void Initialize(Action<ContainerBuilder> registrations)
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new ResolveDependenciesSource());
            registrations(builder);
            var container = builder.Build(); 
            ServiceLocator.SetContainer(container);
        }
    }
}
