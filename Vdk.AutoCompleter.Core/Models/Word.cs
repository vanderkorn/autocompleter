// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Word.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   The word vocabulary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Models
{
    /// <summary>
    /// The word vocabulary.
    /// </summary>
    /// <typeparam name="T">
    /// Type of element vocabulary
    /// </typeparam>
    public class Word<T>
    {
        /// <summary>
        /// Gets or sets the value word.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Gets or sets the frequency word.
        /// </summary>
        public uint Frequency { get; set; }
    }
}