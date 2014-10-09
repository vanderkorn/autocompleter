using System;
using System.IO;
using Autofac;
using Thrift;
using Thrift.Server;
using Thrift.Transport;
using Vdk.AutoCompleter.Common.IOC;
using Vdk.AutoCompleter.Core;
using Vdk.AutoCompleter.Core.Readers;
using Vdk.AutoCompleter.Thrift.Core;
using Vdk.AutoCompleter.Thrift.Server.Models;

namespace Vdk.AutoCompleter.Thrift.Server
{
    class Program
    {
        private const int Throughput = 10;
        static void Main(string[] args)
        {
            CoreInitializer.Initialize(Dependencies);
            var options = new Options();
            // Parse in 'strict mode', success or quit
            if (CommandLine.Parser.Default.ParseArgumentsStrict(args, options))
            {
                using (var lifetime = ServiceLocator.GetContainer().BeginLifetimeScope())
                {
                    var reader = lifetime.Resolve<IVocabularyReader<string>>();
                    reader.AddVocabulary(File.OpenText(options.InputFile));
                   
                    try
                    {

                        var service = new AutoCompleteServiceImplementation();
                        TProcessor processor = new AutoCompleteService.Processor(service);
                        TServerTransport transport = new TServerSocket(options.Port, 10000);

                        //var server = new TSimpleServer(processor, transport);
                        var server = new TThreadPoolServer(processor, transport);

                        Console.WriteLine("The service is ready.");
                        server.Serve();

                        //Console.WriteLine("The service is ready.");
                        //Console.WriteLine("Press <ENTER> to terminate service.");
                        //Console.WriteLine();
                        //Console.ReadLine();

                        // Close the ServiceHostBase to shutdown the service.
                        server.Stop();
                    }
                    catch (Exception ce)
                    {
                        Console.WriteLine("An exception occurred: {0}", ce.Message);
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
