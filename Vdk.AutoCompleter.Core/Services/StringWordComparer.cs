using System;
using System.Collections.Generic;
using System.Linq;
using Vdk.AutoCompleter.Core.Models;

namespace Vdk.AutoCompleter.Core.Services
{
    public class StringWordComparer : IComparer<Word<string>>
    {
        public int Compare(Word<string> x, Word<string> y)
        {
            var resultCompare = x.Frequency.CompareTo(y.Frequency);

            if (resultCompare < 0)
                return 1;

            if (resultCompare > 0)
                return -1;
            
            return String.Compare(x.Value, y.Value, StringComparison.Ordinal);
        }
    }
}