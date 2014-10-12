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
using Vdk.AutoCompleter.Core.Readers;
using Vdk.AutoCompleter.Core.Services;
using Vdk.AutoCompleter.Core.Writers;

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
        public void TestModuleFull()
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

        [TestMethod]
        [DeploymentItem(@"test.in")]
        [DeploymentItem(@"Data\", "Data")]
        public void TestModuleFull2()
        {
            long bytes1 = GC.GetTotalMemory(false); // Get memory in bytes

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var reader = ServiceLocator.Resolve<IVocabularyReader<AsciiString>>();
            reader.AddVocabulary(File.OpenText(@"Data\test.in"));

            stopwatch.Stop();
            Console.WriteLine("Time read elapsed: {0}", stopwatch.Elapsed);

            var lexemes = reader.GetTestPrefixes();
            var enumerable = lexemes as IList<AsciiString> ?? lexemes.ToList();
            Assert.IsTrue(enumerable.Any());

            stopwatch.Reset();
            stopwatch.Start();
            var writer = ServiceLocator.Resolve<IVocabularyWriter<AsciiString>>();
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

        [TestMethod]
        [DeploymentItem(@"test.in")]
        [DeploymentItem(@"Data\", "Data")]
        public void TestModule()
        {
            var reader = ServiceLocator.Resolve<IVocabularyReader<string>>();
            reader.AddVocabulary(File.OpenText(@"Data\test.in"));

            var lexemes = reader.GetTestPrefixes();
            var enumerable = lexemes as IList<string> ?? lexemes.ToList();
            Assert.IsTrue(enumerable.Any());
        }

        [TestMethod]
        [DeploymentItem(@"test.in")]
        [DeploymentItem(@"Data\", "Data")]
        public void TestModule2()
        {
            var reader = ServiceLocator.Resolve<IVocabularyReader<AsciiString>>();
            reader.AddVocabulary(File.OpenText(@"Data\test.in"));

            var lexemes = reader.GetTestPrefixes();
            var enumerable = lexemes as IList<AsciiString> ?? lexemes.ToList();
            Assert.IsTrue(enumerable.Any());
        }

        [TestMethod]
        [DeploymentItem(@"test.in")]
        [DeploymentItem(@"Data\", "Data")]
        public void TimeTestModule()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var reader = ServiceLocator.Resolve<IVocabularyReader<string>>();
            reader.AddVocabulary(File.OpenText(@"Data\test.in"));
   
            var lexemes = reader.GetTestPrefixes();
            var enumerable = lexemes as IList<string> ?? lexemes.ToList();

            var writer = ServiceLocator.Resolve<IVocabularyWriter<string>>();
            writer.GetWordsByPrefixes(enumerable, File.CreateText("test.out"));

            stopwatch.Stop();
            Console.WriteLine("Time write elapsed: {0}", stopwatch.Elapsed);
            Assert.IsTrue(stopwatch.ElapsedMilliseconds <= 10000);
        }

        [TestMethod]
        [DeploymentItem(@"test.in")]
        [DeploymentItem(@"Data\", "Data")]
        public void TimeTestModule2()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var reader = ServiceLocator.Resolve<IVocabularyReader<AsciiString>>();
            reader.AddVocabulary(File.OpenText(@"Data\test.in"));

            var lexemes = reader.GetTestPrefixes();
            var enumerable = lexemes as IList<AsciiString> ?? lexemes.ToList();

            var writer = ServiceLocator.Resolve<IVocabularyWriter<AsciiString>>();
            writer.GetWordsByPrefixes(enumerable, File.CreateText("test.out"));

            stopwatch.Stop();
            Console.WriteLine("Time write elapsed: {0}", stopwatch.Elapsed);
            Assert.IsTrue(stopwatch.ElapsedMilliseconds <= 10000);
        }

        [TestMethod]
        [DeploymentItem(@"test.in")]
        [DeploymentItem(@"Data\", "Data")]
        public void TestModuleExample()
        {
            var autoCompleteService = ServiceLocator.Resolve<IAutoCompleteService<string>>();
            var words = new List<Word<string>>
            {
                new Word<string>()
                {
                    Value = "kare",
                    Frequency = 10
                },
                new Word<string>()
                {
                    Value = "kanojo",
                    Frequency = 20
                },
                 new Word<string>()
                {
                    Value = "karetachi",
                    Frequency = 1
                },
                  new Word<string>()
                {
                    Value = "korosu",
                    Frequency = 7
                },
                new Word<string>()
                {
                    Value = "sakura",
                    Frequency = 3
                }
            };

            foreach (var word in words)
                autoCompleteService.AddWordToVocabulary(word);

            var prefixes = new List<string>
            {
                "k",
                "ka",
                "kar"
            };

            var expectedResult = new Dictionary<string, List<string>>()
            {
               {prefixes[0] , new List<string>(){words[1].Value, words[0].Value, words[3].Value, words[2].Value}},
               {prefixes[1] , new List<string>(){words[1].Value, words[0].Value, words[2].Value}},
               {prefixes[2] , new List<string>(){ words[0].Value, words[2].Value}},

            };

            foreach (var prefix in prefixes)
            {
                var res = autoCompleteService.GetWordsByPrefix(prefix);
                Assert.IsNotNull(res);
                var result = res.Select(w => w.Value).ToList();
                Assert.IsTrue(result.Count == expectedResult[prefix].Count);
                Assert.IsTrue(result.SequenceEqual(expectedResult[prefix]));
            }
        }

        [TestMethod]
        [DeploymentItem(@"test.in")]
        [DeploymentItem(@"Data\", "Data")]
        public void TestModuleExample2()
        {
            var autoCompleteService = ServiceLocator.Resolve<IAutoCompleteService<AsciiString>>();
            var words = new List<Word<AsciiString>>
            {
                new Word<AsciiString>()
                {
                    Value = "kare",
                    Frequency = 10
                },
                new Word<AsciiString>()
                {
                    Value = "kanojo",
                    Frequency = 20
                },
                 new Word<AsciiString>()
                {
                    Value = "karetachi",
                    Frequency = 1
                },
                  new Word<AsciiString>()
                {
                    Value = "korosu",
                    Frequency = 7
                },
                new Word<AsciiString>()
                {
                    Value = "sakura",
                    Frequency = 3
                }
            };

            foreach (var word in words)
                autoCompleteService.AddWordToVocabulary(word);

            var prefixes = new List<AsciiString>
            {
                "k",
                "ka",
                "kar"
            };

            var expectedResult = new Dictionary<AsciiString, List<AsciiString>>()
            {
               {prefixes[0] , new List<AsciiString>(){words[1].Value, words[0].Value, words[3].Value, words[2].Value}},
               {prefixes[1] , new List<AsciiString>(){words[1].Value, words[0].Value, words[2].Value}},
               {prefixes[2] , new List<AsciiString>(){ words[0].Value, words[2].Value}},

            };

            foreach (var prefix in prefixes)
            {
                var res = autoCompleteService.GetWordsByPrefix(prefix);
                Assert.IsNotNull(res);
                var result = res.Select(w => w.Value).ToList();
                Assert.IsTrue(result.Count == expectedResult[prefix].Count);
                Assert.IsTrue(result.SequenceEqual(expectedResult[prefix]));
            }
        }
    }
}
