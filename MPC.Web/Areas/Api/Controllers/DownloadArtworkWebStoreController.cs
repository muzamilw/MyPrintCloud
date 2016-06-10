using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class DownloadArtworkWebStoreController : ApiController
    {
        #region Private

        private readonly IOrderService orderService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DownloadArtworkWebStoreController(IOrderService orderService)
        {
            if (orderService == null)
            {
                throw new ArgumentNullException("orderService");
            }
            this.orderService = orderService;
        }

        #endregion

        #region Public
       // [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public HttpResponseMessage Get(int OrderId, long OrganisationId)
        {
           string path = orderService.DownloadOrderArtwork(OrderId, string.Empty, OrganisationId);
           if (!string.IsNullOrEmpty(path))
           {
               string sFilePath = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + "/" + path;
               var response = Request.CreateResponse(HttpStatusCode.Found);
               response.Headers.Location = new Uri(sFilePath);
               return response;
           }
           else
           {
               return Request.CreateResponse(HttpStatusCode.NotFound);
           } 
            
        }
        public HttpResponseMessage Get(int OrderId, long OrganisationId, int formatxml)
        {
            string path = orderService.DownloadOrderXml(OrderId, OrganisationId);
            if (!string.IsNullOrEmpty(path))
            {
                int contentIndex = path.IndexOf("MPC_Content");
                string sRelativePath = path.Substring(contentIndex, path.Length - contentIndex);

                string sFilePath = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + "/" + sRelativePath;
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(sFilePath);
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            } 
            
        }
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [CompressFilterAttribute]
        public HttpResponseMessage DwonloadDigitalArtwork(int orderId, long organisationId)
        {
            string path = orderService.GenerateDigitalItemsArtwork(orderId, organisationId);
            if (!string.IsNullOrEmpty(path))
            {
                string sFilePath = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + "/" + path;
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(sFilePath);
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            } 

        }
        #endregion
    }
}
