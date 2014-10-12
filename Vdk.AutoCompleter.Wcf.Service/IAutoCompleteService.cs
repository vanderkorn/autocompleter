using System.Collections.Generic;
using System.ServiceModel;

namespace Vdk.AutoCompleter.Wcf.Service
{
    [ServiceContract]
    public interface IAutoCompleteWcfService
    {
        [OperationContract]
        IEnumerable<string> Get(string prefix);
    }
}
