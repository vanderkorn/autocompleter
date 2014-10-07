using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Vdk.AutoCompleter.Core.Collections;
using Vdk.AutoCompleter.Core.Comparers;
using Vdk.AutoCompleter.Core.Models;
using Vdk.AutoCompleter.Core.NGramGenerators;

namespace Vdk.AutoCompleter.Core.Services
{
    public class ConcurentAutoCompleteService<T> : IAutoCompleteService<T> where T : IComparable<T>
    {
        private readonly INGramParser<T> _nGramParser;
        private readonly IComparerFactory<T> _comparerFactory;



        private readonly ConcurrentDictionary<T, ConcurrentSortedSet<Word<T>>> _vocabulary;


        public ConcurentAutoCompleteService(INGramParser<T> nGramParser, IComparerFactory<T> comparerFactory)
        {
            _nGramParser = nGramParser;
            _comparerFactory = comparerFactory;
            _vocabulary = new ConcurrentDictionary<T, ConcurrentSortedSet<Word<T>>>();
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
                    _vocabulary.AddOrUpdate(lexem, new ConcurrentSortedSet<Word<T>>(_comparerFactory.GetComparer())
                    {
                        word
                    }, (key, oldValue) =>
                    {
                        oldValue.Add(word);
                        return oldValue;
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
            //if (_vocabulary.Count == 0)
            //    _vocabulary = new ConcurrentDictionary<T, ConcurrentSortedSet<Word<T>>>((int)count);
        }
    }
}