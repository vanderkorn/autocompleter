// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IComparerFactory.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   The ComparerFactory interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Comparers
{
    using System;
    using System.Collections.Generic;
    using Vdk.AutoCompleter.Core.Models;

    /// <summary>
    /// The Comparer factory interface.
    /// </summary>
    /// <typeparam name="T">
    /// Type of element
    /// </typeparam>
    public interface IComparerFactory<T> where T : IComparable<T>
    {
        /// <summary>
        /// The get comparer.
        /// </summary>
        /// <returns>
        /// The comparer.
        /// </returns>
        IComparer<Word<T>> GetComparer();
    }
}