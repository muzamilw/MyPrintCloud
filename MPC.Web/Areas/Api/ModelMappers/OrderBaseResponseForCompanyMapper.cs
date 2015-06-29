using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.ResponseModels;

    /// <summary>
    /// Order Base Response For Company WebApi Mapper
    /// </summary>
    public static class OrderBaseResponseForCompanyMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static OrderBaseResponseForCompany CreateFrom(this DomainModels.OrderBaseResponseForCompany source)
        {
            return new OrderBaseResponseForCompany
            {
                CompanyAddresses = source.CompanyAddresses != null ? source.CompanyAddresses.Select(cc => cc.CreateFromForOrder()) : 
                new List<AddressDropDown>(),
                CompanyContacts = source.CompanyContacts != null ? source.CompanyContacts.Select(cc => cc.CreateFromDropDownForOrder()) : 
                new List<CompanyContactDropDownForOrder>(),
                TaxRate=source.TaxRate,
                JobManagerId = source.JobManagerId
            };
        }
        
    }
}