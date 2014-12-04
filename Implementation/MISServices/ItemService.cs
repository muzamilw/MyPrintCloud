using System;
using System.Globalization;
using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Item Service
    /// </summary>
    public class ItemService : IItemService
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly IItemRepository itemRepository;
        private readonly IGetItemsListViewRepository itemsListViewRepository;

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public ItemService(IItemRepository itemRepository, IGetItemsListViewRepository itemsListViewRepository)
        {
            if (itemRepository == null)
            {
                throw new ArgumentNullException("itemRepository");
            }
            if (itemsListViewRepository == null)
            {
                throw new ArgumentNullException("itemsListViewRepository");
            }

            this.itemRepository = itemRepository;
            this.itemsListViewRepository = itemsListViewRepository;
        }

        #endregion

        #region Public
        
        /// <summary>
        /// Load Items based on search filters
        /// </summary>
        public ItemListViewSearchResponse GetItems(ItemSearchRequestModel request)
        {
            return itemsListViewRepository.GetItems(request);
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        public Item GetById(long id)
        {
            Item item = itemRepository.Find(id);

            if (item == null)
            {
                throw new MPCException(string.Format(CultureInfo.InvariantCulture, LanguageResources.ItemService_ItemNotFound, id), itemRepository.OrganisationId);
            }

            return item;
        }

        /// <summary>
        /// Save Product Image
        /// </summary>
        public Item SaveProductImage(string filePath, long itemId)
        {
            if (itemId <= 0)
            {
                throw new ArgumentException(LanguageResources.ItemService_InvalidItem, "itemId");
            }

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException(LanguageResources.ItemService_InvalidFilePath, "filePath");
            }

            Item item = GetById(itemId);

            item.ThumbnailPath = filePath;
            itemRepository.SaveChanges();

            return item;
        }

        #endregion
    }
}
