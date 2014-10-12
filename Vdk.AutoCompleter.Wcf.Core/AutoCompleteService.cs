using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Vdk.AutoCompleter.Core.Services;

namespace Vdk.AutoCompleter.Wcf.Core
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)] 
    public class AutoCompleteWcfService : IAutoCompleteWcfService
    {
        private readonly IAutoCompleteService<string> _autoCompleteService;

        //public AutoCompleteWcfService()
        //{
        //}
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
