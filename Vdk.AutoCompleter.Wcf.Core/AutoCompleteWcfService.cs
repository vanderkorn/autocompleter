// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoCompleteWcfService.cs" company="Ivan Kornilov">
//   Ivan Kornilov
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the AutoCompleteWcfService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Wcf.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;

    using NLog;

    using Vdk.AutoCompleter.Core.Services;

    /// <summary>
    /// The autocomplete WCF service.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)] 
    public class AutoCompleteWcfService : IAutoCompleteWcfService
    {
        /// <summary>
        /// The autocomplete service.
        /// </summary>
        private readonly IAutoCompleteService<string> autoCompleteService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteWcfService"/> class.
        /// </summary>
        /// <param name="autoCompleteService">
        /// The auto complete service.
        /// </param>
        public AutoCompleteWcfService(IAutoCompleteService<string> autoCompleteService)
        {
            this.autoCompleteService = autoCompleteService;
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        public Logger Logger { get; set; }

        /// <summary>
        /// The get words.
        /// </summary>
        /// <param name="prefix">
        /// The prefix.
        /// </param>
        /// <returns>
        /// The  words.
        /// </returns>
        public IEnumerable<string> Get(string prefix)
        {
            Logger.Debug("Get request with prefix = {0}", prefix);
            var result = this.autoCompleteService.GetWordsByPrefix(prefix);
            return result != null ? result.Select(w => w.Value) : new List<string>();
        }
    }
}
