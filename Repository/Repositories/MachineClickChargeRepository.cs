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
    public class MachineClickChargeRepository : BaseRepository<MachineClickChargeZone>, IMachineClickChargeZoneRepository
    {
        public MachineClickChargeRepository(IUnityContainer container)
            : base(container)
        {
        }
        protected override IDbSet<MachineClickChargeZone> DbSet
        {
            get
            {
                return db.MachineClickChargeZones;
            }
        }
        public override IEnumerable<MachineClickChargeZone> GetAll()
        {
            try
            {
                return DbSet.Where(c => c.OrganisationId == OrganisationId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
