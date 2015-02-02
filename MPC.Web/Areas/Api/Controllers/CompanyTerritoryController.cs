using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;

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
        public bool Get([FromUri] long companyTerritoryId)
        {
            var companyTerritory= companyTerritoryService.Get(companyTerritoryId);
            if (companyTerritory != null)
            {
                if (companyTerritory.Addresses != null || companyTerritory.CompanyContacts != null)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        #endregion

    }
}