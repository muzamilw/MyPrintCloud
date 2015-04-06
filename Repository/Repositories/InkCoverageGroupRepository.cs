using Microsoft.Practices.Unity;
using MPC.Models.DomainModels;
using MPC.Interfaces.Repository;
using MPC.Repository.BaseRepository;
using System.Data.Entity;

namespace MPC.Repository.Repositories
{
    public class InkCoverageGroupRepository : BaseRepository<InkCoverageGroup>, IInkCoverageGroupRepository
    {
        public InkCoverageGroupRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<InkCoverageGroup> DbSet
        {
            get { return db.InkCoverageGroups; }
        }
    }
}
