// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Options.cs" company="Ivan Kornilov">
//   Copyright (c) 2014, Ivan Kornilov. All Right Reserved.
// </copyright>
// <summary>
//   Defines the Options type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Wcf.Server.Models
{
    using CommandLine;

    /// <summary>
    /// The console arguments.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Gets or sets the input file (vocabulary).
        /// </summary>
        [Option('r', "read", Required = true, HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }

        /// <summary>
        /// Gets or sets the server port.
        /// </summary>
        [Option('p', "port", Required = true, HelpText = "Input port.")]
        public int Port { get; set; }
    }
}