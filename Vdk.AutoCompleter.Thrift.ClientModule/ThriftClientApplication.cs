// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThriftClientApplication.cs" company="Ivan Kornilov">
//   Ivan Kornilov
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the ThriftClientApplication type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Thrift.ClientModule
{
    using System.Collections.Generic;
    using global::Thrift.Protocol;
    using global::Thrift.Transport;
    using Vdk.AutoCompleter.Common;
    using Vdk.AutoCompleter.Thrift.Core;

    /// <summary>
    /// The THRIFT client module.
    /// </summary>
    public class ThriftClientApplication : IApplicationClient<string>
    {
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
            TTransport transport = new TSocket(host, port);
            transport.Open();

            var proto = new TBinaryProtocol(transport);
            this.client = new AutoCompleteService.Client(proto);
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
    }
}