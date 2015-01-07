using MPC.Models.Common;
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

        long GetUserShopCartOrderID(int status);

        ShoppingCart GetShopCartOrderAndDetails(long orderID, OrderStatus orderStatus);

        DiscountVoucher GetVoucherRecord(int VId);

        Estimate GetOrderByID(long orderId);
        bool IsVoucherValid(string voucherCode);

        Estimate CheckDiscountApplied(int orderId);

        bool RollBackDiscountedItems(int orderId, double StateTax, StoreMode Mode);

        double SaveVoucherCodeAndRate(int orderId, string VCode);
        double PerformVoucherdiscountOnEachItem(int orderId, OrderStatus orderStatus, double StateTax, double VDiscountRate, StoreMode Mode);

        bool ResetOrderVoucherCode(int orderId);
    }
}
