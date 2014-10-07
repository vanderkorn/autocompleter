namespace Vdk.AutoCompleter.Core.Converters
{
    public interface IWordValueConverter<T> 
    {
        T FromString(string str);
        string ToString(T obj);
    }
}