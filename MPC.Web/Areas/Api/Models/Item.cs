using System;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Webapi Model
    /// </summary>
    public sealed class Item
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
        /// Invoice Id
        /// </summary>
        public long? InvoiceId { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Tax1
        /// </summary>
        public int? Tax1 { get; set; }

        /// <summary>
        /// Tax2
        /// </summary>
        public int? Tax2 { get; set; }

        /// <summary>
        /// Tax3
        /// </summary>
        public int? Tax3 { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public int? CreatedBy { get; set; }

        /// <summary>
        /// Status Id
        /// </summary>
        public short? StatusId { get; set; }

        /// <summary>
        /// Item Creation Date Time
        /// </summary>
        public DateTime? ItemCreationDateTime { get; set; }

        /// <summary>
        /// Item Last Update Date Time
        /// </summary>
        public DateTime? ItemLastUpdateDateTime { get; set; }

        /// <summary>
        /// Is Multiple Qty
        /// </summary>
        public short? IsMultipleQty { get; set; }

        /// <summary>
        /// Run On Qty
        /// </summary>
        public int? RunOnQty { get; set; }

        /// <summary>
        /// Run on Cost Centre Profit
        /// </summary>
        public double? RunonCostCentreProfit { get; set; }

        /// <summary>
        /// Run on Base Charge
        /// </summary>
        public double? RunonBaseCharge { get; set; }

        /// <summary>
        /// Run On Markup Id
        /// </summary>
        public int? RunOnMarkUpId { get; set; }

        /// <summary>
        /// Run on Percentage Value
        /// </summary>
        public int? RunonPercentageValue { get; set; }

        /// <summary>
        /// Run on Marup Value
        /// </summary>
        public double? RunOnMarkUpValue { get; set; }

        /// <summary>
        /// Run on Net Total
        /// </summary>
        public double? RunOnNetTotal { get; set; }

        /// <summary>
        /// Qty1
        /// </summary>
        public int? Qty1 { get; set; }

        /// <summary>
        /// Qty2
        /// </summary>
        public int? Qty2 { get; set; }

        /// <summary>
        /// Qty3
        /// </summary>
        public int? Qty3 { get; set; }

        /// <summary>
        /// Qty1 Cost Center Profit
        /// </summary>
        public double? Qty1CostCentreProfit { get; set; }

        /// <summary>
        /// Qty2 Cost Centre Profit
        /// </summary>
        public double? Qty2CostCentreProfit { get; set; }

        /// <summary>
        /// Qty3 Cost Centre Profit
        /// </summary>
        public double? Qty3CostCentreProfit { get; set; }

        /// <summary>
        /// Qty1 Base Charge
        /// </summary>
        public double? Qty1BaseCharge1 { get; set; }

        /// <summary>
        /// Qty2 Base Charge2
        /// </summary>
        public double? Qty2BaseCharge2 { get; set; }

        /// <summary>
        /// Qty3 Base Charge3
        /// </summary>
        public double? Qty3BaseCharge3 { get; set; }

        /// <summary>
        /// Qty1 Marup Id 1
        /// </summary>
        public int? Qty1MarkUpId1 { get; set; }

        /// <summary>
        /// Qty2 Marup Id 2
        /// </summary>
        public int? Qty2MarkUpId2 { get; set; }

        /// <summary>
        /// Qty3Marup Id 3
        /// </summary>
        public int? Qty3MarkUpId3 { get; set; }

        /// <summary>
        /// Qty1 Marup Percentage Value
        /// </summary>
        public double? Qty1MarkUpPercentageValue { get; set; }

        /// <summary>
        /// Qty2 Marup Percentage Value
        /// </summary>
        public double? Qty2MarkUpPercentageValue { get; set; }

        /// <summary>
        /// Qty3 Percentage Value
        /// </summary>
        public double? Qty3MarkUpPercentageValue { get; set; }

        /// <summary>
        /// Qty1 Marup1 Value
        /// </summary>
        public double? Qty1MarkUp1Value { get; set; }

        /// <summary>
        /// Qty2 Marup2 Value
        /// </summary>
        public double? Qty2MarkUp2Value { get; set; }

        /// <summary>
        /// Qty3 Marup3 Value
        /// </summary>
        public double? Qty3MarkUp3Value { get; set; }

        /// <summary>
        /// Qty1 Marup Id 1
        /// </summary>
        public double? Qty1NetTotal { get; set; }

        /// <summary>
        /// Qty1 Marup Id 1
        /// </summary>
        public double? Qty2NetTotal { get; set; }

        /// <summary>
        /// Qty1 Marup Id 1
        /// </summary>
        public double? Qty3NetTotal { get; set; }
        public double? Qty1Tax1Value { get; set; }
        public double? Qty1Tax2Value { get; set; }
        public double? Qty1Tax3Value { get; set; }
        public double? Qty1GrossTotal { get; set; }
        public double? Qty2Tax1Value { get; set; }
        public double? Qty2Tax2Value { get; set; }
        public double? Qty2Tax3Value { get; set; }
        public double? Qty2GrossTotal { get; set; }
        public double? Qty3Tax1Value { get; set; }
        public double? Qty3Tax2Value { get; set; }
        public double? Qty3Tax3Value { get; set; }
        public double? Qty3GrossTotal { get; set; }
        public short? IsDescriptionLocked { get; set; }
        public string Qty1Title { get; set; }
        public string Qty2Title { get; set; }
        public string Qty3Title { get; set; }
        public string RunonTitle { get; set; }
        public string AdditionalInformation { get; set; }
        public string Qty2Description { get; set; }
        public string Qty3Description { get; set; }
        public string RunonDescription { get; set; }
        public string EstimateDescriptionTitle1 { get; set; }
        public string EstimateDescriptionTitle2 { get; set; }
        public string EstimateDescriptionTitle3 { get; set; }
        public string EstimateDescriptionTitle4 { get; set; }
        public string EstimateDescriptionTitle5 { get; set; }
        public string EstimateDescriptionTitle6 { get; set; }
        public string EstimateDescriptionTitle7 { get; set; }
        public string EstimateDescriptionTitle8 { get; set; }
        public string EstimateDescriptionTitle9 { get; set; }
        public string EstimateDescriptionTitle10 { get; set; }
        public string EstimateDescription1 { get; set; }
        public string EstimateDescription2 { get; set; }
        public string EstimateDescription3 { get; set; }
        public string EstimateDescription4 { get; set; }
        public string EstimateDescription5 { get; set; }
        public string EstimateDescription6 { get; set; }
        public string EstimateDescription7 { get; set; }
        public string EstimateDescription8 { get; set; }
        public string EstimateDescription9 { get; set; }
        public string EstimateDescription10 { get; set; }
        public string JobDescriptionTitle1 { get; set; }
        public string JobDescriptionTitle2 { get; set; }
        public string JobDescriptionTitle3 { get; set; }
        public string JobDescriptionTitle4 { get; set; }
        public string JobDescriptionTitle5 { get; set; }
        public string JobDescriptionTitle6 { get; set; }
        public string JobDescriptionTitle7 { get; set; }
        public string JobDescriptionTitle8 { get; set; }
        public string JobDescriptionTitle9 { get; set; }
        public string JobDescriptionTitle10 { get; set; }
        public string JobDescription1 { get; set; }
        public string JobDescription2 { get; set; }
        public string JobDescription3 { get; set; }
        public string JobDescription4 { get; set; }
        public string JobDescription5 { get; set; }
        public string JobDescription6 { get; set; }
        public string JobDescription7 { get; set; }
        public string JobDescription8 { get; set; }
        public string JobDescription9 { get; set; }
        public string JobDescription10 { get; set; }
        public short? IsParagraphDescription { get; set; }
        public string EstimateDescription { get; set; }
        public string JobDescription { get; set; }
        public string InvoiceDescription { get; set; }
        public string JobCode { get; set; }
        public int? JobManagerId { get; set; }
        public DateTime? JobEstimatedStartDateTime { get; set; }
        public DateTime? JobEstimatedCompletionDateTime { get; set; }
        public DateTime? JobCreationDateTime { get; set; }
        public int? JobProgressedBy { get; set; }
        public int? JobSelectedQty { get; set; }
        public int? JobStatusId { get; set; }
        public bool? IsJobCardPrinted { get; set; }
        public short? IsItemLibraray { get; set; }
        public int? ItemLibrarayGroupId { get; set; }
        public int? PayInFullInvoiceId { get; set; }
        public short? IsGroupItem { get; set; }
        public short? ItemType { get; set; }
        public short? IsIncludedInPipeLine { get; set; }
        public short? IsRunOnQty { get; set; }
        public short? CanCopyToEstimate { get; set; }
        public int? FlagId { get; set; }
        public byte[] CostCenterDescriptions { get; set; }
        public bool? IsRead { get; set; }
        public byte? IsScheduled { get; set; }
        public bool? IsPaperStatusChanged { get; set; }
        public bool? IsJobCardCreated { get; set; }
        public bool? IsAttachmentAdded { get; set; }
        public bool? IsItemValueChanged { get; set; }
        public int? DepartmentId { get; set; }
        public string ItemNotes { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? JobActualStartDateTime { get; set; }
        public DateTime? JobActualCompletionDateTime { get; set; }
        public bool? IsJobCostingDone { get; set; }
        public string ProductName { get; set; }
        public long? ProductCategoryId { get; set; }
        public string ImagePath { get; set; }
        public string ThumbnailPath { get; set; }
        public string ProductSpecification { get; set; }
        public string CompleteSpecification { get; set; }
        public string DesignGuideLines { get; set; }
        public string ProductCode { get; set; }
        public bool? IsPublished { get; set; }
        public long? CompanyId { get; set; }
        public int? PriceDiscountPercentage { get; set; }
        public bool? IsEnabled { get; set; }
        public bool? IsSpecialItem { get; set; }
        public string IconPath { get; set; }
        public bool? IsPopular { get; set; }
        public bool? IsFeatured { get; set; }
        public bool? IsPromotional { get; set; }
        public string TipsAndHints { get; set; }
        public string FactSheetFileName { get; set; }
        public bool? IsArchived { get; set; }
        public int? NominalCodeId { get; set; }
        public int? RefItemId { get; set; }
        public int? TemplateId { get; set; }
        public string WebDescription { get; set; }
        public int? ItemTypeId { get; set; }
        public bool? IsOrderedItem { get; set; }
        public int? JobCardPrintedBy { get; set; }
        public DateTime? JobCardLastPrintedDate { get; set; }
        public int? EstimateProductionTime { get; set; }
        public int? SortOrder { get; set; }
        public int? IsFinishedGoods { get; set; }
        public string LayoutGridContent { get; set; }
        public string HowToVideoContent { get; set; }
        public string File1 { get; set; }
        public string File2 { get; set; }
        public string File3 { get; set; }
        public string File4 { get; set; }
        public string File5 { get; set; }
        public string GridImage { get; set; }
        public bool? IsQtyRanged { get; set; }
        public double? CostCentreProfitBroker { get; set; }
        public double? BaseChargeBroker { get; set; }
        public double? MarkUpValueBroker { get; set; }
        public double? NetTotalBroker { get; set; }
        public double? TaxValueBroker { get; set; }
        public double? GrossTotalBroker { get; set; }
        public bool? IsCmyk { get; set; }
        public int? SupplierId { get; set; }
        public bool? IsStockControl { get; set; }
        public bool? IsUploadImage { get; set; }
        public bool? IsMarketingBrief { get; set; }
        public int? SupplierId2 { get; set; }
        public int? FinishedGoodId { get; set; }
        public short? IsFinishedGoodPrivate { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public bool? IsTemplateDesignMode { get; set; }
        public string XeroAccessCode { get; set; }
        public double? DefaultItemTax { get; set; }
        public long? OrganisationId { get; set; }
        public double? PackagingWeight { get; set; }
        public bool? IsVdpProduct { get; set; }
        public IEnumerable<ItemVdpPrice> ItemVdpPrices { get; set; }
        #endregion
    }
}