using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class CompanyTerritoryController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;
        private readonly ICompanyTerritoryService companyTerritoryService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public CompanyTerritoryController(ICompanyService companyService, ICompanyTerritoryService companyTerritoryService)
        {
            this.companyService = companyService;
            this.companyTerritoryService = companyTerritoryService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Companies Territories 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        public CompanyTerritoryResponse Get([FromUri] CompanyTerritoryRequestModel request)
        {
            var result = companyService.SearchCompanyTerritories(request);
            return new CompanyTerritoryResponse
                   {
                       CompanyTerritories = result.CompanyTerritories.Select(x => x.CreateFrom()),
                       RowCount = result.RowCount
                   };
        }
        /// <summary>
        /// Get Company Territory By id
        /// </summary>
        /// <param name="companyTerritoryId"></param>
        /// <returns></returns>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        public bool Get([FromUri] long companyTerritoryId)
        {
            var companyTerritory= companyTerritoryService.Get(companyTerritoryId);
            if (companyTerritory != null)
            {
                if (companyTerritory.Addresses.Count != 0|| companyTerritory.CompanyContacts.Count != 0)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        
        [ApiException]
        [HttpPost]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        public CompanyTerritory Post(CompanyTerritory companyTerritory)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return companyTerritoryService.Save(companyTerritory.CreateFrom()).CreateFrom();
        }
        
        [ApiException]
       [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        public bool Delete(CompanyTerritoryDeleteRequest request)
        {
            if (request == null || !ModelState.IsValid || request.CompanyTerritoryId <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
            return companyTerritoryService.Delete(request.CompanyTerritoryId);
        }
        #endregion

    }
}