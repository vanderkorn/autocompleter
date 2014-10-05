using System.IO;
using VDK.AutoCompleter.Common.IOC;

namespace VDK.AutoCompleter.TestClient.Services
{
    public interface IBatchPrefixService:IDependency
    {
        void Run(TextReader reader, TextWriter writer);
    }
}