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
        /// Max reconnect retries
        /// </summary>
        private const int MaxRetries = 3;

        /// <summary>
        /// The WCF service client.
        /// </summary>
        private AutoCompleteWcfServiceClient client;

        /// <summary>
        /// Port server
        /// </summary>
        private int _port;

        /// <summary>
        /// Host server
        /// </summary>
        private string _host;

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
            Exception maxRetriesException = null;
            for (var i = 0; i < MaxRetries; i++)
            {
                try
                {
                    return this.client.Get(prefix);
                }
                catch (ChannelTerminatedException ex)
                {
                    maxRetriesException = ex;
                    this.client.Abort();
                    this.TryReConnect();
                }
                catch (EndpointNotFoundException ex)
                {
                    maxRetriesException = ex;
                    this.client.Abort();
                    this.TryReConnect();
                }
                catch (ServerTooBusyException ex)
                {
                    maxRetriesException = ex;
                    this.client.Abort();
                    this.TryReConnect();
                }
                catch (TimeoutException ex)
                {
                    maxRetriesException = ex;
                    this.client.Abort();
                    this.TryReConnect();
                }
                catch (CommunicationException ex)
                {
                    maxRetriesException = ex;
                    if (this.client.State == CommunicationState.Faulted)
                    {
                        this.client.Abort();
                        this.TryReConnect();
                    }
                    else
                        throw;
                }
            }

            if (maxRetriesException != null)
            {
                throw new Exception(string.Format("WCF call failed after {0} retries", MaxRetries), maxRetriesException);
            }

            throw new Exception(string.Format("WCF call failed after {0} retries", MaxRetries));
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
            this._host = host;
            this._port = port;
            this.TryReConnect();
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

        /// <summary>
        /// Try reconnect to server.
        /// </summary>
        private void TryReConnect()
        {
            var baseAddress = new Uri(string.Format("net.tcp://{0}:{1}/AutoCompleteWcfService", this._host, this._port));
            var bindingElements = new List<BindingElement>();
            var httpBindingElement = new TcpTransportBindingElement();
            var textBindingElement = new CustomTextMessageBindingElement("us-ascii");
            bindingElements.Add(textBindingElement);
            bindingElements.Add(httpBindingElement);
            var binding = new CustomBinding(bindingElements);

            var adress = new EndpointAddress(baseAddress);

            this.client = new AutoCompleteWcfServiceClient(binding, adress);
            try
            {
                this.client.Open();
            }
            catch (ChannelTerminatedException)
            {
            }
            catch (EndpointNotFoundException)
            {
            }
            catch (ServerTooBusyException)
            {
            }
            catch (TimeoutException)
            {
            }
            catch (CommunicationException)
            {
            }
        }
    }
}