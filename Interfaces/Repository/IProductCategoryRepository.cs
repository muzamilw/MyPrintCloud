﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    public interface IProductCategoryRepository : IBaseRepository<ProductCategory, long>
    {
        List<ProductCategory> GetParentCategoriesByStoreId(long companyId);

        List<ProductCategory> GetAllParentCorporateCatalog(int customerId);

        List<ProductCategory> GetAllParentCorporateCatalogByTerritory(int customerId, int ContactId);

        List<ProductCategory> GetAllCategoriesByStoreId(long companyId);

        ProductCategory GetCategoryById(int categoryId);

        List<ProductCategory> GetChildCategories(int categoryId);

        List<ProductCategory> GetAllChildCorporateCatalogByTerritory(int customerId, int ContactId, int ParentCatId);

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
        IEnumerable<ProductCategory> GetParentCategories();

    }
}
