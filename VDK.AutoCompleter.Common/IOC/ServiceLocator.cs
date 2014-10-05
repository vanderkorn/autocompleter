using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;

namespace VDK.AutoCompleter.Common.IOC
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceLocator
    {
        private static IContainer _container;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public static void SetContainer(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService Resolve<TService>(params Parameter[] parameters)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");
            return _container.Resolve<TService>(parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TService> ResolveAll<TService>()
        {
            Type type = typeof(IEnumerable<>).MakeGenericType(new[] { typeof(TService) });
            return (IEnumerable<TService>)_container.Resolve(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IContainer GetContainer(){
            return _container;
        }

    }
}
