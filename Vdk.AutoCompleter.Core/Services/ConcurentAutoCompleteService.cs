// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConcurentAutoCompleteService.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the ConcurentAutoCompleteService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using Vdk.AutoCompleter.Core.Collections;
    using Vdk.AutoCompleter.Core.Comparers;
    using Vdk.AutoCompleter.Core.Models;
    using Vdk.AutoCompleter.Core.NGramGenerators;

    /// <summary>
    /// The concurent autocomplete service.
    /// </summary>
    /// <typeparam name="T">
    /// Type of element
    /// </typeparam>
    public class ConcurentAutoCompleteService<T> : IAutoCompleteService<T> where T : IComparable<T>
    {
        /// <summary>
        /// The n gram parser.
        /// </summary>
        private readonly INGramParser<T> nGramParser;

        /// <summary>
        /// The comparer factory.
        /// </summary>
        private readonly IComparerFactory<T> comparerFactory;

        /// <summary>
        /// The vocabulary.
        /// </summary>
        private readonly ConcurrentDictionary<T, ConcurrentSortedSet<Word<T>>> vocabulary;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurentAutoCompleteService{T}"/> class.
        /// </summary>
        /// <param name="nGramParser">
        /// The n gram parser.
        /// </param>
        /// <param name="comparerFactory">
        /// The comparer factory.
        /// </param>
        public ConcurentAutoCompleteService(INGramParser<T> nGramParser, IComparerFactory<T> comparerFactory)
        {
            this.nGramParser = nGramParser;
            this.comparerFactory = comparerFactory;
            this.vocabulary = new ConcurrentDictionary<T, ConcurrentSortedSet<Word<T>>>();
        }

        /// <summary>
        /// The add word to vocabulary.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        public void AddWordToVocabulary(Word<T> word)
        {
            var r = this.nGramParser.GetLexemes(word.Value).ToList();
            foreach (var lexem in r)
            {
                if (this.vocabulary.ContainsKey(lexem))
                {
                    this.vocabulary[lexem].Add(word);
                }
                else
                {
                    this.vocabulary.AddOrUpdate(
                        lexem,
                        new ConcurrentSortedSet<Word<T>>(this.comparerFactory.GetComparer()) { word },
                        (key, oldValue) =>
                            {
                                oldValue.Add(word);
                                return oldValue;
                            });
                }
            }
        }

        /// <summary>
        /// The add words to vocabulary.
        /// </summary>
        /// <param name="words">
        /// The words.
        /// </param>
        public void AddWordsToVocabulary(IEnumerable<Word<T>> words)
        {
            foreach (var word in words)
            {
                this.AddWordToVocabulary(word);
            }
        }

        /// <summary>
        /// The get words by prefix.
        /// </summary>
        /// <param name="prefix">
        /// The prefix.
        /// </param>
        /// <returns>
        /// The words.
        /// </returns>
        public IEnumerable<Word<T>> GetWordsByPrefix(T prefix)
        {
            return this.vocabulary.ContainsKey(prefix) ? this.vocabulary[prefix].Take(10) : null;
        }

        /// <summary>
        /// The set count words.
        /// </summary>
        /// <param name="count">
        /// The count.
        /// </param>
        public void SetCountWords(uint count)
        {
        }
    }
}