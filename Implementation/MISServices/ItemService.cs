﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using MPC.ExceptionHandling;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ModelMappers;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using IItemService = MPC.Interfaces.MISServices.IItemService;

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
        private readonly ICostCentreRepository costCentreRepository;
        private readonly IStockItemRepository stockItemRepository;
        private readonly IItemPriceMatrixRepository itemPriceMatrixRepository;
        private readonly IItemStateTaxRepository itemStateTaxRepository;
        private readonly ICountryRepository countryRepository;
        private readonly IStateRepository stateRepository;
        private readonly ISectionFlagRepository sectionFlagRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IItemProductDetailRepository itemProductDetailRepository;
        private readonly IProductCategoryItemRepository productCategoryItemRepository;
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly ITemplatePageService templatePageService;

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

        /// <summary>
        /// Create Item State Tax
        /// </summary>
        private ItemStateTax CreateItemStateTax()
        {
            ItemStateTax line = itemStateTaxRepository.Create();
            itemStateTaxRepository.Add(line);
            return line;
        }

        /// <summary>
        /// Delete Item State Tax
        /// </summary>
        private void DeleteItemStateTax(ItemStateTax line)
        {
            itemStateTaxRepository.Delete(line);
        }

        /// <summary>
        /// Create Item Price Matrix
        /// </summary>
        private ItemPriceMatrix CreateItemPriceMatrix()
        {
            ItemPriceMatrix line = itemPriceMatrixRepository.Create();
            itemPriceMatrixRepository.Add(line);
            return line;
        }

        /// <summary>
        /// Create Item Product Detail
        /// </summary>
        private ItemProductDetail CreateItemProductDetail()
        {
            ItemProductDetail line = itemProductDetailRepository.Create();
            itemProductDetailRepository.Add(line);
            return line;
        }

        /// <summary>
        /// Create Product Category Item
        /// </summary>
        private ProductCategoryItem CreateProductCategoryItem()
        {
            ProductCategoryItem line = productCategoryItemRepository.Create();
            productCategoryItemRepository.Add(line);
            return line;
        }

        /// <summary>
        /// Delete Product Category Item
        /// </summary>
        private void DeleteProductCategoryItem(ProductCategoryItem line)
        {
            productCategoryItemRepository.Delete(line);
        }
        
        /// <summary>
        /// Save Product Images
        /// </summary>
        private void SaveProductImages(Item target)
        {
            string mpcContentPath = ConfigurationManager.AppSettings["MPC_Content"];
            HttpServerUtility server = HttpContext.Current.Server;
            string mapPath = server.MapPath(mpcContentPath + "/Products/Organisation" + itemRepository.OrganisationId + "/Product" + target.ItemId);
            
            // Create directory if not there
            if (!Directory.Exists(mapPath))
            {
                Directory.CreateDirectory(mapPath);
            }

            // Save Item Stock Option Images
            SaveItemStockOptionImages(target, mapPath);

            // Thumbnail Path
            SaveThumbnailPath(target, mapPath);

            // Grid Image
            SaveGridImage(target, mapPath);

            // Image Path
            SaveImagePath(target, mapPath);

            // Files 1,2,3,4,5
            SaveItemFiles(target, mapPath);

            // Save Changes
            itemRepository.SaveChanges();
        }

        /// <summary>
        /// Saves Item Stock Option Images
        /// </summary>
        private void SaveItemStockOptionImages(Item target, string mapPath)
        {
            foreach (ItemStockOption itemStockOption in target.ItemStockOptions)
            {
                // Write Image
                SaveItemStockOptionImage(target, mapPath, itemStockOption);
            }
        }

        /// <summary>
        /// Save Item Stock Option Image
        /// </summary>
        private void SaveItemStockOptionImage(Item target, string mapPath, ItemStockOption itemStockOption)
        {
            string imageUrl = SaveImage(mapPath, itemStockOption.ImageURL,
                target.ItemId + itemStockOption.ItemStockOptionId + itemStockOption.OptionSequence + "_StockOption_",
                itemStockOption.FileName,
                itemStockOption.FileSource,
                itemStockOption.FileSourceBytes);

            if (imageUrl != null)
            {
                itemStockOption.ImageURL = imageUrl;
            }
        }

        /// <summary>
        /// Save Image Path
        /// </summary>
        private void SaveImagePath(Item target, string mapPath)
        {
            string imagePathUrl = SaveImage(mapPath, target.ImagePath,
                target.ItemId + target.ProductCode + target.ProductName + "_ImagePath_",
                target.ImagePathImageName,
                target.ImagePathImage,
                target.ImagePathSourceBytes);

            if (imagePathUrl != null)
            {
                // Update Image Path
                target.ImagePath = imagePathUrl;
            }
        }

        /// <summary>
        /// Save Grid Image
        /// </summary>
        private void SaveGridImage(Item target, string mapPath)
        {
            string gridImageUrl = SaveImage(mapPath, target.GridImage,
                target.ItemId + target.ProductCode + target.ProductName + "_GridImage_",
                target.GridImageSourceName,
                target.GridImageBytes,
                target.GridImageSourceBytes);

            if (gridImageUrl != null)
            {
                // Update Grid Image
                target.GridImage = gridImageUrl;
            }
        }

        /// <summary>
        /// Save Thumbnail Path
        /// </summary>
        private void SaveThumbnailPath(Item target, string mapPath)
        {
            string thumbnailImageUrl = SaveImage(mapPath, target.ThumbnailPath,
                target.ItemId + target.ProductCode + target.ProductName + "_ThumbnailPath_",
                target.ThumbnailImageName,
                target.ThumbnailImage,
                target.ThumbnailSourceBytes);

            if (thumbnailImageUrl != null)
            {
                // Update Thumbnail Path
                target.ThumbnailPath = thumbnailImageUrl;
            }
        }

        /// <summary>
        /// Save File1,File2,File3,File4,File5
        /// </summary>
        private void SaveItemFiles(Item target, string mapPath)
        {
            string path = SaveImage(mapPath, target.File1,
                target.ItemId + target.ProductCode + target.ProductName + "_File1_",
                target.File1Name,
                target.File1Byte,
                target.File1SourceBytes);

            if (path != null)
            {
                // Update File1
                target.File1 = path;
            }

            path = SaveImage(mapPath, target.File2,
                target.ItemId + target.ProductCode + target.ProductName + "_File2_",
                target.File2Name,
                target.File2Byte,
                target.File2SourceBytes);

            if (path != null)
            {
                // Update File2
                target.File2 = path;
            }

            path = SaveImage(mapPath, target.File3,
                target.ItemId + target.ProductCode + target.ProductName + "_File3_",
                target.File3Name,
                target.File3Byte,
                target.File3SourceBytes);

            if (path != null)
            {
                // Update File3
                target.File3 = path;
            }

            path = SaveImage(mapPath, target.File4,
                target.ItemId + target.ProductCode + target.ProductName + "_File4_",
                target.File4Name,
                target.File4Byte,
                target.File4SourceBytes);

            if (path != null)
            {
                // Update File4
                target.File4 = path;
            }

            path = SaveImage(mapPath, target.File5,
                target.ItemId + target.ProductCode + target.ProductName + "_File5_",
                target.File5Name,
                target.File5Byte,
                target.File5SourceBytes);

            if (path != null)
            {
                // Update File5
                target.File5 = path;
            }
        }

        /// <summary>
        /// Saves Image to File System
        /// </summary>
        /// <param name="mapPath">File System Path for Item</param>
        /// <param name="existingImage">Existing File if any</param>
        /// <param name="caption">Unique file caption e.g. ItemId + ItemProductCode + ItemProductName + "_thumbnail_"</param>
        /// <param name="fileName">Name of file being saved</param>
        /// <param name="fileSource">Base64 representation of file being saved</param>
        /// <param name="fileSourceBytes">Byte[] representation of file being saved</param>
        /// <returns>Path of File being saved</returns>
        private string SaveImage(string mapPath, string existingImage, string caption, string fileName, 
            string fileSource, byte[] fileSourceBytes)
        {
            if (!string.IsNullOrEmpty(fileSource))
            {
                // Look if file already exists then replace it
                if (!string.IsNullOrEmpty(existingImage) && File.Exists(existingImage))
                {
                    // Remove Existing File
                    File.Delete(existingImage);
                }

                // First Time Upload
                string imageurl = mapPath + "\\" + caption + fileName;
                File.WriteAllBytes(imageurl, fileSourceBytes);

                // Return path
                return imageurl;
            }

            return null;
        }
        
        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public ItemService(IItemRepository itemRepository, IGetItemsListViewRepository itemsListViewRepository, IItemVdpPriceRepository itemVdpPriceRepository,
            IPrefixRepository prefixRepository, IItemVideoRepository itemVideoRepository, IItemRelatedItemRepository itemRelatedItemRepository, 
            ITemplatePageRepository templatePageRepository, ITemplateRepository templateRepository, IItemStockOptionRepository itemStockOptionRepository,
            IItemAddOnCostCentreRepository itemAddOnCostCentreRepository, ICostCentreRepository costCentreRepository, IStockItemRepository stockItemRepository, 
            IItemPriceMatrixRepository itemPriceMatrixRepository, IItemStateTaxRepository itemStateTaxRepository, ICountryRepository countryRepository,
            IStateRepository stateRepository, ISectionFlagRepository sectionFlagRepository, ICompanyRepository companyRepository, 
            IItemProductDetailRepository itemProductDetailRepository, IProductCategoryItemRepository productCategoryItemRepository, 
            IProductCategoryRepository productCategoryRepository, ITemplatePageService templatePageService)
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
            if (costCentreRepository == null)
            {
                throw new ArgumentNullException("costCentreRepository");
            }
            if (stockItemRepository == null)
            {
                throw new ArgumentNullException("stockItemRepository");
            }
            if (itemPriceMatrixRepository == null)
            {
                throw new ArgumentNullException("itemPriceMatrixRepository");
            }
            if (itemStateTaxRepository == null)
            {
                throw new ArgumentNullException("itemStateTaxRepository");
            }
            if (countryRepository == null)
            {
                throw new ArgumentNullException("countryRepository");
            }
            if (stateRepository == null)
            {
                throw new ArgumentNullException("stateRepository");
            }
            if (sectionFlagRepository == null)
            {
                throw new ArgumentNullException("sectionFlagRepository");
            }
            if (companyRepository == null)
            {
                throw new ArgumentNullException("companyRepository");
            }
            if (itemProductDetailRepository == null)
            {
                throw new ArgumentNullException("itemProductDetailRepository");
            }
            if (productCategoryItemRepository == null)
            {
                throw new ArgumentNullException("productCategoryItemRepository");
            }
            if (productCategoryRepository == null)
            {
                throw new ArgumentNullException("productCategoryRepository");
            }
            if (templatePageService == null)
            {
                throw new ArgumentNullException("templatePageService");
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
            this.costCentreRepository = costCentreRepository;
            this.stockItemRepository = stockItemRepository;
            this.itemPriceMatrixRepository = itemPriceMatrixRepository;
            this.itemStateTaxRepository = itemStateTaxRepository;
            this.countryRepository = countryRepository;
            this.stateRepository = stateRepository;
            this.sectionFlagRepository = sectionFlagRepository;
            this.companyRepository = companyRepository;
            this.itemProductDetailRepository = itemProductDetailRepository;
            this.productCategoryItemRepository = productCategoryItemRepository;
            this.productCategoryRepository = productCategoryRepository;
            this.templatePageService = templatePageService;
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
                itemTarget.OrganisationId = itemRepository.OrganisationId;
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
                DeleteItemAddonCostCentre = DeleteItemAddonCostCentre,
                CreateItemStateTax = CreateItemStateTax,
                DeleteItemStateTax = DeleteItemStateTax,
                CreateItemPriceMatrix = CreateItemPriceMatrix,
                CreateItemProductDetail = CreateItemProductDetail,
                CreateProductCategoryItem = CreateProductCategoryItem,
                DeleteProductCategoryItem = DeleteProductCategoryItem
            });

            // Save Changes
            itemRepository.SaveChanges();

            // Save Images and Update Item
            SaveProductImages(itemTarget);

            // Load Properties if Any
            itemTarget = itemRepository.Find(itemTarget.ItemId);

            // Get Updated Minimum Price
            itemTarget.MinPrice = itemRepository.GetMinimumProductValue(itemTarget.ItemId);

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

        /// <summary>
        /// Get Base Data
        /// </summary>
        /// <returns></returns>
        public ItemBaseResponse GetBaseData()
        {
            return new ItemBaseResponse
            {
                CostCentres = costCentreRepository.GetAllNonSystemCostCentres(),
                SectionFlags = sectionFlagRepository.GetAllForCustomerPriceIndex(),
                Countries = countryRepository.GetAll(),
                States = stateRepository.GetAll(),
                Suppliers = companyRepository.GetAllSuppliers(),
                ProductCategories = productCategoryRepository.GetParentCategories()
            };
        }
        
        /// <summary>
        /// Get Stock Items
        /// Used in Products - Stock Item Selection
        /// </summary>
        public InventorySearchResponse GetStockItems(StockItemRequestModel request)
        {
            return stockItemRepository.GetStockItemsForProduct(request);
        }

        /// <summary>
        /// Get Item Price Matrices for Item by Section Flag
        /// </summary>
        public IEnumerable<ItemPriceMatrix> GetItemPriceMatricesBySectionFlagForItem(long sectionFlagId, long itemId)
        {
            return itemPriceMatrixRepository.GetForItemBySectionFlag(sectionFlagId, itemId);
        }

        #endregion
    }
}
