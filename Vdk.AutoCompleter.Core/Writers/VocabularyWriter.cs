// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VocabularyWriter.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   The vocabulary writer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core.Writers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Vdk.AutoCompleter.Core.Converters;
    using Vdk.AutoCompleter.Core.Models;
    using Vdk.AutoCompleter.Core.Services;

    /// <summary>
    /// The vocabulary result writer interface.
    /// </summary>
    /// <typeparam name="T">
    /// Type of element
    /// </typeparam>
    public class VocabularyWriter<T> : IVocabularyWriter<T> where T : IComparable<T>
    {
        /// <summary>
        /// The autocomplete service.
        /// </summary>
        private readonly IAutoCompleteService<T> autoCompleteService;

        /// <summary>
        /// The converter.
        /// </summary>
        private readonly IWordValueConverter<T> converter;

        /// <summary>
        /// Initializes a new instance of the <see cref="VocabularyWriter{T}"/> class.
        /// </summary>
        /// <param name="autoCompleteService">
        /// The auto complete service.
        /// </param>
        /// <param name="converter">
        /// The converter.
        /// </param>
        public VocabularyWriter(IAutoCompleteService<T> autoCompleteService, IWordValueConverter<T> converter)
        {
            this.autoCompleteService = autoCompleteService;
            this.converter = converter;
        }


        /// <summary>
        /// The get words by prefixes.
        /// </summary>
        /// <param name="prefixes">
        /// The prefixes.
        /// </param>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public void GetWordsByPrefixes(IEnumerable<T> prefixes, TextWriter writer)
        {
            foreach (var prefix in prefixes)
            {
                var words = this.autoCompleteService.GetWordsByPrefix(prefix);
                if (words != null)
                {
                    var enumerable = words as Word<T>[] ?? words.ToArray();
                    if (enumerable.Any())
                    {
                        foreach (var val in enumerable.Select(w => w.Value))
                        {
                            writer.WriteLine(this.converter.ToString(val));
                        }
                        writer.WriteLine();
                    }
                }
            }
        }
    }
}