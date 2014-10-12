using Autofac;
using Vdk.AutoCompleter.Common;
using Vdk.AutoCompleter.Common.IOC;
using Vdk.AutoCompleter.Common.Loggers;
using Vdk.AutoCompleter.Core;
using Vdk.AutoCompleter.Thrift.Server.Models;
using Vdk.AutoCompleter.Thrift.ServerModule;

namespace Vdk.AutoCompleter.Thrift.Server
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
                using (var scope = ServiceLocator.GetContainer().BeginLifetimeScope())
                {
                    var app = scope.Resolve<IApplicationServer>();
                    app.Throughput = Throughput;
                    app.Run(options.InputFile, options.Port);
                }
            }
        }

        private static void Dependencies(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutoCompleteModule());
            builder.RegisterModule(new NLogModule());
            builder.RegisterModule(new ThriftServerApplicationModule());
        }
    }
}
