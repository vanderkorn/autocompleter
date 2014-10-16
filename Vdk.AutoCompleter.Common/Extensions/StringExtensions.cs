// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the StringExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Common.Extensions
{
    /// <summary>
    /// String extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Remove duplicate blank spaces
        /// </summary>
        /// <param name="source">String</param>
        /// <returns>String without duplicate blank spaces</returns>
        public static string RemoveDuplicateBlankSpaces(this string source)
        {
           return System.Text.RegularExpressions.Regex.Replace(source, @"\s+", " ");
        }
    }
}
