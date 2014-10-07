using System.Collections.Generic;
using System.IO;
using Vdk.AutoCompleter.Common.IOC;

namespace Vdk.AutoCompleter.Core.Readers
{
    public interface IVocabularyReader<out T> : IDependency
    {
        void AddVocabulary(TextReader reader);
        IEnumerable<T> GetTestPrefixes();
    }
}