// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsciiComparerFactory.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   The ascii comparer factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Comparers
{
    using System.Collections.Generic;
    using Vdk.AutoCompleter.Core.Models;

    /// <summary>
    /// The ASCII comparer factory.
    /// </summary>
    public class AsciiComparerFactory : IComparerFactory<AsciiString>
    {
        /// <summary>
        /// ASCII comparer.
        /// </summary>
        private readonly WordComparer instance = new WordComparer();

        /// <summary>
        /// The get ASCII comparer.
        /// </summary>
        /// <returns>
        /// The comparer.
        /// </returns>
        public IComparer<Word<AsciiString>> GetComparer()
        {
            return this.instance;
        }
    }
}