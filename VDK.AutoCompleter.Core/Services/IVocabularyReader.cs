using System.Collections.Generic;
using System.IO;
using VDK.AutoCompleter.Common.IOC;

namespace VDK.AutoCompleter.Core.Services
{
    public interface IVocabularyReader : IDependency
    {
        void AddVocabulary(TextReader reader);
        IEnumerable<string> GetTestPrefixes();
    }
}