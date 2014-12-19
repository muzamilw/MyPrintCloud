﻿using System;
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

        private bool validateBaseChecks(Company company)
        {
            ////1- New saving company should have 1 address and 1 user
            ////2- if company is editting then company should have a 1 address and 1 user in database after saving
            ////1
            //if (company.CompanyId == 0)
            //{
            //    if (company.NewAddedAddresses.Count == 0 || company.NewAddedCompanyContacts.Count == 0)
            //    {
            //        return false;
            //    }
            //}
            //else if (company.CompanyId > 0)
            //{
            //    var companyToBeUpdated = companyService.GetCompanyById(company.CompanyId);
            //    if (companyToBeUpdated != null &&  companyToBeUpdated.AddressResponse.RowCount+(company.NewAddedAddresses.Count - company.DeletedAddresses.Count) > 1)
            //    {
                    
            //    }
            //}
            return false;
        }

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
            companySavingModel.EdittedAddresses = company.EdittedAddresses != null? company.EdittedAddresses.Select(x => x.CreateFrom()): null;
            companySavingModel.DeletedAddresses = company.DeletedAddresses != null? company.DeletedAddresses.Select(x => x.CreateFrom()): null;
            companySavingModel.NewAddedCompanyContacts = company.NewAddedCompanyContacts != null
                ? company.NewAddedCompanyContacts.Select(x => x.Createfrom())
                : null;
            companySavingModel.EdittedCompanyContacts = company.EdittedCompanyContacts != null
                ? company.EdittedCompanyContacts.Select(x => x.Createfrom())
                : null;
            companySavingModel.DeletedCompanyContacts = company.DeletedCompanyContacts != null
                ? company.DeletedCompanyContacts.Select(x => x.Createfrom())
                : null;
            return companyService.SaveCompany(companySavingModel).CreateFrom();
        }

        public Company Delete(int companyId)
        {
            return null;//todo
        }
    }
}