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

namespace Vdk.AutoCompleter.Wcf.ServerModule
{
    public class WcfServerApplicationServer : IApplicationServer
    {
        private readonly IVocabularyReader<string> _reader;
        private ServiceHost _selfHost;

        public WcfServerApplicationServer(IVocabularyReader<string> reader)
        {
            _reader = reader;
            Throughput = 10;
        }
        public Logger Logger { get; set; }
        public int Throughput { get; set; }
        public void Run(string inputFile, int port)
        {
            _reader.AddVocabulary(File.OpenText(inputFile));
            var baseAddress = new Uri(string.Format("net.tcp://localhost:{0}/AutoCompleteWcfService", port));
             _selfHost = new ServiceHost(typeof(AutoCompleteWcfService), baseAddress);
            try
            {
                //selfHost.AddServiceEndpoint(typeof(IAutoCompleteWcfService), new NetTcpBinding(), "");

                var bindingElements = new List<BindingElement>();
                var httpBindingElement = new TcpTransportBindingElement();
                var textBindingElement = new CustomTextMessageBindingElement("us-ascii");
                bindingElements.Add(textBindingElement);
                bindingElements.Add(httpBindingElement);
                var binding = new CustomBinding(bindingElements);


                _selfHost.AddServiceEndpoint(typeof(IAutoCompleteWcfService), binding, "");

                var smb = new ServiceMetadataBehavior { HttpGetEnabled = false, HttpsGetEnabled = false };
                _selfHost.Description.Behaviors.Add(smb);

                var mexBinding = MetadataExchangeBindings.CreateMexTcpBinding();
                _selfHost.AddServiceEndpoint(typeof(IMetadataExchange), mexBinding, "mex");

                _selfHost.AddDependencyInjectionBehavior<IAutoCompleteWcfService>(ServiceLocator.GetContainer());

                var throttleBehavior = new ServiceThrottlingBehavior
                {
                    MaxConcurrentCalls = Throughput,
                    MaxConcurrentInstances = 20,
                    MaxConcurrentSessions = Throughput,
                };
                _selfHost.Description.Behaviors.Add(throttleBehavior);

                _selfHost.Open();
                Logger.Info("The service is ready.");
                Logger.Info("Press <ENTER> to terminate service.");
            }
            catch (CommunicationException ce)
            {
                Logger.Error("An exception occurred: {0}", ce.Message);
                _selfHost.Abort();
            }
        }

        public void Stop()
        {  
            try
            {
                _selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Logger.Error("An exception occurred: {0}", ce.Message);
                _selfHost.Abort();
            }
        }
    }
}
