
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Area Service Interface
    /// </summary>
    public interface IServiceItemService 
    {
        // <summary>
        // Load Service Item Base Data
        // </summary>
        ServiceItemBaseDataResponse LoadServiceItemBaseData();

        // <summary>
        // Search Service Item
        // </summary>
        ServiceItemSearchRequestResponse SearchServiceItem(ServiceItemSearchRequest request);

        // <summary>
        // Delete Service Item by id
        // </summary>
        void DeleteServiceItem(long serviceItemId);

        // <summary>
        // Add /Update Service Item
        // </summary>
        ServiceItem SaveServiceItem(ServiceItem serviceItem);

    }
}
