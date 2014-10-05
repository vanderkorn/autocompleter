using Autofac;
using VDK.AutoCompleter.Common.IOC;
using VDK.AutoCompleter.Core.Services;

namespace VDK.AutoCompleter.Core
{
    public class AutoCompleteModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
           // builder.RegisterSource(new ResolveDependenciesSource());

            builder.RegisterType<StartEdgeNGramParser>()
                   .WithParameter("minLength", 1)
                   .WithParameter("maxLength", 15)
                   .As<INGramParser>()
                   .InstancePerDependency();

            builder.RegisterType<AutoCompleteService>().As<IAutoCompleteService>().SingleInstance();
            builder.RegisterType<VocabularyWriter>().As<IVocabularyWriter>().InstancePerDependency();
            builder.RegisterType<VocabularyReader>().As<IVocabularyReader>().InstancePerDependency();
        }
    }
}
