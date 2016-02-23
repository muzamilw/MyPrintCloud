using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class DownloadExternalReportController : ApiController
    {
        
         #region Private

        private readonly IReportService reportService;
      
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public DownloadExternalReportController(IReportService reportService)
        {
            this.reportService = reportService;
          
        }

        #endregion


        [HttpGet]
        public HttpResponseMessage DownloadExternalReport(int ReportId, int StoreId,string Datefrom,string DateTo,string CustomerPO,int? OrderStatus)
        {
            
            try
            {
                string path = reportService.DownloadExternalReportWebStore(ReportId, StoreId, Datefrom, DateTo, CustomerPO, OrderStatus);
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
            catch (Exception ex)
            {
                throw ex;

            }

        }


    }

     

}