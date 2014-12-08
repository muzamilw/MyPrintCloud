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
            linesToBeRemoved.ForEach(line => { 
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
        /// Update the header
        /// </summary>
        private static void UpdateHeader(Item source, Item target)
        {
            target.ProductCode = source.ProductCode;
            target.ProductName = source.ProductName;
            target.IsFinishedGoods = source.IsFinishedGoods;
            target.IsArchived = source.IsArchived;
            target.IsPublished = source.IsPublished;
            target.IsFeatured = source.IsFeatured;
            target.IsEnabled = source.IsEnabled;
            target.IsVdpProduct = source.IsVdpProduct;
            target.IsStockControl = source.IsStockControl;
            target.SortOrder = source.SortOrder;
            target.ItemLastUpdateDateTime = DateTime.Now;
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
        }

        #endregion
    }
}
