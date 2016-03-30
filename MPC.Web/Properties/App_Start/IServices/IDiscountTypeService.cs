
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Discount Type Service Interface
    /// </summary>
    public interface IDiscountTypeService
    {
        /// <summary>
        /// Search Discount Type 
        /// </summary>
        DiscountTypeSearchRequestResponse SearchDiscountType(DiscountTypeSearchRequest request);

        /// <summary>
        /// Delete Discount Type by id
        /// </summary>
        void DeleteDiscountType(long discountTypeId);

        /// <summary>
        /// Add /Update Discount Type
        /// </summary>
        DiscountType SaveDiscountType(DiscountType discountType);

    }
}
