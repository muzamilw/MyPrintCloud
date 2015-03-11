using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.Common;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    public interface INewsLetterSubscriberRepository : IBaseRepository<NewsLetterSubscriber, long>
    {
        NewsLetterSubscriber GetSubscriber(string email, long CompanyId);
        int AddSubscriber(NewsLetterSubscriber subsriber);
        bool UpdateSubscriber(string subscriptionCode, SubscriberStatus status);
    }
}
