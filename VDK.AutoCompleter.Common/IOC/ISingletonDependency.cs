namespace VDK.AutoCompleter.Common.IOC
{
    /// <summary>
    /// Base interface for services that are instantiated per shell/tenant.
    /// </summary>
    public interface ISingletonDependency : IDependency {
    }
}