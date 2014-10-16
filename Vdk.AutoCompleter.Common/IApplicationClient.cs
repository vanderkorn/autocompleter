// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApplicationClient.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the IApplicationClient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Common
{
    using System;
    using System.Collections.Generic;
    using Vdk.AutoCompleter.Common.IOC;

    /// <summary>
    /// The client module interface.
    /// </summary>
    /// <typeparam name="T">
    /// Type of element vocabulary
    /// </typeparam>
    public interface IApplicationClient<T> : IDependency, IDisposable
    {
        /// <summary>
        /// The get words.
        /// </summary>
        /// <param name="prefix">
        /// The prefix.
        /// </param>
        /// <returns>
        /// The words.
        /// </returns>
        IList<T> Get(string prefix);

        /// <summary>
        /// The connect to server.
        /// </summary>
        /// <param name="host">
        /// The host server.
        /// </param>
        /// <param name="port">
        /// The port server.
        /// </param>
        void Connect(string host, int port);
    }
}