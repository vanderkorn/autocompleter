using System.Collections.Generic;
using System.IO;
using System.Linq;
using VDK.AutoCompleter.Core.Models;

namespace VDK.AutoCompleter.Core.Services
{
    public class VocabularyWriter : IVocabularyWriter
    {
        private readonly IAutoCompleteService _autoCompleteService;

        public VocabularyWriter(IAutoCompleteService autoCompleteService)
        {
            _autoCompleteService = autoCompleteService;
        }

        public void GetWordsByPrefixes(IEnumerable<string> prefixes, TextWriter writer)
        {
            foreach (var prefix in prefixes)
            {
                var words = _autoCompleteService.GetWordsByPrefix(prefix);
                if (words != null)
                {
                    var enumerable = words as Word[] ?? words.ToArray();
                    if (enumerable.Any())
                    {
                        foreach (var val in enumerable)
                            writer.WriteLine("{0} {1}", val.Value, val.Frequency);
                        //foreach (var val in enumerable.Select(w => w.Value))
                        //    writer.WriteLine(val);
                        writer.WriteLine();
                    }
                }
            }
        }
    }
}