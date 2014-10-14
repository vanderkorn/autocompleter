// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IVocabularyWriter.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the IVocabularyWriter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Writers
{
    using System.Collections.Generic;
    using System.IO;
    using Vdk.AutoCompleter.Common.IOC;

    /// <summary>
    /// The vocabulary result writer interface.
    /// </summary>
    /// <typeparam name="T">
    /// Type of element
    /// </typeparam>
    public interface IVocabularyWriter<T> : IDependency
    {
        /// <summary>
        /// The get words by prefixes.
        /// </summary>
        /// <param name="prefixes">
        /// The prefixes.
        /// </param>
        /// <param name="writer">
        /// The writer.
        /// </param>
        void GetWordsByPrefixes(IEnumerable<T> prefixes, TextWriter writer);
    }
}