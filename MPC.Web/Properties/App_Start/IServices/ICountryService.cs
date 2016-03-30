using System.Collections.Generic;
using Cares.Models.DomainModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Country Service Interface
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// Load all countries
        /// </summary>
        IEnumerable<Country> LoadAll();
    }
}
