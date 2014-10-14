// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceLocator.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the ServiceLocator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Common.IOC
{
    using System;
    using System.Collections.Generic;
    using Autofac;
    using Autofac.Core;

    /// <summary>
    /// The service locator.
    /// </summary>
    public class ServiceLocator
    {
        /// <summary>
        /// The container.
        /// </summary>
        private static IContainer container;

        /// <summary>
        /// The set container.
        /// </summary>
        /// <param name="inContainer">
        /// The container.
        /// </param>
        public static void SetContainer(IContainer inContainer)
        {
            container = inContainer;
        }

        /// <summary>
        /// The resolve service.
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <typeparam name="TService">
        /// Type of service.
        /// </typeparam>
        /// <returns>
        /// The <see cref="TService"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Argument null exception.
        /// </exception>
        public static TService Resolve<TService>(params Parameter[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            return container.Resolve<TService>(parameters);
        }

        /// <summary>
        /// The resolve all services.
        /// </summary>
        /// <typeparam name="TService">
        /// Type of service.
        /// </typeparam>
        /// <returns>
        /// The services.
        /// </returns>
        public static IEnumerable<TService> ResolveAll<TService>()
        {
            var type = typeof(IEnumerable<>).MakeGenericType(new[] { typeof(TService) });
            return (IEnumerable<TService>)container.Resolve(type);
        }

        /// <summary>
        /// The get container.
        /// </summary>
        /// <returns>
        /// The <see cref="IContainer"/>.
        /// </returns>
        public static IContainer GetContainer()
        {
            return container;
        }
    }
}