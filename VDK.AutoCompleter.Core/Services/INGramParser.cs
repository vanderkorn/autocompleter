using System.Collections.Generic;
using Vdk.AutoCompleter.Common.IOC;

namespace Vdk.AutoCompleter.Core.Services
{
    public interface INGramParser<T> 
    {
        IEnumerable<T> GetLexemes(T word);
    }
}