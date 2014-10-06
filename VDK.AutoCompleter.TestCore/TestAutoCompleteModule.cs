using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vdk.AutoCompleter.Common.IOC;
using Vdk.AutoCompleter.Core;
using Vdk.AutoCompleter.Core.Models;
using Vdk.AutoCompleter.Core.Services;

namespace Vdk.AutoCompleter.TestCore
{
    [TestClass]
    public class TestAutoCompleteModule
    {
        [TestInitialize]
        [DeploymentItem(@"test.in")]
        [DeploymentItem(@"Data\", "Data")]
        public void Initialize()
        {
            CoreInitializer.Initialize(Dependencies);
        }

        private void Dependencies(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutoCompleteModule());
        }

        [TestMethod]
        [DeploymentItem(@"test.in")]
        [DeploymentItem(@"Data\", "Data")]
        public void TestModule()
        {
            long bytes1 = GC.GetTotalMemory(false); // Get memory in bytes





           Stopwatch stopwatch = new Stopwatch();
           stopwatch.Start();

            var reader = ServiceLocator.Resolve<IVocabularyReader<string>>();
            reader.AddVocabulary(File.OpenText(@"Data\test.in"));

            stopwatch.Stop();
             Console.WriteLine("Time read elapsed: {0}", stopwatch.Elapsed);

            var lexemes = reader.GetTestPrefixes();
            var enumerable = lexemes as IList<string> ?? lexemes.ToList();
            Assert.IsTrue(enumerable.Any());

            stopwatch.Reset();
            stopwatch.Start();
            var writer = ServiceLocator.Resolve<IVocabularyWriter<string>>();
            writer.GetWordsByPrefixes(enumerable, File.CreateText("test.out"));

            stopwatch.Stop();
            Console.WriteLine("Time write elapsed: {0}", stopwatch.Elapsed);

            long bytes2 = GC.GetTotalMemory(false); // Get memory
            long bytes3 = GC.GetTotalMemory(true); // Get memory

            Console.WriteLine(bytes1);
            Console.WriteLine(bytes2);
            Console.WriteLine(bytes2 - bytes1); // Write difference
            Console.WriteLine(bytes3);
            Console.WriteLine(bytes3 - bytes2); // Write difference
          


        }
    }
}
