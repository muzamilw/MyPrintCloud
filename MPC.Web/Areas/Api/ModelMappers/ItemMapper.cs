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
            string gridImagePath = HttpContext.Current.Server.MapPath("~/" + source.GridImage);
            string imagePath = HttpContext.Current.Server.MapPath("~/" + source.ImagePath);
            string thumbnailPath = HttpContext.Current.Server.MapPath("~/" + source.ThumbnailPath);
            string file1Path = HttpContext.Current.Server.MapPath("~/" + source.File1);
            string file2Path = HttpContext.Current.Server.MapPath("~/" + source.File2);
            string file3Path = HttpContext.Current.Server.MapPath("~/" + source.File3);
            string file4Path = HttpContext.Current.Server.MapPath("~/" + source.File4);
            string file5Path = HttpContext.Current.Server.MapPath("~/" + source.File5);

            // ReSharper disable SuggestUseVarKeywordEvident
            Item item = new Item
            // ReSharper restore SuggestUseVarKeywordEvident
            {
                ItemId = source.ItemId,
                ItemCode = source.ItemCode,
                ProductCode = source.ProductCode,
                ProductName = source.ProductName,
                ProductSpecification = source.ProductSpecification,
                GridImage = gridImagePath,
                ThumbnailPath = thumbnailPath,
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
                new List<ItemSection>()
            };

            // Load Thumbnail Image
            if (thumbnailPath != null && File.Exists(thumbnailPath))
            {
                item.ThumbnailImage = File.ReadAllBytes(thumbnailPath);
            }

            // Load Grid Image
            if (gridImagePath != null && File.Exists(gridImagePath))
            {
                item.GridImageBytes = File.ReadAllBytes(gridImagePath);
            }

            // Load Image Path
            if (imagePath != null && File.Exists(imagePath))
            {
                item.ImagePathImage = File.ReadAllBytes(imagePath);
            }

            // Load File1
            if (file1Path != null && File.Exists(file1Path))
            {
                item.File1Bytes = File.ReadAllBytes(file1Path);
            }

            // Load File2
            if (file2Path != null && File.Exists(file2Path))
            {
                item.File2Bytes = File.ReadAllBytes(file2Path);
            }
            // Load File3
            if (file3Path != null && File.Exists(file3Path))
            {
                item.File3Bytes = File.ReadAllBytes(file3Path);
            }
            // Load File4
            if (file4Path != null && File.Exists(file4Path))
            {
                item.File4Bytes = File.ReadAllBytes(file4Path);
            }
            // Load File5
            if (file5Path != null && File.Exists(file5Path))
            {
                item.File5Bytes = File.ReadAllBytes(file5Path);
            }

            return item;
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ItemListView CreateFromForListView(this DomainModels.GetItemsListView source)
        {
            string thumbnailpath = HttpContext.Current.Server.MapPath("~/" + source.ThumbnailPath);
            // ReSharper disable SuggestUseVarKeywordEvident
            ItemListView item = new ItemListView
            // ReSharper restore SuggestUseVarKeywordEvident
            {
                ItemId = source.ItemId,
                ItemCode = source.ItemCode,
                ProductCode = source.ProductCode,
                ProductName = source.ProductName,
                ProductSpecification = source.ProductSpecification,
                ThumbnailPath = thumbnailpath,
                ProductCategoryName = source.ProductCategoryName,
                IsArchived = source.IsArchived,
                IsEnabled = source.IsEnabled,
                IsPublished = source.IsPublished,
                MinPrice = source.MinPrice
            };

            if (thumbnailpath != null && File.Exists(thumbnailpath))
            {
                item.ThumbnailImage = File.ReadAllBytes(thumbnailpath);
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
                ProductCategoryCustomItems = source.ProductCategoryItems != null ? source.ProductCategoryItems.Select(pci => pci.CreateFrom()).ToList() :
                new List<DomainModels.ProductCategoryItemCustom>(),
                ItemSections = source.ItemSections != null ? source.ItemSections.Select(pci => pci.CreateFrom()).ToList() :
                new List<DomainModels.ItemSection>()
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
                ItemSections = source.ItemSections != null ? source.ItemSections.Select(pci => pci.CreateFrom()) :
                new List<ItemSection>()
            };

            return item;
        }

        public static ItemListView CreateFromForOrderAddProduct(this DomainModels.Item source)
        {
            string thumbnailpath = HttpContext.Current.Server.MapPath("~/" + source.ThumbnailPath);
            // ReSharper disable SuggestUseVarKeywordEvident
            ItemListView item = new ItemListView
            // ReSharper restore SuggestUseVarKeywordEvident
            {
                ItemId = source.ItemId,
                ItemCode = source.ItemCode,
                ProductCode = source.ProductCode,
                ProductName = source.ProductName,
                ProductSpecification = source.ProductSpecification,
                ThumbnailPath = thumbnailpath,
                // ReSharper disable once PossibleNullReferenceException
                ProductCategoryName = source.ProductCategoryItems.Count > 0? source.ProductCategoryItems.FirstOrDefault().ProductCategory.CategoryName : null,
                IsArchived = source.IsArchived,
                IsEnabled = source.IsEnabled,
                IsPublished = source.IsPublished,
                MinPrice = source.MinPrice
            };

            if (thumbnailpath != null && File.Exists(thumbnailpath))
            {
                item.ThumbnailImage = File.ReadAllBytes(thumbnailpath);
            }

            return item;
        }
    }
}