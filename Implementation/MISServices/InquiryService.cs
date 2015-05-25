using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.ModelMappers;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.Repositories;

namespace MPC.Implementation.MISServices
{
    public class InquiryService : IInquiryService
    {
        #region Private
        private readonly IEstimateInquiryRepository estimateInquiryRepository;
        private readonly IInquiryItemRepository inquiryItemRepository;
        private readonly IOrganisationRepository organisationRepository;
        private readonly IPrefixRepository prefixRepository;
        private readonly IEstimateRepository estimateRepository;
        private readonly IInquiryAttachmentRepository inquiryAttachmentRepository;

        private InquiryAttachment CreateInquiryAttachment()
        {
            InquiryAttachment inquiryAttachment = inquiryAttachmentRepository.Create();
            inquiryAttachmentRepository.Add(inquiryAttachment);
            return inquiryAttachment;
        }

        /// <summary>
        /// Delete Inquiry Attachment
        /// </summary>
        private void DeleteInquiryAttachment(InquiryAttachment item)
        {
            inquiryAttachmentRepository.Delete(item);
        }
        /// <summary>
        /// Save Inquiry Attachments
        /// </summary>
        /// <param name="inquiry"></param>
        private void SaveInquiryAttachments(Inquiry inquiry)
        {
            string mpcContentPath = ConfigurationManager.AppSettings["MPC_Content"];
            HttpServerUtility server = HttpContext.Current.Server;
            string mapPath = server.MapPath(mpcContentPath + "/Attachments/" + estimateInquiryRepository.OrganisationId + "/" + inquiry.CompanyId + "/Inquiries/");

            if (inquiry.InquiryAttachments == null)
            {
                return;
            }
            string attachmentMapPath = mapPath + inquiry.InquiryId;
            DirectoryInfo directoryInfo = null;
            // Create directory if not there
            if (!Directory.Exists(attachmentMapPath))
            {
                directoryInfo = Directory.CreateDirectory(attachmentMapPath);
            }
            foreach (InquiryAttachment inquiryAttachment in inquiry.InquiryAttachments)
            {
                string folderPath = directoryInfo != null ? directoryInfo.FullName : attachmentMapPath;
                int indexOf = folderPath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                folderPath = folderPath.Substring(indexOf, folderPath.Length - indexOf);
                inquiryAttachment.AttachmentPath = folderPath;
                if (SaveImage(attachmentMapPath, inquiryAttachment.AttachmentPath, "",
                    inquiryAttachment.OrignalFileName,
                    inquiryAttachment.FileSource, inquiryAttachment.FileSourceBytes) != null)
                {
                    //inquiryAttachment.Extension = "jpg";
                    inquiryAttachment.OrignalFileName = inquiryAttachment.OrignalFileName;
                }
            }
        }
        private string SaveImage(string mapPath, string existingImage, string caption, string fileName,
            string fileSource, byte[] fileSourceBytes)
        {
            if (!string.IsNullOrEmpty(fileSource))
            {
                // Look if file already exists then replace it
                if (!string.IsNullOrEmpty(existingImage))
                {
                    if (Path.IsPathRooted(existingImage))
                    {
                        if (File.Exists(existingImage))
                        {
                            // Remove Existing File
                            File.Delete(existingImage);
                        }
                    }
                    else
                    {
                        string filePath = HttpContext.Current.Server.MapPath("~/" + existingImage);
                        if (File.Exists(filePath))
                        {
                            // Remove Existing File
                            File.Delete(filePath);
                        }
                    }

                }
                if (fileSourceBytes != null)
                {
                    string imageurl = mapPath + "\\" + caption + fileName;
                    File.WriteAllBytes(imageurl, fileSourceBytes);
                    int indexOf = imageurl.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                    imageurl = imageurl.Substring(indexOf, imageurl.Length - indexOf);
                    return imageurl;
                }
                // First Time Upload



            }

            return null;
        }
        /// <summary>
        /// Create New Inquiry Item
        /// </summary>
        /// <returns></returns>
        private InquiryItem CreateNewInquiryItem()
        {
            InquiryItem itemTarget = new InquiryItem();
            inquiryItemRepository.Add(itemTarget);
            return itemTarget;
        }
        /// <summary>
        /// Delete Inquiry Item
        /// </summary>
        private void DeleteInquiryItem(InquiryItem inquiryItem)
        {
            inquiryItemRepository.Delete(inquiryItem);
        }
        #endregion
        #region Constructor
        public InquiryService(IOrganisationRepository organisationRepository, IEstimateInquiryRepository estimateInquiryRepository, IPrefixRepository prefixRepository, IInquiryItemRepository inquiryItemRepository, IEstimateRepository estimateRepository, IInquiryAttachmentRepository inquiryAttachmentRepository)
        {
            this.organisationRepository = organisationRepository;
            this.estimateInquiryRepository = estimateInquiryRepository;
            this.prefixRepository = prefixRepository;
            this.inquiryItemRepository = inquiryItemRepository;
            this.estimateRepository = estimateRepository;
            this.inquiryAttachmentRepository = inquiryAttachmentRepository;
        }
        private Inquiry CreateNewInquiry()
        {
            string inquiryCode = prefixRepository.GetNextInquiryCodePrefix();
            Inquiry itemTarget = estimateInquiryRepository.Create();
            estimateInquiryRepository.Add(itemTarget);
            itemTarget.CreatedDate = itemTarget.CreatedDate = DateTime.Now;
            itemTarget.InquiryCode = inquiryCode;
            itemTarget.OrganisationId = estimateInquiryRepository.OrganisationId;
            return itemTarget;
        }
        #endregion
        #region Public
        /// <summary>
        /// Get All Orders
        /// </summary>
        public GetInquiryResponse GetAll(GetInquiryRequest request)
        {
            return estimateInquiryRepository.GetInquiries(request);
        }
        /// <summary>
        /// Add Inquiry
        /// </summary>
        /// <param name="inquiry"></param>
        /// <returns></returns>
        public Inquiry Add(Inquiry inquiry)
        {
            inquiry.OrganisationId = organisationRepository.OrganisationId;
            inquiry.InquiryCode = prefixRepository.GetNextInquiryCodePrefix();
            estimateInquiryRepository.Add(inquiry);
            estimateInquiryRepository.SaveChanges();
            return inquiry;
        }

        /// <summary>
        /// Update Inquiry
        /// </summary>
        public Inquiry Update(Inquiry recievedInquiry)
        {
            Inquiry inquiry = GetInquiryById(recievedInquiry.InquiryId) ?? CreateNewInquiry();
            // Update Inquiry
            recievedInquiry.UpdateTo(inquiry, new InquiryMapperActions
            {
                CreateInquiryItem = CreateNewInquiryItem,
                DeleteInquiryItem = DeleteInquiryItem,
                CreateInquiryAttachment = CreateInquiryAttachment,
                DeleteInquiryAttachment = DeleteInquiryAttachment,
            });
            SaveInquiryAttachments(inquiry);
            // Save Changes
            estimateInquiryRepository.SaveChanges();
            return inquiry;

        }
        /// <summary>
        /// Creates New Inquiry Attachment new generated code
        /// </summary>

        /// <summary>
        /// Delete Inquiry
        /// </summary>
        /// <param name="inquiryId"></param>
        /// <returns></returns>
        public bool Delete(int inquiryId)
        {
            Inquiry paperSize = GetInquiryById(inquiryId);
            //todo delete work
            estimateInquiryRepository.SaveChanges();
            return true;
        }
        public Inquiry GetInquiryById(int id)
        {
            var estimateId = estimateRepository.GetEstimateIdOfInquiry(id);
            Inquiry inquiry = estimateInquiryRepository.Find(id);
            inquiry.EstimateId = estimateId;
            return inquiry;
        }
        public void ProgressInquiryToEstimate(long inquiryId)
        {
            Inquiry inquiry = estimateInquiryRepository.Find(inquiryId);
            if (inquiry != null)
            {
                inquiry.Status = 26;
            }
            estimateInquiryRepository.Update(inquiry);
            estimateInquiryRepository.SaveChanges();

        }

        /// <summary>
        /// Get Inquiry Items By Inquiry Id
        /// </summary>
        /// <param name="inquiryId"></param>
        /// <returns></returns>
        public IEnumerable<InquiryItem> GetInquiryItems(int inquiryId)
        {
            return estimateInquiryRepository.GetInquiryItems(inquiryId);
        }
        #endregion
    }
}
