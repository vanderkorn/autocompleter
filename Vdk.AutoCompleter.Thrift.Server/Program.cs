// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Thrift.Server
{
    using System;
    using Autofac;
    using Vdk.AutoCompleter.Common;
    using Vdk.AutoCompleter.Common.IOC;
    using Vdk.AutoCompleter.Common.Loggers;
    using Vdk.AutoCompleter.Core;
    using Vdk.AutoCompleter.Thrift.Server.Models;
    using Vdk.AutoCompleter.Thrift.ServerModule;

    /// <summary>
    /// Program THRIFT-server.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The throughput.
        /// </summary>
        private const int Throughput = 10;

        /// <summary>
        /// The main method.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            var options = new Options();
         
            if (CommandLine.Parser.Default.ParseArgumentsStrict(args, options))
            {
                try
                {
                    CoreInitializer.Initialize(Dependencies);
                    using (var scope = ServiceLocator.GetContainer().BeginLifetimeScope())
                    {
                        var app = scope.Resolve<IApplicationServer>();
                        app.Throughput = Throughput;
                        app.Run(options.InputFile, options.Port);
                        Console.ReadLine();
                    }
                }
                catch (Exception ce)
                {
                    Console.WriteLine("An exception occurred: {0}", ce.Message);
                }
            }
        }

        /// <summary>
        /// The dependencies.
        /// </summary>
        /// <param name="builder">
        /// The IOC container builder.
        /// </param>
        private static void Dependencies(ContainerBuilder builder)
        {
            // register autocomplete module
            builder.RegisterModule(new AutoCompleteModule());

            // register logger
            builder.RegisterModule(new NLogModule());

            // register THRIFT server module
            builder.RegisterModule(new ThriftServerApplicationModule());
        }
    }
}
