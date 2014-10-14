namespace Vdk.AutoCompleter.Core.Converters
{
    /// <summary>
    /// The word value converter.
    /// </summary>
    public class WordValueConverter : IWordValueConverter<string>
    {
        /// <summary>
        /// The from string.
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string FromString(string str)
        {
            return str;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ToString(string obj)
        {
            return obj;
        }
    }
}