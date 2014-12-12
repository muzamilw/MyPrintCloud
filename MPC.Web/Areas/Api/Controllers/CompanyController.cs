﻿using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;

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
            var result = companyService.GetCompanyById(companyId);
            
            return companyService.GetCompanyById(companyId).CreateFrom();
        }
        /// <summary>
        /// Add/Update Company
        /// </summary>
        [ApiException]
        public Company Post(Company company)
        {
            if (company == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            CompanySavingModel companySavingModel = new CompanySavingModel();
            companySavingModel.Company = company.CreateFrom();
            companySavingModel.NewAddedCompanyTerritories = company.NewAddedCompanyTerritories!=null?company.NewAddedCompanyTerritories.Select(x => x.CreateFrom()):null;
            companySavingModel.EdittedCompanyTerritories = company.EdittedCompanyTerritories!=null?company.EdittedCompanyTerritories.Select(x => x.CreateFrom()):null;
            companySavingModel.DeletedCompanyTerritories = company.DeletedCompanyTerritories!=null?company.DeletedCompanyTerritories.Select(x => x.CreateFrom()):null;
            companySavingModel.NewAddedAddresses = company.NewAddedAddresses.Select(x => x.CreateFrom());
            companySavingModel.EdittedAddresses = company.EdittedAddresses.Select(x => x.CreateFrom());
            companySavingModel.DeletedAddresses = company.DeletedAddresses.Select(x => x.CreateFrom());
            return companyService.SaveCompany(companySavingModel).CreateFrom();
        }

        public Company Delete(int companyId)
        {
            return null;//todo
        }
    }
}