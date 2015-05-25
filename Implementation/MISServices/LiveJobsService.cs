using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using Ionic.Zip;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Live Jobs Service
    /// </summary>
    public class LiveJobsService : ILiveJobsService
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly IOrderRepository orderRepository;

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public LiveJobsService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Items For Live Job Items 
        /// </summary>
        public LiveJobsSearchResponse GetItemsForLiveJobs(LiveJobsRequestModel request)
        {
            return orderRepository.GetEstimatesForLiveJobs(request);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Stream DownloadArtwork()
        {
            string filePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Attachments/1/32845/Invoices/57281/abc.docx");
            string filePath1 = HttpContext.Current.Server.MapPath("~/MPC_Content/Attachments/1/32845/Invoices/57281/abc.xlsx");
            string filePath2 = HttpContext.Current.Server.MapPath("~/MPC_Content/Attachments/1/32845/Invoices/57281/abc.txt");
            string filePath3 = HttpContext.Current.Server.MapPath("~/MPC_Content/Attachments/1/32845/Invoices/57281/abc.png");
            MemoryStream outputStream = new MemoryStream();

            using (var zip = new ZipFile())
            {

                //zip.AddEntry(filePath);
                zip.AddFile(filePath, "Files");
                zip.AddFile(filePath1, "Files");
                zip.AddFile(filePath2, "Files");
                zip.AddFile(filePath3, "Files");

                zip.Save(outputStream);
            }
            return outputStream;
        }

        #endregion
    }
}
