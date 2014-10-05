using System;
using System.Collections.Generic;
using Autofac;

namespace VDK.AutoCompleter.Common.IOC
{
    /// <summary>
    /// 
    /// </summary>
    public static class AutofacExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TService> ResolveAll<TService>(this ILifetimeScope scope)
        {
            Type type = typeof(IEnumerable<>).MakeGenericType(new[] { typeof(TService) });
            return (IEnumerable<TService>)scope .Resolve(type);
        }
      
    }
}