using System;
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Order Item Webapi Model
    /// </summary>
    public sealed class OrderItem
    {
        #region Public

        /// <summary>
        /// Item Id
        /// </summary>
        public long ItemId { get; set; }

        /// <summary>
        /// Item Code
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Estimate Id
        /// </summary>
        public long? EstimateId { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Tax1
        /// </summary>
        public int? Tax1 { get; set; }

        /// <summary>
        /// Status Id
        /// </summary>
        public short? StatusId { get; set; }

        /// <summary>
        /// Qty1
        /// </summary>
        public int? Qty1 { get; set; }

        /// <summary>
        /// Qty2
        /// </summary>
        public int? Qty2 { get; set; }

        /// <summary>
        /// Qty1 Cost Center Profit
        /// </summary>
        public double? Qty1CostCentreProfit { get; set; }

        /// <summary>
        /// Qty1 Base Charge
        /// </summary>
        public double? Qty1BaseCharge1 { get; set; }

        /// <summary>
        /// Qty1 Marup Id 1
        /// </summary>
        public int? Qty1MarkUpId1 { get; set; }

        /// <summary>
        /// Qty1 Marup Percentage Value
        /// </summary>
        public double? Qty1MarkUpPercentageValue { get; set; }


        /// <summary>
        /// Qty1 Marup1 Value
        /// </summary>
        public double? Qty1MarkUp1Value { get; set; }

        /// <summary>
        /// Qty1 Marup Id 1
        /// </summary>
        public double? Qty1NetTotal { get; set; }

        public double? Qty1Tax1Value { get; set; }
        public double? Qty1GrossTotal { get; set; }
        public string Qty1Title { get; set; }
        public string JobDescriptionTitle1 { get; set; }
        public string JobDescriptionTitle2 { get; set; }
        public string JobDescriptionTitle3 { get; set; }
        public string JobDescriptionTitle4 { get; set; }
        public string JobDescriptionTitle5 { get; set; }
        public string JobDescriptionTitle6 { get; set; }
        public string JobDescriptionTitle7 { get; set; }
        public string JobDescription1 { get; set; }
        public string JobDescription2 { get; set; }
        public string JobDescription3 { get; set; }
        public string JobDescription4 { get; set; }
        public string JobDescription5 { get; set; }
        public string JobDescription6 { get; set; }
        public string JobDescription7 { get; set; }
        public string EstimateDescription { get; set; }
        public string JobDescription { get; set; }
        public string JobCode { get; set; }
        public Guid? JobManagerId { get; set; }
        public DateTime? JobEstimatedStartDateTime { get; set; }
        public DateTime? JobEstimatedCompletionDateTime { get; set; }
        public DateTime? JobCreationDateTime { get; set; }
        public Guid? JobProgressedBy { get; set; }
        public int? JobSelectedQty { get; set; }
        public int? JobStatusId { get; set; }
        public bool? IsJobCardPrinted { get; set; }
        public short? ItemType { get; set; }
        public bool? IsJobCardCreated { get; set; }
        public bool? IsAttachmentAdded { get; set; }
        public string ItemNotes { get; set; }
        public DateTime? JobActualStartDateTime { get; set; }
        public DateTime? JobActualCompletionDateTime { get; set; }
        public bool? IsJobCostingDone { get; set; }
        public string ProductName { get; set; }
        public int? ProductType { get; set; }
        public string ProductCode { get; set; }
        public long? CompanyId { get; set; }
        public int? NominalCodeId { get; set; }
        public double? DefaultItemTax { get; set; }
        public bool? IsQtyRanged { get; set; }
        public string Status { get; set; }
        /// <summary>
        /// Qty2 Marup Id 2
        /// </summary>
        public int? Qty2MarkUpId2 { get; set; }
        /// <summary>
        /// Qty3Marup Id 3
        /// </summary>
        public int? Qty3MarkUpId3 { get; set; }
        public double? Qty2NetTotal { get; set; }
        public double? Qty3NetTotal { get; set; }
        public double? Qty2Tax1Value { get; set; }
        public double? Qty3Tax1Value { get; set; }
        public double? Qty2GrossTotal { get; set; }
        public double? Qty3GrossTotal { get; set; }
        public DateTime? ItemCreationDateTime { get; set; }
        public string InvoiceDescription { get; set; }
        public Guid? JobCardPrintedBy { get; set; }
        public int? RefItemId { get; set; }
        public IEnumerable<string> ProductCategories { get; set; }
        public IEnumerable<ItemSection> ItemSections { get; set; }
        public  ICollection<ItemAttachment> ItemAttachments { get; set; }
        #endregion
    }
}