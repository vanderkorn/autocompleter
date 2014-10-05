using System.Collections.Generic;
using System.IO;
using VDK.AutoCompleter.Common.IOC;

namespace VDK.AutoCompleter.Core.Services
{
    public interface IVocabularyWriter : IDependency
    {
        void GetWordsByPrefixes(IEnumerable<string> prefixes, TextWriter writer);
    }
}