using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class RaveReviewRepository : BaseRepository<RaveReview>, IRaveReviewRepository
    {
        #region Constructor

        public RaveReviewRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<RaveReview> DbSet
        {
            get
            {
                return db.RaveReviews;
            }
        }
        #endregion
        /// <summary>
        /// Get All Stock Sub Category
        /// </summary>
        public override IEnumerable<RaveReview> GetAll()
        {
            return DbSet.ToList();
        }
    }
}
