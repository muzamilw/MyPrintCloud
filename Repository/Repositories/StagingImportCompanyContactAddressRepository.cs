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
    public class StagingImportCompanyContactAddressRepository : BaseRepository<StagingImportCompanyContactAddress>, IStagingImportCompanyContactAddressRepository
    {
        public StagingImportCompanyContactAddressRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<StagingImportCompanyContactAddress> DbSet
        {
            get { return db.StagingImportCompanyContactAddresses; }
        }

        public bool RunProcedure(long organisationId, long? storeId)
        {
            var result = db.usp_importTerritoryContactAddressByStore(organisationId, storeId);
            return true;
        }
        public bool RunProcedureToDeleteAllStagingCompanyContact()
        {
            var result = db.usp_DeleteStagingImportCompanyContactAddress();
            return true;
        }
    }
}
