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
    public class LookupMethodRepository : BaseRepository<LookupMethod>, ILookupMethodRepository
    {
        #region Constructor
        public LookupMethodRepository(IUnityContainer container)
            : base(container)
        {

        }
        protected override IDbSet<LookupMethod> DbSet
        {
            get
            {
                return db.LookupMethods;
            }
        }

        public IEnumerable<LookupMethod> GetAll()
        {
            return DbSet.Where(g => g.OrganisationId == OrganisationId || g.OrganisationId == 0).ToList();
        }

        #endregion
    }
}
