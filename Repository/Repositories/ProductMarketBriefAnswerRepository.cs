using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// ProductMarketBriefAnswer Repository
    /// </summary>
    public class ProductMarketBriefAnswerRepository : BaseRepository<ProductMarketBriefAnswer>, IProductMarketBriefAnswerRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductMarketBriefAnswerRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ProductMarketBriefAnswer> DbSet
        {
            get
            {
                return db.ProductMarketBriefAnswers;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Find Market Brief Answer By Id
        /// </summary>
        public ProductMarketBriefAnswer Find(int id)
        {
            return base.Find(id);
        }

        #endregion
        
    }
}
