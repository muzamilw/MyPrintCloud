using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MPC.Models.DomainModels;
using MPC.Interfaces.Repository;
using MPC.Repository.BaseRepository;
using System.Data.Entity;


namespace MPC.Repository.Repositories
{
    public class CompanyDomainRepository : BaseRepository<CompanyDomain>, ICompanyDomainRepository
    {
        public CompanyDomainRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<CompanyDomain> DbSet
        {
            get
            {
                return db.CompanyDomains;
            }
        }


        #region Public

        public override IEnumerable<CompanyDomain> GetAll()
        {
            return DbSet.ToList();
        }

        #endregion


    }
}
