using System.Collections.Generic;
using System.IO;
using VDK.AutoCompleter.Core.Models;

namespace VDK.AutoCompleter.Core.Services
{
    public class VocabularyReader : IVocabularyReader
    {
        private readonly IAutoCompleteService _autoCompleteService;
        private readonly IList<string> _testPrefixesList;

        public VocabularyReader(IAutoCompleteService autoCompleteService)
        {
            _autoCompleteService = autoCompleteService;
            _testPrefixesList = new List<string>();
        }

        public void AddVocabulary(TextReader reader)
        {
            string line;
            var lineNumber = 0;
            uint n = 0;
            uint m = 0;

            while ((line = reader.ReadLine()) != null)
            {
                if (lineNumber == 0)
                {
                    n = uint.Parse(line);
                }
                else if (lineNumber <= n)
                {
                   var result = line.Split(' ');
                    var word = new Word()
                    {
                        Value = result[0],
                        Frequency = uint.Parse(result[1])
                    };
                    _autoCompleteService.AddWordToVocabulary(word);
                }
                else if (lineNumber == n +1)
                {
                    m = uint.Parse(line);
                }
                else if (lineNumber <= m + (n + 1))
                {
                    _testPrefixesList.Add(line);
                }
                else
                    break;
                lineNumber++;
            }
        }

        public IEnumerable<string> GetTestPrefixes()
        {
            return _testPrefixesList;
        }
    }
}