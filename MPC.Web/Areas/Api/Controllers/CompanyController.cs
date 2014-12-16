using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;
using PagedList;

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
                Companies = result.Companies.Select(x => x.CreateFrom()),
                RowCount = result.RowCount
            };
        }

        /// <summary>
        /// Get Company By Id
        /// </summary>
        /// <returns></returns>
        public Company Get(int companyId)
        {
            return companyService.GetCompanyById(companyId).CreateFrom();
        }
        /// <summary>
        /// Add/Update Company
        /// </summary>
        [ApiException]
        [HttpPost]
        public Company Post(Company company)
        {
            //FormCollection
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
            return companyService.SaveCompany(companySavingModel).CreateFrom();
        }
    }
}