using System;
using MPC.Models.DomainModels;

namespace MPC.Models.ModelMappers
{
    /// <summary>
    ///  Order Mapper actions
    /// </summary>
    public sealed class OrderMapperActions
    {
        #region Public

        /// <summary>
        /// Action to create a Item Section
        /// </summary>
        public Func<ItemSection> CreateItemSection { get; set; }

        /// <summary>
        /// Action to Delete Item Section
        /// </summary>
        public Action<ItemSection> DeleteItemSection { get; set; }

        /// <summary>
        /// Action to create a Section Cost Center
        /// </summary>
        public Func<SectionCostcentre> CreateSectionCostCentre { get; set; }

        /// <summary>
        /// Action to delete a Section Cost Center
        /// </summary>
        public Action<SectionCostcentre> DeleteSectionCostCenter { get; set; }

        /// <summary>
        /// Action to create a Section Cost Center Detail
        /// </summary>
        public Func<SectionCostCentreDetail> CreateSectionCostCenterDetail { get; set; }

        /// <summary>
        /// Action to delete a Section Cost Center Detail
        /// </summary>
        public Action<SectionCostCentreDetail> DeleteSectionCostCenterDetail { get; set; }

        /// <summary>
        /// Action to create a Section Ink Coverage
        /// </summary>
        public Func<SectionInkCoverage> CreateSectionInkCoverage { get; set; }

        /// <summary>
        /// Action to delete a Section Ink Coverage
        /// </summary>
        public Action<SectionInkCoverage> DeleteSectionInkCoverage { get; set; }

        /// <summary>
        /// Action to create a Item Attachment
        /// </summary>
        public Func<ItemAttachment> CreateItemAttachment { get; set; }

        /// <summary>
        /// Action to delete Item Attachment
        /// </summary>
        public Action<ItemAttachment> DeleteItemAttachment { get; set; }

        /// <summary>
        /// Action to create an Item
        /// </summary>
        public Func<Item> CreateItem { get; set; }

        /// <summary>
        /// Action to Delete Item
        /// </summary>
        public Action<Item> DeleteItem { get; set; }

        /// <summary>
        /// Action to create a Pre Payment
        /// </summary>
        public Func<PrePayment> CreatePrePayment { get; set; }

        /// <summary>
        /// Action to delete a Pre Payment
        /// </summary>
        public Action<PrePayment> DeletePrePayment { get; set; }

        /// <summary>
        /// Action to create Delivery Schedule
        /// </summary>
        public Func<ShippingInformation> CreateShippingInformation { get; set; }

        /// <summary>
        /// Action to delete Delivery Schedule
        /// </summary>
        public Action<ShippingInformation> DeleteShippingInformation { get; set; }

        /// <summary>
        /// Action to create an Order
        /// </summary>
        public Func<Estimate> CreateNewOrder { get; set; }

        /// <summary>
        /// Action to Get Next Job Code
        /// </summary>
        public Func<string> GetNextJobCode { get; set; }
        
        #endregion
    }
}
