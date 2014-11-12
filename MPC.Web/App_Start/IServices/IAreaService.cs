
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Area Service Interface
    /// </summary>
    public interface IAreaService
    {
        // <summary>
        // Load Area Base Data
        // </summary>
        AreaBaseDataResponse LoadAreaBaseData();

        // <summary>
        // Search Area
        // </summary>
        AreaSearchRequestResponse SearchArea(AreaSearchRequest request);

        // <summary>
        // Delete Area by id
        // </summary>
        void DeleteArea(long areaId);

        // <summary>
        // Add /Update Area
        // </summary>
        Area SaveArea(Area area);

    }
}
