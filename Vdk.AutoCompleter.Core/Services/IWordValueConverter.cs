using Vdk.AutoCompleter.Common.IOC;

namespace Vdk.AutoCompleter.Core.Services
{
    public interface IWordValueConverter<T> 
    {
        T FromString(string str);
        string ToString(T obj);
    }
}