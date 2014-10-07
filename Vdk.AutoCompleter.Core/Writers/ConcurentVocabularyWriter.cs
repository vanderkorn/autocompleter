//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using Vdk.AutoCompleter.Core.Collections;
//using Vdk.AutoCompleter.Core.Converters;
//using Vdk.AutoCompleter.Core.Models;
//using Vdk.AutoCompleter.Core.Services;

//namespace Vdk.AutoCompleter.Core.Writers
//{
//    public class ConcurentVocabularyWriter<T> : IVocabularyWriter<T> where T : IComparable<T>
//    {
//        private readonly IAutoCompleteService<T> _autoCompleteService;
//        private readonly IWordValueConverter<T> _converter;

//        public ConcurentVocabularyWriter(IAutoCompleteService<T> autoCompleteService, IWordValueConverter<T> converter)
//        {
//            _autoCompleteService = autoCompleteService;
//            _converter = converter;
//        }

//        public void GetWordsByPrefixes(IEnumerable<T> prefixes, TextWriter writer)
//        {
//            //var tasks = new List<Task>();
//            //var dicts = new ConcurrentDictionary<T, IEnumerable<Word<T>>>();

//            //foreach (var prefix in prefixes.ToList())
//            //{
//            //    T prefix1 = prefix;
//            //    var task = new Task(() =>
//            //    {
//            //        var words = _autoCompleteService.GetWordsByPrefix(prefix1);
//            //        dicts.
//            //    });
//            //    tasks.Add(task);
//            //    task.Start();
//            //}

//            //Task.WaitAll(tasks.ToArray());
//            var enumerable1 = prefixes as T[] ?? prefixes.ToArray();
//            var result = new List<IEnumerable<Word<T>>>();
//            for (int i = 0; i < enumerable1.Count(); i++)
//            {
//                result.Add(null);
//            }

//            Parallel.For(0, enumerable1.Count(), i => result[i] = _autoCompleteService.GetWordsByPrefix(enumerable1[i]));


//            foreach (var words in result)
//            {
//                if (words != null)
//                {
//                    var enumerable = words as Word<T>[] ?? words.ToArray();
//                    if (enumerable.Any())
//                    {
//                        //foreach (var val in enumerable)
//                        //    writer.WriteLine("{0} {1}", _converter.ToString(val.Value), val.Frequency);
//                        foreach (var val in enumerable.Select(w => w.Value))
//                            writer.WriteLine(_converter.ToString(val));
//                        writer.WriteLine();
//                    }
//                }
//            }

//            //foreach (var prefix in prefixes)
//            //{
//            //    var words = _autoCompleteService.GetWordsByPrefix(prefix);
//            //    if (words != null)
//            //    {
//            //        var enumerable = words as Word<T>[] ?? words.ToArray();
//            //        if (enumerable.Any())
//            //        {
//            //            //foreach (var val in enumerable)
//            //            //    writer.WriteLine("{0} {1}", _converter.ToString(val.Value), val.Frequency);
//            //            foreach (var val in enumerable.Select(w => w.Value))
//            //                writer.WriteLine(_converter.ToString(val));
//            //            writer.WriteLine();
//            //        }
//            //    }
//            //}


//        }
//    }
//}