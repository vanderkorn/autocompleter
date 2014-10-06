using System;
using System.Collections.Generic;
using Vdk.AutoCompleter.Common.IOC;
using Vdk.AutoCompleter.Core.Models;

namespace Vdk.AutoCompleter.Core.Services
{
    public interface IAutoCompleteService<T> : ISingletonDependency where T : IComparable<T>
    {
        void AddWordToVocabulary(Word<T> word);
        void AddWordsToVocabulary(IEnumerable<Word<T>> words);
        IEnumerable<Word<T>> GetWordsByPrefix(T prefix);
        void SetCountWords(uint count);
    }
}
