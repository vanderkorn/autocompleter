using System;
using System.Collections.Generic;
using VDK.AutoCompleter.Core.Models;

namespace VDK.AutoCompleter.Core.Services
{
    public class WordComparer : IComparer<Word>
    {
        public int Compare(Word x, Word y)
        {
            var resultCompare = x.Frequency.CompareTo(y.Frequency);

            if (resultCompare < 0)
                return 1;

            if (resultCompare > 0)
                return -1;

            return String.Compare(x.Value, y.Value, StringComparison.Ordinal);

            //var resultCompare = String.Compare(x.Value, y.Value, StringComparison.Ordinal);

            //if (resultCompare > 0)
            //    return 1;

            //if (resultCompare < 0)
            //    return -1;

            //return x.Frequency.CompareTo(y.Frequency);
        }
    }
}