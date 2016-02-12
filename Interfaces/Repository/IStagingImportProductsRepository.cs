using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IStagingImportProductsRepository : IBaseRepository<StagingProductPriceImport, long>
    {
        bool RunProcedure(long organisationId, long? storeId);
        bool RunProcedureToDeleteAllStagingProducts();
    }
}
