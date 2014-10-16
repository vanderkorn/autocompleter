// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThriftApplicationClient.cs" company="Ivan Kornilov">
//   Ivan Kornilov
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the ThriftClientApplication type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Thrift.ClientModule
{
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;

    using global::Thrift.Protocol;
    using global::Thrift.Transport;
    using Vdk.AutoCompleter.Common;
    using Vdk.AutoCompleter.Thrift.Core;

    /// <summary>
    /// The THRIFT client module.
    /// </summary>
    public class ThriftApplicationClient : IApplicationClient<string>
    {
        /// <summary>
        /// Max reconnect retries
        /// </summary>
        private const int MaxRetries = 3;

        /// <summary>
        /// Port server
        /// </summary>
        private int _port;

        /// <summary>
        /// Host server
        /// </summary>
        private string _host;

        /// <summary>
        /// The THRIFT client.
        /// </summary>
        private AutoCompleteService.Client client;

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
                    if (this.client == null)
                    {
                        this.TryReConnect();
                    }

                    return this.client.Get(prefix);
                }
                catch (SocketException ex)
                {
                    maxRetriesException = ex;

                    if (this.client != null)
                    {
                        this.client.Dispose();
                    }

                    this.client = null;
                }
            }

            if (maxRetriesException != null)
            {
                throw new Exception(string.Format("Thrift call failed after {0} retries", MaxRetries), maxRetriesException);
            }

            throw new Exception(string.Format("Thrift call failed after {0} retries", MaxRetries));
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
            try
            {
                this.TryReConnect();
            }
            catch (SocketException)
            {
            }
        }

        /// <summary>
        /// The dispose/disconnect from server.
        /// </summary>
        public void Dispose()
        {
            if (this.client != null)
            {
                this.client.Dispose();
            }
        }


        /// <summary>
        /// Reconnect to server.
        /// </summary>
        private void TryReConnect()
        {
            TTransport transport = new TSocket(_host, _port);
            transport.Open();

            var proto = new TBinaryProtocol(transport);
            this.client = new AutoCompleteService.Client(proto);
        }
    }
}