using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Builder;
using Autofac.Core;

namespace VDK.AutoCompleter.Common.IOC
{
    /// <summary>
    /// јвтоматическа€ регистраци€ зависимостей
    /// </summary>
    public class ResolveDependenciesSource : IRegistrationSource
    {
        #region Implementation of IRegistrationSource

        /// <summary>
        /// Retrieve registrations for an unregistered service, to be used
        ///             by the container.
        /// </summary>
        /// <param name="service">The service that was requested.</param><param name="registrationAccessor">A function that will return existing registrations for a service.</param>
        /// <returns>
        /// Registrations providing the service.
        /// </returns>
        /// <remarks>
        /// If the source is queried for service s, and it returns a component that implements both s and s', then it
        ///             will not be queried again for either s or s'. This means that if the source can return other implementations
        ///             of s', it should return these, plus the transitive closure of other components implementing their 
        ///             additional services, along with the implementation of s. It is not an error to return components
        ///             that do not implement <paramref name="service"/>.
        /// </remarks>
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            var ts = service as TypedService;
            if (ts != null && typeof(IDependency).IsAssignableFrom(ts.ServiceType))
            {
                var classType = ts.ServiceType.Assembly.GetTypes().FirstOrDefault(t => ts.ServiceType.IsAssignableFrom(t) && t.IsClass);
                if (classType != null)
                {
                    var registration = RegistrationBuilder.ForType(classType).InstancePerLifetimeScope();

                    registration = registration.As(ts.ServiceType);
                    if (typeof (ISingletonDependency).IsAssignableFrom(ts.ServiceType))
                    {
                        registration = registration.SingleInstance();
                    }

                    yield return registration.CreateRegistration();
                }
            }
        }

        /// <summary>
        /// Gets whether the registrations provided by this source are 1:1 adapters on top
        ///             of other components (I.e. like Meta, Func or Owned.)
        /// </summary>
        public bool IsAdapterForIndividualComponents
        {
            get { return false; }
        }

        #endregion
    }
}
