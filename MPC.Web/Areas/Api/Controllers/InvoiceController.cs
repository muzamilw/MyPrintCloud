using System.IO;
using System.Text;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.WebBase.Mvc;
using MPC.Interfaces.Data;
using Newtonsoft.Json;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Invoice Controller 
    /// </summary>
    public class InvoiceController : ApiController
    {
       #region Private

        private readonly IInvoiceService invoiceService;

        #endregion
       #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public InvoiceController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        #endregion
       #region Public

        
        /// <summary>
        /// Get All Orders
        /// </summary>
        [CompressFilter]
        public InvoiceRequestResponseModel Get([FromUri] GetInvoicesRequestModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
            return invoiceService.SearchInvoices(request).CreateFrom();
        }

        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public Invoice Get(long id)
        {
            if (id <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return invoiceService.GetInvoiceById(id).CreateFrom();
        }

        /// <summary>
        /// Post
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public Invoice Post(Invoice request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            PostDataToZapier(request);
            return invoiceService.SaveInvoice(request.CreateFrom()).CreateFrom();
        }

        private void PostDataToZapier(Invoice invoice)
        {

            var resp = invoiceService.GetZapierInvoiceDetail(invoice.InvoiceId);
            string sData = JsonConvert.SerializeObject(resp, Formatting.None);

            //string sData = string.Empty;
            var request = System.Net.WebRequest.Create("https://zapier.com/hooks/catch/3hqi47/");
            request.ContentType = "application/json";
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(sData);
            request.ContentLength = byteArray.Length;
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                var response = request.GetResponse();
            }

        }
        #endregion
    }
}