// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConcurentVocabularyReader.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the ConcurentVocabularyReader type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Readers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using Vdk.AutoCompleter.Common.Extensions;
    using Vdk.AutoCompleter.Core.Collections;
    using Vdk.AutoCompleter.Core.Converters;
    using Vdk.AutoCompleter.Core.Models;
    using Vdk.AutoCompleter.Core.Services;

    /// <summary>
    /// The concurent vocabulary reader.
    /// </summary>
    /// <typeparam name="T">
    /// Type of element
    /// </typeparam>
    public class ConcurentVocabularyReader<T> : IVocabularyReader<T> where T : IComparable<T>
    {
        /// <summary>
        /// The autocomplete service.
        /// </summary>
        private readonly IAutoCompleteService<T> autoCompleteService;

        /// <summary>
        /// The converter words.
        /// </summary>
        private readonly IWordValueConverter<T> converter;

        /// <summary>
        /// The test prefixes list.
        /// </summary>
        private readonly IList<T> testPrefixesList;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurentVocabularyReader{T}"/> class.
        /// </summary>
        /// <param name="autoCompleteService">
        /// The auto complete service.
        /// </param>
        /// <param name="converter">
        /// The converter.
        /// </param>
        public ConcurentVocabularyReader(IAutoCompleteService<T> autoCompleteService, IWordValueConverter<T> converter)
        {
            this.autoCompleteService = autoCompleteService;
            this.converter = converter;
            this.testPrefixesList = new ConcurrentList<T>();
        }

        /// <summary>
        /// The add vocabulary.
        /// </summary>
        /// <param name="reader">
        /// The reader vocabulary.
        /// </param>
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
                    this.autoCompleteService.SetCountWords(n);
                }
                else if (lineNumber <= n)
                {
                    var result = line.Split(' ');
                    var word = new Word<T>()
                    {
                        Value = this.converter.FromString(result[0]),
                        Frequency = uint.Parse(result[1])
                    };
                    var task = new Task(() => this.autoCompleteService.AddWordToVocabulary(word));
                    task.Start();
                    tasks.Add(task);
                }
                else if (lineNumber == n + 1)
                {
                    m = ushort.Parse(line);
                }
                else if (lineNumber <= m + (n + 1))
                {
                    var word = this.converter.FromString(line);
                    var task = new Task(() => this.testPrefixesList.Add(word));
                    task.Start();
                    tasks.Add(task);
                }
                else break;

                lineNumber++;
            }

            Task.WaitAll(tasks.ToArray());
        }

        /// <summary>
        /// The get test prefixes.
        /// </summary>
        /// <returns>
        /// The test prefixes.
        /// </returns>
        public IEnumerable<T> GetTestPrefixes()
        {
            return this.testPrefixesList;
        }
    }
}