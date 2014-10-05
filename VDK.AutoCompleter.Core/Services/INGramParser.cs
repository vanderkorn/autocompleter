using System.Collections.Generic;
using VDK.AutoCompleter.Common.IOC;

namespace VDK.AutoCompleter.Core.Services
{
    public interface INGramParser : IDependency
    {
        IEnumerable<string> GetLexemes(string word);
    }
}