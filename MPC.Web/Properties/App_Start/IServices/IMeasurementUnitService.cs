using System.Collections.Generic;
using Cares.Models.DomainModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Measurement Unit Service Interface
    /// </summary>
    public interface IMeasurementUnitService
    {
        /// <summary>
        /// Load All Measurement Unit
        /// </summary>
        /// <returns></returns>
        IEnumerable<MeasurementUnit> LoadAll();
    }
}
