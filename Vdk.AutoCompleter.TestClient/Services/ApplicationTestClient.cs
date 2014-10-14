using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vdk.AutoCompleter.Common;
using Vdk.AutoCompleter.Core.Readers;
using Vdk.AutoCompleter.Core.Writers;

namespace Vdk.AutoCompleter.TestClient.Services
{
    public class ApplicationTestClient<T> : IApplicationTestClient
    {
        private readonly IVocabularyReader<T> _reader;
        private readonly IVocabularyWriter<T> _writer;

        public ApplicationTestClient(IVocabularyReader<T> reader, IVocabularyWriter<T> writer)
        {
            _reader = reader;
            _writer = writer;
        }

        public void Run(TextReader reader, TextWriter writer)
        {
            _reader.AddVocabulary(reader);
            var lexemes = _reader.GetTestPrefixes();
            var enumerable = lexemes as IList<T> ?? lexemes.ToList();
            _writer.GetWordsByPrefixes(enumerable, writer);
        }
    }
}
