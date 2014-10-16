// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NlogLoggerHelp.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   NLOG help service
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Common.Loggers
{
    using NLog;

    /// <summary>
    /// NLOG help service
    /// </summary>
    public class NlogLoggerHelp : ILoggerHelp
    {
        /// <summary>
        /// Gets or sets NLOG Logger
        /// </summary>
        public Logger Logger { get; set; }
    }
}