using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;


namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Item Job Status Service
    /// </summary>
    public class ItemJobStatusService : IItemJobStatusService
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly IOrderRepository orderRepository;
        private readonly IItemRepository itemRepository;

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public ItemJobStatusService(IOrderRepository orderRepository, IItemRepository itemRepository)
        {
            this.orderRepository = orderRepository;
            this.itemRepository = itemRepository;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Items For Item Job Status 
        /// </summary>
        public IEnumerable<ItemForItemJobStatus> GetItemsForItemJobStatus()
        {
            IEnumerable<Estimate> estimates = orderRepository.GetEstimatesForItemJobStatus();
            List<ItemForItemJobStatus> itemForItemJobStatuses = new List<ItemForItemJobStatus>();

            foreach (var estimate in estimates)
            {
                if (estimate.Items != null)
                {
                    foreach (var item in estimate.Items)
                    {
                        ItemForItemJobStatus itemForItemJobStatus = new ItemForItemJobStatus()
                        {
                            EstimateId = item.EstimateId,
                            ItemId = item.ItemId,
                            Code = estimate.Estimate_Code + item.ItemCode,
                            CompanyName = item.Company != null ? item.Company.Name : string.Empty,
                            ProductName = item.ProductName,
                            Qty1 = item.Qty1,
                            StatusId = item.StatusId,
                            JobEstimatedCompletionDateTime = item.JobEstimatedCompletionDateTime,
                            Qty1NetTotal = item.Qty1NetTotal
                        };
                        itemForItemJobStatuses.Add(itemForItemJobStatus);
                    }
                }
            }

            return itemForItemJobStatuses.OrderBy(i => i.JobEstimatedCompletionDateTime);
        }


        /// <summary>
        /// Update Item Status
        /// </summary>
        public void UpdateItem(ItemForItemJobStatus item)
        {
            Item itemDbVersion = itemRepository.Find(item.ItemId);
            if (itemDbVersion != null)
            {
                itemDbVersion.StatusId = item.StatusId;
                itemRepository.SaveChanges();
            }
        }
        #endregion
    }
}
