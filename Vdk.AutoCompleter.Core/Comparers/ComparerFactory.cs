// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComparerFactory.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   The comparer factory string .
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Comparers
{
    using System.Collections.Generic;
    using Vdk.AutoCompleter.Core.Models;

    /// <summary>
    /// The comparer factory string .
    /// </summary>
    public class ComparerFactory : IComparerFactory<string>
    {
        /// <summary>
        /// The string comparer.
        /// </summary>
        private readonly StringWordComparer instance = new StringWordComparer();

        /// <summary>
        /// The get comparer string.
        /// </summary>
        /// <returns>
        /// The comparer.
        /// </returns>
        public IComparer<Word<string>> GetComparer() 
        {
            return this.instance;
        }
    }
}