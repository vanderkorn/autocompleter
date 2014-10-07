using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vdk.AutoCompleter.Core.Services
{
    public class NGramParser : INGramParser<string>
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public NGramParser(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public IEnumerable<string> GetLexemes(string word)
        {
            var startIndex = Math.Min(word.Length, _minLength);
            var length = Math.Min(word.Length, _maxLength);

            var list = new ConcurrentBag<string>();
            Parallel.For(startIndex, length, i => list.Add(word.Substring(0, i)));
            return list;

            //for (var i = startIndex; i < length; i++)
            //    yield return word.Substring(0, i);
        }
    }
}