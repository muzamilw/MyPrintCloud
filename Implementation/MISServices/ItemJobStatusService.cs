using System;
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
        private readonly IOrganisationRepository organisationRepository;

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public ItemJobStatusService(IOrderRepository orderRepository, IItemRepository itemRepository, IOrganisationRepository organisationRepository)
        {
            this.orderRepository = orderRepository;
            this.itemRepository = itemRepository;
            this.organisationRepository = organisationRepository;
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
                            StatusId = item.JobStatusId,
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
        /// Get Items For Item Job Status 
        /// </summary>
        public IEnumerable<ItemForItemJobStatus> GetItemsForLateItems()
        {
            IEnumerable<Estimate> estimates = orderRepository.GetEstimatesForItemJobStatus();
            List<ItemForItemJobStatus> itemForItemJobStatuses = new List<ItemForItemJobStatus>();

            foreach (var estimate in estimates)
            {
                if (estimate.Items != null)
                {
                    foreach (var item in estimate.Items)
                    {
                        if (item.JobEstimatedStartDateTime < DateTime.Now || item.JobEstimatedCompletionDateTime < DateTime.Now)
                        {
                            ItemForItemJobStatus itemForItemJobStatus = new ItemForItemJobStatus()
                            {
                                EstimateId = item.EstimateId,
                                ItemId = item.ItemId,
                                Code = estimate.Estimate_Code + item.ItemCode,
                                CompanyName = item.Company != null ? item.Company.Name : string.Empty,
                                ProductName = item.ProductName,
                                Qty1 = item.Qty1,
                                StatusId = item.JobEstimatedStartDateTime < DateTime.Now ? 1:2,   // 1 -> Late started  2-> Late delivery 
                                JobEstimatedStartDateTime = item.JobEstimatedStartDateTime,
                                JobEstimatedCompletionDateTime = item.JobEstimatedCompletionDateTime,
                                Qty1NetTotal = item.Qty1NetTotal
                            };
                            itemForItemJobStatuses.Add(itemForItemJobStatus);   
                        }
                    }
                }
            }

            return itemForItemJobStatuses.OrderBy(i => i.JobEstimatedCompletionDateTime);
        }


        /// <summary>
        /// Get Currency Symbol
        /// </summary>
        public string GetCurrencySymbol()
        {
            Organisation organisation = organisationRepository.GetOrganizatiobByID();
            if (organisation != null)
            {
                return organisation.Currency != null ? organisation.Currency.CurrencySymbol : string.Empty;
            }
            return string.Empty;
        }

        /// <summary>
        /// Update Item Status
        /// </summary>
        public void UpdateItem(ItemForItemJobStatus item)
        {
            Item itemDbVersion = itemRepository.Find(item.ItemId);
            if (itemDbVersion != null)
            {
                itemDbVersion.JobStatusId = item.StatusId;
                itemRepository.SaveChanges();
            }
        }
        #endregion
    }
}
