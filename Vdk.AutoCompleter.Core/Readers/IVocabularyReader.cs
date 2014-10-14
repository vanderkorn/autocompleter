// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IVocabularyReader.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   The vocabulary reader interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Readers
{
    using System.Collections.Generic;
    using System.IO;
    using Vdk.AutoCompleter.Common.IOC;

    /// <summary>
    /// The vocabulary reader interface.
    /// </summary>
    /// <typeparam name="T">
    /// Type of element
    /// </typeparam>
    public interface IVocabularyReader<out T> : IDependency
    {
        /// <summary>
        /// The add vocabulary.
        /// </summary>
        /// <param name="reader">
        /// The reader vocabulary.
        /// </param>
        void AddVocabulary(TextReader reader);

        /// <summary>
        /// The get test prefixes.
        /// </summary>
        /// <returns>
        /// The test prefixes.
        /// </returns>
        IEnumerable<T> GetTestPrefixes();
    }
}