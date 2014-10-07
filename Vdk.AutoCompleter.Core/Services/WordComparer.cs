using System;
using System.Collections.Generic;
using STSdb4.General.Comparers;
using Vdk.AutoCompleter.Core.Models;

namespace Vdk.AutoCompleter.Core.Services
{
    //public class WordComparer<T> : IComparer<Word<T>> where T : IComparable<T>
    //{
    //    public int Compare(Word<T> x, Word<T> y)
    //    {

    //        var resultCompare = x.Frequency.CompareTo(y.Frequency);

    //        if (resultCompare < 0)
    //            return 1;

    //        if (resultCompare > 0)
    //            return -1;
    //        return x.Value.CompareTo(y.Value);
    //    }
    //}
    public class WordComparer : IComparer<Word<AsciiString>>
    {
        public int Compare(Word<AsciiString> x, Word<Models.AsciiString> y)
        {
            var resultCompare = x.Frequency.CompareTo(y.Frequency);

            if (resultCompare < 0)
                return 1;

            if (resultCompare > 0)
                return -1;
            return x.Value.CompareTo(y.Value);
        }
    }

    public interface IComparerFactory<T> where T : IComparable<T>
    {
        IComparer<Word<T>> GetComparer();
    }

    public class ComparerFactory: IComparerFactory<string>
    {
        private readonly StringWordComparer _instance = new StringWordComparer();
        public IComparer<Word<string>> GetComparer() 
        {
            return _instance;
        }
    }
    public class AsciiComparerFactory : IComparerFactory<AsciiString>
    {
        private readonly WordComparer _instance = new WordComparer();
        public IComparer<Word<AsciiString>> GetComparer()
        {
            return _instance;
        }
    }

}