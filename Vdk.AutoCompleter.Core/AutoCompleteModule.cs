// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoCompleteModule.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the AutoCompleteModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Core
{
    using Autofac;
    using Vdk.AutoCompleter.Core.Comparers;
    using Vdk.AutoCompleter.Core.Converters;
    using Vdk.AutoCompleter.Core.Models;
    using Vdk.AutoCompleter.Core.NGramGenerators;
    using Vdk.AutoCompleter.Core.Readers;
    using Vdk.AutoCompleter.Core.Services;
    using Vdk.AutoCompleter.Core.Writers;

    /// <summary>
    /// The autocomplete AUTOFAC module.
    /// </summary>
    public class AutoCompleteModule : Module
    {
        /// <summary>
        /// The load module.
        /// </summary>
        /// <param name="builder">
        /// The container builder.
        /// </param>
        protected override void Load(ContainerBuilder builder)
        {
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

            builder.RegisterGeneric(typeof(ConcurentAutoCompleteService<>))
                .As(typeof(IAutoCompleteService<>))
                .SingleInstance();

            builder.RegisterGeneric(typeof(VocabularyWriter<>))
               .As(typeof(IVocabularyWriter<>))
               .InstancePerDependency();

            builder.RegisterGeneric(typeof(ConcurentVocabularyReader<>))
               .As(typeof(IVocabularyReader<>))
               .InstancePerDependency();
        }
    }
}
