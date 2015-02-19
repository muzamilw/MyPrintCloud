using System;
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
        private readonly ITemplateService templateService;
        private readonly IMachineRepository machineRepository;
        private readonly IPaperSizeRepository paperSizeRepository;
        private readonly IItemSectionRepository itemSectionRepository;

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
        /// Sets Default Paper Sizes For Non Print Item Section
        /// </summary>
        private void SetPaperSizesForNonPrintItemSection(ItemSection line)
        {
            List<PaperSize> paperSizes = paperSizeRepository.GetAll().ToList();
            if (paperSizes.Any())
            {
                PaperSize paperSize = paperSizes.First();

                // Set Default SectionSizeId
                line.SectionSizeId = paperSize.PaperSizeId;
                line.SectionSizeHeight = paperSize.Height;
                line.SectionSizeWidth = paperSize.Width;

                // Set Default ItemSizeId
                line.ItemSizeId = paperSize.PaperSizeId;
                line.ItemSizeHeight = paperSize.Height;
                line.ItemSizeWidth = paperSize.Width;
            }
        }

        /// <summary>
        /// Sets Default Press, StockItem, SectionSize, ItemSize to Non Print Item Section
        /// </summary>
        private void SetNonPrintItemSection(ItemSection line)
        {
            // Set Default Press Id
            MachineSearchResponse machinesResponse = machineRepository.GetMachinesForProduct(new MachineSearchRequestModel());

            if (machinesResponse.Machines != null && machinesResponse.Machines.Any())
            {
                Machine machine = machinesResponse.Machines.FirstOrDefault();
                if (machine != null)
                {
                    line.PressId = machine.MachineId;
                }
            }

            // Set Default StockItemId
            InventorySearchResponse stockItemResponse = stockItemRepository
                .GetStockItemsForProduct(new StockItemRequestModel
                {
                    CategoryId = (long)StockCategoryEnum.Paper
                });

            if (stockItemResponse.StockItems != null && stockItemResponse.StockItems.Any())
            {
                StockItem stockItem = stockItemResponse.StockItems.FirstOrDefault();
                if (stockItem != null)
                {
                    line.StockItemID1 = stockItem.StockItemId;
                }
            }

            // Set Paper Sizes
            SetPaperSizesForNonPrintItemSection(line);
        }
        
        /// <summary>
        /// Create Item Section
        /// </summary>
        private ItemSection CreateItemSection()
        {
            ItemSection line = itemSectionRepository.Create();
            itemSectionRepository.Add(line);

            return line;
        }

        /// <summary>
        /// Delete Item Section
        /// </summary>
        private void DeleteItemSection(ItemSection line)
        {
            itemSectionRepository.Delete(line);
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

                int indexOf = imageurl.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                imageurl = imageurl.Substring(indexOf, imageurl.Length - indexOf);
                return imageurl;
            }

            return null;
        }

        /// <summary>
        /// Calls Template Services to Create Pdf
        /// </summary>
        private void GenereatePdfForTemplate(Item itemTarget, int? templateTypeMode)
        {
            if (itemTarget.TemplateId.HasValue)
            {
                long organisationId = itemTarget.OrganisationId.HasValue
                    ? itemTarget.OrganisationId.Value
                    : itemRepository.OrganisationId;

                Template template = itemTarget.Template;

                if (template == null)
                {
                    throw new MPCException(
                        string.Format(CultureInfo.InvariantCulture, LanguageResources.ItemService_TemplateNotFound,
                            itemTarget.TemplateId.Value),
                        organisationId);
                }

                if (itemTarget.TemplateType.HasValue)
                {
                    // Create Blank Pdf from Template Pages
                    if (itemTarget.TemplateType.Value == 1)
                    {
                        // Generates Pdf from Template Pages
                        GeneratePdfFromTemplatePages(template, organisationId);
                    }
                    else if (itemTarget.TemplateType.Value == 2)
                    {
                        // Create Pre-Built Template from Pdf
                        // Save Template Pdf
                        var mapPath = SavePdfForPreBuiltTemplate(itemTarget);

                        // Genereates Template Pages from Pdf supplied
                        GenerateTemplatePagesFromPdf(itemTarget, mapPath, organisationId, templateTypeMode);
                    }
                }
            }
        }

        /// <summary>
        /// Generate Template Pages from Pdf Provided
        /// </summary>
        private void GenerateTemplatePagesFromPdf(Item itemTarget, string mapPath, long organisationId, int? templateTypeMode)
        {
            try
            {
                if (!itemTarget.TemplateId.HasValue)
                {
                    return;
                }

                templateService.generateTemplateFromPDF(mapPath,
                    templateTypeMode.HasValue ? templateTypeMode.Value : 2,
                    itemTarget.TemplateId.Value, organisationId);
            }
            catch (Exception)
            {
                throw new MPCException(
                    "Saved Successfully but " + LanguageResources.ItemService_FailedToGeneratePagesFromPdf,
                    organisationId);
            }
        }


        /// <summary>
        /// Genereate Pdf From Template Pages
        /// </summary>
        private void GeneratePdfFromTemplatePages(Template template, long organisationId)
        {
            try
            {
                templatePageService.CreateBlankBackgroundPDFsByPages(template.ProductId,
                    template.PDFTemplateHeight.HasValue ? template.PDFTemplateHeight.Value : 0,
                    template.PDFTemplateWidth.HasValue ? template.PDFTemplateWidth.Value : 0,
                    1, template.TemplatePages.ToList(),
                    organisationId);
            }
            catch (Exception)
            {
                throw new MPCException(
                    "Saved Successfully but " + LanguageResources.ItemService_FailedToGeneratePdfFromPages,
                    organisationId);
            }
        }

        /// <summary>
        /// Saves Pdf For PreBuiltTemplate
        /// </summary>
        private string SavePdfForPreBuiltTemplate(Item itemTarget)
        {
            string mpcContentPath = ConfigurationManager.AppSettings["MPC_Content"];
            HttpServerUtility server = HttpContext.Current.Server;
            string mapPath =
                server.MapPath(mpcContentPath + "/Products/Organisation" + itemRepository.OrganisationId +
                               "/Product" + itemTarget.ItemId);
            mapPath = mapPath + "//Templates";

            if (!Directory.Exists(mapPath))
            {
                Directory.CreateDirectory(mapPath);
            }

            mapPath = SaveImage(mapPath, mapPath + "//random_CorporateTemplateUpload.pdf", string.Empty,
                "random_CorporateTemplateUpload.pdf", itemTarget.Template.FileSource, itemTarget.Template.FileSourceBytes);

            return mapPath;
        }

        /// <summary>
        /// Creates New Item and assigns new generated code
        /// </summary>
        private Item CreateNewItem()
        {
            string itemCode = prefixRepository.GetNextItemCodePrefix();
            Item itemTarget = itemRepository.Create();
            itemRepository.Add(itemTarget);
            itemTarget.ItemCreationDateTime = DateTime.Now;
            itemTarget.ItemCode = itemCode;
            itemTarget.OrganisationId = itemRepository.OrganisationId;
            return itemTarget;
        }

        /// <summary>
        /// Copy Item Product Detail
        /// </summary>
        private void CloneItemProductDetail(Item source, Item target)
        {
            if (source.ItemProductDetails == null)
            {
                return;
            }

            // Initialize List
            if (target.ItemProductDetails == null)
            {
                target.ItemProductDetails = new List<ItemProductDetail>();
            }

            foreach (ItemProductDetail itemProductDetail in source.ItemProductDetails)
            {
                ItemProductDetail targetItemProductDetail = itemProductDetailRepository.Create();
                itemProductDetailRepository.Add(targetItemProductDetail);
                targetItemProductDetail.ItemId = target.ItemId;
                target.ItemProductDetails.Add(targetItemProductDetail);
                itemProductDetail.Clone(targetItemProductDetail);
            }
        }

        /// <summary>
        /// Copy Item Sections
        /// </summary>
        private void CloneItemSections(Item source, Item target)
        {
            if (source.ItemSections == null)
            {
                return;
            }

            // Initialize List
            if (target.ItemSections == null)
            {
                target.ItemSections = new List<ItemSection>();
            }

            foreach (ItemSection itemSection in source.ItemSections)
            {
                ItemSection targetItemSection = itemSectionRepository.Create();
                itemSectionRepository.Add(targetItemSection);
                targetItemSection.ItemId = target.ItemId;
                target.ItemSections.Add(targetItemSection);
                itemSection.Clone(targetItemSection);
            }
        }

        /// <summary>
        /// Creates Copy of Item Addon CostCentre
        /// </summary>
        private void CloneItemAddonCostCentres(ItemStockOption itemStockOption, ItemStockOption targetItemStockOption)
        {
            if (targetItemStockOption.ItemAddonCostCentres == null)
            {
                targetItemStockOption.ItemAddonCostCentres = new List<ItemAddonCostCentre>();
            }

            foreach (ItemAddonCostCentre itemAddonCostCentre in itemStockOption.ItemAddonCostCentres)
            {
                ItemAddonCostCentre targetItemAddonCostCentre = itemAddOnCostCentreRepository.Create();
                itemAddOnCostCentreRepository.Add(targetItemAddonCostCentre);
                targetItemAddonCostCentre.ItemStockOptionId = targetItemStockOption.ItemStockOptionId;
                targetItemStockOption.ItemAddonCostCentres.Add(targetItemAddonCostCentre);
                itemAddonCostCentre.Clone(targetItemAddonCostCentre);
            }     
        }

        /// <summary>
        /// Copy Item Stock Options
        /// </summary>
        private void CloneItemStockOptions(Item source, Item target)
        {
            if (source.ItemStockOptions == null)
            {
                return;
            }

            // Initialize List
            if (target.ItemStockOptions == null)
            {
                target.ItemStockOptions = new List<ItemStockOption>();
            }

            foreach (ItemStockOption itemStockOption in source.ItemStockOptions)
            {
                ItemStockOption targetItemStockOption = itemStockOptionRepository.Create();
                itemStockOptionRepository.Add(targetItemStockOption);
                targetItemStockOption.ItemId = target.ItemId;
                target.ItemStockOptions.Add(targetItemStockOption);
                itemStockOption.Clone(targetItemStockOption);

                // Clone Item Addon Cost Centres
                if (itemStockOption.ItemAddonCostCentres == null)
                {
                    continue;
                }

                // Clone ItemAddonCostCentres
                CloneItemAddonCostCentres(itemStockOption, targetItemStockOption);
            }
        }

        /// <summary>
        /// Copy Item Price Matrices
        /// </summary>
        private void CloneItemPriceMatrices(Item source, Item target)
        {
            if (source.ItemPriceMatrices == null)
            {
                return;
            }

            // Initialize List
            if (target.ItemPriceMatrices == null)
            {
                target.ItemPriceMatrices = new List<ItemPriceMatrix>();
            }

            foreach (ItemPriceMatrix itemPriceMatrix in source.ItemPriceMatrices)
            {
                ItemPriceMatrix targetItemPriceMatrix = itemPriceMatrixRepository.Create();
                itemPriceMatrixRepository.Add(targetItemPriceMatrix);
                targetItemPriceMatrix.ItemId = target.ItemId;
                target.ItemPriceMatrices.Add(targetItemPriceMatrix);
                itemPriceMatrix.Clone(targetItemPriceMatrix);
            }
        }

        /// <summary>
        /// Copy Item State Taxes
        /// </summary>
        private void CloneItemStateTaxes(Item source, Item target)
        {
            if (source.ItemStateTaxes == null)
            {
                return;
            }

            // Initialize List
            if (target.ItemStateTaxes == null)
            {
                target.ItemStateTaxes = new List<ItemStateTax>();
            }

            foreach (ItemStateTax itemStateTax in source.ItemStateTaxes)
            {
                ItemStateTax targetItemStateTax = itemStateTaxRepository.Create();
                itemStateTaxRepository.Add(targetItemStateTax);
                targetItemStateTax.ItemId = target.ItemId;
                target.ItemStateTaxes.Add(targetItemStateTax);
                itemStateTax.Clone(targetItemStateTax);
            }
        }

        /// <summary>
        /// Copy Item Vdp Prices
        /// </summary>
        private void CloneItemVdpPrices(Item source, Item target)
        {
            if (source.ItemVdpPrices == null)
            {
                return;
            }

            // Initialize List
            if (target.ItemVdpPrices == null)
            {
                target.ItemVdpPrices = new List<ItemVdpPrice>();
            }

            foreach (ItemVdpPrice itemVdpPrice in source.ItemVdpPrices)
            {
                ItemVdpPrice targetItemVdpPrice = itemVdpPriceRepository.Create();
                itemVdpPriceRepository.Add(targetItemVdpPrice);
                targetItemVdpPrice.ItemId = target.ItemId;
                target.ItemVdpPrices.Add(targetItemVdpPrice);
                itemVdpPrice.Clone(targetItemVdpPrice);
            }
        }

        /// <summary>
        /// Copy Product Category Items
        /// </summary>
        private void CloneProductCategoryItems(Item source, Item target)
        {
            if (source.ProductCategoryItems == null)
            {
                return;
            }

            // Initialize List
            if (target.ProductCategoryItems == null)
            {
                target.ProductCategoryItems = new List<ProductCategoryItem>();
            }

            foreach (ProductCategoryItem productCategoryItem in source.ProductCategoryItems)
            {
                ProductCategoryItem targetProductCategoryItem = productCategoryItemRepository.Create();
                productCategoryItemRepository.Add(targetProductCategoryItem);
                targetProductCategoryItem.ItemId = target.ItemId;
                target.ProductCategoryItems.Add(targetProductCategoryItem);
                productCategoryItem.Clone(targetProductCategoryItem);
            }
        }

        /// <summary>
        /// Copy Item Related Items
        /// </summary>
        private void CloneItemRelatedItems(Item source, Item target)
        {
            if (source.ItemRelatedItems == null)
            {
                return;
            }

            // Initialize List
            if (target.ItemRelatedItems == null)
            {
                target.ItemRelatedItems = new List<ItemRelatedItem>();
            }

            foreach (ItemRelatedItem itemRelatedItem in source.ItemRelatedItems)
            {
                ItemRelatedItem targetItemRelatedItem = itemRelatedItemRepository.Create();
                itemRelatedItemRepository.Add(targetItemRelatedItem);
                targetItemRelatedItem.ItemId = target.ItemId;
                target.ItemRelatedItems.Add(targetItemRelatedItem);
                itemRelatedItem.Clone(targetItemRelatedItem);
            }
        }

        /// <summary>
        /// Clone Item Stock Option Images
        /// </summary>
        private void CloneItemStockOptionImages(Item target, string mapPath)
        {
            foreach (ItemStockOption itemStockOption in target.ItemStockOptions)
            {
                // Write Image
                CloneItemStockOptionImage(target, mapPath, itemStockOption);
            }
        }

        /// <summary>
        /// Clone Item Stock Option Image
        /// </summary>
        private void CloneItemStockOptionImage(Item target, string mapPath, ItemStockOption itemStockOption)
        {
            if (string.IsNullOrEmpty((itemStockOption.ImageURL)) || !File.Exists(itemStockOption.ImageURL))
            {
                return;
            }

            byte[] fileBytes = File.ReadAllBytes(itemStockOption.ImageURL);
            string imageUrl = SaveImage(mapPath, string.Empty,
                target.ItemId + itemStockOption.ItemStockOptionId + itemStockOption.OptionSequence + "_StockOption_",
                "image.png",
                "stockOption",
                fileBytes);

            if (imageUrl != null)
            {
                itemStockOption.ImageURL = imageUrl;
            }
        }

        /// <summary>
        /// Clone Image Path
        /// </summary>
        private void CloneImagePath(Item target, string mapPath)
        {
            if (string.IsNullOrEmpty((target.ImagePath)) || !File.Exists(target.ImagePath))
            {
                return;
            }

            byte[] fileBytes = File.ReadAllBytes(target.ImagePath);
            string imagePathUrl = SaveImage(mapPath, string.Empty,
                target.ItemId + target.ProductCode + target.ProductName + "_ImagePath_",
                "imagePath.png",
                "imagePath",
                fileBytes);

            if (imagePathUrl != null)
            {
                // Update Image Path
                target.ImagePath = imagePathUrl;
            }
        }

        /// <summary>
        /// Save Grid Image
        /// </summary>
        private void CloneGridImage(Item target, string mapPath)
        {
            if (string.IsNullOrEmpty((target.GridImage)) || !File.Exists(target.GridImage))
            {
                return;
            }

            byte[] fileBytes = File.ReadAllBytes(target.GridImage);
            string gridImageUrl = SaveImage(mapPath, target.GridImage,
                target.ItemId + target.ProductCode + target.ProductName + "_GridImage_",
                "gridImage.png",
                "gridImage",
                fileBytes);

            if (gridImageUrl != null)
            {
                // Update Grid Image
                target.GridImage = gridImageUrl;
            }
        }

        /// <summary>
        /// Clone Thumbnail Path
        /// </summary>
        private void CloneThumbnailPath(Item target, string mapPath)
        {
            if (string.IsNullOrEmpty((target.ThumbnailPath)) || !File.Exists(target.ThumbnailPath))
            {
                return;
            }

            byte[] fileBytes = File.ReadAllBytes(target.ThumbnailPath);
            string thumbnailImageUrl = SaveImage(mapPath, target.ThumbnailPath,
                target.ItemId + target.ProductCode + target.ProductName + "_ThumbnailPath_",
                "thumbnailPath.png",
                "thumbnail",
                fileBytes);

            if (thumbnailImageUrl != null)
            {
                // Update Thumbnail Path
                target.ThumbnailPath = thumbnailImageUrl;
            }
        }

        /// <summary>
        /// Clone File1,File2,File3,File4,File5
        /// </summary>
        private void CloneItemFiles(Item target, string mapPath)
        {
            byte[] fileBytes;
            string extension;
            string path;
            if (!string.IsNullOrEmpty((target.File1)) && File.Exists(target.File1))
            {
                fileBytes = File.ReadAllBytes(target.File1);
                extension = Path.GetExtension(target.File1);
                path = SaveImage(mapPath, string.Empty,
                target.ItemId + target.ProductCode + target.ProductName + "_File1_",
                "file1" + extension,
                "file1",
                fileBytes);

                if (path != null)
                {
                    // Update File1
                    target.File1 = path;
                }
            }
            
            

            if (!string.IsNullOrEmpty((target.File2)) && File.Exists(target.File2))
            {
                fileBytes = File.ReadAllBytes(target.File2);
                extension = Path.GetExtension(target.File2);
                path = SaveImage(mapPath, string.Empty,
                    target.ItemId + target.ProductCode + target.ProductName + "_File2_",
                    "file2" + extension,
                    "file2",
                    fileBytes);

                if (path != null)
                {
                    // Update File2
                    target.File2 = path;
                }
            }
            
            if (!string.IsNullOrEmpty((target.File3)) && File.Exists(target.File3))
            {
                fileBytes = File.ReadAllBytes(target.File3);
                extension = Path.GetExtension(target.File3);
                path = SaveImage(mapPath, string.Empty,
                    target.ItemId + target.ProductCode + target.ProductName + "_File3_",
                    "file3" + extension,
                    "file3",
                    fileBytes);

                if (path != null)
                {
                    // Update File3
                    target.File3 = path;
                }
            }

            if (!string.IsNullOrEmpty((target.File4)) && File.Exists(target.File4))
            {
                fileBytes = File.ReadAllBytes(target.File4);
                extension = Path.GetExtension(target.File4);
                path = SaveImage(mapPath, string.Empty,
                    target.ItemId + target.ProductCode + target.ProductName + "_File4_",
                    "file4" + extension,
                    "file4",
                    fileBytes);

                if (path != null)
                {
                    // Update File4
                    target.File4 = path;
                }

            }

            if (!string.IsNullOrEmpty((target.File5)) && File.Exists(target.File5))
            {
                fileBytes = File.ReadAllBytes(target.File5);
                extension = Path.GetExtension(target.File5);
                path = SaveImage(mapPath, string.Empty,
                    target.ItemId + target.ProductCode + target.ProductName + "_File5_",
                    "file5" + extension,
                    "file5",
                    fileBytes);

                if (path != null)
                {
                    // Update File5
                    target.File5 = path;
                }
            }
        }

        /// <summary>
        /// Clone Product Images
        /// </summary>
        private void CloneProductImages(Item target)
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
            CloneItemStockOptionImages(target, mapPath);

            // Thumbnail Path
            CloneThumbnailPath(target, mapPath);

            // Grid Image
            CloneGridImage(target, mapPath);

            // Image Path
            CloneImagePath(target, mapPath);

            // Files 1,2,3,4,5
            CloneItemFiles(target, mapPath);
        }

        /// <summary>
        /// Creates Copy of Product
        /// </summary>
        private void CloneItem(Item source, Item target)
        {
            // Clone Item
            source.Clone(target);
            
            // Clone Item Product Detail
            CloneItemProductDetail(source, target);

            // Clone Item Vdp Prices
            CloneItemVdpPrices(source, target);
            
            // Clone Item Sections
            CloneItemSections(source, target);

            // Clone Item Stock Options
            CloneItemStockOptions(source, target);

            // Clone Item Price Matrices
            CloneItemPriceMatrices(source, target);

            // Clone Item States
            CloneItemStateTaxes(source, target);

            // Clone Product Categories
            CloneProductCategoryItems(source, target);

            // Clone Item Related Items
            CloneItemRelatedItems(source, target);

            // Save Changes
            itemRepository.SaveChanges();
            
            // Copy Files and place them under new Product folder
            CloneProductImages(target);

            // Clone Template - Call CloneTemplate from TemplateService
            // That will clone Template deeply
            if (source.TemplateId.HasValue)
            {
                long templateId = templateService.CopyTemplate(source.TemplateId.Value, 0, string.Empty, target.OrganisationId.HasValue ? 
                    target.OrganisationId.Value : itemRepository.OrganisationId);

                target.TemplateId = templateId;
            }

            // Save Changes
            itemRepository.SaveChanges();
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
            IProductCategoryRepository productCategoryRepository, ITemplatePageService templatePageService, ITemplateService templateService,
            IMachineRepository machineRepository, IPaperSizeRepository paperSizeRepository, IItemSectionRepository itemSectionRepository)
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
            if (templateService == null)
            {
                throw new ArgumentNullException("templateService");
            }
            if (machineRepository == null)
            {
                throw new ArgumentNullException("machineRepository");
            }
            if (paperSizeRepository == null)
            {
                throw new ArgumentNullException("paperSizeRepository");
            }
            if (itemSectionRepository == null)
            {
                throw new ArgumentNullException("itemSectionRepository");
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
            this.templateService = templateService;
            this.machineRepository = machineRepository;
            this.paperSizeRepository = paperSizeRepository;
            this.itemSectionRepository = itemSectionRepository;
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
            Item itemTarget = GetById(item.ItemId) ?? CreateNewItem();
            
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
                DeleteProductCategoryItem = DeleteProductCategoryItem,
                CreateItemSection = CreateItemSection,
                SetDefaultsForItemSection = SetNonPrintItemSection,
                DeleteItemSection = DeleteItemSection
            });

            // Save Changes
            itemRepository.SaveChanges();

            // Save Images and Update Item
            SaveProductImages(itemTarget);

            // Load Properties if Any
            itemTarget = itemRepository.Find(itemTarget.ItemId);

            // Genereate Pdf for Template if required using Template Services
            GenereatePdfForTemplate(itemTarget, item.TemplateTypeMode);

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
        public ItemBaseResponse GetBaseData()
        {
            return new ItemBaseResponse
            {
                CostCentres = costCentreRepository.GetAllNonSystemCostCentres(),
                SectionFlags = sectionFlagRepository.GetAllForCustomerPriceIndex(),
                Countries = countryRepository.GetAll(),
                States = stateRepository.GetAll(),
                Suppliers = companyRepository.GetAllSuppliers(),
                ProductCategories = productCategoryRepository.GetParentCategories(),
                PaperSizes = paperSizeRepository.GetAll()
            };
        }

        /// <summary>
        /// Get Base Data For Designer Template
        /// </summary>
        public ItemDesignerTemplateBaseResponse GetBaseDataForDesignerTemplate()
        {
            List<ProductCategory> templateCategories;
            List<CategoryRegion> categoryRegions;
            List<CategoryType> categoryTypes;
            // ReSharper disable SuggestUseVarKeywordEvident
            using (GlobalTemplateDesigner.TemplateSvcSPClient pSc = new GlobalTemplateDesigner.TemplateSvcSPClient())
            // ReSharper restore SuggestUseVarKeywordEvident
            {
                templateCategories = pSc.GetCategories().Select(category => new ProductCategory
                {
                    ProductCategoryId = category.ProductCategoryID,
                    CategoryName = category.CategoryName,
                    RegionId = category.RegionID,
                    CategoryTypeId = category.CatagoryTypeID,
                    ZoomFactor = category.ZoomFactor,
                    ScaleFactor = category.ScaleFactor
                }).ToList();
                categoryRegions = pSc.getCategoryRegions().Select(category => new CategoryRegion
                {
                    RegionId = category.RegionID,
                    RegionName = category.RegionName
                }).ToList();

                categoryTypes = pSc.getCategoryTypes().Select(category => new CategoryType
                {
                    TypeId = category.TypeID,
                    TypeName = category.TypeName
                }).ToList();

            }

            return new ItemDesignerTemplateBaseResponse
            {
                TemplateCategories = templateCategories,
                CategoryRegions = categoryRegions,
                CategoryTypes = categoryTypes
            };
        }

        /// <summary>
        /// Get Machines
        /// Used in Products - Press Selection
        /// </summary>
        public MachineSearchResponse GetMachines(MachineSearchRequestModel request)
        {
            return machineRepository.GetMachinesForProduct(request);
        }
        
        /// <summary>
        /// Clone Product
        /// </summary>
        public Item CloneProduct(long itemId)
        {
            // Find Item - Throws Exception if not exist
            Item source = GetById(itemId);

            // Create New Instance
            // And Generate ItemCode for it
            Item target = CreateNewItem();

            // Clone
            CloneItem(source, target);

            // Return Product
            return target;
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
