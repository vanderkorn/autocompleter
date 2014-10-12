using System.Collections.Generic;
using System.ServiceModel;

namespace Vdk.AutoCompleter.Wcf.Core
{
    [ServiceContract]
    public interface IAutoCompleteWcfService
    {
        [OperationContract]
        IEnumerable<string> Get(string prefix);
    }
}
