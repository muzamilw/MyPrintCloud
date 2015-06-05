using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// ProductMarketBriefQuestion Repository
    /// </summary>
    public class ProductMarketBriefQuestionRepository : BaseRepository<ProductMarketBriefQuestion>, IProductMarketBriefQuestionRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductMarketBriefQuestionRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ProductMarketBriefQuestion> DbSet
        {
            get
            {
                return db.ProductMarketBriefQuestions;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Find Market Brief Question By Id
        /// </summary>
        public ProductMarketBriefQuestion Find(int id)
        {
            return base.Find(id);
        }

        #endregion

        
    }
}
