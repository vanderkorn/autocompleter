using System.Collections.Generic;
using VDK.AutoCompleter.Common.IOC;
using VDK.AutoCompleter.Core.Models;

namespace VDK.AutoCompleter.Core.Services
{
    public interface IAutoCompleteService : ISingletonDependency
    {
        void AddWordToVocabulary(Word word);
        void AddWordsToVocabulary(IEnumerable<Word> words);
        IEnumerable<Word> GetWordsByPrefix(string prefix);
    }
}
