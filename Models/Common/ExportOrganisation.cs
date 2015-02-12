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

        public List<PaperSize> PaperSizes { get; set; }

        public List<CostCentre> CostCentre { get; set; }

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

        public List<Item> Items { get; set; }
        public List<ItemSection> ItemSection { get; set; }
        public List<SectionCostcentre> SectionCostcentre { get; set; }
        public List<SectionCostCentreResource> SectionCostCentreResource { get; set; }

        public List<PaymentGateway> PaymentGateway { get; set; }

        public List<CmsSkinPageWidget> CmsSkinPageWidget { get; set; }

        public List<CompanyCostCentre> CompanyCostCentre { get; set; }

        public List<CompanyCMYKColor> CompanyCMYKColor { get; set; }

        




















        



        


    }
}
