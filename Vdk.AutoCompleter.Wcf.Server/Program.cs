using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Autofac;
using Autofac.Integration.Wcf;
using Microsoft.ServiceModel.Samples;
using Vdk.AutoCompleter.Common.IOC;
using Vdk.AutoCompleter.Core;
using Vdk.AutoCompleter.Core.Readers;
using Vdk.AutoCompleter.Core.Services;
using Vdk.AutoCompleter.Wcf.Server.Models;
using Vdk.AutoCompleter.Wcf.Service;

namespace Vdk.AutoCompleter.Wcf.Server
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
                    var baseAddress = new Uri(string.Format("net.tcp://localhost:{0}/AutoCompleteWcfService", options.Port));
                    var selfHost = new ServiceHost(typeof(AutoCompleteWcfService), baseAddress);
                    try
                    {
                        //selfHost.AddServiceEndpoint(typeof(IAutoCompleteWcfService), new NetTcpBinding(), "");

                        var bindingElements = new List<BindingElement>();
                        var httpBindingElement = new TcpTransportBindingElement();
                        var textBindingElement = new CustomTextMessageBindingElement("us-ascii");
                        bindingElements.Add(textBindingElement);
                        bindingElements.Add(httpBindingElement);
                        var binding = new CustomBinding(bindingElements);


                        selfHost.AddServiceEndpoint(typeof(IAutoCompleteWcfService), binding, "");
                
                        var smb = new ServiceMetadataBehavior { HttpGetEnabled = false, HttpsGetEnabled = false };
                        selfHost.Description.Behaviors.Add(smb);

                        var mexBinding = MetadataExchangeBindings.CreateMexTcpBinding();
                        selfHost.AddServiceEndpoint(typeof(IMetadataExchange), mexBinding, "mex");

                        selfHost.AddDependencyInjectionBehavior<IAutoCompleteWcfService>(ServiceLocator.GetContainer());

                        var throttleBehavior = new ServiceThrottlingBehavior
                        {
                            MaxConcurrentCalls = Throughput,
                            MaxConcurrentInstances = 20,
                            MaxConcurrentSessions = Throughput,
                        };
                        selfHost.Description.Behaviors.Add(throttleBehavior);

                        selfHost.Open();
                        Console.WriteLine("The service is ready.");
                        Console.WriteLine("Press <ENTER> to terminate service.");
                        Console.WriteLine();
                        Console.ReadLine();

                        // Close the ServiceHostBase to shutdown the service.
                        selfHost.Close();
                    }
                    catch (CommunicationException ce)
                    {
                        Console.WriteLine("An exception occurred: {0}", ce.Message);
                        selfHost.Abort();
                    }

                }
            }
        }

        private static void Dependencies(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutoCompleteModule());
            builder.Register(c => new AutoCompleteWcfService(c.Resolve<IAutoCompleteService<string>>())).As<IAutoCompleteWcfService>();
        }
    }
}
