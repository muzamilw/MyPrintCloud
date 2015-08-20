using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class CompanyCMYKColorRepository : BaseRepository<CompanyCMYKColor>, ICompanyCMYKColorRepository
    {
        #region Constructor

        public CompanyCMYKColorRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<CompanyCMYKColor> DbSet
        {
            get
            {
                return db.CompanyCmykColors;
            }
        }
        #endregion
        /// <summary>
        /// Get All Company CMYK COlors
        /// </summary>
        public override IEnumerable<CompanyCMYKColor> GetAll()
        {
           
            return DbSet.ToList();
        }
    }
}
