using System.Collections.Generic;
using Thrift.Protocol;
using Thrift.Transport;
using Vdk.AutoCompleter.Common;
using Vdk.AutoCompleter.Thrift.Core;

namespace Vdk.AutoCompleter.Thrift.ClientModule
{
    public class ThriftClientApplication : IApplicationClient<string>
    {
        private AutoCompleteService.Client _client;

        public IList<string> Get(string prefix)
        {
            return _client.Get(prefix);
        }

        public void Connect(string host, int port)
        {
            TTransport transport = new TSocket(host, port);
            transport.Open();

            var proto = new TBinaryProtocol(transport);
            _client = new AutoCompleteService.Client(proto);
        }

        public void Dispose()
        {
            if (_client != null) _client.Dispose();
        }
    }
}