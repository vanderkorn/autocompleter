using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vdk.AutoCompleter.Common;
using Vdk.AutoCompleter.Common.IOC;
using Vdk.AutoCompleter.Wcf.ClientModule;

namespace Vdk.AutoCompleter.Wcf.TestServer
{
    [TestClass]
    public class WcftServerTest
    {
        private const string Host = "localhost";
        private const int Port = 813;

        [TestInitialize]
        public void Initialize()
        {
            CoreInitializer.Initialize(Dependencies);
        }

        private void Dependencies(ContainerBuilder builder)
        {
            builder.RegisterModule(new WcfClientApplicationModule());
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

        [TestMethod]
        public void TestMethod3()
        {
            var tasks = new List<Task>();

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
            foreach (var prefix in prefixes)
            {
                for (int i = 0; i < 10; i++)
                {
                    string prefix1 = prefix;
                    var task = new Task(() =>
                    {
                        try
                        {
                            using (var scope = ServiceLocator.GetContainer().BeginLifetimeScope())
                            {
                                var app = scope.Resolve<IApplicationClient<string>>();
                                app.Connect(Host, Port);
                                var res = app.Get(prefix1);
                                Assert.IsNotNull(res);
                                Assert.IsTrue(res.Any());
                            }

                        }
                        catch (Exception ex)
                        {

                            throw;
                        }

                    });
                    tasks.Add(task);

                }

            }
            tasks.ForEach(t => t.Start());
            Task.WaitAll(tasks.ToArray());


        }
    }
}
