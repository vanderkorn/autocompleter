using System;
using System.Collections.Generic;
using Vdk.AutoCompleter.Common.IOC;

namespace Vdk.AutoCompleter.Common
{
    public interface IApplicationClient<T> : IDependency, IDisposable
    {
        IList<T> Get(string prefix);
        void Connect(string host, int port);
    }
}