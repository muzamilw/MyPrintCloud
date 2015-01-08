using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.WebStoreServices
{
    public interface IOrderService
    {
        int GetFirstItemIDByOrderId(int orderId);

        long ProcessPublicUserOrder(string orderTitle, long OrganisationId, int storeMode, long CompanyId);

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
