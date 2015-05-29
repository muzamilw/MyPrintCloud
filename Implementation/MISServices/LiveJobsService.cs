using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
        private readonly IItemAttachmentRepository itemAttachmentRepository;
        private readonly ISystemUserRepository systemUserRepository;

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public LiveJobsService(IOrderRepository orderRepository, IItemAttachmentRepository itemAttachmentRepository, ISystemUserRepository systemUserRepository)
        {
            this.orderRepository = orderRepository;
            this.itemAttachmentRepository = itemAttachmentRepository;
            this.systemUserRepository = systemUserRepository;
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
        public Stream DownloadArtwork(List<long?> itemList)
        {
            List<ItemAttachment> attachments = itemAttachmentRepository.GetItemAttachmentsByIds(itemList);
            MemoryStream outputStream = new MemoryStream();
            ZipFile zip = new ZipFile();
            if (attachments != null)
            {
                string mpcContentPath = ConfigurationManager.AppSettings["MPC_Content"];
                foreach (var attachment in attachments)
                {
                    HttpServerUtility server = HttpContext.Current.Server;
                    string mapPath = server.MapPath(mpcContentPath + "/Attachments/" + orderRepository.OrganisationId + "/" + attachment.CompanyId + "/Products/");
                    string attachmentMapPath = mapPath + attachment.ItemId + "\\" + attachment.FileName + attachment.FileType;
                    if (File.Exists(attachmentMapPath))
                    {
                        zip.AddFile(attachmentMapPath, "Files");
                    }
                }
            }
            zip.Save(outputStream);
            return outputStream;
        }
        
        
       
        /// <summary>
        /// Get System Users
        /// </summary>
        public IEnumerable<SystemUser> GetSystemUsers()
        {
            return systemUserRepository.GetAll();
        }

        #endregion
    }


}

