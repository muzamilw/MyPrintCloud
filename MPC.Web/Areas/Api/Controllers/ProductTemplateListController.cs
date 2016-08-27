using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ProductTemplateListController : ApiController
    {
        #region Private
        private readonly ICompanyService _companyService;
        #endregion

        #region Constructor

        public ProductTemplateListController(ICompanyService companyService)
        {
            if (companyService == null)
            {
                throw new ArgumentNullException("companyService");
            }

            this._companyService = companyService;
        }
        #endregion
        #region Public

        public string Get(long id)
        {
            return string.Empty;
        }
        
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public ProductTemplateListResponseModel Get([FromUri] TemplateListRequestModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
            if(request.CategoryId == 0 && request.ParentCategoryId == 0)
                return _companyService.GetProductTemplateBase(request.StoreId, request.CategoryId);
            else
            {
                return _companyService.GetFilteredProductTemplates(request.StoreId, request.CategoryId, request.ParentCategoryId);
            }
        }

        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public string Post(TemplateListRequestModel htm)
        {
            return _companyService.ExportProductTemplates(htm.StoreId, htm.CategoryId, htm.ParentCategoryId, htm.IsPdf);

        }


        
        public bool Delete(int i)
        {
            return true;
        }
        #endregion
        

        
    }

    
}