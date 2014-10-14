// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAutoCompleteService.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the IAutoCompleteService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Services
{
    using System;
    using System.Collections.Generic;
    using Vdk.AutoCompleter.Common.IOC;
    using Vdk.AutoCompleter.Core.Models;

    /// <summary>
    /// The AutoCompleteService interface.
    /// </summary>
    /// <typeparam name="T">
    /// Type of element
    /// </typeparam>
    public interface IAutoCompleteService<T> : ISingletonDependency where T : IComparable<T>
    {
        /// <summary>
        /// The add word to vocabulary.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        void AddWordToVocabulary(Word<T> word);

        /// <summary>
        /// The add words to vocabulary.
        /// </summary>
        /// <param name="words">
        /// The words.
        /// </param>
        void AddWordsToVocabulary(IEnumerable<Word<T>> words);

        /// <summary>
        /// The get words by prefix.
        /// </summary>
        /// <param name="prefix">
        /// The prefix.
        /// </param>
        /// <returns>
        /// The words.
        /// </returns>
        IEnumerable<Word<T>> GetWordsByPrefix(T prefix);

        /// <summary>
        /// The set count words.
        /// </summary>
        /// <param name="count">
        /// The count.
        /// </param>
        void SetCountWords(uint count);
    }
}
