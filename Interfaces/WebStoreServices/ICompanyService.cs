﻿using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.WebStoreServices
{
    /// <summary>
    /// My Organization Service Interface
    /// </summary>
    public interface ICompanyService
    {

        MyCompanyDomainBaseReponse GetBaseData(long companyId);
        long GetCompanyIdByDomain(string domain);
        List<ProductCategory> GetCompanyParentCategoriesById(long companyId);
        CompanyResponse GetAllCompaniesOfOrganisation(CompanyRequestModel request);
        List<CmsPage> GetSecondaryPages(long companyId);

        List<PageCategory> GetSecondaryPageCategories();
    }
}
