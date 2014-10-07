using System;
using System.Collections.Generic;
using Vdk.AutoCompleter.Core.Models;

namespace Vdk.AutoCompleter.Core.Services
{
    public class AsciiNGramParser : INGramParser<AsciiString>
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public AsciiNGramParser(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public IEnumerable<AsciiString> GetLexemes(AsciiString word)
        {
            var startIndex = Math.Min(word.Length, _minLength);
            var length = Math.Min(word.Length, _maxLength);
            for (var i = startIndex; i < length; i++)
                yield return word.Substring(0, i);
        }
    }
}