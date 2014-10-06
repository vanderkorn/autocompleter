using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vdk.AutoCompleter.Core.Models;
using Vdk.AutoCompleter.Core.Services;

namespace Vdk.AutoCompleter.TestClient.Services
{
    public class BatchPrefixService : IBatchPrefixService
    {
        private readonly IVocabularyReader<AsciiString> _reader;
        private readonly IVocabularyWriter<AsciiString> _writer;

        public BatchPrefixService(IVocabularyReader<AsciiString> reader, IVocabularyWriter<AsciiString> writer)
        {
            _reader = reader;
            _writer = writer;
        }

        public void Run(TextReader reader, TextWriter writer)
        {
            _reader.AddVocabulary(reader);
            var lexemes = _reader.GetTestPrefixes();
            var enumerable = lexemes as IList<AsciiString> ?? lexemes.ToList();
            _writer.GetWordsByPrefixes(enumerable, writer);
        }
    }
}
