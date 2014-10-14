namespace Vdk.AutoCompleter.Core.Converters
{
    /// <summary>
    /// The WordValueConverter interface.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface IWordValueConverter<T> 
    {
        /// <summary>
        /// The from string.
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T FromString(string str);

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string ToString(T obj);
    }
}