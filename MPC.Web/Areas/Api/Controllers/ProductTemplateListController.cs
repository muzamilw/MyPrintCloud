using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Models.DomainModels;
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
        
        public string Get()
        {
            return string.Empty;
        }
        
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public List<usp_GetStoreProductTemplatesList_Result> Get(long id)
        {
           return _companyService.GetProductTemplatesListByStoreId(id);
        }

        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public string Post(HtmlData htm)
        {
            return _companyService.ExportProductTemplates(htm.Html);

        }


        
        public bool Delete(int i)
        {
            return true;
        }
        #endregion
        

        
    }

    public class HtmlData
    {
        public string Html { get; set; }
    }
}