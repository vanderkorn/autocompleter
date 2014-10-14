// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsciiWordValueConverter.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the AsciiWordValueConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Converters
{
    using Vdk.AutoCompleter.Core.Models;

    /// <summary>
    /// The ASCII word value converter.
    /// </summary>
    public class AsciiWordValueConverter : IWordValueConverter<AsciiString>
    {
        /// <summary>
        /// The ASCII string from string.
        /// </summary>
        /// <param name="str">
        /// The string.
        /// </param>
        /// <returns>
        /// The <see cref="AsciiString"/>.
        /// </returns>
        public AsciiString FromString(string str)
        {
            return AsciiString.Parse(str);
        }

        /// <summary>
        /// The ASCII string to string.
        /// </summary>
        /// <param name="obj">
        /// The ASCII string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ToString(AsciiString obj)
        {
            return obj.ToString();
        }
    }
}