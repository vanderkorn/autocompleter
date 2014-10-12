using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vdk.AutoCompleter.Common;
using Vdk.AutoCompleter.Common.IOC;
using Vdk.AutoCompleter.Thrift.ClientModule;

namespace Vdk.AutoCompleter.Thrift.TestServer
{
    [TestClass]
    public class ThriftServerTest
    {
        private const string Host = "localhost";
        private const int Port = 823;

        [TestInitialize]
        public void Initialize()
        {
            CoreInitializer.Initialize(Dependencies);
        }

        private void Dependencies(ContainerBuilder builder)
        {
            builder.RegisterModule(new ThriftClientApplicationModule());
        }

        [TestMethod]
        public void TestMethod()
        {
            var app = ServiceLocator.Resolve<IApplicationClient<string>>();
            app.Connect(Host, Port);
            var res = app.Get("ab");
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Any());
            foreach (var r in res)
                Console.WriteLine(r);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var prefixes = new List<string>()
            {
                "ab",
                "a",
                "aca",
                "b",
                "ba",
                "bc",
                "cab",
                "c",
                "cb",
                "cabc"
            };
            Parallel.ForEach(prefixes, prefix =>
            {
                var app = ServiceLocator.Resolve<IApplicationClient<string>>();
                app.Connect(Host, Port);
                var res = app.Get(prefix);
                Assert.IsNotNull(res);
                Assert.IsTrue(res.Any());
              
            });
        }
    }
}
