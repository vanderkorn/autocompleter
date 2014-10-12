using System.IO;
using Vdk.AutoCompleter.Common.IOC;

namespace Vdk.AutoCompleter.Common
{
    public interface IApplicationTestClient<T> : IDependency
    {
        void Run(TextReader reader, TextWriter writer);
    }
}