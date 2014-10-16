// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WcfApplicationServer.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   The wcf server application server.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Wcf.ServerModule
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using Autofac.Integration.Wcf;
    using Microsoft.ServiceModel.Samples;
    using NLog;
    using Vdk.AutoCompleter.Common;
    using Vdk.AutoCompleter.Common.IOC;
    using Vdk.AutoCompleter.Core.Readers;
    using Vdk.AutoCompleter.Wcf.Core;

    /// <summary>
    /// The WCF server module.
    /// </summary>
    public class WcfApplicationServer : IApplicationServer
    {
        /// <summary>
        /// The reader vocabulary.
        /// </summary>
        private readonly IVocabularyReader<string> reader;

        /// <summary>
        /// The self host.
        /// </summary>
        private ServiceHost selfHost;

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfApplicationServer"/> class.
        /// </summary>
        /// <param name="reader">
        /// The reader vocabulary.
        /// </param>
        public WcfApplicationServer(IVocabularyReader<string> reader)
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
        /// The input file (vocabulary).
        /// </param>
        /// <param name="port">
        /// The server port.
        /// </param>
        public void Run(string inputFile, int port)
        {
            this.reader.AddVocabulary(File.OpenText(inputFile));
            var baseAddress = new Uri(string.Format("net.tcp://0.0.0.0:{0}/AutoCompleteWcfService", port));
        
            this.selfHost = new ServiceHost(typeof(AutoCompleteWcfService), baseAddress);
            try
            {
                var bindingElements = new List<BindingElement>();
                var tcpTransportBindingElement = new TcpTransportBindingElement();
   
                var textBindingElement = new CustomTextMessageBindingElement("us-ascii");
                bindingElements.Add(textBindingElement);
                bindingElements.Add(tcpTransportBindingElement);
                var binding = new CustomBinding(bindingElements);
                binding.ReceiveTimeout = new TimeSpan(0, 0, 120, 0);
                this.selfHost.AddServiceEndpoint(typeof(IAutoCompleteWcfService), binding, string.Empty);

                var smb = new ServiceMetadataBehavior { HttpGetEnabled = false, HttpsGetEnabled = false };
                this.selfHost.Description.Behaviors.Add(smb);

                var mexBinding = MetadataExchangeBindings.CreateMexTcpBinding();
                this.selfHost.AddServiceEndpoint(typeof(IMetadataExchange), mexBinding, "mex");

                this.selfHost.AddDependencyInjectionBehavior<IAutoCompleteWcfService>(ServiceLocator.GetContainer());

                var throttleBehavior = new ServiceThrottlingBehavior
                {
                    MaxConcurrentCalls = this.Throughput,
                    MaxConcurrentInstances = 20,
                    MaxConcurrentSessions = this.Throughput,
                };
                this.selfHost.Description.Behaviors.Add(throttleBehavior);

                this.selfHost.Open();
                Logger.Info("The WCF server is ready. Listening {0} port ...", port);
                Logger.Info("Press <ENTER> to terminate server.");
            }
            catch (CommunicationException ce)
            {
                Logger.Error("An exception occurred: {0}", ce.Message);
                this.selfHost.Abort();
            }
        }

        /// <summary>
        /// The stop server.
        /// </summary>
        public void Stop()
        {  
            try
            {
                this.selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Logger.Error("An exception occurred: {0}", ce.Message);
                this.selfHost.Abort();
            }
        }
    }
}
