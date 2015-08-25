using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;
namespace MPC.Models.Common
{
    public class ExportOrganisation
    {
        public Organisation Organisation { get; set; }

        public List<Company> SuppliersList { get; set; }
        public List<PaperSize> PaperSizes { get; set; }

      

        public List<CostCentre> CostCentre { get; set; }

        public List<CostCentreType> CostCentreType { get; set; }
        public List<CostCenterChoice> CostCenterChoice { get; set; }

        public List<CostCentreQuestion> CostCentreQuestion { get; set; }


        public List<CostCentreAnswer> CostCentreAnswer { get; set; }

        public List<CostcentreInstruction> CostcentreInstruction { get; set; }

        public List<CostcentreResource> CostcentreResource { get; set; }
        public List<CostCentreMatrix> CostCentreMatrix { get; set; }
        public List<CostCentreMatrixDetail> CostCentreMatrixDetail { get; set; }

        public List<CostcentreWorkInstructionsChoice> CostcentreWorkInstructionsChoice { get; set; }

        public List<StockCategory> StockCategory { get; set; }
        public List<StockSubCategory> StockSubCategory { get; set; }

        public List<StockItem> StockItem { get; set; }

        public List<StockCostAndPrice> StockCostAndPrice { get; set; }

        // delivery carriers missing
        //public List<StockItem> StockItem { get; set; }
        public List<Report> Reports { get; set; }

        // report notes table add in edmx
        public List<ReportNote> ReportNote { get; set; }

        public List<Prefix> Prefixes { get; set; }

        public List<Markup> Markups { get; set; }

        public List<Machine> Machines { get; set; }

        public List<LookupMethod> LookupMethods { get; set; }

        public List<Phrase> Phrases { get; set; }

        public List<PhraseField> PhraseField { get; set; }

        public List<SectionFlag> SectionFlags { get; set; }

        public Company Company { get; set; }

        public List<CompanyDomain> CompanyDomain { get; set; }

        public List<SystemUser> SystemUser { get; set; }

        public List<CmsOffer> CmsOffer { get; set; }

        public List<MediaLibrary> MediaLibrary { get; set; }

        public List<CompanyBannerSet> CompanyBannerSet { get; set; }

        public List<CompanyBanner> CompanyBanner { get; set; }

        public List<CmsPage> SecondaryPages { get; set; }

        public List<RaveReview> RaveReview { get; set; }

        public List<CompanyTerritory> CompanyTerritory { get; set; }


        public List<Address> Address { get; set; }

        public List<CompanyContact> CompanyContact { get; set; }
        public List<ProductCategory> ProductCategory { get; set; }

        public List<TemplateFont> TemplateFonts { get; set; }

        //public List<DiscountVoucher> DiscountVouchers { get; set; }
        public List<Item> Items { get; set; }
        public List<ItemAttachment> ItemAttachment { get; set; }
        public List<ItemSection> ItemSection { get; set; }
        public Template ItemTemplate { get; set; }

        public List<TemplatePage> TemplatePages { get; set; }
        public List<TemplateObject> TemplateObjects { get; set; }
      

        public List<TemplateColorStyle> TemplateColorStyle { get; set; }

        public List<TemplateBackgroundImage> TemplateBackgroundImage { get; set; }

        public List<ImagePermission> ImagePermission { get; set; }
        public List<SectionCostcentre> SectionCostcentre { get; set; }
        public List<SectionCostCentreResource> SectionCostCentreResource { get; set; }

        public List<PaymentGateway> PaymentGateways { get; set; }

        public List<CmsSkinPageWidget> CmsSkinPageWidget { get; set; }

        public List<CompanyCostCentre> CompanyCostCentre { get; set; }

        public List<CompanyCMYKColor> CompanyCMYKColor { get; set; }


        public List<Campaign> Campaigns { get; set; }


        public Company RetailCompany { get; set; }



        public List<CompanyDomain> RetailCompanyDomain { get; set; }

        public List<SystemUser> RetailSystemUser { get; set; }

        public List<CmsOffer> RetailCmsOffer { get; set; }

        public List<MediaLibrary> RetailMediaLibrary { get; set; }

        public List<CompanyBannerSet> RetailCompanyBannerSet { get; set; }

        public List<CompanyBanner> RetailCompanyBanner { get; set; }

        public List<CmsPage> RetailSecondaryPages { get; set; }

        public List<RaveReview> RetailRaveReview { get; set; }

        public List<CompanyTerritory> RetailCompanyTerritory { get; set; }


        public List<Address> RetailAddress { get; set; }

        public List<CompanyContact> RetailCompanyContact { get; set; }
        public List<ProductCategory> RetailProductCategory { get; set; }

        public List<Item> RetailItems { get; set; }
        public List<ItemSection> RetailItemSection { get; set; }
        public List<SectionCostcentre> RetailSectionCostcentre { get; set; }
        public List<SectionCostCentreResource> RetailSectionCostCentreResource { get; set; }

       

        public List<ItemImage> RetailItemImages { get; set; }

        public List<ItemRelatedItem> RetailRelatedItems { get; set; }
        public List<ItemVdpPrice> RetailItemVDPPrices { get; set; }
        public List<ItemPriceMatrix> RetailItemPriceMatrix { get; set; }

        public List<ItemProductDetail> RetailItemProductDetail { get; set; }

        public List<ItemStockOption> RetailItemStockOptions { get; set; }
        public List<ItemStateTax> RetailItemStateTax { get; set; }
        public List<ItemVideo> RetailItemVideo { get; set; }
        public Template RetailItemTemplate { get; set; }

        public List<TemplatePage> RetailTemplatePages { get; set; }
        public List<TemplateObject> RetailTemplateObjects { get; set; }
        public List<TemplateFont> RetailTemplateFonts { get; set; }

        public List<TemplateColorStyle> RetailTemplateColorStyle { get; set; }

        public List<TemplateBackgroundImage> RetailTemplateBackgroundImage { get; set; }

        public List<ImagePermission> RetailImagePermission { get; set; }
        public List<PaymentGateway> RetailPaymentGateways { get; set; }

        public List<CmsSkinPageWidget> RetailCmsSkinPageWidget { get; set; }

        public List<CompanyCostCentre> RetailCompanyCostCentre { get; set; }

        public List<CompanyCMYKColor> RetailCompanyCMYKColor { get; set; }


        public List<Campaign> RetailCampaigns { get; set; }

        public List<SmartForm> RetailSmartForms { get; set; }

        public List<SmartForm> SmartForms { get; set; }

        public List<FieldVariable> RetailFieldVariables { get; set; }

        public List<FieldVariable> FieldVariables { get; set; }

    


    }

    public class ImportOrganisation
    {
        public long NewOrganisationID { get; set; }

        public long OldOrganisationID { get; set; }

        public List<long> CostCentreIDs { get; set; }

        public long ReportID { get; set; }

        public long NewCompanyID { get; set; }
        public long RetailOldCompanyID { get; set; }

        public long OldCompanyID { get; set; }

        public long ContactID { get; set; }

        public long MediaLibraryID { get; set; }

        public long BannerID { get; set; }

        public long ProductCategoryID { get; set; }

        public long ItemID { get; set; }

        public long CompanyIDWOP { get; set; }

        public long RetailOldCompanyIDWOP { get; set; }

        public long OldCompanyIDWOP { get; set; }




       

    }

    public class ExportSets
    {
        public ExportOrganisation ExportOrganisationSet1 { get; set; }

        public ExportOrganisation ExportOrganisationSet2 { get; set; }

        public ExportOrganisation ExportOrganisationSet3 { get; set; }

        public ExportOrganisation ExportOrganisationSet4 { get; set; }

        public ExportOrganisation ExportRetailStore1 { get; set; }

        public List<ProductCategory> ExportRetailStore2 { get; set; }

        public List<Item> ExportRetailStore3 { get; set; }

        public List<CmsPage> ExportRetailStore4 { get; set; }

        public ExportOrganisation ExportStore1 { get; set; }

        public List<ProductCategory> ExportStore2 { get; set; }

        public List<Item> ExportStore3 { get; set; }

        public List<CmsPage> ExportStore4 { get; set; }

        public ExportOrganisation ExportStore1WOP { get; set; }

        public List<ProductCategory> ExportStore2WOP { get; set; }

        public List<Item> ExportStore3WOP { get; set; }

        public List<CmsPage> ExportStore4WOP { get; set; }

        public ExportOrganisation ExportRetailStore1WOP { get; set; }

        public List<ProductCategory> ExportRetailStore2WOP { get; set; }

        public List<Item> ExportRetailStore3WOP { get; set; }

        public List<CmsPage> ExportRetailStore4WOP { get; set; }

      



    }

  
}
