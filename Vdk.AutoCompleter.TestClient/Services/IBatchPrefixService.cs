using System.IO;
using Vdk.AutoCompleter.Common.IOC;

namespace Vdk.AutoCompleter.TestClient.Services
{
    public interface IBatchPrefixService:IDependency
    {
        void Run(TextReader reader, TextWriter writer);
    }
}