using System;
using System.Collections.Generic;
using System.Linq;
using Vdk.AutoCompleter.Core.Models;

namespace Vdk.AutoCompleter.Core.Services
{
    public class AutoCompleteService<T> : IAutoCompleteService<T> where T : IComparable<T>
    {
        private readonly INGramParser<T> _nGramParser;
        private readonly IComparerFactory<T> _comparerFactory;



        private  Dictionary<T, SortedSet<Word<T>>> _vocabulary;


        public AutoCompleteService(INGramParser<T> nGramParser, IComparerFactory<T> comparerFactory)
        {
            _nGramParser = nGramParser;
            _comparerFactory = comparerFactory;
            _vocabulary = new Dictionary<T, SortedSet<Word<T>>>();
        }

    

        public void AddWordToVocabulary(Word<T> word)
        {
            var r = _nGramParser.GetLexemes(word.Value).ToList();
            foreach (var lexem in r)
            {
                if (_vocabulary.ContainsKey(lexem))
                {
                    _vocabulary[lexem].Add(word);
                }
                else
                {
                    _vocabulary.Add(lexem, new SortedSet<Word<T>>(_comparerFactory.GetComparer())
                    {
                        word
                    });
                }
            }
        }

        public void AddWordsToVocabulary(IEnumerable<Word<T>> words)
        {
            foreach (var word in words)
                AddWordToVocabulary(word);
        }

        public IEnumerable<Word<T>> GetWordsByPrefix(T prefix)
        {
            return _vocabulary.ContainsKey(prefix) ? _vocabulary[prefix].Take(10) : null;
        }

        public void SetCountWords(uint count)
        {
            if (_vocabulary.Count == 0)
                _vocabulary = new Dictionary<T, SortedSet<Word<T>>>((int) count);
        }
    }
}