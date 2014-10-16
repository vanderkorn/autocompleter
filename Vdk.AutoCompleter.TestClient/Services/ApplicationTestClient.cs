// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationTestClient.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   The application test client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.TestClient.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Vdk.AutoCompleter.Common;
    using Vdk.AutoCompleter.Core.Readers;
    using Vdk.AutoCompleter.Core.Writers;

    /// <summary>
    /// The application test client.
    /// </summary>
    /// <typeparam name="T">
    /// Type of element (word)
    /// </typeparam>
    public class ApplicationTestClient<T> : IApplicationTestClient<T>
    {
        /// <summary>
        /// The vocabulary reader.
        /// </summary>
        private readonly IVocabularyReader<T> _reader;

        /// <summary>
        /// The writer words.
        /// </summary>
        private readonly IVocabularyWriter<T> _writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationTestClient{T}"/> class.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public ApplicationTestClient(IVocabularyReader<T> reader, IVocabularyWriter<T> writer)
        {
            this._reader = reader;
            this._writer = writer;
        }

        /// <summary>
        /// The run TestClient.
        /// </summary>
        /// <param name="reader">
        /// The reader vocabulary and test data.
        /// </param>
        /// <param name="writer">
        /// The writer results.
        /// </param>
        public void Run(TextReader reader, TextWriter writer)
        {
            this._reader.AddVocabulary(reader);
            var lexemes = this._reader.GetTestPrefixes();
            var enumerable = lexemes as IList<T> ?? lexemes.ToList();
            this._writer.GetWordsByPrefixes(enumerable, writer);
        }
    }
}
