// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Options.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the Options type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.TestClient.Models
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
        [Option('r', "read", Required = false, HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }
    }
}
