// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WcfClientApplication.cs" company="Ivan Kornilov">
//   Ivan Kornilov
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the WcfClientApplication type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Wcf.ClientModule
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using Microsoft.ServiceModel.Samples;
    using Vdk.AutoCompleter.Common;
    using Vdk.AutoCompleter.Wcf.ClientModule.AutoCompleteWcfService;

    /// <summary>
    /// The WCF client module.
    /// </summary>
    public class WcfClientApplication : IApplicationClient<string>
    {
        /// <summary>
        /// The WCF service client.
        /// </summary>
        private AutoCompleteWcfServiceClient client;

        /// <summary>
        /// The get words.
        /// </summary>
        /// <param name="prefix">
        /// The prefix.
        /// </param>
        /// <returns>
        /// The words.
        /// </returns>
        public IList<string> Get(string prefix)
        {
            return this.client.Get(prefix);
        }

        /// <summary>
        /// The connect to server.
        /// </summary>
        /// <param name="host">
        /// The host server.
        /// </param>
        /// <param name="port">
        /// The port server.
        /// </param>
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
            this.client = new AutoCompleteWcfServiceClient(binding, adress);
        }

        /// <summary>
        /// The dispose/disconnect from server.
        /// </summary>
        public void Dispose()
        {
            if (this.client != null)
            {
                this.client.Close();
            }
        }
    }
}