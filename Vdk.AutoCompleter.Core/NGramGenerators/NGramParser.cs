// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NGramParser.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the NGramParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.NGramGenerators
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The n gram parser.
    /// </summary>
    public class NGramParser : INGramParser<string>
    {
        /// <summary>
        /// The min length NGRAM.
        /// </summary>
        private readonly int minLength;

        /// <summary>
        /// The max length NGRAM.
        /// </summary>
        private readonly int maxLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="NGramParser"/> class.
        /// </summary>
        /// <param name="minLength">
        /// The min length.
        /// </param>
        /// <param name="maxLength">
        /// The max length.
        /// </param>
        public NGramParser(int minLength, int maxLength)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        /// <summary>
        /// The get NGRAM lexemes.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        /// <returns>
        /// The NGRAM lexemes.
        /// </returns>
        public IEnumerable<string> GetLexemes(string word)
        {
            var startIndex = Math.Min(word.Length, this.minLength);
            var length = Math.Min(word.Length, this.maxLength);

            for (var i = startIndex; i < length; i++)
            {
                yield return word.Substring(0, i);
            }
        }
    }
}