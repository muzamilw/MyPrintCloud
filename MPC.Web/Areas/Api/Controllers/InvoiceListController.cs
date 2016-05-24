using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.WebBase.Mvc;
using MPC.Interfaces.Data;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Invoice Controller 
    /// </summary>
    public class InvoiceListController : ApiController
    {
       #region Private

        private readonly IInvoiceService invoiceService;

        #endregion
       #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public InvoiceListController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        #endregion
       #region Public

        

        /// <summary>
        /// Get All Invoices
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewInvoicing })]
        [CompressFilterAttribute]
        public InvoiceListResponseModel GetInvoiceResponse([FromUri] InvoicesRequestModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
            return invoiceService.GetInvoicesList(request).CreateFromList();
        }

        
        #endregion
    }
}