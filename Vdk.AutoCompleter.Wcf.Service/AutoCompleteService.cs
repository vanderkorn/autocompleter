using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Vdk.AutoCompleter.Core.Services;

namespace Vdk.AutoCompleter.Wcf.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class AutoCompleteWcfService : IAutoCompleteWcfService
    {
        private readonly IAutoCompleteService<string> _autoCompleteService;

        public AutoCompleteWcfService(IAutoCompleteService<string> autoCompleteService)
        {
            _autoCompleteService = autoCompleteService;
        }

        public IEnumerable<string> Get(string prefix)
        {
            var result = _autoCompleteService.GetWordsByPrefix(prefix);
            if (result != null) return result.Select(w => w.Value);
            return new List<string>();
        }
    }
}
