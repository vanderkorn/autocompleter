// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoCompleteServiceImplementation.cs" company="Ivan Kornilov">
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the AutoCompleteServiceImplementation type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Thrift.ServerModule
{
    using System.Collections.Generic;
    using System.Linq;
    using Vdk.AutoCompleter.Common.IOC;
    using Vdk.AutoCompleter.Core.Services;
    using Vdk.AutoCompleter.Thrift.Core;

    /// <summary>
    /// The autocomplete THRIFT service implementation.
    /// </summary>
    public class AutoCompleteServiceImplementation : AutoCompleteService.Iface
    {
        /// <summary>
        /// The autocomplete service.
        /// </summary>
        private readonly IAutoCompleteService<string> autoCompleteService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteServiceImplementation"/> class.
        /// </summary>
        public AutoCompleteServiceImplementation()
        {
            this.autoCompleteService = ServiceLocator.Resolve<IAutoCompleteService<string>>();
        }

        /// <summary>
        /// The get words.
        /// </summary>
        /// <param name="prefix">
        /// The prefix.
        /// </param>
        /// <returns>
        /// The words.
        /// </returns>
        public List<string> Get(string prefix)
        {
            var result = this.autoCompleteService.GetWordsByPrefix(prefix);
            return result != null ? result.Select(w => w.Value).ToList() : new List<string>();
        }
    }
}
