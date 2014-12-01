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
        public Nullable<long> EstimateId { get; set; }
        public Nullable<long> InvoiceId { get; set; }
        public string Title { get; set; }
        public Nullable<int> Tax1 { get; set; }
        public Nullable<int> Tax2 { get; set; }
        public Nullable<int> Tax3 { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<short> Status { get; set; }
        public Nullable<System.DateTime> ItemCreationDateTime { get; set; }
        public Nullable<System.DateTime> ItemLastUpdateDateTime { get; set; }
        public Nullable<short> IsMultipleQty { get; set; }
        public Nullable<int> RunOnQty { get; set; }
        public Nullable<double> RunonCostCentreProfit { get; set; }
        public Nullable<double> RunonBaseCharge { get; set; }
        public Nullable<int> RunOnMarkUpID { get; set; }
        public Nullable<int> RunonPercentageValue { get; set; }
        public Nullable<double> RunOnMarkUpValue { get; set; }
        public Nullable<double> RunOnNetTotal { get; set; }
        public Nullable<int> Qty1 { get; set; }
        public Nullable<int> Qty2 { get; set; }
        public Nullable<int> Qty3 { get; set; }
        public Nullable<double> Qty1CostCentreProfit { get; set; }
        public Nullable<double> Qty2CostCentreProfit { get; set; }
        public Nullable<double> Qty3CostCentreProfit { get; set; }
        public Nullable<double> Qty1BaseCharge1 { get; set; }
        public Nullable<double> Qty2BaseCharge2 { get; set; }
        public Nullable<double> Qty3BaseCharge3 { get; set; }
        public Nullable<int> Qty1MarkUpID1 { get; set; }
        public Nullable<int> Qty2MarkUpID2 { get; set; }
        public Nullable<int> Qty3MarkUpID3 { get; set; }
        public Nullable<double> Qty1MarkUpPercentageValue { get; set; }
        public Nullable<double> Qty2MarkUpPercentageValue { get; set; }
        public Nullable<double> Qty3MarkUpPercentageValue { get; set; }
        public Nullable<double> Qty1MarkUp1Value { get; set; }
        public Nullable<double> Qty2MarkUp2Value { get; set; }
        public Nullable<double> Qty3MarkUp3Value { get; set; }
        public Nullable<double> Qty1NetTotal { get; set; }
        public Nullable<double> Qty2NetTotal { get; set; }
        public Nullable<double> Qty3NetTotal { get; set; }
        public Nullable<double> Qty1Tax1Value { get; set; }
        public Nullable<double> Qty1Tax2Value { get; set; }
        public Nullable<double> Qty1Tax3Value { get; set; }
        public Nullable<double> Qty1GrossTotal { get; set; }
        public Nullable<double> Qty2Tax1Value { get; set; }
        public Nullable<double> Qty2Tax2Value { get; set; }
        public Nullable<double> Qty2Tax3Value { get; set; }
        public Nullable<double> Qty2grossTotal { get; set; }
        public Nullable<double> Qty3Tax1Value { get; set; }
        public Nullable<double> Qty3Tax2Value { get; set; }
        public Nullable<double> Qty3Tax3Value { get; set; }
        public Nullable<double> Qty3GrossTotal { get; set; }
        public Nullable<short> IsDescriptionLocked { get; set; }
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
        public Nullable<short> IsParagraphDescription { get; set; }
        public string EstimateDescription { get; set; }
        public string JobDescription { get; set; }
        public string InvoiceDescription { get; set; }
        public string JobCode { get; set; }
        public Nullable<int> JobManagerId { get; set; }
        public Nullable<System.DateTime> JobEstimatedStartDateTime { get; set; }
        public Nullable<System.DateTime> JobEstimatedCompletionDateTime { get; set; }
        public Nullable<System.DateTime> JobCreationDateTime { get; set; }
        public Nullable<int> JobProgressedBy { get; set; }
        public Nullable<int> jobSelectedQty { get; set; }
        public Nullable<int> JobStatusId { get; set; }
        public Nullable<bool> IsJobCardPrinted { get; set; }
        public Nullable<short> IsItemLibraray { get; set; }
        public Nullable<int> ItemLibrarayGroupId { get; set; }
        public Nullable<int> PayInFullInvoiceId { get; set; }
        public Nullable<short> IsGroupItem { get; set; }
        public Nullable<short> ItemType { get; set; }
        public Nullable<short> IsIncludedInPipeLine { get; set; }
        public Nullable<short> IsRunOnQty { get; set; }
        public Nullable<short> CanCopyToEstimate { get; set; }
        public Nullable<int> FlagId { get; set; }
        public byte[] CostCenterDescriptions { get; set; }
        public Nullable<bool> IsRead { get; set; }
        public Nullable<byte> IsScheduled { get; set; }
        public Nullable<bool> IsPaperStatusChanged { get; set; }
        public Nullable<bool> IsJobCardCreated { get; set; }
        public Nullable<bool> IsAttachmentAdded { get; set; }
        public Nullable<bool> IsItemValueChanged { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string ItemNotes { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
        public Nullable<System.DateTime> JobActualStartDateTime { get; set; }
        public Nullable<System.DateTime> JobActualCompletionDateTime { get; set; }
        public Nullable<bool> IsJobCostingDone { get; set; }
        public string ProductName { get; set; }
        public Nullable<long> ProductCategoryId { get; set; }
        public string ImagePath { get; set; }
        public string ThumbnailPath { get; set; }
        public string ProductSpecification { get; set; }
        public string CompleteSpecification { get; set; }
        public string DesignGuideLines { get; set; }
        public string ProductCode { get; set; }
        public Nullable<bool> IsPublished { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public Nullable<int> PriceDiscountPercentage { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
        public Nullable<bool> IsSpecialItem { get; set; }
        public string IconPath { get; set; }
        public Nullable<bool> IsPopular { get; set; }
        public Nullable<bool> IsFeatured { get; set; }
        public Nullable<bool> IsPromotional { get; set; }
        public string TipsAndHints { get; set; }
        public string FactSheetFileName { get; set; }
        public Nullable<bool> IsArchived { get; set; }
        public Nullable<int> NominalCodeId { get; set; }
        public Nullable<int> RefItemId { get; set; }
        public Nullable<int> TemplateId { get; set; }
        public string WebDescription { get; set; }
        public Nullable<int> ItemTypeId { get; set; }
        public Nullable<bool> IsOrderedItem { get; set; }
        public Nullable<int> JobCardPrintedBy { get; set; }
        public Nullable<System.DateTime> JobCardLastPrintedDate { get; set; }
        public Nullable<int> EstimateProductionTime { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<int> IsFinishedGoods { get; set; }
        public string LayoutGridContent { get; set; }
        public string HowToVideoContent { get; set; }
        public string file1 { get; set; }
        public string file2 { get; set; }
        public string file3 { get; set; }
        public string file4 { get; set; }
        public string file5 { get; set; }
        public string GridImage { get; set; }
        public Nullable<bool> isQtyRanged { get; set; }
        public Nullable<double> CostCentreProfitBroker { get; set; }
        public Nullable<double> BaseChargeBroker { get; set; }
        public Nullable<double> MarkUpValueBroker { get; set; }
        public Nullable<double> NetTotalBroker { get; set; }
        public Nullable<double> TaxValueBroker { get; set; }
        public Nullable<double> GrossTotalBroker { get; set; }
        public Nullable<bool> isCMYK { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public Nullable<bool> isStockControl { get; set; }
        public Nullable<bool> isUploadImage { get; set; }
        public Nullable<bool> isMarketingBrief { get; set; }
        public Nullable<int> SupplierID2 { get; set; }
        public Nullable<int> FinishedGoodId { get; set; }
        public Nullable<short> IsFinishedGoodPrivate { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public Nullable<bool> isTemplateDesignMode { get; set; }
        public string XeroAccessCode { get; set; }
        public Nullable<double> DefaultItemTax { get; set; }
        public Nullable<long> OrganisationId { get; set; }
        public Nullable<double> PackagingWeight { get; set; }
        public Nullable<bool> IsVDPProduct { get; set; }

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
