using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VDK.AutoCompleter.Common.IOC;
using VDK.AutoCompleter.Core;
using VDK.AutoCompleter.Core.Services;

namespace VDK.AutoCompleter.TestCore
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
           Stopwatch stopwatch = new Stopwatch();
           stopwatch.Start();

            var reader = ServiceLocator.Resolve<IVocabularyReader>();
            reader.AddVocabulary(File.OpenText(@"Data\test.in"));

            stopwatch.Stop();
             Console.WriteLine("Time read elapsed: {0}", stopwatch.Elapsed);

            var lexemes = reader.GetTestPrefixes();
            var enumerable = lexemes as IList<string> ?? lexemes.ToList();
            Assert.IsTrue(enumerable.Any());

            stopwatch.Reset();
            stopwatch.Start();
            var writer = ServiceLocator.Resolve<IVocabularyWriter>();
            writer.GetWordsByPrefixes(enumerable, File.CreateText("test.out"));

            stopwatch.Stop();
            Console.WriteLine("Time write elapsed: {0}", stopwatch.Elapsed);

        }
    }
}
