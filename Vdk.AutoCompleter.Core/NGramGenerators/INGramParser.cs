// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INGramParser.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the INGramParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.NGramGenerators
{
    using System.Collections.Generic;

    /// <summary>
    /// The NGRAM parser.
    /// </summary>
    /// <typeparam name="T">
    /// Type of element NGRAM parser
    /// </typeparam>
    public interface INGramParser<T> 
    {
        /// <summary>
        /// The get NGRAM lexemes.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        /// <returns>
        /// The NGRAM lexemes.
        /// </returns>
        IEnumerable<T> GetLexemes(T word);
    }
}