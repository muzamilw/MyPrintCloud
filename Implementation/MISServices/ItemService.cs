using System;
using System.Globalization;
using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ModelMappers;
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
        private readonly IItemVdpPriceRepository itemVdpPriceRepository;
        private readonly IPrefixRepository prefixRepository;
        private readonly IItemVideoRepository itemVideoRepository;
        private readonly IItemRelatedItemRepository itemRelatedItemRepository;
        private readonly ITemplatePageRepository templatePageRepository;
        private readonly ITemplateRepository templateRepository;
        private readonly IItemStockOptionRepository itemStockOptionRepository;
        private readonly IItemAddOnCostCentreRepository itemAddOnCostCentreRepository;

        /// <summary>
        /// Create Item Vdp Price
        /// </summary>
        private ItemVdpPrice CreateItemVdpPrice()
        {
            ItemVdpPrice line = itemVdpPriceRepository.Create();
            itemVdpPriceRepository.Add(line);
            return line;
        }

        /// <summary>
        /// Delete Item Vdp Price
        /// </summary>
        private void DeleteItemVdpPrice(ItemVdpPrice line)
        {
            itemVdpPriceRepository.Delete(line);
        }

        /// <summary>
        /// Create Item Video
        /// </summary>
        private ItemVideo CreateItemVideo()
        {
            ItemVideo video = itemVideoRepository.Create();
            itemVideoRepository.Add(video);
            return video;
        }

        /// <summary>
        /// Delete Item Video
        /// </summary>
        private void DeleteItemVideo(ItemVideo video)
        {
            itemVideoRepository.Delete(video);
        }

        /// <summary>
        /// Create Item RelatedItem
        /// </summary>
        private ItemRelatedItem CreateItemRelatedItem()
        {
            ItemRelatedItem relatedItem = itemRelatedItemRepository.Create();
            itemRelatedItemRepository.Add(relatedItem);
            return relatedItem;
        }

        /// <summary>
        /// Delete Item RelatedItem
        /// </summary>
        private void DeleteItemRelatedItem(ItemRelatedItem relatedItem)
        {
            itemRelatedItemRepository.Delete(relatedItem);
        }

        /// <summary>
        /// Create Template
        /// </summary>
        private Template CreateTemplate()
        {
            Template template = templateRepository.Create();
            templateRepository.Add(template);
            return template;
        }

        /// <summary>
        /// Create Template Page
        /// </summary>
        private TemplatePage CreateTemplatePage()
        {
            TemplatePage relatedItem = templatePageRepository.Create();
            templatePageRepository.Add(relatedItem);
            return relatedItem;
        }

        /// <summary>
        /// Delete Template Page
        /// </summary>
        private void DeleteTemplatePage(TemplatePage relatedItem)
        {
            templatePageRepository.Delete(relatedItem);
        }

        /// <summary>
        /// Create Item Stock Option
        /// </summary>
        private ItemStockOption CreateItemStockOption()
        {
            ItemStockOption line = itemStockOptionRepository.Create();
            itemStockOptionRepository.Add(line);
            return line;
        }

        /// <summary>
        /// Delete Item Stock Option
        /// </summary>
        private void DeleteItemStockOption(ItemStockOption line)
        {
            itemStockOptionRepository.Delete(line);
        }

        /// <summary>
        /// Create Item Addon Cost Centre
        /// </summary>
        private ItemAddonCostCentre CreateItemAddonCostCentre()
        {
            ItemAddonCostCentre line = itemAddOnCostCentreRepository.Create();
            itemAddOnCostCentreRepository.Add(line);
            return line;
        }

        /// <summary>
        /// Delete Item Addon Cost Centre
        /// </summary>
        private void DeleteItemAddonCostCentre(ItemAddonCostCentre line)
        {
            itemAddOnCostCentreRepository.Delete(line);
        }

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public ItemService(IItemRepository itemRepository, IGetItemsListViewRepository itemsListViewRepository, IItemVdpPriceRepository itemVdpPriceRepository,
            IPrefixRepository prefixRepository, IItemVideoRepository itemVideoRepository, IItemRelatedItemRepository itemRelatedItemRepository, 
            ITemplatePageRepository templatePageRepository, ITemplateRepository templateRepository, IItemStockOptionRepository itemStockOptionRepository,
            IItemAddOnCostCentreRepository itemAddOnCostCentreRepository)
        {
            if (itemRepository == null)
            {
                throw new ArgumentNullException("itemRepository");
            }
            if (itemsListViewRepository == null)
            {
                throw new ArgumentNullException("itemsListViewRepository");
            }
            if (itemVdpPriceRepository == null)
            {
                throw new ArgumentNullException("itemVdpPriceRepository");
            }
            if (prefixRepository == null)
            {
                throw new ArgumentNullException("prefixRepository");
            }
            if (itemVideoRepository == null)
            {
                throw new ArgumentNullException("itemVideoRepository");
            }
            if (itemRelatedItemRepository == null)
            {
                throw new ArgumentNullException("itemRelatedItemRepository");
            }
            if (templatePageRepository == null)
            {
                throw new ArgumentNullException("templatePageRepository");
            }
            if (templateRepository == null)
            {
                throw new ArgumentNullException("templateRepository");
            }
            if (itemStockOptionRepository == null)
            {
                throw new ArgumentNullException("itemStockOptionRepository");
            }
            if (itemAddOnCostCentreRepository == null)
            {
                throw new ArgumentNullException("itemAddOnCostCentreRepository");
            }

            this.itemRepository = itemRepository;
            this.itemsListViewRepository = itemsListViewRepository;
            this.itemVdpPriceRepository = itemVdpPriceRepository;
            this.prefixRepository = prefixRepository;
            this.itemVideoRepository = itemVideoRepository;
            this.itemRelatedItemRepository = itemRelatedItemRepository;
            this.templatePageRepository = templatePageRepository;
            this.templateRepository = templateRepository;
            this.itemStockOptionRepository = itemStockOptionRepository;
            this.itemAddOnCostCentreRepository = itemAddOnCostCentreRepository;
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
            if (id <= 0)
            {
                return null;
            }

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
        public Item SaveProductImage(string filePath, long itemId, ItemFileType itemFileType)
        {
            if (itemId <= 0)
            {
                throw new ArgumentException(LanguageResources.ItemService_InvalidItem, "itemId");
            }

            if (filePath == null)
            {
                throw new ArgumentException(LanguageResources.ItemService_InvalidFilePath, "filePath");
            }
            
            Item item = GetById(itemId);

            switch (itemFileType)
            {
                case ItemFileType.Thumbnail:
                    item.ThumbnailPath = filePath;
                    break;
                case ItemFileType.Grid:
                    item.GridImage = filePath;
                    break;
                case ItemFileType.ImagePath:
                    item.ImagePath = filePath;
                    break;
                case ItemFileType.File1:
                    item.File1 = filePath;
                    break;
            }
            
            itemRepository.SaveChanges();

            return item;
        }

        /// <summary>
        /// Delete File
        /// </summary>
        public string DeleteProductImage(long itemId, ItemFileType itemFileType)
        {
            if (itemId <= 0)
            {
                throw new ArgumentException(LanguageResources.ItemService_InvalidItem, "itemId");
            }
            
            Item item = GetById(itemId);
            string filePath = item.ThumbnailPath;

            SaveProductImage(string.Empty, itemId, itemFileType);

            return filePath;
        }

        /// <summary>
        /// Save Product/Item
        /// </summary>
        public Item SaveProduct(Item item)
        {
            // Get Db Version
            Item itemTarget = GetById(item.ItemId);

            // If New then Add, Update If Existing
            if (itemTarget == null)
            {
                // Gets Next Item Code and Increments it by 1
                string itemCode = prefixRepository.GetNextItemCodePrefix();
                itemTarget = itemRepository.Create();
                itemRepository.Add(itemTarget);
                itemTarget.ItemCreationDateTime = DateTime.Now;
                itemTarget.ItemCode = itemCode;
            }

            // Update
            item.UpdateTo(itemTarget, new ItemMapperActions
            {
                CreateItemVdpPrice = CreateItemVdpPrice,
                DeleteItemVdpPrice = DeleteItemVdpPrice,
                CreateItemVideo = CreateItemVideo,
                DeleteItemVideo = DeleteItemVideo,
                CreateItemRelatedItem = CreateItemRelatedItem,
                DeleteItemRelatedItem = DeleteItemRelatedItem,
                CreateTemplatePage = CreateTemplatePage,
                DeleteTemplatePage = DeleteTemplatePage,
                CreateTemplate = CreateTemplate,
                CreateItemStockOption = CreateItemStockOption,
                DeleteItemStockOption = DeleteItemStockOption,
                CreateItemAddonCostCentre = CreateItemAddonCostCentre,
                DeleteItemAddonCostCentre = DeleteItemAddonCostCentre
            });

            // Save Changes
            itemRepository.SaveChanges();

            // Load Properties if Any
            itemTarget = itemRepository.Find(itemTarget.ItemId);

            // Return Item
            return itemTarget;
        }

        /// <summary>
        /// Archive Product
        /// </summary>
        public void ArchiveProduct(long itemId)
        {
            // Get Item
            Item item = GetById(itemId);

            // Archive
            item.IsArchived = true;

            // Save Changes
            itemRepository.SaveChanges();
        }

        #endregion
    }
}
