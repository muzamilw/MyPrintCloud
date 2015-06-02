using System;
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ModelMappers
{
    /// <summary>
    ///  Item Mapper actions
    /// </summary>
    public sealed class ItemMapperActions
    {
        #region Public

        /// <summary>
        /// Action to create a Item Vdp Price
        /// </summary>
        public Func<ItemVdpPrice> CreateItemVdpPrice { get; set; }

        /// <summary>
        /// Action to delete a Item Vdp Price
        /// </summary>
        public Action<ItemVdpPrice> DeleteItemVdpPrice { get; set; }

        /// <summary>
        /// Action to create a Item Video
        /// </summary>
        public Func<ItemVideo> CreateItemVideo { get; set; }

        /// <summary>
        /// Action to delete a Item Video
        /// </summary>
        public Action<ItemVideo> DeleteItemVideo { get; set; }

        /// <summary>
        /// Action to create a Item Related Item
        /// </summary>
        public Func<ItemRelatedItem> CreateItemRelatedItem { get; set; }

        /// <summary>
        /// Action to delete a Item Related Item
        /// </summary>
        public Action<ItemRelatedItem> DeleteItemRelatedItem { get; set; }

        /// <summary>
        /// Action to create a Template Page
        /// </summary>
        public Func<TemplatePage> CreateTemplatePage { get; set; }

        /// <summary>
        /// Action to delete a Template Page
        /// </summary>
        public Action<TemplatePage> DeleteTemplatePage { get; set; }

        /// <summary>
        /// Action to create a Template
        /// </summary>
        public Func<Template> CreateTemplate { get; set; }

        /// <summary>
        /// Action to create a Item Stock Option
        /// </summary>
        public Func<ItemStockOption> CreateItemStockOption { get; set; }

        /// <summary>
        /// Action to delete a Item Stock Option
        /// </summary>
        public Action<ItemStockOption> DeleteItemStockOption { get; set; }

        /// <summary>
        /// Action to create a Item Addon Cost Centre
        /// </summary>
        public Func<ItemAddonCostCentre> CreateItemAddonCostCentre { get; set; }

        /// <summary>
        /// Action to delete a Item Addon Cost Centre
        /// </summary>
        public Action<ItemAddonCostCentre> DeleteItemAddonCostCentre { get; set; }

        /// <summary>
        /// Action to create a Item Price Matrix
        /// </summary>
        public Func<ItemPriceMatrix> CreateItemPriceMatrix { get; set; }

        /// <summary>
        /// Action to create a Item State Tax
        /// </summary>
        public Func<ItemStateTax> CreateItemStateTax { get; set; }

        /// <summary>
        /// Action to delete a Item State Tax
        /// </summary>
        public Action<ItemStateTax> DeleteItemStateTax { get; set; }

        /// <summary>
        /// Action to create a Item Product Detail
        /// </summary>
        public Func<ItemProductDetail> CreateItemProductDetail { get; set; }

        /// <summary>
        /// Action to create a Product Category Item
        /// </summary>
        public Func<ProductCategoryItem> CreateProductCategoryItem { get; set; }

        /// <summary>
        /// Action to delete a Product Category Item
        /// </summary>
        public Action<ProductCategoryItem> DeleteProductCategoryItem { get; set; }

        /// <summary>
        /// Action to create a Item Section
        /// </summary>
        public Func<ItemSection> CreateItemSection { get; set; }

        /// <summary>
        /// Action to set defaults to Item Section
        /// </summary>
        public Action<ItemSection> SetDefaultsForItemSection { get; set; }

        /// <summary>
        /// Action to delete a Item Section
        /// </summary>
        public Action<ItemSection> DeleteItemSection { get; set; }

        /// <summary>
        /// Action to create a Item Image
        /// </summary>
        public Func<ItemImage> CreateItemImage { get; set; }

        /// <summary>
        /// Action to delete a Item Image
        /// </summary>
        public Action<ItemImage> DeleteItemImage { get; set; }

        /// <summary>
        /// Action to delete a Template Object
        /// </summary>
        public Action<List<TemplatePage>> DeleteTemplateObject { get; set; }

        /// <summary>
        /// Action to create a Product Market Brief Question
        /// </summary>
        public Func<ProductMarketBriefQuestion> CreateProductMarketBriefQuestion { get; set; }

        /// <summary>
        /// Action to delete a Product MarketBrief Question
        /// </summary>
        public Action<ProductMarketBriefQuestion> DeleteProductMarketBriefQuestion { get; set; }

        /// <summary>
        /// Action to create a Product Market Brief Answer
        /// </summary>
        public Func<ProductMarketBriefAnswer> CreateProductMarketBriefAnswer { get; set; }

        /// <summary>
        /// Action to delete a Product MarketBrief Answer
        /// </summary>
        public Action<ProductMarketBriefAnswer> DeleteProductMarketBriefAnswer { get; set; }

        /// <summary>
        /// Action to create a Section Ink Coverage
        /// </summary>
        public Func<SectionInkCoverage> CreateSectionInkCoverage { get; set; }

        /// <summary>
        /// Action to delete a Section Ink Coverage
        /// </summary>
        public Action<SectionInkCoverage> DeleteSectionInkCoverage { get; set; }

        #endregion
    }
}
