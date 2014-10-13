// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAutoCompleteWcfService.cs" company="Ivan Kornilov">
//   Ivan Kornilov
//   Copyright ©  2014, Ivan Kornilov. All rights reserved.
// </copyright>
// <summary>
//   Defines the IAutoCompleteWcfService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vdk.AutoCompleter.Wcf.Core
{
    using System.Collections.Generic;
    using System.ServiceModel;

    /// <summary>
    /// The autocomplete WCF service interface.
    /// </summary>
    [ServiceContract]
    public interface IAutoCompleteWcfService
    {
        /// <summary>
        /// The get words.
        /// </summary>
        /// <param name="prefix">
        /// The prefix.
        /// </param>
        /// <returns>
        /// The  words.
        /// </returns>
        [OperationContract]
        IEnumerable<string> Get(string prefix);
    }
}
