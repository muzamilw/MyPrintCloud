using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Get Items List View Repository 
    /// </summary>
    public interface IGetItemsListViewRepository : IBaseRepository<GetItemsListView, long>
    {
        /// <summary>
        /// Get Items
        /// </summary>
        ItemListViewSearchResponse GetItems(ItemSearchRequestModel request);

    }
}
