using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IOrderRepository : IBaseRepository<Estimate,long>
    {
        int GetFirstItemIDByOrderId(int orderId);

       

        long CreateNewOrder(int customerID, int contactId, Company Company, Organisation Organisation, Prefix prefix, string orderTitle = null);

        long GetOrderID(int customerID, int contactId, string orderTitle, Company company, Organisation org, Prefix prefix);
    }
}
