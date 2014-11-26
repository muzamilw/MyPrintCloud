using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<ProductCategory> DbSet
        {
            get
            {
                return db.ProductCategories;
            }
        }


        public List<ProductCategory> GetParentCategoriesByTerritory(long companyId)
        {

            return db.ProductCategories.Where(
                p => p.CompanyId == companyId && (p.ParentCategoryId == null || p.ParentCategoryId == 0)).ToList();
           
        }

    }
}
