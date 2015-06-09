using System.IO;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;
    using System.Collections.Generic;

    /// <summary>
    /// Item Stock Option Mapper
    /// </summary>
    public static class ItemStockOptionMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static ItemStockOption CreateFrom(this DomainModels.ItemStockOption source)
        {
            // ReSharper disable SuggestUseVarKeywordEvident
            ItemStockOption itemStockOption = new ItemStockOption
            // ReSharper restore SuggestUseVarKeywordEvident
            {
                ItemStockOptionId = source.ItemStockOptionId,
                ItemId = source.ItemId,
                StockLabel = source.StockLabel,
                StockId = source.StockId,
                OptionSequence = source.OptionSequence,
                
                ItemAddOnCostCentres = source.ItemAddonCostCentres != null ? source.ItemAddonCostCentres.Select(addon => addon.CreateFrom()) :
                new List<ItemAddOnCostCentre>()
            };

            if (source.StockItem != null)
            {
                var sourceStockItem = source.StockItem;
                itemStockOption.StockItemName = sourceStockItem.ItemName;
                itemStockOption.StockItemDescription = sourceStockItem.ItemDescription;
                itemStockOption.inStock = sourceStockItem.inStock;
                itemStockOption.Allocated = sourceStockItem.Allocated;
            }

            if (string.IsNullOrEmpty(source.ImageURL))
            {
                return itemStockOption;
            }

            string imageUrl = HttpContext.Current.Server.MapPath("~/" + source.ImageURL);
            if (File.Exists(imageUrl))
            {
                itemStockOption.ImageUrlBytes = File.ReadAllBytes(imageUrl);
            }

            return itemStockOption;
        }

        /// <summary>
        /// Create From WebApi Model
        /// </summary>
        public static DomainModels.ItemStockOption CreateFrom(this ItemStockOption source)
        {
            return new DomainModels.ItemStockOption
            {
                ItemStockOptionId = source.ItemStockOptionId,
                ItemId = source.ItemId,
                StockLabel = source.StockLabel,
                StockId = source.StockId,
                FileSource = source.FileSource,
                OptionSequence = source.OptionSequence,
                FileName = source.FileName,
                ItemAddonCostCentres = source.ItemAddOnCostCentres != null ? source.ItemAddOnCostCentres.Select(addon => addon.CreateFrom()).ToList() :
                new List<DomainModels.ItemAddonCostCentre>()
            };
        }

    }
}