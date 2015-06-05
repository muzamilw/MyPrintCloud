using System.Linq;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class DashboardMapper
    {
        /// <summary>
        ///Order Statuses Response
        /// </summary>
        public static OrderStatusesResponse CreateFrom(this MPC.Models.ResponseModels.OrderStatusesResponse source)
        {
            return new OrderStatusesResponse
            {
                PendingOrdersCount = source.PendingOrdersCount,
                CompletedOrdersCount = source.CompletedOrdersCount,
                CurrentMonthOdersCount = source.CurrentMonthOdersCount,
                InProductionOrdersCount = source.InProductionOrdersCount,
                LiveStoresCount = source.LiveStoresCount,
                TotalEarnings = source.TotalEarnings,
                UnConfirmedOrdersCount = source.UnConfirmedOrdersCount,
                Estimates = source.Estimates.Select(x=>x.CreateFromForListView()),
                Companies = source.Companies.Select(company => company.CreateFromCustomer())
            };
        }
    }
}