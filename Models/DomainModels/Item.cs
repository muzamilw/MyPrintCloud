using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Item Domain Model
    /// </summary>
    [Serializable]
    public class Item
    {
        #region Persisted Properties
        
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
        public Guid? JobManagerId { get; set; }
        public DateTime? JobEstimatedStartDateTime { get; set; }
        public DateTime? JobEstimatedCompletionDateTime { get; set; }
        public DateTime? JobCreationDateTime { get; set; }
        public Guid? JobProgressedBy { get; set; }
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
        public long? TemplateId { get; set; }
        public string WebDescription { get; set; }
        public int? ItemTypeId { get; set; }
        public bool? IsOrderedItem { get; set; }
        public Guid? JobCardPrintedBy { get; set; }
        public DateTime? JobCardLastPrintedDate { get; set; }
        public int? EstimateProductionTime { get; set; }
        public int? SortOrder { get; set; }
        public int? ProductType { get; set; }
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
        public int? IsTemplateDesignMode { get; set; }
        public string XeroAccessCode { get; set; }
        public double? DefaultItemTax { get; set; }
        public long? OrganisationId { get; set; }
        public double? PackagingWeight { get; set; }
        public bool? IsVdpProduct { get; set; }
        public int? TemplateType { get; set; }
        public int? DesignerCategoryId { get; set; }
        public double? Scalar { get; set; }
        public double? ZoomFactor { get; set; }
        public bool? isAddCropMarks { get; set; }
        public double? ItemLength { get; set; }
        public double? ItemWidth { get; set; }
        public double? ItemHeight { get; set; }
        public double? ItemWeight { get; set; }
        public bool? printCropMarks { get; set; }
        public bool? drawBleedArea { get; set; }
        public bool? isMultipagePDF { get; set; }
        public bool? drawWaterMarkTxt { get; set; }
        public bool? allowPdfDownload { get; set; }
        public bool? allowImageDownload { get; set; }
        public long? SmartFormId { get; set; }
        public bool? IsDigitalDownload { get; set; }
        public bool? IsRealStateProduct { get; set; }
        public int? ProductDisplayOptions { get; set; }

        [NotMapped]
        public double MinPrice { get; set; }

        /// <summary>
        /// Being used for Template Service generateTemplateFromPdf method, if mode is 2 then preserves
        /// existing template object else removes
        /// </summary>
        [NotMapped]
        public int? TemplateTypeMode { get; set; }

        #endregion

        #region Reference Properties

        public virtual Status Status { get; set; }

        public virtual Estimate Estimate { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual Template Template { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<ItemAttachment> ItemAttachments { get; set; }
        public virtual ICollection<ItemImage> ItemImages { get; set; }
        public virtual ICollection<ItemPriceMatrix> ItemPriceMatrices { get; set; }
        public virtual ICollection<ItemRelatedItem> ItemRelatedItems { get; set; }
        public virtual ICollection<ItemRelatedItem> RelatedItems { get; set; }
        public virtual ICollection<ItemStateTax> ItemStateTaxes { get; set; }
        public virtual ICollection<ItemVdpPrice> ItemVdpPrices { get; set; }
        public virtual ICollection<ItemSection> ItemSections { get; set; }
        public virtual ICollection<ItemStockOption> ItemStockOptions { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<DeliveryNoteDetail> DeliveryNoteDetails { get; set; }
        public virtual ICollection<ItemVideo> ItemVideos { get; set; }
        public virtual ICollection<ProductCategoryItem> ProductCategoryItems { get; set; }
        public virtual ICollection<ItemProductDetail> ItemProductDetails { get; set; }
        public virtual ICollection<FavoriteDesign> FavoriteDesigns { get; set; }
        public virtual ICollection<ShippingInformation> ShippingInformations { get; set; }
        public virtual ICollection<ProductMarketBriefQuestion> ProductMarketBriefQuestions { get; set; }
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
        public virtual ICollection<GoodsReceivedNoteDetail> GoodsReceivedNoteDetails { get; set; }

            #endregion
        #region Additional Properties

        [NotMapped]
        public string ThumbnailImageName { get; set; }
        [NotMapped]
        public string ImagePathImageName { get; set; }
        [NotMapped]
        public string GridImageSourceName { get; set; }
        [NotMapped]
        public string ThumbnailImage { get; set; }
        [NotMapped]
        public string GridImageBytes { get; set; }
        [NotMapped]
        public string ImagePathImage { get; set; }
        [NotMapped]
        public string File1Name { get; set; }
        [NotMapped]
        public string File2Name { get; set; }
        [NotMapped]
        public string File3Name { get; set; }
        [NotMapped]
        public string File4Name { get; set; }
        [NotMapped]
        public string File5Name { get; set; }

        [NotMapped]
        public string File1Byte { get; set; }
        [NotMapped]
        public string File2Byte { get; set; }
        [NotMapped]
        public string File3Byte { get; set; }
        [NotMapped]
        public string File4Byte { get; set; }
        [NotMapped]
        public string File5Byte { get; set; }
        [NotMapped]
        public bool? File1Deleted { get; set; }
        [NotMapped]
        public bool? File2Deleted { get; set; }
        [NotMapped]
        public bool? File3Deleted { get; set; }
        [NotMapped]
        public bool? File4Deleted { get; set; }
        [NotMapped]
        public bool? File5Deleted { get; set; }

        /// <summary>
        /// Thumbnail Image Bytes - byte[] representation of Base64 string Thumbnail Image
        /// </summary>
        [NotMapped]
        public byte[] ThumbnailSourceBytes
        {
            get
            {
                if (string.IsNullOrEmpty(ThumbnailImage))
                {
                    return null;
                }

                int firtsAppearingCommaIndex = ThumbnailImage.IndexOf(',');

                if (firtsAppearingCommaIndex < 0)
                {
                    return null;
                }

                if (ThumbnailImage.Length < firtsAppearingCommaIndex + 1)
                {
                    return null;
                }

                string sourceSubString = ThumbnailImage.Substring(firtsAppearingCommaIndex + 1);

                try
                {
                    return Convert.FromBase64String(sourceSubString.Trim('\0'));
                }
                catch (FormatException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Grid Image Bytes - byte[] representation of Base64 string Grid Image
        /// </summary>
        [NotMapped]
        public byte[] GridImageSourceBytes
        {
            get
            {
                if (string.IsNullOrEmpty(GridImageBytes))
                {
                    return null;
                }

                int firtsAppearingCommaIndex = GridImageBytes.IndexOf(',');

                if (firtsAppearingCommaIndex < 0)
                {
                    return null;
                }

                if (GridImageBytes.Length < firtsAppearingCommaIndex + 1)
                {
                    return null;
                }

                string sourceSubString = GridImageBytes.Substring(firtsAppearingCommaIndex + 1);

                try
                {
                    return Convert.FromBase64String(sourceSubString.Trim('\0'));
                }
                catch (FormatException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Image Path Source Bytes - byte[] representation of Base64 string Image Path
        /// </summary>
        [NotMapped]
        public byte[] ImagePathSourceBytes
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePathImage))
                {
                    return null;
                }

                int firtsAppearingCommaIndex = ImagePathImage.IndexOf(',');

                if (firtsAppearingCommaIndex < 0)
                {
                    return null;
                }

                if (ImagePathImage.Length < firtsAppearingCommaIndex + 1)
                {
                    return null;
                }

                string sourceSubString = ImagePathImage.Substring(firtsAppearingCommaIndex + 1);

                try
                {
                    return Convert.FromBase64String(sourceSubString.Trim('\0'));
                }
                catch (FormatException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// File1 Source Bytes - byte[] representation of Base64 string File1
        /// </summary>
        [NotMapped]
        public byte[] File1SourceBytes
        {
            get
            {
                if (string.IsNullOrEmpty(File1Byte))
                {
                    return null;
                }

                int firtsAppearingCommaIndex = File1Byte.IndexOf(',');

                if (firtsAppearingCommaIndex < 0)
                {
                    return null;
                }

                if (File1Byte.Length < firtsAppearingCommaIndex + 1)
                {
                    return null;
                }

                string sourceSubString = File1Byte.Substring(firtsAppearingCommaIndex + 1);

                try
                {
                    return Convert.FromBase64String(sourceSubString.Trim('\0'));
                }
                catch (FormatException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// File2 Source Bytes - byte[] representation of Base64 string File2
        /// </summary>
        [NotMapped]
        public byte[] File2SourceBytes
        {
            get
            {
                if (string.IsNullOrEmpty(File2Byte))
                {
                    return null;
                }

                int firtsAppearingCommaIndex = File2Byte.IndexOf(',');

                if (firtsAppearingCommaIndex < 0)
                {
                    return null;
                }

                if (File2Byte.Length < firtsAppearingCommaIndex + 1)
                {
                    return null;
                }

                string sourceSubString = File2Byte.Substring(firtsAppearingCommaIndex + 1);

                try
                {
                    return Convert.FromBase64String(sourceSubString.Trim('\0'));
                }
                catch (FormatException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// File3 Source Bytes - byte[] representation of Base64 string File3
        /// </summary>
        [NotMapped]
        public byte[] File3SourceBytes
        {
            get
            {
                if (string.IsNullOrEmpty(File3Byte))
                {
                    return null;
                }

                int firtsAppearingCommaIndex = File3Byte.IndexOf(',');

                if (firtsAppearingCommaIndex < 0)
                {
                    return null;
                }

                if (File3Byte.Length < firtsAppearingCommaIndex + 1)
                {
                    return null;
                }

                string sourceSubString = File3Byte.Substring(firtsAppearingCommaIndex + 1);

                try
                {
                    return Convert.FromBase64String(sourceSubString.Trim('\0'));
                }
                catch (FormatException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// File4 Source Bytes - byte[] representation of Base64 string File4
        /// </summary>
        [NotMapped]
        public byte[] File4SourceBytes
        {
            get
            {
                if (string.IsNullOrEmpty(File4Byte))
                {
                    return null;
                }

                int firtsAppearingCommaIndex = File4Byte.IndexOf(',');

                if (firtsAppearingCommaIndex < 0)
                {
                    return null;
                }

                if (File4Byte.Length < firtsAppearingCommaIndex + 1)
                {
                    return null;
                }

                string sourceSubString = File4Byte.Substring(firtsAppearingCommaIndex + 1);

                try
                {
                    return Convert.FromBase64String(sourceSubString.Trim('\0'));
                }
                catch (FormatException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// File5 Source Bytes - byte[] representation of Base64 string File5
        /// </summary>
        [NotMapped]
        public byte[] File5SourceBytes
        {
            get
            {
                if (string.IsNullOrEmpty(File5Byte))
                {
                    return null;
                }

                int firtsAppearingCommaIndex = File5Byte.IndexOf(',');

                if (firtsAppearingCommaIndex < 0)
                {
                    return null;
                }

                if (File5Byte.Length < firtsAppearingCommaIndex + 1)
                {
                    return null;
                }

                string sourceSubString = File5Byte.Substring(firtsAppearingCommaIndex + 1);

                try
                {
                    return Convert.FromBase64String(sourceSubString.Trim('\0'));
                }
                catch (FormatException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Product Category Items Custom - Used for Categories Selection for Product
        /// </summary>
        public ICollection<ProductCategoryItemCustom> ProductCategoryCustomItems { get; set; }

        #endregion

        #region Public

        /// <summary>
        /// Makes a copy of Item
        /// </summary>
        public void Clone(Item target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemClone_InvalidItem, "target");
            }

            target.ProductName = ProductName + " Copy";
            target.ProductCode = ProductCode;
            target.ProductSpecification = ProductSpecification;
            target.IsEnabled = IsEnabled;
            target.IsFeatured = IsFeatured;
            target.ProductType = ProductType;
            target.IsPublished = IsPublished;
            target.SortOrder = SortOrder;
            target.IsVdpProduct = IsVdpProduct;
            target.ItemWeight = ItemWeight;
            target.ItemLength = ItemLength;
            target.ItemWidth = ItemWidth;
            target.ItemHeight = ItemHeight;
            target.IsStockControl = IsStockControl;
            target.FlagId = FlagId;
            target.IsQtyRanged = IsQtyRanged;
            target.PackagingWeight = PackagingWeight;
            target.DefaultItemTax = DefaultItemTax;
            target.SupplierId = SupplierId;
            target.SupplierId2 = SupplierId2;
            target.EstimateProductionTime = EstimateProductionTime;
            target.TemplateType = TemplateType;
            target.IsTemplateDesignMode = IsTemplateDesignMode;
            target.IsCmyk = IsCmyk;
            target.ZoomFactor = ZoomFactor;
            target.Scalar = Scalar;
            target.DesignerCategoryId = DesignerCategoryId;
            target.CompanyId = CompanyId;
            target.ThumbnailPath = ThumbnailPath;
            target.GridImage = GridImage;
            target.ImagePath = ImagePath;
            target.File1 = File1;
            target.File2 = File2;
            target.File3 = File3;
            target.File4 = File4;
            target.File5 = File5;
            target.ProductDisplayOptions = ProductDisplayOptions;
            target.IsUploadImage = IsUploadImage;
            target.IsDigitalDownload = IsDigitalDownload;
            target.IsRealStateProduct = IsRealStateProduct;
            target.SmartFormId = SmartFormId;
            target.ItemType = ItemType;
            
            // Copy Internal Descriptions
            CloneInternalDescriptions(target);
        }

        /// <summary>
        /// Clone Internal Descriptions
        /// </summary>
        /// <param name="target"></param>
        private void CloneInternalDescriptions(Item target)
        {
            target.WebDescription = WebDescription;
            target.TipsAndHints = TipsAndHints;
            target.XeroAccessCode = XeroAccessCode;
            target.MetaTitle = MetaTitle;
            target.MetaDescription = MetaDescription;
            target.MetaKeywords = MetaKeywords;
            target.JobDescriptionTitle1 = JobDescriptionTitle1;
            target.JobDescription1 = JobDescription1;
            target.JobDescriptionTitle2 = JobDescriptionTitle2;
            target.JobDescription2 = JobDescription2;
            target.JobDescriptionTitle3 = JobDescriptionTitle3;
            target.JobDescription3 = JobDescription3;
            target.JobDescriptionTitle4 = JobDescriptionTitle4;
            target.JobDescription4 = JobDescription4;
            target.JobDescriptionTitle5 = JobDescriptionTitle5;
            target.JobDescription5 = JobDescription5;
            target.JobDescriptionTitle6 = JobDescriptionTitle6;
            target.JobDescription6 = JobDescription6;
            target.JobDescriptionTitle7 = JobDescriptionTitle7;
            target.JobDescription7 = JobDescription7;
        }

        /// <summary>
        /// Makes a copy of Item
        /// </summary>
        public void CloneForOrder(Item target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemClone_InvalidItem, "target");
            }

            Clone(target);

            // Copy Internal Descriptions
            CloneInternalDescriptions(target);

            target.Qty1 = Qty1;
            target.Qty1NetTotal = Qty1NetTotal;
            target.Qty1Tax1Value = Qty1Tax1Value;
            target.Qty1GrossTotal = Qty1GrossTotal;
            target.Qty2 = Qty2;
            target.Qty2NetTotal = Qty2NetTotal;
            target.Qty2Tax1Value = Qty2Tax1Value;
            target.Qty2GrossTotal = Qty2GrossTotal;
            target.Qty3 = Qty3;
            target.Qty3NetTotal = Qty3NetTotal;
            target.Qty3Tax1Value = Qty3Tax1Value;
            target.Qty3GrossTotal = Qty3GrossTotal;
            target.InvoiceDescription = InvoiceDescription;
            target.ItemNotes = ItemNotes;
        }

        #endregion

        #region Additional Properties

        /// <summary>
        /// If Template Type changes to blank
        /// </summary>
        [NotMapped]
        public bool? HasTemplateChangedToCustom { get; set; }

        /// <summary>
        /// TemplateId - used to keep track of Current Template before deleting it for desinger type
        /// </summary>
        [NotMapped]
        public long? OldTemplateId { get; set; }

        #endregion
    }
}