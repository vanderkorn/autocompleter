using System;
using Autofac;

namespace VDK.AutoCompleter.Common.IOC
{
    /// <summary>
    /// Регистрация базовых зависимостей
    /// </summary>
    public static class CoreInitializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrations"></param>
        public static void  Initialize(Action<ContainerBuilder> registrations)
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new ResolveDependenciesSource());
      
            registrations(builder);

            var container = builder.Build(); 
            ServiceLocator.SetContainer(container);
        }
    }
}
