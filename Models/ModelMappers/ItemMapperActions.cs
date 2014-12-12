using System;
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

        #endregion
    }
}
