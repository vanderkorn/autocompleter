using System;
using System.Collections.Generic;
using System.Linq;
using Vdk.AutoCompleter.Common.IOC;
using Vdk.AutoCompleter.Core.Services;
using Vdk.AutoCompleter.Thrift.Core;

namespace Vdk.AutoCompleter.Thrift.Server
{
    public class AutoCompleteServiceImplementation : AutoCompleteService.Iface
    {
        private readonly IAutoCompleteService<string> _autoCompleteService;
        public AutoCompleteServiceImplementation()
        {
            _autoCompleteService = ServiceLocator.Resolve<IAutoCompleteService<string>>();
        }

        public List<string> Get(string prefix)
        {
            var result = _autoCompleteService.GetWordsByPrefix(prefix);
            if (result != null) return result.Select(w => w.Value).ToList();
            return new List<string>();
        }
    }
}
