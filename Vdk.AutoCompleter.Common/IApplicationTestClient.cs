// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApplicationTestClient.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the IApplicationTestClient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Common
{
    using System.IO;
    using Vdk.AutoCompleter.Common.IOC;

    /// <summary>
    /// The TestClient module interface.
    /// </summary>
    public interface IApplicationTestClient : IDependency
    {
        /// <summary>
        /// The run TestClient.
        /// </summary>
        /// <param name="reader">
        /// The reader dictionary and test data.
        /// </param>
        /// <param name="writer">
        /// The writer results.
        /// </param>
        void Run(TextReader reader, TextWriter writer);
    }
}