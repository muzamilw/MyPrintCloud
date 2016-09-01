using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.ResponseModels;
using MPC.WebBase.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MPC.Models.RequestModels;
using MPC.MIS.Areas.Api.ModelMappers;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class SpotColorController : ApiController
    {
       #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SpotColorController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        /// <summary>
        /// Delete
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewProduct })]
        [CompressFilterAttribute]
        public TemplateColorStyle Delete(SpotColorArchiveRequestModel request)
        {
            if (request == null || !ModelState.IsValid || request.SpotColorId <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return companyService.ArchiveSpotColor(request.SpotColorId).CreateFrom();
            
        }

        public SpotColorsResponseModel Get([FromUri] SpotColorArchiveRequestModel request)
        {
            var response = companyService.GetTemplateColorStyles(request.StoreId, request.TerritoryId,
                request.IsStoreColors);
            return response != null ? new SpotColorsResponseModel {SpotColors = response} : null;
        }

    }
}