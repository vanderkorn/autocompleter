namespace Vdk.AutoCompleter.Core.Models
{
    public class Word<T>
    {
        public T Value { get; set; }
        public uint Frequency { get; set; }
    }


}
