using System.Collections.Generic;
using System.IO;
using Vdk.AutoCompleter.Common.IOC;

namespace Vdk.AutoCompleter.Core.Services
{
    public interface IVocabularyReader<T> : IDependency
    {
        void AddVocabulary(TextReader reader);
        IEnumerable<T> GetTestPrefixes();
    }
}