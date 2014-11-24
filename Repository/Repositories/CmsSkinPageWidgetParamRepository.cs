using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class CmsSkinPageWidgetParamRepository : BaseRepository<CmsSkinPageWidgetParam>, ICmsSkinPageWidgetParamRepository
    {
        public CmsSkinPageWidgetParamRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CmsSkinPageWidgetParam> DbSet
        {
            get
            {
                return db.PageWidgetParams;
            }
        }
    }
}
