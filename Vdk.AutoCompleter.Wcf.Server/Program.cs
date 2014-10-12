using System;
using Autofac;
using Vdk.AutoCompleter.Common;
using Vdk.AutoCompleter.Common.IOC;
using Vdk.AutoCompleter.Common.Loggers;
using Vdk.AutoCompleter.Core;
using Vdk.AutoCompleter.Wcf.Server.Models;
using Vdk.AutoCompleter.Wcf.ServerModule;

namespace Vdk.AutoCompleter.Wcf.Server
{
    class Program
    {
        private const int Throughput = 10;
        static void Main(string[] args)
        {
            var options = new Options();
            // Parse in 'strict mode', success or quit
            if (CommandLine.Parser.Default.ParseArgumentsStrict(args, options))
            {   
                CoreInitializer.Initialize(Dependencies);
                try
                {
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

        private static void Dependencies(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutoCompleteModule());
            builder.RegisterModule(new NLogModule());
            builder.RegisterModule(new WcfServerApplicationModule());
        }
    }
}