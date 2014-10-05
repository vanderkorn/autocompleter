using System.Collections.Generic;
using System.Linq;
using VDK.AutoCompleter.Core.Models;

namespace VDK.AutoCompleter.Core.Services
{
    public class AutoCompleteService : IAutoCompleteService
    {
        private readonly INGramParser _nGramParser;
        private readonly SortedDictionary<string, SortedSet<Word>> _vocabulary;
        public AutoCompleteService(INGramParser nGramParser)
        {
            _nGramParser = nGramParser;
            _vocabulary = new SortedDictionary<string, SortedSet<Word>>();
        }

        public void AddWordToVocabulary(Word word)
        {
            foreach (var lexem in _nGramParser.GetLexemes(word.Value))
            {
                if (_vocabulary.ContainsKey(lexem))
                {
                    _vocabulary[lexem].Add(word);
                }
                else
                {
                    _vocabulary.Add(lexem, new SortedSet<Word>(new WordComparer())
                    {
                        word
                    });
                }
            }
        }

        public void AddWordsToVocabulary(IEnumerable<Word> words)
        {
            foreach (var word in words)
                AddWordToVocabulary(word);
        }

        public IEnumerable<Word> GetWordsByPrefix(string prefix)
        {
            if (_vocabulary.ContainsKey(prefix))
                return _vocabulary[prefix].Take(10);
            return null;
        }
    }
}