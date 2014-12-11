using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    /// <summary>
    /// Item Service Interface
    /// </summary>
    public interface IItemService
    {
        /// <summary>
        /// Load Items, based on search filters
        /// </summary>
        ItemListViewSearchResponse GetItems(ItemSearchRequestModel request);

        /// <summary>
        /// Get by Id
        /// </summary>
        Item GetById(long id);

        /// <summary>
        /// Save Product Image
        /// </summary>
        Item SaveProductImage(string filePath, long itemId);

        /// <summary>
        /// Save Product
        /// </summary>
        Item SaveProduct(Item item);

        /// <summary>
        /// Archive Product
        /// </summary>
        void ArchiveProduct(long itemId);
    }
}
