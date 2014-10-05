using System.Collections.Generic;
using System.IO;
using System.Linq;
using VDK.AutoCompleter.Core.Services;

namespace VDK.AutoCompleter.TestClient.Services
{
    public class BatchPrefixService : IBatchPrefixService
    {
        private readonly IVocabularyReader _reader;
        private readonly IVocabularyWriter _writer;

        public BatchPrefixService(IVocabularyReader reader, IVocabularyWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }

        public void Run(TextReader reader, TextWriter writer)
        {
            _reader.AddVocabulary(reader);
            var lexemes = _reader.GetTestPrefixes();
            var enumerable = lexemes as IList<string> ?? lexemes.ToList();
            _writer.GetWordsByPrefixes(enumerable, writer);
        }
    }
}
