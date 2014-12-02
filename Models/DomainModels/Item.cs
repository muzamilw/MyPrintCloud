﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class Item
    {
        public long ItemId { get; set; }
        public string ItemCode { get; set; }
        public long? EstimateId { get; set; }
        public long? InvoiceId { get; set; }
        public string Title { get; set; }
        public int? Tax1 { get; set; }
        public int? Tax2 { get; set; }
        public int? Tax3 { get; set; }
        public int? CreatedBy { get; set; }
        public short? StatusId { get; set; }
        public DateTime? ItemCreationDateTime { get; set; }
        public DateTime? ItemLastUpdateDateTime { get; set; }
        public short? IsMultipleQty { get; set; }
        public int? RunOnQty { get; set; }
        public double? RunonCostCentreProfit { get; set; }
        public double? RunonBaseCharge { get; set; }
        public int? RunOnMarkUpID { get; set; }
        public int? RunonPercentageValue { get; set; }
        public double? RunOnMarkUpValue { get; set; }
        public double? RunOnNetTotal { get; set; }
        public int? Qty1 { get; set; }
        public int? Qty2 { get; set; }
        public int? Qty3 { get; set; }
        public double? Qty1CostCentreProfit { get; set; }
        public double? Qty2CostCentreProfit { get; set; }
        public double? Qty3CostCentreProfit { get; set; }
        public double? Qty1BaseCharge1 { get; set; }
        public double? Qty2BaseCharge2 { get; set; }
        public double? Qty3BaseCharge3 { get; set; }
        public int? Qty1MarkUpID1 { get; set; }
        public int? Qty2MarkUpID2 { get; set; }
        public int? Qty3MarkUpID3 { get; set; }
        public double? Qty1MarkUpPercentageValue { get; set; }
        public double? Qty2MarkUpPercentageValue { get; set; }
        public double? Qty3MarkUpPercentageValue { get; set; }
        public double? Qty1MarkUp1Value { get; set; }
        public double? Qty2MarkUp2Value { get; set; }
        public double? Qty3MarkUp3Value { get; set; }
        public double? Qty1NetTotal { get; set; }
        public double? Qty2NetTotal { get; set; }
        public double? Qty3NetTotal { get; set; }
        public double? Qty1Tax1Value { get; set; }
        public double? Qty1Tax2Value { get; set; }
        public double? Qty1Tax3Value { get; set; }
        public double? Qty1GrossTotal { get; set; }
        public double? Qty2Tax1Value { get; set; }
        public double? Qty2Tax2Value { get; set; }
        public double? Qty2Tax3Value { get; set; }
        public double? Qty2grossTotal { get; set; }
        public double? Qty3Tax1Value { get; set; }
        public double? Qty3Tax2Value { get; set; }
        public double? Qty3Tax3Value { get; set; }
        public double? Qty3GrossTotal { get; set; }
        public short? IsDescriptionLocked { get; set; }
        public string qty1title { get; set; }
        public string qty2title { get; set; }
        public string qty3Title { get; set; }
        public string RunonTitle { get; set; }
        public string AdditionalInformation { get; set; }
        public string qty2Description { get; set; }
        public string qty3Description { get; set; }
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
        public int? jobSelectedQty { get; set; }
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
        public string file1 { get; set; }
        public string file2 { get; set; }
        public string file3 { get; set; }
        public string file4 { get; set; }
        public string file5 { get; set; }
        public string GridImage { get; set; }
        public bool? isQtyRanged { get; set; }
        public double? CostCentreProfitBroker { get; set; }
        public double? BaseChargeBroker { get; set; }
        public double? MarkUpValueBroker { get; set; }
        public double? NetTotalBroker { get; set; }
        public double? TaxValueBroker { get; set; }
        public double? GrossTotalBroker { get; set; }
        public bool? isCMYK { get; set; }
        public int? SupplierId { get; set; }
        public bool? isStockControl { get; set; }
        public bool? isUploadImage { get; set; }
        public bool? isMarketingBrief { get; set; }
        public int? SupplierID2 { get; set; }
        public int? FinishedGoodId { get; set; }
        public short? IsFinishedGoodPrivate { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public bool? isTemplateDesignMode { get; set; }
        public string XeroAccessCode { get; set; }
        public double? DefaultItemTax { get; set; }
        public long? OrganisationId { get; set; }
        public double? PackagingWeight { get; set; }
        public bool? IsVDPProduct { get; set; }
        public virtual Status Status { get; set; }

        public virtual ICollection<ItemAttachment> ItemAttachments { get; set; }
        public virtual ICollection<ItemImage> ItemImages { get; set; }
        public virtual ICollection<ItemPriceMatrix> ItemPriceMatrices { get; set; }
        public virtual ICollection<ItemRelatedItem> ItemRelatedItems { get; set; }
        public virtual ICollection<ItemRelatedItem> ItemRelatedItems1 { get; set; }
        public virtual ICollection<ItemStateTax> ItemStateTaxes { get; set; }
        public virtual ICollection<ItemVDPPrice> ItemVDPPrices { get; set; }
        public virtual ICollection<ItemSection> ItemSections { get; set; }
        public virtual ICollection<ItemStockOption> ItemStockOptions { get; set; }
    }
}