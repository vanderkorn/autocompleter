using System.Collections;
using System.Collections.Generic;
using Autofac;
using Vdk.AutoCompleter.Common.IOC;
using Vdk.AutoCompleter.Core.Comparers;
using Vdk.AutoCompleter.Core.Converters;
using Vdk.AutoCompleter.Core.Models;
using Vdk.AutoCompleter.Core.NGramGenerators;
using Vdk.AutoCompleter.Core.Readers;
using Vdk.AutoCompleter.Core.Services;
using Vdk.AutoCompleter.Core.Writers;

namespace Vdk.AutoCompleter.Core
{
    public class AutoCompleteModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
           // builder.RegisterSource(new ResolveDependenciesSource());

            builder.RegisterType<AsciiComparerFactory>()
                      .As<IComparerFactory<AsciiString>>()
                      .InstancePerDependency();

            builder.RegisterType<ComparerFactory>()
                      .As<IComparerFactory<string>>()
                      .InstancePerDependency();

            builder.RegisterType<AsciiWordValueConverter>()
                            .As<IWordValueConverter<AsciiString>>()
                            .InstancePerDependency();

            builder.RegisterType<WordValueConverter>()
                            .As<IWordValueConverter<string>>()
                            .InstancePerDependency();



            builder.RegisterType<NGramParser>()
                   .WithParameter("minLength", 1)
                   .WithParameter("maxLength", 15)
                   .As<INGramParser<string>>()
                   .InstancePerDependency();

            builder.RegisterType<AsciiNGramParser>()
                   .WithParameter("minLength", 1)
                   .WithParameter("maxLength", 15)
                   .As<INGramParser<AsciiString>>()
                   .InstancePerDependency();



            //builder.RegisterGeneric(typeof(AutoCompleteService<>))
            //    .As(typeof(IAutoCompleteService<>))
            //    .SingleInstance();

            //builder.RegisterGeneric(typeof(VocabularyWriter<>))
            //   .As(typeof(IVocabularyWriter<>))
            //   .InstancePerDependency();

            //builder.RegisterGeneric(typeof(VocabularyReader<>))
            //   .As(typeof(IVocabularyReader<>))
            //   .InstancePerDependency();



            builder.RegisterGeneric(typeof(ConcurentAutoCompleteService<>))
                .As(typeof(IAutoCompleteService<>))
                .SingleInstance();

            builder.RegisterGeneric(typeof(VocabularyWriter<>))
               .As(typeof(IVocabularyWriter<>))
               .InstancePerDependency();

            builder.RegisterGeneric(typeof(ConcurentVocabularyReader<>))
               .As(typeof(IVocabularyReader<>))
               .InstancePerDependency();



            //builder.RegisterType<AutoCompleteService<string>>().As<IAutoCompleteService<string>>().SingleInstance();
            //builder.RegisterType<VocabularyWriter<string>>().As<IVocabularyWriter<string>>().InstancePerDependency();
            //builder.RegisterType<VocabularyReader<string>>().As<IVocabularyReader<string>>().InstancePerDependency();
            //builder.RegisterType<NGramParser>()
            //       .WithParameter("minLength", 1)
            //       .WithParameter("maxLength", 15)
            //       .As<INGramParser>()
            //       .InstancePerDependency();

            //builder.RegisterType<AutoCompleteService>().As<IAutoCompleteService>().SingleInstance();
            //builder.RegisterType<VocabularyWriter>().As<IVocabularyWriter>().InstancePerDependency();
            //builder.RegisterType<VocabularyReader>().As<IVocabularyReader>().InstancePerDependency();
        }
    }
}
