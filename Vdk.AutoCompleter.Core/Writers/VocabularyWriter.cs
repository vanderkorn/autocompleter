using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vdk.AutoCompleter.Core.Converters;
using Vdk.AutoCompleter.Core.Models;
using Vdk.AutoCompleter.Core.Services;

namespace Vdk.AutoCompleter.Core.Writers
{
    public class VocabularyWriter<T> : IVocabularyWriter<T> where T : IComparable<T>
    {
        private readonly IAutoCompleteService<T> _autoCompleteService;
        private readonly IWordValueConverter<T> _converter;

        public VocabularyWriter(IAutoCompleteService<T> autoCompleteService, IWordValueConverter<T> converter)
        {
            _autoCompleteService = autoCompleteService;
            _converter = converter;
        }

        public void GetWordsByPrefixes(IEnumerable<T> prefixes, TextWriter writer)
        {
            foreach (var prefix in prefixes)
            {
                var words = _autoCompleteService.GetWordsByPrefix(prefix);
                if (words != null)
                {
                    var enumerable = words as Word<T>[] ?? words.ToArray();
                    if (enumerable.Any())
                    {
                        //foreach (var val in enumerable)
                        //    writer.WriteLine("{0} {1}", _converter.ToString(val.Value), val.Frequency);
                        foreach (var val in enumerable.Select(w => w.Value))
                            writer.WriteLine(_converter.ToString(val));
                        writer.WriteLine();
                    }
                }
            }
        }
    }
}