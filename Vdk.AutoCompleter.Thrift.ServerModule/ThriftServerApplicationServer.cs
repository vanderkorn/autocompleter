// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThriftServerApplicationServer.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the ThriftServerApplicationServer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Thrift.ServerModule
{
    using System;
    using System.IO;
    using NLog;
    using global::Thrift;
    using global::Thrift.Protocol;
    using global::Thrift.Server;
    using global::Thrift.Transport;
    using Vdk.AutoCompleter.Common;
    using Vdk.AutoCompleter.Core.Readers;
    using Vdk.AutoCompleter.Thrift.Core;

    /// <summary>
    /// The THRIFT server module.
    /// </summary>
    public class ThriftServerApplicationServer : IApplicationServer
    {
        /// <summary>
        /// The reader dictionary.
        /// </summary>
        private readonly IVocabularyReader<string> reader;

        /// <summary>
        /// The THRIFT server.
        /// </summary>
        private TThreadPoolServer server;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThriftServerApplicationServer"/> class.
        /// </summary>
        /// <param name="reader">
        /// The reader dictionary.
        /// </param>
        public ThriftServerApplicationServer(IVocabularyReader<string> reader)
        {
            this.reader = reader;
            this.Throughput = 10;
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        public Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets the throughput.
        /// </summary>
        public int Throughput { get; set; }

        /// <summary>
        /// The start server.
        /// </summary>
        /// <param name="inputFile">
        /// The input file (dictionary).
        /// </param>
        /// <param name="port">
        /// The server port.
        /// </param>
        public void Run(string inputFile, int port)
        {
            this.reader.AddVocabulary(File.OpenText(inputFile));

            try
            {
                var service = new AutoCompleteServiceImplementation();
                TProcessor processor = new AutoCompleteService.Processor(service);
                TServerTransport transport = new TServerSocket(port);

                this.server = new TThreadPoolServer(
                    processor,
                    transport,
                    new TTransportFactory(),
                    new TTransportFactory(),
                    new TBinaryProtocol.Factory(),
                    new TBinaryProtocol.Factory(),
                    1,
                    this.Throughput,
                    (t) => { });

                Logger.Info("The service is ready.");
                this.server.Serve();
            }
            catch (Exception ce)
            {
                Logger.Error("An exception occurred: {0}", ce.Message);
            }
        }

        /// <summary>
        /// The stop server.
        /// </summary>
        public void Stop()
        {
            try
            {
                this.server.Stop();
            }
            catch (Exception ce)
            {
                Logger.Error("An exception occurred: {0}", ce.Message);
            }
        }
    }
}