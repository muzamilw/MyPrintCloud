using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MPC.Interfaces.MISServices;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class DigitalArtworkDownloadController : ApiController
    {

        private readonly IOrderService _orderService;
        public DigitalArtworkDownloadController(IOrderService orderService)
        {
            if (orderService == null)
            {
                throw new ArgumentNullException("orderService");
            }
            this._orderService = orderService;
        }
        public HttpResponseMessage Get(int orderId, long organisationId)
        {
            string path = _orderService.GenerateDigitalItemsArtwork(orderId, organisationId);
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

        
    }
}