// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApplicationServer.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the IApplicationServer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Common
{
    using NLog;
    using Vdk.AutoCompleter.Common.IOC;

    /// <summary>
    /// The Server module interface.
    /// </summary>
    public interface IApplicationServer : IDependency
    {
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets the throughput.
        /// </summary>
        int Throughput { get; set; }

        /// <summary>
        /// The start server.
        /// </summary>
        /// <param name="inputFile">
        /// The input file (vocabulary).
        /// </param>
        /// <param name="port">
        /// The server port.
        /// </param>
        void Run(string inputFile, int port);

        /// <summary>
        /// The stop server.
        /// </summary>
        void Stop();
    }
}