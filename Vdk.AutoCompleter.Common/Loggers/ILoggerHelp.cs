// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILoggerHelp.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Interface NLOG help service
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Common.Loggers
{
    using NLog;

    using Vdk.AutoCompleter.Common.IOC;

    /// <summary>
    /// Interface logger help service
    /// </summary>
    public interface ILoggerHelp : IDependency
    {
        /// <summary>
        /// Gets or sets logger
        /// </summary>
        Logger Logger { get; set; }
    }
}