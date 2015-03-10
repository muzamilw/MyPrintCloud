using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.MIS.ModelMappers;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;
using Newtonsoft.Json;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class CompanyController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion
        /// <summary>
        /// Get All Companies Of Organisation
        /// </summary>
        /// <returns></returns>
        public CompanyResponse Get([FromUri] CompanyRequestModel request)
        {
            var result = companyService.GetAllCompaniesOfOrganisation(request);
            return new CompanyResponse
            {
                Companies = result.Companies.Select(x => x.ListViewModelCreateFrom()),
                RowCount = result.RowCount
            };
        }

        /// <summary>
        /// Get Company By Id
        /// </summary>
        /// <returns></returns>
        public CompanyResponse Get([FromUri]int companyId)
        {
            //var result = companyService.GetCompanyById(companyId);

            CompanyResponse companyResponse = companyService.GetCompanyById(companyId).CreateFrom();

            return companyResponse;
        }
        /// <summary>
        /// Add/Update Company
        /// </summary>
        [ApiException]
        [HttpPost]
        public Company Post(Company company)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            CompanySavingModel companySavingModel = new CompanySavingModel();
            companySavingModel.Company = company.CreateFrom();
            companySavingModel.NewAddedCompanyTerritories = company.NewAddedCompanyTerritories != null ? company.NewAddedCompanyTerritories.Select(x => x.CreateFrom()) : null;
            companySavingModel.EdittedCompanyTerritories = company.EdittedCompanyTerritories != null ? company.EdittedCompanyTerritories.Select(x => x.CreateFrom()) : null;
            companySavingModel.DeletedCompanyTerritories = company.DeletedCompanyTerritories != null ? company.DeletedCompanyTerritories.Select(x => x.CreateFrom()) : null;
            companySavingModel.NewAddedAddresses = company.NewAddedAddresses != null ? company.NewAddedAddresses.Select(x => x.CreateFrom()) : null;
            companySavingModel.EdittedAddresses = company.EdittedAddresses != null ? company.EdittedAddresses.Select(x => x.CreateFrom()) : null;
            companySavingModel.DeletedAddresses = company.DeletedAddresses != null ? company.DeletedAddresses.Select(x => x.CreateFrom()) : null;

            companySavingModel.NewProductCategories = company.NewProductCategories != null ? company.NewProductCategories.Select(x => x.CreateFrom()) : null;
            companySavingModel.EdittedProductCategories = company.EdittedProductCategories != null ? company.EdittedProductCategories.Select(x => x.CreateFrom()) : null;
            companySavingModel.DeletedProductCategories = company.DeletedProductCategories != null ? company.DeletedProductCategories.Select(x => x.CreateFrom()) : null;
            companySavingModel.NewAddedCompanyContacts = company.NewAddedCompanyContacts != null
                ? company.NewAddedCompanyContacts.Select(x => x.Createfrom())
                : null;
            companySavingModel.EdittedCompanyContacts = company.EdittedCompanyContacts != null
                ? company.EdittedCompanyContacts.Select(x => x.Createfrom())
                : null;
            companySavingModel.DeletedCompanyContacts = company.DeletedCompanyContacts != null
                ? company.DeletedCompanyContacts.Select(x => x.Createfrom())
                : null;

            companySavingModel.NewAddedCmsPages = company.NewAddedCmsPages != null ? company.NewAddedCmsPages.Select(x => x.CreateFrom()).ToList() : null;
            companySavingModel.EditCmsPages = company.EditCmsPages != null ? company.EditCmsPages.Select(x => x.CreateFrom()).ToList() : null;
            companySavingModel.DeletedCmsPages = company.DeletedCmsPages != null ? company.DeletedCmsPages.Select(x => x.CreateFrom()).ToList() : null;
            companySavingModel.PageCategories = company.PageCategories != null ? company.PageCategories.Select(x => x.CreateFrom()).ToList() : null;
            companySavingModel.CmsPageWithWidgetList = company.CmsPageWithWidgetList != null ? company.CmsPageWithWidgetList.Select(x => x.CreateFrom()).ToList() : null;

            companySavingModel.NewAddedProducts = company.NewAddedProducts != null ? company.NewAddedProducts.Select(x => x.CreateFromForCompany()) : null;
            companySavingModel.EdittedProducts = company.EdittedProducts != null ? company.EdittedProducts.Select(x => x.CreateFromForCompany()) : null;
            companySavingModel.Deletedproducts = company.Deletedproducts != null ? company.Deletedproducts.Select(x => x.CreateFromForCompany()) : null;

            //companySavingModel.CompanyCostCentres = company.CompanyCostCentres != null ? company.CompanyCostCentres.Select(x => x.CreateFrom()).ToList() : null;

            return companyService.SaveCompany(companySavingModel).CreateFrom();
        }
        [HttpDelete]
        public Company Delete(CompanyRequestModel model)
        {
            return companyService.DeleteCompany(model.CompanyId).CreateFrom();
        }
    }
}