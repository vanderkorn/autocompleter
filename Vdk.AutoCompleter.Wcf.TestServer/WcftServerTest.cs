// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WcftServerTest.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the WcftServerTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Wcf.TestServer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Autofac;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Vdk.AutoCompleter.Common;
    using Vdk.AutoCompleter.Common.IOC;
    using Vdk.AutoCompleter.Wcf.ClientModule;

    /// <summary>
    /// The wcf server tests.
    /// </summary>
    [TestClass]
    public class WcftServerTest
    {
        /// <summary>
        /// The host.
        /// </summary>
        private const string Host = "localhost";

        /// <summary>
        /// The port.
        /// </summary>
        private const int Port = 813;

        /// <summary>
        /// The initialize test.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            CoreInitializer.Initialize(this.Dependencies);
        }

        /// <summary>
        /// The test over method Get
        /// </summary>
        [TestMethod]
        public void GetTest()
        {
            var app = ServiceLocator.Resolve<IApplicationClient<string>>();
            app.Connect(Host, Port);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var res = app.Get("ab");
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Any());
            stopwatch.Stop();
            Console.WriteLine("Time read elapsed: {0}", stopwatch.Elapsed);
            foreach (var r in res)
                Console.WriteLine(r);
        }

        /// <summary>
        /// The multithreaded short test over method Get.
        /// </summary>
        [TestMethod]
        public void GetThreadedTest()
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

        /// <summary>
        /// The multithreaded long test over method Get.
        /// </summary>
        [TestMethod]
        public void GetLongThreadedTest()
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

        /// <summary>
        /// The register dependencies.
        /// </summary>
        /// <param name="builder">
        /// The IOC container builder.
        /// </param>
        private void Dependencies(ContainerBuilder builder)
        {
            builder.RegisterModule(new WcfClientApplicationModule());
        }
    }
}
