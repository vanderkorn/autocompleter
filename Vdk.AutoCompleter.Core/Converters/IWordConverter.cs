using Vdk.AutoCompleter.Common.IOC;

namespace Vdk.AutoCompleter.Core.Converters
{
    public interface IWordConverter<T> : IDependency
    {
        T FromString(string str);
        string ToString(T obj);
    }
}