using System;
using System.IO;
using NLog;
using Thrift;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;
using Vdk.AutoCompleter.Common;
using Vdk.AutoCompleter.Core.Readers;
using Vdk.AutoCompleter.Thrift.Core;

namespace Vdk.AutoCompleter.Thrift.ServerModule
{
    public class ThriftServerApplicationServer : IApplicationServer
    {
        private readonly IVocabularyReader<string> _reader;

        public ThriftServerApplicationServer(IVocabularyReader<string> reader)
        {
            _reader = reader;
            Throughput = 10;
        }

        public Logger Logger { get; set; }
        public int Throughput { get; set; }

        public void Run(string inputFile, int port)
        {
            _reader.AddVocabulary(File.OpenText(inputFile));

            try
            {

                var service = new AutoCompleteServiceImplementation();
                TProcessor processor = new AutoCompleteService.Processor(service);
                TServerTransport transport = new TServerSocket(port);

                //var server = new TSimpleServer(processor, transport);
                var server = new TThreadPoolServer(processor, transport,
                    new TTransportFactory(),
                    new TTransportFactory(),
                    new TBinaryProtocol.Factory(),
                    new TBinaryProtocol.Factory(),
                    1,
                    Throughput,
                    (t) =>
                    {

                    });

                Logger.Info("The service is ready.");
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
                Logger.Error("An exception occurred: {0}", ce.Message);
            }
        }
    }
}