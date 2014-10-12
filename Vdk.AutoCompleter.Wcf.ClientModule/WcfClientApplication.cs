using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.ServiceModel.Samples;
using Vdk.AutoCompleter.Common;
using Vdk.AutoCompleter.Wcf.ClientModule.AutoCompleteWcfService;

namespace Vdk.AutoCompleter.Wcf.ClientModule
{
    public class WcfClientApplication : IApplicationClient<string>
    {
        private AutoCompleteWcfServiceClient _client;

        public void Dispose()
        {
            if (_client!=null)
            _client.Close();
        }

        public IList<string> Get(string prefix)
        {
            return _client.Get(prefix);
        }

        public void Connect(string host, int port)
        {
            var baseAddress = new Uri(string.Format("net.tcp://{0}:{1}/AutoCompleteWcfService", host, port));
            var bindingElements = new List<BindingElement>();
            var httpBindingElement = new TcpTransportBindingElement();
            var textBindingElement = new CustomTextMessageBindingElement("us-ascii");
            bindingElements.Add(textBindingElement);
            bindingElements.Add(httpBindingElement);
            var binding = new CustomBinding(bindingElements);
            var adress = new EndpointAddress(baseAddress);
             _client = new AutoCompleteWcfServiceClient(binding, adress);
        }

    }
}
