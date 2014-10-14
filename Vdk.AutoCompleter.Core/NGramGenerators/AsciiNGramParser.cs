// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsciiNGramParser.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the AsciiNGramParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.NGramGenerators
{
    using System;
    using System.Collections.Generic;
    using Vdk.AutoCompleter.Core.Models;

    /// <summary>
    /// The ASCII NGRAM parser.
    /// </summary>
    public class AsciiNGramParser : INGramParser<AsciiString>
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
        /// Initializes a new instance of the <see cref="AsciiNGramParser"/> class.
        /// </summary>
        /// <param name="minLength">
        /// The min length.
        /// </param>
        /// <param name="maxLength">
        /// The max length.
        /// </param>
        public AsciiNGramParser(int minLength, int maxLength)
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
        public IEnumerable<AsciiString> GetLexemes(AsciiString word)
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