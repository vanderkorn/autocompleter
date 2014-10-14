// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringWordComparer.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the StringWordComparer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Comparers
{
    using System;
    using System.Collections.Generic;
    using Vdk.AutoCompleter.Core.Models;

    /// <summary>
    /// The string word comparer.
    /// </summary>
    public class StringWordComparer : IComparer<Word<string>>
    {
        /// <summary>
        /// The compare string.
        /// </summary>
        /// <param name="x">
        /// The string x.
        /// </param>
        /// <param name="y">
        /// The string y.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Compare(Word<string> x, Word<string> y)
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

            return string.Compare(x.Value, y.Value, StringComparison.Ordinal);
        }
    }
}