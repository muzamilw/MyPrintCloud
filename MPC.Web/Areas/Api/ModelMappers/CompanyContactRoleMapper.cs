using ApiModels = MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CompanyContactRoleMapper
    {
        public static ApiModels.CompanyContactRole CreateFrom(this DomainModels.CompanyContactRole source)
        {
            return new ApiModels.CompanyContactRole
            {
                ContactRoleId = source.ContactRoleId,
                ContactRoleName = source.ContactRoleName
            };
        }
    }
}