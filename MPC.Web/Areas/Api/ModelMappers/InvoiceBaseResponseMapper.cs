using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.ResponseModels;
    public static class InvoiceBaseResponseMapper
    {
        public static InvoiceBaseResponse CreateFrom(this DomainModels.InvoiceBaseResponse source)
        {
            return new InvoiceBaseResponse 
            {
                SectionFlags = source.SectionFlags != null ? source.SectionFlags.Select(cc => cc.CreateFromDropDown()) :  new List<SectionFlagDropDown>(),
                SystemUsers = source.SystemUsers != null ? source.SystemUsers.Select(cc => cc.CreateFrom()) :  new List<SystemUserDropDown>(),
                CurrencySymbol = source.CurrencySymbol,
                LoggedInUserId = source.LoggedInUserId,
                CostCenters = source.CostCenters != null ? source.CostCenters.Select(x => x.CreateFrom()).ToList() : new List<CostCentre>()
            };
        }
    }
}