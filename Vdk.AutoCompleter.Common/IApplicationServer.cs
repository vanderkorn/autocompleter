using System.Collections;
using NLog;
using Vdk.AutoCompleter.Common.IOC;

namespace Vdk.AutoCompleter.Common
{
    public interface IApplicationServer : IDependency
    {
        Logger Logger { get; set; }
        int Throughput { get; set; }
        void Run(string inputFile, int port);
        void Stop();
    }
}
