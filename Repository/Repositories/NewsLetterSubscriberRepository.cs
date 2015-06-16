using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class NewsLetterSubscriberRepository : BaseRepository<NewsLetterSubscriber>, INewsLetterSubscriberRepository
    {
          #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public NewsLetterSubscriberRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<NewsLetterSubscriber> DbSet
        {
            get
            {
                return db.NewsLetterSubscribers;
            }
        }

        public  NewsLetterSubscriber GetSubscriber(string email, long CompanyId)
        {
            NewsLetterSubscriber result = null;
            try
            {
                result = db.NewsLetterSubscribers.Where(c => c.Email == email && c.ContactCompanyID == CompanyId).FirstOrDefault();
                    
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public  int AddSubscriber(NewsLetterSubscriber subsriber)
        {
            try
            {
                    db.NewsLetterSubscribers.Add(subsriber);
                    db.SaveChanges();

                    return subsriber.SubscriberId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  bool UpdateSubscriber(string subscriptionCode, SubscriberStatus status)
        {
            try
            {

                NewsLetterSubscriber subscriber = db.NewsLetterSubscribers.Where(c => c.SubscriptionCode == subscriptionCode).FirstOrDefault();
                    if (subscriber != null)
                    {
                        subscriber.Status = (int)status;
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
