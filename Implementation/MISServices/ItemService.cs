using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
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
        private readonly IItemImageRepository itemImageRepository;
        private readonly IOrganisationRepository organizationRepository;
        private readonly ISmartFormRepository smartFormRepository;
        private readonly ILengthConversionService lengthConversionService;
        private readonly ITemplateObjectRepository templateObjectRepository;
        private readonly IProductMarketBriefQuestionRepository productMarketBriefQuestionRepository;
        private readonly IProductMarketBriefAnswerRepository productMarketBriefAnswerRepository;
        private readonly ISectionInkCoverageRepository sectionInkCoverageRepository;

        /// <summary>
        /// Create ProductMarketBriefQuestion
        /// </summary>
        private ProductMarketBriefQuestion CreateProductMarketBriefQuestion()
        {
            ProductMarketBriefQuestion line = productMarketBriefQuestionRepository.Create();
            productMarketBriefQuestionRepository.Add(line);
            return line;
        }

        /// <summary>
        /// Delete ProductMarketBriefQuestion
        /// </summary>
        private void DeleteProductMarketBriefQuestion(ProductMarketBriefQuestion line)
        {
            productMarketBriefQuestionRepository.Delete(line);
        }

        /// <summary>
        /// Create ProductMarketBriefAnswer
        /// </summary>
        private ProductMarketBriefAnswer CreateProductMarketBriefAnswer()
        {
            ProductMarketBriefAnswer line = productMarketBriefAnswerRepository.Create();
            productMarketBriefAnswerRepository.Add(line);
            return line;
        }

        /// <summary>
        /// Delete ProductMarketBriefAnswer
        /// </summary>
        private void DeleteProductMarketBriefAnswer(ProductMarketBriefAnswer line)
        {
            productMarketBriefAnswerRepository.Delete(line);
        }

        /// <summary>
        /// Delete Template Object
        /// </summary>
        private void DeleteTemplateObject(List<TemplatePage> templatePages)
        {
            // Return if no template pages
            if (templatePages.Count == 0)
            {
                return;
            }

            List<TemplateObject> templateObjects = 
                templateObjectRepository.GetByTemplatePages(templatePages.Select(tp => (long?)tp.ProductPageId).ToList()).ToList();
            templateObjects.ForEach(tempObj => templateObjectRepository.Delete(tempObj));
        }

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
        /// Creates New Section Ink Coverage
        /// </summary>
        private SectionInkCoverage CreateSectionInkCoverage()
        {
            SectionInkCoverage itemTarget = sectionInkCoverageRepository.Create();
            sectionInkCoverageRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Delete Section Ink Coverage
        /// </summary>
        private void DeleteSectionInkCoverage(SectionInkCoverage item)
        {
            sectionInkCoverageRepository.Delete(item);
        }

        /// <summary>
        /// Create Item Image
        /// </summary>
        private ItemImage CreateItemImage()
        {
            ItemImage line = itemImageRepository.Create();
            itemImageRepository.Add(line);

            return line;
        }

        /// <summary>
        /// Delete Item Image
        /// </summary>
        private void DeleteItemImage(ItemImage line)
        {
            itemImageRepository.Delete(line);
        }

        /// <summary>
        /// Template Pages BackgroundFileName updation
        /// </summary>
        private void UpdateTemplatePagesBackgroundFileNames(Item itemTarget)
        {
            if (itemTarget.Template != null && itemTarget.Template.ProductId > 0)
            {
                if (itemTarget.Template.TemplatePages != null)
                {
                    List<TemplatePage> newlyAddedTemplatePages = 
                        itemTarget.Template.TemplatePages.Where(tmp => ((tmp.IsNewlyAdded.HasValue && tmp.IsNewlyAdded.Value) || 
                            (tmp.OldPageNo != tmp.PageNo))).ToList();
                    foreach (TemplatePage templatePage in newlyAddedTemplatePages)
                    {
                        templatePage.BackgroundFileName = itemTarget.Template.ProductId + "/Side" + templatePage.PageNo + ".pdf";
                        templatePage.BackGroundType = 1;
                    }
                }

                // Convert Template Length to Points
                ConvertTemplateLengthToPoints(itemTarget);
            }
        }

        /// <summary>
        /// Converts Template Length to Points
        /// </summary>
        private void ConvertTemplateLengthToPoints(Item itemTarget)
        {
            Organisation organisation = organizationRepository.GetOrganizatiobByID();

            if (organisation == null || organisation.LengthUnit == null)
            {
                return;
            }
            
            if (itemTarget.Template.PDFTemplateHeight.HasValue && itemTarget.Template.PDFTemplateHeight.Value > 0)
            {
                itemTarget.Template.PDFTemplateHeight = 
                    lengthConversionService.ConvertLengthFromSystemUnitToPoints(itemTarget.Template.PDFTemplateHeight.Value, organisation.LengthUnit);
            }

            if (itemTarget.Template.PDFTemplateWidth.HasValue && itemTarget.Template.PDFTemplateWidth.Value > 0)
            {
                itemTarget.Template.PDFTemplateWidth = 
                    lengthConversionService.ConvertLengthFromSystemUnitToPoints(itemTarget.Template.PDFTemplateWidth.Value, organisation.LengthUnit);
            }

            // Convert Template Pages length to Points
            if (itemTarget.Template.TemplatePages == null)
            {
                return;
            }

            foreach (TemplatePage templatePage in itemTarget.Template.TemplatePages)
            {
                if (templatePage.Height.HasValue && templatePage.Height.Value > 0)
                {
                    templatePage.Height = lengthConversionService.ConvertLengthFromSystemUnitToPoints(templatePage.Height.Value, organisation.LengthUnit);
                }

                if (templatePage.OldHeight.HasValue && templatePage.OldHeight.Value > 0)
                {
                    templatePage.OldHeight = lengthConversionService.ConvertLengthFromSystemUnitToPoints(templatePage.OldHeight.Value, organisation.LengthUnit);
                }

                if (templatePage.Width.HasValue && templatePage.Width.Value > 0)
                {
                    templatePage.Width = lengthConversionService.ConvertLengthFromSystemUnitToPoints(templatePage.Width.Value, organisation.LengthUnit);
                }

                if (templatePage.OldWidth.HasValue && templatePage.OldWidth.Value > 0)
                {
                    templatePage.OldWidth = lengthConversionService.ConvertLengthFromSystemUnitToPoints(templatePage.OldWidth.Value, organisation.LengthUnit);
                }
            }
        }

        /// <summary>
        /// Converts Template Length to System Unit
        /// </summary>
        private void ConvertTemplateLengthToSystemUnit(Item itemTarget)
        {
            if (itemTarget.Template == null || itemTarget.Template.ProductId <= 0)
            {
                return;
            }

            Organisation organisation = organizationRepository.GetOrganizatiobByID();

            if (organisation == null || organisation.LengthUnit == null)
            {
                return;
            }

            if (itemTarget.Template.PDFTemplateHeight.HasValue && itemTarget.Template.PDFTemplateHeight.Value > 0)
            {
                itemTarget.Template.PDFTemplateHeight = 
                    lengthConversionService.ConvertLengthFromPointsToSystemUnit(itemTarget.Template.PDFTemplateHeight.Value, organisation.LengthUnit);
            }

            if (itemTarget.Template.PDFTemplateWidth.HasValue && itemTarget.Template.PDFTemplateWidth.Value > 0)
            {
                itemTarget.Template.PDFTemplateWidth = 
                    lengthConversionService.ConvertLengthFromPointsToSystemUnit(itemTarget.Template.PDFTemplateWidth.Value, organisation.LengthUnit);
            }

            // Convert Template Pages length to Points
            if (itemTarget.Template.TemplatePages == null)
            {
                return;
            }

            foreach (TemplatePage templatePage in itemTarget.Template.TemplatePages)
            {
                if (templatePage.Height.HasValue && templatePage.Height.Value > 0)
                {
                    templatePage.Height = lengthConversionService.ConvertLengthFromPointsToSystemUnit(templatePage.Height.Value, organisation.LengthUnit);
                }

                if (templatePage.Width.HasValue && templatePage.Width.Value > 0)
                {
                    templatePage.Width = lengthConversionService.ConvertLengthFromPointsToSystemUnit(templatePage.Width.Value, organisation.LengthUnit);
                }
            }
        }

        /// <summary>
        /// Save Product Images
        /// </summary>
        private void SaveProductImages(Item target, List<ItemImage> itemImagesRemoved)
        {
            string mpcContentPath = ConfigurationManager.AppSettings["MPC_Content"];
            HttpServerUtility server = HttpContext.Current.Server;
            string mapPath = server.MapPath(mpcContentPath + "/Products/" + itemRepository.OrganisationId + "/" + target.ItemId);

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

            // Item Images
            SaveItemImages(target, itemImagesRemoved, mapPath);
        }

        /// <summary>
        /// Saves Item Stock Option Images
        /// </summary>
        private void SaveItemStockOptionImages(Item target, string mapPath)
        {
            foreach (ItemStockOption itemStockOption in target.ItemStockOptions)
            {
                // Write Image
                SaveItemStockOptionImage(mapPath, itemStockOption);
            }
        }

        /// <summary>
        /// Save Item Stock Option Image
        /// </summary>
        private void SaveItemStockOptionImage(string mapPath, ItemStockOption itemStockOption)
        {
            string imageUrl = SaveImage(mapPath, itemStockOption.ImageURL,
                itemStockOption.ItemStockOptionId + "_" + StringHelper.SimplifyString(itemStockOption.StockLabel) + "_StockOption_",
                itemStockOption.FileName,
                itemStockOption.FileSource,
                itemStockOption.FileSourceBytes);

            if (imageUrl != null)
            {
                itemStockOption.ImageURL = imageUrl;
            }
        }

        /// <summary>
        /// Saves Item Images
        /// </summary>
        private void SaveItemImages(Item target, List<ItemImage> itemImagesRemoved, string mapPath)
        {
            foreach (ItemImage itemImage in target.ItemImages)
            {
                // Write Image
                SaveItemImage(mapPath, itemImage);
            }

            // Delete Files From File System that have been removed from Db
            itemImagesRemoved.ForEach(DeleteItemImageFile);
        }

        /// <summary>
        /// Save Item Image
        /// </summary>
        private void SaveItemImage(string mapPath, ItemImage itemImage)
        {
            string imageUrl = SaveImage(mapPath, itemImage.ImageURL,
                itemImage.ProductImageId + "_ItemImage_",
                itemImage.FileName,
                itemImage.FileSource,
                itemImage.FileSourceBytes);

            if (imageUrl != null)
            {
                itemImage.ImageURL = imageUrl;
            }
        }

        /// <summary>
        /// Delete Item Image
        /// </summary>
        private void DeleteItemImageFile(ItemImage itemImage)
        {
            SaveImage(string.Empty, itemImage.ImageURL,
                itemImage.ProductImageId + "_ItemImage_",
                itemImage.FileName,
                itemImage.FileSource,
                itemImage.FileSourceBytes, true);
        }

        /// <summary>
        /// Save Image Path
        /// </summary>
        private void SaveImagePath(Item target, string mapPath)
        {
            string imagePathUrl = SaveImage(mapPath, target.ImagePath,
                target.ItemId + "_" + StringHelper.SimplifyString(target.ProductName) + "_ImagePath_",
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
                target.ItemId + "_" + StringHelper.SimplifyString(target.ProductName) + "_GridImage_",
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
                target.ItemId + "_" + StringHelper.SimplifyString(target.ProductName) + "_ThumbnailPath_",
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
                target.ItemId + "_" + StringHelper.SimplifyString(target.ProductName) + "_File1_",
                target.File1Name,
                target.File1Byte,
                target.File1SourceBytes, target.File1Deleted.HasValue && target.File1Deleted.Value);

            if (path != null)
            {
                // Update File1
                target.File1 = path;
            }

            path = SaveImage(mapPath, target.File2,
              target.ItemId + "_" + StringHelper.SimplifyString(target.ProductName) + "_File2_",
                target.File2Name,
                target.File2Byte,
                target.File2SourceBytes, target.File2Deleted.HasValue && target.File2Deleted.Value);

            if (path != null)
            {
                // Update File2
                target.File2 = path;
            }

            path = SaveImage(mapPath, target.File3,
               target.ItemId + "_" + StringHelper.SimplifyString(target.ProductName) + "_File3_",
                target.File3Name,
                target.File3Byte,
                target.File3SourceBytes, target.File3Deleted.HasValue && target.File3Deleted.Value);

            if (path != null)
            {
                // Update File3
                target.File3 = path;
            }

            path = SaveImage(mapPath, target.File4,
                target.ItemId + "_" + StringHelper.SimplifyString(target.ProductName) + "_File4_",
                target.File4Name,
                target.File4Byte,
                target.File4SourceBytes, target.File4Deleted.HasValue && target.File4Deleted.Value);

            if (path != null)
            {
                // Update File4
                target.File4 = path;
            }

            path = SaveImage(mapPath, target.File5,
              target.ItemId + "_" + StringHelper.SimplifyString(target.ProductName) + "_File5_",
                target.File5Name,
                target.File5Byte,
                target.File5SourceBytes, target.File5Deleted.HasValue && target.File5Deleted.Value);

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
        /// <param name="fileDeleted">True if file has been deleted</param>
        /// <returns>Path of File being saved</returns>
        private string SaveImage(string mapPath, string existingImage, string caption, string fileName,
            string fileSource, byte[] fileSourceBytes, bool fileDeleted = false)
        {
            if (!string.IsNullOrEmpty(fileSource) || fileDeleted)
            {
                // Look if file already exists then replace it
                if (!string.IsNullOrEmpty(existingImage))
                {
                    if (Path.IsPathRooted(existingImage))
                    {
                        if (File.Exists(existingImage))
                        {
                            // Remove Existing File
                            File.Delete(existingImage);
                        }
                    }
                    else
                    {
                        string filePath = HttpContext.Current.Server.MapPath("~/" + existingImage);
                        if (File.Exists(filePath))
                        {
                            // Remove Existing File
                            File.Delete(filePath);
                        }
                    }

                }

                // If File has been deleted then set the specified field as empty
                // Used for File1, File2, File3, File4, File5
                if (fileDeleted)
                {
                    return string.Empty;
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
                        GeneratePdfFromTemplatePages(itemTarget, template, organisationId);
                    }
                    else if (itemTarget.TemplateType.Value == 2)
                    {
                        // Create Pre-Built Template from Pdf
                        // Save Template Pdf
                        var mapPath = SavePdfForPreBuiltTemplate(itemTarget);

                        // Return if edit case and no changes made to template type
                        if (string.IsNullOrEmpty(itemTarget.Template.FileSource))
                        {
                            return;
                        }

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

                templateService.generateTemplateFromPDF(HttpContext.Current.Server.MapPath("~/" + mapPath),
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
        private void GeneratePdfFromTemplatePages(Item itemTarget, Template template, long organisationId)
        {
            try
            {
                List<TemplatePage> templatePagesWithSameDimensions = template.TemplatePages.Where(tempPage =>
                    (tempPage.Height == template.PDFTemplateHeight) && (tempPage.Width == template.PDFTemplateWidth) &&
                    ((tempPage.IsNewlyAdded.HasValue && tempPage.IsNewlyAdded.Value) || 
                    (itemTarget.HasTemplateChangedToCustom.HasValue && itemTarget.HasTemplateChangedToCustom.Value) ||
                    (tempPage.OldPageNo != tempPage.PageNo) ||
                    (template.HasDeletedTemplatePages.HasValue && template.HasDeletedTemplatePages.Value)))
                    .ToList();

                // Pages with different dimensions - Added New Or have Updated the dimensions
                List<TemplatePage> templatePagesWithCustomDimensions = template.TemplatePages.Where(tempPage =>
                    ((tempPage.Height != template.PDFTemplateHeight) || (tempPage.Width != template.PDFTemplateWidth)) &&
                    ((tempPage.IsNewlyAdded.HasValue && tempPage.IsNewlyAdded.Value) ||
                    (itemTarget.HasTemplateChangedToCustom.HasValue && itemTarget.HasTemplateChangedToCustom.Value) || 
                    (tempPage.OldHeight != tempPage.Height || tempPage.OldWidth != tempPage.Width) ||
                    (tempPage.OldPageNo != tempPage.PageNo) ||
                    (template.HasDeletedTemplatePages.HasValue && template.HasDeletedTemplatePages.Value)))
                    .ToList();

                templatePageService.CreateBlankBackgroundPDFsByPages(template.ProductId,
                    template.PDFTemplateHeight.HasValue ? template.PDFTemplateHeight.Value : 0,
                    template.PDFTemplateWidth.HasValue ? template.PDFTemplateWidth.Value : 0,
                    1, templatePagesWithSameDimensions,
                    organisationId);

                templatePagesWithCustomDimensions.ForEach(tempPageCustom => 
                    templatePageService.CreatePageBlankBackgroundPDFs(
                    template.ProductId,
                    tempPageCustom,
                    tempPageCustom.Height.HasValue ? tempPageCustom.Height.Value : 0,
                    tempPageCustom.Width.HasValue ? tempPageCustom.Width.Value : 0,
                    organisationId));
            }
            catch (Exception exp)
            {
                throw new MPCException(
                    "Saved Successfully but " + LanguageResources.ItemService_FailedToGeneratePdfFromPages + ". " + exp.Message,
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
                server.MapPath(mpcContentPath + "/Products/" + itemRepository.OrganisationId +
                               "/" + itemTarget.ItemId);
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
            string itemCode = prefixRepository.GetNextItemCodePrefix(false);
            Item itemTarget = itemRepository.Create();
            itemRepository.Add(itemTarget);
            itemTarget.ItemCreationDateTime = DateTime.Now;
            itemTarget.ItemCode = itemCode;
            itemTarget.OrganisationId = itemRepository.OrganisationId;
            return itemTarget;
        }

        /// <summary>
        /// True Is Item Image New
        /// </summary>
        private bool IsNewItemImage(ItemImage source)
        {
            return source.ProductImageId == 0;
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
        /// Creates Copy of Section Ink Coverages
        /// </summary>
        private void CloneSectionInkCoverages(ItemSection itemSection, ItemSection targetItemSection)
        {
            if (targetItemSection.SectionInkCoverages == null)
            {
                targetItemSection.SectionInkCoverages = new List<SectionInkCoverage>();
            }

            foreach (SectionInkCoverage sectionInkCoverage in itemSection.SectionInkCoverages.ToList())
            {
                SectionInkCoverage targetSectionInkCoverage = sectionInkCoverageRepository.Create();
                sectionInkCoverageRepository.Add(targetSectionInkCoverage);
                targetSectionInkCoverage.SectionId = targetItemSection.ItemSectionId;
                targetItemSection.SectionInkCoverages.Add(targetSectionInkCoverage);
                sectionInkCoverage.Clone(targetSectionInkCoverage);
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

                // Clone Section Ink Coverages
                if (itemSection.SectionInkCoverages == null)
                {
                    continue;
                }

                // Copy Section Ink Coverages
                CloneSectionInkCoverages(itemSection, targetItemSection);
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
                targetItemStockOption.CompanyId = target.CompanyId;
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
        /// Copy Item Image Items
        /// </summary>
        private void CloneItemImageItems(Item source, Item target)
        {
            if (source.ItemImages == null)
            {
                return;
            }

            // Initialize List
            if (target.ItemImages == null)
            {
                target.ItemImages = new List<ItemImage>();
            }

            foreach (ItemImage itemImage in source.ItemImages)
            {
                ItemImage targetItemImage = itemImageRepository.Create();
                itemImageRepository.Add(targetItemImage);
                targetItemImage.ItemId = target.ItemId;
                target.ItemImages.Add(targetItemImage);
                itemImage.Clone(targetItemImage);
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
                CloneItemStockOptionImage(mapPath, itemStockOption);
            }
        }

        /// <summary>
        /// Clone Item Stock Option Image
        /// </summary>
        private void CloneItemStockOptionImage(string mapPath, ItemStockOption itemStockOption)
        {
            if (string.IsNullOrEmpty((itemStockOption.ImageURL)))
            {
                return;
            }

            byte[] fileBytes;
            if (Path.IsPathRooted(itemStockOption.ImageURL))
            {
                if (!File.Exists(itemStockOption.ImageURL))
                {
                    return;
                }

                fileBytes = File.ReadAllBytes(itemStockOption.ImageURL);
            }
            else
            {
                string path = HttpContext.Current.Server.MapPath("~/" + itemStockOption.ImageURL);
                if (!File.Exists(path))
                {
                    return;
                }

                fileBytes = File.ReadAllBytes(path);
            }

            string imageUrl = SaveImage(mapPath, string.Empty,
                itemStockOption.ItemStockOptionId + StringHelper.SimplifyString(itemStockOption.StockLabel) + "_StockOption_",
                "image.png",
                "stockOption",
                fileBytes);

            if (imageUrl != null)
            {
                itemStockOption.ImageURL = imageUrl;
            }
        }

        /// <summary>
        /// Clone Item Images
        /// </summary>
        private void CloneItemImages(Item target, string mapPath)
        {
            foreach (ItemImage itemImage in target.ItemImages)
            {
                // Write Image
                CloneItemImage(mapPath, itemImage);
            }
        }

        /// <summary>
        /// Clone Item Image
        /// </summary>
        private void CloneItemImage(string mapPath, ItemImage itemImage)
        {
            if (string.IsNullOrEmpty(itemImage.ImageURL))
            {
                return;
            }

            byte[] fileBytes;
            if (Path.IsPathRooted(itemImage.ImageURL))
            {
                if (!File.Exists(itemImage.ImageURL))
                {
                    return;
                }

                fileBytes = File.ReadAllBytes(itemImage.ImageURL);
            }
            else
            {
                if (!File.Exists("~/" + itemImage.ImageURL))
                {
                    return;
                }

                fileBytes = File.ReadAllBytes("~/" + itemImage.ImageURL);
            }

            string imageUrl = SaveImage(mapPath, string.Empty,
                itemImage.ProductImageId + "_ItemImage_",
                "image.png",
                "Image",
                fileBytes);

            if (imageUrl != null)
            {
                itemImage.ImageURL = imageUrl;
            }
        }

        /// <summary>
        /// Clone Image Path
        /// </summary>
        private void CloneImagePath(Item target, string mapPath)
        {
            if (string.IsNullOrEmpty(target.ImagePath))
            {
                return;
            }

            byte[] fileBytes;
            if (Path.IsPathRooted(target.ImagePath))
            {
                if (!File.Exists(target.ImagePath))
                {
                    return;
                }

                fileBytes = File.ReadAllBytes(target.ImagePath);
            }
            else
            {
                string path = HttpContext.Current.Server.MapPath("~/" + target.ImagePath);
                if (!File.Exists(path))
                {
                    return;
                }

                fileBytes = File.ReadAllBytes(path);
            }

            string imagePathUrl = SaveImage(mapPath, string.Empty,
                target.ItemId + "_" + StringHelper.SimplifyString(target.ProductName) + "_ImagePath_",
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
            if (string.IsNullOrEmpty(target.GridImage))
            {
                return;
            }

            byte[] fileBytes;
            if (Path.IsPathRooted(target.GridImage))
            {
                if (!File.Exists(target.GridImage))
                {
                    return;
                }

                fileBytes = File.ReadAllBytes(target.GridImage);
            }
            else
            {
                string path = HttpContext.Current.Server.MapPath("~/" + target.GridImage);
                if (!File.Exists(path))
                {
                    return;
                }

                fileBytes = File.ReadAllBytes(path);
            }

            string gridImageUrl = SaveImage(mapPath, string.Empty,
                target.ItemId + "_" + StringHelper.SimplifyString(target.ProductName) + "_GridImage_",
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
            if (string.IsNullOrEmpty(target.ThumbnailPath))
            {
                return;
            }

            byte[] fileBytes;
            if (Path.IsPathRooted(target.ThumbnailPath))
            {
                if (!File.Exists(target.ThumbnailPath))
                {
                    return;
                }

                fileBytes = File.ReadAllBytes(target.ThumbnailPath);
            }
            else
            {
                string path = HttpContext.Current.Server.MapPath("~/" + target.ThumbnailPath);
                if (!File.Exists(path))
                {
                    return;
                }

                fileBytes = File.ReadAllBytes(path);
            }

            string thumbnailImageUrl = SaveImage(mapPath, string.Empty,
                target.ItemId + "_" + StringHelper.SimplifyString(target.ProductName) + "_ThumbnailPath_",
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
            byte[] fileBytes = null;
            string extension = string.Empty;
            string path;
            if (!string.IsNullOrEmpty(target.File1))
            {
                if (Path.IsPathRooted(target.File1))
                {
                    if (File.Exists(target.File1))
                    {
                        fileBytes = File.ReadAllBytes(target.File1);
                        extension = Path.GetExtension(target.File1);
                    }
                }
                else
                {
                    string filePath = HttpContext.Current.Server.MapPath("~/" + target.File1);
                    if (File.Exists(filePath))
                    {
                        fileBytes = File.ReadAllBytes(filePath);
                        extension = Path.GetExtension(filePath);
                    }
                }

                if (fileBytes != null && !string.IsNullOrEmpty(extension))
                {
                    path = SaveImage(mapPath, string.Empty,
                    target.ItemId + "_" + StringHelper.SimplifyString(target.ProductName) + "_File1_",
                    "file1" + extension,
                    "file1",
                    fileBytes);

                    if (path != null)
                    {
                        // Update File1
                        target.File1 = path;
                    }
                }

            }

            fileBytes = null;
            extension = string.Empty;
            if (!string.IsNullOrEmpty(target.File2))
            {
                if (Path.IsPathRooted(target.File2))
                {
                    if (File.Exists(target.File2))
                    {
                        fileBytes = File.ReadAllBytes(target.File2);
                        extension = Path.GetExtension(target.File2);
                    }
                }
                else
                {
                    string filePath = HttpContext.Current.Server.MapPath("~/" + target.File2);
                    if (File.Exists(filePath))
                    {
                        fileBytes = File.ReadAllBytes(filePath);
                        extension = Path.GetExtension(filePath);
                    }
                }

                if (fileBytes != null && !string.IsNullOrEmpty(extension))
                {
                    path = SaveImage(mapPath, string.Empty,
                    target.ItemId + "_" + StringHelper.SimplifyString(target.ProductName) + "_File2_",
                    "file2" + extension,
                    "file2",
                    fileBytes);

                    if (path != null)
                    {
                        // Update File2
                        target.File2 = path;
                    }
                }

            }

            fileBytes = null;
            extension = string.Empty;
            if (!string.IsNullOrEmpty(target.File3))
            {
                if (Path.IsPathRooted(target.File3))
                {
                    if (File.Exists(target.File3))
                    {
                        fileBytes = File.ReadAllBytes(target.File3);
                        extension = Path.GetExtension(target.File3);
                    }
                }
                else
                {
                    string filePath = HttpContext.Current.Server.MapPath("~/" + target.File3);
                    if (File.Exists(filePath))
                    {
                        fileBytes = File.ReadAllBytes(filePath);
                        extension = Path.GetExtension(filePath);
                    }
                }

                if (fileBytes != null && !string.IsNullOrEmpty(extension))
                {
                    path = SaveImage(mapPath, string.Empty,
                    target.ItemId + "_" + StringHelper.SimplifyString(target.ProductName) + "_File3_",
                    "file3" + extension,
                    "file3",
                    fileBytes);

                    if (path != null)
                    {
                        // Update File3
                        target.File3 = path;
                    }
                }

            }

            fileBytes = null;
            extension = string.Empty;
            if (!string.IsNullOrEmpty(target.File4))
            {
                if (Path.IsPathRooted(target.File4))
                {
                    if (File.Exists(target.File4))
                    {
                        fileBytes = File.ReadAllBytes(target.File4);
                        extension = Path.GetExtension(target.File4);
                    }
                }
                else
                {
                    string filePath = HttpContext.Current.Server.MapPath("~/" + target.File4);
                    if (File.Exists(filePath))
                    {
                        fileBytes = File.ReadAllBytes(filePath);
                        extension = Path.GetExtension(filePath);
                    }
                }

                if (fileBytes != null && !string.IsNullOrEmpty(extension))
                {
                    path = SaveImage(mapPath, string.Empty,
                    target.ItemId + "_" + StringHelper.SimplifyString(target.ProductName) + "_File4_",
                    "file4" + extension,
                    "file4",
                    fileBytes);

                    if (path != null)
                    {
                        // Update File4
                        target.File4 = path;
                    }
                }

            }

            fileBytes = null;
            extension = string.Empty;
            if (!string.IsNullOrEmpty(target.File5))
            {
                if (Path.IsPathRooted(target.File5))
                {
                    if (File.Exists(target.File5))
                    {
                        fileBytes = File.ReadAllBytes(target.File5);
                        extension = Path.GetExtension(target.File5);
                    }
                }
                else
                {
                    string filePath = HttpContext.Current.Server.MapPath("~/" + target.File5);
                    if (File.Exists(filePath))
                    {
                        fileBytes = File.ReadAllBytes(filePath);
                        extension = Path.GetExtension(filePath);
                    }
                }

                if (fileBytes != null && !string.IsNullOrEmpty(extension))
                {
                    path = SaveImage(mapPath, string.Empty,
                     target.ItemId + "_" + StringHelper.SimplifyString(target.ProductName) + "_File5_",
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
        }

        /// <summary>
        /// Clone Product Images
        /// </summary>
        private void CloneProductImages(Item target)
        {
            string mpcContentPath = ConfigurationManager.AppSettings["MPC_Content"];
            HttpServerUtility server = HttpContext.Current.Server;
            string mapPath = server.MapPath(mpcContentPath + "/Products/" + itemRepository.OrganisationId + "/" + target.ItemId);

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

            // Item Images
            CloneItemImages(target, mapPath);
        }

        /// <summary>
        /// Creates Copy of Product Market Brief Answers
        /// </summary>
        private void CloneProductMarketBriefAnswer(ProductMarketBriefQuestion marketBriefQuestion, ProductMarketBriefQuestion targetmarketBriefQuestion)
        {
            if (targetmarketBriefQuestion.ProductMarketBriefAnswers == null)
            {
                targetmarketBriefQuestion.ProductMarketBriefAnswers = new List<ProductMarketBriefAnswer>();
            }

            foreach (ProductMarketBriefAnswer productMarketBriefAnswer in marketBriefQuestion.ProductMarketBriefAnswers)
            {
                ProductMarketBriefAnswer targetProductMarketBriefAnswer = productMarketBriefAnswerRepository.Create();
                productMarketBriefAnswerRepository.Add(targetProductMarketBriefAnswer);
                targetProductMarketBriefAnswer.MarketBriefQuestionId = targetmarketBriefQuestion.MarketBriefQuestionId;
                targetmarketBriefQuestion.ProductMarketBriefAnswers.Add(targetProductMarketBriefAnswer);
                productMarketBriefAnswer.Clone(targetProductMarketBriefAnswer);
            }
        }

        /// <summary>
        /// Copy Market Brief Questions
        /// </summary>
        private void CloneProductMarketBriefQuestions(Item source, Item target)
        {
            if (source.ProductMarketBriefQuestions == null)
            {
                return;
            }

            // Initialize List
            if (target.ProductMarketBriefQuestions == null)
            {
                target.ProductMarketBriefQuestions = new List<ProductMarketBriefQuestion>();
            }

            foreach (ProductMarketBriefQuestion productMarketBriefQuestion in source.ProductMarketBriefQuestions)
            {
                ProductMarketBriefQuestion targetProductMarketBriefQuestion = productMarketBriefQuestionRepository.Create();
                productMarketBriefQuestionRepository.Add(targetProductMarketBriefQuestion);
                targetProductMarketBriefQuestion.ItemId = target.ItemId;
                target.ProductMarketBriefQuestions.Add(targetProductMarketBriefQuestion);
                productMarketBriefQuestion.Clone(targetProductMarketBriefQuestion);

                // Clone Market Brief Answers
                if (productMarketBriefQuestion.ProductMarketBriefAnswers == null)
                {
                    continue;
                }

                // Clone Market Brief Answers
                CloneProductMarketBriefAnswer(productMarketBriefQuestion, targetProductMarketBriefQuestion);
            }
        }

        /// <summary>
        /// Creates Copy of Product
        /// </summary>
        private void CloneItem(Item source, Item target)
        {
            // Clone Item
            source.Clone(target);

            // Append Item Code to Product Name and Code
            AppendItemCodeToClonedProduct(target);

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

            // Clone Item Image Items
            CloneItemImageItems(source, target);

            // Clone Product Market Brief Questions
            CloneProductMarketBriefQuestions(source, target);

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

        /// <summary>
        /// Appends Item Code to Product Name and Code
        /// Validates the Product Name and Code Length
        /// If Exceeds them takes the part of it
        /// </summary>
        private static void AppendItemCodeToClonedProduct(Item target)
        {
            // Append Item Code in Product Name and Code to distinguish it from copied one
            target.ProductName += ' ' + target.ItemCode;
            target.ProductCode += ' ' + target.ItemCode;

            // Validate if Code exceeds the length
            if (target.ProductCode.Length > 50)
            {
                target.ProductCode = target.ProductCode.Substring(0, 49);
            }

            // Validate if Name exceeds the length
            if (target.ProductName.Length > 500)
            {
                target.ProductName = target.ProductName.Substring(0, 499);
            }
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
            IMachineRepository machineRepository, IPaperSizeRepository paperSizeRepository, IItemSectionRepository itemSectionRepository,
            IItemImageRepository itemImageRepository, IOrganisationRepository organizationRepository, ISmartFormRepository smartFormRepository,
            ILengthConversionService lengthConversionService, ITemplateObjectRepository templateObjectRepository, 
            IProductMarketBriefQuestionRepository productMarketBriefQuestionRepository, IProductMarketBriefAnswerRepository productMarketBriefAnswerRepository, 
            ISectionInkCoverageRepository sectionInkCoverageRepository)
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
            if (itemImageRepository == null)
            {
                throw new ArgumentNullException("itemImageRepository");
            }
            if (organizationRepository == null)
            {
                throw new ArgumentNullException("organizationRepository");
            }
            if (smartFormRepository == null)
            {
                throw new ArgumentNullException("smartFormRepository");
            }
            if (lengthConversionService == null)
            {
                throw new ArgumentNullException("lengthConversionService");
            }
            if (templateObjectRepository == null)
            {
                throw new ArgumentNullException("templateObjectRepository");
            }
            if (productMarketBriefQuestionRepository == null)
            {
                throw new ArgumentNullException("productMarketBriefQuestionRepository");
            }
            if (productMarketBriefAnswerRepository == null)
            {
                throw new ArgumentNullException("productMarketBriefAnswerRepository");
            }
            if (sectionInkCoverageRepository == null)
            {
                throw new ArgumentNullException("sectionInkCoverageRepository");
            }

            this.organizationRepository = organizationRepository;
            this.smartFormRepository = smartFormRepository;
            this.lengthConversionService = lengthConversionService;
            this.templateObjectRepository = templateObjectRepository;
            this.productMarketBriefQuestionRepository = productMarketBriefQuestionRepository;
            this.productMarketBriefAnswerRepository = productMarketBriefAnswerRepository;
            this.sectionInkCoverageRepository = sectionInkCoverageRepository;
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
            this.itemImageRepository = itemImageRepository;
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
        public Item GetById(long id, bool changeTemplateSizeUnits = true)
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

            // If Fetching item for Cloning then don't change Size values
            if (!changeTemplateSizeUnits)
            {
                return item;
            }
            
            // If template Exists then Convert the Height & Width to System Unit
            ConvertTemplateLengthToSystemUnit(item);

            return item;
        }

        /// <summary>
        /// Returns Pdf File  for Template
        /// </summary>
// ReSharper disable UnusedMember.Local
        private void GetTemplatePdfFile(Item item)
// ReSharper restore UnusedMember.Local
        {
            if (item.TemplateType == 2 && item.Template != null)
            {
                string mpcContentPath = ConfigurationManager.AppSettings["MPC_Content"];
                HttpServerUtility server = HttpContext.Current.Server;
                string mapPath =
                    server.MapPath(mpcContentPath + "/Products/" + itemRepository.OrganisationId + "/" + item.ItemId +
                                   "/Templates/random_CorporateTemplateUpload.pdf");
                if (File.Exists(mapPath))
                {
                    item.Template.FileOriginalBytes = File.ReadAllBytes(mapPath);
                }
            }
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

            // Check for Code Duplication
            bool isDuplicateCode = itemRepository.IsDuplicateProductCode(item.ProductCode, item.ItemId, item.CompanyId);
            if (isDuplicateCode)
            {
                throw new MPCException(LanguageResources.ItemService_ProductCodeDuplicated, itemRepository.OrganisationId);
            }

            // Item Images that are being removed
// ReSharper disable SuggestUseVarKeywordEvident
            List<ItemImage> itemImagesToBeRemoved = new List<ItemImage>();
// ReSharper restore SuggestUseVarKeywordEvident
            if (itemTarget.ItemImages != null && item.ItemImages != null)
            {
                itemImagesToBeRemoved = itemTarget.ItemImages.Where(
                ii => !IsNewItemImage(ii) && item.ItemImages.All(image => image.ProductImageId != ii.ProductImageId))
                  .ToList();
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
                DeleteProductCategoryItem = DeleteProductCategoryItem,
                CreateItemSection = CreateItemSection,
                SetDefaultsForItemSection = SetNonPrintItemSection,
                DeleteItemSection = DeleteItemSection,
                CreateItemImage = CreateItemImage,
                DeleteItemImage = DeleteItemImage,
                DeleteTemplateObject = DeleteTemplateObject,
                CreateProductMarketBriefQuestion = CreateProductMarketBriefQuestion,
                DeleteProductMarketBriefQuestion = DeleteProductMarketBriefQuestion,
                CreateProductMarketBriefAnswer = CreateProductMarketBriefAnswer,
                DeleteProductMarketBriefAnswer = DeleteProductMarketBriefAnswer,
                CreateSectionInkCoverage = CreateSectionInkCoverage,
                DeleteSectionInkCoverage = DeleteSectionInkCoverage
            });

            // Save Changes
            itemRepository.SaveChanges();

            // Save Images and Update Item
            SaveProductImages(itemTarget, itemImagesToBeRemoved);

            // Update Template Pages Background Image
            UpdateTemplatePagesBackgroundFileNames(itemTarget);

            // Save Changes
            itemRepository.SaveChanges();

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
            Organisation organisation = organizationRepository.GetOrganizatiobByID();
            return new ItemBaseResponse
            {
                CostCentres = costCentreRepository.GetAllNonSystemCostCentres(),
                SectionFlags = sectionFlagRepository.GetDefaultSectionFlags(),
                Countries = countryRepository.GetAll(),
                States = stateRepository.GetAll(),
                Suppliers = companyRepository.GetAllSuppliers(),
                PaperSizes = paperSizeRepository.GetAll(),
                LengthUnit = organisation != null && organisation.LengthUnit != null ? organisation.LengthUnit.UnitName : string.Empty,
                CurrencyUnit = organisation != null && organisation.Currency != null ? organisation.Currency.CurrencySymbol : string.Empty,
                WeightUnit = organisation != null && organisation.WeightUnit != null ? organisation.WeightUnit.UnitName : string.Empty,
                Inks = stockItemRepository.GetStockItemOfCategoryInk(),
                Machines = machineRepository.GetAll(),
                A4PaperStockItem = stockItemRepository.GetA4PaperStock()
            };
        }

        /// <summary>
        /// Get Base Data For Designer Template
        /// </summary>
        public ItemDesignerTemplateBaseResponse GetBaseDataForDesignerTemplate(long? companyId)
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

            // ReSharper disable SuggestUseVarKeywordEvident
            List<SmartForm> smartForms = new List<SmartForm>();
            // ReSharper restore SuggestUseVarKeywordEvident
            if (companyId.HasValue)
            {
                // Get Smart Forms for company
                smartForms = smartFormRepository.GetAllForCompany(companyId.Value).ToList();
            }


            return new ItemDesignerTemplateBaseResponse
            {
                TemplateCategories = templateCategories,
                CategoryRegions = categoryRegions,
                CategoryTypes = categoryTypes,
                SmartForms = smartForms
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
            Item source = GetById(itemId, false);

            // Create New Instance
            // And Generate ItemCode for it
            Item target = CreateNewItem();

            // Clone
            CloneItem(source, target);

            // Load Item Full
            target = itemRepository.GetItemWithDetails(target.ItemId);

            // Get Updated Minimum Price
            target.MinPrice = itemRepository.GetMinimumProductValue(target.ItemId);

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
        /// <summary>
        /// Method for Order Add Product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ItemSearchResponse GetItemsByCompanyId(ItemSearchRequestModel request)
        {
            return itemRepository.GetAllStoreProducts(request);
        }

        /// <summary>
        /// Get Product Categories for Company
        /// </summary>
        public IEnumerable<ProductCategory> GetProductCategoriesForCompany(long? companyId)
        {
            return productCategoryRepository.GetParentCategories(companyId);
        }
        /// <summary>
        /// Get Product Categories for Company Including Archived once also
        /// </summary>
        public IEnumerable<ProductCategory> GetProductCategoriesIncludingArchived(long? companyId)
        {
            return productCategoryRepository.GetParentCategoriesIncludingArchived(companyId);
        }

        /// <summary>
        /// Deletes Product Permanently
        /// </summary>
        public void DeleteProduct(long itemId)
        {
            DeleteItem(itemId, itemRepository.OrganisationId);
        }

        #endregion

        #region DeleteProducts

        public bool DeleteItem(long ItemID, long OrganisationID)
        {
            try
            {


                List<string> ImagesPath = new List<string>();
                Item DelItem = itemRepository.GetItemByItemID(ItemID);
                itemRepository.DeleteItemBySP(ItemID);
                if (DelItem != null)
                {

                    if (DelItem.ItemAttachments != null && DelItem.ItemAttachments.Count > 0)
                    {
                        foreach (var itemAttach in DelItem.ItemAttachments)
                        {
                            string path = itemAttach.FolderPath + itemAttach.FileName;

                            ImagesPath.Add(path);
                        }
                    }
                    if (DelItem.ItemStockOptions != null && DelItem.ItemStockOptions.Count > 0)
                    {
                        foreach (var itemStock in DelItem.ItemStockOptions)
                        {
                            string path = itemStock.ImageURL;

                            ImagesPath.Add(path);
                        }
                    }

                    // delete files


                    if (ImagesPath != null && ImagesPath.Count > 0)
                    {
                        foreach (var img in ImagesPath)
                        {
                            if (!string.IsNullOrEmpty(img))
                            {
                                string filePath = HttpContext.Current.Server.MapPath("~/" + img);
                                if (File.Exists(filePath))
                                {
                                    File.Delete(filePath);
                                }
                            }
                        }
                    }


                    if (DelItem.TemplateId != null && DelItem.TemplateId > 0)
                    {
                        templateService.DeleteTemplateFiles(DelItem.ItemId, OrganisationID);
                        // delete template folder
                    }

                    // delete item files
                    string SourceDelFiles = HttpContext.Current.Server.MapPath("/MPC_Content/products/" + OrganisationID + "/" + ItemID);

                    if (Directory.Exists(SourceDelFiles))
                    {
                        Directory.Delete(SourceDelFiles, true);
                    }

                    // delete itemattachments

                    string SourceDelAttachments = HttpContext.Current.Server.MapPath("/MPC_Content/Attachments/Organisation" + OrganisationID + "/" + OrganisationID + "/" + ItemID);

                    if (Directory.Exists(SourceDelAttachments))
                    {
                        Directory.Delete(SourceDelAttachments, true);
                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        #endregion

    }
}
