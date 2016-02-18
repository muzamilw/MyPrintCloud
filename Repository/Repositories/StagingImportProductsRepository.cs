using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Repository.Repositories
{
    public class StagingImportProductsRepository : BaseRepository<StagingProductPriceImport>, IStagingImportProductsRepository
    {

        public StagingImportProductsRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<StagingProductPriceImport> DbSet
        {
            get { return db.StagingProductPriceImports; }
        }

      
        public bool RunProcedureToDeleteAllStagingProducts()
        {
            var result = db.usp_DeleteStagingImportProduct();
            return true;
        }

        public bool RunProcedure(long organisationId, long? storeId)
        {
            var result = db.usp_ImportProductPriceMatrix(organisationId, storeId);
            return true;
        }
      
    }
}
