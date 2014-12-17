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
        List<ProductCategory> GetParentCategoriesByTerritory(long companyId);

        List<ProductCategory> GetAllParentCorporateCatalog(int customerId);

        List<ProductCategory> GetAllParentCorporateCatalogByTerritory(int customerId, int ContactId);

        List<ProductCategory> GetParentCategories();

        ProductCategory GetCategoryById(int categoryId);

        List<ProductCategory> GetChildCategories(int categoryId);

        List<ProductCategory> GetAllChildCorporateCatalogByTerritory(int customerId, int ContactId, int ParentCatId);
        
    }
}
