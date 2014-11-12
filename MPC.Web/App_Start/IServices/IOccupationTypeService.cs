using System.Collections.Generic;
using Cares.Models.DomainModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Occupation Type Service Interface
    /// </summary>
    public interface IOccupationTypeService
    {
        /// <summary>
        /// Get All Occupation Types
        /// </summary>
        IEnumerable<OccupationType> LoadAll();
    }
}
