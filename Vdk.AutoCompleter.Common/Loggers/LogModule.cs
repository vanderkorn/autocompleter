// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogModule.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the LogModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Common.Loggers
{
    using System;
    using System.Linq;
    using Autofac;
    using Autofac.Core;

    /// <summary>
    /// The log module.
    /// </summary>
    /// <typeparam name="TLogger">
    /// Type of logger
    /// </typeparam>
    public abstract class LogModule<TLogger> : Module
    {
        /// <summary>
        /// The create logger.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="TLogger"/>.
        /// </returns>
        protected abstract TLogger CreateLoggerFor(Type type);

        /// <summary>
        /// The attach to component registration.
        /// </summary>
        /// <param name="componentRegistry">
        /// The component registry.
        /// </param>
        /// <param name="registration">
        /// The registration.
        /// </param>
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            var type = registration.Activator.LimitType;
            if (this.HasPropertyDependencyOnLogger(type))
            {
                registration.Activated += this.InjectLoggerViaProperty;
            }

            if (this.HasConstructorDependencyOnLogger(type))
            {
                registration.Preparing += this.InjectLoggerViaConstructor;
            }
        }

        /// <summary>
        /// The has property dependency on logger.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool HasPropertyDependencyOnLogger(Type type)
        {
            return type.GetProperties().Any(property => property.CanWrite && property.PropertyType == typeof(TLogger));
        }

        /// <summary>
        /// The has constructor dependency on logger.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool HasConstructorDependencyOnLogger(Type type)
        {
            return type.GetConstructors()
                       .SelectMany(constructor => constructor.GetParameters()
                                                             .Where(parameter => parameter.ParameterType == typeof(TLogger)))
                       .Any();
        }

        /// <summary>
        /// The inject logger via property.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="event">
        /// The event.
        /// </param>
        private void InjectLoggerViaProperty(object sender, ActivatedEventArgs<object> @event)
        {
            var type = @event.Instance.GetType();
            var propertyInfo = type.GetProperties().First(x => x.CanWrite && x.PropertyType == typeof(TLogger));
            propertyInfo.SetValue(@event.Instance, this.CreateLoggerFor(type), null);
        }

        /// <summary>
        /// The inject logger via constructor.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="event">
        /// The event.
        /// </param>
        private void InjectLoggerViaConstructor(object sender, PreparingEventArgs @event)
        {
            var type = @event.Component.Activator.LimitType;
            @event.Parameters = @event.Parameters.Union(new[]
            {
                new ResolvedParameter((parameter, context) => parameter.ParameterType == typeof(TLogger), (p, i) => this.CreateLoggerFor(type))
            });
        }
    }
}