using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Vdk.AutoCompleter.Core.Collections;
using Vdk.AutoCompleter.Core.Converters;
using Vdk.AutoCompleter.Core.Models;
using Vdk.AutoCompleter.Core.Services;

namespace Vdk.AutoCompleter.Core.Readers
{
    public class ConcurentVocabularyReader<T> : IVocabularyReader<T> where T : IComparable<T>
    {
        private readonly IAutoCompleteService<T> _autoCompleteService;
        private readonly IWordValueConverter<T> _converter;
        private readonly IList<T> _testPrefixesList;

        public ConcurentVocabularyReader(IAutoCompleteService<T> autoCompleteService, IWordValueConverter<T> converter)
        {
            _autoCompleteService = autoCompleteService;
            _converter = converter;
            _testPrefixesList = new ConcurrentList<T>();
        }

        public void AddVocabulary(TextReader reader)
        {
            string line;
            uint lineNumber = 0;
            uint n = 0;
            ushort m = 0;
            var tasks = new List<Task>();

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
                    var task = new Task(() => _autoCompleteService.AddWordToVocabulary(word));
                    task.Start();
                    tasks.Add(task);


                }
                else if (lineNumber == n + 1)
                {
                    m = ushort.Parse(line);
                }
                else if (lineNumber <= m + (n + 1))
                {
                    var word = _converter.FromString(line);
                    var task = new Task(() => _testPrefixesList.Add(word));
                    task.Start();
                    tasks.Add(task);

                }
                else
                    break;
                lineNumber++;
            }
            //tasks.ForEach(t=>t.Start());
            Task.WaitAll(tasks.ToArray());
        }

        public IEnumerable<T> GetTestPrefixes()
        {
            return _testPrefixesList;
        }
    }
}