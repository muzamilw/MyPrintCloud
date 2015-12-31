using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Stock Item Mapper
    /// </summary>
    public static class ItemMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static Item CreateFrom(this DomainModels.Item source)
        {
            // ReSharper disable SuggestUseVarKeywordEvident
            Item item = new Item
            // ReSharper restore SuggestUseVarKeywordEvident
            {
                ItemId = source.ItemId,
                ItemCode = source.ItemCode,
                ProductCode = source.ProductCode,
                ProductName = source.ProductName,
                Title = source.Title,
                ProductSpecification = source.ProductSpecification,
                IsArchived = source.IsArchived,
                IsEnabled = source.IsEnabled,
                IsPublished = source.IsPublished,
                OrganisationId = source.OrganisationId,
                ProductType = source.ProductType,
                SortOrder = source.SortOrder,
                IsFeatured = source.IsFeatured,
                IsVdpProduct = source.IsVdpProduct,
                IsStockControl = source.IsStockControl,
                WebDescription = source.WebDescription,
                TipsAndHints = source.TipsAndHints,
                XeroAccessCode = source.XeroAccessCode,
                MetaTitle = source.MetaTitle,
                MetaDescription = source.MetaDescription,
                MetaKeywords = source.MetaKeywords,
                JobDescriptionTitle1 = source.JobDescriptionTitle1,
                JobDescription1 = source.JobDescription1,
                JobDescriptionTitle2 = source.JobDescriptionTitle2,
                JobDescription2 = source.JobDescription2,
                JobDescriptionTitle3 = source.JobDescriptionTitle3,
                JobDescription3 = source.JobDescription3,
                JobDescriptionTitle4 = source.JobDescriptionTitle4,
                JobDescription4 = source.JobDescription4,
                JobDescriptionTitle5 = source.JobDescriptionTitle5,
                JobDescription5 = source.JobDescription5,
                JobDescriptionTitle6 = source.JobDescriptionTitle6,
                JobDescription6 = source.JobDescription6,
                JobDescriptionTitle7 = source.JobDescriptionTitle7,
                JobDescription7 = source.JobDescription7,
                JobDescriptionTitle8 = source.JobDescriptionTitle8,
                JobDescription8 = source.JobDescription8,
                JobDescriptionTitle9 = source.JobDescriptionTitle9,
                JobDescription9 = source.JobDescription9,
                JobDescriptionTitle10 = source.JobDescriptionTitle10,
                JobDescription10 = source.JobDescription10,
                TemplateId = source.TemplateId,
                FlagId = source.FlagId,
                IsQtyRanged = source.IsQtyRanged,
                PackagingWeight = source.PackagingWeight,
                DefaultItemTax = source.DefaultItemTax,
                SupplierId = source.SupplierId,
                SupplierId2 = source.SupplierId2,
                EstimateProductionTime = source.EstimateProductionTime,
                MinPrice = source.MinPrice,
                TemplateType = source.TemplateType,
                IsTemplateDesignMode = source.IsTemplateDesignMode,
                IsCmyk = source.IsCmyk,
                ZoomFactor = source.ZoomFactor,
                Scalar = source.Scalar,
                DesignerCategoryId = source.DesignerCategoryId,
                ProductDisplayOptions = source.ProductDisplayOptions,
                IsRealStateProduct = source.IsRealStateProduct,
                IsUploadImage = source.IsUploadImage,
                IsDigitalDownload = source.IsDigitalDownload,
                PrintCropMarks = source.printCropMarks,
                DrawWaterMarkTxt = source.drawWaterMarkTxt,
                IsAddCropMarks = source.isAddCropMarks,
                DrawBleedArea = source.drawBleedArea,
                IsMultipagePdf = source.isMultipagePDF,
                AllowPdfDownload = source.allowPdfDownload,
                AllowImageDownload = source.allowImageDownload,
                ItemLength = source.ItemLength,
                ItemWeight = source.ItemWeight,
                ItemHeight = source.ItemHeight,
                ItemWidth = source.ItemWidth,
                CompanyId = source.CompanyId,
                SmartFormId = source.SmartFormId,
                ItemProductDetail = source.ItemProductDetails != null && source.ItemProductDetails.Count > 0 ?
                source.ItemProductDetails.FirstOrDefault().CreateFrom() : null,
                Template = source.Template != null ? source.Template.CreateFrom() : new Template(),
                ItemVdpPrices = source.ItemVdpPrices != null ? source.ItemVdpPrices.Select(vdp => vdp.CreateFrom()) : new List<ItemVdpPrice>(),
                ItemVideos = source.ItemVideos != null ? source.ItemVideos.Select(vdp => vdp.CreateFrom()) : new List<ItemVideo>(),
                ItemRelatedItems = source.ItemRelatedItems != null ? source.ItemRelatedItems.Select(vdp => vdp.CreateFrom()) : new List<ItemRelatedItem>(),
                ItemStockOptions = source.ItemStockOptions != null ? source.ItemStockOptions.Select(vdp => vdp.CreateFrom()) : new List<ItemStockOption>(),
                ItemStateTaxes = source.ItemStateTaxes != null ? source.ItemStateTaxes.Select(vdp => vdp.CreateFrom()) : new List<ItemStateTax>(),
                ItemPriceMatrices = source.ItemPriceMatrices != null ? source.ItemPriceMatrices.Select(vdp => vdp.CreateFrom()) : new List<ItemPriceMatrix>(),
                ProductCategoryItems = source.ProductCategoryItems != null ? source.ProductCategoryItems.Select(pci => pci.CreateFrom()) : new List<ProductCategoryItem>(),
                ItemSections = source.ItemSections != null ? source.ItemSections.Select(pci => pci.CreateFrom()) :
                new List<ItemSection>(),
                ItemImages = source.ItemImages != null ? source.ItemImages.Select(pci => pci.CreateFrom()) : new List<ItemImage>(),
                ProductMarketBriefQuestions = source.ProductMarketBriefQuestions != null ?
                source.ProductMarketBriefQuestions.Select(questions => questions.CreateFrom()).ToList().OrderBy(q => q.SortOrder) : null
            };

            // Load Thumbnail Image
            if (!string.IsNullOrEmpty(source.ThumbnailPath))
            {
                string thumbnailPath = HttpContext.Current.Server.MapPath("~/" + source.ThumbnailPath);
                if (File.Exists(thumbnailPath))
                {

                   // item.ThumbnailImage = File.ReadAllBytes(thumbnailPath);
                    item.ThumbnailPath = "/mis/" + source.ThumbnailPath;
                }
            }

            // Load Grid Image
            if (!string.IsNullOrEmpty(source.GridImage))
            {
                string gridImagePath = HttpContext.Current.Server.MapPath("~/" + source.GridImage);
                if (File.Exists(gridImagePath))
                {
                   // item.GridImageBytes = File.ReadAllBytes(gridImagePath);
                  //  item.GridImage = gridImagePath;
                    item.GridImage = "/mis/" + source.GridImage;
                }
            }

            // Load Image Path
            if (!string.IsNullOrEmpty(source.ImagePath))
            {
                string imagePath = HttpContext.Current.Server.MapPath("~/" + source.ImagePath);
                if (File.Exists(imagePath))
                {
                    item.ImagePathImage = File.ReadAllBytes(imagePath);
                }
            }
            
            // Load File1
            if (!string.IsNullOrEmpty(source.File1))
            {
                string file1Path = HttpContext.Current.Server.MapPath("~/" + source.File1);

                if (File.Exists(file1Path))
                {

                    item.File1 = GetfilePath(file1Path);
                  //  item.File1Bytes = File.ReadAllBytes(file1Path);
                }
            }

            // Load File2
            if (!string.IsNullOrEmpty(source.File2))
            {
                string file2Path = HttpContext.Current.Server.MapPath("~/" + source.File2);
                if (File.Exists(file2Path))
                {

                    item.File2 = GetfilePath(file2Path);
                   // item.File2Bytes = File.ReadAllBytes(file2Path);
                }
            }
            // Load File3
            if (!string.IsNullOrEmpty(source.File3))
            {
                string file3Path = HttpContext.Current.Server.MapPath("~/" + source.File3);
                if (File.Exists(file3Path))
                {
                    item.File3 = GetfilePath(file3Path);
                   // item.File3Bytes = File.ReadAllBytes(file3Path);
                }
            }
            // Load File4
            if (!string.IsNullOrEmpty(source.File4))
            {
                string file4Path = HttpContext.Current.Server.MapPath("~/" + source.File4);
                if (File.Exists(file4Path))
                {
                    item.File4 = GetfilePath(file4Path);
                  //  item.File4Bytes = File.ReadAllBytes(file4Path);
                }
            }
            // Load File5
            if (!string.IsNullOrEmpty(source.File5))
            {
                string file5Path = HttpContext.Current.Server.MapPath("~/" + source.File5);
                if (File.Exists(file5Path))
                {
                    item.File5 = GetfilePath(file5Path);
                  //  item.File5Bytes = File.ReadAllBytes(file5Path);
                }
            }

            return item;
        }

        public static string GetfilePath(string Path)
        {
            var url = "/mis/Content/Images/AnyFile.png";

            // for pdf
            if (Path.Contains(".pdf"))
            {
                url = "/mis/Content/Images/PDFFile.png";

            }// for psd
            else if (Path.Contains(".psd"))
            {
                url = "/mis/Content/Images/PSDFile.png";

            }// for ai
            else if (Path.Contains(".ai"))
            {
                url = "/mis/Content/Images/IllustratorFile.png";

            } // for indd
            else if (Path.Contains(".indd"))
            {
                url = "/mis/Content/Images/InDesignFile.png";

            }// for jpg
            else if (Path.Contains(".jpg") || Path.Contains(".jpeg"))
            {
                url = "/mis/Content/Images/JPGFile.png";

            }//for png
            else if (Path.Contains(".png"))
            {
                url = "/mis/Content/Images/PNGFile.png";

            }

            return url;
        }
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ItemListView CreateFromForListView(this DomainModels.GetItemsListView source)
        {
            // ReSharper disable SuggestUseVarKeywordEvident
            ItemListView item = new ItemListView
            // ReSharper restore SuggestUseVarKeywordEvident
            {
                ItemId = source.ItemId,
                ItemCode = source.ItemCode,
                ProductCode = source.ProductCode,
                ProductName = source.ProductName,
                ProductSpecification = source.ProductSpecification,
                ProductCategoryName = source.ProductCategoryName,
                IsArchived = source.IsArchived,
                IsEnabled = source.IsEnabled,
                IsPublished = source.IsPublished,
                MinPrice = source.MinPrice,
                OrganisationId = source.OrganisationId,
                CompanyId = source.CompanyId,
                PrintCropMarks = source.printCropMarks,
                DrawWaterMarkTxt = source.drawWaterMarkTxt,
                TemplateId = source.TemplateId,
                TemplateType = source.TemplateType,
                ProductType = source.ProductType
            };

            // Load Thumbnail Image
            if (!string.IsNullOrEmpty(source.ThumbnailPath))
            {
                string thumbnailPath = HttpContext.Current.Server.MapPath("~/" + source.ThumbnailPath);
                if (File.Exists(thumbnailPath))
                {

                    item.ThumbnailImage = File.ReadAllBytes(thumbnailPath);
                    item.ThumbnailPath = thumbnailPath;
                }
            }

            return item;
        }

        /// <summary>
        /// Crete From WebApi Model
        /// </summary>
        public static DomainModels.Item CreateFrom(this Item source)
        {
            return new DomainModels.Item
            {
                ItemId = source.ItemId,
                ItemCode = source.ItemCode,
                ProductCode = source.ProductCode,
                Title = source.Title,
                ProductName = source.ProductName,
                ProductSpecification = source.ProductSpecification,
                GridImage = source.GridImage,
                ThumbnailPath = source.ThumbnailPath,
                IsArchived = source.IsArchived,
                IsEnabled = source.IsEnabled,
                IsPublished = source.IsPublished,
                OrganisationId = source.OrganisationId,
                ProductType = source.ProductType,
                SortOrder = source.SortOrder,
                IsFeatured = source.IsFeatured,
                IsVdpProduct = source.IsVdpProduct,
                IsStockControl = source.IsStockControl,
                WebDescription = source.WebDescription,
                TipsAndHints = source.TipsAndHints,
                XeroAccessCode = source.XeroAccessCode,
                MetaTitle = source.MetaTitle,
                MetaDescription = source.MetaDescription,
                MetaKeywords = source.MetaKeywords,
                JobDescriptionTitle1 = source.JobDescriptionTitle1,
                JobDescription1 = source.JobDescription1,
                JobDescriptionTitle2 = source.JobDescriptionTitle2,
                JobDescription2 = source.JobDescription2,
                JobDescriptionTitle3 = source.JobDescriptionTitle3,
                JobDescription3 = source.JobDescription3,
                JobDescriptionTitle4 = source.JobDescriptionTitle4,
                JobDescription4 = source.JobDescription4,
                JobDescriptionTitle5 = source.JobDescriptionTitle5,
                JobDescription5 = source.JobDescription5,
                JobDescriptionTitle6 = source.JobDescriptionTitle6,
                JobDescription6 = source.JobDescription6,
                JobDescriptionTitle7 = source.JobDescriptionTitle7,
                JobDescription7 = source.JobDescription7,
                JobDescriptionTitle8 = source.JobDescriptionTitle8,
                JobDescription8 = source.JobDescription8,
                JobDescriptionTitle9 = source.JobDescriptionTitle9,
                JobDescription9 = source.JobDescription9,
                JobDescriptionTitle10 = source.JobDescriptionTitle10,
                JobDescription10 = source.JobDescription10,
                TemplateId = source.TemplateId,
                FlagId = source.FlagId,
                IsQtyRanged = source.IsQtyRanged,
                PackagingWeight = source.PackagingWeight,
                DefaultItemTax = source.DefaultItemTax,
                SupplierId = source.SupplierId,
                SupplierId2 = source.SupplierId2,
                EstimateProductionTime = source.EstimateProductionTime,
                TemplateType = source.TemplateType,
                IsTemplateDesignMode = source.IsTemplateDesignMode,
                IsCmyk = source.IsCmyk,
                ZoomFactor = source.ZoomFactor,
                Scalar = source.Scalar,
                TemplateTypeMode = source.TemplateTypeMode,
                DesignerCategoryId = source.DesignerCategoryId,
                CompanyId = source.CompanyId,
                ProductDisplayOptions = source.ProductDisplayOptions,
                IsRealStateProduct = source.IsRealStateProduct,
                IsUploadImage = source.IsUploadImage,
                IsDigitalDownload = source.IsDigitalDownload,
                printCropMarks = source.PrintCropMarks,
                drawWaterMarkTxt = source.DrawWaterMarkTxt,
                isAddCropMarks = source.IsAddCropMarks,
                drawBleedArea = source.DrawBleedArea,
                isMultipagePDF = source.IsMultipagePdf,
                allowPdfDownload = source.AllowPdfDownload,
                allowImageDownload = source.AllowImageDownload,
                ItemLength = source.ItemLength,
                ItemWeight = source.ItemWeight,
                ItemHeight = source.ItemHeight,
                ItemWidth = source.ItemWidth,
                SmartFormId = source.SmartFormId,
                Template = source.Template != null ? source.Template.CreateFrom() : new DomainModels.Template(),
                ItemVdpPrices = source.ItemVdpPrices != null ? source.ItemVdpPrices.Select(vdp => vdp.CreateFrom()).ToList() : new List<DomainModels.ItemVdpPrice>(),
                ItemVideos = source.ItemVideos != null ? source.ItemVideos.Select(vdp => vdp.CreateFrom()).ToList() : new List<DomainModels.ItemVideo>(),
                ItemRelatedItems = source.ItemRelatedItems != null ? source.ItemRelatedItems.Select(vdp => vdp.CreateFrom()).ToList() :
                new List<DomainModels.ItemRelatedItem>(),
                ItemStockOptions = source.ItemStockOptions != null ? source.ItemStockOptions.Select(stockOption => stockOption.CreateFrom()).ToList() :
                new List<DomainModels.ItemStockOption>(),
                ItemStateTaxes = source.ItemStateTaxes != null ? source.ItemStateTaxes.Select(vdp => vdp.CreateFrom()).ToList() :
                new List<DomainModels.ItemStateTax>(),
                ItemPriceMatrices = source.ItemPriceMatrices != null ? source.ItemPriceMatrices.Select(vdp => vdp.CreateFrom()).ToList() :
                new List<DomainModels.ItemPriceMatrix>(),
                ItemProductDetails = source.ItemProductDetail != null ? new List<DomainModels.ItemProductDetail> { source.ItemProductDetail.CreateFrom() } :
                new List<DomainModels.ItemProductDetail>(),
                ThumbnailImageName = source.ThumbnailImageName,
                ImagePathImageName = source.ImagePathImageName,
                GridImageSourceName = source.GridImageSourceName,
                ThumbnailImage = source.ThumbnailImageByte,
                GridImageBytes = source.GridImageSourceByte,
                ImagePathImage = source.ImagePathImageByte,
                File1Byte = source.File1Byte,
                File1Name = source.File1Name,
                File2Byte = source.File2Byte,
                File2Name = source.File2Name,
                File3Byte = source.File3Byte,
                File3Name = source.File3Name,
                File4Byte = source.File4Byte,
                File4Name = source.File4Name,
                File5Byte = source.File5Byte,
                File5Name = source.File5Name,
                File1Deleted = source.File1Deleted,
                File2Deleted = source.File2Deleted,
                File3Deleted = source.File3Deleted,
                File4Deleted = source.File4Deleted,
                File5Deleted = source.File5Deleted,
                ProductCategoryCustomItems = source.ProductCategoryItems != null ? source.ProductCategoryItems.Select(pci => pci.CreateFrom()).ToList() :
                new List<DomainModels.ProductCategoryItemCustom>(),
                ItemSections = source.ItemSections != null ? source.ItemSections.Select(pci => pci.CreateFrom()).ToList() :
                new List<DomainModels.ItemSection>(),
                ItemImages = source.ItemImages != null ? source.ItemImages.Select(pci => pci.CreateFrom()).ToList() :
                new List<DomainModels.ItemImage>(),
                ProductMarketBriefQuestions = source.ProductMarketBriefQuestions != null ? source.ProductMarketBriefQuestions.Select(pci => pci.CreateFrom()).ToList() :
                new List<DomainModels.ProductMarketBriefQuestion>()
            };
        }

        /// <summary>
        /// Crete From WebApi Model
        /// </summary>
        public static DomainModels.Item CreateFromForCompany(this Item source)
        {
            return new DomainModels.Item
            {
                ItemId = source.ItemId,
                ItemCode = source.ItemCode,
                ProductCode = source.ProductCode,
                ProductName = source.ProductName,
                ProductSpecification = source.ProductSpecification,
                GridImage = source.GridImage,
                ThumbnailPath = source.ThumbnailPath,
                IsArchived = source.IsArchived,
                IsEnabled = source.IsEnabled,
                IsPublished = source.IsPublished,
                OrganisationId = source.OrganisationId,
                ProductType = source.ProductType,
                SortOrder = source.SortOrder,
                IsFeatured = source.IsFeatured,
                IsVdpProduct = source.IsVdpProduct,
                IsStockControl = source.IsStockControl,
                WebDescription = source.WebDescription,
                TipsAndHints = source.TipsAndHints,
                XeroAccessCode = source.XeroAccessCode,
                MetaTitle = source.MetaTitle,
                MetaDescription = source.MetaDescription,
                MetaKeywords = source.MetaKeywords,
                JobDescriptionTitle1 = source.JobDescriptionTitle1,
                JobDescription1 = source.JobDescription1,
                JobDescriptionTitle2 = source.JobDescriptionTitle2,
                JobDescription2 = source.JobDescription2,
                JobDescriptionTitle3 = source.JobDescriptionTitle3,
                JobDescription3 = source.JobDescription3,
                JobDescriptionTitle4 = source.JobDescriptionTitle4,
                JobDescription4 = source.JobDescription4,
                JobDescriptionTitle5 = source.JobDescriptionTitle5,
                JobDescription5 = source.JobDescription5,
                JobDescriptionTitle6 = source.JobDescriptionTitle6,
                JobDescription6 = source.JobDescription6,
                JobDescriptionTitle7 = source.JobDescriptionTitle7,
                JobDescription7 = source.JobDescription7,
                JobDescriptionTitle8 = source.JobDescriptionTitle8,
                JobDescription8 = source.JobDescription8,
                JobDescriptionTitle9 = source.JobDescriptionTitle9,
                JobDescription9 = source.JobDescription9,
                JobDescriptionTitle10 = source.JobDescriptionTitle10,
                JobDescription10 = source.JobDescription10,
                TemplateId = source.TemplateId,
                FlagId = source.FlagId,
                IsQtyRanged = source.IsQtyRanged,
                PackagingWeight = source.PackagingWeight,
                DefaultItemTax = source.DefaultItemTax,
                SupplierId = source.SupplierId,
                SupplierId2 = source.SupplierId2,
                EstimateProductionTime = source.EstimateProductionTime,
                Template = source.Template != null ? source.Template.CreateFrom() : new DomainModels.Template(),
                ItemVdpPrices = source.ItemVdpPrices != null ? source.ItemVdpPrices.Select(vdp => vdp.CreateFrom()).ToList() : new List<DomainModels.ItemVdpPrice>(),
                ItemVideos = source.ItemVideos != null ? source.ItemVideos.Select(vdp => vdp.CreateFrom()).ToList() : new List<DomainModels.ItemVideo>(),
                ItemRelatedItems = source.ItemRelatedItems != null ? source.ItemRelatedItems.Select(vdp => vdp.CreateFrom()).ToList() :
                new List<DomainModels.ItemRelatedItem>(),
                ItemStockOptions = source.ItemStockOptions != null ? source.ItemStockOptions.Select(stockOption => stockOption.CreateFrom()).ToList() :
                new List<DomainModels.ItemStockOption>(),
                ItemStateTaxes = source.ItemStateTaxes != null ? source.ItemStateTaxes.Select(vdp => vdp.CreateFrom()).ToList() :
                new List<DomainModels.ItemStateTax>(),
                ItemPriceMatrices = source.ItemPriceMatrices != null ? source.ItemPriceMatrices.Select(vdp => vdp.CreateFrom()).ToList() :
                new List<DomainModels.ItemPriceMatrix>(),
                ItemProductDetails = source.ItemProductDetail != null ? new List<DomainModels.ItemProductDetail> { source.ItemProductDetail.CreateFrom() } :
                new List<DomainModels.ItemProductDetail>(),
                ThumbnailImageName = source.ThumbnailImageName,
                ImagePathImageName = source.ImagePathImageName,
                GridImageSourceName = source.GridImageSourceName,
                ThumbnailImage = source.ThumbnailImageByte,
                GridImageBytes = source.GridImageSourceByte,
                ImagePathImage = source.ImagePathImageByte,
                File1Byte = source.File1,
                File1Name = source.File1Name,
                File2Byte = source.File2,
                File2Name = source.File2Name,
                File3Byte = source.File3,
                File3Name = source.File3Name,
                File4Byte = source.File4,
                File4Name = source.File4Name,
                File5Byte = source.File5,
                File5Name = source.File5Name,
                ProductCategoryCustomItems = source.ProductCategoryItems != null ? source.ProductCategoryItems.Select(pci => pci.CreateFrom()).ToList() :
                new List<DomainModels.ProductCategoryItemCustom>()
            };
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ItemForWidgets CreateFromForWidgets(this DomainModels.Item source)
        {
            return new ItemForWidgets
            {
                ItemId = source.ItemId,
                ProductName = source.ProductName,
            };
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static OrderItem CreateFromForOrder(this DomainModels.Item source)
        {
            // ReSharper disable SuggestUseVarKeywordEvident
            OrderItem item = new OrderItem
            // ReSharper restore SuggestUseVarKeywordEvident
            {
                ItemId = source.ItemId,
                ItemCode = source.ItemCode,
                ProductCode = source.ProductCode,
                ProductName = source.ProductName,
                ProductType = source.ProductType,
                JobDescriptionTitle1 = source.JobDescriptionTitle1,
                JobDescription1 = source.JobDescription1,
                JobDescriptionTitle2 = source.JobDescriptionTitle2,
                JobDescription2 = source.JobDescription2,
                JobDescriptionTitle3 = source.JobDescriptionTitle3,
                JobDescription3 = source.JobDescription3,
                JobDescriptionTitle4 = source.JobDescriptionTitle4,
                JobDescription4 = source.JobDescription4,
                JobDescriptionTitle5 = source.JobDescriptionTitle5,
                JobDescription5 = source.JobDescription5,
                JobDescriptionTitle6 = source.JobDescriptionTitle6,
                JobDescription6 = source.JobDescription6,
                JobDescriptionTitle7 = source.JobDescriptionTitle7,
                JobDescription7 = source.JobDescription7,
                IsQtyRanged = source.IsQtyRanged,
                DefaultItemTax = source.DefaultItemTax,
                Qty1 = source.Qty1,
                Qty1NetTotal = source.Qty1NetTotal,
                StatusId = source.StatusId,
                Status = source.Status != null ? source.Status.StatusName : string.Empty,
                ItemNotes = source.ItemNotes,
                JobCode = source.JobCode,
                JobStatusId = source.JobStatusId,
                JobManagerId = source.JobManagerId,
                JobCreationDateTime = source.JobCreationDateTime,
                JobActualStartDateTime = source.JobActualStartDateTime,
                JobActualCompletionDateTime = source.JobActualCompletionDateTime,
                JobEstimatedStartDateTime = source.JobEstimatedStartDateTime,
                JobEstimatedCompletionDateTime = source.JobEstimatedCompletionDateTime,
                JobProgressedBy = source.JobProgressedBy,
                JobCardPrintedBy = source.JobCardPrintedBy,
                InvoiceDescription = source.InvoiceDescription,
                NominalCodeId = source.NominalCodeId,
                ProductCategories = source.ProductCategoryItems != null ? source.ProductCategoryItems.Select(pci => pci.ProductCategory.CategoryName) :
                new List<string>(),
                ItemSections = source.ItemSections != null ? source.ItemSections.Select(pci => pci.CreateFromForOrder()) :
                new List<ItemSection>(),
                Qty1MarkUpId1 = source.Qty1MarkUpId1,
                Qty2MarkUpId2 = source.Qty2MarkUpId2,
                Qty3MarkUpId3 = source.Qty3MarkUpId3,
                Qty2NetTotal = source.Qty2NetTotal,
                Qty3NetTotal = source.Qty3NetTotal,
                Qty1Tax1Value = source.Qty1Tax1Value,
                Qty2Tax1Value = source.Qty2Tax1Value,
                Qty3Tax1Value = source.Qty3Tax1Value,
                Qty1GrossTotal = source.Qty1GrossTotal,
                Qty2GrossTotal = source.Qty2GrossTotal,
                Qty3GrossTotal = source.Qty3GrossTotal,
                Qty2 = source.Qty2,
                Qty3 = source.Qty3,
                Tax1 = source.Tax1,
                ItemType = source.ItemType,
                EstimateId = source.EstimateId,
                JobSelectedQty = source.JobSelectedQty,
                ItemAttachments = source.ItemAttachments != null ? source.ItemAttachments.Select(attachment => attachment.CreateFrom()).ToList() : null
            };
            return item;
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static DomainModels.Item CreateFromForOrder(this OrderItem source)
        {
            // ReSharper disable SuggestUseVarKeywordEvident
            DomainModels.Item item = new DomainModels.Item
            // ReSharper restore SuggestUseVarKeywordEvident
            {
                ItemId = source.ItemId,
                ItemCode = source.ItemCode,
                ProductCode = source.ProductCode,
                ProductName = source.ProductName,
                ProductType = source.ProductType,
                JobDescriptionTitle1 = source.JobDescriptionTitle1,
                JobDescription1 = source.JobDescription1,
                JobDescriptionTitle2 = source.JobDescriptionTitle2,
                JobDescription2 = source.JobDescription2,
                JobDescriptionTitle3 = source.JobDescriptionTitle3,
                JobDescription3 = source.JobDescription3,
                JobDescriptionTitle4 = source.JobDescriptionTitle4,
                JobDescription4 = source.JobDescription4,
                JobDescriptionTitle5 = source.JobDescriptionTitle5,
                JobDescription5 = source.JobDescription5,
                JobDescriptionTitle6 = source.JobDescriptionTitle6,
                JobDescription6 = source.JobDescription6,
                JobDescriptionTitle7 = source.JobDescriptionTitle7,
                JobDescription7 = source.JobDescription7,
                IsQtyRanged = source.IsQtyRanged,
                DefaultItemTax = source.DefaultItemTax,
                Qty1 = source.Qty1,
                Qty1NetTotal = source.Qty1NetTotal,
                StatusId = source.StatusId,
                ItemNotes = source.ItemNotes,
                JobCode = source.JobCode,
                JobStatusId = source.JobStatusId,
                JobManagerId = source.JobManagerId,
                JobCreationDateTime = source.JobCreationDateTime,
                JobActualStartDateTime = source.JobActualStartDateTime,
                JobActualCompletionDateTime = source.JobActualCompletionDateTime,
                JobEstimatedStartDateTime = source.JobEstimatedStartDateTime,
                JobEstimatedCompletionDateTime = source.JobEstimatedCompletionDateTime,
                JobProgressedBy = source.JobProgressedBy,
                JobCardPrintedBy = source.JobCardPrintedBy,
                InvoiceDescription = source.InvoiceDescription,
                NominalCodeId = source.NominalCodeId,
                ItemSections = source.ItemSections != null ? source.ItemSections.Select(pci => pci.CreateFromForOrder()).ToList() :
                new List<DomainModels.ItemSection>(),
                Qty1MarkUpId1 = source.Qty1MarkUpId1,
                Qty2MarkUpId2 = source.Qty2MarkUpId2,
                Qty3MarkUpId3 = source.Qty3MarkUpId3,
                Qty2NetTotal = source.Qty2NetTotal,
                Qty3NetTotal = source.Qty3NetTotal,
                Qty1Tax1Value = source.Qty1Tax1Value,
                Qty2Tax1Value = source.Qty2Tax1Value,
                Qty3Tax1Value = source.Qty3Tax1Value,
                Qty1GrossTotal = source.Qty1GrossTotal,
                Qty2GrossTotal = source.Qty2GrossTotal,
                Qty3GrossTotal = source.Qty3GrossTotal,
                Qty2 = source.Qty2,
                Qty3 = source.Qty3,
                JobSelectedQty = source.JobSelectedQty,
                Tax1 = source.Tax1,
                ItemType = source.ItemType,
                EstimateId = source.EstimateId,
                RefItemId = source.RefItemId,
                ItemAttachments = source.ItemAttachments != null ? source.ItemAttachments.Select(attachment => attachment.CreateFrom()).ToList() : null
            };
            return item;
        }


        public static ItemListViewForOrder CreateFromForOrderAddProduct(this DomainModels.Item source)
        {
            // ReSharper disable SuggestUseVarKeywordEvident
            ItemListViewForOrder item = new ItemListViewForOrder
            // ReSharper restore SuggestUseVarKeywordEvident
            {
                ItemId = source.ItemId,
                ItemCode = source.ItemCode,
                ProductCode = source.ProductCode,
                ProductName = source.ProductName,
                ProductType = source.ProductType,
                IsQtyRanged = source.IsQtyRanged,
                DefaultItemTax = source.DefaultItemTax,
                JobDescriptionTitle1 = source.JobDescriptionTitle1,
                JobDescription1 = source.JobDescription1,
                JobDescriptionTitle2 = source.JobDescriptionTitle2,
                JobDescription2 = source.JobDescription2,
                JobDescriptionTitle3 = source.JobDescriptionTitle3,
                JobDescription3 = source.JobDescription3,
                JobDescriptionTitle4 = source.JobDescriptionTitle4,
                JobDescription4 = source.JobDescription4,
                JobDescriptionTitle5 = source.JobDescriptionTitle5,
                JobDescription5 = source.JobDescription5,
                JobDescriptionTitle6 = source.JobDescriptionTitle6,
                JobDescription6 = source.JobDescription6,
                JobDescriptionTitle7 = source.JobDescriptionTitle7,
                JobDescription7 = source.JobDescription7,
                CompanyId = source.CompanyId,
                CompanyName = source.Company != null ? source.Company.Name : string.Empty,
                ProductSpecification = source.ProductSpecification
            };
            // Load Thumbnail Image
            if (!string.IsNullOrEmpty(source.ThumbnailPath))
            {
                string thumbnailPath = HttpContext.Current.Server.MapPath("~/" + source.ThumbnailPath);
                if (File.Exists(thumbnailPath))
                {

                    item.ThumbnailImage = File.ReadAllBytes(thumbnailPath);
                    item.ThumbnailPath = thumbnailPath;
                }
            }
            return item;
        }

        

    }
}