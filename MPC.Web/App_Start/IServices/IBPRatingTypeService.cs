using System.Collections.Generic;
using System.Linq;
using Cares.Models.DomainModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Business partner Rating Type Service Interface
    /// </summary>
    public interface IBPRatingTypeService
    {
        IEnumerable<BpRatingType> LoadAll();
    }
}
