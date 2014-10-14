// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NLogModule.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the NLogModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Common.Loggers
{
    using System;
    using NLog;

    /// <summary>
    /// The NLOG module.
    /// </summary>
    public class NLogModule : LogModule<Logger>
    {
        /// <summary>
        /// The create logger.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="Logger"/>.
        /// </returns>
        protected override Logger CreateLoggerFor(Type type)
        {
            return LogManager.GetLogger(type.FullName);
        }
    }
}