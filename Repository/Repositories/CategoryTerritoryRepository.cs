using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class CategoryTerritoryRepository : BaseRepository<CategoryTerritory>, ICategoryTerritoryRepository
    {
        public CategoryTerritoryRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<CategoryTerritory> DbSet
        {
            get { return db.CategoryTerritories; }
        }
    }
}
