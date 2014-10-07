using System.Collections.Generic;

namespace Vdk.AutoCompleter.Core.NGramGenerators
{
    public interface INGramParser<T> 
    {
        IEnumerable<T> GetLexemes(T word);
    }
}