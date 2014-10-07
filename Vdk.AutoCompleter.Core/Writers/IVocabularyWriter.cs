using System.Collections.Generic;
using System.IO;
using Vdk.AutoCompleter.Common.IOC;

namespace Vdk.AutoCompleter.Core.Writers
{
    public interface IVocabularyWriter<T> : IDependency
    {
        void GetWordsByPrefixes(IEnumerable<T> prefixes, TextWriter writer);
    }
}