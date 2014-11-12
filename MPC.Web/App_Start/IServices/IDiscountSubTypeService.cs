
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Discount Sub Type Service Interface
    /// </summary>
    public interface IDiscountSubTypeService
    {
        /// <summary>
        /// Load Discount Sub Type Base Data
        /// </summary>
        DiscountSubTypeBaseDataResponse LoadDiscountSubTypeBaseData();

        /// <summary>
        /// Discount Sub Type Search
        /// </summary>
        DiscountSubTypeSearchRequestResponse SearchDiscountSubType(DiscountSubTypeSearchRequest request);

        /// <summary>
        /// Delete Discount Sub Type by id
        /// </summary>
        void DeleteDiscountSubType(long discountSubTypeId);

        /// <summary>
        /// Add / Update Discount Sub Type
        /// </summary>
        DiscountSubType SaveDiscountSubType(DiscountSubType discountSubType);

    }
}
