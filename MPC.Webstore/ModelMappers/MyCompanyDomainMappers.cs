using System.IO;
using System.Linq;
using DomainResponse = MPC.Models.ResponseModels;
using ApiResponse = MPC.Webstore.ResponseModels;
using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.Webstore.Models;
using CommonModels = MPC.Models.Common;


namespace MPC.Webstore.ModelMappers
{
    public static class MyCompanyDomainMappers
    {
        #region Base Reposne Mapper
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        //public static ApiResponse.MyCompanyDomainBaseResponse CreateFrom(this DomainResponse.MyCompanyDomainBaseReponse source)
        //{
        //    return new ApiResponse.MyCompanyDomainBaseResponse
        //    {
        //        Company =  source.Company.CreateFrom(),
        //        CmsSkinPageWidgets = source.CmsSkinPageWidgets.Select(cpw => cpw.CreateFrom()).ToList(),
        //        Banners = source.Banners != null ? source.Banners.Select(banner => banner.CreateFrom()).ToList() : null,
        //        SystemPages = source.SystemPages != null ? source.SystemPages.Select(page => page.CreateFrom()).ToList() : null,
        //        SecondaryPages = source.SecondaryPages != null ? source.SecondaryPages.Select(page => page.CreateFrom()).ToList() : null,
        //        PageCategories = source.PageCategories != null ? source.PageCategories.Select(page => page.CreateFrom()).ToList() : null
              
        //    };
        //}

        //public static ApiResponse.MyCompanyDomainBaseResponse CreateFromCompany(this DomainResponse.MyCompanyDomainBaseReponse source)
        //{
        //    return new ApiResponse.MyCompanyDomainBaseResponse
        //    {
        //        Company = source.Company.CreateFrom()
        //    };
        //}

        //public static ApiResponse.MyCompanyDomainBaseResponse CreateFromOrganisation(this DomainResponse.MyCompanyDomainBaseReponse source)
        //{
        //    return new ApiResponse.MyCompanyDomainBaseResponse
        //    {
        //        Organisation = source.Organisation.CreateFrom()
        //    };
        //}


      

        //public static ApiResponse.MyCompanyDomainBaseResponse CreateFromBanner(this DomainResponse.MyCompanyDomainBaseReponse source)
        //{
        //    return new ApiResponse.MyCompanyDomainBaseResponse
        //    {
        //        Banners = source.Banners != null ? source.Banners.Select(banner => banner.CreateFrom()).ToList() : null
        //    };
        //}

        //public static ApiResponse.MyCompanyDomainBaseResponse CreateFromSecondaryPages(this DomainResponse.MyCompanyDomainBaseReponse source)
        //{
        //    return new ApiResponse.MyCompanyDomainBaseResponse
        //    {
        //        SecondaryPages = source.SecondaryPages != null ? source.SecondaryPages.Select(page => page.CreateFrom()).ToList() : null,
        //        PageCategories = source.PageCategories != null ? source.PageCategories.Select(page => page.CreateFrom()).ToList() : null
              
        //    };
        //}

        //public static ApiResponse.MyCompanyDomainBaseResponse CreateFromCurrency(this DomainResponse.MyCompanyDomainBaseReponse source)
        //{
        //    return new ApiResponse.MyCompanyDomainBaseResponse
        //    {
        //        Currency = source.Currency
              
        //    };
        //}

        //public static ApiResponse.MyCompanyDomainBaseResponse CreateFromResxFile(this DomainResponse.MyCompanyDomainBaseReponse source)
        //{
        //    return new ApiResponse.MyCompanyDomainBaseResponse
        //    {
        //        ResourceFile = source.ResourceFile
        //    };
        //}
        #endregion
    }
    
}