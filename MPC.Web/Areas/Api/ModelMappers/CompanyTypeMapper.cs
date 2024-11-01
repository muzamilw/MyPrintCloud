﻿using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CompanyTypeMapper
    {
        #region public
        public static CompanyType CreateFrom(this MPC.Models.DomainModels.CompanyType source)
        {
            return new CompanyType
            {
               TypeId = source.TypeId,
               IsFixed = source.IsFixed,
               TypeName = source.TypeName
            };
        }
        public static MPC.Models.DomainModels.CompanyType CreateFrom(this CompanyType source)
        {
            return new MPC.Models.DomainModels.CompanyType
            {
                TypeId = source.TypeId,
                IsFixed = source.IsFixed,
                TypeName = source.TypeName
            };
        }
        #endregion
    }
}