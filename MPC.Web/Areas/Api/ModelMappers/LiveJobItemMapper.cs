using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;
using DomainResponseModels = MPC.Models.ResponseModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{

    /// <summary>
    /// Live Job Item Mapper
    /// </summary>
    public static class LiveJobItemMapper
    {
        public static LiveJobItem CreateFromForLiveJobItem(this DomainModels.Item source)
        {
            return new LiveJobItem()
            {
                EstimateId = source.EstimateId,
                ItemId = source.ItemId,
                EstimateDate = source.Estimate != null ? source.Estimate.Order_Date : null,
                CompanyName = source.Estimate != null ? source.Estimate.Company != null ? source.Estimate.Company.Name : string.Empty : string.Empty,
                ProductName = source.ProductName,
                CompanyId = source.Estimate != null ? source.Estimate.CompanyId : 0,
                Qty1 = source.Qty1,
                JobManagerId = source.JobManagerId,
                JobCode = source.JobCode,
                //StatusName = source.Status != null ? source.Status.StatusName : string.Empty,
                isDirectSale = source.Estimate != null ? source.Estimate.isDirectSale : false,
                ItemAttachments = source.ItemAttachments.Select(attch => attch.CreateFromForLiveJobs()).ToList(),
                JobStatusId = source.JobStatusId
            };
        }


        /// <summary>
        /// Domain to Web
        /// </summary>
        public static LiveJobsSearchResponse CreateFrom(this DomainResponseModels.LiveJobsSearchResponse source)
        {
            return new LiveJobsSearchResponse()
            {
                Items = source.Items.Select(item => item.CreateFromForLiveJobItem()).ToList(),
                TotalCount = source.TotalCount
            };
        }
    }
}