// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WordComparer.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   The word comparer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Comparers
{
    using System.Collections.Generic;
    using Vdk.AutoCompleter.Core.Models;

    /// <summary>
    /// The word comparer ASCII string.
    /// </summary>
    public class WordComparer : IComparer<Word<AsciiString>>
    {
        /// <summary>
        /// The compare ASCII string.
        /// </summary>
        /// <param name="x">
        /// The ASCII string x.
        /// </param>
        /// <param name="y">
        /// The ASCII string y.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Compare(Word<AsciiString> x, Word<AsciiString> y)
        {
            var resultCompare = x.Frequency.CompareTo(y.Frequency);

            if (resultCompare < 0)
            {
                return 1;
            }

            if (resultCompare > 0)
            {
                return -1;
            }

            return x.Value.CompareTo(y.Value);
        }
    }
}