// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Options.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the Options type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Wcf.Client.Models
{
    using CommandLine;

    /// <summary>
    /// The console arguments.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Gets or sets the server host.
        /// </summary>
        [Option('h', "host", Required = true, HelpText = "Input host.")]
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the server port.
        /// </summary>
        [Option('p', "port", Required = true, HelpText = "Input port.")]
        public int Port { get; set; }
    }
}
