using System.Collections.Generic;
using System.Linq;
using System;
using MPC.Models.DomainModels;

namespace MPC.Models.ModelMappers
{

    /// <summary>
    /// Item mapper
    /// </summary>
    public static class ItemMapper
    {
        #region Private

        /// <summary>
        /// True if the ItemVdpPrice is new
        /// </summary>
        private static bool IsNewItemVdpPrice(ItemVdpPrice sourceItemVdpPrice)
        {
            return sourceItemVdpPrice.ItemVdpPriceId == 0;
        }

        /// <summary>
        /// Initialize target ItemVdpPrices
        /// </summary>
        private static void InitializeItemVdpPrices(Item item)
        {
            if (item.ItemVdpPrices == null)
            {
                item.ItemVdpPrices = new List<ItemVdpPrice>();
            }
        }

        /// <summary>
        /// Update lines
        /// </summary>
        private static void UpdateItemVdpPrices(Item source, Item target, ItemMapperActions actions)
        {
            InitializeItemVdpPrices(source);
            InitializeItemVdpPrices(target);

            UpdateOrAddItemVdpPrices(source, target, actions);
            DeleteItemVdpPrices(source, target, actions);
        }

        /// <summary>
        /// Delete lines no longer needed
        /// </summary>
        private static void DeleteItemVdpPrices(Item source, Item target, ItemMapperActions actions)
        {
            List<ItemVdpPrice> linesToBeRemoved = target.ItemVdpPrices.Where(
                vdp => !IsNewItemVdpPrice(vdp) && source.ItemVdpPrices.All(sourceVdp => sourceVdp.ItemVdpPriceId != vdp.ItemVdpPriceId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.ItemVdpPrices.Remove(line);
                actions.DeleteItemVdpPrice(line);
            });
        }

        /// <summary>
        /// Update or add Item Vdp Prices
        /// </summary>
        private static void UpdateOrAddItemVdpPrices(Item source, Item target, ItemMapperActions actions)
        {
            foreach (ItemVdpPrice sourceLine in source.ItemVdpPrices.ToList())
            {
                UpdateOrAddItemVdpPrice(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target lines 
        /// </summary>
        private static void UpdateOrAddItemVdpPrice(ItemVdpPrice sourceItemVdpPrice, Item target, ItemMapperActions actions)
        {
            ItemVdpPrice targetLine;
            if (IsNewItemVdpPrice(sourceItemVdpPrice))
            {
                targetLine = actions.CreateItemVdpPrice();
                target.ItemVdpPrices.Add(targetLine);
            }
            else
            {
                targetLine = target.ItemVdpPrices.FirstOrDefault(vdp => vdp.ItemVdpPriceId == sourceItemVdpPrice.ItemVdpPriceId);
            }
            sourceItemVdpPrice.UpdateTo(targetLine);
        }

        /// <summary>
        /// True if the ItemVideo is new
        /// </summary>
        private static bool IsNewItemVideo(ItemVideo sourceItemVideo)
        {
            return sourceItemVideo.VideoId <= 0;
        }

        /// <summary>
        /// Initialize target ItemVideos
        /// </summary>
        private static void InitializeItemVideos(Item item)
        {
            if (item.ItemVideos == null)
            {
                item.ItemVideos = new List<ItemVideo>();
            }
        }

        /// <summary>
        /// Update or add Item Vdp Prices
        /// </summary>
        private static void UpdateOrAddItemVideos(Item source, Item target, ItemMapperActions actions)
        {
            foreach (ItemVideo sourceLine in source.ItemVideos.ToList())
            {
                UpdateOrAddItemVideo(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target videos 
        /// </summary>
        private static void UpdateOrAddItemVideo(ItemVideo sourceItemVideo, Item target, ItemMapperActions actions)
        {
            ItemVideo targetLine;
            if (IsNewItemVideo(sourceItemVideo))
            {
                targetLine = actions.CreateItemVideo();
                target.ItemVideos.Add(targetLine);
            }
            else
            {
                targetLine = target.ItemVideos.FirstOrDefault(vdp => vdp.VideoId == sourceItemVideo.VideoId);
            }
            sourceItemVideo.UpdateTo(targetLine);
        }

        /// <summary>
        /// Delete videos no longer needed
        /// </summary>
        private static void DeleteItemVideos(Item source, Item target, ItemMapperActions actions)
        {
            List<ItemVideo> linesToBeRemoved = target.ItemVideos.Where(
                vdp => !IsNewItemVideo(vdp) && source.ItemVideos.All(sourceVdp => sourceVdp.VideoId != vdp.VideoId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.ItemVideos.Remove(line);
                actions.DeleteItemVideo(line);
            });
        }

        /// <summary>
        /// Update Videos
        /// </summary>
        private static void UpdateItemVideos(Item source, Item target, ItemMapperActions actions)
        {
            InitializeItemVideos(source);
            InitializeItemVideos(target);

            UpdateOrAddItemVideos(source, target, actions);
            DeleteItemVideos(source, target, actions);
        }

        /// <summary>
        /// True if the ItemRelatedItem is new
        /// </summary>
        private static bool IsNewItemRelatedItem(ItemRelatedItem sourceItemRelatedItem)
        {
            return sourceItemRelatedItem.Id == 0;
        }

        /// <summary>
        /// Initialize target ItemRelatedItems
        /// </summary>
        private static void InitializeItemRelatedItems(Item item)
        {
            if (item.ItemRelatedItems == null)
            {
                item.ItemRelatedItems = new List<ItemRelatedItem>();
            }
        }

        /// <summary>
        /// Update or add Item Vdp Prices
        /// </summary>
        private static void UpdateOrAddItemRelatedItems(Item source, Item target, ItemMapperActions actions)
        {
            foreach (ItemRelatedItem sourceLine in source.ItemRelatedItems.ToList())
            {
                UpdateOrAddItemRelatedItem(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target RelatedItems 
        /// </summary>
        private static void UpdateOrAddItemRelatedItem(ItemRelatedItem sourceItemRelatedItem, Item target, ItemMapperActions actions)
        {
            ItemRelatedItem targetLine;
            if (IsNewItemRelatedItem(sourceItemRelatedItem))
            {
                targetLine = actions.CreateItemRelatedItem();
                target.ItemRelatedItems.Add(targetLine);
            }
            else
            {
                targetLine = target.ItemRelatedItems.FirstOrDefault(vdp => vdp.Id == sourceItemRelatedItem.Id);
            }
            sourceItemRelatedItem.UpdateTo(targetLine);
        }

        /// <summary>
        /// Delete RelatedItems no longer needed
        /// </summary>
        private static void DeleteItemRelatedItems(Item source, Item target, ItemMapperActions actions)
        {
            List<ItemRelatedItem> linesToBeRemoved = target.ItemRelatedItems.Where(
                vdp => !IsNewItemRelatedItem(vdp) && source.ItemRelatedItems.All(sourceVdp => sourceVdp.Id != vdp.Id))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.ItemRelatedItems.Remove(line);
                actions.DeleteItemRelatedItem(line);
            });
        }

        /// <summary>
        /// Update RelatedItems
        /// </summary>
        private static void UpdateItemRelatedItems(Item source, Item target, ItemMapperActions actions)
        {
            InitializeItemRelatedItems(source);
            InitializeItemRelatedItems(target);

            UpdateOrAddItemRelatedItems(source, target, actions);
            DeleteItemRelatedItems(source, target, actions);
        }

        /// <summary>
        /// True if the TemplatePage is new
        /// </summary>
        private static bool IsNewTemplatePage(TemplatePage sourceTemplatePage)
        {
            return sourceTemplatePage.ProductPageId == 0;
        }

        /// <summary>
        /// Initialize target TemplatePages
        /// </summary>
        private static void InitializeTemplatePages(Item item)
        {
            if (item.Template.TemplatePages == null)
            {
                item.Template.TemplatePages = new List<TemplatePage>();
            }
        }

        /// <summary>
        /// Update or add Item Vdp Prices
        /// </summary>
        private static void UpdateOrAddTemplatePages(Item source, Item target, ItemMapperActions actions)
        {
            foreach (TemplatePage sourceLine in source.Template.TemplatePages.ToList())
            {
                UpdateOrAddTemplatePage(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target RelatedItems 
        /// </summary>
        private static void UpdateOrAddTemplatePage(TemplatePage sourceTemplatePage, Item target, ItemMapperActions actions)
        {
            TemplatePage targetLine;
            if (IsNewTemplatePage(sourceTemplatePage))
            {
                targetLine = actions.CreateTemplatePage();
                // Used this to Set the Default BackgroundFileName - that is to be set only for new ones
                targetLine.IsNewlyAdded = true;
                target.Template.TemplatePages.Add(targetLine);
            }
            else
            {
                targetLine = target.Template.TemplatePages.FirstOrDefault(vdp => vdp.ProductPageId == sourceTemplatePage.ProductPageId);
            }
            sourceTemplatePage.UpdateTo(targetLine);
        }

        /// <summary>
        /// Delete RelatedItems no longer needed
        /// </summary>
        private static void DeleteTemplatePages(Item source, Item target, ItemMapperActions actions)
        {
            List<TemplatePage> linesToBeRemoved = target.Template.TemplatePages.Where(
                vdp => !IsNewTemplatePage(vdp) && source.Template.TemplatePages.All(sourceVdp => sourceVdp.ProductPageId != vdp.ProductPageId))
                  .ToList();
            
            // If template pages are deleted then regenerate pdf
            if (linesToBeRemoved.Count > 0)
            {
                target.Template.HasDeletedTemplatePages = true;
            }

            // Remove Template Object related to Template Pages going to be deleted
            actions.DeleteTemplateObject(linesToBeRemoved);
            
            // Remove Template Pages
            linesToBeRemoved.ForEach(line =>
            {
                target.Template.TemplatePages.Remove(line);
                actions.DeleteTemplatePage(line);
            });
        }

        /// <summary>
        /// Update RelatedItems
        /// </summary>
        private static void UpdateTemplatePages(Item source, Item target, ItemMapperActions actions)
        {
            InitializeTemplatePages(source);
            InitializeTemplatePages(target);

            UpdateOrAddTemplatePages(source, target, actions);
            DeleteTemplatePages(source, target, actions);
        }

        /// <summary>
        /// Update Template
        /// </summary>
        private static void UpdateTemplate(Item source, Item target, ItemMapperActions actions)
        {
            // Initialize Template
            if (target.Template == null)
            {
                if (source.TemplateType.HasValue && source.TemplateType.Value != 3)
                {
                    target.Template = actions.CreateTemplate();    
                }
            }

            // Start Template with designer empty
            if ((source.Template == null || target.Template == null) || (source.TemplateType.HasValue && source.TemplateType.Value == 3))
            {
                if (target.TemplateId.HasValue && target.TemplateId.Value > 0)
                {
                    target.OldTemplateId = target.TemplateId;
                    target.TemplateId = null;
                }
                return;
            }

            // Update Template
            source.Template.UpdateTo(target.Template);
            switch (source.IsTemplateDesignMode)
            {
                case 1:
                    target.Template.IsCorporateEditable = true;
                    break;
                case 2:
                    target.Template.IsCorporateEditable = false;
                    break;
                default:
                    target.Template.IsCorporateEditable = null;
                    break;
            }

            // Set Template Name as Item Name
            target.Template.ProductName = target.ProductName;

            // Update Template Pages
            UpdateTemplatePages(source, target, actions);
        }

        /// <summary>
        /// True if the ItemStockOption is new
        /// </summary>
        private static bool IsNewItemStockOption(ItemStockOption sourceItemStockOption)
        {
            return sourceItemStockOption.ItemStockOptionId == 0;
        }

        /// <summary>
        /// Initialize target ItemStockOptions
        /// </summary>
        private static void InitializeItemStockOptions(Item item)
        {
            if (item.ItemStockOptions == null)
            {
                item.ItemStockOptions = new List<ItemStockOption>();
            }
        }

        /// <summary>
        /// Initialize target ItemAddonCostCentres
        /// </summary>
        private static void InitializeItemAddonCostCentres(ItemStockOption itemStockOption)
        {
            if (itemStockOption.ItemAddonCostCentres == null)
            {
                itemStockOption.ItemAddonCostCentres = new List<ItemAddonCostCentre>();
            }
        }

        /// <summary>
        /// True if the ItemAddonCostCentre is new
        /// </summary>
        private static bool IsNewItemAddonCostCentre(ItemAddonCostCentre source)
        {
            return source.ProductAddOnId <= 0;
        }

        /// <summary>
        /// Update or add Item Addon Cost Centres
        /// </summary>
        private static void UpdateOrAddItemAddonCostCentres(ItemStockOption source, ItemStockOption target, ItemMapperActions actions)
        {
            foreach (ItemAddonCostCentre sourceLine in source.ItemAddonCostCentres.ToList())
            {
                UpdateOrAddItemAddonCostCentre(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Stock Options
        /// </summary>
        private static void UpdateOrAddItemAddonCostCentre(ItemAddonCostCentre source, ItemStockOption target, ItemMapperActions actions)
        {
            ItemAddonCostCentre targetLine;
            if (IsNewItemAddonCostCentre(source))
            {
                targetLine = actions.CreateItemAddonCostCentre();
                target.ItemAddonCostCentres.Add(targetLine);
            }
            else
            {
                targetLine = target.ItemAddonCostCentres.FirstOrDefault(so => so.ProductAddOnId == source.ProductAddOnId);
            }

            source.UpdateTo(targetLine);
        }

        /// <summary>
        /// Delete Item Addon Cost Centres no longer needed
        /// </summary>
        private static void DeleteItemAddonCostCentres(ItemStockOption source, ItemStockOption target, ItemMapperActions actions)
        {
            List<ItemAddonCostCentre> linesToBeRemoved = target.ItemAddonCostCentres.Where(
                so => !IsNewItemAddonCostCentre(so) && source.ItemAddonCostCentres.All(sourceItemAddonCostCentre => sourceItemAddonCostCentre.ProductAddOnId !=
                    so.ProductAddOnId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.ItemAddonCostCentres.Remove(line);
                actions.DeleteItemAddonCostCentre(line);
            });
        }

        /// <summary>
        /// Update or add Item Stock Options
        /// </summary>
        private static void UpdateOrAddItemStockOptions(Item source, Item target, ItemMapperActions actions)
        {
            foreach (ItemStockOption sourceLine in source.ItemStockOptions.ToList())
            {
                UpdateOrAddItemStockOption(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Stock Options
        /// </summary>
        private static void UpdateOrAddItemStockOption(ItemStockOption source, Item target, ItemMapperActions actions)
        {
            ItemStockOption targetLine;
            if (IsNewItemStockOption(source))
            {
                targetLine = actions.CreateItemStockOption();
                target.ItemStockOptions.Add(targetLine);
            }
            else
            {
                targetLine = target.ItemStockOptions.FirstOrDefault(so => so.ItemStockOptionId == source.ItemStockOptionId);
            }

            source.UpdateTo(targetLine);

            // Set Company Id
            if (targetLine != null)
            {
                targetLine.CompanyId = target.CompanyId;
            }

            // Update Item Addon Cost Centres
            // Initialize addon cost centres
            InitializeItemAddonCostCentres(source);
            InitializeItemAddonCostCentres(targetLine);
            UpdateOrAddItemAddonCostCentres(source, targetLine, actions);
            DeleteItemAddonCostCentres(source, targetLine, actions);
        }

        /// <summary>
        /// Delete RelatedItems no longer needed
        /// </summary>
        private static void DeleteItemStockOptions(Item source, Item target, ItemMapperActions actions)
        {
            List<ItemStockOption> linesToBeRemoved = target.ItemStockOptions.Where(
                so => !IsNewItemStockOption(so) && source.ItemStockOptions.All(sourceItemStockOption => sourceItemStockOption.ItemStockOptionId != so.ItemStockOptionId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.ItemStockOptions.Remove(line);
                actions.DeleteItemStockOption(line);
            });
        }

        /// <summary>
        /// Update Item Stock Options
        /// </summary>
        private static void UpdateItemStockOptions(Item source, Item target, ItemMapperActions actions)
        {
            InitializeItemStockOptions(source);
            InitializeItemStockOptions(target);

            UpdateOrAddItemStockOptions(source, target, actions);
            DeleteItemStockOptions(source, target, actions);
        }

        /// <summary>
        /// True if the ItemPriceMatrix is new
        /// </summary>
        private static bool IsNewItemPriceMatrix(ItemPriceMatrix source)
        {
            return source.PriceMatrixId == 0;
        }

        /// <summary>
        /// Initialize target Item Price Matrices
        /// </summary>
        private static void InitializeItemPriceMatrices(Item item)
        {
            if (item.ItemPriceMatrices == null)
            {
                item.ItemPriceMatrices = new List<ItemPriceMatrix>();
            }
        }

        /// <summary>
        /// Update target Item Price Matrix
        /// </summary>
        private static void UpdateOrAddItemPriceMatrix(ItemPriceMatrix source, Item target, ItemMapperActions actions)
        {
            ItemPriceMatrix targetLine;
            if (IsNewItemPriceMatrix(source))
            {
                targetLine = actions.CreateItemPriceMatrix();
                target.ItemPriceMatrices.Add(targetLine);
            }
            else
            {
                targetLine = target.ItemPriceMatrices.FirstOrDefault(so => so.PriceMatrixId == source.PriceMatrixId);
            }

            source.UpdateTo(targetLine, target);
        }

        /// <summary>
        /// Update or add Item Price Matrices
        /// </summary>
        private static void UpdateOrAddItemPriceMatrices(Item source, Item target, ItemMapperActions actions)
        {
            foreach (ItemPriceMatrix sourceLine in source.ItemPriceMatrices.ToList())
            {
                UpdateOrAddItemPriceMatrix(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update Item Price Matrices
        /// </summary>
        private static void UpdateItemPriceMatrices(Item source, Item target, ItemMapperActions actions)
        {
            InitializeItemPriceMatrices(source);
            InitializeItemPriceMatrices(target);

            UpdateOrAddItemPriceMatrices(source, target, actions);
        }

        /// <summary>
        /// True if the ItemStateTax is new
        /// </summary>
        private static bool IsNewItemStateTax(ItemStateTax sourceItemStateTax)
        {
            return sourceItemStateTax.ItemStateTaxId == 0;
        }

        /// <summary>
        /// Initialize target ItemStateTaxs
        /// </summary>
        private static void InitializeItemStateTaxs(Item item)
        {
            if (item.ItemStateTaxes == null)
            {
                item.ItemStateTaxes = new List<ItemStateTax>();
            }
        }

        /// <summary>
        /// Update or add Item Vdp Prices
        /// </summary>
        private static void UpdateOrAddItemStateTaxs(Item source, Item target, ItemMapperActions actions)
        {
            foreach (ItemStateTax sourceLine in source.ItemStateTaxes.ToList())
            {
                UpdateOrAddItemStateTax(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target StateTaxs 
        /// </summary>
        private static void UpdateOrAddItemStateTax(ItemStateTax sourceItemStateTax, Item target, ItemMapperActions actions)
        {
            ItemStateTax targetLine;
            if (IsNewItemStateTax(sourceItemStateTax))
            {
                targetLine = actions.CreateItemStateTax();
                target.ItemStateTaxes.Add(targetLine);
            }
            else
            {
                targetLine = target.ItemStateTaxes.FirstOrDefault(vdp => vdp.ItemStateTaxId == sourceItemStateTax.ItemStateTaxId);
            }
            sourceItemStateTax.UpdateTo(targetLine);
        }

        /// <summary>
        /// Delete StateTaxs no longer needed
        /// </summary>
        private static void DeleteItemStateTaxs(Item source, Item target, ItemMapperActions actions)
        {
            List<ItemStateTax> linesToBeRemoved = target.ItemStateTaxes.Where(
                vdp => !IsNewItemStateTax(vdp) && source.ItemStateTaxes.All(sourceVdp => sourceVdp.ItemStateTaxId != vdp.ItemStateTaxId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.ItemStateTaxes.Remove(line);
                actions.DeleteItemStateTax(line);
            });
        }

        /// <summary>
        /// Update StateTaxs
        /// </summary>
        private static void UpdateItemStateTaxes(Item source, Item target, ItemMapperActions actions)
        {
            InitializeItemStateTaxs(source);
            InitializeItemStateTaxs(target);

            UpdateOrAddItemStateTaxs(source, target, actions);
            DeleteItemStateTaxs(source, target, actions);
        }

        /// <summary>
        /// Initialize Item Product Detail
        /// </summary>
        private static void InitializeItemProductDetails(Item item)
        {
            if (item.ItemProductDetails == null)
            {
                item.ItemProductDetails = new List<ItemProductDetail>();
            }
        }

        /// <summary>
        /// True if the ItemProductDetail is new
        /// </summary>
        private static bool IsNewItemProductDetail(ItemProductDetail sourceItemProductDetail)
        {
            return sourceItemProductDetail.ItemDetailId == 0;
        }

        /// <summary>
        /// Update Item Product Detail
        /// </summary>
        private static void UpdateOrAddItemProductDetail(ItemProductDetail sourceItemProductDetail, Item target, ItemMapperActions actions)
        {
            ItemProductDetail targetLine;
            if (IsNewItemProductDetail(sourceItemProductDetail))
            {
                targetLine = actions.CreateItemProductDetail();
                target.ItemProductDetails.Add(targetLine);
            }
            else
            {
                targetLine = target.ItemProductDetails.FirstOrDefault(vdp => vdp.ItemDetailId == sourceItemProductDetail.ItemDetailId);
            }

            sourceItemProductDetail.UpdateTo(targetLine);
        }

        /// <summary>
        /// Update Item Product Detail
        /// </summary>
        private static void UpdateItemProductDetail(Item source, Item target, ItemMapperActions actions)
        {
            InitializeItemProductDetails(source);
            InitializeItemProductDetails(target);

            UpdateOrAddItemProductDetail(source.ItemProductDetails.FirstOrDefault() ?? new ItemProductDetail(), target, actions);
        }

        /// <summary>
        /// True if the ProductCategoryItem is new
        /// </summary>
        private static bool IsNewProductCategoryItem(ProductCategoryItemCustom sourceProductCategoryItem)
        {
            return sourceProductCategoryItem.ProductCategoryItemId == 0 && sourceProductCategoryItem.IsSelected.HasValue && sourceProductCategoryItem.IsSelected.Value;
        }

        /// <summary>
        /// True if the ProductCategoryItem is to be deleted
        /// </summary>
        private static bool IsRemovedProductCategoryItem(ProductCategoryItemCustom sourceProductCategoryItem)
        {
            return sourceProductCategoryItem.ProductCategoryItemId > 0 &&
                (!sourceProductCategoryItem.IsSelected.HasValue || !sourceProductCategoryItem.IsSelected.Value);
        }

        /// <summary>
        /// Initialize target ProductCategoryItems
        /// </summary>
        private static void InitializeProductCategoryItems(Item item)
        {
            if (item.ProductCategoryItems == null)
            {
                item.ProductCategoryItems = new List<ProductCategoryItem>();
            }
        }

        /// <summary>
        /// Initialize source ProductCategoryItems custom
        /// </summary>
        private static void InitializeProductCategoryCustomItems(Item item)
        {
            if (item.ProductCategoryCustomItems == null)
            {
                item.ProductCategoryCustomItems = new List<ProductCategoryItemCustom>();
            }
        }

        /// <summary>
        /// Update or add Product Category Item
        /// </summary>
        private static void AddProductCategoryItems(Item source, Item target, ItemMapperActions actions)
        {
            foreach (ProductCategoryItemCustom sourceLine in source.ProductCategoryCustomItems.ToList())
            {
                AddProductCategoryItem(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Add target Product Category Items 
        /// </summary>
        private static void AddProductCategoryItem(ProductCategoryItemCustom sourceProductCategoryItem, Item target, ItemMapperActions actions)
        {
            if (IsNewProductCategoryItem(sourceProductCategoryItem))
            {
                ProductCategoryItem targetLine = actions.CreateProductCategoryItem();
                target.ProductCategoryItems.Add(targetLine);
                sourceProductCategoryItem.UpdateTo(targetLine);
            }
        }

        /// <summary>
        /// Delete Product Category UnSelected
        /// </summary>
        private static void DeleteProductCategoryItems(Item source, Item target, ItemMapperActions actions)
        {
            List<ProductCategoryItemCustom> lines = source.ProductCategoryCustomItems.Where(IsRemovedProductCategoryItem).ToList();
            
            lines.ForEach(line =>
            {
                ProductCategoryItem lineToRemove = target.ProductCategoryItems.FirstOrDefault(
                    lineToBeRemoved => lineToBeRemoved.ProductCategoryItemId == line.ProductCategoryItemId);
                
                if (lineToRemove != null && lineToRemove.ProductCategoryItemId > 0)
                {
                    target.ProductCategoryItems.Remove(lineToRemove);
                    actions.DeleteProductCategoryItem(lineToRemove);
                }
                
            });
        }

        /// <summary>
        /// Update Product Category Items
        /// </summary>
        private static void UpdateProductCategoryItems(Item source, Item target, ItemMapperActions actions)
        {
            InitializeProductCategoryCustomItems(source);
            InitializeProductCategoryItems(target);

            AddProductCategoryItems(source, target, actions);
            DeleteProductCategoryItems(source, target, actions);
        }

        /// <summary>
        /// True if the ItemSection is new
        /// </summary>
        private static bool IsNewItemSection(ItemSection sourceItemSection)
        {
            return sourceItemSection.ItemSectionId <= 0;
        }

        /// <summary>
        /// Initialize target ItemSections
        /// </summary>
        private static void InitializeItemSections(Item item)
        {
            if (item.ItemSections == null)
            {
                item.ItemSections = new List<ItemSection>();
            }
        }

        /// <summary>
        /// Update or add Item Sections
        /// </summary>
        private static void UpdateOrAddItemSections(Item source, Item target, ItemMapperActions actions)
        {
            foreach (ItemSection sourceLine in source.ItemSections.ToList())
            {
                UpdateOrAddItemSection(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Item Sections 
        /// </summary>
        private static void UpdateOrAddItemSection(ItemSection sourceItemSection, Item target, ItemMapperActions actions)
        {
            ItemSection targetLine;
            if (IsNewItemSection(sourceItemSection))
            {
                targetLine = actions.CreateItemSection();
                target.ItemSections.Add(targetLine);
            }
            else
            {
                targetLine = target.ItemSections.FirstOrDefault(vdp => vdp.ItemSectionId == sourceItemSection.ItemSectionId);
            }
            
            sourceItemSection.UpdateTo(targetLine);

            // Update Section Ink Coverages
            UpdateSectionInkCoverages(sourceItemSection, targetLine, actions);
        }

        /// <summary>
        /// Delete sections no longer needed
        /// </summary>
        private static void DeleteItemSections(Item source, Item target, ItemMapperActions actions)
        {
            List<ItemSection> linesToBeRemoved = target.ItemSections.Where(
                vdp => !IsNewItemSection(vdp) && source.ItemSections.All(sourceVdp => sourceVdp.ItemSectionId != vdp.ItemSectionId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.ItemSections.Remove(line);
                actions.DeleteItemSection(line);
            });
        }

        #region Section Ink Coverage

        /// <summary>
        /// True if the Section Ink COverage is new
        /// </summary>
        private static bool IsNewSectionInkCoverage(SectionInkCoverage source)
        {
            return source.Id <= 0;
        }

        /// <summary>
        /// Initialize target Section Ink Coverage
        /// </summary>
        private static void InitializeSectionInkCoverages(ItemSection item)
        {
            if (item.SectionInkCoverages == null)
            {
                item.SectionInkCoverages = new List<SectionInkCoverage>();
            }
        }

        /// <summary>
        /// Update or add Item Section Ink Coverages
        /// </summary>
        private static void UpdateOrAddSectionInkCoverages(ItemSection source, ItemSection target, ItemMapperActions actions)
        {
            foreach (SectionInkCoverage sourceLine in source.SectionInkCoverages.ToList())
            {
                UpdateOrAddSectionInkCoverage(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Item Sections 
        /// </summary>
        private static void UpdateOrAddSectionInkCoverage(SectionInkCoverage sourceSectionInkCoverage, ItemSection target, ItemMapperActions actions)
        {
            SectionInkCoverage targetLine;
            if (IsNewSectionInkCoverage(sourceSectionInkCoverage))
            {
                targetLine = actions.CreateSectionInkCoverage();
                target.SectionInkCoverages.Add(targetLine);
            }
            else
            {
                targetLine = target.SectionInkCoverages.FirstOrDefault(vdp => vdp.Id == sourceSectionInkCoverage.Id);
            }

            sourceSectionInkCoverage.UpdateTo(targetLine);
        }

        /// <summary>
        /// Delete section ink coverage no longer needed
        /// </summary>
        private static void DeleteSectionInkCoverages(ItemSection source, ItemSection target, ItemMapperActions actions)
        {
            List<SectionInkCoverage> linesToBeRemoved = target.SectionInkCoverages.Where(
                vdp => !IsNewSectionInkCoverage(vdp) && source.SectionInkCoverages.All(sourceVdp => sourceVdp.Id != vdp.Id))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.SectionInkCoverages.Remove(line);
                actions.DeleteSectionInkCoverage(line);
            });
        }

        /// <summary>
        /// Update Section Cost Centres
        /// </summary>
        private static void UpdateSectionInkCoverages(ItemSection source, ItemSection target, ItemMapperActions actions)
        {
            InitializeSectionInkCoverages(source);
            InitializeSectionInkCoverages(target);

            UpdateOrAddSectionInkCoverages(source, target, actions);

            // Delete
            DeleteSectionInkCoverages(source, target, actions);
        }

        #endregion Section Ink Coverage


        /// <summary>
        /// Update Item Sections
        /// </summary>
        private static void UpdateItemSections(Item source, Item target, ItemMapperActions actions)
        {
            InitializeItemSections(source);
            InitializeItemSections(target);
            
            // Set Defaults to Non-Print Section
            bool isPrintItem = true;
            if (target.ItemProductDetails != null && target.ItemProductDetails.Any() && !target.ItemSections.Any())
            {
                ItemProductDetail itemProductDetail = target.ItemProductDetails.First();
                if (itemProductDetail.isPrintItem.HasValue && !itemProductDetail.isPrintItem.Value)
                {
                    ItemSection targetItemSection = actions.CreateItemSection();
                    targetItemSection.ItemId = target.ItemId;
                    targetItemSection.SectionName = "Section 1";
                    targetItemSection.SectionNo = 1;
                    target.ItemSections.Add(targetItemSection);
                    actions.SetDefaultsForItemSection(targetItemSection);
                    isPrintItem = false;
                }
            }

            // Add Item Sections if Print Item
            if (isPrintItem)
            {
                UpdateOrAddItemSections(source, target, actions);    
            }
            
            // Delete
            DeleteItemSections(source, target, actions);
        }

        /// <summary>
        /// True if the ItemImage is new
        /// </summary>
        private static bool IsNewItemImage(ItemImage sourceItemImage)
        {
            return sourceItemImage.ProductImageId == 0;
        }

        /// <summary>
        /// Initialize target ItemImages
        /// </summary>
        private static void InitializeItemImages(Item item)
        {
            if (item.ItemImages == null)
            {
                item.ItemImages = new List<ItemImage>();
            }
        }

        /// <summary>
        /// Update or add Item Vdp Prices
        /// </summary>
        private static void UpdateOrAddItemImages(Item source, Item target, ItemMapperActions actions)
        {
            foreach (ItemImage sourceLine in source.ItemImages.ToList())
            {
                UpdateOrAddItemImage(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target Images 
        /// </summary>
        private static void UpdateOrAddItemImage(ItemImage sourceItemImage, Item target, ItemMapperActions actions)
        {
            ItemImage targetLine;
            if (IsNewItemImage(sourceItemImage))
            {
                targetLine = actions.CreateItemImage();
                target.ItemImages.Add(targetLine);
            }
            else
            {
                targetLine = target.ItemImages.FirstOrDefault(image => image.ProductImageId == sourceItemImage.ProductImageId);
            }
            sourceItemImage.UpdateTo(targetLine);
        }

        /// <summary>
        /// Delete Images no longer needed
        /// </summary>
        private static void DeleteItemImages(Item source, Item target, ItemMapperActions actions)
        {
            List<ItemImage> linesToBeRemoved = target.ItemImages.Where(
                ii => !IsNewItemImage(ii) && source.ItemImages.All(image => image.ProductImageId != ii.ProductImageId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.ItemImages.Remove(line);
                actions.DeleteItemImage(line);
            });
        }

        /// <summary>
        /// Update Images
        /// </summary>
        private static void UpdateItemImages(Item source, Item target, ItemMapperActions actions)
        {
            InitializeItemImages(source);
            InitializeItemImages(target);

            UpdateOrAddItemImages(source, target, actions);
            DeleteItemImages(source, target, actions);
        }

        /// <summary>
        /// True if the ProductMarketBriefQuestion is new
        /// </summary>
        private static bool IsNewProductMarketBriefQuestion(ProductMarketBriefQuestion sourceProductMarketBriefQuestion)
        {
            return sourceProductMarketBriefQuestion.MarketBriefQuestionId <= 0;
        }

        /// <summary>
        /// Initialize target ProductMarketBriefQuestions
        /// </summary>
        private static void InitializeProductMarketBriefQuestions(Item item)
        {
            if (item.ProductMarketBriefQuestions == null)
            {
                item.ProductMarketBriefQuestions = new List<ProductMarketBriefQuestion>();
            }
        }

        /// <summary>
        /// Update lines
        /// </summary>
        private static void UpdateProductMarketBriefQuestions(Item source, Item target, ItemMapperActions actions)
        {
            InitializeProductMarketBriefQuestions(source);
            InitializeProductMarketBriefQuestions(target);

            UpdateOrAddProductMarketBriefQuestions(source, target, actions);
            DeleteProductMarketBriefQuestions(source, target, actions);
        }

        /// <summary>
        /// Delete lines no longer needed
        /// </summary>
        private static void DeleteProductMarketBriefQuestions(Item source, Item target, ItemMapperActions actions)
        {
            List<ProductMarketBriefQuestion> linesToBeRemoved = target.ProductMarketBriefQuestions.Where(
                vdp => !IsNewProductMarketBriefQuestion(vdp) && source.ProductMarketBriefQuestions.All(sourceVdp => sourceVdp.MarketBriefQuestionId 
                    != vdp.MarketBriefQuestionId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.ProductMarketBriefQuestions.Remove(line);
                actions.DeleteProductMarketBriefQuestion(line);
            });
        }

        /// <summary>
        /// Update or add Item Vdp Prices
        /// </summary>
        private static void UpdateOrAddProductMarketBriefQuestions(Item source, Item target, ItemMapperActions actions)
        {
            foreach (ProductMarketBriefQuestion sourceLine in source.ProductMarketBriefQuestions.ToList())
            {
                UpdateOrAddProductMarketBriefQuestion(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target lines 
        /// </summary>
        private static void UpdateOrAddProductMarketBriefQuestion(ProductMarketBriefQuestion sourceProductMarketBriefQuestion, Item target, ItemMapperActions actions)
        {
            ProductMarketBriefQuestion targetLine;
            if (IsNewProductMarketBriefQuestion(sourceProductMarketBriefQuestion))
            {
                targetLine = actions.CreateProductMarketBriefQuestion();
                target.ProductMarketBriefQuestions.Add(targetLine);
            }
            else
            {
                targetLine = target.ProductMarketBriefQuestions.FirstOrDefault(vdp => vdp.MarketBriefQuestionId == sourceProductMarketBriefQuestion.MarketBriefQuestionId);
            }

            sourceProductMarketBriefQuestion.UpdateTo(targetLine);

            // Update Market Brief Answers
            UpdateProductMarketBriefAnswers(sourceProductMarketBriefQuestion, targetLine, actions);
        }

        /// <summary>
        /// True if the ProductMarketBriefAnswer is new
        /// </summary>
        private static bool IsNewProductMarketBriefAnswer(ProductMarketBriefAnswer sourceProductMarketBriefAnswer)
        {
            return sourceProductMarketBriefAnswer.MarketBriefAnswerId <= 0;
        }

        /// <summary>
        /// Initialize target ProductMarketBriefAnswers
        /// </summary>
        private static void InitializeProductMarketBriefAnswers(ProductMarketBriefQuestion item)
        {
            if (item.ProductMarketBriefAnswers == null)
            {
                item.ProductMarketBriefAnswers = new List<ProductMarketBriefAnswer>();
            }
        }

        /// <summary>
        /// Update lines
        /// </summary>
        private static void UpdateProductMarketBriefAnswers(ProductMarketBriefQuestion source, ProductMarketBriefQuestion target, ItemMapperActions actions)
        {
            InitializeProductMarketBriefAnswers(source);
            InitializeProductMarketBriefAnswers(target);

            UpdateOrAddProductMarketBriefAnswers(source, target, actions);
            DeleteProductMarketBriefAnswers(source, target, actions);
        }

        /// <summary>
        /// Delete lines no longer needed
        /// </summary>
        private static void DeleteProductMarketBriefAnswers(ProductMarketBriefQuestion source, ProductMarketBriefQuestion target, ItemMapperActions actions)
        {
            List<ProductMarketBriefAnswer> linesToBeRemoved = target.ProductMarketBriefAnswers.Where(
                vdp => !IsNewProductMarketBriefAnswer(vdp) && source.ProductMarketBriefAnswers.All(sourceVdp => sourceVdp.MarketBriefAnswerId
                    != vdp.MarketBriefAnswerId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.ProductMarketBriefAnswers.Remove(line);
                actions.DeleteProductMarketBriefAnswer(line);
            });
        }

        /// <summary>
        /// Update or add Item Vdp Prices
        /// </summary>
        private static void UpdateOrAddProductMarketBriefAnswers(ProductMarketBriefQuestion source, ProductMarketBriefQuestion target, ItemMapperActions actions)
        {
            foreach (ProductMarketBriefAnswer sourceLine in source.ProductMarketBriefAnswers.ToList())
            {
                UpdateOrAddProductMarketBriefAnswer(sourceLine, target, actions);
            }
        }

        /// <summary>
        /// Update target lines 
        /// </summary>
        private static void UpdateOrAddProductMarketBriefAnswer(ProductMarketBriefAnswer sourceProductMarketBriefAnswer, ProductMarketBriefQuestion target, 
            ItemMapperActions actions)
        {
            ProductMarketBriefAnswer targetLine;
            if (IsNewProductMarketBriefAnswer(sourceProductMarketBriefAnswer))
            {
                targetLine = actions.CreateProductMarketBriefAnswer();
                target.ProductMarketBriefAnswers.Add(targetLine);
            }
            else
            {
                targetLine = target.ProductMarketBriefAnswers.FirstOrDefault(vdp => vdp.MarketBriefAnswerId == sourceProductMarketBriefAnswer.MarketBriefAnswerId);
            }
            sourceProductMarketBriefAnswer.UpdateTo(targetLine);
        }

        /// <summary>
        /// Update Images
        /// </summary>
        private static void UpdateImages(Item source, Item target)
        {
            target.ThumbnailImageName = source.ThumbnailImageName;
            target.ThumbnailImage = source.ThumbnailImage;
            target.GridImageSourceName = source.GridImageSourceName;
            target.GridImageBytes = source.GridImageBytes;
            target.ImagePathImageName = source.ImagePathImageName;
            target.ImagePathImage = source.ImagePathImage;
            target.File1Name = source.File1Name;
            target.File1Byte = source.File1Byte;
            target.File2Name = source.File2Name;
            target.File2Byte = source.File2Byte;
            target.File3Name = source.File3Name;
            target.File3Byte = source.File3Byte;
            target.File4Name = source.File4Name;
            target.File4Byte = source.File4Byte;
            target.File5Name = source.File5Name;
            target.File5Byte = source.File5Byte;
            target.File1Deleted = source.File1Deleted;
            target.File2Deleted = source.File2Deleted;
            target.File3Deleted = source.File3Deleted;
            target.File4Deleted = source.File4Deleted;
            target.File5Deleted = source.File5Deleted;
        }

        /// <summary>
        /// Update the header
        /// </summary>
        private static void UpdateHeader(Item source, Item target)
        {
            target.ProductCode = source.ProductCode;
            target.ProductName = source.ProductName;
            target.ProductType = source.ProductType;
            target.IsArchived = source.IsArchived;
            target.IsPublished = source.IsPublished;
            target.IsFeatured = source.IsFeatured;
            target.IsEnabled = source.IsEnabled;
            target.IsVdpProduct = source.IsVdpProduct;
            target.IsStockControl = source.IsStockControl;
            target.SortOrder = source.SortOrder;
            target.ItemLastUpdateDateTime = DateTime.Now;
            target.CompanyId = source.CompanyId;
            target.ProductDisplayOptions = source.ProductDisplayOptions;
            target.IsRealStateProduct = source.IsRealStateProduct;
            target.IsUploadImage = source.IsUploadImage;
            target.IsDigitalDownload = source.IsDigitalDownload;
            target.printCropMarks = source.printCropMarks;
            target.drawWaterMarkTxt = source.drawWaterMarkTxt;
            target.isAddCropMarks = source.isAddCropMarks;
            target.drawBleedArea = source.drawBleedArea;
            target.isMultipagePDF = source.isMultipagePDF;
            target.allowPdfDownload = source.allowPdfDownload;
            target.allowImageDownload = source.allowImageDownload;
            target.ItemLength = source.ItemLength;
            target.ItemWeight = source.ItemWeight;
            target.ItemHeight = source.ItemHeight;
            target.ItemWidth = source.ItemWidth;
            target.SmartFormId = source.SmartFormId;
            target.Title = source.Title;
            target.IsNotifyTemplate = source.IsNotifyTemplate;
            target.DigitalDownloadPrice = source.DigitalDownloadPrice;
            // Update Images
            UpdateImages(source, target);

            // Update Internal Description
            UpdateInternalDescription(source, target);

            // Update Price Table Defaults
            UpdatePriceTableDefaultsHeader(source, target);

            // Update Supplier Prices
            UpdateSupplierPricesHeader(source, target);

            // Update Template Properties
            UpdateTemplatePropertiesHeader(source, target);
        }

        /// <summary>
        /// Update Internal Description
        /// </summary>
        private static void UpdateInternalDescription(Item source, Item target)
        {
            target.XeroAccessCode = source.XeroAccessCode;
            target.WebDescription = source.WebDescription;
            target.ProductSpecification = source.ProductSpecification;
            target.TipsAndHints = source.TipsAndHints;
            target.MetaTitle = source.MetaTitle;
            target.MetaDescription = source.MetaDescription;
            target.MetaKeywords = source.MetaKeywords;

            // Update Job Description
            UpdateJobDescription(source, target);
        }

        /// <summary>
        /// Update Job Description
        /// </summary>
        private static void UpdateJobDescription(Item source, Item target)
        {
            target.JobDescriptionTitle1 = source.JobDescriptionTitle1;
            target.JobDescription1 = source.JobDescription1;
            target.JobDescriptionTitle2 = source.JobDescriptionTitle2;
            target.JobDescription2 = source.JobDescription2;
            target.JobDescriptionTitle3 = source.JobDescriptionTitle3;
            target.JobDescription3 = source.JobDescription3;
            target.JobDescriptionTitle4 = source.JobDescriptionTitle4;
            target.JobDescription4 = source.JobDescription4;
            target.JobDescriptionTitle5 = source.JobDescriptionTitle5;
            target.JobDescription5 = source.JobDescription5;
            target.JobDescriptionTitle6 = source.JobDescriptionTitle6;
            target.JobDescription6 = source.JobDescription6;
            target.JobDescriptionTitle7 = source.JobDescriptionTitle7;
            target.JobDescription7 = source.JobDescription7;
            target.JobDescriptionTitle8 = source.JobDescriptionTitle8;
            target.JobDescription8 = source.JobDescription8;
            target.JobDescriptionTitle9 = source.JobDescriptionTitle9;
            target.JobDescription9 = source.JobDescription9;
            target.JobDescriptionTitle10 = source.JobDescriptionTitle10;
            target.JobDescription10 = source.JobDescription10;
        }

        /// <summary>
        /// Update Price Table Defaults Headers
        /// </summary>
        private static void UpdatePriceTableDefaultsHeader(Item source, Item target)
        {
            target.FlagId = source.FlagId;
            target.IsQtyRanged = source.IsQtyRanged;
            target.PackagingWeight = source.PackagingWeight;
            target.DefaultItemTax = source.DefaultItemTax;
        }

        /// <summary>
        /// Update Supplier Prices Headers
        /// </summary>
        private static void UpdateSupplierPricesHeader(Item source, Item target)
        {
            target.EstimateProductionTime = source.EstimateProductionTime;
            target.SupplierId = source.SupplierId;
            target.SupplierId2 = source.SupplierId2;
        }

        /// <summary>
        /// Update Template Properties Header
        /// </summary>
        private static void UpdateTemplatePropertiesHeader(Item source, Item target)
        {
            // If Template Type changed to Custom
            if (target.TemplateType != 1 && source.TemplateType == 1)
            {
                target.HasTemplateChangedToCustom = true;
            }
            target.TemplateType = source.TemplateType;
            target.IsCmyk = source.IsCmyk;
            target.IsTemplateDesignMode = source.IsTemplateDesignMode;
            target.ZoomFactor = source.ZoomFactor;
            target.Scalar = source.Scalar;
            target.DesignerCategoryId = source.DesignerCategoryId;
        }

        #endregion

        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this Item source, Item target,
            ItemMapperActions actions)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            if (actions == null)
            {
                throw new ArgumentNullException("actions");
            }
            if (actions.CreateItemVdpPrice == null)
            {
                throw new ArgumentException(LanguageResources.ItemMapper_CreateItemVdpPriceMustBeSpecified, "actions");
            }
            if (actions.DeleteItemVdpPrice == null)
            {
                throw new ArgumentException(LanguageResources.ItemMapper_DeleteItemVdpPriceMustBeSpecified, "actions");
            }

            UpdateHeader(source, target);
            UpdateItemVdpPrices(source, target, actions);
            UpdateItemVideos(source, target, actions);
            UpdateItemRelatedItems(source, target, actions);
            UpdateTemplate(source, target, actions);
            UpdateItemStockOptions(source, target, actions);
            UpdateItemPriceMatrices(source, target, actions);
            UpdateItemStateTaxes(source, target, actions);
            UpdateItemProductDetail(source, target, actions);
            UpdateProductCategoryItems(source, target, actions);
            UpdateItemSections(source, target, actions);
            UpdateItemImages(source, target, actions);
            UpdateProductMarketBriefQuestions(source, target, actions);
        }

        #endregion
    }
}
