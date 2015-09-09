using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    public interface IProductCategoryRepository : IBaseRepository<ProductCategory, long>
    {
        List<ProductCategory> GetAllCategories();
        List<ProductCategory> GetParentCategoriesByStoreId(long companyId, long OrganisationId);

        List<ProductCategory> GetAllParentCorporateCatalog(int customerId);

        List<ProductCategory> GetAllParentCorporateCatalogByTerritory(int customerId, int ContactId);

        List<ProductCategory> GetAllCategoriesByStoreId(long companyId, long OrganisationId);

        ProductCategory GetCategoryById(long categoryId);

        List<ProductCategory> GetChildCategories(long categoryId, long CompanyId);

        List<ProductCategory> GetAllChildCorporateCatalogByTerritory(long customerId, long ContactId, long ParentCatId);

        List<ProductCategoriesView> GetMappedCategoryNames(bool isClearCache, int companyID);

        List<ProductCategoriesView> GetProductDesignerMappedCategoryNames(int CompanyID);

      

        ProductCategoriesView GetMappedCategory(string CatName, int CID);

        /// <summary>
        /// get name of parent categogry by ItemID
        /// </summaery>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        string GetImmidiateParentCategory(long ItemID,out string CurrentProductCategoryName);

        /// <summary>
        /// Get parent categories
        /// </summary>
        IEnumerable<ProductCategory> GetParentCategories(long? companyId);
        List<ProductCategory> GetChildCategories(long categoryId);
        List<ProductCategory> GetChildCategoriesIncludingArchive(long categoryId);
        List<ProductCategory> GetAllRetailPublishedCat();
        IEnumerable<ProductCategory> GetParentCategoriesIncludingArchived(long? companyId);
    }
}
