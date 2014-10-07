namespace Vdk.AutoCompleter.Core.Converters
{
    public class WordValueConverter : IWordValueConverter<string>
    {
        public string FromString(string str)
        {
            return str;
        }

        public string ToString(string obj)
        {
            return obj;
        }
    }
}