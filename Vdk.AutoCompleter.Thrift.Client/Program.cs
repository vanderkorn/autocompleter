// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Thrift.Client
{
    using System;
    using System.Linq;
    using Autofac;
    using Vdk.AutoCompleter.Common;
    using Vdk.AutoCompleter.Common.Extensions;
    using Vdk.AutoCompleter.Common.IOC;
    using Vdk.AutoCompleter.Thrift.Client.Models;
    using Vdk.AutoCompleter.Thrift.ClientModule;

    /// <summary>
    /// Program THRIFT client.
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
            var options = new Options();

            if (CommandLine.Parser.Default.ParseArgumentsStrict(args, options))
            {
                CoreInitializer.Initialize(Dependencies);
                using (var scope = ServiceLocator.GetContainer().BeginLifetimeScope())
                {
                    Console.WriteLine("Connecting to THRIFT server {0}:{1} ...", options.Host, options.Port);
                    var app = scope.Resolve<IApplicationClient<string>>();
                    app.Connect(options.Host, options.Port);
                    Console.WriteLine("Connected to THRIFT server {0}:{1} ", options.Host, options.Port);
                    Console.WriteLine("Please send command. Format get <prefix>");
                    while (true)
                    {
                        var line = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            line = line.ToLowerInvariant().Trim().RemoveDuplicateBlankSpaces();
                            if (string.Equals(line, "exit", StringComparison.Ordinal))
                            {
                                break;
                            }

                            var arr = line.Split(' ');
                            if (arr.Length < 2 || !string.Equals(arr[0], "get", StringComparison.Ordinal))
                            {
                                continue;
                            }

                            var response = app.Get(arr[1]);
                            if (response != null)
                            {
                                if (response.Any())
                                {
                                    foreach (var val in response)
                                    {
                                        Console.WriteLine(val);
                                    }

                                    Console.WriteLine();
                                }
                            }
                        }
                    }
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
            // register THRIFT client module
            builder.RegisterModule(new ThriftApplicationClientModule());
        }
    }
}
