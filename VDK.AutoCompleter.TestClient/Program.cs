using System;
using System.IO;
using Autofac;
using VDK.AutoCompleter.Common.IOC;
using VDK.AutoCompleter.Core;
using VDK.AutoCompleter.TestClient.Models;
using VDK.AutoCompleter.TestClient.Services;

namespace VDK.AutoCompleter.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            CoreInitializer.Initialize(Dependencies);
            var options = new Options();
            // Parse in 'strict mode', success or quit
            if (CommandLine.Parser.Default.ParseArgumentsStrict(args, options))
            {
                using (var lifetime = ServiceLocator.GetContainer().BeginLifetimeScope())
                {
                    var module = lifetime.Resolve<IBatchPrefixService>();
               
                    if (!string.IsNullOrWhiteSpace(options.InputFile))
                    {
                        module.Run(File.OpenText(options.InputFile), Console.Out);
                    }
                    else
                    {
                        module.Run(Console.In, Console.Out);
                    }
                }
              
            }
        }

        private static void Dependencies(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutoCompleteModule());
        }
    }
}
