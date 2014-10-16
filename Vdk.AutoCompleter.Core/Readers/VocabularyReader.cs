// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VocabularyReader.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the VocabularyReader type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Readers
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Vdk.AutoCompleter.Common.Extensions;
    using Vdk.AutoCompleter.Core.Converters;
    using Vdk.AutoCompleter.Core.Models;
    using Vdk.AutoCompleter.Core.Services;

    /// <summary>
    /// The vocabulary reader.
    /// </summary>
    /// <typeparam name="T">
    /// Type of element
    /// </typeparam>
    public class VocabularyReader<T> : IVocabularyReader<T> where T : IComparable<T>
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
        /// Initializes a new instance of the <see cref="VocabularyReader{T}"/> class.
        /// </summary>
        /// <param name="autoCompleteService">
        /// The auto complete service.
        /// </param>
        /// <param name="converter">
        /// The converter.
        /// </param>
        public VocabularyReader(IAutoCompleteService<T> autoCompleteService, IWordValueConverter<T> converter)
        {
            this.autoCompleteService = autoCompleteService;
            this.converter = converter;
            this.testPrefixesList = new List<T>();
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
                this.autoCompleteService.AddWordToVocabulary(word);
                }
                else if (lineNumber == n + 1)
                {
                    m = ushort.Parse(line);
                }
                else if (lineNumber <= m + (n + 1))
                {
                    var word = this.converter.FromString(line);
                    this.testPrefixesList.Add(word);
                }
                else break;

                lineNumber++;
            }
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