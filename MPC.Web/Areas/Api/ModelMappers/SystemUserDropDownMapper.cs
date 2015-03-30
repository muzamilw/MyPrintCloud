using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class SystemUserDropDownMapper
    {
        public static SystemUserDropDown CreateFrom(this MPC.Models.DomainModels.SystemUser source)
        {
            return new SystemUserDropDown
                   {
                       SystemUserId = source.SystemUserId,
                       UserName = source.UserName,
                       FullName = source.FullName,
                       VariableString = GetVariableString(source)
                   };
        }

        private static string GetVariableString(MPC.Models.DomainModels.SystemUser source)
        {
            string sv = "{resource, ID="+source.Email+",Name="+source.FullName+",returnvalue=costperhour}";
            return sv;
        }
    }
}