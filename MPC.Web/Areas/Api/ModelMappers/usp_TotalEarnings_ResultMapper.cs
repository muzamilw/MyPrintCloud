using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// usp_TotalEarnings_Result Mapper
    /// </summary>
    public static class usp_TotalEarnings_ResultMapper
    {
        public static usp_TotalEarnings_Result CreateFrom(this DomainModels.usp_TotalEarnings_Result source)
        {

            return new usp_TotalEarnings_Result()
            {
                Month = source.Month,
                Orders = source.Orders,
                Total = source.Total,
                monthname = source.monthname,
                year = source.year,
                store = source.store,
            };
        }

    }
}