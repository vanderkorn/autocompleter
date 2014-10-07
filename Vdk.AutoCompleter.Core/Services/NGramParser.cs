﻿using System;
using System.Collections.Generic;

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
            for (var i = startIndex; i < length; i++)
                yield return word.Substring(0, i);
        }
    }
}