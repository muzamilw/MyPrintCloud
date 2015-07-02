using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    public interface IStagingImportCompanyContactAddressRepository : IBaseRepository<StagingImportCompanyContactAddress, long>
    {
        bool RunProcedure(long organisationId, long? storeId);
        bool RunProcedureToDeleteAllStagingCompanyContact();
    }
}
