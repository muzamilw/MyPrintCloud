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
        List<ProductCategory> GetParentCategoriesByStoreId(long companyId);

        List<ProductCategory> GetAllParentCorporateCatalog(int customerId);

        List<ProductCategory> GetAllParentCorporateCatalogByTerritory(int customerId, int ContactId);

        List<ProductCategory> GetAllCategoriesByStoreId(long companyId);
    }
}
