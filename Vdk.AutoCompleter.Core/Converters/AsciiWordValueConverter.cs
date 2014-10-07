using Vdk.AutoCompleter.Core.Models;

namespace Vdk.AutoCompleter.Core.Converters
{
    public class AsciiWordValueConverter : IWordValueConverter<AsciiString>
    {
        public AsciiString FromString(string str)
        {
            return AsciiString.Parse(str);
        }

        public string ToString(AsciiString obj)
        {
            return obj.ToString();
        }
    }
}