using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.ServiceModel.Samples;
using Vdk.AutoCompleter.Wcf.Client.AutoCompleteWcfService;

namespace Vdk.AutoCompleter.Wcf.Client
{
    class Program
    {
        static void Main(string[] args)
        {    var baseAddress = new Uri(string.Format("net.tcp://localhost:{0}/AutoCompleteWcfService", 813));
            var bindingElements = new List<BindingElement>();
            var httpBindingElement = new TcpTransportBindingElement();
            var textBindingElement = new CustomTextMessageBindingElement("us-ascii");
            bindingElements.Add(textBindingElement);
            bindingElements.Add(httpBindingElement);
            var binding = new CustomBinding(bindingElements);
            var adress = new EndpointAddress(baseAddress);
            var client = new AutoCompleteWcfServiceClient(binding, adress);
            var response = client.Get("ba");
            client.Close();
          }
    }
}
