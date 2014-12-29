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
            if (target.Template == null)
            {
                target.Template = actions.CreateTemplate();
            }

            target.Template.PDFTemplateHeight = source.Template.PDFTemplateHeight;
            target.Template.PDFTemplateWidth = source.Template.PDFTemplateWidth;

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

            // Update Internal Description
            UpdateInternalDescription(source, target);

            // Update Price Table Defaults
            UpdatePriceTableDefaultsHeader(source, target);
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
        }

        #endregion
    }
}
