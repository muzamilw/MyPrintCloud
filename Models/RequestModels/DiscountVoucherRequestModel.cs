using System;

namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Activity Request Model
    /// </summary>
    public class DiscountVoucherRequestModel : GetPagedListRequest
    {
        public string SearchString { get; set; }
        public long CompanyId { get; set; }
    }
}
