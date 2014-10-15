// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.TestClient
{
    using System;
    using System.IO;
    using Autofac;
    using Vdk.AutoCompleter.Common;
    using Vdk.AutoCompleter.Common.IOC;
    using Vdk.AutoCompleter.Core;
    using Vdk.AutoCompleter.TestClient.Models;
    using Vdk.AutoCompleter.TestClient.Services;

    /// <summary>
    /// Client application to solve the basic problem of finding autocomplete words.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            CoreInitializer.Initialize(Dependencies);
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArgumentsStrict(args, options))
            {
                using (var lifetime = ServiceLocator.GetContainer().BeginLifetimeScope())
                {
                    var module = lifetime.Resolve<IApplicationTestClient<string>>();
                    module.Run(!string.IsNullOrWhiteSpace(options.InputFile) ? File.OpenText(options.InputFile) : Console.In, Console.Out);
                }
            }
        }

        /// <summary>
        /// The register dependencies.
        /// </summary>
        /// <param name="builder">
        /// The IOC container builder.
        /// </param>
        private static void Dependencies(ContainerBuilder builder)
        {
            // register autocomplete module
            builder.RegisterModule(new AutoCompleteModule());

            // register test client module
            builder.RegisterGeneric(typeof(ApplicationTestClient<>))
             .As(typeof(IApplicationTestClient<>))
             .InstancePerDependency();
        }
    }
}
