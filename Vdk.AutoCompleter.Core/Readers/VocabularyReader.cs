using System;
using System.Collections.Generic;
using System.IO;
using Vdk.AutoCompleter.Core.Converters;
using Vdk.AutoCompleter.Core.Models;
using Vdk.AutoCompleter.Core.Services;

namespace Vdk.AutoCompleter.Core.Readers
{
    public class VocabularyReader<T> : IVocabularyReader<T> where T : IComparable<T>
    {
        private readonly IAutoCompleteService<T> _autoCompleteService;
        private readonly IWordValueConverter<T> _converter;
        private readonly IList<T> _testPrefixesList;

        public VocabularyReader(IAutoCompleteService<T> autoCompleteService, IWordValueConverter<T> converter)
        {
            _autoCompleteService = autoCompleteService;
            _converter = converter;
            _testPrefixesList = new List<T>();
        }

        public void AddVocabulary(TextReader reader)
        {
            string line;
            uint lineNumber = 0;
            uint n = 0;
            ushort m = 0;


            while ((line = reader.ReadLine()) != null)
            {
                if (lineNumber == 0)
                {
                    n = uint.Parse(line);
                    _autoCompleteService.SetCountWords(n);
                }
                else if (lineNumber <= n)
                {
                   var result = line.Split(' ');
                   var word = new Word<T>()
                    {
                        Value = _converter.FromString(result[0]),
                        Frequency = uint.Parse(result[1])
                    };
                _autoCompleteService.AddWordToVocabulary(word);
     
                
            
                }
                else if (lineNumber == n +1)
                {
                    m = ushort.Parse(line);
                }
                else if (lineNumber <= m + (n + 1))
                {
                    var word = _converter.FromString(line);
               _testPrefixesList.Add(word);

       
                }
                else
                    break;
                lineNumber++;
            }
        }

        public IEnumerable<T> GetTestPrefixes()
        {
            return _testPrefixesList;
        }
    }
}