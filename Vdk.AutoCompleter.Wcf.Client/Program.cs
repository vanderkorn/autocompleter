using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.ServiceModel.Samples;
using Vdk.AutoCompleter.Wcf.Client.AutoCompleteWcfService;
using Vdk.AutoCompleter.Wcf.Client.Models;

namespace Vdk.AutoCompleter.Wcf.Client
{
    class Program
    {
        static void Main(string[] args)
        {   
            var options = new Options();
            // Parse in 'strict mode', success or quit
            if (CommandLine.Parser.Default.ParseArgumentsStrict(args, options))
            {
                var baseAddress = new Uri(string.Format("net.tcp://{0}:{1}/AutoCompleteWcfService", options.Host, options.Port));
                var bindingElements = new List<BindingElement>();
                var httpBindingElement = new TcpTransportBindingElement();
                var textBindingElement = new CustomTextMessageBindingElement("us-ascii");
                bindingElements.Add(textBindingElement);
                bindingElements.Add(httpBindingElement);
                var binding = new CustomBinding(bindingElements);
                var adress = new EndpointAddress(baseAddress);
                var client = new AutoCompleteWcfServiceClient(binding, adress);
                while (true)
                {
                    var line = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        if (line == "exit")
                            break;
                        var arr = line.Split(' ');
                        if (arr.Length < 2 || arr[0] != "get") 
                            continue;

                        var response = client.Get(arr[1]);
                        if (response != null)
                        {
                            if (response.Any())
                            {
                                foreach (var val in response)
                                    Console.WriteLine(val);
                                Console.WriteLine();
                            }
                        }
                    }

                }
                client.Close();
            }
          }
    }
}
