using System;
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

        public RaveReview GetRaveReview(long companyId)
        {
            try
            {
                List<RaveReview> EnabledReviewRecordList = new List<RaveReview>();
                EnabledReviewRecordList = (from review in db.RaveReviews
                                           where review.isDisplay == true && review.CompanyId == companyId
                                           select review).ToList();
                if (EnabledReviewRecordList.Count > 0)
                {
                    int randomRecordFromList = new Random().Next() % EnabledReviewRecordList.Count(); //To make sure its valid index in list
                    var Record = EnabledReviewRecordList.Skip(randomRecordFromList).Take(1);
                    var finalRecord = Record.ToList().First();

                    return finalRecord;
                }
                else
                {
                    return EnabledReviewRecordList.FirstOrDefault(c => c.CompanyId == companyId);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }
    }
}
