using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;

namespace MPC.Models.RequestModels
{
    public class CompanySavingModel
    {
        public Company Company { get; set; }
        public IEnumerable<CompanyTerritory> NewAddedCompanyTerritories { get; set; }
        public IEnumerable<CompanyTerritory> EdittedCompanyTerritories { get; set; }
        public IEnumerable<CompanyTerritory> DeletedCompanyTerritories { get; set; }
        public IEnumerable<Address> NewAddedAddresses { get; set; }
        public IEnumerable<Address> EdittedAddresses { get; set; }
        public IEnumerable<Address> DeletedAddresses { get; set; }
        public IEnumerable<ProductCategory> NewProductCategories { get; set; }
        public IEnumerable<ProductCategory> EdittedProductCategories { get; set; }
        public IEnumerable<ProductCategory> DeletedProductCategories { get; set; }
        public IEnumerable<CompanyContact> NewAddedCompanyContacts { get; set; }
        public IEnumerable<CompanyContact> EdittedCompanyContacts { get; set; }
        public IEnumerable<CompanyContact> DeletedCompanyContacts { get; set; }

        public List<CmsPage> NewAddedCmsPages { get; set; }
        public List<CmsPage> EditCmsPages { get; set; }
        public List<CmsPage> DeletedCmsPages { get; set; }
        public List<PageCategory> PageCategories { get; set; }

       public List<CmsPageWithWidgetList> CmsPageWithWidgetList { get; set; }
    }
}
