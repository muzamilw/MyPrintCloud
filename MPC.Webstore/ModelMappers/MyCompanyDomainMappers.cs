using System;
using System.Collections.Generic;
using System.Linq;
using DomainResponse = MPC.Models.ResponseModels;
using ApiResponse = MPC.Webstore.ResponseModels;


namespace MPC.Webstore.ModelMappers
{
    public class MyCompanyDomainMappers
    {
        #region Base Reposne Mapper
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiResponse.MyCompanyDomainBaseResponse CreateFrom(DomainResponse.MyCompanyDomainBaseReponse source)
        {
            return new ApiResponse.MyCompanyDomainBaseResponse
            {
               CompanyDomain = CompanyDomainMapper.CreateFrom(source.CompanyDomain)
            };
        }

        #endregion
    }
    
}