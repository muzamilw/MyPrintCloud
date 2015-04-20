using System.Collections.Generic;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    /// <summary>
    /// Item Job Status Service Interface
    /// </summary>
    public interface IItemJobStatusService
    {
        /// <summary>
        /// Get Items For Item Job Status 
        /// </summary>
        IEnumerable<ItemForItemJobStatus> GetItemsForItemJobStatus();


        /// <summary>
        /// Update Item Status
        /// </summary>
        void UpdateItem(ItemForItemJobStatus item);
    }
}
